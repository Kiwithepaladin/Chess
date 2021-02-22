using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    internal abstract class ChessPiece
    {
        internal abstract int X { get; set; }
        internal abstract int Y { get; set; }
        internal abstract char Letter { get; set; }
        internal abstract UnitType PieceType { get; set; }
        internal abstract bool CanEat { get; set; }
        public override string ToString()
        {
            return (" " + Letter + " ");
        }
        internal abstract bool CheckMovement(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<List<BoardCell>> gameBoard, bool turn);
        internal virtual void Move(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<ChessPiece> enemeyList, List<List<BoardCell>> gameBoard, bool turn)
        {

                int index = Utilities.FindIndex(originalY, originalX, list);
                gameBoard[originalY][originalX].isOccupied = false;
                gameBoard[newY][newX].isOccupied = true;
                list[index].X = newX;
                list[index].Y = newY;
                gameBoard[newY][newX].valueInCell = list[index].Letter;
                if (list[index].CanEat)
                {
                    index = Utilities.FindIndex(newY, newX, enemeyList);
                    enemeyList.RemoveAt(index);
                }
        }
    }   
}
