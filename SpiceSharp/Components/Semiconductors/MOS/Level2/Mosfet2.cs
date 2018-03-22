﻿using SpiceSharp.Attributes;
using SpiceSharp.Components.MosfetBehaviors.Level2;
using SpiceSharp.Simulations;

namespace SpiceSharp.Components
{
    /// <summary>
    /// A MOS2 Mosfet.
    /// Level 2, A. Vladimirescu and S. Liu, The Simulation of MOS Integrated Circuits Using SPICE2, ERL Memo No. M80/7, Electronics Research Laboratory University of California, Berkeley, October 1980.
    /// </summary>
    [Pin(0, "Drain"), Pin(1, "Gate"), Pin(2, "Source"), Pin(3, "Bulk"), Connected(0, 2), Connected(0, 3)]
    public class Mosfet2 : Component
    {
        /// <summary>
        /// Set the model for the MOS2 Mosfet.
        /// </summary>
        public void SetModel(Model model) => Model = model;

        /// <summary>
        /// Nodes
        /// </summary>
        [ParameterName("dnode"), ParameterInfo("Number of drain node")]
        public int DrainNode { get; internal set; }
        [ParameterName("gnode"), ParameterInfo("Number of gate node")]
        public int GateNode { get; internal set; }
        [ParameterName("snode"), ParameterInfo("Number of source node")]
        public int SourceNode { get; internal set; }
        [ParameterName("bnode"), ParameterInfo("Number of bulk node")]
        public int BulkNode { get; internal set; }

        /// <summary>
        /// Constants
        /// </summary>
        [ParameterName("pincount"), ParameterInfo("Number of pins")]
		public const int Mosfet2PinCount = 4;
        
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">The name of the device</param>
        public Mosfet2(Identifier name) : base(name, Mosfet2PinCount)
        {
            // Add parameters
            ParameterSets.Add(new BaseParameters());

            // Add factories
            Behaviors.Add(typeof(TemperatureBehavior), () => new TemperatureBehavior(Name));
            Behaviors.Add(typeof(LoadBehavior), () => new LoadBehavior(Name));
            Behaviors.Add(typeof(FrequencyBehavior), () => new FrequencyBehavior(Name));
            Behaviors.Add(typeof(TransientBehavior), () => new TransientBehavior(Name));
            Behaviors.Add(typeof(NoiseBehavior), () => new NoiseBehavior(Name));
        }

        /// <summary>
        /// Setup the device
        /// </summary>
        /// <param name="simulation">Simulation</param>
        public override void Setup(Simulation simulation)
        {
            var nodes = BindNodes(simulation);
            DrainNode = nodes[0].Index;
            GateNode = nodes[1].Index;
            SourceNode = nodes[2].Index;
            BulkNode = nodes[3].Index;
        }
    }
}
