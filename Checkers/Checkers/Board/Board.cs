using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    public class Board
    {
        private const int boardSize = 8;
        private List<List<BoardCell>> gameBoard;
        private List<ChessPiece> playersUnits;
        private List<ChessPiece> enemyUnits;
        private string turnString;
        private King playerKing;
        private King enemyKing;
        private bool turn;
        #region BoardRelated
        public Board()
        {
            NewBoard();
        }
        public void NewBoard()
        {
            gameBoard = new List<List<BoardCell>>();
            playersUnits = new List<ChessPiece>();
            enemyUnits = new List<ChessPiece>();
            turn = true;
            turnString = "Player";

            for (int i = 0; i < boardSize; i++)
            {
                gameBoard.Add(new List<BoardCell>());
                for (int j = 0; j < boardSize; j++)
                {
                    BoardCell temp = new BoardCell
                    {
                        x = j,
                        y = i,
                        isOccupied = false,
                        valueInCell = 'x'
                    };
                    gameBoard[i].Add(temp);
                }
            }
            DrawInitalBoard();
            DrawBoard();
        }
        private void DrawBoard()
        {
            Console.Clear();
            if (turn)
            {
                Console.ForegroundColor = ConsoleColor.Blue;
                turnString = "Blue Player";
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                turnString = "Red Player";
            }
            Console.WriteLine("             {0} turn", turnString);
            Console.ResetColor();
            Console.Write("\n" + "    0   1   2   3   4   5   6   7" + "\n");
            for (int i = 0; i < boardSize; i++)
            {
                Console.Write("\n");
                Console.Write(i + "");
                for (int j = 0; j < boardSize; j++)
                {
                    if (gameBoard[i][j].isOccupied)
                    {
                        if (Utilities.IsPlayerUnit(j, i, playersUnits))
                        {
                            Console.ForegroundColor = ConsoleColor.Blue;
                            Console.Write("   " + gameBoard[i][j].valueInCell);
                            Console.ResetColor();
                        }
                        else if (Utilities.IsPlayerUnit(j, i, enemyUnits))
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.Write("   " + gameBoard[i][j].valueInCell);
                            Console.ResetColor();
                        }
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.ResetColor();
                    }
                    else
                    {
                        Console.Write("   " + 'x');
                    }
                }
            }
            Turn();
        }
        private void DrawInitalBoard()
        {
            for (int y = 0; y < boardSize; y++)
            {
                for (int x = 0; x < boardSize; x++)
                {
                    InitialzeAllPices(y,x, playersUnits, enemyUnits, gameBoard);
                }
            }
        }
        #endregion
        private void Turn()
        {
            try
            {
                while (playersUnits.Contains(playerKing) && enemyUnits.Contains(enemyKing))
                {
                    int targetX, targetY, moveX, moveY;
                    List<ChessPiece> tempList = playersUnits;
                    List<ChessPiece> tempOppositeList = enemyUnits;
                    Console.WriteLine("\n" + "\n" + "Choose a target (Y axis): ");
                    int.TryParse(Console.ReadLine(), out targetY);
                    Console.WriteLine("Choose a target (X axis): ");
                    int.TryParse(Console.ReadLine(), out targetX);
                    Concede(targetY);
                    if (turn)
                    {
                        tempList = playersUnits;
                        tempOppositeList = enemyUnits;
                    }
                    else
                    {
                        tempList = enemyUnits;
                        tempOppositeList = playersUnits;
                    }
                    if (Utilities.IsPlayerUnit(targetX, targetY, tempList))
                    {
                        Console.WriteLine("Where would you like to move your target (Y axis): ");
                        int.TryParse(Console.ReadLine(), out moveY);
                        Console.WriteLine("Where would you like to move your target (X axis): ");
                        int.TryParse(Console.ReadLine(), out moveX);
                        int index = Utilities.FindIndex(moveY, moveX, tempList);
                        Rook tempRook = tempList[index] as Rook;
                        if (Utilities.IsPlayerUnit(moveX, moveY, tempList) && tempList[index].PieceType == UnitType.Rook &&
                        !playerKing.unitMoved && !tempRook.unitMoved)
                        {
                            int index2 = Utilities.FindIndex(targetY, targetX, tempList);
                            King tempKing = tempList[index2] as King;
                            Utilities.Swap(targetY, targetX, moveY, moveX, tempList, tempOppositeList, gameBoard, turn);
                            turn = !turn;
                            DrawBoard();
                        }
                        else if (IsValidInput(targetX, targetY, moveX, moveY,tempList))
                        {
                            MoveUnit(targetY, targetX, moveY, moveX, tempList,tempOppositeList);
                        }
                        else
                        {
                               Turn();
                        }
                    }
                    else
                    {
                        Errors.InvalidInputErrorMsg();
                        Turn();
                    }
                }
                if (!playersUnits.Contains(playerKing))
                    Console.WriteLine("Red player Won!!!");
                else if (!enemyUnits.Contains(enemyKing))
                {
                    Console.WriteLine("Blue player Won!!!");

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                Errors.InvalidInputErrorMsg();
                Turn();
            }

        }
        private bool IsValidInput(int orgX, int orgY, int destX, int destY,List<ChessPiece> tempList)
        {
                int index1 = Utilities.FindIndex(orgY, orgX, tempList);
                if (tempList[index1].CheckMovement(orgY,orgX,destY,destX,tempList,gameBoard,turn))
                {
                    return true;
                }
                 return false;
        }
        private void MoveUnit(int moveY, int moveX, int targetY, int targetX, List<ChessPiece> list,List<ChessPiece> enemyList)
        {
            try
            {
                int tempIndex = Utilities.FindIndex(moveY, moveX, list);
                list[tempIndex].Move(moveY, moveX, targetY, targetX, list, enemyList, gameBoard, turn);
            }
            catch (ArgumentOutOfRangeException)
            {

                Console.WriteLine("Problem in MoveUnit method");
            }
            turn = !turn;
            DrawBoard();
        }
        #region DrawInital
        private void InitialzeAllPices(int y, int x, List<ChessPiece> playerList, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard)
        {
            //DrawInitalPawns(y, x, playerList, enemyList, gameBoard);
            //DrawInitalBishops(y, x, playerList, enemyList, gameBoard);
            DrawInitalRooks(y, x, playerList, enemyList, gameBoard);
            //DrawInitalKnights(y, x, playerList, enemyList, gameBoard);
            //DrawInitalQueens(y, x, playerList, enemyList, gameBoard);
            DrawInitalKings(y, x, playerList, enemyList, gameBoard);
        }
        private void DrawInitalPawns(int y, int x, List<ChessPiece> playerList, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard)
        {

            Pawn tempPawn = new Pawn(x, y);

            if (y == 1)
            {
                gameBoard[y][x].isOccupied = true;
                enemyList.Add(tempPawn);
                int index = Utilities.FindIndex(y, x, enemyList);
                gameBoard[y][x].valueInCell = enemyList[index].Letter;
            }
            else if (y == boardSize - 2)
            {
                gameBoard[y][x].isOccupied = true;
                playerList.Add(tempPawn);
                int index = Utilities.FindIndex(y, x, playerList);
                gameBoard[y][x].valueInCell = playerList[index].Letter;
            }

        }
        private void DrawInitalRooks(int y, int x, List<ChessPiece> playerList, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard)
        {
            Rook tempRook = new Rook(x, y);
            if (y == boardSize - boardSize && (x == boardSize - boardSize || x == boardSize - 1))
            {
                gameBoard[y][x].isOccupied = true;
                enemyList.Add(tempRook);
                int index = Utilities.FindIndex(y, x, enemyList);
                gameBoard[y][x].valueInCell = enemyList[index].Letter;
            }
            if (y == boardSize - 1 && (x == boardSize - boardSize || x == boardSize - 1))
            {
                gameBoard[y][x].isOccupied = true;
                playerList.Add(tempRook);
                int index = Utilities.FindIndex(y, x, playerList);
                gameBoard[y][x].valueInCell = playerList[index].Letter;
            }
        }
        private void DrawInitalBishops(int y, int x, List<ChessPiece> playerList, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard)
        {
            Bishop tempBishop = new Bishop(x, y);
            if (y == 0 && (x == 2 || x == boardSize - 3))
            {
                gameBoard[y][x].isOccupied = true;
                enemyList.Add(tempBishop);
                int index = Utilities.FindIndex(y, x, enemyList);
                gameBoard[y][x].valueInCell = enemyList[index].Letter;
            }
            if (y == boardSize - 1 && (x == 2 || x == boardSize - 3))
            {
                gameBoard[y][x].isOccupied = true;
                playerList.Add(tempBishop);
                int index = Utilities.FindIndex(y, x, playerList);
                gameBoard[y][x].valueInCell = playerList[index].Letter;
            }
        }
        private void DrawInitalKnights(int y, int x, List<ChessPiece> playerList, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard)
        {
            Knight tempKnight = new Knight(x, y);
            if (y == boardSize - boardSize && (x == 1 || x == boardSize - 2))
            {
                gameBoard[y][x].isOccupied = true;
                enemyList.Add(tempKnight);
                int index = Utilities.FindIndex(y, x, enemyList);
                gameBoard[y][x].valueInCell = enemyList[index].Letter;
            }
            if (y == boardSize - 1 && (x == 1 || x == boardSize - 2))
            {
                gameBoard[y][x].isOccupied = true;
                playerList.Add(tempKnight);
                int index = Utilities.FindIndex(y, x, playerList);
                gameBoard[y][x].valueInCell = playerList[index].Letter;
            }
        } 
        private void DrawInitalQueens(int y, int x, List<ChessPiece> playerList, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard)
        {
            Queen tempQueen = new Queen(x, y);
            if (y == boardSize - boardSize && x == 3)
            {
                gameBoard[y][x].isOccupied = true;
                enemyList.Add(tempQueen);
                int index = Utilities.FindIndex(y, x, enemyList);
                gameBoard[y][x].valueInCell = enemyList[index].Letter;
            }
            if (y == boardSize - 1 && x == 4)
            {
                gameBoard[y][x].isOccupied = true;
                playerList.Add(tempQueen);
                int index = Utilities.FindIndex(y, x, playerList);
                gameBoard[y][x].valueInCell = playerList[index].Letter;
            }
        }
        private void DrawInitalKings(int y, int x, List<ChessPiece> playerList, List<ChessPiece> enemyList, List<List<BoardCell>> gameBoard)
        {
            King tempKing = new King(x, y);
            if (y == boardSize - boardSize && x == 4)
            {
                gameBoard[y][x].isOccupied = true;
                enemyList.Add(tempKing);
                enemyKing = tempKing;
                int index = Utilities.FindIndex(y, x, enemyList);
                gameBoard[y][x].valueInCell = enemyList[index].Letter;
            }
            if (y == boardSize - 1 && x == 3)
            {
                gameBoard[y][x].isOccupied = true;
                playerList.Add(tempKing);
                playerKing = tempKing;
                int index = Utilities.FindIndex(y, x, playerList);
                gameBoard[y][x].valueInCell = playerList[index].Letter;
            }
        }
        #endregion
        private void Concede(int Y)
        {
            if (Y == 42)
                NewBoard();
            else if (Y == 31 && turn)
            {
                playersUnits.Clear();
            }
            else if (Y == 31 && !turn)
            {
                enemyUnits.Clear();
            }
        }
        private void CheckCheck()
        {

        }

    }
}
