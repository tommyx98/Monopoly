using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Parking : Tile
    {
        public override void Action(Player player) 
        {
            Console.WriteLine("\n" + player.Name + " landed on parking and is parked safely");
        }
    }
}
