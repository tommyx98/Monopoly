using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Dice
    {
        public int[] RollDice() 
        {
            Random random = new();

            int dice1 = random.Next(1, 7);
            int dice2 = random.Next(1, 7);
            int[] playerThrow = {dice1, dice2};

            return playerThrow;
        }
    }
}
