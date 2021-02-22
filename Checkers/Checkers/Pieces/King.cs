using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    internal class King : ChessPiece
    {
        internal override int X { get; set; }
        internal override int Y { get; set; }
        internal override char Letter { get; set; }
        internal override UnitType PieceType { get; set; }
        internal override bool CanEat { get; set; }
        internal bool unitMoved { get; set; }
        public King()
        {

        }
        public King(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Letter = 'K';
            PieceType = UnitType.King;
            CanEat = false;
        }

        internal override bool CheckMovement(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<List<BoardCell>> gameBoard, bool turn)
        {
           
            int index = Utilities.FindIndex(newY, newX, list);
            int index2 = Utilities.FindIndex(originalY, originalX, list);
            if(((Math.Abs(originalX-newX) == 0) || Math.Abs(originalX - newX) == 1) && ((Math.Abs(originalY - newY) == 0) || Math.Abs(originalY - newY) == 1))
              {
                if(!Utilities.IsPlayerUnit(newX, newY, list) && gameBoard[newY][newX].isOccupied)
                {
                    CanEat = true;
                    return true;
                }
                return true;
            }
            else
                return false;  
        }
        internal override void Move(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<ChessPiece> enemeyList, List<List<BoardCell>> gameBoard, bool turn)
        {
            base.Move(originalY, originalX, newY, newX, list, enemeyList, gameBoard, turn);
            unitMoved = true;
        }

    }
}
