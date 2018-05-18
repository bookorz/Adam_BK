using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UI_TEST
{
    public partial class RunningScreen : Form
    {
        public RunningScreen()
        {
            InitializeComponent();
            List<Wafer> List = new List<Wafer>();
            
            for(int i = 25; i >= 1; i--)
            {
                Wafer w = new Wafer();
                w.Slot = i;
                w.Flag = true;
                w.ID = "       ???        ";
                List.Add(w);
            }

            dataGridView1.DataSource = List;
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView2.DataSource = List;
            dataGridView2.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            dataGridView3.DataSource = List;
            dataGridView3.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
        }

        public class Wafer
        {
            public bool Flag { get; set; }
            public int Slot { get; set; }
            public string ID { get; set; }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
