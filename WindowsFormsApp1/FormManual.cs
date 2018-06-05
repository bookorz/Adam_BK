using Adam;
using Adam.UI_Update.Manual;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Management;

namespace GUI
{
    public partial class FormManual : Form
    {
        Boolean isRobotMoveDown = false;//Get option 1
        Boolean isRobotMoveUp = false;//Put option 1
        public FormManual()
        {
            InitializeComponent();

        }

        private void FormManual_Load(object sender, EventArgs e)
        {
            Initialize();
            Update_Manual_Status();
        }

        public void Initialize()
        {
            foreach (Node port in NodeManagement.GetLoadPortList())
            {
                if (Cb_LoadPortSelect.Text.Equals(""))
                {
                    Cb_LoadPortSelect.Text = port.Name;
                }
                Cb_LoadPortSelect.Items.Add(port.Name);
            }
            ManualPortStatusUpdate.UpdateMapping(Cb_LoadPortSelect.Text, "?????????????????????????");
        }

        private void PortFunction_Click(object sender, EventArgs e)
        {
            Node port = NodeManagement.Get(Cb_LoadPortSelect.Text);
            Transaction txn = new Transaction();
            txn.FormName = "FormManual";
            if (port == null)
            {
                MessageBox.Show(Cb_LoadPortSelect.Text + " can't found!");
                return;
            }
            Button btn = (Button)sender;
            switch (btn.Name)
            {
                case "Btn_LOAD_A":
                    if (ChkWithSlotMap_A.Checked)
                    {
                        txn.Method = Transaction.Command.LoadPortType.MappingLoad;
                    }
                    else
                    {
                        txn.Method = Transaction.Command.LoadPortType.Load;
                    }
                    break;
                case "Btn_UNLOAD_A":
                    if (ChkWithSlotMap_A.Checked)
                    {
                        txn.Method = Transaction.Command.LoadPortType.MappingUnload;
                    }
                    else
                    {
                        txn.Method = Transaction.Command.LoadPortType.Unload;
                    }
                    break;
                case "Btn_Reset_A":
                    txn.Method = Transaction.Command.LoadPortType.Reset;
                    break;
                case "Btn_Initialize_A":
                    txn.Method = Transaction.Command.LoadPortType.InitialPos;
                    break;
                case "Btn_ForceInitial_A":
                    txn.Method = Transaction.Command.LoadPortType.ForceInitialPos;
                    break;
                case "Btn_UnClamp_A":
                    txn.Method = Transaction.Command.LoadPortType.UnClamp;
                    break;
                case "Btn_Clamp_A":
                    txn.Method = Transaction.Command.LoadPortType.Clamp;
                    break;
                case "Btn_UnDock_A":
                    txn.Method = Transaction.Command.LoadPortType.UnDock;
                    break;
                case "Btn_Dock_A":
                    txn.Method = Transaction.Command.LoadPortType.Dock;
                    break;
                case "Btn_VacuumOFF_A":
                    txn.Method = Transaction.Command.LoadPortType.VacuumOFF;
                    break;
                case "Btn_VacuumON_A":
                    txn.Method = Transaction.Command.LoadPortType.VacuumON;
                    break;
                case "Btn_LatchDoor_A":
                    txn.Method = Transaction.Command.LoadPortType.LatchDoor;
                    break;
                case "Btn_UnLatchDoor_A":
                    txn.Method = Transaction.Command.LoadPortType.UnLatchDoor;
                    break;
                case "Btn_DoorClose_A":
                    txn.Method = Transaction.Command.LoadPortType.DoorClose;
                    break;
                case "Btn_DoorOpen_A":
                    txn.Method = Transaction.Command.LoadPortType.DoorOpen;
                    break;
                case "Btn_DoorDown_A":
                    txn.Method = Transaction.Command.LoadPortType.DoorDown;
                    break;
                case "Btn_DoorUp_A":
                    txn.Method = Transaction.Command.LoadPortType.DoorUp;
                    break;
                case "Btn_ReadLED_A":
                    txn.Method = Transaction.Command.LoadPortType.GetLED;
                    break;
                case "Btn_ReadVersion_A":
                    txn.Method = Transaction.Command.LoadPortType.ReadVersion;
                    break;
                case "Btn_ReadStatus_A":
                    txn.Method = Transaction.Command.LoadPortType.ReadStatus;
                    break;
                case "Btn_Map_A":
                    txn.Method = Transaction.Command.LoadPortType.GetMapping;
                    break;
                case "Btn_MapperWaitPosition_A":
                    txn.Method = Transaction.Command.LoadPortType.MapperWaitPosition;
                    break;
                case "Btn_MapperStartPosition_A":
                    txn.Method = Transaction.Command.LoadPortType.MapperStartPosition;
                    break;
                case "Btn_MapperArmRetracted_A":
                    txn.Method = Transaction.Command.LoadPortType.MapperArmRetracted;
                    break;
                case "Btn_MapperArmStretch_A":
                    txn.Method = Transaction.Command.LoadPortType.MapperArmStretch;
                    break;
                case "Btn_MappingDown_A":
                    txn.Method = Transaction.Command.LoadPortType.MappingDown;
                    break;
            }
            if (!txn.Method.Equals(""))
            {
                ManualPortStatusUpdate.LockUI(true);
                port.SendCommand(txn);
            }
            else
            {
                MessageBox.Show("Command is empty!");
            }
        }

