using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Railroad : Property
    {
        private int rent = 500; 
        public Railroad(string name) 
        {
            Owner = null;
            Name = name;
            Price = 4000; // all railroads cost 4000
            MortgageValue = Price / 2;
            Mortgaged = false;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine("\n" + player.Name + " landed on " + Name);

            // if properties does not have owner give player option to purchase
            if (Owner == null)
            {
                player.BuyProperty(this);
            }

            else
            {
                // player landed on owned properties, needs to pay owner
                if (player != Owner)
                {
                    int toBePaid = rent;
                    int ownedRailroads = 0;

                    // amount to be paid for railroads is based on amount of railroads owned
                    // so finds if owner of this railroad owns multiple railroads
                    foreach(Property property in Owner.Properties)
                    {
                        if(property.GetType() == typeof(Railroad))
                        {
                            ownedRailroads++;
                        }
                    }

                    // checks how many of 4 total railroads owner owns and adjust amount to be paid thereafter
                    if(ownedRailroads > 1)
                    {
                        if(ownedRailroads == 2)
                        {
                            toBePaid = 1000;
                        }

                        else if(ownedRailroads == 3)
                        {
                            toBePaid = 2000;
                        }

                        else
                        {
                            toBePaid = 4000;
                        }
                    }

                    Console.WriteLine("Rent is calculated based on how many railroads the owner owns" +
                        "\n 1 = $500, 2 = $1000, 3 = $2000, 4 = $4000");
                    Console.WriteLine(player.Name + " is standing on " + Owner.Name + "s property"
                        + " pay $" + toBePaid + " in rent");

                    Owner.Money += toBePaid;
                    player.Pay(toBePaid);
                }
            }
        }
    }
}
