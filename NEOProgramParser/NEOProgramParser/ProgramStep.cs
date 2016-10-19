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
            C = 28, //LED Charging
            F = 29, //Flash LEDs
            E = 30, //LED Error
            T = 31, //Timer
            S = 32  //Output switch
        }

        public static readonly int stepSizeInBytes = 36;

        public byte[] bytes;

        public ProgramStep(byte[] bytes)
        {
            this.bytes = bytes;
        }

        public override string ToString()
        {
            return "Voltage_ByteLow:\t" + bytes[(int)ProgramStepBytes.Voltage_ByteLow].ToString("x2") +
                "\tVoltage_ByteHigh:\t" + bytes[(int)ProgramStepBytes.Voltage_ByteHigh].ToString("x2") +
                "\tCurrent_ByteLow:\t" + bytes[(int)ProgramStepBytes.Current_ByteLow].ToString("x2") +
                "\tCurrent_ByteHigh:\t" + bytes[(int)ProgramStepBytes.Current_ByteHigh].ToString("x2") +
                "\tVoltageLowJumpStep:\t" + bytes[(int)ProgramStepBytes.VoltageLowJumpStep].ToString("x2") +
                "\tVoltageHighJumpStep:\t" + bytes[(int)ProgramStepBytes.VoltageHighJumpStep].ToString("x2") +
                "\tVoltageLowJump_ByteLow:\t" + bytes[(int)ProgramStepBytes.VoltageLowJump_ByteLow].ToString("x2") +
                "\tVoltageLowJump_ByteHigh:\t" + bytes[(int)ProgramStepBytes.VoltageLowJump_ByteHigh].ToString("x2") +
                "\tVoltageHighJump_ByteLow:\t" + bytes[(int)ProgramStepBytes.VoltageHighJump_ByteLow].ToString("x2") +
                "\tVoltageHighJump_ByteHigh:\t" + bytes[(int)ProgramStepBytes.VoltageHighJump_ByteHigh].ToString("x2") +
                "\tCurrentLowJumpStep:\t" + bytes[(int)ProgramStepBytes.CurrentLowJumpStep].ToString("x2") +
                "\tCurrentHighJumpStep:\t" + bytes[(int)ProgramStepBytes.CurrentHighJumpStep].ToString("x2") +
                "\tCurrentLowJump_ByteLow:\t" + bytes[(int)ProgramStepBytes.CurrentLowJump_ByteLow].ToString("x2") +
                "\tCurrentLowJump_ByteHigh:\t" + bytes[(int)ProgramStepBytes.CurrentLowJump_ByteHigh].ToString("x2") +
                "\tCurrentHighJump_ByteLow:\t" + bytes[(int)ProgramStepBytes.CurrentHighJump_ByteLow].ToString("x2") +
                "\tCurrentHighJump_ByteHigh:\t" + bytes[(int)ProgramStepBytes.CurrentHighJump_ByteHigh].ToString("x2") +
                "\tRelativeTimeJumpStep:\t" + bytes[(int)ProgramStepBytes.RelativeTimeJumpStep].ToString("x2") +
                "\tAbsoluteTimeJumpStep:\t" + bytes[(int)ProgramStepBytes.AbsoluteTimeJumpStep].ToString("x2") +
                "\tRelativeTime_0:\t" + bytes[(int)ProgramStepBytes.RelativeTime_0].ToString("x2") +
                "\tRelativeTime_1:\t" + bytes[(int)ProgramStepBytes.RelativeTime_1].ToString("x2") +
                "\tRelativeTime_2:\t" + bytes[(int)ProgramStepBytes.RelativeTime_2].ToString("x2") +
                "\tAbsoluteTime_0:\t" + bytes[(int)ProgramStepBytes.AbsoluteTime_0].ToString("x2") +
                "\tAbsoluteTime_1:\t" + bytes[(int)ProgramStepBytes.AbsoluteTime_1].ToString("x2") +
                "\tAbsoluteTime_2:\t" + bytes[(int)ProgramStepBytes.AbsoluteTime_2].ToString("x2") +
                "\tC:\t" + bytes[(int)ProgramStepBytes.C].ToString("x2") +
                "\tF:\t" + bytes[(int)ProgramStepBytes.F].ToString("x2") +
                "\tE:\t" + bytes[(int)ProgramStepBytes.E].ToString("x2") +
                "\tT:\t" + bytes[(int)ProgramStepBytes.T].ToString("x2") +
                "\tS:\t" + bytes[(int)ProgramStepBytes.S].ToString("x2");
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