        private void Btn_ClearSlotResult_A_Click(object sender, EventArgs e)
        {
            ManualPortStatusUpdate.UpdateMapping(Cb_LoadPortSelect.Text, "?????????????????????????");
        }

        private void Btn_ClearMSG_A_Click(object sender, EventArgs e)
        {
            RTxt_Message_A.Clear();
        }

        private void Cb_LoadPortSelect_TextUpdate(object sender, EventArgs e)
        {
            LblLED_A.Text = "";
            LblVersion_A.Text = "";
            LblStatus_A.Text = "";
            RTxt_Message_A.Clear();
            ManualPortStatusUpdate.UpdateMapping(Cb_LoadPortSelect.Text, "?????????????????????????");
            for (int i = 0; i < 19; i++)
            {
                string Idx = (i + 1).ToString("00");
                Label StsLb = this.Controls.Find("Lab_StateCode_" + Idx + "_A", true).FirstOrDefault() as Label;
                StsLb.Text = "";
            }

        }


        private void AlignerFunction_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            String nodeName = "NA";
            String angle = "0";
            string speed = "0";
            if (btn.Name.IndexOf("A1") > 0)
            {
                nodeName = "Aligner01";
                if (cbA1Angle.Text.Equals(""))
                {
                    cbA1Angle.Text = "0";
                }
                if (udA1AngleOffset.Text.Equals(""))
                {
                    udA1AngleOffset.Text = "0";
                }
                angle = Convert.ToString(int.Parse(cbA1Angle.Text) + int.Parse(udA1AngleOffset.Text));
                speed = nudA1Speed.Text.Equals("100") ? "0" : nudA1Speed.Text;
            }
            if (btn.Name.IndexOf("A2") > 0)
            {
                nodeName = "Aligner02";
                if (cbA2Angle.Text.Equals(""))
                {
                    cbA2Angle.Text = "0";
                }
                if (udA2AngleOffset.Text.Equals(""))
                {
                    udA2AngleOffset.Text = "0";
                }
                angle = Convert.ToString(int.Parse(cbA2Angle.Text) + int.Parse(udA2AngleOffset.Text));
                speed = nudA2Speed.Text.Equals("100") ? "0" : nudA2Speed.Text;
            };
            Node aligner = NodeManagement.Get(nodeName);
            Transaction[] txns = new Transaction[1];
            txns[0] = new Transaction();
            txns[0].FormName = "FormManual";
            if (aligner == null)
            {
                MessageBox.Show(nodeName + " can't found!");
                return;
            }
            String btnFuncName = btn.Name.Replace("A1", "").Replace("A2", ""); // A1 , A2 共用功能
            if (!btnFuncName.Equals("btnConn") && !btnFuncName.Equals("btnDisConn"))
            {
                string status = "";
                if (nodeName.Equals("Aligner01"))
                {
                    status = tbA1Status.Text;
                }
                if (nodeName.Equals("Aligner02"))
                {
                    status = tbA2Status.Text;
                }
                switch (status)
                {
                    case "Disconnected":
                    case "N/A":
                    case "":
                        MessageBox.Show("Please connect first.", "Error");
                        return;
                    default:
                        break;
                }
            }
            switch (btnFuncName)
            {
                case "btnConn":
                    ControllerManagement.Get(aligner.Controller).Connect();
                    aligner.State = "";
                    SetFormEnable(false);
                    Thread.Sleep(500);//暫解
                    setAlignerStatus();
                    SetFormEnable(true);
                    return;
                case "btnDisConn":
                    ControllerManagement.Get(aligner.Controller).Close();
                    aligner.State = "";
                    SetFormEnable(false);
                    Thread.Sleep(500);//暫解
                    setAlignerStatus();
                    SetFormEnable(true);
                    return;
                case "btnInit":
                    //txns = new Transaction[4];
                    //txns[0].Method = Transaction.Command.AlignerType.Reset;
                    //txns[1].Method = Transaction.Command.AlignerType.AlignerOrigin;
                    //txns[2].Method = Transaction.Command.AlignerType.AlignerServo;
                    //txns[3].Method = Transaction.Command.AlignerType.AlignerHome;
                    break;
                case "btnOrg":
                    txns[0].Method = Transaction.Command.AlignerType.AlignerOrigin;
                    break;
                case "btnHome":
                    txns[0].Method = Transaction.Command.AlignerType.AlignerHome;
                    break;
                case "btnServoOn":
                    txns[0].Method = Transaction.Command.AlignerType.AlignerServo;
                    txns[0].Arm = "1";
                    break;
                case "btnServoOff":
                    txns[0].Method = Transaction.Command.AlignerType.AlignerServo;
                    txns[0].Arm = "0";
                    break;
                case "btnVacuOn":
                    txns[0].Method = Transaction.Command.AlignerType.WaferHold;
                    txns[0].Arm = "1";
                    break;
                case "btnVacuOff":
                    txns[0].Method = Transaction.Command.AlignerType.WaferRelease;
                    txns[0].Arm = "1";
                    break;
                case "btnChgSpeed":
                    txns[0].Method = Transaction.Command.AlignerType.AlignerSpeed;
                    txns[0].Arm = speed;
                    break;
                case "btnReset":
                    txns[0].Method = Transaction.Command.AlignerType.Reset;
                    break;
                case "btnAlign":
                    txns[0].Method = Transaction.Command.AlignerType.Align;                   
                    txns[0].Value = angle;
                    break;
                case "btnChgMode":
                    int mode = -1;
                    if (btn.Name.IndexOf("A1") > 0)
                    {
                        mode = cbA1Mode.SelectedIndex;
                    }
                    if (btn.Name.IndexOf("A2") > 0)
                    {
                        mode = cbA2Mode.SelectedIndex;
                    }
                    if (mode < 0)
                    {
                        MessageBox.Show(" Insufficient information, please select mode!", "Invalid Mode");
                        return;
                    }
                    txns[0].Method = Transaction.Command.AlignerType.AlignerMode;
                    txns[0].Arm = Convert.ToString(mode);
                    break;
            }
            if (!txns[0].Method.Equals(""))
            {
                aligner.SendCommand(txns[0]);
            }
            else
            {
                MessageBox.Show("Command is empty!");
            }
            SetFormEnable(false);
            setAlignerStatus();
        }

