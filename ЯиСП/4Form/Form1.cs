using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Windows.Forms;
using ClassLibrary.Task4;

namespace _4Form
{
    public partial class Form1 : Form
    {
        int time = 0;
        CarStation CarStation;

        MachinePanel MachinePanel;
        FactoriesPanel FactoriesPanel;
        public Form1()
        {
            InitializeComponent();
            panel2.Controls[2].SendToBack();
            CarStation = new CarStation();
            FactoriesPanel = new FactoriesPanel(panel1, CarStation);
            MachinePanel = new MachinePanel(panel2, CarStation, FactoriesPanel.keyValuePairs);
            FactoriesPanel.AddFactories();
            FactoriesPanel.AddFactories();
            MachinePanel.AddGazel();
            MachinePanel.AddFixMachine();
            MachinePanel.AddKamaz();
            timer1.Start();
        }

        
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }
        
        private void timer1_Tick(object sender, EventArgs e)
        {
            time += timer1.Interval;
            FactoriesPanel.Refresh();
            MachinePanel.Refresh();
        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FactoriesPanel.AddFactories();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            FactoriesPanel.Remove();
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            panel2.Controls[panel2.Controls.IndexOf(panel1)].SendToBack();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            MachinePanel.AddGazel();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            MachinePanel.AddKamaz();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            MachinePanel.AddFixMachine();
        }
    }
}
