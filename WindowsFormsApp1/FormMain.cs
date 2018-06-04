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
using DIOControl;
using Adam.UI_Update.Layout;
using Adam.UI_Update.Alarm;

namespace Adam
{
    public partial class FormMain : Form, IEngineReport, IDIOTriggerReport
    {
        public static RouteControl RouteCtrl;
        public static DIO DIO;
        public static AlarmMapping AlmMapping;
        private static readonly ILog logger = LogManager.GetLogger(typeof(FormMain));
        object CurrentSelected = null;
        AlarmFrom alarmFrom = new AlarmFrom();
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
            DIO = new DIO(this);
            AlmMapping = new AlarmMapping();
           
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
            
            this.Visible = false;

            Control[] ctrlForm = new Control[] { formMonitoring, formCommunications, formWafer, formStatus, formOCR, formSystem };

            try
            {
                for (int i = 0; i < ctrlForm.Length; i++)
                {
                    ((Form)ctrlForm[i]).TopLevel = false;
                    tbcMian.TabPages[i].Controls.Add(((Form)ctrlForm[i]));
                    ((Form)ctrlForm[i]).Show();
                    tbcMian.SelectTab(i);
                }

                tbcMian.SelectTab(0);
                alarmFrom.Hide();
                alarmFrom.Show();
                //alarmFrom.Visible = false;
                alarmFrom.Hide();


                //this.FormBorderStyle = FormBorderStyle.FixedToolWindow;
                //this.WindowState = FormWindowState.Minimized;
                //this.WindowState = FormWindowState.Maximized;
                //this.TopLevel = true;

            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            Thread.Sleep(2000);
       
            if (SplashScreen.Instance != null)
            {
                SplashScreen.Instance.BeginInvoke(new MethodInvoker(SplashScreen.Instance.Dispose));
                SplashScreen.Instance = null;
            }
            this.Visible = true;
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
            //if (MessageBox.Show(strMsg, "Manual", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == DialogResult.OK)
            //{
                GUI.FormManual formManual = new GUI.FormManual();
                formManual.Show();
            //}
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
            AlarmFrom alarmFrom = new AlarmFrom();
            alarmFrom.Text = "MessageFrom";
            alarmFrom.BackColor = Color.Blue;
            alarmFrom.ResetAll_bt.Enabled = false;
            alarmFrom.ShowDialog();
        }

        private void aAAToolStripMenuItem_Click(object sender, EventArgs e)
        {

            alarmFrom.Visible = true;
        }

