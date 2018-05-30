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
        

        private Menu.Monitoring.FormMonitoring formMonitoring = new Menu.Monitoring.FormMonitoring();
        private Menu.Communications.FormCommunications formCommunications = new Menu.Communications.FormCommunications();
        private Menu.WaferMapping.FormWaferMapping formWafer = new Menu.WaferMapping.FormWaferMapping();
        private Menu.Status.FormStatus formStatus = new Menu.Status.FormStatus();
        private Menu.OCR.FormOCR formOCR = new Menu.OCR.FormOCR();
        private Menu.SystemSetting.FormSystemSetting formSystem = new Menu.SystemSetting.FormSystemSetting();

        public FormMain()
        {
            InitializeComponent();
            XmlConfigurator.Configure();
            Initialize();
            RouteCtrl = new RouteControl(this);

            //ts.SelectedIndex = 0;
            //t2.Text = "CURRENT USER NAME";
            //t1.Text = "CURRENT_ID";
            //t3.SelectedIndex = 3;
            //t4.Text = "***";
            //t5.Text = "***";
            //ts.Enabled = false;
            //t4.Enabled = true;
            //t5.Enabled = true;
            ////for (int i = 0; i < 100; i++)
            ////{
            ////    this.dg1.Rows.Add("", "", "", "", "");
            ////}
            ////dg1.Rows[60].Selected = true;
            //tabControl3.DrawItem += new DrawItemEventHandler(tabControl2_DrawItem);
            //tabControl3.TabPages.Remove(tp_bk);//隱藏目前沒使用到的頁面
            //cb17.SelectedIndex = 0;
            //cb18.SelectedIndex = 0;
            //cb19.SelectedIndex = 0;
            //cb20.SelectedIndex = 0;
            //cb21.SelectedIndex = 0;
            //cb22.SelectedIndex = 0;
            //cb23.SelectedIndex = 0;
            //cb24.SelectedIndex = 0;
            //cb25.SelectedIndex = 0;
            //for (double i = 360; i > 0; i = i - 1)
            //{
            //    dd1.Items.Add(i);
            //    dd2.Items.Add(i);
            //}
            //dd1.Items.Add("Offset");
            //dd2.Items.Add("Offset");
            //dd1.SelectedIndex = 360;
            //dd2.SelectedIndex = 360;
            //cba1.SelectedIndex = 0;
            //cba2.SelectedIndex = 0;

            //dgDevice.Rows.Add("Robot", "Kawasaki", 1, "", "Robot1", true);
            //dgDevice.Rows.Add("Aligner", "Sanwa", 1, "$1", "Aligner1", true);
            //dgDevice.Rows.Add("Aligner", "Sanwa", 2, "$2", "Aligner2", true);
            //dgDevice.Rows.Add("Load Port", "TDK", 1, "", "LoadPort01", true);
            //dgDevice.Rows.Add("Load Port", "TDK", 2, "", "LoadPort02", true);
            //dgDevice.Rows.Add("Load Port", "TDK", 3, "", "LoadPort03", true);
            //dgDevice.Rows.Add("DIO", "Advantech", 1, "", "DIO1", true);
            //dgDevice.Rows.Add("DIO", "Advantech", 2, "", "DIO2", true);

            ////vSBRobotStatus.Maximum = pbRobotState.Width - pnlRobotState.Width + vSBRobotStatus.Width; 水平
            //vSBRobotStatus.Maximum = pbRobotState.Height - pnlRobotState.Height + 40;//+ vSBRobotStatus.Height
            //vSBAlignerStatus.Maximum = pbAlignerState.Height - pnlAlignerState.Height + 40;// + vSBAlignerStatus.Height
            //vSBPortStatus.Maximum = pbPortState.Height - pnlPortState.Height + 40;//+ vSBPortStatus.Height
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
            Control[] ctrlForm = new Control[] { formMonitoring, formCommunications, formWafer, formStatus, formOCR, formSystem };

            try
            {
                for (int i = 0; i < ctrlForm.Length; i++)
                {
                    ((Form)ctrlForm[i]).TopLevel = false;
                    tbcMian.TabPages[i].Controls.Add(((Form)ctrlForm[i]));
                    ((Form)ctrlForm[i]).Show();
                    IntPtr dummy = ((Form)ctrlForm[i]).Handle;
                }
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
            if (MessageBox.Show(strMsg, "Initialize", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == DialogResult.OK)
            {
                Transaction txn = new Transaction();
                txn.Method = Transaction.Command.RobotType.Reset;
                NodeManagement.Get("Robot01").SendCommand(txn);
                txn = new Transaction();
                txn.Method = Transaction.Command.RobotType.Reset;
                NodeManagement.Get("Robot02").SendCommand(txn);
                txn = new Transaction();
                txn.Method = Transaction.Command.AlignerType.Reset;
                NodeManagement.Get("Aligner01").SendCommand(txn);
                txn = new Transaction();
                txn.Method = Transaction.Command.AlignerType.Reset;
                NodeManagement.Get("Aligner02").SendCommand(txn);
            }
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
                tbcMian.TabPages.Add(page);
                hiddenPages.Remove(page);
            }
            else
            {
                tbcMian.TabPages.Remove(page);
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
            Transaction txn = new Transaction();
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
                case "Robot":
                    switch (Txn.Method)
                    {
                        case Transaction.Command.RobotType.RobotSpeed:
                            ManualRobotStatusUpdate.ShowMsg("Change speed completed.");
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.RobotType.RobotHome;
                            txn.Arm = "";
                            Node.SendCommand(txn);
                            break;
                        case Transaction.Command.RobotType.Reset:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.RobotType.RobotServo;
                            txn.Arm = "1";
                            Node.SendCommand(txn);
                            break;
                        case Transaction.Command.RobotType.RobotServo:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.RobotType.RobotMode;
                            txn.Arm = "1";
                            Node.SendCommand(txn);
                            break;
                        case Transaction.Command.RobotType.RobotMode:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.RobotType.WaferRelease;
                            txn.Arm = "1";
                            Node.SendCommand(txn);
                            break;

                        
                    }
                    break;
                case "Aligner":
                    switch (Txn.Method)
                    {
                        case Transaction.Command.AlignerType.Reset:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.AlignerType.AlignerServo;
                            txn.Arm = "1";
                            Node.SendCommand(txn);
                            break;
                        case Transaction.Command.AlignerType.AlignerServo:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.AlignerType.AlignerMode;
                            txn.Arm = "1";
                            Node.SendCommand(txn);
                            break;
                        case Transaction.Command.AlignerType.AlignerMode:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.AlignerType.AlignerSpeed;
                            txn.Arm = "0";
                            Node.SendCommand(txn);
                            break;
                        case Transaction.Command.AlignerType.AlignerSpeed:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.AlignerType.AlignerOrigin;
                            txn.Arm = "0";
                            Node.SendCommand(txn);
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
                case "Robot":
                    switch (Txn.Method)
                    {
                        case Transaction.Command.RobotType.RobotSpeed:
                            ManualRobotStatusUpdate.ShowMsg("Change speed fail.");
                            break;
                        default:
                            ManualRobotStatusUpdate.ShowMsg("Execute: failure!");
                            break;
                    }
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
                case "Robot":
                   
                    switch (Txn.Method)
                    {
                        case Transaction.Command.RobotType.WaferRelease:
                            if (Txn.Arm.Equals("1"))
                            {
                                txn = new Transaction();
                                txn.TargetJobs = new List<Job>();
                                txn.Method = Transaction.Command.RobotType.WaferRelease;
                                txn.Arm = "2";
                                Node.SendCommand(txn);
                            }
                            else
                            {
                                txn = new Transaction();
                                txn.TargetJobs = new List<Job>();
                                txn.Method = Transaction.Command.RobotType.RobotSpeed;
                                txn.Arm = "0";
                                Node.SendCommand(txn);
                            }
                            break;

                        case Transaction.Command.RobotType.RobotHome:
                            //end

                            break;
                        default:
                            ManualRobotStatusUpdate.ShowMsg("Execute: Success!");
                            break;
                    }
                    break;
                case "Aligner":
                    switch (Txn.Method)
                    {

                        case Transaction.Command.AlignerType.AlignerOrigin:
                            txn = new Transaction();
                            txn.TargetJobs = new List<Job>();
                            txn.Method = Transaction.Command.AlignerType.Retract;
                            txn.Arm = "0";
                            Node.SendCommand(txn);
                            break;
                        case Transaction.Command.AlignerType.Retract:
                            //end
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
            //switch (e.TabPage.Text)
            //{
            //    case "Status":
            //        string[] r_status = new string[] { "01010101011111010111101111101011", "01011101011111010111101010101011" };
            //        string[] a_status = new string[] { "01110101011111010111101010101011", "11010101011111010111101010101011" };
            //        string[] l_status = new string[] { "111101010?1?2101100?", "1211010101?210?11001", "1111010101?2101100?1", "1111010101?210?11001",
            //                                           "2111010101?210?11001", "111101?0101210?11001", "1011010101?210?11001", "011101?101210?11001"};

            //        //add robot status
            //        dgvRstatus.Rows.Clear();
            //        for (int i = 0; i < r_status.Length; i++)
            //        {
            //            DataGridViewRow row = (DataGridViewRow)dgvRstatus.Rows[0].Clone();
            //            String robot = "Robot" + (i + 1);
            //            row.Cells[0].Value = robot;
            //            for (int j = 0; j < r_status[i].Length; j++)
            //            {
            //                string status = r_status[i].Substring(j, 1);
            //                row.Cells[j + 1].Value = status;
            //                row.Cells[j + 1].Style.BackColor = ConfigUtil.GetStatusColor("Robot", "Sanwa", status);
            //            }
            //            dgvRstatus.Rows.Add(row);
            //        }
            //        //add aligner status
            //        dgvAstatus.Rows.Clear();
            //        for (int i = 0; i < a_status.Length; i++)
            //        {
            //            DataGridViewRow row = (DataGridViewRow)dgvAstatus.Rows[0].Clone();
            //            String robot = "Aligner" + (i + 1);
            //            row.Cells[0].Value = robot;
            //            for (int j = 0; j < a_status[i].Length; j++)
            //            {
            //                string status = a_status[i].Substring(j, 1);
            //                row.Cells[j + 1].Value = status;
            //                row.Cells[j + 1].Style.BackColor = ConfigUtil.GetStatusColor("Aligner", "Sanwa", status);
            //            }
            //            dgvAstatus.Rows.Add(row);
            //        }
            //        //add load port status        
            //        dgvLstatus.Rows.Clear();
            //        for (int i = 0; i < l_status.Length; i++)
            //        {
            //            DataGridViewRow row = (DataGridViewRow)dgvLstatus.Rows[0].Clone();
            //            String robot = "LoadPort" + (i + 1);
            //            row.Cells[0].Value = robot;
            //            for (int j = 0; j < l_status[i].Length; j++)
            //            {
            //                string status = l_status[i].Substring(j, 1);
            //                row.Cells[j + 1].Value = status;
            //                row.Cells[j + 1].Style.BackColor = ConfigUtil.GetStatusColor("LoadPort", "TDK", status);
            //            }
            //            dgvLstatus.Rows.Add(row);
            //        }
            //        break;
            //    case "OCR":
            //        Transaction txn = new Transaction();
            //        txn.Method = Transaction.Command.OCRType.GetOnline;
            //        NodeManagement.Get("OCR01").SendCommand(txn);
            //        break;
            //    default:
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort01", "111211111?111110111W11111");
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort02", "11WW111112111111011110111");
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort03", "0000000000000000000000000");
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort04", "0000000000000000000000000");
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort05", "0000000000000000000000000");
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort06", "0000000000000000000000000");
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort07", "0000000000000000000000000");
            //        WaferAssignUpdate.UpdateLoadPortMapping("LoadPort08", "0000000000000000000000000");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort01", "LD");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort02", "LD");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort03", "LD");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort04", "LD");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort05", "UD");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort06", "UD");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort07", "UD");
            //        WaferAssignUpdate.UpdateLoadPortMode("LoadPort08", "UD");
            //        break;
            //}
        }

        private void vSBRobotStatus_Scroll(object sender, ScrollEventArgs e)
        {
            //pbRobotState.Top = -vSBRobotStatus.Value;
        }

        private void vSBAlignerStatus_Scroll(object sender, ScrollEventArgs e)
        {
            //pbAlignerState.Top = -vSBAlignerStatus.Value;
        }

        private void vSBPortStatus_Scroll(object sender, ScrollEventArgs e)
        {
            //pbPortState.Top = -vSBPortStatus.Value;
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
                EnablePage(tbcMian.TabPages[5], false);

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

        
    }
}
