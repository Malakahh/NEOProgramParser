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
        List<byte> loadedBytes;

        byte[] programName = new byte[8];

        public ProgramParser(string path)
        {
            loadedBytes = new List<byte>(File.ReadAllBytes(path));

            GetProgramName();

            Console.WriteLine("Program Name: " + PrettyPrintProgramName());
            Console.Write(this.PrettyPrintLoadedBytes());

            Console.ReadKey();
        }

        void GetProgramName()
        {
            for (int i = 0; i < programName.Length; i++)
            {
                programName[i] = loadedBytes[1 + i];
            }
        }

        string PrettyPrintProgramName()
        {
            string s = "";

            foreach (byte b in programName)
            {
                s += Convert.ToChar(b);
            }

            return s;
        }

        string PrettyPrintLoadedBytes()
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
