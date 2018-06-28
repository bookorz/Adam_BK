using Adam;
using Adam.UI_Update.Running;
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

namespace UI_TEST
{
    public partial class FormRunning : Form
    {
        public FormRunning()
        {
            InitializeComponent();


        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ExStyle |= 0x02000000;
                return cp;
            }
        }

        private void RunningScreen_Load(object sender, EventArgs e)
        {
            ChangeSpeed();
            foreach (Node port in NodeManagement.GetLoadPortList())
            {
                port.Available = false;
                port.Fetchable = false;
                port.ReserveList.Clear();
                foreach (Job j in port.JobList.Values.ToList())
                {
                    j.Destination = "";
                    j.DestinationSlot = "";
                    j.DisplayDestination = "";
                }
                RunningUpdate.UpdateNodesJob(port.Name);
            }
            FormMain.RouteCtrl.Start("FormRunning");

        }

        private void LoadPort_Dest_Click(object sender, EventArgs e)
        {
            string PortName = (sender as Button).Name.Replace("_Dest", "");
            Node currPort = NodeManagement.Get(PortName);
            if (currPort.IsMapping)
            {
                string Dest = (sender as Button).Text;

                ContextMenu m = new ContextMenu();
                MenuItem mi = new MenuItem("Reset", SelectDest);
                mi.Tag = PortName;
                m.MenuItems.Add(mi);
                foreach (Node port in NodeManagement.GetLoadPortList())
                {

                    if (port.ReserveList.Count == 0 && port.IsMapping)
                    {
                        mi = new MenuItem(port.Name, SelectDest);
                        mi.Tag = PortName;
                        m.MenuItems.Add(mi);
                    }

                }
                m.Show((Button)sender, new Point(0, 0));
            }
            else
            {
                MessageBox.Show("Foup不存在哦");
            }
        }

        private void SelectDest(object sender, EventArgs e)
        {
            try
            {
                string PortName = (sender as MenuItem).Tag.ToString();
                Button Org_Dest = this.Controls.Find(PortName + "_Dest", true).FirstOrDefault() as Button;
                string Dest = (sender as MenuItem).Text;

                Node Org_Dest_Port = NodeManagement.Get(Org_Dest.Text);
                if (Org_Dest_Port != null)
                {
                    Org_Dest_Port.ReserveList.Clear();
                    JobManagement.ClearAssignJobByPort(Org_Dest_Port.Name);
                }

                Node Port = NodeManagement.Get(PortName);

                if (Dest.Equals("Reset"))
                {
                    Org_Dest.Text = "Assign";
                    Port.DestPort = "";
                    Port.Mode = "UD";
                }
                else
                {
                    Node DestPort = NodeManagement.Get(Dest);

                    if ((from jb in DestPort.JobList.Values
                         where jb.MapFlag
                         select jb).Count() != 0)
                    {
                        MessageBox.Show("請選擇一個空的Foup");
                        return;
                    }


                    DestPort.ReserveList.Clear();
                    JobManagement.ClearAssignJobByPort(DestPort.Name);

                    foreach (Job job in Port.JobList.Values.ToList())
                    {
                        job.Destination = DestPort.Name;
                        job.DestinationSlot = job.Slot;
                        job.DisplayDestination = DestPort.Name.Replace("Load", "");
                        job.ProcessFlag = false;
                        DestPort.ReserveList.TryAdd(job.Slot, job);

                    }
                    Org_Dest.Text = Dest;
                    Port.DestPort = Dest;
                    if (PortName.Equals(Dest))
                    {
                        Port.Mode = "LU";
                    }
                    else
                    {
                        Port.Mode = "LD";
                        DestPort.Mode = "UD";
                    }
                    if (Start_btn.Tag != null)
                    {
                        if (Start_btn.Tag.Equals("Start"))
                        {
                            Port.Available = true;
                        }
                    }
                }
            }
            catch (Exception e1)
            {
                MessageBox.Show(e1.StackTrace);
            }
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            if (Start_btn.Tag == null)
            {
                Start_btn.Tag = "Stop";
            }
            if (Start_btn.Tag.Equals("Start"))
            {
                FormMain.RouteCtrl.Stop();
                //FormMain.RouteCtrl.Stop();
                foreach (Node port in NodeManagement.GetLoadPortList())
                {

                    port.Available = false;
                }

                Start_btn.BackColor = Color.DimGray;
                Start_btn.Text = "Start Running";
                Start_btn.Tag = "Stop";
                FormMain.RouteCtrl.Start("FormRunning");
            }
            else
            {
                var findByPass = from node in NodeManagement.GetList()
                                 where node.ByPass
                                 select node;

                if (findByPass.Count() != 0)
                {
                    string msg = "";
                    foreach (Node node in findByPass)
                    {
                        msg += node.Name + " ";
                    }
                    msg += "\n為By pass 模式，確定要繼續?";

                    if (MessageBox.Show(msg, "警告", MessageBoxButtons.OKCancel) == DialogResult.Cancel)
                    {
                        return;
                    }
                }
                var findAvailablePort = from port in NodeManagement.GetLoadPortList()
                                        where port.ByPass || port.IsMapping
                                        select port;
                if (findAvailablePort.Count() != 0)
                {
                    foreach (Node port in findAvailablePort)
                    {
                        if (port.IsMapping)
                        {
                            port.Available = true;
                        }
                    }
                }
                //FormMain.RouteCtrl.Start("Normal");
                Start_btn.BackColor = Color.Red;
                Start_btn.Text = "Stop";
                Start_btn.Tag = "Start";

            }

        }

