using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace ClassLibrary.Task4
{
    public class Factory
    {
        public enum Sost
        {
            Run, Crush, NotSugar, Critical, Stop
        }
        public Sost Check { get; set; }
        public double SugarSupply { get; set; }
        public double MaxSugarSupply { get; private set; }

        public delegate void SugarLoading (Factory e);
        public event SugarLoading SugarIsOver;

        public delegate void Fix(Factory e);
        public event Fix Breakdown;

        const double sugarForOneSweet = 0.5;

        public int prochnost = 25;


        bool isOK()
        {
            Random random = new Random();
            return random.Next(1000) > 970 ? false : true;
        }

        public Factory()
        {
            SugarSupply = 50;
            MaxSugarSupply = 50;
        }

        bool flag = true;

        public void StartCandyProduction()
        {
            Check = Sost.Run;
            while (true)
            {
                if (Check == Sost.Critical) continue;
                Thread.Sleep(100);
                if (Check == Sost.Crush)
                {
                    if (prochnost > 0)
                    {
                        prochnost--;
                    }
                    else Check = Sost.Critical;
                }

                if (Check == Sost.Run)
                {
                   
                    if (!isOK())
                    {
                        Check = Sost.Crush;
                        
                        if (flag)
                        {
                            //ThreadPool.QueueUserWorkItem(x => Breakdown.Invoke(this));
                            Thread thread = new Thread(new ThreadStart(someAction));
                            thread.Start();
                            flag = false;
                        }
                    }
                    else if (SugarSupply - sugarForOneSweet < 0)
                    {
                        Check = Sost.NotSugar;
                        SugarIsOver.Invoke(this);
                    }
                    else
                    {
                        flag = true;
                        SugarSupply -= sugarForOneSweet;
                    }
                }
            }
                
        }
        private async void someAction()
        {
            Task.Run(() => Breakdown.Invoke(this));
        }
    }
}
