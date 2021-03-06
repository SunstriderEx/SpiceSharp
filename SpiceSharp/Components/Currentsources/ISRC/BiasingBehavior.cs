﻿using System;
using SpiceSharp.Algebra;
using SpiceSharp.Attributes;
using SpiceSharp.Behaviors;
using SpiceSharp.Simulations;
using SpiceSharp.Simulations.Behaviors;

namespace SpiceSharp.Components.CurrentSourceBehaviors
{
    /// <summary>
    /// DC biasing behavior for a <see cref="CurrentSource" />.
    /// </summary>
    /// <remarks>
    /// This behavior also includes transient behavior logic. When transient analysis is
    /// performed, then waveforms need to be used to calculate the operating point anyway.
    /// </remarks>
    public class BiasingBehavior : ExportingBehavior, IBiasingBehavior, IConnectedBehavior
    {
        /// <summary>
        /// Necessary behaviors and parameters
        /// </summary>
        protected CommonBehaviors.IndependentBaseParameters BaseParameters { get; private set; }

        /// <summary>
        /// Gets voltage across the voltage source
        /// </summary>
        /// <param name="state"></param>
        /// <returns></returns>
        [ParameterName("v"), ParameterInfo("Voltage accross the supply")]
        public double GetVoltage(BaseSimulationState state)
        {
			if (state == null)
				throw new ArgumentNullException(nameof(state));
            return state.Solution[PosNode] - state.Solution[NegNode];
        }
        [ParameterName("p"), ParameterInfo("Power supplied by the source")]
        public double GetPower(BaseSimulationState state)
        {
			if (state == null)
				throw new ArgumentNullException(nameof(state));
            return (state.Solution[PosNode] - state.Solution[PosNode]) * -Current;
        }
        [ParameterName("c"), ParameterName("i"), ParameterInfo("Current through current source")]
        public double Current { get; protected set; }

        /// <summary>
        /// Nodes
        /// </summary>
        protected int PosNode { get; private set; }
        protected int NegNode { get; private set; }
        protected VectorElement<double> PosPtr { get; private set; }
        protected VectorElement<double> NegPtr { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public BiasingBehavior(string name) : base(name) { }

        /// <summary>
        /// Setup behavior
        /// </summary>
        /// <param name="simulation">Simulation</param>
        /// <param name="provider">Data provider</param>
        public override void Setup(Simulation simulation, SetupDataProvider provider)
        {
            if (provider == null)
                throw new ArgumentNullException(nameof(provider));

            // Get parameters
            BaseParameters = provider.GetParameterSet<CommonBehaviors.IndependentBaseParameters>();

            // Setup the waveform
            BaseParameters.Waveform?.Setup();

            // Give some warnings if no value is given
            if (!BaseParameters.DcValue.Given)
            {
                // no DC value - either have a transient value or none
                CircuitWarning.Warning(this,
                    BaseParameters.Waveform != null
                        ? "{0} has no DC value, transient time 0 value used".FormatString(Name)
                        : "{0} has no value, DC 0 assumed".FormatString(Name));
            }
        }
        
        /// <summary>
        /// Connect the behavior
        /// </summary>
        /// <param name="pins">Pins</param>
        public void Connect(params int[] pins)
        {
            if (pins == null)
                throw new ArgumentNullException(nameof(pins));
            if (pins.Length != 2)
                throw new CircuitException("Pin count mismatch: 2 pins expected, {0} given".FormatString(pins.Length));
            PosNode = pins[0];
            NegNode = pins[1];
        }

        /// <summary>
        /// Get the matrix elements
        /// </summary>
        /// <param name="variables">Variables</param>
        /// <param name="solver">Solver</param>
        public void GetEquationPointers(VariableSet variables, Solver<double> solver)
        {
            if (solver == null)
                throw new ArgumentNullException(nameof(solver));

            PosPtr = solver.GetRhsElement(PosNode);
            NegPtr = solver.GetRhsElement(NegNode);
        }

        /// <summary>
        /// Execute behavior
        /// </summary>
        /// <param name="simulation">Base simulation</param>
        public void Load(BaseSimulation simulation)
        {
            if (simulation == null)
                throw new ArgumentNullException(nameof(simulation));

            var state = simulation.RealState;
            double value;

            // Time domain analysis
            if (simulation is TimeSimulation)
            {
                // Use the waveform if possible
                if (BaseParameters.Waveform != null)
                    value = BaseParameters.Waveform.Value;
                else
                    value = BaseParameters.DcValue * state.SourceFactor;
            }
            else
            {
                // AC or DC analysis use the DC value
                value = BaseParameters.DcValue * state.SourceFactor;
            }

            // NOTE: Spice 3f5's documentation is IXXXX POS NEG VALUE but in the code it is IXXXX NEG POS VALUE
            // I solved it by inverting the current when loading the rhs vector
            PosPtr.Value -= value;
            NegPtr.Value += value;
            Current = value;
        }

        /// <summary>
        /// Tests convergence at the device-level.
        /// </summary>
        /// <param name="simulation">The base simulation.</param>
        /// <returns>
        /// <c>true</c> if the device determines the solution converges; otherwise, <c>false</c>.
        /// </returns>
        public bool IsConvergent(BaseSimulation simulation) => true;
    }
}
