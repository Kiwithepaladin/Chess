using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    internal class Pawn : ChessPiece
    {
        internal override int X { get; set; }
        internal override int Y { get; set; }
        internal override char Letter { get; set; }
        internal override UnitType PieceType { get; set; }
        internal override bool CanEat { get; set; }
        internal bool unitMoved;
        public Pawn()
        {

        }
        public Pawn(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Letter = 'P';
            PieceType = UnitType.Pawn;
            CanEat = false;
            unitMoved = false;
        }
        internal override bool CheckMovement(int originalY, int originalX, int newY, int newX, List<ChessPiece> listOne, List<List<BoardCell>> gameBoard,  bool turn)
        {
            if (turn)
            {
                if (!Utilities.IsPlayerUnit(newX,newY,listOne) && gameBoard[newY][newX].isOccupied && (originalX + 1 == newX || originalX - 1 == newX) && originalY - newY == 1)
                {
                    this.CanEat = true;
                    return true;
                }
                if (unitMoved)
                {
                    
                    if (originalY - newY == 1 && originalX - newX == 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if ((originalY - newY == 2 || originalY - newY == 1) && originalX - newX == 0)
                        return true;
                    else
                        return false;
                }
            }
            else
            {
                if (!Utilities.IsPlayerUnit(newX, newY, listOne) && gameBoard[newY][newX].isOccupied && (originalX + 1 == newX || originalX - 1 == newX) && originalY - newY == -1)
                {
                    CanEat = true;
                    return true;
                }
                if (unitMoved)
                {
                    if (originalY - newY == -1  && originalX - newX == 0)
                        return true;
                    else
                        return false;
                }
                else
                {
                    if ((originalY - newY == -2 || originalY - newY == -1) && originalX - newX == 0)
                        return true;
                    else
                        return false;
                }
            }
        }
        internal override void Move(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard, bool turn)
        {
            base.Move(originalY, originalX, newY, newX, list, enemyList, gameBoard, turn);
            unitMoved = true;
        }

    }
}
