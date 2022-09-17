using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class ChanceMove : Chance
    {
        private int positionToMove;

        public ChanceMove(string info, int position) 
        {
            text = info;
            positionToMove = position;
        }
        public override void Action(Player player) 
        {
            Console.WriteLine(text);
            player.MovePlayer(positionToMove);
        }
    }
}
