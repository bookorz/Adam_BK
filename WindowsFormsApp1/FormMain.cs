using SANWA.Utility;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TransferControl.Engine;
using TransferControl.Management;
using log4net.Config;
using Adam.UI_Update.Monitoring;
using log4net;
using Adam.UI_Update.Manual;
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
        FromAlarm alarmFrom = new FromAlarm();
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

            DIO.Connect();
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
                NodeManagement.Get("Robot01").ExcuteScript("RobotInit", "Initialize");
                NodeManagement.Get("Robot02").ExcuteScript("RobotInit", "Initialize");
                NodeManagement.Get("Aligner01").ExcuteScript("AlignerInit", "Initialize");
                NodeManagement.Get("Aligner02").ExcuteScript("AlignerInit", "Initialize");
                NodeManagement.Get("LoadPort01").ExcuteScript("LoadPortInit", "Initialize");
                NodeManagement.Get("LoadPort02").ExcuteScript("LoadPortInit", "Initialize");
                NodeManagement.Get("LoadPort03").ExcuteScript("LoadPortInit", "Initialize");
                NodeManagement.Get("LoadPort04").ExcuteScript("LoadPortInit", "Initialize");
            }
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            string strMsg = "Move to Home position. OK?";
            if (MessageBox.Show(strMsg, "Org.Back", MessageBoxButtons.OKCancel, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1, MessageBoxOptions.ServiceNotification) == DialogResult.OK)
            {
                Transaction txn = new Transaction();
                txn.Method = Transaction.Command.RobotType.RobotHome;
                NodeManagement.Get("Robot01").SendCommand(txn);
                txn = new Transaction();
                txn.Method = Transaction.Command.RobotType.RobotHome;
                NodeManagement.Get("Robot02").SendCommand(txn);
            }
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
            FormTerminal formTerminal = new FormTerminal();
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
            FromAlarm alarmFrom = new FromAlarm();
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
            switch (Txn.Method)
            {
                case Transaction.Command.RobotType.Reset:
                    AlarmManagement.Remove(Node.Name);
                    AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
                    break;
            }
            Transaction txn = new Transaction();

            switch (Node.Type)
            {
                case "LoadPort":
                    switch (Txn.Method)
                    {
                        case Transaction.Command.LoadPortType.GetMapping:

                            WaferAssignUpdate.UpdateLoadPortMapping(Node.Name, Msg.Value);

                            break;
                        case Transaction.Command.LoadPortType.GetLED:
                            if (RouteCtrl.GetMode().Equals("Start"))
                            {
                                if (Msg.Value.Length >= 13)
                                {
                                    //When Presence & Placement on
                                    if (Msg.Value[3] == '1' && Msg.Value[4] == '1')
                                    {
                                        Node.ExcuteScript("LoadPortFoupIn", "LoadPortFoup", true);
                                    }
                                    else
                                    {
                                        Node.ExcuteScript("LoadPortFoupOut", "LoadPortFoup", true);
                                    }
                                }
                            }
                            break;
                    }
                    break;
            }

            switch (Txn.FormName)
            {
                case "FormManual":
                    switch (Node.Type)
                    {
                        case "LoadPort":
                            if (!Txn.CommandType.Equals("MOV"))
                            {
                                ManualPortStatusUpdate.LockUI(false);
                            }
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
                                    //case Transaction.Command.RobotType.Stop:
                                    //case Transaction.Command.RobotType.Pause:
                                    //case Transaction.Command.RobotType.Continue:
                                    Thread.Sleep(1000);
                                    //向Robot 詢問狀態
                                    Node robot = NodeManagement.Get(Node.Name);
                                    robot.ExcuteScript("RobotStateGet", "FormManual");
                                    ManualRobotStatusUpdate.UpdateGUI(Txn, Node.Name, Msg.Value);//update 手動功能畫面
                                    break;
                                case Transaction.Command.RobotType.GetSpeed:
                                case Transaction.Command.RobotType.GetRIO:
                                case Transaction.Command.RobotType.GetError:
                                case Transaction.Command.RobotType.GetMode:
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
                                case Transaction.Command.AlignerType.Reset:
                                case Transaction.Command.AlignerType.AlignerServo:
                                    Thread.Sleep(500);
                                    //向Aligner 詢問狀態
                                    Node aligner = NodeManagement.Get(Node.Name);
                                    aligner.ExcuteScript("AlignerStateGet", "FormManual");
                                    ManualAlignerStatusUpdate.UpdateGUI(Txn, Node.Name, Msg.Value);//update 手動功能畫面
                                    break;
                                case Transaction.Command.AlignerType.GetMode:
                                case Transaction.Command.AlignerType.GetSV:
                                case Transaction.Command.AlignerType.GetStatus:
                                case Transaction.Command.AlignerType.GetSpeed:
                                case Transaction.Command.AlignerType.GetRIO:
                                case Transaction.Command.AlignerType.GetError:
                                    ManualAlignerStatusUpdate.UpdateGUI(Txn, Node.Name, Msg.Value);//update 手動功能畫面
                                    break;
                            }
                            break;
                        default:
                            AlarmManagement.Remove(Node.Name);
                            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());

                            break;
                    }
                    break;
                default:

                    break;
            }
        }

        public void On_Command_Error(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            switch (Txn.FormName)
            {
                case "FormManual":
                    switch (Node.Type)
                    {
                        case "LoadPort":
                            ManualPortStatusUpdate.LockUI(false);
                            break;

                    }
                    break;
            }
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
                CurrentAlarm.Type = Detail.Code_Type;
                CurrentAlarm.IsStop = Detail.IsStop;
                if (CurrentAlarm.IsStop)
                {
                    RouteCtrl.Stop();
                }
            }
            catch (Exception e)
            {
                CurrentAlarm.Desc = "未定義";
                logger.Error(Node.Controller + "-" + Node.AdrNo + "(GetAlarmMessage)" + e.Message + "\n" + e.StackTrace);
            }
            CurrentAlarm.TimeStamp = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss:fff");

            AlarmManagement.Add(CurrentAlarm);
           
            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
            AlarmUpdate.UpdateAlarmHistory(AlarmManagement.GetHistory());

        }

        public void On_Command_Finished(Node Node, Transaction Txn, ReturnMessage Msg)
        {
            logger.Debug("On_Command_Finished");
            //Transaction txn = new Transaction();
            switch (Txn.FormName)
            {
                case "FormManual":

                    switch (Node.Type)
                    {
                        case "LoadPort":

                            ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command + " Finished");
                            ManualPortStatusUpdate.LockUI(false);
                            switch (Txn.Method)
                            {
                                case Transaction.Command.LoadPortType.Unload:
                                case Transaction.Command.LoadPortType.MappingUnload:
                                case Transaction.Command.LoadPortType.UnDock:
                                    WaferAssignUpdate.UpdateLoadPortMapping(Node.Name, "");
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


            Transaction txn = new Transaction();
            switch (Node.Type)
            {
                case "LoadPort":
                    ManualPortStatusUpdate.UpdateLog(Node.Name, Msg.Command + " Trigger");
                    switch (Msg.Command)
                    {
                        case "MANSW":
                            if (RouteCtrl.GetMode().Equals("Start"))
                            {
                                Node.ExcuteScript("LoadPortMapping", "MANSW", true);
                            }
                            break;
                        case "PODON":
                            if (RouteCtrl.GetMode().Equals("Start"))
                            {

                                Node.ExcuteScript("LoadPortFoupIn", "LoadPortFoup", true);

                            }
                            break;
                        case "PODOF":
                            if (RouteCtrl.GetMode().Equals("Start"))
                            {
                                Node.ExcuteScript("LoadPortFoupOut", "LoadPortFoup", true);
                            }
                            break;
                    }
                    break;
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
            if (ControllerManagement.CheckAllConnection())
            {
                ConnectionStatusUpdate.UpdateOnlineStatus("Online");
            }
            logger.Debug("On_Controller_State_Changed");
        }

        public void On_Port_Finished(string PortName)
        {
            logger.Debug("On_Port_Finished");
            Transaction txn = new Transaction();
            txn.Method = Transaction.Command.LoadPortType.Unload;
            NodeManagement.Get(PortName).SendCommand(txn, true);
        }

        public void On_Job_Location_Changed(Job Job)
        {
            JobMoveUpdate.UpdateJobMove(Job.Job_Id);
            WaferAssignUpdate.RefreshMapping(Job.LastNode);
            WaferAssignUpdate.RefreshMapping(Job.Position);
        }

        public void On_Script_Finished(Node Node, string ScriptName, string FormName)
        {
            logger.Debug("On_Script_Finished: " + Node.Name + " Script:" + ScriptName + " Finished, Form name:" + FormName);
            switch (FormName)
            {
                case "FormManual-Script":

                    switch (Node.Type)
                    {
                        case "Robot":
                            ManualRobotStatusUpdate.UpdateGUI(new Transaction(), Node.Name, "");//update 手動功能畫面
                            break;
                    }
                    break;
            }
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
            CurrentAlarm.AlarmType = "System";
            AlarmManagement.Add(CurrentAlarm);
            AlarmUpdate.UpdateAlarmList(AlarmManagement.GetAll());
            AlarmUpdate.UpdateAlarmHistory(AlarmManagement.GetHistory());
        }





        private void Signal_MouseClick(object sender, MouseEventArgs e)
        {
            switch ((sender as Button).Name)
            {
                case "Red_Signal":
                    if (DIO.GetIO("OUT", "Red").ToUpper().Equals("TRUE"))
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

        private void Connection_btn_Click(object sender, EventArgs e)
        {
            
            if (Connection_btn.Tag.ToString() == "Offline")
            {
                RouteCtrl.ConnectAll();

                ConnectionStatusUpdate.UpdateOnlineStatus("Connecting");
            }
            else
            {
                RouteCtrl.DisconnectAll();
                ConnectionStatusUpdate.UpdateOnlineStatus("Offline");
            }

        }

        private void Mode_btn_Click(object sender, EventArgs e)
        {
            
            if (Mode_btn.Tag.ToString() == "Manual")
            {
               
                if (Connection_btn.Tag.ToString() == "Offline")
                {
                    MessageBox.Show("尚未連線");
                    return;
                }

                btnMaintence.Text = "Start Mode";
                btnMaintence.BackColor = Color.Red;
                btnMaintence.Enabled = false;
                btnTeach.Enabled = false;
                EnablePage(tbcMian.TabPages[5], false);

                Node Aligner1 = NodeManagement.Get("Aligner01");
                if (Aligner1 != null)
                {
                    Aligner1.LockByNode = "LoadPort01";
                }
                if (AlarmManagement.HasCritical())
                {
                    MessageBox.Show("關鍵Alarm尚未解除");
                }
                else
                {
                    NodeStatusUpdate.UpdateCurrentState();
                    ThreadPool.QueueUserWorkItem(new WaitCallback(RouteCtrl.Start), "Normal");
                    Mode_btn.Tag = "Start";
                    Mode_btn.BackColor = Color.Lime;
                }
            }
            else
            {
                btnMaintence.Text = "Maintenance Mode";
                btnMaintence.BackColor = Color.WhiteSmoke;
                btnMaintence.Enabled = true;
                btnTeach.Enabled = true;
                EnablePage(hiddenPages[0], true);
                RouteCtrl.Stop();
                Mode_btn.Tag = "Manual";
                Mode_btn.BackColor = Color.Orange;
            }
        }
    }
}
