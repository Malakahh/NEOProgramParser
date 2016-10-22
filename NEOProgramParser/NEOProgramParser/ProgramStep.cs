using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOProgramParser
{
    class ProgramStep
    {
        //Weird data encoding
        public enum ProgramStepBytes {
            Voltage_ByteLow = 0,
            Voltage_ByteHigh = 1,
            Current_ByteLow = 2,
            Current_ByteHigh = 3,
            VoltageLowJumpStep = 4,
            VoltageHighJumpStep = 5,
            VoltageLowJump_ByteLow = 6,
            VoltageLowJump_ByteHigh = 7,
            VoltageHighJump_ByteLow = 8,
            VoltageHighJump_ByteHigh = 9,
            CurrentLowJumpStep = 10,
            CurrentHighJumpStep = 11,
            CurrentLowJump_ByteLow = 12,
            CurrentLowJump_ByteHigh = 13,
            CurrentHighJump_ByteLow = 14,
            CurrentHighJump_ByteHigh = 15,
            RelativeTimeJumpStep = 16,
            AbsoluteTimeJumpStep = 17,
            RelativeTime_0 = 20,
            RelativeTime_1 = 21,
            RelativeTime_2 = 22,
            AbsoluteTime_0 = 24,
            AbsoluteTime_1 = 25,
            AbsoluteTime_2 = 26,
            C = 28, //LED Charge
            F = 29, //Flash LEDs
            E = 30, //LED Error
            T = 31, //Timer
            S = 32  //Output switch
        }

        public static readonly int stepSizeInBytes = 36;

        private byte[] bytes;

        public ProgramStep(byte[] bytes)
        {
            this.bytes = bytes;
        }

        private bool IsSet(ProgramStepBytes psb)
        {
            return this.bytes[(int)psb] != 0xFF;
        }

        public int CalcFinalWordCount()
        {
            int cntWords = 2; // We have a defined minimum if two WORDS
            ProgramStepBytes[] toTest = new ProgramStepBytes[]
            {
                ProgramStepBytes.CurrentLowJumpStep,
                ProgramStepBytes.CurrentHighJumpStep,
                ProgramStepBytes.VoltageLowJumpStep,
                ProgramStepBytes.VoltageHighJumpStep,
                ProgramStepBytes.RelativeTimeJumpStep,
                ProgramStepBytes.AbsoluteTimeJumpStep
            };

            foreach (ProgramStepBytes psb in toTest)
            {
                if (IsSet(psb))
                {
                    cntWords++;

                    //These require two words, per documnetation
                    if (psb == ProgramStepBytes.RelativeTimeJumpStep || psb == ProgramStepBytes.AbsoluteTimeJumpStep)
                    {
                        cntWords++;
                    }
                }
            }

            return cntWords;
        }

        private byte[] GenerateWord_VoltageSetPoint()
        {
            byte[] word = new byte[2] { 0, 0 };

            word[0] = (byte)(this.bytes[(int)ProgramStepBytes.Voltage_ByteHigh] & 0x03);
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.CurrentLowJumpStep)) << 2));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.CurrentHighJumpStep)) << 3));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.VoltageLowJumpStep)) << 4));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.VoltageHighJumpStep)) << 5));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.RelativeTimeJumpStep)) << 6));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.AbsoluteTimeJumpStep)) << 7));

            word[1] = this.bytes[(int)ProgramStepBytes.Voltage_ByteLow];

            return word;
        }

        private byte[] GenerateWord_CurrentSetPoint()
        {
            byte[] word = new byte[2] { 0, 0 };

            word[0] = (byte)(this.bytes[(int)ProgramStepBytes.Current_ByteHigh] & 0x03);
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.C)) << 2));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.F)) << 3));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.E)) << 4));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.S)) << 5));
            word[0] = (byte)( word[0] | (BoolToBit(IsSet(ProgramStepBytes.T)) << 6));

            word[1] = this.bytes[(int)ProgramStepBytes.Current_ByteLow];

            return word;
        }

        private byte[][] GenerateWords_TimeJump(ProgramStepBytes step, ProgramStepBytes b0, ProgramStepBytes b1, ProgramStepBytes b2)
        {
            byte[][] words = new byte[2][];
            words[0] = new byte[2] { 0, 0 };
            words[1] = new byte[2] { 0, 0 };

            words[0][0] = (byte)(this.bytes[(int)step] << 2); //Because... reason. Documentation states this needs to be the case
            words[0][1] = this.bytes[(int)b0];

            words[1][0] = this.bytes[(int)b2];
            words[1][0] = this.bytes[(int)b1];

            return words;
        }

        private byte[][] GenerateWords_AbsoluteJump()
        {
            return GenerateWords_TimeJump(
                ProgramStepBytes.AbsoluteTimeJumpStep,
                ProgramStepBytes.AbsoluteTime_0,
                ProgramStepBytes.AbsoluteTime_1,
                ProgramStepBytes.AbsoluteTime_2);
        }

        private byte[][] GenerateWords_RelativeJump()
        {
            return GenerateWords_TimeJump(
                ProgramStepBytes.RelativeTimeJumpStep,
                ProgramStepBytes.RelativeTime_0,
                ProgramStepBytes.RelativeTime_1,
                ProgramStepBytes.RelativeTime_2);
        }

        private byte[] GenerateWord_BasicJump(ProgramStepBytes step, ProgramStepBytes high, ProgramStepBytes low)
        {
            byte[] word = new byte[2] { 0, 0 };

            word[0] = (byte)((this.bytes[(int)step] & 0x3F) << 2); //6 bit
            word[0] = (byte)(word[0] | (this.bytes[(int)high] & 0x03));

            word[1] = this.bytes[(int)low];

            return word;
        }

        private byte[] GenerateWord_VoltageHighJump()
        {
            return GenerateWord_BasicJump(
                ProgramStepBytes.VoltageHighJumpStep,
                ProgramStepBytes.VoltageHighJump_ByteHigh,
                ProgramStepBytes.VoltageHighJump_ByteLow);
        }

        private byte[] GenerateWord_VoltageLowJump()
        {
            return GenerateWord_BasicJump(
                ProgramStepBytes.VoltageLowJumpStep,
                ProgramStepBytes.VoltageLowJump_ByteHigh,
                ProgramStepBytes.VoltageLowJump_ByteLow);
        }

        private byte[] GenerateWord_CurrentHighJump()
        {
            return GenerateWord_BasicJump(
                ProgramStepBytes.CurrentHighJumpStep,
                ProgramStepBytes.CurrentHighJump_ByteHigh,
                ProgramStepBytes.CurrentHighJump_ByteLow);
        }

        private byte[] GenerateWord_CurrentLowJump()
        {
            return GenerateWord_BasicJump(
                ProgramStepBytes.CurrentLowJumpStep,
                ProgramStepBytes.CurrentLowJump_ByteHigh,
                ProgramStepBytes.CurrentLowJump_ByteLow);
        }

        public byte[][] Convert()
        {
            byte[][] words = new byte[CalcFinalWordCount()][];
            int itr = 0;

            words[itr++] = GenerateWord_VoltageSetPoint();
            words[itr++] = GenerateWord_CurrentSetPoint();

            if (IsSet(ProgramStepBytes.AbsoluteTimeJumpStep))
            {
                byte[][] temp = GenerateWords_AbsoluteJump();
                words[itr++] = temp[0];
                words[itr++] = temp[1];
            }

            if (IsSet(ProgramStepBytes.RelativeTimeJumpStep))
            {
                byte[][] temp = GenerateWords_RelativeJump();
                words[itr++] = temp[0];
                words[itr++] = temp[1];
            }

            if (IsSet(ProgramStepBytes.VoltageHighJumpStep))
            {
                words[itr++] = GenerateWord_VoltageHighJump();
            }

            if (IsSet(ProgramStepBytes.VoltageLowJumpStep))
            {
                words[itr++] = GenerateWord_VoltageLowJump();
            }

            if (IsSet(ProgramStepBytes.CurrentHighJumpStep))
            {
                words[itr++] = GenerateWord_CurrentHighJump();
            }

            if (IsSet(ProgramStepBytes.CurrentLowJumpStep))
            {
                words[itr++] = GenerateWord_CurrentLowJump();
            }

            return words;
        }

        public override string ToString()
        {
            string s = "";

            foreach (ProgramStepBytes psb in Enum.GetValues(typeof(ProgramStepBytes)))
            {
                if (s != "")
                {
                    s += "\t";
                }

                s += psb.ToString() + "\t" + bytes[(int)psb].ToString("x2");
            }

            return s;
        }

        public static ProgramStep CreateProgramStep(byte[] bytes)
        {
            if (bytes[(int)ProgramStepBytes.Voltage_ByteLow] == 0xFF || bytes[(int)ProgramStepBytes.Current_ByteLow] == 0xFF)
                return null;

            return new ProgramStep(bytes);
        }

        private static int BoolToBit(bool b)
        {
            return b ? 1 : 0;
        }

        //public BytePair Voltage = new BytePair();
        //public BytePair Current = new BytePair();

        //public BytePair VoltageJump = new BytePair();
        //public BytePair VoltageJumpStep = new BytePair();

        //public BytePair CurrentJump = new BytePair();
        //public BytePair CurrentJumpStep = new BytePair();

        //public byte RelativeTimeJump;
        //public byte RelativeTimeJumpStep;
        //public byte AbsoluteTimeJump;
        //public byte AbsoluteTimeJumpStep;

        //public byte LEDCharging;
        //public byte FlashLEDs;
        //public byte LEDError;
        //public byte Timer;
        //public byte OutputSwitch;
    }    
}
