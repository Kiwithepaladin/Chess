using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chess.Project
{
    public static class Errors
    {
        public static void InvalidInputErrorMsg()
        {
            Console.WriteLine("Invalid input. press any button to continue...");
        }
        public static void CantExecuteMove()
        {
            Console.WriteLine("Invalid input, can't exucte this move. press any button to continue...");
        }
        public static void UnitIsInThePath()
        {
            Console.WriteLine("Unit is in the way");
        }
    }
}
