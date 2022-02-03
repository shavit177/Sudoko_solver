using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko_solver
{
   public class ConsoleReader : IFileFormatReader
    {
        public string readData()
        {
            return Console.ReadLine();
        }
    }
}
