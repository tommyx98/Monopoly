using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class UtilityCard : Property
    {
        public UtilityCard(string name) 
        {
            Owner = null;
            Name = name;
            Price = 3000; // all utility cards cost the same
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
                    Dice dice = new();
                    int[] playerThrow = dice.RollDice();
                    int totalThrow = playerThrow[0] + playerThrow[1];

                    int toBePaid = 80 * totalThrow;
                    int ownedUtilities = 0;

                    // amount to be paid for utilities is based on amount of utilities owned
                    // so finds if owner of this utility owns multiple utilities
                    foreach (Property property in Owner.Properties)
                    {
                        if (property.GetType() == typeof(UtilityCard))
                        {
                            ownedUtilities++;
                        }
                    }

                    Console.WriteLine(player.Name + " rolls a dice to calculate rent. " +
                        "Rent is 80 or 200 times amount shown on dice depending on if " +
                        "the owner of this utility owns another");
                    Console.WriteLine(player.Name + " rolled: " + totalThrow);

                    // if owner owns > 1 utility rent = dice throw * 10
                    if(ownedUtilities > 1)
                    {
                        toBePaid = 200 * totalThrow;
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
