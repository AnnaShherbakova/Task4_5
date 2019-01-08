using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ClassLibrary.Task4;
using System.Threading;

namespace ClassLibrary.Task4
{
    public class CarStation
    {
        
        public List<IEquipment> Mashins = new List<IEquipment>();
        Queue<IEquipment> SugamMashin = new Queue<IEquipment>();
        Queue<IEquipment> fixMachines = new Queue<IEquipment>();

        ManualResetEvent evtObjS = new ManualResetEvent(true);
        ManualResetEvent evtObjF = new ManualResetEvent(true);
        

        public void AddGazel(Gazel gazel)
        {
            gazel.WorkIsDone += AddBackS;
            Mashins.Add(gazel);
            SugamMashin.Enqueue(gazel);
        }

        void AddBackS(IEquipment equipment)
        {
            SugamMashin.Enqueue(equipment);
            evtObjS.Set();
        }

        void AddBackF(IEquipment equipment)
        {
            fixMachines.Enqueue(equipment);
            evtObjF.Set();
        }

        public void AddKamaz(Kamaz kamaz)
        {
            kamaz.WorkIsDone += AddBackS;
            Mashins.Add(kamaz);
            SugamMashin.Enqueue(kamaz);
        }

        public void AddFizMach(FixMachine fixMachine)
        {
            fixMachine.WorkIsDone += AddBackF;
            Mashins.Add(fixMachine);
            fixMachines.Enqueue(fixMachine);
        }

        public void FileASugarCar(Factory Target)
        {
            IEquipment Mashin;
            lock (SugamMashin)
            {
                System.Threading.ManualResetEvent evtObj =
                    new System.Threading.ManualResetEvent(false);

                //while (SugamMashin.Count == 0) ;
                evtObjS.WaitOne();
                Mashin = SugamMashin.Dequeue();
                if (SugamMashin.Count == 0)
                {
                    evtObjS.Reset();
                }
            }
            if (Mashin is Kamaz)
            {
                (Mashin as Kamaz).Movement(Target);
            }
            else if (Mashin is Gazel)
            {
                (Mashin as Gazel).Movement(Target);
            }
        }

        public void FileARepairCar(Factory Target)
        {
            FixMachine Mashin;
            lock (fixMachines)
            {
                evtObjF.WaitOne();
                //while (fixMachines.Count == 0) ;
                Mashin = fixMachines.Dequeue() as FixMachine;
                if (fixMachines.Count == 0)
                    evtObjF.Reset();
            }
            Mashin.Movement(Target);
        }
    }
}