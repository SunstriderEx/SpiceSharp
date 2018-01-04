﻿using System;
using SpiceSharp.Diagnostics;
using SpiceSharp.Attributes;
using SpiceSharp.Circuits;
using SpiceSharp.Components.DIO;

namespace SpiceSharp.Behaviors.DIO
{
    /// <summary>
    /// Temperature behavior for a <see cref="Components.DiodeModel"/>
    /// </summary>
    public class ModelTemperatureBehavior : Behaviors.TemperatureBehavior
    {
        /// <summary>
        /// Necessary behaviors and parameters
        /// </summary>
        ModelBaseParameters mbp;

        /// <summary>
        /// Conductance
        /// </summary>
        [SpiceName("cond"), SpiceInfo("Ohmic conductance")]
        public double DIOconductance { get; internal set; }

        /// <summary>
        /// Shared parameters
        /// </summary>
        public double vtnom { get; protected set; }
        public double xfc { get; protected set; }
        public double DIOf2 { get; internal set; }
        public double DIOf3 { get; internal set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public ModelTemperatureBehavior(Identifier name) : base(name) { }

        /// <summary>
        /// Setup the behavior
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="pool">Behaviors</param>
        public override void Setup(ParametersCollection parameters, BehaviorPool pool)
        {
            mbp = parameters.Get<ModelBaseParameters>();
        }

        /// <summary>
        /// Execute behavior
        /// </summary>
        /// <param name="ckt">Circuit</param>
        public override void Temperature(Circuit ckt)
        {
            if (!mbp.DIOnomTemp.Given)
            {
                mbp.DIOnomTemp.Value = ckt.State.NominalTemperature;
            }
            vtnom = Circuit.CONSTKoverQ * mbp.DIOnomTemp;

            // limit grading coeff to max of .9
            if (mbp.DIOgradingCoeff > .9)
            {
                mbp.DIOgradingCoeff.Value = 0.9;
                CircuitWarning.Warning(this, $"{Name}: grading coefficient too large, limited to 0.9");
            }

            // limit activation energy to min of .1
            if (mbp.DIOactivationEnergy < .1)
            {
                mbp.DIOactivationEnergy.Value = 0.1;
                CircuitWarning.Warning(this, $"{Name}: activation energy too small, limited to 0.1");
            }

            // limit depletion cap coeff to max of .95
            if (mbp.DIOdepletionCapCoeff > .95)
            {
                mbp.DIOdepletionCapCoeff.Value = 0.95;
                CircuitWarning.Warning(this, $"{Name}: coefficient Fc too large, limited to 0.95");
            }

            if (!mbp.DIOresist.Given || mbp.DIOresist.Value == 0)
                DIOconductance = 0;
            else
                DIOconductance = 1 / mbp.DIOresist;
            xfc = Math.Log(1 - mbp.DIOdepletionCapCoeff);

            DIOf2 = Math.Exp((1 + mbp.DIOgradingCoeff) * xfc);
            DIOf3 = 1 - mbp.DIOdepletionCapCoeff * (1 + mbp.DIOgradingCoeff);
        }
    }
}
