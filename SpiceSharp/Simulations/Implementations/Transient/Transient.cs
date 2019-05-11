using System;

namespace SpiceSharp.Simulations
{
    /// <summary>
    /// This class implements the transient analysis.
    /// </summary>
    public class Transient : TimeSimulation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transient"/> class.
        /// </summary>
        /// <param name="name">The identifier of the simulation.</param>
        public Transient(string name) : base(name)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transient"/> class.
        /// </summary>
        /// <param name="name">The identifier of the simulation.</param>
        /// <param name="step">The step size.</param>
        /// <param name="final">The final time.</param>
        public Transient(string name, double step, double final) 
            : base(name, step, final)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Transient"/> class.
        /// </summary>
        /// <param name="name">The identifier of the simulation.</param>
        /// <param name="step">The step size.</param>
        /// <param name="final">The final time.</param>
        /// <param name="maxStep">The maximum step.</param>
        public Transient(string name, double step, double final, double maxStep) 
            : base(name, step, final, maxStep)
        {
        }

        private ExportDataEventArgs exportargs;
        private TimeConfiguration timeConfig;
        private int startIters;
        private TimeSpan startselapsed;
        private bool simulationRunning;

        /// <summary>
        /// Executes the simulation.
        /// </summary>
        /// <exception cref="SpiceSharp.CircuitException">{0}: transient terminated".FormatString(Name)</exception>
        protected override void Execute()
        {
            // First do temperature-dependent calculations and IC
            base.Execute();
            exportargs = new ExportDataEventArgs(this);
            timeConfig = Configurations.Get<TimeConfiguration>();

            // Start our statistics
            Statistics.TransientTime.Start();
            startIters = Statistics.Iterations;
            startselapsed = Statistics.SolveTime.Elapsed;

            simulationRunning = true;

            if (!StepByStepSimulation)
                DoTick();
        }

        public void DoTick() => DoTick(-1);

        public void DoTick(double deltaTime)
        {
            if (!simulationRunning)
                return;

            if (deltaTime <= 0)
                deltaTime = timeConfig.Step;

            try
            {
                var newDelta = Math.Min(timeConfig.FinalTime / 50.0, deltaTime) / 10.0;
                do
                {
                    // Accept the last evaluated time point
                    Accept();

                    // Export the current timepoint
                    if (Method.Time >= timeConfig.InitTime)
                        OnExport(exportargs);

                    // Detect the end of the simulation
                    if (Method.Time >= timeConfig.FinalTime)
                    {
                        // Keep our statistics
                        Statistics.TransientTime.Stop();
                        Statistics.TransientIterations += Statistics.Iterations - startIters;
                        Statistics.TransientSolveTime += Statistics.SolveTime.Elapsed - startselapsed;

                        // Finished!
                        return;
                    }

                    // Continue integration
                    Method.Continue(this, ref newDelta);

                    // Find a valid time point
                    while (true)
                    {
                        // Probe the next time point
                        Probe(newDelta);

                        // Try to solve the new point
                        var converged = TimeIterate(timeConfig.TranMaxIterations);
                        Statistics.TimePoints++;

                        // Did we fail to converge to a solution?
                        if (!converged)
                        {
                            Method.NonConvergence(this, out newDelta);
                            Statistics.Rejected++;
                        }
                        else
                        {
                            // If our integration method approves of our solution, continue to the next timepoint
                            if (Method.Evaluate(this, out newDelta))
                                break;
                            Statistics.Rejected++;
                        }
                    }
                } while (!StepByStepSimulation);
            }
            catch (CircuitException ex)
            {
                // Keep our statistics
                Statistics.TransientTime.Stop();
                Statistics.TransientIterations += Statistics.Iterations - startIters;
                Statistics.TransientSolveTime += Statistics.SolveTime.Elapsed - startselapsed;
                throw new CircuitException("{0}: transient terminated".FormatString(Name), ex);
            }
        }

        public override void FinalizeSimulation()
        {
            base.FinalizeSimulation();
            simulationRunning = false;
        }
    }
}
