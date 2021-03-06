﻿using System;
using SpiceSharp.Algebra;
using SpiceSharp.Behaviors;
using SpiceSharp.IntegrationMethods;
using SpiceSharp.Simulations;

namespace SpiceSharp.Components.JFETBehaviors
{
    /// <summary>
    /// Transient behavior for a <see cref="JFET" />.
    /// </summary>
    /// <seealso cref="SpiceSharp.Behaviors.BaseTransientBehavior" />
    /// <seealso cref="SpiceSharp.Components.IConnectedBehavior" />
    public class TransientBehavior : BiasingBehavior, ITimeBehavior
    {
        /// <summary>
        /// States
        /// </summary>
        protected StateDerivative Qgs { get; private set; }
        protected StateDerivative Qgd { get; private set; }

        /// <summary>
        /// Gets the G-S capacitance.
        /// </summary>
        /// <value>
        /// The G-S capacitance.
        /// </value>
        public double CapGs { get; private set; }

        /// <summary>
        /// Gets the G-D capacitance.
        /// </summary>
        /// <value>
        /// The G-D capacitance.
        /// </value>
        public double CapGd { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TransientBehavior"/> class.
        /// </summary>
        /// <param name="name">The identifier of the behavior.</param>
        /// <remarks>
        /// The identifier of the behavior should be the same as that of the entity creating it.
        /// </remarks>
        public TransientBehavior(string name) : base(name)
        {
        }

        /// <summary>
        /// Allocate elements in the Y-matrix and Rhs-vector to populate during loading. Additional
        /// equations can also be allocated here.
        /// </summary>
        /// <param name="solver">The solver.</param>
        public void GetEquationPointers(Solver<double> solver)
        {
            // No extra pointers needed
        }

        /// <summary>
        /// Creates all necessary states for the transient behavior.
        /// </summary>
        /// <param name="method">The integration method.</param>
        public void CreateStates(IntegrationMethod method)
        {
            Qgs = method.CreateDerivative();
            Qgd = method.CreateDerivative();
        }
        
        /// <summary>
        /// Calculates the state values from the current DC solution.
        /// </summary>
        /// <param name="simulation">Time-based simulation</param>
        /// <remarks>
        /// In this method, the initial value is calculated based on the operating point solution,
        /// and the result is stored in each respective <see cref="T:SpiceSharp.IntegrationMethods.StateDerivative" /> or <see cref="T:SpiceSharp.IntegrationMethods.StateHistory" />.
        /// </remarks>
        public void GetDcState(TimeSimulation simulation)
        {
            var vgs = Vgs;
            var vgd = Vgd;
            CalculateStates(vgs, vgd);
        }

        /// <summary>
        /// Perform time-dependent calculations.
        /// </summary>
        /// <param name="simulation">The time-based simulation.</param>
        public void Transient(TimeSimulation simulation)
        {
            // Calculate the states
            var vgs = Vgs;
            var vgd = Vgd;
            CalculateStates(vgs, vgd);

            // Integrate and add contributions
            Qgs.Integrate();
            var ggs = Qgs.Jacobian(CapGs);
            var cg = Qgs.Derivative;
            Qgd.Integrate();
            var ggd = Qgd.Jacobian(CapGd);
            cg = cg + Qgd.Derivative;
            var cd = -Qgd.Derivative;
            var cgd = Qgd.Derivative;

            var ceqgd = ModelParameters.JFETType * (cgd - ggd * vgd);
            var ceqgs = ModelParameters.JFETType * (cg - cgd - ggs * vgs);
            var cdreq = ModelParameters.JFETType * (cd + cgd);
            GateNodePtr.Value += -ceqgs - ceqgd;
            DrainPrimeNodePtr.Value += -cdreq + ceqgd;
            SourcePrimeNodePtr.Value += cdreq + ceqgs;

            // Load Y-matrix
            GateDrainPrimePtr.Value += -ggd;
            GateSourcePrimePtr.Value += -ggs;
            DrainPrimeGatePtr.Value += -ggd;
            SourcePrimeGatePtr.Value += -ggs;
            GateGatePtr.Value += ggd + ggs;
            DrainPrimeDrainPrimePtr.Value += ggd;
            SourcePrimeSourcePrimePtr.Value += ggs;
        }

        /// <summary>
        /// Calculates the states.
        /// </summary>
        /// <param name="vgs">The VGS.</param>
        /// <param name="vgd">The VGD.</param>
        private void CalculateStates(double vgs, double vgd)
        {
            // Charge storage elements
            var czgs = TempCapGs * BaseParameters.Area;
            var czgd = TempCapGd * BaseParameters.Area;
            var twop = TempGatePotential + TempGatePotential;
            var fcpb2 = CorDepCap * CorDepCap;
            var czgsf2 = czgs / ModelTemperature.F2;
            var czgdf2 = czgd / ModelTemperature.F2;
            if (vgs < CorDepCap)
            {
                var sarg = Math.Sqrt(1 - vgs / TempGatePotential);
                Qgs.Current = twop * czgs * (1 - sarg);
                CapGs = czgs / sarg;
            }
            else
            {
                Qgs.Current = czgs * F1 + czgsf2 *
                              (ModelTemperature.F3 * (vgs - CorDepCap) + (vgs * vgs - fcpb2) / (twop + twop));
                CapGs = czgsf2 * (ModelTemperature.F3 + vgs / twop);
            }

            if (vgd < CorDepCap)
            {
                var sarg = Math.Sqrt(1 - vgd / TempGatePotential);
                Qgd.Current = twop * czgd * (1 - sarg);
                CapGd = czgd / sarg;
            }
            else
            {
                Qgd.Current = czgd * F1 + czgdf2 *
                              (ModelTemperature.F3 * (vgd - CorDepCap) + (vgd * vgd - fcpb2) / (twop + twop));
                CapGd = czgdf2 * (ModelTemperature.F3 + vgd / twop);
            }
        }
    }
}
