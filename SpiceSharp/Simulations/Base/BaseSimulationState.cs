﻿using System;
using SpiceSharp.Algebra;

namespace SpiceSharp.Simulations
{
    /// <summary>
    /// A simulation state for simulations using real numbers.
    /// </summary>
    /// <seealso cref="SimulationState" />
    public class BaseSimulationState : SimulationState
    {
        /// <summary>
        /// Gets or sets the initialization flag.
        /// </summary>
        /// <value>
        /// The flag.
        /// </value>
        public InitializationModes Init { get; set; }

        /// <summary>
        /// Gets or sets the flag for ignoring time-related effects. If true, each device should assume the circuit is not moving in time.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the simulation assumes a DC solution; otherwise, <c>false</c>.
        /// </value>
        public bool UseDc { get; set; }

        /// <summary>
        /// Gets or sets the flag for using initial conditions. If true, the operating point will not be calculated, and initial conditions will be used instead.
        /// </summary>
        /// <value>
        ///   <c>true</c> if [use ic]; otherwise, <c>false</c>.
        /// </value>
        public bool UseIc { get; set; }

        /// <summary>
        /// The current source factor.
        /// This parameter is changed when doing source stepping for aiding convergence.
        /// </summary>
        /// <remarks>
        /// In source stepping, all sources are considered to be at 0 which has typically only one single solution (all nodes and
        /// currents are 0V and 0A). By increasing the source factor in small steps, it is possible to progressively reach a solution
        /// without having non-convergence.
        /// </remarks>
        public double SourceFactor { get; set; } = 1.0;

        /// <summary>
        /// Gets or sets the a conductance that is shunted with PN junctions to aid convergence.
        /// </summary>
        /// <value>
        /// The conductance.
        /// </value>
        public double Gmin { get; set; } = 1e-12;

        /// <summary>
        /// Is the current iteration convergent?
        /// This parameter is used to communicate convergence.
        /// </summary>
        /// <value>
        ///   <c>true</c> if the iterations converged; otherwise, <c>false</c>.
        /// </value>
        public bool IsConvergent { get; set; } = true;

        /// <summary>
        /// The current temperature for this circuit in Kelvin.
        /// </summary>
        /// <value>
        /// The temperature.
        /// </value>
        public double Temperature { get; set; } = 300.15;

        /// <summary>
        /// The nominal temperature for the circuit in Kelvin.
        /// Used by models as the default temperature where the parameters were measured.
        /// </summary>
        /// <value>
        /// The nominal temperature.
        /// </value>
        public double NominalTemperature { get; set; } = 300.15;

        /// <summary>
        /// Gets the solver for solving linear systems of equations.
        /// </summary>
        /// <value>
        /// The solver.
        /// </value>
        public RealSolver Solver { get; } = new RealSolver();

        /// <summary>
        /// Gets the solution vector.
        /// </summary>
        /// <value>
        /// The solution.
        /// </value>
        public Vector<double> Solution { get; private set; }

        /// <summary>
        /// Gets the previous solution vector.
        /// </summary>
        /// <value>
        /// The old solution.
        /// </value>
        /// <remarks>
        /// This vector is needed for determining convergence.
        /// </remarks>
        public Vector<double> OldSolution { get; private set; }

        /// <summary>
        /// Setup the simulation state.
        /// </summary>
        /// <param name="nodes">The unknown variables for which the state is used.</param>
        /// <exception cref="ArgumentNullException">nodes</exception>
        public override void Setup(VariableSet nodes)
        {
            if (nodes == null)
                throw new ArgumentNullException(nameof(nodes));

            // Initialize all matrices
            Solution = new DenseVector<double>(Solver.Order);
            OldSolution = new DenseVector<double>(Solver.Order);

            // Initialize all states and parameters
            Init = InitializationModes.None;
            UseDc = true;
            UseIc = false;

            base.Setup(nodes);
        }

        /// <summary>
        /// Unsetup the state.
        /// </summary>
        public override void Unsetup()
        {
            Solution = null;
            OldSolution = null;
            base.Unsetup();
        }

        /// <summary>
        /// Stores the solution.
        /// </summary>
        public void StoreSolution()
        {
            var tmp = OldSolution;
            OldSolution = Solution;
            Solution = tmp;
        }
    }
}
