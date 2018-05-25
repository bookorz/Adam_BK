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
using System.Collections;
using System.Diagnostics;
using Adam.UI_Update.OCR;

namespace Adam
{
    public partial class FormMain : Form, IEngineReport
    {
        public static RouteControl RouteCtrl;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FormMain));

        public FormMain()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            Initialize();
            RouteCtrl = new RouteControl(this);

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

            //vSBRobotStatus.Maximum = pbRobotState.Width - pnlRobotState.Width + vSBRobotStatus.Width; 水平
            vSBRobotStatus.Maximum = pbRobotState.Height - pnlRobotState.Height + 40 ;//+ vSBRobotStatus.Height
            vSBAlignerStatus.Maximum = pbAlignerState.Height - pnlAlignerState.Height + 40;// + vSBAlignerStatus.Height
            vSBPortStatus.Maximum = pbPortState.Height - pnlPortState.Height  + 40;//+ vSBPortStatus.Height
        }

        private void Initialize()
        {
            PathManagement.LoadConfig();
            //檢查OCR程式有沒有開
            //Process[] pc = Process.GetProcessesByName("VB9BReaderForm");
            //if (pc.Count() == 0)
            //{
            //    Process OCRProg = new Process();
            //    // FileName 是要執行的檔案
            //    OCRProg.StartInfo.FileName = "C:/Program Files (x86)/HST Vision/e-Reader8000/VB9BReaderForm.exe";
            //    OCRProg.Start();


            //}
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button7_Click(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
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
               

                dtAligner01.Columns.Add("Wafer_ID");
                dr = dtAligner01.NewRow();
                dr["Wafer_ID"] = string.Empty;
                dtAligner01.Rows.Add(dr);
                dataGridView11.DataSource = dtAligner01;
                dataGridView12.DataSource = dtAligner01;
              

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

        private void label50_Click(object sender, EventArgs e)
        {

        }

        private void textBox23_TextChanged(object sender, EventArgs e)
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
                case "OCR":
                    switch (Txn.Method)
                    {
                        case Transaction.Command.OCRType.GetOnline:
                            OCRUpdate.UpdateOCRStatus(Node.Name,Msg.Value);
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
                case "OCR":
                    switch (Txn.Method)
                    {
                        case Transaction.Command.OCRType.Read:
                            OCRUpdate.UpdateOCRRead(Node.Name,Msg.Value);
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
                EnablePage(tabControl1.TabPages[5], false);



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

        private void tabControl1_Selected(object sender, TabControlEventArgs e)
        {
            switch (e.TabPage.Text)
            {
                case "Status":
                    string[] r_status = new string[] { "01010101011111010111101111101011", "01011101011111010111101010101011" };
                    string[] a_status = new string[] { "01110101011111010111101010101011", "11010101011111010111101010101011" };
                    string[] l_status = new string[] { "111101010?1?2101100?", "1211010101?210?11001", "1111010101?2101100?1", "1111010101?210?11001",
                                                       "2111010101?210?11001", "111101?0101210?11001", "1011010101?210?11001", "011101?101210?11001"};

                    //add robot status
                    dgvRstatus.Rows.Clear();
                    for (int i = 0; i < r_status.Length ; i++)
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvRstatus.Rows[0].Clone();
                        String robot = "Robot" + (i + 1);
                        row.Cells[0].Value = robot;
                        for(int j = 0; j < r_status[i].Length; j++)
                        {
                            string status = r_status[i].Substring(j, 1);
                            row.Cells[j + 1].Value = status;
                            row.Cells[j + 1].Style.BackColor = ConfigUtil.GetStatusColor("Robot","Sanwa", status);
                        }
                        dgvRstatus.Rows.Add(row);
                    }
                    //add aligner status
                    dgvAstatus.Rows.Clear();
                    for (int i = 0; i < a_status.Length ; i++)
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvAstatus.Rows[0].Clone();
                        String robot = "Aligner" + (i + 1);
                        row.Cells[0].Value = robot;
                        for (int j = 0; j < a_status[i].Length; j++)
                        {
                            string status = a_status[i].Substring(j, 1);
                            row.Cells[j + 1].Value = status;
                            row.Cells[j + 1].Style.BackColor = ConfigUtil.GetStatusColor("Aligner", "Sanwa", status);
                        }
                        dgvAstatus.Rows.Add(row);
                    }
                    //add load port status        
                    dgvLstatus.Rows.Clear();
                    for (int i = 0; i < l_status.Length ; i++)
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvLstatus.Rows[0].Clone();
                        String robot = "LoadPort" + (i + 1);
                        row.Cells[0].Value = robot;
                        for (int j = 0; j < l_status[i].Length; j++)
                        {
                            string status = l_status[i].Substring(j, 1);
                            row.Cells[j + 1].Value = status;
                            row.Cells[j + 1].Style.BackColor = ConfigUtil.GetStatusColor("LoadPort", "TDK", status);
                        }
                        dgvLstatus.Rows.Add(row);
                    }
                    break;
                case "OCR":
                    Transaction txn = new Transaction();
                    txn.Method = Transaction.Command.OCRType.GetOnline;
                    NodeManagement.Get("OCR01").SendCommand(txn);
                    break;
                default:
                    break;
            }
        }

        private void label82_Click(object sender, EventArgs e)
        {

        }

        private void vSBRobotStatus_Scroll(object sender, ScrollEventArgs e)
        {
            pbRobotState.Top = -vSBRobotStatus.Value;
        }

        private void vSBAlignerStatus_Scroll(object sender, ScrollEventArgs e)
        {
            pbAlignerState.Top = -vSBAlignerStatus.Value;
        }

        private void vSBPortStatus_Scroll(object sender, ScrollEventArgs e)
        {
            pbPortState.Top = -vSBPortStatus.Value;
        }

        private void OCRButton(object sender, EventArgs e)
        {
            Button TriggerBtn = sender as Button;
            Transaction txn = new Transaction();
            switch (TriggerBtn.Name)
            {
                case "OCR01Online_Btn":
                    OCR01Online_Btn.Enabled = false;
                    OCR01Offline_Btn.Enabled = true;
                    txn.Method = Transaction.Command.OCRType.Online;
                    NodeManagement.Get("OCR01").SendCommand(txn);
                    break;
                case "OCR01Offline_Btn":
                    OCR01Online_Btn.Enabled = true;
                    OCR01Offline_Btn.Enabled = false;
                    txn.Method = Transaction.Command.OCRType.Offline;
                    NodeManagement.Get("OCR01").SendCommand(txn);
                    break;
                case "OCR01Read_Bt":
                    txn.Method = Transaction.Command.OCRType.Read;
                    NodeManagement.Get("OCR01").SendCommand(txn);
                    break;
                case "OCR02Online_Btn":
                    OCR01Online_Btn.Enabled = false;
                    OCR01Offline_Btn.Enabled = true;
                    txn.Method = Transaction.Command.OCRType.Online;
                    NodeManagement.Get("OCR02").SendCommand(txn);
                    break;
                case "OCR02Offline_Btn":
                    OCR01Online_Btn.Enabled = true;
                    OCR01Offline_Btn.Enabled = false;
                    txn.Method = Transaction.Command.OCRType.Offline;
                    NodeManagement.Get("OCR02").SendCommand(txn);
                    break;
                case "OCR02Read_Bt":
                    txn.Method = Transaction.Command.OCRType.Read;
                    NodeManagement.Get("OCR02").SendCommand(txn);
                    break;
            }

        }

        private void tableLayoutPanel13_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