        public void On_Command_Excuted(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            logger.Debug("On_Command_Excuted");
            Transaction txn = new Transaction();
            switch (Txn.FormName)
            {
                case "FormManual":
                    switch (Node.Type)
                    {
                        case "LoadPort":
                            ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command + " Excuted");
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
                                case Transaction.Command.RobotType.RobotMode:
                                case Transaction.Command.RobotType.Reset:
                                case Transaction.Command.RobotType.RobotServo:
                                case Transaction.Command.RobotType.GetSpeed:
                                case Transaction.Command.RobotType.GetRIO:
                                case Transaction.Command.RobotType.GetError:
                                case Transaction.Command.RobotType.GetMode:
                                case Transaction.Command.RobotType.Stop:
                                case Transaction.Command.RobotType.Pause:
                                case Transaction.Command.RobotType.Continue:
                                case Transaction.Command.RobotType.GetStatus:
                                case Transaction.Command.RobotType.GetSV:
                                    ManualRobotStatusUpdate.UpdateGUI(Txn, Node.Name, Msg.Value);//update 手動功能畫面
                                    break;
                            }
                            break;
                        case "Aligner":
                            switch (Txn.Method)
                            {
                                case Transaction.Command.AlignerType.AlignerSpeed:
                                case Transaction.Command.AlignerType.AlignerMode:
                                case Transaction.Command.AlignerType.GetStatus:
                                case Transaction.Command.AlignerType.GetSpeed:
                                case Transaction.Command.AlignerType.GetRIO:
                                case Transaction.Command.AlignerType.GetError:
                                case Transaction.Command.AlignerType.Reset:
                                case Transaction.Command.AlignerType.AlignerServo:
                                case Transaction.Command.AlignerType.GetMode:
                                case Transaction.Command.AlignerType.GetSV:
                                    ManualAlignerStatusUpdate.UpdateGUI(Txn, Node.Name, Msg.Value);//update 手動功能畫面
                                    break;
                            }
                            break;
                    }
                    break;
            }
        }

        public void On_Command_Error(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            logger.Debug("On_Command_Error");
            AlarmInfo CurrentAlarm = new AlarmInfo();
            CurrentAlarm.NodeName = Node.Name;
            CurrentAlarm.AlarmCode = Msg.Value;
            try
            {

                AlarmMessage Detail = AlmMapping.Get(Node.Brand, Node.Type, CurrentAlarm.AlarmCode);

                CurrentAlarm.SystemAlarmCode = Detail.CodeID;
                CurrentAlarm.Desc = Detail.Code_Cause;
                CurrentAlarm.EngDesc = Detail.Code_Cause_English;
            }
            catch (Exception e)
            {
                CurrentAlarm.Desc = "未定義";
                logger.Error(Node.Controller + "-" + Node.AdrNo + "(GetAlarmMessage)" + e.Message + "\n" + e.StackTrace);
            }
            CurrentAlarm.TimeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");

            AlarmManagement.Add(CurrentAlarm);
            Form form = Application.OpenForms["AlarmFrom"];

            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
            AlarmUpdate.UpdateAlarmHistory(AlarmManagement.GetHistory());
        }

        public void On_Command_Finished(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            logger.Debug("On_Command_Finished");
            switch (Txn.FormName)
            {
                case "FormManual":
                    Transaction txn = new Transaction();
                    switch (Node.Type)
                    {
                        case "LoadPort":
                            ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command + " Finished");
                            
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
                            ManualRobotStatusUpdate.UpdateGUI(Txn, Node.Name, Msg.Value);//update 手動功能畫面
                            break;
                        case "Aligner":
                            ManualAlignerStatusUpdate.UpdateGUI(Txn, Node.Name, Msg.Value);//update 手動功能畫面
                            break;
                    }
                    break;
            }
        }

        public void On_Command_TimeOut(Node Node, Transaction Txn)
        {
            logger.Debug("On_Command_TimeOut");
            AlarmInfo CurrentAlarm = new AlarmInfo();
            CurrentAlarm.NodeName = Node.Name.Replace("Status", "");
            CurrentAlarm.AlarmCode = "00000001";
            CurrentAlarm.SystemAlarmCode = "FF00000001";
            CurrentAlarm.Desc = "命令逾時,連線異常";
            CurrentAlarm.TimeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");

            AlarmManagement.Add(CurrentAlarm);
            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
            AlarmUpdate.UpdateAlarmHistory(AlarmManagement.GetHistory());
        }

        public void On_Event_Trigger(Node Node, ReturnMessage Msg)
        {
            logger.Debug("On_Event_Trigger");
            if (Msg.Command.Equals("ERROR"))
            {
                Node.InitialComplete = false;
                logger.Debug("On_Command_Error");
                AlarmInfo CurrentAlarm = new AlarmInfo();
                CurrentAlarm.NodeName = Node.Name;
                CurrentAlarm.AlarmCode = Msg.Value;
                try
                {

                    AlarmMessage Detail = AlmMapping.Get(Node.Brand, Node.Type, CurrentAlarm.AlarmCode);

                    CurrentAlarm.SystemAlarmCode = Detail.CodeID;
                    CurrentAlarm.Desc = Detail.Code_Cause;
                    CurrentAlarm.EngDesc = Detail.Code_Cause_English;
                }
                catch
                {
                    CurrentAlarm.Desc = "未定義";
                }
                CurrentAlarm.TimeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");

                AlarmManagement.Add(CurrentAlarm);
                AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
                AlarmUpdate.UpdateAlarmHistory(AlarmManagement.GetHistory());
            }
            else
            {
                Transaction txn = new Transaction();
                switch (Node.Type)
                {
                    case "LoadPort":
                        
                        switch (Msg.Command)
                        {
                            case "MANSW":
                                if (RouteCtrl.GetMode().Equals("Auto"))
                                {
                                    ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command + " Trigger");
                                    txn.Method = Transaction.Command.LoadPortType.MappingLoad;
                                    Node.SendCommand(txn);
                                }
                                break;
                        }
                        break;
                }
            }
        }

        public void On_Node_State_Changed(Node Node, string Status)
        {
            logger.Debug("On_Node_State_Changed");
            NodeStatusUpdate.UpdateNodeState(Node.Name, Status);
            switch (Node.Name)
            {
                case "Robot01":
                case "Robot02":
                    ManualRobotStatusUpdate.UpdateRobotStatus(Node.Name, Status);//update 手動功能畫面
                    break;
                case "Aligner01":
                case "Aligner02":
                    ManualAlignerStatusUpdate.UpdateAlignerStatus(Node.Name, Status);//update 手動功能畫面
                    break;
            }
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

        public void On_Job_Location_Changed(Job Job)
        {
            JobMoveUpdate.UpdateJobMove(Job.Job_Id);
            WaferAssignUpdate.RefreshMapping(Job.LastNode);
            WaferAssignUpdate.RefreshMapping(Job.Position);
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
                DIO.Connect();
            }
            else
            {
                RouteCtrl.DisconnectAll();
                DIO.Close();
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


        public void On_Connection_Status_Report(string DIOName, string Status)
        {
            ConnectionStatusUpdate.UpdateControllerStatus(DIOName, Status);
        }


        public void On_Data_Chnaged(string Parameter, string Value)
        {
            DIOUpdate.UpdateDIOStatus(Parameter, Value);
        }

        public void On_Error_Occurred(string DIOName, string ErrorMsg)
        {
            //斷線 發ALARM
            logger.Debug("On_Error_Occurred");
            AlarmInfo CurrentAlarm = new AlarmInfo();
            CurrentAlarm.NodeName = "DIO";
            CurrentAlarm.AlarmCode = "00000002";
            CurrentAlarm.SystemAlarmCode = "FF00000002";
            CurrentAlarm.Desc = "連線異常";
            CurrentAlarm.TimeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");

            AlarmManagement.Add(CurrentAlarm);
            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
            AlarmUpdate.UpdateAlarmHistory(AlarmManagement.GetHistory());
        }





        private void Signal_MouseClick(object sender, MouseEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "Red_Signal":
                    if (DIO.GetIO("OUT","Red").ToUpper().Equals("TRUE"))
                    {
                        DIO.SetIO("Red", "False");
                    }
                    else
                    {
                        DIO.SetIO("Red", "True");
                    }
                    break;
                case "Orange_Signal":
                    if (DIO.GetIO("OUT", "Orange").ToUpper().Equals("TRUE"))
                    {
                        DIO.SetIO("Orange", "False");
                    }
                    else
                    {
                        DIO.SetIO("Orange", "True");
                    }
                    break;
                case "Green_Signal":
                    if (DIO.GetIO("OUT", "Green").ToUpper().Equals("TRUE"))
                    {
                        DIO.SetIO("Green", "False");
                    }
                    else
                    {
                        DIO.SetIO("Green", "True");
                    }
                    break;
                case "Blue_Signal":
                    if (DIO.GetIO("OUT", "Blue").ToUpper().Equals("TRUE"))
                    {
                        DIO.SetIO("Blue", "False");
                    }
                    else
                    {
                        DIO.SetIO("Blue", "True");
                    }
                    break;
                case "Buzzer1_Signal":
                    if (DIO.GetIO("OUT", "Buzzer1").ToUpper().Equals("TRUE"))
                    {
                        DIO.SetIO("Buzzer1", "False");
                    }
                    else
                    {
                        DIO.SetIO("Buzzer1", "True");
                    }
                    break;
                case "Buzzer2_Signal":
                    if (DIO.GetIO("OUT", "Buzzer2").ToUpper().Equals("TRUE"))
                    {
                        DIO.SetIO("Buzzer2", "False");
                    }
                    else
                    {
                        DIO.SetIO("Buzzer2", "True");
                    }
                    break;
            }
        }

        private void Conn_gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    switch (e.Value)
                    {
                        case "Connecting":
                            e.CellStyle.BackColor = Color.Yellow;
                            e.CellStyle.ForeColor = Color.Black;
                            break;
                        case "Connected":
                            e.CellStyle.BackColor = Color.Green;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        default:
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.White;
                            break;

                    }
                    break;

            }
        }
    }
}
