using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    internal class Knight : ChessPiece
    {
        internal override int X { get; set; }
        internal override int Y { get; set; }
        internal override char Letter { get; set; }
        internal override UnitType PieceType { get; set; }
        internal override bool CanEat { get; set; }
        public Knight()
        {

        }
        public Knight(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Letter = 'N';
            PieceType = UnitType.Knight;
            CanEat = false;
        }

        internal override bool CheckMovement(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<List<BoardCell>> gameBoard, bool turn)
        {
            //forward, backwards, left,right
            if ((originalY + 2 == newY &&(originalX - newX == -1 || originalX - newX == 1)
               ) || (originalY - 2 == newY && (originalX - newX == -1 || originalX - newX == 1)
               ) || (originalX + 2 == newX && (originalY - newY == -1 || originalY - newY == 1)
               ) || (originalX - 2 == newX && (originalY - newY == -1 || originalY - newY == 1)))
            {
                if (!Utilities.IsPlayerUnit(newX, newY, list) && gameBoard[newY][newX].isOccupied)
                {
                    CanEat = true;
                    return true;
                }
                return true;
            }
            else
                return false;
        }
    }
}
