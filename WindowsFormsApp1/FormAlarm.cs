//using SorterControl.Management;
using Adam.UI_Update.Alarm;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam
{
    public partial class FromAlarm : Form
    {
        public FromAlarm()
        {
            InitializeComponent();
        }

        private void ResetAll_bt_Click(object sender, EventArgs e)
        {
            Transaction Txn;

            foreach(AlarmInfo eachA in AlarmManagement.GetAll())
            {
                if (!eachA.Type.Equals("Device alert error"))
                {
                    AlarmManagement.Remove(eachA);
                }
            }

            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());

            var NodeList = AlarmManagement.GetAll().GroupBy(t => t.NodeName);
            foreach (var group in NodeList)
            {
                Txn = new Transaction();
                Txn.Method = Transaction.Command.RobotType.Reset;
                NodeManagement.Get(group.First().NodeName).SendCommand(Txn);
            }
        }

        private void AlarmFrom_Load(object sender, EventArgs e)
        {
            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
        }

        private void AlarmFrom_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
            this.Visible = false;
        }
    }
}
