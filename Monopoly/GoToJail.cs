using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class GoToJail : Tile
    {
        public override void Action(Player player) 
        {
            Console.WriteLine("\n" + player.Name + " will be sent to jail!");

            player.MoveToJail();         
        }
    }
}
