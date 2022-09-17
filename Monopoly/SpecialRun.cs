using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    public class SpecialRun : FacadeDecorator
    {
        public SpecialRun(FacadeMethods facadeMethods) : base(facadeMethods) { }
        private Stopwatch timer = new();

        public override void RunGame(Board theBoard) 
        {
            timer.Start();
            base.RunGame(theBoard);
            timer.Stop();
            Console.WriteLine("\n\n\n Time played in minutes: " + timer.ElapsedMilliseconds / 60000);
        }
    }
}
