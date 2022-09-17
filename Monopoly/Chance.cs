using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public abstract class Chance
    {
        protected string text;

        public abstract void Action(Player player);
    }
}
