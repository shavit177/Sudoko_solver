using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Sudoko_solver
{
    public class FileReader : IFileFormatReader
    {
        public string readData()
        {
            // Read the file as one string.
            Console.WriteLine("please enter valid file name- ");
            string filename = Console.ReadLine();

            string text = System.IO.File.ReadAllText(@validatingFileName(filename));
            return text;
        }
        public string validatingFileName(string filename)
        {
            string currentDir = Environment.CurrentDirectory;
            DirectoryInfo directory = new DirectoryInfo(
                Path.GetFullPath(Path.Combine(currentDir, @"..\..\..\boardfiles\" + filename)));
            return directory.ToString();
        }
    }
}
