using SANWA.Utility;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Engine;
using TransferControl.Management;
using log4net.Config;
using Adam.UI_Update.Monitoring;
using log4net;
using Newtonsoft.Json;
using Adam.UI_Update.Manual;
using DIOControl;
using Adam.UI_Update.Layout;

namespace Adam
{
    public partial class FormMain : Form, IEngineReport, IDIOTriggerReport
    {
        public static RouteControl RouteCtrl;
        public static DIO DigitalDIO;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FormMain));

        public FormMain()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            

            ts.SelectedIndex = 0;
            t2.Text = "CURRENT USER NAME";
            t1.Text = "CURRENT_ID";
            t3.SelectedIndex = 3;
            t4.Text = "***";
            t5.Text = "***";
            ts.Enabled = false;
            t4.Enabled = true;
            t5.Enabled = true;
            //for (int i = 0; i < 100; i++)
            //{
            //    this.dg1.Rows.Add("", "", "", "", "");
            //}
            //dg1.Rows[60].Selected = true;
            tabControl3.DrawItem += new DrawItemEventHandler(tabControl2_DrawItem);
            tabControl3.TabPages.Remove(tp_bk);//隱藏目前沒使用到的頁面
            cb17.SelectedIndex = 0;
            cb18.SelectedIndex = 0;
            cb19.SelectedIndex = 0;
            cb20.SelectedIndex = 0;
            cb21.SelectedIndex = 0;
            cb22.SelectedIndex = 0;
            cb23.SelectedIndex = 0;
            cb24.SelectedIndex = 0;
            cb25.SelectedIndex = 0;
            for (double i = 360; i > 0; i = i - 1)
            {
                dd1.Items.Add(i);
                dd2.Items.Add(i);
            }
            dd1.Items.Add("Offset");
            dd2.Items.Add("Offset");
            dd1.SelectedIndex = 360;
            dd2.SelectedIndex = 360;
            cba1.SelectedIndex = 0;
            cba2.SelectedIndex = 0;

            dgDevice.Rows.Add("Robot", "Kawasaki", 1, "", "Robot1", true);
            dgDevice.Rows.Add("Aligner", "Sanwa", 1, "$1", "Aligner1", true);
            dgDevice.Rows.Add("Aligner", "Sanwa", 2, "$2", "Aligner2", true);
            dgDevice.Rows.Add("Load Port", "TDK", 1, "", "LoadPort01", true);
            dgDevice.Rows.Add("Load Port", "TDK", 2, "", "LoadPort02", true);
            dgDevice.Rows.Add("Load Port", "TDK", 3, "", "LoadPort03", true);
            dgDevice.Rows.Add("DIO", "Advantech", 1, "", "DIO1", true);
            dgDevice.Rows.Add("DIO", "Advantech", 2, "", "DIO2", true);
        }

        private void Initialize()
        {
            PathManagement.LoadConfig();          
            RouteCtrl = new RouteControl(this);
            DigitalDIO = new DIO(this);
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Initialize();

            DataTable dtConnectStatus = new DataTable();
            DataTable dtLoadPort01 = new DataTable();
            DataTable dtAligner01 = new DataTable();
            DataTable dtRobot01 = new DataTable();
            DataTable dtLoadport02 = new DataTable();

            DataRow dr;

            string[] str = new string[] { "Robot01", "Robot02", "Aligner01", "Aligner02", "LoadPort01", "LoadPort02", "LoadPort03", "LoadPort04", "LoadPort05", "LoadPort06", "LoadPort07", "LoadPort08", "IO_01", "IO_02", "IO_03" };

            try
            {
                LoadPort01.AutoGenerateColumns = false;
                dataGridView1.AutoGenerateColumns = false;
                dataGridView3.AutoGenerateColumns = false;
                dataGridView4.AutoGenerateColumns = false;
                dataGridView5.AutoGenerateColumns = false;
                dataGridView6.AutoGenerateColumns = false;
                dataGridView7.AutoGenerateColumns = false;

                dataGridView9.AutoGenerateColumns = false;

                dataGridView11.AutoGenerateColumns = false;
                dataGridView12.AutoGenerateColumns = false;

                //dtConnectStatus.Columns.Add("Device_ID");
                //dtConnectStatus.Columns.Add("Connection_Status");
                //dtConnectStatus.Columns.Add("Device_Status");

                //for (int i = 0; i < str.Length; i++)
                //{
                //    dr = dtConnectStatus.NewRow();
                //    dr["Device_ID"] = str[i].ToString();
                //    dr["Connection_Status"] = string.Empty;
                //    dr["Device_Status"] = string.Empty;
                //    dtConnectStatus.Rows.Add(dr);
                //}

                //dataGridView2.DataSource = dtConnectStatus;

                dtLoadPort01.Columns.Add("Slot");
                dtLoadPort01.Columns.Add("Wafer_ID");

                for (int i = 25; i > 0; i--)
                {
                    dr = dtLoadPort01.NewRow();
                    dr["Slot"] = (i).ToString();
                    dr["Wafer_ID"] = string.Empty;
                    dtLoadPort01.Rows.Add(dr);
                }

                LoadPort01.DataSource = dtLoadPort01;
                dataGridView1.DataSource = dtLoadPort01;
                dataGridView3.DataSource = dtLoadPort01;
                dataGridView4.DataSource = dtAligner01;
                dataGridView5.DataSource = dtAligner01;
                dataGridView6.DataSource = dtAligner01;
                dataGridView7.DataSource = dtAligner01;

                dataGridView13.DataSource = dtLoadPort01;
                dataGridView14.DataSource = dtLoadPort01;
                dataGridView15.DataSource = dtLoadPort01;

                dtAligner01.Columns.Add("Wafer_ID");
                dr = dtAligner01.NewRow();
                dr["Wafer_ID"] = string.Empty;
                dtAligner01.Rows.Add(dr);
                dataGridView11.DataSource = dtAligner01;
                dataGridView12.DataSource = dtAligner01;
                dataGridView16.DataSource = dtAligner01;
                dataGridView17.DataSource = dtAligner01;
                dataGridView18.DataSource = dtAligner01;
                dataGridView19.DataSource = dtAligner01;

                dtRobot01.Columns.Add("Arm");
                dtRobot01.Columns.Add("Wafer_ID");
                dr = dtRobot01.NewRow();
                dr["Arm"] = "Upper";
                dr["Wafer_ID"] = string.Empty;
                dtRobot01.Rows.Add(dr);
                dr = dtRobot01.NewRow();
                dr["Arm"] = "Lower";
                dr["Wafer_ID"] = string.Empty;
                dtRobot01.Rows.Add(dr);
                dataGridView9.DataSource = dtRobot01;



                dtLoadport02.Columns.Add("Device_ID");
                dtLoadport02.Columns.Add("Action");
                dr = dtLoadport02.NewRow();
                dr["Device_ID"] = "LoadPort01";
                dr["Action"] = string.Empty;
                dtLoadport02.Rows.Add(dr);
                dr = dtLoadport02.NewRow();
                dr["Device_ID"] = "LoadPort02";
                dr["Action"] = string.Empty;
                dtLoadport02.Rows.Add(dr);
                dr = dtLoadport02.NewRow();
                dr["Device_ID"] = "LoadPort03";
                dr["Action"] = string.Empty;
                dtLoadport02.Rows.Add(dr);
                dataGridView20.DataSource = dtLoadport02;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void LoadPort01_DataBindingComplete(object sender, DataGridViewBindingCompleteEventArgs e)
        {
            ((DataGridView)sender).ClearSelection();
        }

        private void groupBox6_Enter(object sender, EventArgs e)
        {

        }

        private void label17_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown1_ValueChanged(object sender, EventArgs e)
        {

        }

        private void label28_Click(object sender, EventArgs e)
        {

        }

        private void numericUpDown8_ValueChanged(object sender, EventArgs e)
        {

        }

        private void groupBox5_Enter(object sender, EventArgs e)
        {

        }

        private void tableLayoutPanel19_Paint(object sender, PaintEventArgs e)
        {

        }

        private void groupBox17_Enter(object sender, EventArgs e)
        {

        }

        private void panel3_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button24_Click(object sender, EventArgs e)
        {

        }

        private void button25_Click(object sender, EventArgs e)
        {

        }

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
        {

        }

        private void button28_Click(object sender, EventArgs e)
        {

        }

        private void splitButton1_Click(object sender, EventArgs e)
        {
        }

        private void tabControl2_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;
            Brush _textBrush;

            // Get the item from the collection.
            TabPage _tabPage = tabControl3.TabPages[e.Index];

            // Get the real bounds for the tab rectangle.
            Rectangle _tabBounds = tabControl3.GetTabRect(e.Index);

            if (e.State == DrawItemState.Selected)
            {

                // Draw a different background color, and don't paint a focus rectangle.
                //_textBrush = new SolidBrush(Color.DarkGray);
                //g.FillRectangle(Brushes.Transparent, e.Bounds);
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                g.FillRectangle(Brushes.SkyBlue, e.Bounds);
            }
            else
            {
                _textBrush = new System.Drawing.SolidBrush(e.ForeColor);
                e.DrawBackground();
            }

            // Use our own font.
            Font _tabFont = new Font("微軟正黑體", (float)18.0, FontStyle.Bold, GraphicsUnit.Pixel);

            // Draw string. Center the text.
            StringFormat _stringFlags = new StringFormat();
            _stringFlags.Alignment = StringAlignment.Center;
            _stringFlags.LineAlignment = StringAlignment.Center;
            g.DrawString(_tabPage.Text, _tabFont, _textBrush, _tabBounds, new StringFormat(_stringFlags));
        }

        private void panel40_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button18_Click(object sender, EventArgs e)
        {
            GUI.FormLogin formLogin = new GUI.FormLogin();
            formLogin.ShowDialog();
        }

        private void contextMenuStrip2_Opening(object sender, CancelEventArgs e)
        {

        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            FormAlarmHis form4 = new FormAlarmHis();
            form4.Text = "Message History";
            form4.label21.Text = "Message History";
            form4.Show();
        }

        private void bBBToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAlarmHis form4 = new FormAlarmHis();
            form4.Show();
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            string strMsg = "This equipment performs the initialization and origin search OK?\r\n" + "This equipment will be initalized, each axis will return to home position.\r\n" + "Check the condition of the wafer.";
            MessageBox.Show(strMsg, "Initialize", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string strMsg = "Move to Home position. OK?";
            MessageBox.Show(strMsg, "Org.Back", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification);

        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            string strMsg = "Switching to manual mode.\r\n" + "In this mode, your operation may damage the equipment.\r\n" + "Suffcient cautions are required for your operation.";
            if (MessageBox.Show(strMsg, "Manual", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == DialogResult.OK)
            {
                GUI.FormManual formManual = new GUI.FormManual();
                formManual.Show();
            }


        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            FormUnitCtrlData form2 = new FormUnitCtrlData();
            form2.ShowDialog();
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            FormTransTest form3 = new FormTransTest();
            form3.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            UI_TEST.IO iO = new UI_TEST.IO();
            iO.ShowDialog();
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            UI_TEST.LLSetting lLSetting = new UI_TEST.LLSetting();
            lLSetting.ShowDialog();
        }

        private void settingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI_TEST.Setting setting = new UI_TEST.Setting();
            setting.ShowDialog();
        }

        private void terminalToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void terminalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GUI.FormTerminal formTerminal = new GUI.FormTerminal();
            formTerminal.ShowDialog();
        }

        private void button70_Click(object sender, EventArgs e)
        {
            UI_TEST.Teaching teaching = new UI_TEST.Teaching();
            teaching.ShowDialog();
        }

        private void runingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI_TEST.RunningScreen runningScreen = new UI_TEST.RunningScreen();
            runningScreen.ShowDialog();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            GUI.FormVersion formVersion = new GUI.FormVersion();
            formVersion.ShowDialog();
        }

        private void myButtonCheck4_Load(object sender, EventArgs e)
        {


        }



        private void tabControl1_TabIndexChanged(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private List<TabPage> hiddenPages = new List<TabPage>();

        private void EnablePage(TabPage page, bool enable)
        {
            if (enable)
            {
                tabControl1.TabPages.Add(page);
                hiddenPages.Remove(page);
            }
            else
            {
                tabControl1.TabPages.Remove(page);
                hiddenPages.Add(page);
            }
        }

        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            GUI.FormLogSave formLogSave = new GUI.FormLogSave();
            formLogSave.ShowDialog();
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SorterControl.AlarmFrom alarmFrom = new SorterControl.AlarmFrom();
            alarmFrom.Text = "MessageFrom";
            alarmFrom.BackColor = Color.Blue;
            alarmFrom.ResetAll_bt.Enabled = false;
            alarmFrom.ShowDialog();
        }

        private void aAAToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SorterControl.AlarmFrom alarmFrom = new SorterControl.AlarmFrom();
            alarmFrom.ShowDialog();
        }

        private void Connection_Click(object sender, EventArgs e)
        {
            if (Connection.Checked)
            {
                RouteCtrl.ConnectAll();
            }
            else
            {
                RouteCtrl.DisconnectAll();
            }
        }

        public void On_Command_Excuted(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            logger.Debug("On_Command_Excuted");

            switch (Node.Type)
            {
                case "LoadPort":
                    ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command);
                    switch (Txn.Method)
                    {
                        case Transaction.Command.LoadPortType.ReadVersion:
                            ManualPortStatusUpdate.UpdateVersion(Node.Name, Msg.Value);
                            break;
                        case Transaction.Command.LoadPortType.GetLED:
                            ManualPortStatusUpdate.UpdateLED(Node.Name, Msg.Value);
                            break;
                        case Transaction.Command.LoadPortType.ReadStatus:
                            ManualPortStatusUpdate.UpdateStatus(Node.Name, Msg.Value);
                            break;
                        case Transaction.Command.LoadPortType.GetCount:

                            break;
                        case Transaction.Command.LoadPortType.GetMapping:
                            ManualPortStatusUpdate.UpdateMapping(Node.Name, Msg.Value);
                            break;
                    }

                    break;
            }
        }

        public void On_Command_Error(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            logger.Debug("On_Command_Error");
            switch (Node.Type)
            {
                case "LoadPort":
                    ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command);
                    break;
            }
            
        }

        public void On_Command_Finished(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            logger.Debug("On_Command_Finished");
            Transaction txn = new Transaction();
            switch (Node.Type)
            {
                case "LoadPort":
                    ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command);
                    
                    switch (Msg.Command)
                    {
                        case Transaction.Command.LoadPortType.MappingLoad:
                            txn.Method = Transaction.Command.LoadPortType.GetMapping;
                            Node.SendCommand(txn);
                            break;
                    }
                    break;
            }
            
        }

        public void On_Command_TimeOut(Node Node, Transaction Txn)
        {
            logger.Debug("On_Command_TimeOut");
            switch (Node.Type)
            {
                case "LoadPort":
                    ManualPortStatusUpdate.UpdateLog(Node.Name, Txn.CommandEncodeStr + " Timeout!");
                    break;
            }
        }

        public void On_Event_Trigger(Node Node, ReturnMessage Msg)
        {
            logger.Debug("On_Event_Trigger");
            Transaction txn = new Transaction();
            switch (Node.Type)
            {
                case "LoadPort":
                    ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command);
                    switch (Msg.Command)
                    {
                        case "MANSW":
                            txn.Method = Transaction.Command.LoadPortType.MappingLoad;
                            Node.SendCommand(txn);
                            break;
                    }
                    break;
            }
            //logger.Debug(JsonConvert.SerializeObject(Msg));
            
            
        }

        public void On_Node_State_Changed(Node Node, string Status)
        {
            logger.Debug("On_Node_State_Changed");
            NodeStatusUpdate.UpdateNodeState(Node.Name, Status);
        }

        public void On_Controller_State_Changed(string Device_ID, string Status)
        {
            ConnectionStatusUpdate.UpdateControllerStatus(Device_ID, Status);
            logger.Debug("On_Controller_State_Changed");
        }

        public void On_Port_Finished(string PortName)
        {
            logger.Debug("On_Port_Finished");
        }



        private void button8_Click(object sender, EventArgs e)
        {

        }



        private void Mode_sw_Click(object sender, EventArgs e)
        {
            if (Mode_sw.Checked)
            {
                splitButton5.Text = "Auto Mode";
                splitButton5.BackColor = Color.Red;
                splitButton5.Enabled = false;
                button70.Enabled = false;
                EnablePage(tabControl1.TabPages[7], false);



                JobManagement.Initial();

                Node Aligner1 = NodeManagement.Get("Aligner01");
                if (Aligner1 != null)
                {
                    Aligner1.LockByNode = "LoadPort01";
                }

                Node P1 = NodeManagement.Get("LoadPort01");
                P1.Available = true;
                P1.Fetchable = true;
                P1.JobList.Clear();

                for (int i = 1; i <= 25; i++)
                {
                    Job w = new Job();
                    w.Job_Id = "Wafer" + i.ToString("000");

                    w.AlignerFlag = true;
                    w.OCRFlag = false;
                    w.Position = P1.Name;
                    w.ProcessFlag = false;
                    w.FromPort = P1.Name;
                    w.Slot = i.ToString();
                    w.Destination = "LoadPort02";
                    w.DestinationSlot = i.ToString(); ;
                    JobManagement.Add(w.Job_Id, w);
                    P1.JobList.TryAdd(w.Slot, w);
                }

                RouteCtrl.Auto("Normal");
            }
            else
            {
                splitButton5.Text = "Maintenance Mode";
                splitButton5.BackColor = Color.WhiteSmoke;
                splitButton5.Enabled = true;
                button70.Enabled = true;
                EnablePage(hiddenPages[0], true);
                RouteCtrl.Stop();//book
                //test
				//yyyy
            }
        }

        public void On_Data_Chnaged(string Parameter, string Value)
        {
            switch (Parameter)
            {
                case "Red":
                case "Orange":
                case "Green":
                case "Blue":
                    DIOUpdate.UpdateSignalTower(Parameter, Value);
                    break;
                default:
                    DIOUpdate.UpdateDIOStatus(Parameter, Value);
                    break;
            }
            

        }

        public void On_Error_Occurred(string ErrorMsg)
        {
            
        }

        private void panel5_Click(object sender, EventArgs e)
        {
            if (Red_St.BackColor == Color.Red) {
                DigitalDIO.SetIO("Red", "false");
            }
            else
            {
                DigitalDIO.SetIO("Red","true");
            }
            
        }

        private void Orange_St_Click(object sender, EventArgs e)
        {
            if (Orange_St.BackColor == Color.DarkOrange)
            {
                DigitalDIO.SetIO("Orange", "false");
            }
            else
            {
                DigitalDIO.SetIO("Orange", "true");
            }
        }

        private void Green_St_Click(object sender, EventArgs e)
        {
            if (Green_St.BackColor == Color.Green)
            {
                DigitalDIO.SetIO("Green", "false");
            }
            else
            {
                DigitalDIO.SetIO("Green", "true");
            }
        }

        private void Blue_St_Click(object sender, EventArgs e)
        {
            if (Blue_St.BackColor == Color.Blue)
            {
                DigitalDIO.SetIO("Blue", "false");
            }
            else
            {
                DigitalDIO.SetIO("Blue", "true");
            }
        }

        private void button7_Click_1(object sender, EventArgs e)
        {
            DigitalDIO.SetBlink("Red","TRUE");
        }

        private void button9_Click(object sender, EventArgs e)
        {
            DigitalDIO.SetBlink("Orange", "TRUE");
        }

        private void button10_Click(object sender, EventArgs e)
        {
            DigitalDIO.SetBlink("Green", "TRUE");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            DigitalDIO.SetBlink("Blue", "TRUE");
        }
    }
}
