using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    public class BoardCell
    {
        public int x;
        public int y;
        public bool isOccupied;
        public char valueInCell;


        public BoardCell()
        {
            x = 0;
            y = 0;
            isOccupied = false;
            valueInCell = 'X';
        }
    }
}
