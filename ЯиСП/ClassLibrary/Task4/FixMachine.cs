using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClassLibrary.Task4
{
    public class FixMachine : IEquipment
    {
        public delegate void ComeBack(IEquipment equipment);
        public event ComeBack WorkIsDone;

        public Factory Target = null;
        public bool Busy { get; private set; }
        public FixMachine()
        {
            Busy = false;
        }

        public void Movement(Factory Target)
        {
            this.Target = Target;
            if (Target.Check == Factory.Sost.Critical)
            {
                this.Target = null;
                WorkIsDone(this);
                return;
            }
            Target.Check = Factory.Sost.Stop;
            Busy = true;
            Thread.Sleep(5000);
            Target.prochnost = 25;
            Busy = false;
            Target.Check = Factory.Sost.Run;
            this.Target = null;
            WorkIsDone(this);
        }
    }
}
