using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using ClassLibrary.Task4;

namespace _4Form
{
    public class FactoriesPanel
    {
        int size = 100;
        public Panel MainPanel { get; set; }
        public List<System.Threading.Thread> threads { get; private set; }
        public List<Factory> Factories { get; private set; }
        Image fctN = Image.FromFile("C:\\Users\\Asus\\Desktop\\ЯиСП\\4Form\\bin\\Debug\\plant.jpg");
        Image fctC = Image.FromFile("C:\\Users\\Asus\\Desktop\\ЯиСП\\4Form\\bin\\Debug\\plant1.jpg");
        Image fctCK = Image.FromFile("C:\\Users\\Asus\\Desktop\\ЯиСП\\4Form\\bin\\Debug\\plant_s.png");
        CarStation CarStation;
        public Dictionary<Factory, PictureBox> keyValuePairs = new Dictionary<Factory, PictureBox>();
        public FactoriesPanel(Panel panel, CarStation carStation)
        {
            CarStation = carStation;
            MainPanel = panel;
            Factories = new List<Factory>();
            threads = new List<System.Threading.Thread>();
        }

        public void Refresh()
        {
            for (int i = 0; i < Factories.Count; i++)
            {
                PictureBox pictureBox = MainPanel.Controls[2 * i] as PictureBox;
                TextBox textBox = MainPanel.Controls[2 * i + 1] as TextBox;
                if (Factories[i].Check == Factory.Sost.Crush || Factories[i].Check == Factory.Sost.Stop)
                {
                    pictureBox.Image = fctC;
                }
                if (Factories[i].Check == Factory.Sost.Run)
                {
                    pictureBox.Image = fctN;
                }
                if (Factories[i].Check == Factory.Sost.Critical)
                {
                    pictureBox.Image = fctCK;
                }
                if (Factories[i].Check == Factory.Sost.NotSugar)
                {
                    textBox.Text = "No sugar!!!";
                }
                else
                {
                    textBox.Text = Factories[i].SugarSupply.ToString();
                }
            }
        }

        public void AddFactories()
        {
            Factory factory = new Factory();
            Factories.Add(factory);
            factory.Breakdown += CarStation.FileARepairCar;
            factory.SugarIsOver += CarStation.FileASugarCar;
            PictureBox pictureBox = new PictureBox();
            MainPanel.Controls.Add(pictureBox);
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as PictureBox).Size = new Size(size, size);
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as PictureBox).Left = 0;
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as PictureBox).Top =
                size * (Factories.Count - 1) + size / 20;
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as PictureBox).SizeMode = PictureBoxSizeMode.Zoom;
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as PictureBox).Image = fctN;
            keyValuePairs.Add(factory, pictureBox);
            MainPanel.Controls.Add(new TextBox());
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as TextBox).Left = size + size / 20;
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as TextBox).Top = size * (Factories.Count - 1) + size / 2;
            (MainPanel.Controls[MainPanel.Controls.Count - 1] as TextBox).ReadOnly = true;
            threads.Add(new System.Threading.Thread(new System.Threading.ThreadStart(factory.StartCandyProduction)));
            threads[threads.Count - 1].Start();
        }

        public void Remove()
        {
            threads[threads.Count - 1].Abort();
            threads.RemoveAt(threads.Count - 1);
            Factories.RemoveAt(Factories.Count - 1);
            MainPanel.Controls.RemoveAt(MainPanel.Controls.Count - 1);
            MainPanel.Controls.RemoveAt(MainPanel.Controls.Count - 1);
        }
    }
}
