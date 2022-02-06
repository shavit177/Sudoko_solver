using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko_solver
{
    static class Solver
    {

        public static int ZERO_VAL = 48;
        public static bool solve(ref Board board)
        {// recursive action which solves the board
            singlePosInBoard(ref board);
            for (int rows = 0; rows < board.N; rows++)
            {
                for (int cols = 0; cols < board.N; cols++)
                {
                    if(board.board[rows,cols].value == '0')
                    {//empty cell

                        foreach (char value in board.board[rows,cols].posValue) // for every possibility
                        {

                            Board copyBoard = new Board(board);
                            Cell cell = new Cell(value, board.board[rows, cols].posValue.Replace(Convert.ToString(value),""), rows, cols);
                            if (isValidCell(cell, board)) // if cell is valid
                            {// set new cell in place
                                board.board[rows, cols] = cell;
                                board.updatePosValLists(cell);
                                if (solve(ref board))
                                {
                                    return true;
                                }
                                else
                                {
                                    board = copyBoard;
                                }
                            }
                        }
                        return false;
                    }
                }
            }
            Console.WriteLine("!!solved:))");

            return true;
        }
 
        public static void preGuessing()
        {

        }
        public static void singlePosInBoard(ref Board board)
        {// detects cells with only one possibility and sets it.
            for (int rows = 0; rows < board.N; rows++)
            {
                for (int cols = 0; cols < board.N; cols++)
                {
                    if (board.board[rows, cols].value == '0')
                    {
                        if (board.board[rows, cols].posValue.Length == 1)
                        {
                            board.board[rows, cols] = new Cell(board.board[rows, cols].posValue[0], rows, cols);
                            board.updatePosValLists(board.board[rows, cols]);
                        }
                        else
                        {
                            hiddenSinglePos(ref board, rows, cols);
                        }
                    }
                }
            }
        }
        public static void hiddenSinglePos(ref Board board, int row, int col)
        {// set all the values who are single possibilitiy in row/column/ piece.
            singleInRow(ref board, row);
            singleInCol(ref board, col);
            singleInPiece(ref board, board.findPiece(row,col));
        }
        public static void singleInRow(ref Board board,int row)
        {// finds single possibility of cell in a row
            int[] countArr = new int[board.N+1];
            for (int cols = 0; cols < board.N; cols++)
            {
                foreach(char pos in board.board[row, cols].posValue)
                {
                    countArr[(int)pos - ZERO_VAL] += 1;
                }
            }
            for(int i=0; i<countArr.Length;i++)
            {
                if (countArr[i] == 1)
                {
                    if(board.board[row, i - 1].value == '0')
                        board.board[row, i-1].value = board.board[row, i-1].posValue[0];
                }
            }
        }
        public static void singleInCol(ref Board board, int col)
        {
            int[] countArr = new int[board.N + 1];
            for (int rows = 0; rows < board.N; rows++)
            {
                foreach (char pos in board.board[rows, col].posValue)
                {
                    countArr[(int)pos - ZERO_VAL] += 1;
                }
            }
            for (int i = 0; i < countArr.Length; i++)
            {
                if (countArr[i] == 1)
                {
                    if (board.board[i - 1,col].value == '0' && board.board[i - 1, col].posValue.Length>0)
                        board.board[i - 1, col].value = board.board[i - 1,col].posValue[0];
                }
            }
        }
        public static void singleInPiece(ref Board board, int[] piece)
        {
            int[] countArr = new int[board.N + 1];
            for (int rows = piece[0]; rows <piece[0]+board.nPiece; rows++)
            {
                for (int cols = piece[1]; cols < piece[1] + board.nPiece; cols++)
                {
                    foreach (char pos in board.board[rows, cols].posValue)
                    {
                        countArr[(int)pos - ZERO_VAL] += 1;
                    }
                }
            }
            for (int i = 0; i < countArr.Length; i++)
            {
                if (countArr[i] == 1)
                {
                    if (board.board[piece[0]+(i - 1)/board.nPiece, piece[1]+(i-1)%board.nPiece].value == '0')
                        board.board[piece[0]+(i - 1) / board.nPiece, piece[1]+(i - 1) % board.nPiece].value = board.board[piece[0]+(i - 1) / board.nPiece, piece[1]+(i - 1) % board.nPiece].posValue[0];
                }
            }
        }
        public static bool isValidCell(Cell cell, Board board)
        {
            if(!board.inRow(cell.value,cell.rowI)&& !board.inCol(cell.value, cell.colI) && !board.inPiece(cell.value, board.findPiece(cell.rowI, cell.colI)))
            {
                return true;
            }
            return false;
        }
    }
}
