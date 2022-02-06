using System;
using System.Collections.Generic;
using System.Text;

namespace Sudoko_solver
{

    public class Cell
    {
        //CONSTANTS -------------------------------------------------

        //PROPERTIES -------------------------------------------------
        public int rowI;
        public int colI;
        public bool isLocked;  //To know whether it can be altered or not.
                               //If Locked, then it cannot be altered
        public char value;   //current value
        public bool isSource;  //To know whether this is the source data or not.
        public string posValue;//to list down possible values it can hold
        //public int quarter;    //to know which quarter the array belongs
        //public string rem;     //remarks columns
        public Cell(Cell c)
        {
            value = c.value;
            isSource = c.isSource;
            posValue = (string)c.posValue.Clone();
            colI = c.colI;
            rowI = c.rowI;
        }
        public Cell(char value, int row, int col) 
        {// init cell with source values
            this.value = value;
            isLocked = true;
            isSource = true;
            posValue = "";
            colI = col;
            rowI = row;
        }
        public Cell(char value, string posValues, int row, int col)
        {// init cells to be completed
            this.value =value;
            isLocked = false;
            isSource = false;
            posValue = posValues;
            colI = col;
            rowI = row;
        }
        public void lockCell()
        {
            this.isLocked = true;
        }
        public void unLockCell()
        {
            this.isLocked = false;
        }
        public override string ToString()
        {
            return Convert.ToString(this.value);
        }

    }


}
