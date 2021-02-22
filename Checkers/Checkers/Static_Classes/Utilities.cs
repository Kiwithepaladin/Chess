using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    internal class Utilities
    {
        internal static int FindIndex(int y, int x, List<ChessPiece> templist)
        {
            int index;
            return index = templist.FindIndex(coordinates => coordinates.X.Equals(x) && coordinates.Y.Equals(y));
        }
        internal static bool IsPlayerUnit(int numx, int numy, List<ChessPiece> unitList)
        {
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i].X == numx && unitList[i].Y == numy)
                {
                    return true;
                }
            }
            return false;
        }
        internal static void Swap(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<ChessPiece> enemeyList, List<List<BoardCell>> gameBoard, bool turn)
        {

            int index = FindIndex(originalY, originalX, list);
            gameBoard[originalY][originalX].isOccupied = true;
            gameBoard[newY][newX].isOccupied = true;
            
            list[index].X = newX;
            list[index].Y = newY;
            int index2 = FindIndex(newY, newX, list);
            list[index2].X = originalX;
            list[index].Y = originalY;
            var test = gameBoard[newY][newX].valueInCell;
            gameBoard[newY][newX].valueInCell = list[index].Letter;
            gameBoard[originalY][originalX].valueInCell = test;
            if (list[index].CanEat)
            {
                index = Utilities.FindIndex(newY, newX, enemeyList);
                enemeyList.RemoveAt(index);
            }
        }
    }
    
}
