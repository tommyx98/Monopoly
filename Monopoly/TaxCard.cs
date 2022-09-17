using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class TaxCard : Tile
    {
        private int Tax { get; set; }
        public string Name { get; set; }

        public TaxCard(int tax, string name) 
        {
            Tax = tax;
            Name = name;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine("\n" + player.Name + " landed on " + Name);
            Console.WriteLine("Pay " + Tax + " in tax");

            player.Pay(Tax);
        }
    }
}