        private void RunningScreen_FormClosing(object sender, FormClosingEventArgs e)
        {
            FormMain.RouteCtrl.Stop();
        }

        private void LoadPort_lb_MouseClick(object sender, MouseEventArgs e)
        {

            if (e.Button == MouseButtons.Right)
            {
                string PortName = ((Label)sender).Name.Replace("_lb", "");
                ContextMenu m = new ContextMenu();
                MenuItem tmp = new MenuItem("Fake Data(Full)", FakeData);
                tmp.Tag = PortName;
                m.MenuItems.Add(tmp);
                tmp = new MenuItem("Fake Data(Empty)", FakeData);
                tmp.Tag = PortName;
                m.MenuItems.Add(tmp);
                m.Show((Label)sender, new Point(e.X, e.Y));
            }
        }

        private void FakeData(object sender, EventArgs e)
        {

            switch (((MenuItem)sender).Text)
            {

                case "Fake Data(Full)":
                    RunningUpdate.UpdateLoadPortMapping(((MenuItem)sender).Tag.ToString(), "1111000000000000000000000");
                    break;
                case "Fake Data(Empty)":
                    RunningUpdate.UpdateLoadPortMapping(((MenuItem)sender).Tag.ToString(), "0000000000000000000000000");
                    break;
            }

        }

      
        private void RunningSpeed_cb_TextChanged(object sender, EventArgs e)
        {
            ChangeSpeed();
        }

        private void ChangeSpeed()
        {
            string sp = RunningSpeed_cb.Text.Replace("%", "");
            if (sp.Equals("100"))
            {
                sp = "0";
            }
            foreach (Node node in NodeManagement.GetList())
            {
                if (node.Type.Equals("Robot"))
                {
                    Transaction txn = new Transaction();
                    txn.Method = Transaction.Command.RobotType.RobotSpeed;
                    txn.Value = sp;
                    txn.FormName = "FormRunning";
                    node.SendCommand(txn);
                }
                else
                if (node.Type.Equals("Aligner"))
                {
                    Transaction txn = new Transaction();
                    txn.Method = Transaction.Command.AlignerType.AlignerSpeed;
                    txn.Value = sp;
                    txn.FormName = "FormRunning";
                    node.SendCommand(txn);
                }
            }
        }
    }
}
