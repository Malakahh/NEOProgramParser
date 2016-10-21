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
            //Desktop
            //args = new string[] { "D:\\GitRepos\\NEOProgramParser\\NEOProgramParser\\NEOProgramParser\\bin\\Debug\\NB241017.GBC" };

            //Laptop
            //args = new string[] { "C:\\GitRepos\\NEOProgramParser\\NEOProgramParser\\NEOProgramParser\\NB241017.GBC" };
            args = new string[] { "C:\\GitRepos\\NEOProgramParser\\NEOProgramParser\\NEOProgramParser\\SMALLTST.GBC" };

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
