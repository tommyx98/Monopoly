using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class ChanceFactory
    {
        public Chance ChanceGoToJail(string text) {
            return new ChanceGoToJail(text);
        }

        public Chance ChanceOutOfJailCard(string text) {
            return new ChanceOutOfJailCard(text);
        }

        public Chance ChanceMove(string text, int position) {
            return new ChanceMove(text, position);
        }

        public Chance ChanceCollect(string text, int amount) {
            return new ChanceCollect(text, amount);
        }

        public Chance ChancePay(string text, int amount) {
            return new ChancePay(text, amount);
        }
    }
}
