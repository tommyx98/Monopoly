using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Jail : Tile 
    {
        public Dictionary<Player, int> InJail { get; set; }

        public Jail() 
        {
            InJail = new();
        }
        public override void Action(Player player) 
        {
            bool playerIsInJail = false;

            // checking if current player is in jail
            foreach(Player key in InJail.Keys)
            {
                if(key == player)
                {
                    playerIsInJail = true;
                }
            }

            // if player is on this tile and is not found in jail that means player is just visting
            if (!playerIsInJail)
            {
                Console.WriteLine("\n" + player.Name + " is visiting jail");
                return;
            }

            // player is in jail
            JailLogic(player);
        }

        private void JailLogic(Player player) 
        {
            Dice dice = new();
            string userInput = "";
            int[] diceThrow = dice.RollDice();
            int spacesToMove = diceThrow[0] + diceThrow[1];

            // player has been in jail for less than 3 turns and has not managed to escape
            if (InJail[player] > 0)
            {
                // asking user for options
                do
                {
                    Console.WriteLine("\n" + player.Name + " is in jail");
                    Console.WriteLine("You will now get 3 options: " +
                        "\n 1. Try rolling doubles, if succeeded immediately move forward" +
                        "\n 2. Use get out of jail card, move this turn" +
                        "\n 3. Pay 1000, move next turn");

                    userInput = Console.ReadLine();

                    // player tries to use invalid option
                    if (userInput.Equals("2") && player.OutOfJailCard < 1)
                    {
                        Console.WriteLine("\nYou do not have a get out of jail card");
                        userInput = "";
                    }

                    // player tries to use invalid option
                    if (userInput.Equals("3") && player.Money < 1000)
                    {
                        Console.WriteLine("\nNot enough funds to pay amount. Amount in bank: "
                            , player.Money);
                        userInput = "";
                    }
                }
                while (!userInput.Equals("1") && !userInput.Equals("2") && !userInput.Equals("3"));
            }

            if (userInput.Equals("1"))
            {
                // player is freed from jail and can move forward
                if(diceThrow[0] == diceThrow[1])
                {
                    InJail.Remove(player);

                    Console.WriteLine("You rolled " + diceThrow[0] + " and " + diceThrow[1],
                        ". Move forward " + spacesToMove + " spaces");

                    player.IsInJail = false;
                    player.MovePlayer(spacesToMove);

                }

                else
                {
                    Console.WriteLine("You rolled " + diceThrow[0] + " and " + diceThrow[1] +
                        ". You do not move forward");

                    // removes turn left in jail
                    InJail[player] -= 1;
                }
            }

            else if (userInput.Equals("2"))
            {
                InJail.Remove(player);

                Console.WriteLine("You used get out of jail card and is now freed");

                Console.WriteLine("You rolled " + diceThrow[0] + " and " + diceThrow[1] +
                        ". Move forward " + spacesToMove + " spaces");

                player.IsInJail = false;
                player.MovePlayer(spacesToMove);
            }

            else if (userInput.Equals("3"))
            {
                InJail.Remove(player);
                player.Pay(1000);
            }

            // player has been in jail for three turns and is set free
            else
            {
                InJail.Remove(player);
                Console.WriteLine(player.Name + " has been in jail for 3 turns and is set free");

                Console.WriteLine("You rolled " + diceThrow[0] + " and " + diceThrow[1]+
                        ". Move forward " + spacesToMove + " spaces");

                player.IsInJail = false;
                player.MovePlayer(spacesToMove);
            }
        }
    }
}
