using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko_solver
{
    public class Board
    {
        //CONSTANTS -------------------------------------------------
        public int N { get; set; } // size of the board NxN
        //PROPERTIES --------------------------------------------------
        public Cell[,] board { get; set; }
        public string inputby { get; set; } //file or console way of input data
        public int nPiece { get; set; } //width of each piece of the board
        public Board(Board b) // copy of other board
        {
            N = b.N;
            board = b.board;
            inputby = b.inputby;
            nPiece = b.nPiece;
        }
        public Board(string data, string inputby, int N)
        {
            this.inputby = inputby;
            this.board = new Cell[N, N];
            this.N = N;
            this.nPiece = (int)Math.Sqrt(N);
            for (int rows = 0; rows < this.N; rows++)
            {
                for (int cols = 0; cols < this.N; cols++)
                {
                    char curChar = Convert.ToChar(data[cols + rows * N]);
                    if (curChar == '0')
                    {
                        this.board[rows, cols] = new Cell(curChar, genPosValuesForCell('0', rows, cols),rows,cols);
                    }
                    else
                    {
                        this.board[rows,cols] = new Cell(curChar,rows,cols);
                        updatePosValLists(curChar, rows, cols);
                    }
                }
            }
            //updateAllPosValLists();
        }

        public void PrintBoard()
        {
            for (int rows = 0; rows < this.N; rows++)
            {
                for (int j = 0; j < N; j++)
                {
                    Console.Write((rows % nPiece == 0) ? "===" : "---");
                }
                Console.WriteLine();
                for (int cols = 0; cols < N; cols++)
                {
                    Console.Write((cols % nPiece == 0) ? "||" : "|");
                    Console.Write(this.board[rows, cols]);
                }
                Console.WriteLine("||");
            }
        }
        public bool inCol(char celVal, int col)
        {// return true/false for value in column
            for (int rows = 0; rows < N; rows++)
            {
                if (board[rows, col] != null)
                {
                    if (board[rows, col].value == celVal)
                        return true;
                }
            }
            return false;
        }
        public bool inRow(char celVal, int row)
        {// return true/false for value in row
            for (int cols = 0; cols < N; cols++)
            {
                if (board[row, cols] != null)
                {
                    if (board[row, cols].value == celVal)
                        return true;
                }
            }
            return false;
        }
        public bool inPiece(char celVal, int[] piece)
        {// return true/false for value in piece
            for (int rows = piece[0]; rows < this.nPiece; rows++)
            {
                for (int cols = piece[1]; cols < nPiece; cols++)
                {
                    if (board[rows, cols] != null)
                    {
                        if (board[rows, cols].value == celVal)
                            return true;
                    }
                }
            }
            return false;
        }
        public int[] findPiece(int row,int col)
        {//return which piece contains the cell (piece is defined by its left-top cell and its nPiece size)
            int[] ret = new int[2];
            ret[0] = (col / nPiece)*nPiece;
            ret[1] = (row / nPiece)*nPiece;
            return ret;
        }
        public string genPosValuesForCell(char value,int row, int col)
        {//generate a string with all possible values to be completed in cell
            //recieves only un-sourced cells
            string posValues = "";
            for (int i = 1; i <= this.N; i++)
            {
                char curVal = Convert.ToString(i)[0];
                if (!inRow(curVal, row) && !inCol(curVal, col) && !inPiece(curVal, findPiece(row,col))&& curVal!=value)
                {
                    posValues += Convert.ToString(i);
                }
            }
            return posValues;
        }
        public void updatePosValListsRows(char value, int col)
        {
            for (int rows = 0; rows < N; rows++) //update row
            {
                Cell curCell = this.board[rows, col];
                if (curCell != null)
                {
                    curCell.posValue = curCell.posValue.Replace(Convert.ToString(value), "");
                }
            }
        }
        public void updatePosValListsCols(char value, int row)
        {
            for (int cols = 0; cols < N; cols++) //update col
            {
                Cell curCell = this.board[row, cols];
                if (curCell != null)
                {
                    curCell.posValue = curCell.posValue.Replace(Convert.ToString(value), "");
                }
            }
        }
        public void updatePosValListsPiece(char value, int row, int col)
        {
            int[] curPiece = findPiece(row,col);
            for (int rows = curPiece[0]; rows < this.nPiece; rows++) // update piece
            {
                for (int cols = curPiece[1]; cols < this.nPiece; cols++)
                {
                    Cell curCell = this.board[rows, cols];
                    if (curCell != null)
                    {
                        curCell.posValue = curCell.posValue.Replace(Convert.ToString(value), "");
                    }
                }
            }
        }

        public void updatePosValLists(char value, int row, int col)
        {
            updatePosValListsRows(value, col);
            updatePosValListsCols(value, row);
            updatePosValListsPiece(value,row,col);
        }
    }
}
