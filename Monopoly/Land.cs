using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public enum Color
    {
        BROWN,
        LBLUE,
        PINK,
        ORANGE,
        RED,
        YELLOW,
        GREEN,
        BLUE
    }
    public class Land : Property
    {
        public int Houses { get; set; }
        public int Hotels { get; set; }
        public int HouseCost { get; set; }
        public int HotelCost { get; set; }
        public int Rent { get; set; }

        private int oneHouseRent;
        private int twoHouseRent;
        private int threeHouseRent;
        private int fourHouseRent;
        private int hotelRent;

        public Color Group { get; set; }

        public Land(string name, int price, int houseCost, int hotelCost, 
            int rent, Color group, int houseRent1, int houseRent2, int houseRent3, 
            int houseRent4, int rentHotel) 
        {
            Owner = null;
            Name = name;
            Price = price;
            MortgageValue = Price / 2;
            Mortgaged = false;
            Houses = 0;
            Hotels = 0;
            HouseCost = houseCost;
            HotelCost = hotelCost;
            Rent = rent;
            Group = group;
            oneHouseRent = houseRent1;
            twoHouseRent = houseRent2;
            threeHouseRent = houseRent3;
            fourHouseRent = houseRent4;
            hotelRent = rentHotel;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine("\n" + player.Name + " landed on " + Name + " color " + Group);

            // if properties does not have owner give player option to purchase
            if (Owner == null)
            {
                player.BuyProperty(this);
            }

            else if (Mortgaged)
            {
                Console.WriteLine("\n" + player.Name + " landed on " + Name + " color " + Group);
                Console.WriteLine("Property is mortgaged no rent payd");
            }

            else
            {
                // player landed on owned properties, needs to pay owner
                if(player != Owner)
                {
                    int toBePaid = Rent;

                    // if building on land calculate price to be paid
                    if(Houses > 0)
                    {
                        // there can max be 4 houses and 1 hotel and I count hotel as a 5th house
                        if(Hotels > 0)
                        {
                            toBePaid = hotelRent;
                            Console.WriteLine("Hotel on property");
                        }

                        else
                        {
                            if(Houses == 1)
                            {
                                toBePaid = oneHouseRent;
                                Console.WriteLine("1 house on property");
                            }

                            if (Houses == 2)
                            {
                                toBePaid = twoHouseRent;
                                Console.WriteLine("2 houses on property");
                            }

                            if (Houses == 3)
                            {
                                toBePaid = threeHouseRent;
                                Console.WriteLine("3 houses on property");
                            }

                            if (Houses == 4)
                            {
                                toBePaid = fourHouseRent;
                                Console.WriteLine("4 houses on property");
                            }
                        }
                    }

                    Console.WriteLine(player.Name + " is standing on " + Owner.Name + "s property"
                        + " pay $" + toBePaid + " in rent");

                    Owner.Money += toBePaid;
                    player.Pay(toBePaid);
                }               
            }
        }
    }
}