        private void SetFormEnable(bool enable)
        {
            if (enable)
            {
                this.Cursor = Cursors.Default;
                tbcManual.Enabled = true;
                pnlMotionStop.Visible = false;
            }
            else
            {
                this.Cursor = Cursors.WaitCursor;
                tbcManual.Enabled = false;
            }

        }

        private void RobotFunction_Click(object sender, EventArgs e)
        {

            Button btn = (Button)sender;
            if (!btn.Name.Equals("btnRConn") && !btn.Name.Equals("btnRConn"))
            {
                switch (tbRStatus.Text)
                {
                    case "Disconnected":
                    case "N/A":
                    case "":
                        MessageBox.Show("Please connect first.", "Error");
                        return;
                    default:
                        break;
                }
            }
            String nodeName = rbR1.Checked ? "Robot01" : "Robot02";
            Node robot = NodeManagement.Get(nodeName);
            Transaction[] txns = new Transaction[1];
            txns[0] = new Transaction();
            txns[0].FormName = "FormManual";
            if (!btn.Name.Equals("btnRStop"))
            {
                pnlMotionStop.Visible = true;
            }
            switch (btn.Name)
            {
                case "btnRConn":
                    try
                    {
                        ControllerManagement.Get(robot.Controller).Connect();
                        robot.State = "";
                        SetFormEnable(false);
                        Thread.Sleep(500);//暫解
                        setRobotStatus();
                        SetFormEnable(true);
                    }
                    catch (Exception e1)
                    {

                    }

                    return;
                case "btnRDisConn":
                    try
                    {
                        ControllerManagement.Get(robot.Controller).Close();
                        robot.State = "";
                        SetFormEnable(false);
                        Thread.Sleep(500);//暫解
                        setRobotStatus();
                        SetFormEnable(true);
                    }
                    catch (Exception e1)
                    {

                    }

                    return;
                case "btnRInit":
                    //txns[0].Method = Transaction.Command.LoadPortType.MappingDown;
                    isRobotMoveDown = false;//Get option 1
                    isRobotMoveUp = false;//Put option 1
                    break;
                case "btnROrg":
                    txns[0].Method = Transaction.Command.RobotType.RobotHome;
                    isRobotMoveDown = false;//Get option 1
                    isRobotMoveUp = false;//Put option 1
                    break;
                case "btnRHome":
                    txns[0].Method = Transaction.Command.RobotType.RobotHome;
                    isRobotMoveDown = false;//Get option 1
                    isRobotMoveUp = false;//Put option 1
                    break;
                case "btnRChgSpeed":
                    txns[0].Method = Transaction.Command.RobotType.RobotSpeed;
                    txns[0].Arm = nudRSpeed.Text.Equals("100") ? "0" : nudRSpeed.Text;
                    break;
                //上臂
                case "btnRRVacuOn":
                    txns[0].Method = Transaction.Command.RobotType.WaferHold;
                    txns[0].Arm = "1";
                    break;
                case "btnRRVacuOff":
                    txns[0].Method = Transaction.Command.RobotType.WaferRelease;
                    txns[0].Arm = "1";
                    break;
                //下臂
                case "btnRLVacuOn":
                    txns[0].Method = Transaction.Command.RobotType.WaferHold;
                    txns[0].Arm = "2";
                    break;
                case "btnRLVacuOff":
                    txns[0].Method = Transaction.Command.RobotType.WaferRelease;
                    txns[0].Arm = "2";
                    break;
                case "btnRGet":
                    if (cbRA1Point.Text == "" || cbRA1Slot.Text == "" || cbRA1Arm.Text == "")
                    {
                        MessageBox.Show(" Insufficient information, please select source!", "Invalid source");
                        return;
                    }
                    if (isRobotMoveDown)
                        txns[0].Method = Transaction.Command.RobotType.GetAfterWait;
                    else
                        txns[0].Method = Transaction.Command.RobotType.Get;
                    txns[0].Point = ConfigUtil.GetStagePoint(cbRA1Point.Text);
                    txns[0].Arm = ConfigUtil.GetArmID(cbRA1Arm.Text);
                    txns[0].Slot = cbRA1Slot.Text;
                    isRobotMoveDown = false;//Get option 1
                    isRobotMoveUp = false;//Put option 1
                    break;
                case "btnRPut":
                    if (cbRA2Point.Text == "" || cbRA2Slot.Text == "" || cbRA2Arm.Text == "")
                    {
                        MessageBox.Show(" Insufficient information, please select destination!", "Invalid destination");
                        return;
                    }
                    if (isRobotMoveUp)
                        txns[0].Method = Transaction.Command.RobotType.PutBack;
                    else
                        txns[0].Method = Transaction.Command.RobotType.Put;
                    txns[0].Point = ConfigUtil.GetStagePoint(cbRA2Point.Text);
                    txns[0].Arm = ConfigUtil.GetArmID(cbRA2Arm.Text);
                    txns[0].Slot = cbRA2Slot.Text;
                    isRobotMoveDown = false;//Get option 1
                    isRobotMoveUp = false;//Put option 1
                    break;
                case "btnRGetWait":
                    if (cbRA1Point.Text == "" || cbRA1Slot.Text == "" || cbRA1Arm.Text == "")
                    {
                        MessageBox.Show(" Insufficient information, please select source!", "Invalid source");
                        return;
                    }
                    txns[0].Method = Transaction.Command.RobotType.GetWait;
                    txns[0].Point = ConfigUtil.GetStagePoint(cbRA1Point.Text);
                    txns[0].Arm = ConfigUtil.GetArmID(cbRA1Arm.Text);
                    txns[0].Slot = cbRA1Slot.Text;
                    break;
                case "btnRPutWait":
                    if (cbRA2Point.Text == "" || cbRA2Slot.Text == "" || cbRA2Arm.Text == "")
                    {
                        MessageBox.Show(" Insufficient information, please select destination!", "Invalid destination");
                        return;
                    }
                    txns[0].Method = Transaction.Command.RobotType.PutWait;
                    txns[0].Point = ConfigUtil.GetStagePoint(cbRA2Point.Text);
                    txns[0].Arm = ConfigUtil.GetArmID(cbRA2Arm.Text);
                    txns[0].Slot = cbRA2Slot.Text;
                    break;
                case "btnRMoveDown":
                    isRobotMoveDown = true;
                    txns[0].Method = Transaction.Command.RobotType.WaitBeforeGet;//GET option 1
                    txns[0].Point = ConfigUtil.GetStagePoint(cbRA1Point.Text);
                    txns[0].Arm = ConfigUtil.GetArmID(cbRA1Arm.Text);
                    txns[0].Slot = cbRA1Slot.Text;
                    break;
                case "btnRMoveUp":
                    isRobotMoveUp = true;
                    txns[0].Method = Transaction.Command.RobotType.WaitBeforePut;//Put option 1
                    txns[0].Point = ConfigUtil.GetStagePoint(cbRA2Point.Text);
                    txns[0].Arm = ConfigUtil.GetArmID(cbRA2Arm.Text);
                    txns[0].Slot = cbRA2Slot.Text;
                    break;
                case "btnRChgMode":
                    if (cbRMode.SelectedIndex < 0)
                    {
                        MessageBox.Show(" Insufficient information, please select mode!", "Invalid Mode");
                        return;
                    }
                    txns[0].Method = Transaction.Command.RobotType.RobotMode;
                    txns[0].Arm = Convert.ToString(cbRMode.SelectedIndex);
                    break;

                case "btnRPutPut":
                    //txns[0].Method = Transaction.Command.RobotType.MappingDown;
                    break;
                case "btnRGetGet":
                    //txns[0].Method = Transaction.Command.RobotType.MappingDown;
                    break;
                case "btnRGetPut":
                    //txns[0].Method = Transaction.Command.RobotType.MappingDown;
                    break;
                case "btnRPutGet":
                    //txns[0].Method = Transaction.Command.RobotType.MappingDown;
                    break;
                case "btnRReset":
                    txns[0].Method = Transaction.Command.RobotType.Reset;
                    break;
                case "btnRServoOn":
                    txns[0].Method = Transaction.Command.RobotType.RobotServo;
                    txns[0].Arm = "1";
                    break;
                case "btnRServoOff":
                    txns[0].Method = Transaction.Command.RobotType.RobotServo;
                    txns[0].Arm = "0";
                    break;
                case "btnRStop":
                    txns[0].Method = Transaction.Command.RobotType.Stop;
                    txns[0].Value = "1";//立即停止
                    txns[0].Value = "0";//減速停止
                    SetFormEnable(true);
                    break;
                case "btnRPause":
                    txns[0].Method = Transaction.Command.RobotType.Pause;
                    break;
                case "btnRContinue":
                    txns[0].Method = Transaction.Command.RobotType.Continue;
                    break;
            }
            if (!txns[0].Method.Equals(""))
            {
                robot.SendCommand(txns[0]);
            }
            else
            {
                MessageBox.Show("Command is empty!");
            }
            SetFormEnable(false);
            Update_Manual_Status();
        }
        private void setRobotStatus()
        {
            Control[] controls = new Control[] { tbRError, tbRLVacuSolenoid, tbRLwaferSensor, tbRRVacuSolenoid, tbRRwaferSensor, tbRServo, nudRSpeed, tbRStatus };
            foreach (Control control in controls)
            {
                control.Text = "";
                control.BackColor = Color.WhiteSmoke;
            }
            String nodeName = rbR1.Checked ? "Robot01" : "Robot02";
            SetDeviceStatus(nodeName);
            if (tbRStatus.Text.Equals("N/A") || tbRStatus.Text.Equals("Disconnected") || tbRStatus.Text.Equals("") || tbRStatus.Text.Equals("Connecting"))
            {
                return;//連線狀態下才執行
            }
            //向Robot 詢問狀態
            Node robot = NodeManagement.Get(nodeName);
            Transaction[] txns = new Transaction[6];
            txns[0] = new Transaction();
            txns[0].Method = Transaction.Command.RobotType.GetStatus;

            txns[1] = new Transaction();
            txns[1].Method = Transaction.Command.RobotType.GetSpeed;

            txns[2] = new Transaction();
            txns[2].Method = Transaction.Command.RobotType.GetMode;

            txns[3] = new Transaction();
            txns[3].Method = Transaction.Command.RobotType.GetError;
            txns[3].Value = "00";// 履歷號碼  2 位數  10 進位, 00最新

            txns[4] = new Transaction();
            txns[4].Method = Transaction.Command.RobotType.GetRIO;
            txns[4].Value = "4";//4 R-Hold Status 回饋 R 軸 Wafer/ Panel 保留狀態

            txns[5] = new Transaction();
            txns[5].Method = Transaction.Command.RobotType.GetSV;
            txns[5].Value = "1";



            foreach (Transaction txn in txns)
            {
                if (!txn.Method.Equals(""))
                {
                    txn.FormName = "FormManual";
                    robot.SendCommand(txn);
                }
                else
                {
                    MessageBox.Show("Command is empty!");
                }
            }

        }


