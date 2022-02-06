using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko_solver
{
    public static class InputReaderService
    {
        //CONSTANTS -------------------------------
        public static int ZERO_VAL = 48;
        public static void recievingInput()
        {
            //recieves a way of input and the input itself and activates solve methods.
            IFileFormatReader reader;
            Board B;
            Console.WriteLine("pleace enter a way of inputting data: for reading from file enter 'file' \n for reading from console enter 'console");
            bool input=false; //wether input is valid or not
            string inputby = Console.ReadLine();
            while (!input)
            {
                if(inputby == "file")
                {
                    input = true;
                    reader = new FileReader();
                    B = isValidInput(reader.readData());
                    if (B != null)
                    {

                        OutputService.setOutput(B, inputby);
                    }
                }
                else if(inputby == "console")
                {
                    input = true;
                    reader = new ConsoleReader();
                    B = isValidInput(reader.readData());
                    if (B != null)
                        OutputService.setOutput(B,inputby);
                        //Solver.solve(ref B);
                }
                else
                {
                    Console.WriteLine("input is invalid");
                    inputby = Console.ReadLine();
                }
            }
            
        }
        public static Board isValidInput(string data)
        {//scanning the input, returns a board if its valid and null if its not.

            int len = data.Length;
            double nBoard = Math.Sqrt(len);
            double nPiece = Math.Sqrt(nBoard);
            if ((int)nPiece != nPiece) //valid length of input
                return null;
            foreach(char c in data)
            {
                if(c-ZERO_VAL > nBoard || c-ZERO_VAL<0)//valid value
                {
                    return null;
                }
            }
            Board b = new Board(data,(int)nBoard);
            return (b.IsValidBoard())? b: null; // valid board
        }
    }
}
