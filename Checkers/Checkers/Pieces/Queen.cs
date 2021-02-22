using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    internal class Queen : ChessPiece
    {
        internal override int X { get; set; }
        internal override int Y { get; set; }
        internal override char Letter { get; set; }
        internal override UnitType PieceType { get; set; }
        internal override bool CanEat { get; set; }
        public Queen()
        {

        }
        public Queen(int _x, int _y)
        {
            X = _x;
            Y = _y;
            Letter = 'Q';
            PieceType = UnitType.Queen;
            CanEat = false;
        }
        internal override bool CheckMovement(int originalY, int originalX, int newY, int newX, List<ChessPiece> list, List<List<BoardCell>> gameBoard, bool turn)
        {
            int tempX = originalX, tempY = originalY;
            //Right
            if (originalY == newY && originalX < newX)
            {
                for (int i = 1; i < Math.Abs(originalX - newX); i++)
                {
                    tempX++;
                    if (!gameBoard[tempY][tempX].isOccupied && tempX != originalX)
                    {
                        if (!Utilities.IsPlayerUnit(newX, newY, list))
                        {
                            CanEat = true;
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //Left
            else if (originalY == newY && originalX > newX)
            {
                for (int i = 1; i < Math.Abs(originalX - newX); i++)
                {
                    tempX--;
                    if (!gameBoard[tempY][tempX].isOccupied && tempX != originalX)
                    {
                        if (!Utilities.IsPlayerUnit(newX, newY, list))
                        {
                            CanEat = true;
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //Down
            else if (originalX == newX && originalY < newY)
            {
                for (int i = 1; i < Math.Abs(originalY - newY); i++)
                {
                    tempY++;
                    if (!gameBoard[tempY][tempX].isOccupied && tempY != originalY)
                    {
                        if (!Utilities.IsPlayerUnit(newX, newY, list))
                        {
                            CanEat = true;
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            //Up
            else if (originalX == newX && originalY > newY)
            {
                for (int i = 1; i < Math.Abs(originalY - newY); i++)
                {
                    tempY--;
                    if (!gameBoard[tempY][tempX].isOccupied && tempY != originalY)
                    {
                        if (!Utilities.IsPlayerUnit(newX, newY, list))
                        {
                            CanEat = true;
                            return true;
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            if ((Math.Abs(originalX - newX) == Math.Abs(originalY - newY)))
            {
                if (newY < originalY)
                {
                    if (newX < originalX)
                    {
                        //NorthWest
                        for (int i = 1; i < Math.Abs(originalX - newX); i++)
                        {
                            tempY--;
                            tempX--;
                            if (!gameBoard[tempY][tempX].isOccupied && tempX != originalX)
                            {
                                if (!Utilities.IsPlayerUnit(newX, newY, list))
                                {
                                    CanEat = true;
                                    return true;
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //NorthEast
                        for (int i = 1; i < Math.Abs(originalX - newX); i++)
                        {
                            tempY--;
                            tempX++;
                            if (!gameBoard[tempY][tempX].isOccupied && tempX != originalX)
                            {
                                if (!Utilities.IsPlayerUnit(newX, newY, list))
                                {
                                    CanEat = true;
                                    return true;
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
                else
                {
                    if (newX < originalX)
                    {
                        //SouthWest
                        for (int i = 1; i < Math.Abs(originalX - newX); i++)
                        {
                            tempY++;
                            tempX--;
                            if (!gameBoard[tempY][tempX].isOccupied && tempX != originalX)
                            {
                                if (!Utilities.IsPlayerUnit(newX, newY, list))
                                {
                                    CanEat = true;
                                    return true;
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        //SouthEast
                        for (int i = 1; i < Math.Abs(originalX - newX); i++)
                        {
                            tempY++;
                            tempX++;
                            if (!gameBoard[tempY][tempX].isOccupied && tempX != originalX)
                            {
                                if (!Utilities.IsPlayerUnit(newX, newY, list))
                                {
                                    CanEat = true;
                                    return true;
                                }
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}