using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Player
    {
        public int Money { get; set; }
        public List<Property> Properties { get; set;}
        public int CurPos { get; set; }
        public int OutOfJailCard { get; set; }
        public string Name { get; set; }
        public Board Board { get; set; }
        public bool IsInJail { get; set; }

        public Player(string name, Board board) 
        {
            Money = 30000;
            Properties = new();
            CurPos = 0;
            OutOfJailCard = 0;
            Name = name;
            Board = board;
            IsInJail = false;
        }

        public void Pay(int toPay) 
        {
            if(Money >= toPay)
            {
                Money -= toPay;
                Console.WriteLine(Name + " paid: " + "$" + toPay + "\n Current funds: " + "$" + Money);
                return;
            }

            Console.WriteLine("Not enough funds. Need: " + "$" + toPay + " Have: " + "$" + Money);
            Console.WriteLine("Accuire funds through motgaging");

            Mortgage(toPay);
        }

        private void Mortgage(int toPay) 
        {
            int potentialMoney = 0;
            List<Property> possibleToMortgage = new();

            // checks if it is possible for player to accuire funds
            foreach(Property property in Properties)
            {
                if (!property.Mortgaged)
                {
                    possibleToMortgage.Add(property);

                    if (property.GetType() == typeof(Land))
                    {
                        Land land = (Land)property;

                        // checks if land has houses/hotels and potential value from them + mortgage value of land
                        potentialMoney += land.HouseCost / 2 * land.Houses + 
                            land.HotelCost / 2 * land.Hotels + land.MortgageValue;
                    }

                    else
                    {
                        potentialMoney += property.MortgageValue;
                    }
                }
            }

            // impossible for player to accuire funds through mortgaging
            if (potentialMoney < toPay)
            {
                Console.WriteLine("Impossible to accuire funds through mortgaging");
                Bankrupt();
            }

            // player accuire funds through mortgaging
            else
            {
                while (Money < toPay)
                {
                    Console.WriteLine("\nPossible properties to mortgage" +
                        " displayed as name/houses/hotels: ");

                    foreach(Property property in possibleToMortgage)
                    {
                        if (property.GetType() == typeof(Land))
                        {
                            Land land = (Land)property;

                            Console.WriteLine(property.Name, land.Houses, land.Hotels);
                        }

                        // property of type utilitycard and railroad, can not have houses
                        else
                        {
                            Console.WriteLine(property.Name, 0, 0);
                        }
                    }

                    Console.WriteLine("\nEnter name of property to mortgage, note if property" +
                        " has buildings these must be sold first: ");

                    string input = Console.ReadLine();

                    // mortgaging given property
                    foreach(Property property in possibleToMortgage.ToList())
                    {
                        if (property.Name.Equals(input))
                        {
                            if (property.GetType() == typeof(Land))
                            {
                                Land land = (Land)property;

                                Console.WriteLine("\nHouses on property: " + land.Houses + 
                                    "\n Hotel on land: " + land.Hotels);
                                Console.WriteLine("House mortgage value: " + land.HouseCost/2
                                    + "\nHotel mortgage value: " + land.HotelCost/2
                                    +"\n Property mortgage value: " + land.MortgageValue);

                                // must sell houses before mortgaging land
                                if(land.Houses > 0)
                                {
                                    string numberInput = "";
                                    int givenNumber = 0;

                                    while(givenNumber <= 0)
                                    {
                                        do
                                        {
                                            Console.WriteLine("\nEnter buildings to sell, if property" +
                                            " has hotel this will be sold before houses, " +
                                            "if given number > houses + hotels, property" +
                                            " will be mortgaged aswell");
                                            numberInput = Console.ReadLine();
                                        }
                                        while (!int.TryParse(numberInput, out givenNumber));
                                    }

                                    // hotels are sold before houses
                                    if(land.Hotels > 0)
                                    {
                                        Money += land.HotelCost / 2;
                                        land.Hotels = 0;
                                        givenNumber--;
                                        Console.WriteLine("\nMortgaged hotel. Added $",
                                            land.HotelCost / 2, " to funds");
                                    }

                                    if(givenNumber > 0)
                                    {
                                        // sell all houses + land
                                        if(givenNumber > land.Houses)
                                        {
                                            int moneyToBeAdded = (land.HouseCost / 2) * land.Houses 
                                                + property.MortgageValue;
                                            Money += moneyToBeAdded;
                                            land.Houses = 0;
                                            property.Mortgaged = true;
                                            possibleToMortgage.Remove(property);
                                            Console.WriteLine("\nMortgaged all houses and property. Added $" +
                                            moneyToBeAdded + " to funds");
                                        }

                                        // sell specified amount of houses
                                        else
                                        {
                                            int moneyToBeAdded = (land.HouseCost / 2) * givenNumber;
                                            Money += moneyToBeAdded;
                                            land.Houses -= givenNumber;
                                            Console.WriteLine("\nMortgaged " + givenNumber + " house/s. Added $" +
                                            moneyToBeAdded + " to funds");
                                        }                                          
                                    }                                   
                                }

                                else
                                {
                                    property.Mortgaged = true;
                                    Money += property.MortgageValue;
                                    Console.WriteLine(property.Name + " mortgaged. Added $" +
                                        property.MortgageValue + " to funds");
                                    possibleToMortgage.Remove(property);
                                }
                                
                            }

                            else
                            {
                                property.Mortgaged = true;
                                Money += property.MortgageValue;
                                Console.WriteLine(property.Name + " mortgaged. Added $" + 
                                    property.MortgageValue + " to funds");
                                possibleToMortgage.Remove(property);
                            }
                        }
                    }
                }
            }
        }

        public void UnMortgageProperty() 
        {
            List<Land> possibleToUnMortgage = new();

            // find all property that is mortgaged
            foreach(Property property in Properties)
            {
                if(property.GetType() == typeof(Land))
                {
                    Land land = (Land)property;

                    if (land.Mortgaged)
                    {
                        possibleToUnMortgage.Add(land);
                    }
                }
            }

            if(possibleToUnMortgage.Count < 1)
            {
                Console.WriteLine("\nNo properties to un mortgage");
                return;
            }

            bool run = true;

            while (run)
            {
                Console.WriteLine("\nAll possible properties to un mortgage: ");

                foreach (Land possibleUnMortgage in possibleToUnMortgage)
                {
                    Console.WriteLine(possibleUnMortgage.Name);
                }

                Console.WriteLine("\nEnter name of property to un mortgage: ");

                string input = Console.ReadLine();

                foreach(Land possibleUnMortgage in possibleToUnMortgage.ToList())
                {
                    if (possibleUnMortgage.Name.Equals(input))
                    {
                        int unMortgageCost = Convert.ToInt32(possibleUnMortgage.MortgageValue * 1.1);
                        Console.WriteLine("Cost to un mortgage " + possibleUnMortgage.Name + ": " 
                            + unMortgageCost + "\n Funds available: " + Money);

                        string answer = "";

                        do
                        {
                            Console.WriteLine("Un mortgage? y/n: ");
                            answer = Console.ReadLine();
                        }
                        while (!answer.Equals("y") && !answer.Equals("n"));

                        if (answer.Equals("y"))
                        {
                            if(Money >= unMortgageCost)
                            {
                                Console.WriteLine(possibleUnMortgage.Name + " is no longer mortgaged");
                                Money -= unMortgageCost;
                                possibleUnMortgage.Mortgaged = false;
                                possibleToUnMortgage.Remove(possibleUnMortgage);
                            }

                            else
                            {
                                Console.WriteLine("Not enough funds to un mortgage " + possibleUnMortgage.Name);
                            }
                        }
                    }
                }

                if(possibleToUnMortgage.Count < 1)
                {
                    Console.WriteLine("\nNo properties to un mortgage");
                    run = true;
                    break;
                }

                Console.WriteLine("\nContinue? y/n: ");
                string input2 = Console.ReadLine();

                while (!input2.Equals("y") && !input2.Equals("n"))
                {
                    Console.WriteLine("Continue? y/n: ");
                    input2 = Console.ReadLine();
                }

                if (input2.Equals("n"))
                {
                    run = false;
                }
            }
        }

        private void Bankrupt() 
        {
            Console.WriteLine("\n" + Name + " could not pay and has therefore declared bankruptcy");

            Board.AllPlayers.Remove(this);
        }

        public void BuyProperty(Property property) 
        {
            // ask player to buy property
            string input = "";
            do
            {
                Console.WriteLine("Property price: " + "$" + property.Price + "\n Available funds: " + "$" + Money);
                Console.WriteLine("Do you wish to buy property y/n: ");
                input = Console.ReadLine();
            }
            while (!input.Equals("y") && !input.Equals("n"));

            // if player does not wish to buy property, property remains unowned

            // player tries to buy property
            if (input.Equals("y"))
            {
                if (Money < property.Price)
                {
                    Console.WriteLine("Not enough funds to buy " + property.Name + ". " +
                        "Funds needed: " + "$" + property.Price + " Funds have: " + "$" + Money);
                }

                else
                {
                    property.Owner = this;
                    Money -= property.Price;
                    Properties.Add(property);
                    Console.WriteLine("You bought: " + property.Name);
                }
            }            
        }

        // assuming valid argument is given
        public void MovePlayer(int postionToMove) 
        {
            // if next move is less than last position on board move normally forward
            if(CurPos + postionToMove < Board.TheBoard.Length)
            {
                CurPos += postionToMove;
                Board.TheBoard[CurPos].Action(this);
            }

            else
            {
                // makes sure player does not try to go outside board
                int calculatedPosition = (CurPos + postionToMove) - Board.TheBoard.Length;
                CurPos = calculatedPosition;

                // passed start, monopoly rules state money income when passing start
                if(calculatedPosition > 0)
                {
                    Console.WriteLine("\nYou passed start collect $4000");
                    Money += 4000;
                    Board.TheBoard[CurPos].Action(this);
                }

                // lands on start
                else
                {
                    Board.TheBoard[CurPos].Action(this);
                }
            }
        }

        public void MoveToJail() 
        {
            // the jail is located at position 10 on the board
            CurPos = 10;
            IsInJail = true;

            Tile jailLocation = Board.TheBoard[CurPos];
            Jail jail = (Jail)jailLocation;

            jail.InJail.Add(this, 3);
        }

        public void BuyBuildings() 
        {
            List<Land> possibleToBuild = new();

            List<Land> browns = new();
            List<Land> lBlue = new();
            List<Land> pink = new();
            List<Land> orange = new();
            List<Land> red = new();
            List<Land> yellow = new();
            List<Land> green = new();
            List<Land> blue = new();

            // needs predefined number of land group to build on them, this find all owned land and its group and puts in defined list
            foreach (Property property in Properties)
            {
                if(property.GetType() == typeof(Land))
                {
                    Land land = (Land)property;

                    // checks if land is buildable (not mortaged) here to avoid another loop further down

                    if(land.Group == Color.BROWN && !land.Mortgaged)
                    {
                        browns.Add(land);
                    }

                    if(land.Group == Color.LBLUE && !land.Mortgaged)
                    {
                        lBlue.Add(land);
                    }

                    if(land.Group == Color.PINK && !land.Mortgaged)
                    {
                        pink.Add(land);
                    }

                    if(land.Group == Color.ORANGE && !land.Mortgaged)
                    {
                        orange.Add(land);
                    }

                    if(land.Group == Color.RED && !land.Mortgaged)
                    {
                        red.Add(land);
                    }

                    if(land.Group == Color.YELLOW && !land.Mortgaged)
                    {
                        yellow.Add(land);
                    }

                    if(land.Group == Color.GREEN && !land.Mortgaged)
                    {
                        green.Add(land);
                    }

                    if(land.Group == Color.BLUE && !land.Mortgaged)
                    {
                        blue.Add(land);
                    }
                }
            }

            List<List<Land>> tempList = new();
            tempList.Add(lBlue);
            tempList.Add(pink);
            tempList.Add(orange);
            tempList.Add(red);
            tempList.Add(yellow);
            tempList.Add(green);

            // only 2 brown card therefore need 2 to build
            if (browns.Count == 2)
            {
                possibleToBuild.AddRange(browns);
            }

            // only 2 blue card therefore need 2 to build
            if (blue.Count == 2)
            {
                possibleToBuild.AddRange(blue);
            }

            // rest needs 3 to build
            foreach(List<Land> list in tempList)
            {
                if(list.Count == 3)
                {
                    possibleToBuild.AddRange(list);
                }
            }

            // list needs to contain atleast 2 elements to build as minimum rquirement of land of one group is 2
            if(possibleToBuild.Count < 2)
            {
                Console.WriteLine("\nNot possible to build");
                return;
            }

            bool run = true;

            while (run)
            {
                Console.WriteLine("\nAll possible land to build on: ");

                foreach(Land possibleBuild in possibleToBuild)
                {
                    // max 4 houses and 1 hotel on land
                    if(possibleBuild.Houses < 4 && possibleBuild.Hotels < 1)
                    {
                        Console.WriteLine(possibleBuild.Name);
                    }
                }

                Console.WriteLine("\nEnter name of property to build on: ");

                string input = Console.ReadLine();

                foreach (Land property in possibleToBuild)
                {
                    if (property.Name.Equals(input))
                    {
                        int purchase = 0;
                        string purchaseInput = "";

                        do
                        {
                            Console.WriteLine("Cost per house: " + "$" + property.HouseCost + 
                                "\n Cost of hotel: " + "$" + property.HotelCost +
                                "\n Available funds: " + "$" + Money);
                            Console.WriteLine("How many houses/hotels? Will be purchased in order" +
                            " houses-hotels, max 4 houses, 1 hotels: ");
                            purchaseInput = Console.ReadLine();
                        }
                        while (!int.TryParse(purchaseInput, out purchase));

                        if(Money < property.HouseCost * purchase)
                        {
                            Console.WriteLine("\nNot enough funds to build " + purchase + " houses/hotel");
                        }

                        // checking that we can build house on property
                        if(property.Houses < 4 && purchase > 0 && Money >= property.HouseCost * purchase)
                        {
                            // buy houses until reached number set or houses >= 4, max 4 houses on a property
                            while(purchase > 0 && property.Houses < 4 && Money >= property.HouseCost)
                            {
                                property.Houses += 1;
                                purchase--;
                                Money -= property.HouseCost;
                            }

                            Console.WriteLine("\nBought house/s on " + property.Name +
                                " total house/s on property: " + property.Houses);
                        }

                        // checking that we can build hotel on property, need 4 houses first
                        if (property.Houses == 4 && purchase > 0 && property.Hotels < 1 && Money >= property.HotelCost)
                        {
                            // max 1 hotel
                            property.Hotels = 1;

                            Money -= property.HotelCost;

                            Console.WriteLine("\nBought hotel on " + property.Name);
                        }

                        if(property.Houses == 4 && property.Hotels == 1)
                        {
                            Console.WriteLine("\nNot possible to build anymore on " + property.Name);
                        }
                    }
                }

                Console.WriteLine("\nContinue? y/n: ");
                string input2 = Console.ReadLine();

                while (!input2.Equals("y") && !input2.Equals("n"))
                {
                    Console.WriteLine("Continue? y/n: ");
                    input2 = Console.ReadLine();
                }

                if (input2.Equals("n"))
                {
                    run = false;
                }
            }
        }


    }
}
