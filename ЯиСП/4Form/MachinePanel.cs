using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using ClassLibrary.Task4;
using System.Windows.Forms;

namespace _4Form
{
    public class MachinePanel
    {
        Image gazel = Image.FromFile("C:\\Users\\Asus\\Desktop\\ЯиСП\\4Form\\bin\\Debug\\gazel.png");
        Image kamaz = Image.FromFile("C:\\Users\\Asus\\Desktop\\ЯиСП\\4Form\\bin\\Debug\\kamaz.png");
        Image repair = Image.FromFile("C:\\Users\\Asus\\Desktop\\ЯиСП\\4Form\\bin\\Debug\\repair.png");
        CarStation CarStation { get; set; }
        Panel panel;
        Dictionary<IEquipment, PictureBox> keyValuePairs = new Dictionary<IEquipment, PictureBox>();
        Dictionary<Factory, PictureBox> keyValuePairsFactory;
        public MachinePanel(Panel panel, CarStation CarStation, Dictionary<Factory, PictureBox> keyValuePairsFactory)
        {
            this.panel = panel;
            this.CarStation = CarStation;
            this.keyValuePairsFactory = keyValuePairsFactory;
        }

        public void AddGazel()
        {
            Gazel _gazel = new Gazel();
            CarStation.AddGazel(_gazel);
            PictureBox pictureBox = new PictureBox();
            pictureBox.BringToFront();
            pictureBox.Top = 10 + (CarStation.Mashins.Count - 1) * 70;
            pictureBox.Left = 700;
            pictureBox.Size = new Size(70, 70);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = gazel;
            keyValuePairs.Add(_gazel, pictureBox);
            panel.Controls.Add(pictureBox);
        }

        public void AddKamaz()
        {
            Kamaz _kamaz = new Kamaz();
            CarStation.AddKamaz(_kamaz);
            PictureBox pictureBox = new PictureBox();
            pictureBox.BringToFront();
            pictureBox.Top = 10 + (CarStation.Mashins.Count - 1) * 70;
            pictureBox.Left = 700;
            pictureBox.Size = new Size(70, 70);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = kamaz;
            keyValuePairs.Add(_kamaz, pictureBox);
            panel.Controls.Add(pictureBox);
        }

        public void AddFixMachine()
        {
            FixMachine _fix = new FixMachine();
            CarStation.AddFizMach(_fix);
            PictureBox pictureBox = new PictureBox();
            pictureBox.BringToFront();
            pictureBox.Top = 10 + (CarStation.Mashins.Count - 1) * 70;
            pictureBox.Left = 700;
            pictureBox.Size = new Size(70, 70);
            pictureBox.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox.Image = repair;
            keyValuePairs.Add(_fix, pictureBox);
            panel.Controls.Add(pictureBox);
        }

        public void Refresh()
        {
            for (int i = 0; i < CarStation.Mashins.Count; i++)
            {
                if (CarStation.Mashins[i] is Gazel)
                {
                    Gazel gazel = CarStation.Mashins[i] as Gazel;
                    PictureBox basepictureBox = keyValuePairs[gazel];
                    if (gazel.Busy == true && gazel.Target != null)
                    {
                        PictureBox secongpictureBox = keyValuePairsFactory[gazel.Target];
                        basepictureBox.Top = secongpictureBox.Top;
                        basepictureBox.Left = secongpictureBox.Left + 200;
                    }
                    else
                    {
                        basepictureBox.Top = 10 + i * 70;
                        basepictureBox.Left = 700;
                    }
                }
                else if (CarStation.Mashins[i] is Kamaz)
                {
                    Kamaz kamaz = CarStation.Mashins[i] as Kamaz;
                    PictureBox basepictureBox = keyValuePairs[kamaz];
                    if (kamaz.Busy == true && kamaz.Target != null)
                    {
                        PictureBox secongpictureBox = keyValuePairsFactory[kamaz.Target];
                        basepictureBox.Top = secongpictureBox.Top;
                        basepictureBox.Left = secongpictureBox.Left + 200;
                    }
                    else
                    {
                        basepictureBox.Top = 10 + i * 70;
                        basepictureBox.Left = 700;
                    }
                }
                else
                {
                    FixMachine fixMachine = CarStation.Mashins[i] as FixMachine;
                    PictureBox basepictureBox = keyValuePairs[fixMachine];
                    if (fixMachine.Busy == true && fixMachine.Target != null)
                    {
                        PictureBox secongpictureBox = keyValuePairsFactory[fixMachine.Target];
                        basepictureBox.Top = secongpictureBox.Top;
                        basepictureBox.Left = secongpictureBox.Left + 200;
                    }
                    else
                    {
                        basepictureBox.Top = 10 + i * 70;
                        basepictureBox.Left = 700;
                    }
                }
            }
        }
    }
}
