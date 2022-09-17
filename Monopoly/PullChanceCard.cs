using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class PullChanceCard : Tile
    {
        List<Chance> ChanceCards { get; set; }

        public PullChanceCard(List<Chance> cards) 
        {
            ChanceCards = cards;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine("\nYou landed on chance, random chance card is pulled");
            Random random = new Random();

            int randomIndex = random.Next(ChanceCards.Count);

            // assuming list is not empty
            ChanceCards[randomIndex].Action(player);
        }
    }
}
