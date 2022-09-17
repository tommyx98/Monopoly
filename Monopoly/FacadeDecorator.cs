using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public abstract class FacadeDecorator : FacadeMethods
    {
        private readonly FacadeMethods _original;

        public FacadeDecorator(FacadeMethods original) 
        {
            _original = original;
        }

        public override void RunGame(Board theBoard) 
        {
            _original.RunGame(theBoard);
        }
    }
}
