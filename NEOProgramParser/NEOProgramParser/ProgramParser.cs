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

        public byte[] ProgramName = new byte[8];
        public byte[] WordCount = new byte[2];

        List<byte> loadedBytes;
       
        List<ProgramStep> steps = new List<ProgramStep>();

        public ProgramParser(string path)
        {
            loadedBytes = new List<byte>(File.ReadAllBytes(path));

            ParseProgramName();
            ParseProgramSteps();
            ParseWordCount();

            //Console.WriteLine("Program Name: " + PrettyPrintProgramName());
            //Console.Write(this.PrettyPrintLoadedBytes());
            //Console.Write(this.PrettyPrintProgramStepsByte());

            //Console.ReadKey();
        }

        private void ParseProgramName()
        {
            for (int i = 0; i < ProgramName.Length; i++)
            {
                ProgramName[i] = loadedBytes[1 + i];
            }
        }

        private void ParseProgramSteps()
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

        private void ParseWordCount()
        {
            int count = 0;

            foreach (ProgramStep ps in steps)
            {
                count += ps.CalcFinalWordCount(); 
            }

            this.WordCount[0] = (byte)((count & 0xFF00) >> 8);
            this.WordCount[1] = (byte)(count & 0xFF);
        }

        public void WriteToFile()
        {
            int count = this.WordCount[0] << 8;
            count = count | this.WordCount[1];

            byte[] bytes = new byte[count * 2];
            int itr = 0;

            foreach (ProgramStep ps in steps)
            {
                byte[][] words = ps.Convert();

                foreach (byte[] word in words)
                {
                    bytes[itr++] = word[0];
                    bytes[itr++] = word[1];
                }
            }

            File.WriteAllBytes(Directory.GetCurrentDirectory() + "\\" + PrettyPrintProgramName() + " - Translated.txt", bytes);
        }

        private string PrettyPrintProgramName()
        {
            string s = "";

            foreach (byte b in ProgramName)
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
