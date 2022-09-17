using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Facade : FacadeMethods
    {
        public Board MakeMonopolyBoard() 
        {
            Board board = new();

            Tile[] theBoard = new Tile[40];

            Factory factory = new();

            // chance cards are at position 2, 7, 17, 22, 33, 36

            theBoard[0] = factory.CornerCreation("start");
            theBoard[1] = factory.Land("Mediterranean Avenue", 1200, 1000, 1000, 40, Color.BROWN, 200, 600, 1800, 3200, 5000);

            theBoard[3] = factory.Land("Baltic Avenue", 1200, 1000, 1000, 80, Color.BROWN, 400, 1200, 3600, 6400, 9000);
            theBoard[4] = factory.TaxCard(4000, "Income Tax");
            theBoard[5] = factory.Railroad("Reading Railroad");
            theBoard[6] = factory.Land("Oriental Avenue", 2000, 1000, 1200, 120, Color.LBLUE, 600, 1800, 5400, 8000, 11000);

            theBoard[8] = factory.Land("Vermont Avenue", 2000, 1000, 1200, 120, Color.LBLUE, 600, 1800, 5400, 8000, 11000);
            theBoard[9] = factory.Land("Connecticut Avenue", 2400, 1000, 1300, 160, Color.LBLUE, 800, 2000, 6000, 9000, 12000);
            theBoard[10] = factory.CornerCreation("jail");
            theBoard[11] = factory.Land("ST. Charles Place", 2800, 2000, 2100, 200, Color.PINK, 1000, 3000, 9000, 12500, 15000);
            theBoard[12] = factory.UtilityCard("Electric Company");
            theBoard[13] = factory.Land("States Avenue", 2800, 2000, 2100, 200, Color.PINK, 1000, 3000, 9000, 12500, 15000);
            theBoard[14] = factory.Land("Virginia Avenue", 3200, 2200, 2400, 240, Color.PINK, 1200, 3600, 10000, 14000, 18000);
            theBoard[15] = factory.Railroad("Pensylvania Railroad");
            theBoard[16] = factory.Land("ST. James Place", 3600, 2000, 2100, 280, Color.ORANGE, 1400, 4000, 11000, 15000, 19000);

            theBoard[18] = factory.Land("Tennessee Avenue", 3600, 2000, 2100, 280, Color.ORANGE, 1400, 4000, 11000, 15000, 19000);
            theBoard[19] = factory.Land("New York Avenue", 4000, 2200, 2400, 320, Color.ORANGE, 1600, 4400, 12000, 16000, 20000);
            theBoard[20] = factory.CornerCreation("parking");
            theBoard[21] = factory.Land("Kentucky Avenue", 4400, 3000, 3400, 360, Color.RED, 1900, 5000, 14000, 17500, 21000);

            theBoard[23] = factory.Land("Indiana Avenue", 4400, 3000, 3400, 360, Color.RED, 1900, 5000, 14000, 17500, 21000);
            theBoard[24] = factory.Land("Illinois Avenue", 4800, 3300, 3900, 400, Color.RED, 2000, 6000, 15000, 18500, 22000);
            theBoard[25] = factory.Railroad("B. & O. Railroad");
            theBoard[26] = factory.Land("Atlantic Avenue", 5200, 3000, 3500, 440, Color.YELLOW, 2200, 6600, 16000, 19500, 23000);
            theBoard[27] = factory.Land("Ventnor Avenue", 5200, 3000, 3500, 440, Color.YELLOW, 2200, 6000, 16000, 19500, 23000);
            theBoard[28] = factory.UtilityCard("Water Works");
            theBoard[29] = factory.Land("Marvin Gardens", 5600, 3000, 4000, 480, Color.YELLOW, 2400, 7200, 17000, 20500, 24000);
            theBoard[30] = factory.CornerCreation("goToJail");
            theBoard[31] = factory.Land("Pacific Avenue", 6000, 4000, 5200, 520, Color.GREEN, 2600, 7800, 18000, 22000, 25500);
            theBoard[32] = factory.Land("North Carolina Avenue", 6000, 4000, 5200, 520, Color.GREEN, 2600, 7800, 18000, 22000, 25500);

            theBoard[34] = factory.Land("Pennsylvania Avenue", 6400, 5000, 6400, 560, Color.GREEN, 3000, 9000, 20000, 24000, 28000);
            theBoard[35] = factory.Railroad("Short Line Railroad");

            theBoard[37] = factory.Land("Park Place", 7000, 4000, 6000, 700, Color.BLUE, 3500, 10000, 22000, 26000, 30000);
            theBoard[38] = factory.TaxCard(2000, "Luxury Tax");
            theBoard[39] = factory.Land("Boardwalk", 8000, 4000, 7000, 1000, Color.BLUE, 4000, 12000, 28000, 34000, 40000);

            ChanceFactory chanceFactory = new();
            List<Chance> chanceCards = new();

            chanceCards.Add(chanceFactory.ChanceGoToJail("You landed on go to jail, you will now be sent to jail"));
            chanceCards.Add(chanceFactory.ChancePay("Pay school fees of $3000", 3000));
            chanceCards.Add(chanceFactory.ChanceCollect("Bank error in your favor. Collect $2000", 2000));
            chanceCards.Add(chanceFactory.ChanceOutOfJailCard("Get Out of Jail Free"));
            chanceCards.Add(chanceFactory.ChanceMove("Advance to Boardwalk", 39));

            Tile chance = factory.PullChanceCard(chanceCards);

            theBoard[2] = chance;
            theBoard[7] = chance;
            theBoard[17] = chance;
            theBoard[22] = chance;
            theBoard[33] = chance;
            theBoard[36] = chance;

            board.TheBoard = theBoard;

            return board;
        }

        public  List<Player> CreatePlayerList(Board theBoard) 
        {
            int totalPlayers = 0;
            string totalPlayersInput = "";

            do
            {
                Console.WriteLine("How many players min 2 max 6: ");
                totalPlayersInput = Console.ReadLine();
            }
            while (!int.TryParse(totalPlayersInput, out totalPlayers) || totalPlayers < 2 || totalPlayers > 6);

            List<Player> players = new();

            // creating players and adding to list
            for (int i = 0; i < totalPlayers; i++)
            {
                int playerNumber = i + 1;
                Console.WriteLine("\nEnter name of player " + playerNumber + ": ");
                string playerName = Console.ReadLine();
                Player player = new(playerName, theBoard);
                players.Add(player);
            }

            return players;
        }

        public override void RunGame(Board theBoard) 
        {
            int roundsPlayed = 1;
            Dice dice = new();

            // run game as long as there are more than two players on the board
            while (theBoard.AllPlayers.Count > 1)
            {
                Console.WriteLine("\n\nRound number: " + roundsPlayed);

                // turn to list to avoid enumeration error 
                foreach (Player player in theBoard.AllPlayers.ToList())
                {
                    // check to make sure that we still want to perform action on player
                    // if current players < 2 stop iteration, winner declared
                    if(theBoard.AllPlayers.Count < 2)
                    {
                        break;
                    }

                    Console.WriteLine("------------------------------------------");
                    Console.WriteLine("\n\n" + player.Name + "s turn");

                    // player is not in jail act normally
                    if (!player.IsInJail)
                    {
                        string input = "";
                        string input2 = "";

                        do
                        {
                            Console.WriteLine("Do you wish to buy buildings? y/n");
                            input = Console.ReadLine();
                        }
                        while (!input.Equals("y") && !input.Equals("n"));

                        if (input.Equals("y"))
                        {
                            player.BuyBuildings();
                        }

                        do
                        {
                            Console.WriteLine("\nDo you wish to un mortgage land? y/n");
                            input2 = Console.ReadLine();
                        }
                        while (!input2.Equals("y") && !input2.Equals("n"));

                        if (input2.Equals("y"))
                        {
                            player.UnMortgageProperty();
                        }

                        int[] diceRoll = dice.RollDice();
                        int spacesToMove = diceRoll[0] + diceRoll[1];

                        Console.WriteLine("\nRolling dice:" + "\nYou rolled: " + diceRoll[0] +
                            " & " + diceRoll[1] + " moving " + spacesToMove + " spaces");

                        player.MovePlayer(spacesToMove);
                    }

                    // player is in jail get jail location and perform action on jail
                    else
                    {
                        theBoard.TheBoard[10].Action(player);
                    }
                    
                }

                roundsPlayed++;
            }

            Console.WriteLine("\n\nGame over winner: " + theBoard.AllPlayers[0].Name);
        }

    }
}
