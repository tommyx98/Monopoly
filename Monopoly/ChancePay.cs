using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class ChancePay : Chance
    {
        private int payAmount;

        public ChancePay(string info, int amount) 
        {
            text = info;
            payAmount = amount;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine(text);
            player.Pay(payAmount);
        }
    }
}
