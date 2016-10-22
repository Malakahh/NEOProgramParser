using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace NEOProgramParser
{
    class ProgramParser
    {
        static readonly int metaDataSize = 24;

        List<byte> loadedBytes;
        
        byte[] programName = new byte[8];
        List<ProgramStep> steps = new List<ProgramStep>();

        public ProgramParser(string path)
        {
            loadedBytes = new List<byte>(File.ReadAllBytes(path));

            ParseProgramName();
            ParseProgramSteps();

            Console.WriteLine("Program Name: " + PrettyPrintProgramName());
            //Console.Write(this.PrettyPrintLoadedBytes());
            Console.Write(this.PrettyPrintProgramStepsByte());

            Console.ReadKey();
        }

        void ParseProgramName()
        {
            for (int i = 0; i < programName.Length; i++)
            {
                programName[i] = loadedBytes[1 + i];
            }
        }

        void ParseProgramSteps()
        {
            for (int i = metaDataSize; i < loadedBytes.Count; i += ProgramStep.stepSizeInBytes)
            {
                List<byte> list = new List<byte>();
                
                for (int k = 0; k < ProgramStep.stepSizeInBytes; k++)
                {
                    list.Add(loadedBytes[i + k]);
                }

                ProgramStep ps = ProgramStep.CreateProgramStep(list.ToArray());

                if (ps != null)
                {
                    steps.Add(ps);
                }
            }
        }

        private string PrettyPrintProgramName()
        {
            string s = "";

            foreach (byte b in programName)
            {
                s += Convert.ToChar(b);
            }

            return s;
        }

        private string PrettyPrintProgramStepsByte()
        {
            string s = "";

            foreach (ProgramStep ps in steps)
            {
                byte[][] words = ps.Convert();

                s += "**Step** size in words: " + ps.CalcFinalWordCount() + " == " + words.Length + ": " + (ps.CalcFinalWordCount() == words.Length).ToString() + "\n";

                for (int i = 0; i < words.Length; i++)
                {
                    if (words[i] != null)
                    {
                        s += words[i][0].ToString("x2");
                        s += words[i][1].ToString("x2");
                        s += " ";
                    }
                }

                s += "\n\n";
            }

            return s;
        }

        private string PrettyPrintProgramStepsString()
        {
            string s = "";

            foreach (ProgramStep ps in steps)
            {
                s += ps.ToString() + "\n\n";
            }

            return s;
        }

        private string PrettyPrintLoadedBytes()
        {
            string s = "";

            for (int i = 0; i < loadedBytes.Count; i++)
            {
                s += loadedBytes[i].ToString("x2");

                if ((i + 1) % 2 == 0)
                {
                    s += " ";
                }

                if ((i + 1) % 16 == 0 && i > 0)
                {
                    s += "\n";
                }
            }

            return s;
        }
    }
}
