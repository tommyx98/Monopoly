using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Factory
    {
        public Tile CornerCreation(string corner) 
        {
            if (corner.Equals("start"))
            {
                return new Start();
            }

            else if (corner.Equals("parking"))
            {
                return new Parking();
            }

            else if (corner.Equals("jail"))
            {
                return new Jail();
            }

            else if (corner.Equals("goToJail"))
            {
                return new GoToJail();
            }

            else
            {
                return null;
            }
        }

        public Tile TaxCard(int tax, string name) 
        {
            return new TaxCard(tax, name);
        }

        public Tile Land(string name, int price, int houseCost, 
            int hotelCost, int rent, Color group, int oneHouseRent, int twoHouseRent,
            int threeHouseRent, int fourHouseRent, int hotelRent) 
        {
            return new Land(name, price, houseCost, hotelCost, rent, group, 
                oneHouseRent, twoHouseRent, threeHouseRent, fourHouseRent, hotelRent);
        }

        public Tile UtilityCard(string name) 
        {
            return new UtilityCard(name);
        }

        public Tile Railroad(string name) 
        {
            return new Railroad(name);
        }

        public Tile PullChanceCard(List<Chance> cards) 
        {
            return new PullChanceCard(cards);
        }

    }
}
