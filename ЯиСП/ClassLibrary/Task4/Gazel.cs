using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClassLibrary.Task4
{
    public class Gazel : IEquipment
    {

        public delegate void ComeBack(IEquipment equipment);
        public event ComeBack WorkIsDone;

        public Factory Target = null;
        public double Capacity { get; private set; }
        public double Mass { get; set; }
        public bool Busy { get; private set; }
        public Gazel()
        {
            Busy = false;
            Capacity = 15;
        }

        public void Movement(Factory Target)
        {
            this.Target = Target;
            Busy = true;
            Thread.Sleep(2000);
            Target.SugarSupply += Capacity;
            Busy = false;
            Target.Check = Factory.Sost.Run;
            this.Target = null;
            WorkIsDone(this);
        }
    }
}
