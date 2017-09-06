/* Generated By:CSharpCC: Do not edit this line. SpiceSharpParserTokenManager.cs */
namespace SpiceSharp.Parser
{

    using System;

    public class SpiceSharpParserTokenManager : SpiceSharpParserConstants
    {
        public System.IO.TextWriter debugStream = Console.Out;
        public void SetDebugStream(System.IO.TextWriter ds) { debugStream = ds; }
        private int mccStopStringLiteralDfa_0(int pos, long active0)
        {
            switch (pos)
            {
                case 0:
                    if ((active0 & 401408L) != 0L)
                        return 12;
                    if ((active0 & 4096L) != 0L)
                        return 38;
                    if ((active0 & 6L) != 0L)
                    {
                        mccmatchedKind = 23;
                        return 39;
                    }
                    return -1;
                case 1:
                    if ((active0 & 6L) != 0L)
                    {
                        mccmatchedKind = 23;
                        mccmatchedPos = 1;
                        return 39;
                    }
                    return -1;
                case 2:
                    if ((active0 & 6L) != 0L)
                    {
                        mccmatchedKind = 23;
                        mccmatchedPos = 2;
                        return 39;
                    }
                    return -1;
                case 3:
                    if ((active0 & 6L) != 0L)
                    {
                        if (mccmatchedPos != 3)
                        {
                            mccmatchedKind = 23;
                            mccmatchedPos = 3;
                        }
                        return 39;
                    }
                    return -1;
                case 4:
                    if ((active0 & 4L) != 0L)
                        return 39;
                    if ((active0 & 2L) != 0L)
                    {
                        mccmatchedKind = 23;
                        mccmatchedPos = 4;
                        return 39;
                    }
                    return -1;
                default:
                    return -1;
            }
        }
        private int mccStartNfa_0(int pos, long active0)
        {
            return mccMoveNfa_0(mccStopStringLiteralDfa_0(pos, active0), pos + 1);
        }
        private int mccStopAtPos(int pos, int kind)
        {
            mccmatchedKind = kind;
            mccmatchedPos = pos;
            return pos + 1;
        }
        private int mccStartNfaWithStates_0(int pos, int kind, int state)
        {
            mccmatchedKind = kind;
            mccmatchedPos = pos;
            try { curChar = input_stream.ReadChar(); }
            catch (System.IO.IOException) { return pos + 1; }
            return mccMoveNfa_0(state, pos + 1);
        }
        private int mccMoveStringLiteralDfa0_0()
        {
            switch ((int)curChar)
            {
                case 40:
                    return mccStopAtPos(0, 3);
                case 41:
                    return mccStopAtPos(0, 4);
                case 42:
                    return mccStopAtPos(0, 11);
                case 44:
                    return mccStopAtPos(0, 14);
                case 45:
                    return mccStartNfaWithStates_0(0, 12, 38);
                case 46:
                    mccmatchedKind = 13;
                    return mccMoveStringLiteralDfa1_0(393216L);
                case 61:
                    return mccStopAtPos(0, 5);
                case 91:
                    return mccStopAtPos(0, 6);
                case 93:
                    return mccStopAtPos(0, 7);
                case 77:
                case 109:
                    return mccMoveStringLiteralDfa1_0(4L);
                case 83:
                case 115:
                    return mccMoveStringLiteralDfa1_0(2L);
                default:
                    return mccMoveNfa_0(0, 0);
            }
        }
        private int mccMoveStringLiteralDfa1_0(long active0)
        {
            try { curChar = input_stream.ReadChar(); }
            catch (System.IO.IOException)
            {
                mccStopStringLiteralDfa_0(0, active0);
                return 1;
            }
            switch ((int)curChar)
            {
                case 69:
                case 101:
                    return mccMoveStringLiteralDfa2_0(active0, 393216L);
                case 79:
                case 111:
                    return mccMoveStringLiteralDfa2_0(active0, 4L);
                case 85:
                case 117:
                    return mccMoveStringLiteralDfa2_0(active0, 2L);
                default:
                    break;
            }
            return mccStartNfa_0(0, active0);
        }
        private int mccMoveStringLiteralDfa2_0(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return mccStartNfa_0(0, old0);
            try { curChar = input_stream.ReadChar(); }
            catch (System.IO.IOException)
            {
                mccStopStringLiteralDfa_0(1, active0);
                return 2;
            }
            switch ((int)curChar)
            {
                case 66:
                case 98:
                    return mccMoveStringLiteralDfa3_0(active0, 2L);
                case 68:
                case 100:
                    return mccMoveStringLiteralDfa3_0(active0, 4L);
                case 78:
                case 110:
                    return mccMoveStringLiteralDfa3_0(active0, 393216L);
                default:
                    break;
            }
            return mccStartNfa_0(1, active0);
        }
        private int mccMoveStringLiteralDfa3_0(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return mccStartNfa_0(1, old0);
            try { curChar = input_stream.ReadChar(); }
            catch (System.IO.IOException)
            {
                mccStopStringLiteralDfa_0(2, active0);
                return 3;
            }
            switch ((int)curChar)
            {
                case 67:
                case 99:
                    return mccMoveStringLiteralDfa4_0(active0, 2L);
                case 68:
                case 100:
                    if ((active0 & 262144L) != 0L)
                    {
                        mccmatchedKind = 18;
                        mccmatchedPos = 3;
                    }
                    return mccMoveStringLiteralDfa4_0(active0, 131072L);
                case 69:
                case 101:
                    return mccMoveStringLiteralDfa4_0(active0, 4L);
                default:
                    break;
            }
            return mccStartNfa_0(2, active0);
        }
        private int mccMoveStringLiteralDfa4_0(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return mccStartNfa_0(2, old0);
            try { curChar = input_stream.ReadChar(); }
            catch (System.IO.IOException)
            {
                mccStopStringLiteralDfa_0(3, active0);
                return 4;
            }
            switch ((int)curChar)
            {
                case 75:
                case 107:
                    return mccMoveStringLiteralDfa5_0(active0, 2L);
                case 76:
                case 108:
                    if ((active0 & 4L) != 0L)
                        return mccStartNfaWithStates_0(4, 2, 39);
                    break;
                case 83:
                case 115:
                    if ((active0 & 131072L) != 0L)
                        return mccStopAtPos(4, 17);
                    break;
                default:
                    break;
            }
            return mccStartNfa_0(3, active0);
        }
        private int mccMoveStringLiteralDfa5_0(long old0, long active0)
        {
            if (((active0 &= old0)) == 0L)
                return mccStartNfa_0(3, old0);
            try { curChar = input_stream.ReadChar(); }
            catch (System.IO.IOException)
            {
                mccStopStringLiteralDfa_0(4, active0);
                return 5;
            }
            switch ((int)curChar)
            {
                case 84:
                case 116:
                    if ((active0 & 2L) != 0L)
                        return mccStartNfaWithStates_0(5, 1, 39);
                    break;
                default:
                    break;
            }
            return mccStartNfa_0(4, active0);
        }
        private void mccCheckNAdd(int state)
        {
            if (mccrounds[state] != mccround)
            {
                mccstateSet[mccnewStateCnt++] = state;
                mccrounds[state] = mccround;
            }
        }
        private void mccAddStates(int start, int end)
        {
            do
            {
                mccstateSet[mccnewStateCnt++] = mccnextStates[start];
            } while (start++ != end);
        }
        private void mccCheckNAddTwoStates(int state1, int state2)
        {
            mccCheckNAdd(state1);
            mccCheckNAdd(state2);
        }
        private void mccCheckNAddStates(int start, int end)
        {
            do
            {
                mccCheckNAdd(mccnextStates[start]);
            } while (start++ != end);
        }
        private void mccCheckNAddStates(int start)
        {
            mccCheckNAdd(mccnextStates[start]);
            mccCheckNAdd(mccnextStates[start + 1]);
        }
        static readonly long[] mccbitVec0 = { 0L, 0L, -1L, -1L };
        private int mccMoveNfa_0(int startState, int curPos)
        {
            int startsAt = 0;
            mccnewStateCnt = 38;
            int i = 1;
            mccstateSet[0] = startState;
            int kind = Int32.MaxValue;
            for (;;)
            {
                if (++mccround == Int32.MaxValue)
                    ReInitRounds();
                if (curChar < 64)
                {
                    long l = 1L << curChar;
                    do
                    {
                        switch (mccstateSet[--i])
                        {
                            case 39:
                                if ((576285010831605760 & l) != 0L)
                                {
                                    if (kind > 24)
                                        kind = 24;
                                    mccCheckNAdd(29);
                                }
                                if ((576285010831605760 & l) != 0L)
                                {
                                    if (kind > 23)
                                        kind = 23;
                                    mccCheckNAdd(27);
                                }
                                break;
                            case 38:
                                if ((287948901175001088 & l) != 0L)
                                {
                                    if (kind > 19)
                                        kind = 19;
                                    mccCheckNAddStates(0, 3);
                                }
                                else if (curChar == 46)
                                    mccCheckNAdd(12);
                                break;
                            case 0:
                                if ((287948901175001088 & l) != 0L)
                                {
                                    if (kind > 24)
                                        kind = 24;
                                    mccCheckNAdd(29);
                                }
                                else if ((2305846307748577280 & l) != 0L)
                                {
                                    if (kind > 15)
                                        kind = 15;
                                }
                                else if ((9216 & l) != 0L)
                                {
                                    if (kind > 16)
                                        kind = 16;
                                    mccCheckNAddTwoStates(31, 32);
                                }
                                else if ((43980465111040 & l) != 0L)
                                    mccCheckNAddTwoStates(3, 11);
                                else if ((4294967808 & l) != 0L)
                                {
                                    if (kind > 8)
                                        kind = 8;
                                }
                                else if (curChar == 34)
                                    mccCheckNAddStates(4, 6);
                                else if (curChar == 46)
                                    mccCheckNAdd(12);
                                if ((287948901175001088 & l) != 0L)
                                {
                                    if (kind > 19)
                                        kind = 19;
                                    mccCheckNAddStates(0, 3);
                                }
                                else if (curChar == 13)
                                    mccAddStates(7, 9);
                                break;
                            case 1:
                                if ((2305846307748577280 & l) != 0L && kind > 15)
                                    kind = 15;
                                break;
                            case 2:
                                if ((43980465111040 & l) != 0L)
                                    mccCheckNAddTwoStates(3, 11);
                                break;
                            case 3:
                                if ((287948901175001088 & l) == 0L)
                                    break;
                                if (kind > 19)
                                    kind = 19;
                                mccCheckNAddStates(0, 3);
                                break;
                            case 4:
                                if (curChar != 46)
                                    break;
                                if (kind > 19)
                                    kind = 19;
                                mccCheckNAddStates(10, 12);
                                break;
                            case 5:
                                if ((287948901175001088 & l) == 0L)
                                    break;
                                if (kind > 19)
                                    kind = 19;
                                mccCheckNAddStates(10, 12);
                                break;
                            case 7:
                                if ((43980465111040 & l) != 0L)
                                    mccCheckNAdd(8);
                                break;
                            case 8:
                                if ((287948901175001088 & l) == 0L)
                                    break;
                                if (kind > 19)
                                    kind = 19;
                                mccCheckNAdd(8);
                                break;
                            case 11:
                                if (curChar == 46)
                                    mccCheckNAdd(12);
                                break;
                            case 12:
                                if ((287948901175001088 & l) == 0L)
                                    break;
                                if (kind > 19)
                                    kind = 19;
                                mccCheckNAddStates(13, 15);
                                break;
                            case 13:
                                if (curChar == 34)
                                    mccCheckNAddStates(4, 6);
                                break;
                            case 14:
                                if ((-17179878401 & l) != 0L)
                                    mccCheckNAddStates(4, 6);
                                break;
                            case 16:
                                if ((566935692288 & l) != 0L)
                                    mccCheckNAddStates(4, 6);
                                break;
                            case 17:
                                if (curChar == 34 && kind > 20)
                                    kind = 20;
                                break;
                            case 18:
                                if (curChar == 10)
                                    mccCheckNAddStates(4, 6);
                                break;
                            case 19:
                                if (curChar == 13)
                                    mccstateSet[mccnewStateCnt++] = 18;
                                break;
                            case 21:
                                mccAddStates(16, 17);
                                break;
                            case 25:
                                if ((576285010831605760 & l) == 0L)
                                    break;
                                if (kind > 22)
                                    kind = 22;
                                mccstateSet[mccnewStateCnt++] = 25;
                                break;
                            case 27:
                                if ((576285010831605760 & l) == 0L)
                                    break;
                                if (kind > 23)
                                    kind = 23;
                                mccCheckNAdd(27);
                                break;
                            case 28:
                                if ((287948901175001088 & l) == 0L)
                                    break;
                                if (kind > 24)
                                    kind = 24;
                                mccCheckNAdd(29);
                                break;
                            case 29:
                                if ((576285010831605760 & l) == 0L)
                                    break;
                                if (kind > 24)
                                    kind = 24;
                                mccCheckNAdd(29);
                                break;
                            case 30:
                                if ((9216 & l) == 0L)
                                    break;
                                if (kind > 16)
                                    kind = 16;
                                mccCheckNAddTwoStates(31, 32);
                                break;
                            case 31:
                                if (curChar == 43 && kind > 9)
                                    kind = 9;
                                break;
                            case 32:
                                if (curChar != 42)
                                    break;
                                if (kind > 10)
                                    kind = 10;
                                mccCheckNAdd(33);
                                break;
                            case 33:
                                if ((-9217 & l) == 0L)
                                    break;
                                if (kind > 10)
                                    kind = 10;
                                mccCheckNAdd(33);
                                break;
                            case 34:
                                if (curChar == 13)
                                    mccAddStates(7, 9);
                                break;
                            case 35:
                                if (curChar == 10)
                                    mccCheckNAdd(31);
                                break;
                            case 36:
                                if (curChar == 10)
                                    mccCheckNAdd(32);
                                break;
                            case 37:
                                if (curChar == 10 && kind > 16)
                                    kind = 16;
                                break;
                            default: break;
                        }
                    } while (i != startsAt);
                }
                else if (curChar < 128)
                {
                    long l = 1L << (curChar & 63);
                    do
                    {
                        switch (mccstateSet[--i])
                        {
                            case 39:
                                if ((576460745995190270 & l) != 0L)
                                {
                                    if (kind > 24)
                                        kind = 24;
                                    mccCheckNAdd(29);
                                }
                                if ((576460745995190270 & l) != 0L)
                                {
                                    if (kind > 23)
                                        kind = 23;
                                    mccCheckNAdd(27);
                                }
                                break;
                            case 0:
                                if ((576460745995190270 & l) != 0L)
                                {
                                    if (kind > 24)
                                        kind = 24;
                                    mccCheckNAdd(29);
                                }
                                else if ((671088640 & l) != 0L)
                                {
                                    if (kind > 15)
                                        kind = 15;
                                }
                                else if (curChar == 64)
                                    mccstateSet[mccnewStateCnt++] = 24;
                                else if (curChar == 123)
                                    mccCheckNAdd(21);
                                if ((576460743847706622 & l) != 0L)
                                {
                                    if (kind > 23)
                                        kind = 23;
                                    mccCheckNAdd(27);
                                }
                                break;
                            case 1:
                                if ((671088640 & l) != 0L && kind > 15)
                                    kind = 15;
                                break;
                            case 6:
                                if ((137438953504 & l) != 0L)
                                    mccAddStates(18, 19);
                                break;
                            case 9:
                                if ((13907447705069760 & l) == 0L)
                                    break;
                                if (kind > 19)
                                    kind = 19;
                                mccCheckNAdd(10);
                                break;
                            case 10:
                                if ((576460743847706622 & l) == 0L)
                                    break;
                                if (kind > 19)
                                    kind = 19;
                                mccCheckNAdd(10);
                                break;
                            case 14:
                                if ((-268435457 & l) != 0L)
                                    mccCheckNAddStates(4, 6);
                                break;
                            case 15:
                                if (curChar == 92)
                                    mccAddStates(20, 21);
                                break;
                            case 16:
                                if ((5700160605929540 & l) != 0L)
                                    mccCheckNAddStates(4, 6);
                                break;
                            case 20:
                                if (curChar == 123)
                                    mccCheckNAdd(21);
                                break;
                            case 21:
                                if ((-2882303761517117441 & l) != 0L)
                                    mccCheckNAddTwoStates(21, 22);
                                break;
                            case 22:
                                if (curChar == 125 && kind > 21)
                                    kind = 21;
                                break;
                            case 23:
                                if (curChar == 64)
                                    mccstateSet[mccnewStateCnt++] = 24;
                                break;
                            case 24:
                                if ((576460743847706622 & l) == 0L)
                                    break;
                                if (kind > 22)
                                    kind = 22;
                                mccCheckNAdd(25);
                                break;
                            case 25:
                                if ((576460745995190270 & l) == 0L)
                                    break;
                                if (kind > 22)
                                    kind = 22;
                                mccCheckNAdd(25);
                                break;
                            case 26:
                                if ((576460743847706622 & l) == 0L)
                                    break;
                                if (kind > 23)
                                    kind = 23;
                                mccCheckNAdd(27);
                                break;
                            case 27:
                                if ((576460745995190270 & l) == 0L)
                                    break;
                                if (kind > 23)
                                    kind = 23;
                                mccCheckNAdd(27);
                                break;
                            case 28:
                                if ((576460745995190270 & l) == 0L)
                                    break;
                                if (kind > 24)
                                    kind = 24;
                                mccCheckNAdd(29);
                                break;
                            case 29:
                                if ((576460745995190270 & l) == 0L)
                                    break;
                                if (kind > 24)
                                    kind = 24;
                                mccCheckNAdd(29);
                                break;
                            case 33:
                                if (kind > 10)
                                    kind = 10;
                                mccstateSet[mccnewStateCnt++] = 33;
                                break;
                            default: break;
                        }
                    } while (i != startsAt);
                }
                else
                {
                    int i2 = (curChar & 0xff) >> 6;
                    long l2 = 1L << (curChar & 63);
                    do
                    {
                        switch (mccstateSet[--i])
                        {
                            case 14:
                                if ((mccbitVec0[i2] & l2) != 0L)
                                    mccAddStates(4, 6);
                                break;
                            case 21:
                                if ((mccbitVec0[i2] & l2) != 0L)
                                    mccAddStates(16, 17);
                                break;
                            case 33:
                                if ((mccbitVec0[i2] & l2) == 0L)
                                    break;
                                if (kind > 10)
                                    kind = 10;
                                mccstateSet[mccnewStateCnt++] = 33;
                                break;
                            default: break;
                        }
                    } while (i != startsAt);
                }
                if (kind != Int32.MaxValue)
                {
                    mccmatchedKind = kind;
                    mccmatchedPos = curPos;
                    kind = Int32.MaxValue;
                }
                ++curPos;
                if ((i = mccnewStateCnt) == (startsAt = 38 - (mccnewStateCnt = startsAt)))
                    return curPos;
                try { curChar = input_stream.ReadChar(); }
                catch (System.IO.IOException) { return curPos; }
            }
        }
        static readonly int[] mccnextStates = {
   3, 4, 6, 9, 14, 15, 17, 35, 36, 37, 5, 6, 9, 12, 6, 9,
   21, 22, 7, 8, 16, 19,
};
        public static readonly string[] mccstrLiteralImages = {
"", null, null, "(", ")", "=", "[", "]", null, null, null, "*", "-", ".", ",",
null, null, null, null, null, null, null, null, null, null, null, null, null, null, };
        public static readonly string[] lexStateNames = {
   "DEFAULT",
};
        static readonly long[] mcctoToken = {
   33552639,
};
        static readonly long[] mcctoSkip = {
   1792,
};
        protected SimpleCharStream input_stream;
        private readonly int[] mccrounds = new int[38];
        private readonly int[] mccstateSet = new int[76];
        protected char curChar;
        public SpiceSharpParserTokenManager(SimpleCharStream stream)
        {
            if (SimpleCharStream.staticFlag)
                throw new System.SystemException("ERROR: Cannot use a static CharStream class with a non-static lexical analyzer.");
            input_stream = stream;
        }
        public SpiceSharpParserTokenManager(SimpleCharStream stream, int lexState)
           : this(stream)
        {
            SwitchTo(lexState);
        }
        public void ReInit(SimpleCharStream stream)
        {
            mccmatchedPos = mccnewStateCnt = 0;
            curLexState = defaultLexState;
            input_stream = stream;
            ReInitRounds();
        }
        private void ReInitRounds()
        {
            int i;
            mccround = -2147483647;
            for (i = 38; i-- > 0;)
                mccrounds[i] = Int32.MinValue;
        }
        public void ReInit(SimpleCharStream stream, int lexState)
        {
            ReInit(stream);
            SwitchTo(lexState);
        }
        public void SwitchTo(int lexState)
        {
            if (lexState >= 1 || lexState < 0)
                throw new TokenMgrError("Error: Ignoring invalid lexical state : " + lexState + ". State unchanged.", TokenMgrError.InvalidLexicalState);
            else
                curLexState = lexState;
        }

