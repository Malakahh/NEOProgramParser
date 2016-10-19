using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NEOProgramParser
{
    class Program
    {
        static void Main(string[] args)
        {
            args = new string[] { "D:\\GitRepos\\NEOProgramParser\\NEOProgramParser\\NEOProgramParser\\bin\\Debug\\NB241017.GBC" };

            if (args.Length == 0)
            {
                Console.WriteLine("No path found");
                Console.ReadKey();
            }
            else
            {
                Console.WriteLine("Path: " + args[0] + "\n");
                new ProgramParser(args[0]);
            }
        }
    }
}
