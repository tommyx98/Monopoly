using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Start : Tile
    {
        public override void Action(Player player) 
        {
            Console.WriteLine("\n" + player.Name + " will receive 4000 for passing or landing on go");
            player.Money += 4000;
        }
    }
}