        protected Token mccFillToken()
        {
            Token t = Token.NewToken(mccmatchedKind);
            t.kind = mccmatchedKind;
            string im = mccstrLiteralImages[mccmatchedKind];
            t.image = (im == null) ? input_stream.GetImage() : im;
            t.beginLine = input_stream.BeginLine;
            t.beginColumn = input_stream.BeginColumn;
            t.endLine = input_stream.EndLine;
            t.endColumn = input_stream.EndColumn;
            return t;
        }

        int curLexState = 0;
        int defaultLexState = 0;
        int mccnewStateCnt;
        int mccround;
        int mccmatchedPos;
        int mccmatchedKind;

        public Token GetNextToken()
        {
            Token matchedToken;
            int curPos = 0;

            for (;;)
            {
                try
                {
                    curChar = input_stream.BeginToken();
                }
                catch (System.IO.IOException)
                {
                    mccmatchedKind = 0;
                    matchedToken = mccFillToken();
                    return matchedToken;
                }

                mccmatchedKind = Int32.MaxValue;
                mccmatchedPos = 0;
                curPos = mccMoveStringLiteralDfa0_0();
                if (mccmatchedKind != Int32.MaxValue)
                {
                    if (mccmatchedPos + 1 < curPos)
                        input_stream.Backup(curPos - mccmatchedPos - 1);
                    if ((mcctoToken[mccmatchedKind >> 6] & (1L << (mccmatchedKind & 63))) != 0L)
                    {
                        matchedToken = mccFillToken();
                        return matchedToken;
                    }
                    else
                    {
                        goto EOFLoop;
                    }
                }
                int error_line = input_stream.EndLine;
                int error_column = input_stream.EndColumn;
                string error_after = null;
                bool EOFSeen = false;
                try { input_stream.ReadChar(); input_stream.Backup(1); }
                catch (System.IO.IOException)
                {
                    EOFSeen = true;
                    error_after = curPos <= 1 ? "" : input_stream.GetImage();
                    if (curChar == '\n' || curChar == '\r')
                    {
                        error_line++;
                        error_column = 0;
                    }
                    else
                        error_column++;
                }
                if (!EOFSeen)
                {
                    input_stream.Backup(1);
                    error_after = curPos <= 1 ? "" : input_stream.GetImage();
                }
                throw new TokenMgrError(EOFSeen, curLexState, error_line, error_column, error_after, curChar, TokenMgrError.LexicalError);
                EOFLoop:;
            }
        }

    }
}
