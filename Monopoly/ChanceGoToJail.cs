using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class ChanceGoToJail : Chance
    {
        public ChanceGoToJail(string info) 
        {
            text = info;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine(text);
            player.MoveToJail();
        }
    }
}
