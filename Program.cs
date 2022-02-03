using System;

namespace Sudoko_solver
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(Convert.ToString(3)[0]);
            string posValue = "123456789";
            posValue = posValue.Replace(Convert.ToString('2'), "");
            Console.WriteLine(posValue);
             IFileFormatReader reader;
            reader = new FileReader();
            //Console.WriteLine(reader.readData());
            Board B = new Board(reader.readData(), "file", 9);

            B.PrintBoard();
        }
    }
}
