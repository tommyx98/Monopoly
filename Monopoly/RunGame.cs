using System;
using System.Collections.Generic;

namespace Monopoly
{
    public class RunGame
    {
        static void Main(string[] args) 
        {
            Facade facade = new();
            FacadeMethods decorator = new SpecialRun(facade);

            Board theBoard = facade.MakeMonopolyBoard();

            theBoard.AllPlayers = facade.CreatePlayerList(theBoard);

            Console.WriteLine("\nAll players get $" + theBoard.AllPlayers[0].Money + " in starting funds ");

            // implemented decorator to show mastery of thi design pattern
            decorator.RunGame(theBoard);
        }
    }
}
