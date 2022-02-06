using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoko_solver
{
    public static class OutputService
    {
        public static void setOutput(Board b, string inputby)
        {//output the solved board over given input
            string output = "";
            if (Solver.solve(ref b))
            {
                for (int rows = 0; rows < b.N; rows++)
                {
                    for (int cols = 0; cols < b.N; cols++)
                    {
                        output += b.board[rows, cols].value;
                    }
                }
            }
            if (inputby == "file")
            {
                writeToFile(output);
            }
            else if (inputby == "console")
            {
                Console.WriteLine(output);
            }
        }
        public static void writeToFile(string data)
        {
            //writing data to file if the file doesnt exist yet.
            Console.WriteLine("please enter file name to save the solution in- ");
            string fileName = Console.ReadLine();
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(
                Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\boardfiles\" + fileName)));
            if (!File.Exists(directory.ToString()))
            {
                // Create a file to write to.
                File.WriteAllText(directory.ToString(), data);
            }
            else
            {
                Console.WriteLine("file already exists");
                writeToFile(data);
            }

        }
    }
}
