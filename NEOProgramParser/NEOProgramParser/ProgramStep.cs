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

        public static int stepSizeInBytes = 36;

        public byte[] bytes;

        public ProgramStep(byte[] bytes)
        {
            this.bytes = bytes;
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
