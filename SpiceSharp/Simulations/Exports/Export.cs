﻿using System;

namespace SpiceSharp.Simulations
{
    /// <summary>
    /// A template for exporting data for a simulation.
    /// </summary>
    /// <typeparam name="T">The base value type.</typeparam>
    public abstract class Export<T>
    {
        /// <summary>
        /// Returns true if the exporter is currently valid.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is valid; otherwise, <c>false</c>.
        /// </value>
        public bool IsValid
        {
            get
            {
                if (Extractor == null)
                    LazyLoad();
                return Extractor != null;
            }
        }

        /// <summary>
        /// Gets or sets the extractor function.
        /// </summary>
        /// <value>
        /// The extractor.
        /// </value>
        protected Func<T> Extractor { get; set; }

        /// <summary>
        /// Gets the simulation from which the data needs to be extracted.
        /// </summary>
        /// <value>
        /// The simulation.
        /// </value>
        protected Simulation Simulation { get; }

        /// <summary>
        /// Gets the current value from the simulation.
        /// </summary>
        /// <value>
        /// The current value.
        /// </value>
        /// <remarks>
        /// This property will return a default if there is nothing to extract.
        /// </remarks>
        public T Value
        {
            get
            {
                if (Extractor == null)
                {
                    // Try initializing (lazy loading)
                    LazyLoad();
                    if (Extractor == null)
                        return default(T);
                }

                return Extractor();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Export{T}"/> class.
        /// </summary>
        /// <param name="simulation">The simulation.</param>
        /// <exception cref="ArgumentNullException">simulation</exception>
        protected Export(Simulation simulation)
        {
            Simulation = simulation ?? throw new ArgumentNullException(nameof(simulation));
            simulation.AfterSetup += Initialize;
            simulation.BeforeUnsetup += Finalize;
        }

        /// <summary>
        /// Destroys the export.
        /// </summary>
        public virtual void Destroy()
        {
            Simulation.AfterSetup -= Initialize;
            Simulation.BeforeUnsetup -= Initialize;
            Extractor = null;
        }

        /// <summary>
        /// Load the export extractor if the simulation has already started.
        /// </summary>
        protected void LazyLoad()
        {
            // If we're already too far, emulate a call from the simulation
            if (Simulation.Status == Simulation.Statuses.Setup || Simulation.Status == Simulation.Statuses.Running)
                Initialize(Simulation, EventArgs.Empty);
        }

        /// <summary>
        /// Initializes the export.
        /// </summary>
        /// <param name="sender">The object (simulation) sending the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected abstract void Initialize(object sender, EventArgs e);

        /// <summary>
        /// Finalizes the export.
        /// </summary>
        /// <param name="sender">The object (simulation) sending the event.</param>
        /// <param name="e">The <see cref="EventArgs"/> instance containing the event data.</param>
        protected virtual void Finalize(object sender, EventArgs e)
        {
            Extractor = null;
        }
    }
}
