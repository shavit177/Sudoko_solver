using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko_solver
{
   public class ConsoleReader : IFileFormatReader
    {
        public string readData()
        {
            Console.WriteLine("please enter data-");
            return Console.ReadLine();
        }
    }
}
