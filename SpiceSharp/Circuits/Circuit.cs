﻿using System.Collections.Generic;
using SpiceSharp.Circuits;

namespace SpiceSharp
{
    /// <summary>
    /// Represents an electronic circuit.
    /// </summary>
    public class Circuit : EntityCollection
    {
        /// <summary>
        /// Common constants
        /// </summary>
        public const double Charge = 1.6021918e-19;
        public const double CelsiusKelvin = 273.15;
        public const double Boltzmann = 1.3806226e-23;
        public const double ReferenceTemperature = 300.15; // 27degC
        public const double Root2 = 1.4142135623730951;
        public const double Vt0 = Boltzmann * (27.0 + CelsiusKelvin) / Charge;
        public const double KOverQ = Boltzmann / Charge;

        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit"/> class.
        /// </summary>
        public Circuit()
            : base()
        { 
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit"/> class.
        /// </summary>
        /// <param name="comparer">The <see cref="T:System.Collections.Generic.IEqualityComparer`1" /> implementation to use when comparing entity names, or <c>null</c> to use the default <see cref="T:System.Collections.Generic.EqualityComparer`1" />.</param>
        public Circuit(IEqualityComparer<string> comparer)
            : base(comparer)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit"/> class.
        /// </summary>
        /// <param name="entities">The entities describing the circuit.</param>
        public Circuit(IEnumerable<Entity> entities)
            : base()
        {
            if (entities == null)
                return;
            foreach (var entity in entities)
                Add(entity);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Circuit"/> class.
        /// </summary>
        /// <param name="entities">The entities describing the circuit.</param>
        public Circuit(params Entity[] entities)
            : base()
        {
            if (entities == null)
                return;
            foreach (var entity in entities)
                Add(entity);
        }

        /// <summary>
        /// Validates the circuit. Checks for voltage loops, floating nodes, etc.
        /// </summary>
        /// <seealso cref="Validator"/>
        public void Validate()
        {
            var validator = new Validator();
            validator.Validate(this);
        }
    }
}
