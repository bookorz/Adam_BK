//using SorterControl.Management;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SorterControl
{
    public partial class AlarmFrom : Form
    {
        public AlarmFrom()
        {
            InitializeComponent();
        }

        private void ResetAll_bt_Click(object sender, EventArgs e)
        {

            //var NodeList = ((List<AlarmInfo>)AlarmList_gv.DataSource).ToList().GroupBy(t => t.NodeName);
            //foreach(var group in NodeList)
            //{
            //    NodeManagement.Get(group.First().NodeName).SendCommand(new Transaction(new List<Job>(), "", "", Transaction.Command.RobotType.Reset, "", 300000));
            //}
        }

        private void AlarmFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