        private void setAlignerStatus()
        {
            Control[] controls = new Control[] { tbA1Error, tbA1Servo, tbA1Status, tbA1VacSolenoid, tbA1WaferSensor, tbA1WaferSensor, tbA2Error, tbA2Servo, tbA2Status, tbA2VacSolenoid, tbA2WaferSensor, tbA2WaferSensor, nudA1Speed, nudA2Speed };
            foreach (Control control in controls)
            {
                control.Text = "";
                control.BackColor = Color.WhiteSmoke;
            }
            SetDeviceStatus("Aligner01");
            SetDeviceStatus("Aligner02");
            Node aligner1 = NodeManagement.Get("Aligner01");
            Node aligner2 = NodeManagement.Get("Aligner02");
            Transaction[] txns = new Transaction[6];
            txns[0] = new Transaction();
            txns[0].Method = Transaction.Command.AlignerType.GetStatus;

            txns[1] = new Transaction();
            txns[1].Method = Transaction.Command.AlignerType.GetSpeed;
            
            txns[2] = new Transaction();
            txns[2].Method = Transaction.Command.AlignerType.GetMode;

            txns[3] = new Transaction();
            txns[3].Method = Transaction.Command.AlignerType.GetError;
            txns[3].Value = "00";// 履歷號碼  2 位數  10 進位, 00最新
            
            txns[4] = new Transaction();
            txns[4].Method = Transaction.Command.AlignerType.GetRIO;
            txns[4].Value = "4";// 4 Hold Status 回饋 Wafer/ Panel 保留狀態

            txns[5] = new Transaction();
            txns[5].Method = Transaction.Command.AlignerType.GetSV;
            txns[5].Value = "1";// 4 Hold Status 回饋 Wafer/ Panel 保留狀態

            foreach (Transaction txn in txns)
            {
                if (!txn.Method.Equals(""))
                {
                    txn.FormName = "FormManual";
                    if (!tbA1Status.Text.Equals("N/A") && !tbA1Status.Text.Equals("Disconnected") && !tbA1Status.Text.Equals(""))
                    {
                        aligner1.SendCommand(txn);//連線狀態下才執行
                    }
                    if (!tbA2Status.Text.Equals("N/A") && !tbA2Status.Text.Equals("Disconnected") && !tbA2Status.Text.Equals(""))
                    {
                        aligner2.SendCommand(txn);//連線狀態下才執行
                    }

                }
                else
                {
                    MessageBox.Show("Command is empty!");
                }
            }
        }

