﻿//using SorterControl.Management;
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

        private void AlarmFrom_Load(object sender, EventArgs e)
        {
            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
        }
    }
}
