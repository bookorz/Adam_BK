using Adam.Util;
using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.UI_Update.Manual
{
    class ManualRobotStatusUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(ManualRobotStatusUpdate));
        //delegate void ShowMessage(string str);
        delegate void UpdateGUI_D(Transaction txn, string name, string msg);
        delegate void UpdateStatus_D(string device);
        delegate void UpdateRobotStatus_D(string name, string status);

        public static void UpdateGUI(Transaction txn, string name, string msg)
        {
            try
            {
                string method = txn.Method;
                Form manual = Application.OpenForms["FormManual"];

                if (manual == null)
                    return;

                if (manual.InvokeRequired)
                {
                    UpdateGUI_D ph = new UpdateGUI_D(UpdateGUI);
                    manual.BeginInvoke(ph, txn, name, msg);
                }
                else
                {
                    switch (method)
                    {
                        case Transaction.Command.RobotType.GetStatus:
                            StateUtil.UpdateSTS(name, msg);
                            break;
                        case Transaction.Command.RobotType.GetSpeed:
                            StateUtil.UpdateSP(name, msg);
                            break;
                        case Transaction.Command.RobotType.GetRIO:
                            StateUtil.UpdateRIO(name, msg);
                            break;
                        case Transaction.Command.RobotType.GetError:
                            StateUtil.UpdateError(name, msg);
                            break;
                        default:
                            manual.Cursor = Cursors.Default;
                            Control tbcManual = manual.Controls.Find("tbcManual", true).FirstOrDefault() as Control;
                            tbcManual.Enabled = true;
                            break;
                    }
                    UpdateStatus(name);
                    if (method.Equals(Transaction.Command.RobotType.GetRIO))
                    {
                        Transaction next_txn = new Transaction();
                        next_txn = new Transaction();
                        switch (txn.Value)
                        {
                            case "4":
                                next_txn.Method = Transaction.Command.RobotType.GetRIO;
                                next_txn.Value = "5";//5 L-Hold Status 回饋 L 軸 Wafer/ Panel 保留狀態
                                break;
                            case "5":
                                next_txn.Method = Transaction.Command.RobotType.GetRIO;
                                next_txn.Value = "8";//8:回饋 R 軸在席 Sensor 的狀態 
                                break;
                            case "8":
                                next_txn.Method = Transaction.Command.RobotType.GetRIO;
                                next_txn.Value = "9";//9:回饋 L 軸在席 Sensor 的狀態
                                break;
                        }
                        if (!next_txn.Method.Equals(""))
                        {
                            next_txn.FormName = "FormManual";
                            Node robot = NodeManagement.Get(name);
                            robot.SendCommand(next_txn);
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                    if (method.Equals(Transaction.Command.RobotType.RobotServo))
                    {
                        Transaction next_txn = new Transaction();
                        next_txn = new Transaction();
                        next_txn.Method = Transaction.Command.RobotType.GetStatus;
                        if (!next_txn.Method.Equals(""))
                        {
                            next_txn.FormName = "FormManual";
                            Node robot = NodeManagement.Get(name);
                            robot.SendCommand(next_txn);
                        }
                        else
                        {
                            //do nothing
                        }
                    }
                }
            }
            catch (Exception e)
            {
                logger.Error("Robot Manual Function: fail.");
                Console.WriteLine("Exception caught: {0}", e);
            }
        }

        public static void UpdateStatus(string device)
        {
            Form manual = Application.OpenForms["FormManual"];

            if (manual == null)
                return;

            if (manual.InvokeRequired)
            {
                UpdateStatus_D ph = new UpdateStatus_D(UpdateStatus);
                manual.BeginInvoke(ph, device);
            }
            else
            {
                StateUtil.Init();
                RobotState robot = StateUtil.GetDeviceState(device) != null ? (RobotState)StateUtil.GetDeviceState(device) : null;
                if (robot == null)
                {
                    return;
                }
                
                Control nudRSpeed = manual.Controls.Find("nudRSpeed", true).FirstOrDefault() as Control;
                if (nudRSpeed != null)
                {
                    nudRSpeed.Text = robot.Speed.Equals("00") ? "100" : Int32.Parse(robot.Speed).ToString(); 
                }
                Control tbRRwaferSensor = manual.Controls.Find("tbRRwaferSensor", true).FirstOrDefault() as Control;
                if (tbRRwaferSensor != null)
                {
                    tbRRwaferSensor.Text = robot.Present_R.Equals("1") ? "ON" : "OFF";
                    Color color = new Color();
                    switch (tbRRwaferSensor.Text)
                    {
                        case "OFF":
                            color = Color.MintCream;
                            break;
                        case "ON":
                            color = Color.LightGreen;
                            break;
                        default:
                            color = Color.White;
                            break;
                    }
                    tbRRwaferSensor.BackColor = color;
                }
                Control tbRRVacuSolenoid = manual.Controls.Find("tbRRVacuSolenoid", true).FirstOrDefault() as Control;
                if (tbRRVacuSolenoid != null)
                {
                    tbRRVacuSolenoid.Text = robot.Vacuum_R.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                    switch (tbRRVacuSolenoid.Text)
                    {
                        case "OFF":
                            color = Color.MintCream;
                            break;
                        case "ON":
                            color = Color.LightGreen;
                            break;
                        default:
                            color = Color.White;
                            break;
                    }
                    tbRRVacuSolenoid.BackColor = color;
                }
                Control tbRLwaferSensor = manual.Controls.Find("tbRLwaferSensor", true).FirstOrDefault() as Control;
                if (tbRLwaferSensor != null)
                {
                    tbRLwaferSensor.Text = robot.Present_L.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                    switch (tbRLwaferSensor.Text)
                    {
                        case "OFF":
                            color = Color.MintCream;
                            break;
                        case "ON":
                            color = Color.LightGreen;
                            break;
                        default:
                            color = Color.White;
                            break;
                    }
                    tbRLwaferSensor.BackColor = color;
                }
                Control tbRLVacuSolenoid = manual.Controls.Find("tbRLVacuSolenoid", true).FirstOrDefault() as Control;
                if (tbRLVacuSolenoid != null)
                {
                    tbRLVacuSolenoid.Text = robot.Vacuum_L.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                    switch (tbRLVacuSolenoid.Text)
                    {
                        case "OFF":
                            color = Color.MintCream;
                            break;
                        case "ON":
                            color = Color.LightGreen;
                            break;
                        default:
                            color = Color.White;
                            break;
                    }
                    tbRLVacuSolenoid.BackColor = color;
                }
                string servo = "";
                string state = robot.State != null ? robot.State.Trim() : "";
                if (state.Length == 32)
                {
                    servo = state.Substring(10 - 1, 1).Equals("1") ? "ON" : "OFF";// 10 Servo On 0 = Servo off 1 = Servo On
                }
                Control tbRServo = manual.Controls.Find("tbRServo", true).FirstOrDefault() as Control;
                if (tbRServo != null)
                {
                    tbRServo.Text = servo;
                    Color color = new Color();
                    switch (tbRServo.Text)
                    {
                        case "OFF":
                            color = Color.MintCream;
                            break;
                        case "ON":
                            color = Color.LightGreen;
                            break;
                        default:
                            color = Color.White;
                            break;
                    }
                    tbRServo.BackColor = color;
                }
                Control tbRError = manual.Controls.Find("tbRError", true).FirstOrDefault() as Control;
                if (tbRError != null)
                {
                    tbRError.Text = robot.Error;
                }
            }
                
        }

        public static void UpdateRobotStatus(string name, string status)
        {
            Form manual = Application.OpenForms["FormManual"];

            if (manual == null)
                return;

            if (manual.InvokeRequired)
            {
                UpdateRobotStatus_D ph = new UpdateRobotStatus_D(UpdateRobotStatus);
                manual.BeginInvoke(ph, name, status);
            }
            else
            {
                StateUtil.Init();
                RobotState robot = StateUtil.GetDeviceState(name) != null ? (RobotState)StateUtil.GetDeviceState(name) : null;
                if (robot == null)
                {
                    return;
                }
                //update robot status           
                TextBox tbRStatus = manual.Controls.Find("tbRStatus", true).FirstOrDefault() as TextBox;
                tbRStatus.Text = status;
                switch (status)
                {
                    case "RUN":
                        tbRStatus.BackColor = Color.Lime;
                        break;
                    case "IDLE":
                        tbRStatus.BackColor = Color.Yellow;
                        break;
                }
            }
                
        }
    }
}
