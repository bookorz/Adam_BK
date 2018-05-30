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
using Adam.UI_Update.WaferMapping;
using System.Threading;
using Adam.UI_Update.Authority;

namespace Adam
{
    public partial class FormMain : Form, IEngineReport
    {
        public static RouteControl RouteCtrl;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FormMain));
        object CurrentSelected = null;

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
            vSBRobotStatus.Maximum = pbRobotState.Height - pnlRobotState.Height + 40;//+ vSBRobotStatus.Height
            vSBAlignerStatus.Maximum = pbAlignerState.Height - pnlAlignerState.Height + 40;// + vSBAlignerStatus.Height
            vSBPortStatus.Maximum = pbPortState.Height - pnlPortState.Height + 40;//+ vSBPortStatus.Height
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

        private void Form1_Load(object sender, EventArgs e)
        {

            try
            {


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

        private void btnLogInOut_Click(object sender, EventArgs e)
        {
            switch (btnLogInOut.Text)
            {
                case "Login":
                    GUI.FormLogin formLogin = new GUI.FormLogin();
                    formLogin.ShowDialog();
                    break;
                case "Logout":
                    AuthorityUpdate.UpdateLogoutInfo();
                    //disable authroity function
                    AuthorityUpdate.UpdateFuncInit("");
                    break;
            }
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

            Transaction txn = new Transaction();
            txn.Method = Transaction.Command.RobotType.RobotHome;
            NodeManagement.Get("Robot01").SendCommand(txn);
            txn = new Transaction();
            txn.Method = Transaction.Command.RobotType.RobotHome;
            NodeManagement.Get("Robot02").SendCommand(txn);

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

        private void terminalToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GUI.FormTerminal formTerminal = new GUI.FormTerminal();
            formTerminal.ShowDialog();
        }

        private void btnTeach_Click(object sender, EventArgs e)
        {
            UI_TEST.Teaching teaching = new UI_TEST.Teaching();
            teaching.ShowDialog();
        }

        private void runingToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UI_TEST.RunningScreen runningScreen = new UI_TEST.RunningScreen();
            runningScreen.ShowDialog();
        }

        private void btnVersion_Click(object sender, EventArgs e)
        {
            GUI.FormVersion formVersion = new GUI.FormVersion();
            formVersion.ShowDialog();
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
                            OCRUpdate.UpdateOCRStatus(Node.Name, Msg.Value);
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
                            OCRUpdate.UpdateOCRRead(Node.Name, Msg.Value);
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
                    for (int i = 0; i < r_status.Length; i++)
                    {
                        DataGridViewRow row = (DataGridViewRow)dgvRstatus.Rows[0].Clone();
                        String robot = "Robot" + (i + 1);
                        row.Cells[0].Value = robot;
                        for (int j = 0; j < r_status[i].Length; j++)
                        {
                            string status = r_status[i].Substring(j, 1);
                            row.Cells[j + 1].Value = status;
                            row.Cells[j + 1].Style.BackColor = ConfigUtil.GetStatusColor("Robot", "Sanwa", status);
                        }
                        dgvRstatus.Rows.Add(row);
                    }
                    //add aligner status
                    dgvAstatus.Rows.Clear();
                    for (int i = 0; i < a_status.Length; i++)
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
                    for (int i = 0; i < l_status.Length; i++)
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
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort01", "111211111?111110111W11111");
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort02", "11WW111112111111011110111");
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort03", "0000000000000000000000000");
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort04", "0000000000000000000000000");
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort05", "0000000000000000000000000");
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort06", "0000000000000000000000000");
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort07", "0000000000000000000000000");
                    WaferAssignUpdate.UpdateLoadPortMapping("LoadPort08", "0000000000000000000000000");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort01", "LD");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort02", "LD");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort03", "LD");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort04", "LD");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort05", "UD");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort06", "UD");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort07", "UD");
                    WaferAssignUpdate.UpdateLoadPortMode("LoadPort08", "UD");
                    break;
            }
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

        private void tgsConnection_CheckedChanged(object sender, EventArgs e)
        {
            if (tgsConnection.Checked)
            {
                RouteCtrl.ConnectAll();
            }
            else
            {
                RouteCtrl.DisconnectAll();
            }
        }

        private void tgsMode_SW_CheckedChanged(object sender, EventArgs e)
        {
            if (tgsMode_SW.Checked)
            {
                btnMaintence.Text = "Auto Mode";
                btnMaintence.BackColor = Color.Red;
                btnMaintence.Enabled = false;
                btnTeach.Enabled = false;
                EnablePage(tabControl1.TabPages[5], false);

                Node Aligner1 = NodeManagement.Get("Aligner01");
                if (Aligner1 != null)
                {
                    Aligner1.LockByNode = "LoadPort01";
                }

                ThreadPool.QueueUserWorkItem(new WaitCallback(RouteCtrl.Auto), "Normal");

               
            }
            else
            {
                btnMaintence.Text = "Maintenance Mode";
                btnMaintence.BackColor = Color.WhiteSmoke;
                btnMaintence.Enabled = true;
                btnTeach.Enabled = true;
                EnablePage(hiddenPages[0], true);
                RouteCtrl.Stop();//book
                                 //test
                                 //yyyy
            }
        }

        private void Assign_Gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    switch (e.Value)
                    {
                        case "No wafer":
                            e.CellStyle.BackColor = Color.Gray;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        case "Crossed":

                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        case "Undefined":
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        case "Double":
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        default:
                            e.CellStyle.BackColor = Color.Green;
                            e.CellStyle.ForeColor = Color.White;
                            break;

                    }
                    break;
                
            }
        }

        private void Assign_Gv_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();


                string PortName = (sender as DataGridView).Name.Replace("Assign_Gv", "");
                if (NodeManagement.Get(PortName).Mode.Equals("LD"))
                {

                    CurrentSelected = sender;
                    foreach (Node eachPort in NodeManagement.GetLoadPortList("UD"))
                    {
                        List<MenuItem> tmpAry = new List<MenuItem>();
                        for (int i = 1; i <= 25; i++)
                        {
                            MenuItem tmp;
                            if (!eachPort.JobList.ContainsKey(i.ToString()))
                            {
                                tmp = new MenuItem(eachPort.Name + "-" + i.ToString(), AssignPort);

                            }
                            else
                            {
                                tmp = new MenuItem(eachPort.Name + "-" + i.ToString(), AssignPort);
                                tmp.Enabled = false;
                            }
                            tmpAry.Add(tmp);
                        }
                        m.MenuItems.Add(eachPort.Name, tmpAry.ToArray());
                    }


                }

                m.Show((DataGridView)sender, new Point(e.X, e.Y));

            }
        }

        private void AssignPort(object sender, EventArgs e)
        {
            string PortName = (sender as MenuItem).Text.Split('-')[0];
            string Slot = (sender as MenuItem).Text.Split('-')[1];
            if ((CurrentSelected as DataGridView).SelectedRows.Count == 0)
            {
                MessageBox.Show("請選擇來源Slot");
            }
            else if ((CurrentSelected as DataGridView).SelectedRows.Count == 1)
            {
                string waferId = (CurrentSelected as DataGridView).SelectedRows[0].Cells["Job_Id"].Value.ToString();
                string OrgDest = (CurrentSelected as DataGridView).SelectedRows[0].Cells["Destination"].Value.ToString();
                string OrgDestSlot = (CurrentSelected as DataGridView).SelectedRows[0].Cells["DestinationSlot"].Value.ToString();
                Job wafer = JobManagement.Get(waferId);
                if (wafer != null)
                {
                    wafer.Destination = PortName;
                    wafer.DisplayDestination = PortName.Replace("Load","");
                    wafer.DestinationSlot = Slot;
                    wafer.ProcessFlag = false;
                    wafer.Position = PortName;
                    if (!OrgDest.Equals(""))
                    {
                        NodeManagement.Get(OrgDest).RemoveJob(OrgDestSlot);
                    }
                    NodeManagement.Get(PortName).AddJob(Slot,wafer);
                    (CurrentSelected as DataGridView).Refresh();
                }
                else
                {
                    MessageBox.Show("找不到此Wafer資料:" + wafer.Job_Id);
                }

            }
            else if ((CurrentSelected as DataGridView).SelectedRows.Count > 1)
            {
                int StartSlot = Convert.ToInt32(Slot);
                foreach(DataGridViewRow each in (CurrentSelected as DataGridView).SelectedRows)
                {
                    string waferId = each.Cells["Job_Id"].Value.ToString();
                    string OrgDest = each.Cells["Destination"].Value.ToString();
                    string OrgDestSlot = each.Cells["DestinationSlot"].Value.ToString();
                    Job wafer = JobManagement.Get(waferId);
                    if (wafer != null)
                    {
                        while (true)
                        {
                            if (NodeManagement.Get(PortName).GetJob(StartSlot.ToString()) == null)
                            {
                                wafer.Destination = PortName;
                                wafer.DisplayDestination = PortName.Replace("Load", "");
                                wafer.DestinationSlot = StartSlot.ToString();
                                wafer.Position = PortName;
                                wafer.ProcessFlag = false;
                                if (!OrgDest.Equals(""))
                                {
                                    NodeManagement.Get(OrgDest).RemoveJob(OrgDestSlot);
                                }
                                NodeManagement.Get(PortName).AddJob(StartSlot.ToString(), wafer);
                               
                                break;
                            }
                            else
                            {
                                StartSlot++;
                                if (StartSlot > 25)
                                {
                                    break;
                                }
                            }
                        }
                        StartSlot++;
                        if (StartSlot > 25)
                        {
                            break;
                        }
                    }
                }
                 (CurrentSelected as DataGridView).Refresh();
            }
           
        }

        private void State_lb_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CurrentSelected = sender;
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Change to LD", PortModeChange));
                m.MenuItems.Add(new MenuItem("Change to UD", PortModeChange));
                m.Show((Label)sender, new Point(e.X, e.Y));
            }
        }

        private void PortModeChange(object sender, EventArgs e)
        {
            string Name = (CurrentSelected as Label).Name.Replace("State_lb", "");
            switch (((MenuItem)sender).Text)
            {
                case "Change to LD":
                    NodeManagement.Get(Name).Mode = "LD";
                    WaferAssignUpdate.UpdateLoadPortMode(Name, "LD");
                    break;
                case "Change to UD":
                    NodeManagement.Get(Name).Mode = "UD";
                    WaferAssignUpdate.UpdateLoadPortMode(Name, "UD");
                    break;
            }

        }

        private void PortStart_Btn_Click(object sender, EventArgs e)
        {
            string PortName = (sender as Button).Name.Replace("_Start_Btn", "");
            Node port =  NodeManagement.Get(PortName);
            if (port != null)
            {
                port.Available = true;
               
            }
            else
            {
                MessageBox.Show(PortName + " 不存在");
            }
        }
    }
}
