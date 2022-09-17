using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class Board
    {
        public Tile[] TheBoard { get; set; }
        public List<Player> AllPlayers { get; set; }

        // Monopoly has 40 tiles on the board
        public Board() 
        {
            TheBoard = new Tile[40];
        }
    }
}