        private void SetDeviceStatus(string name)
        {
            Node node = NodeManagement.Get(name);
            string status = node.State != "" ? node.State : "N/A";
            if (status.Equals("N/A") && ControllerManagement.Get(node.Controller) != null)
                status = ControllerManagement.Get(node.Controller).Status;// 如果 NODE 無狀態，改抓 Controller 的狀態     
            TextBox tbStatus = null;
            switch (name)
            {
                case "Robot01":
                case "Robot02":
                    tbStatus = tbRStatus;
                    break;
                case "Aligner01":
                    tbStatus = tbA1Status;
                    break;
                case "Aligner02":
                    tbStatus = tbA2Status;
                    break;
            }
            if (tbStatus == null)
                return;
            tbStatus.Text = status;
            Color color = new Color();
            switch (tbStatus.Text)
            {
                case "RUN":
                    color = Color.Lime;
                    break;
                case "IDLE":
                    color = Color.Yellow;
                    break;
                case "Connected":
                    color = Color.LightBlue;
                    break;
                case "Connecting":
                    color = Color.LightSeaGreen;
                    break;
                case "Disconnected":
                case "N/A":
                    color = Color.LightGray;
                    break;
            }
            tbStatus.BackColor = color;
        }

        private void FormManual_EnabledChanged(object sender, EventArgs e)
        {
            Update_Manual_Status();
        }

        private void rb_CheckedChanged(object sender, EventArgs e)
        {
            Update_Manual_Status();
        }

        private void tbcManual_SelectedIndexChanged(object sender, EventArgs e)
        {
            Update_Manual_Status();
        }

        private void Update_Manual_Status()
        {
            if (tbcManual.SelectedTab.Text.Equals("Robot"))
                setRobotStatus();
            if (tbcManual.SelectedTab.Text.Equals("Aligner"))
                setAlignerStatus();
        }
    }
}
