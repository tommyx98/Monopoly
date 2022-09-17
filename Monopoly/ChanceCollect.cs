using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class ChanceCollect : Chance
    {
        private int collectAmount;

        public ChanceCollect(string info, int collect) 
        {
            text = info;
            collectAmount = collect;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine(text);
            player.Money += collectAmount;
        }
    }
}
