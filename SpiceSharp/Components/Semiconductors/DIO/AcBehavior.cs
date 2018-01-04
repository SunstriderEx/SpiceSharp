﻿using System;
using System.Numerics;
using SpiceSharp.Circuits;
using SpiceSharp.Sparse;
using SpiceSharp.Components;
using SpiceSharp.Components.DIO;
using SpiceSharp.Simulations;

namespace SpiceSharp.Behaviors.DIO
{
    /// <summary>
    /// AC behavior for <see cref="Diode"/>
    /// </summary>
    public class AcBehavior : Behaviors.AcBehavior, IConnectedBehavior, IModelBehavior
    {
        /// <summary>
        /// Necessary behaviors
        /// </summary>
        BaseParameters bp;
        ModelBaseParameters mbp;
        LoadBehavior load;
        TemperatureBehavior temp;
        ModelTemperatureBehavior modeltemp;

        /// <summary>
        /// Nodes
        /// </summary>
        int DIOposNode, DIOnegNode, DIOposPrimeNode;
        protected MatrixElement DIOposPosPrimePtr { get; private set; }
        protected MatrixElement DIOnegPosPrimePtr { get; private set; }
        protected MatrixElement DIOposPrimePosPtr { get; private set; }
        protected MatrixElement DIOposPrimeNegPtr { get; private set; }
        protected MatrixElement DIOposPosPtr { get; private set; }
        protected MatrixElement DIOnegNegPtr { get; private set; }
        protected MatrixElement DIOposPrimePosPrimePtr { get; private set; }

        /// <summary>
        /// Gets the junction capacitance
        /// </summary>
        public double DIOcap { get; protected set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        public AcBehavior(Identifier name) : base(name) { }

        /// <summary>
        /// Setup the behavior
        /// </summary>
        /// <param name="parameters">Parameters</param>
        /// <param name="pool">Behaviors</param>
        public override void Setup(ParametersCollection parameters, BehaviorPool pool)
        {
            // Get parameters
            bp = parameters.Get<BaseParameters>();

            // Get behaviors
            load = pool.GetBehavior<LoadBehavior>();
            temp = pool.GetBehavior<TemperatureBehavior>();
        }

        /// <summary>
        /// Connect
        /// </summary>
        /// <param name="pins">Pins</param>
        public void Connect(params int[] pins)
        {
            DIOposNode = pins[0];
            DIOnegNode = pins[1];
        }

        /// <summary>
        /// Setup model parameters and behaviors
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="pool"></param>
        public void SetupModel(ParametersCollection parameters, BehaviorPool pool)
        {
            // Get parameters
            mbp = parameters.Get<ModelBaseParameters>();

            // Get behaviors
            modeltemp = pool.GetBehavior<ModelTemperatureBehavior>();
        }

        /// <summary>
        /// Get matrix pointers
        /// </summary>
        /// <param name="matrix">Matrix</param>
        public override void GetMatrixPointers(Matrix matrix)
        {
            // Get node
            DIOposPrimeNode = load.DIOposPrimeNode;

            // Get matrix pointers
            DIOposPosPrimePtr = matrix.GetElement(DIOposNode, DIOposPrimeNode);
            DIOnegPosPrimePtr = matrix.GetElement(DIOnegNode, DIOposPrimeNode);
            DIOposPrimePosPtr = matrix.GetElement(DIOposPrimeNode, DIOposNode);
            DIOposPrimeNegPtr = matrix.GetElement(DIOposPrimeNode, DIOnegNode);
            DIOposPosPtr = matrix.GetElement(DIOposNode, DIOposNode);
            DIOnegNegPtr = matrix.GetElement(DIOnegNode, DIOnegNode);
            DIOposPrimePosPrimePtr = matrix.GetElement(DIOposPrimeNode, DIOposPrimeNode);
        }

        /// <summary>
        /// Unsetup the device
        /// </summary>
        public override void Unsetup()
        {
            DIOposPosPrimePtr = null;
            DIOnegPosPrimePtr = null;
            DIOposPrimePosPtr = null;
            DIOposPrimeNegPtr = null;
            DIOposPosPtr = null;
            DIOnegNegPtr = null;
            DIOposPrimePosPrimePtr = null;
        }

        /// <summary>
        /// Calculate AC parameters
        /// </summary>
        /// <param name="sim"></param>
        public override void InitializeParameters(FrequencySimulation sim)
        {
            var state = sim.Circuit.State;
            double arg, czero, sarg, capd, czof2;
            double vd = state.Solution[DIOposPrimeNode] - state.Solution[DIOnegNode];

            // charge storage elements
            czero = temp.DIOtJctCap * bp.DIOarea;
            if (vd < temp.DIOtDepCap)
            {
                arg = 1 - vd / mbp.DIOjunctionPot;
                sarg = Math.Exp(-mbp.DIOgradingCoeff * Math.Log(arg));
                capd = mbp.DIOtransitTime * load.DIOconduct + czero * sarg;
            }
            else
            {
                czof2 = czero / modeltemp.DIOf2;
                capd = mbp.DIOtransitTime * load.DIOconduct + czof2 * (modeltemp.DIOf3 + mbp.DIOgradingCoeff * vd / mbp.DIOjunctionPot);
            }
            DIOcap = capd;
        }

        /// <summary>
        /// Perform AC analysis
        /// </summary>
        /// <param name="ckt">Circuit</param>
        public override void Load(Circuit ckt)
        {
            var state = ckt.State;
            double gspr, geq, xceq;

            gspr = modeltemp.DIOconductance * bp.DIOarea;
            geq = load.DIOconduct;
            xceq = DIOcap * state.Laplace.Imaginary;

            DIOposPosPtr.Value.Real += gspr;
            DIOnegNegPtr.Value.Cplx += new Complex(geq, xceq);

            DIOposPrimePosPrimePtr.Value.Cplx += new Complex(geq + gspr, xceq);

            DIOposPosPrimePtr.Value.Real -= gspr;
            DIOnegPosPrimePtr.Value.Cplx -= new Complex(geq, xceq);

            DIOposPrimePosPtr.Value.Real -= gspr;
            DIOposPrimeNegPtr.Value.Cplx -= new Complex(geq, xceq);
        }
    }
}
