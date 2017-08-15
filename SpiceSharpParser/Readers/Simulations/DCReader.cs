﻿using SpiceSharp.Simulations;
using SpiceSharp.Parser.Readers.Extensions;

namespace SpiceSharp.Parser.Readers
{
    /// <summary>
    /// A class that can read DC analysis
    /// </summary>
    public class DCReader : Reader
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public DCReader() : base(StatementType.Control)
        {
            Identifier = "dc";
        }

        /// <summary>
        /// Read
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="parameters">Parameters</param>
        /// <param name="netlist">Netlist</param>
        /// <returns></returns>
        public override bool Read(Statement st, Netlist netlist)
        {
            DC dc = new DC("DC " + (netlist.Simulations.Count + 1));
            int count = st.Parameters.Count / 4;
            switch (st.Parameters.Count - 4 * count)
            {
                case 0:
                    if (st.Parameters.Count == 0)
                        throw new ParseException(st.Name, "Source st.Name expected");
                    break;
                case 1: throw new ParseException(st.Parameters[count * 4], "Start value expected");
                case 2: throw new ParseException(st.Parameters[count * 4 + 1], "Stop value expected");
                case 3: throw new ParseException(st.Parameters[count * 4 + 2], "Step value expected");
            }

            // Format: .DC SRCNAM VSTART VSTOP VINCR [SRC2 START2 STOP2 INCR2]
            for (int i = 0; i < count; i++)
            {
                DC.Sweep sweep = new DC.Sweep(
                    st.Parameters[i * 4].image.ToLower(),
                    netlist.ParseDouble(st.Parameters[i * 4 + 1]),
                    netlist.ParseDouble(st.Parameters[i * 4 + 2]),
                    netlist.ParseDouble(st.Parameters[i * 4 + 3]));
                dc.Sweeps.Add(sweep);
            }

            netlist.Simulations.Add(dc);
            Generated = dc;
            return true;
        }
    }
}