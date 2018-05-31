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
        delegate void UpdateGUI(Transaction txn, string name, string msg);

        public static void UpdateGUIInfo(Transaction txn, string name, string msg)
        {
            try
            {
                string method = txn.Method;
                Form manual = Application.OpenForms["FormManual"];

                if (manual == null)
                    return;

                if (manual.InvokeRequired)
                {
                    UpdateGUI ph = new UpdateGUI(UpdateGUIInfo);
                    manual.BeginInvoke(ph, txn, name, msg);
                }
                else
                {
                    manual.Cursor = Cursors.Default;
                    manual.Enabled = true;
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
                    }
                    updateStatus(name);
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
                }
            }
            catch (Exception e)
            {
                logger.Error("Robot Manual Function: fail.");
                Console.WriteLine("Exception caught: {0}", e);
            }
        }

        private static void updateStatus(string device)
        {
            StateUtil.Init();
            RobotState robot = StateUtil.GetDeviceState(device) != null ? (RobotState)StateUtil.GetDeviceState(device) : null;
            Form form = Application.OpenForms["FormManual"];
            if (form == null || robot == null)
            {
                return;
            }

            Control tbRStatus = form.Controls.Find("tbRStatus", true).FirstOrDefault() as Control;
            if (tbRStatus != null)
            {
                tbRStatus.Text = robot.Status;
                Color color = new Color();
                switch (tbRStatus.Text)
                {
                    case "N/A":
                        color = Color.Gray;
                        break;
                    case "0:Start":
                        color = Color.LightGreen;
                        break;
                    case "1:Start Finish":
                        color = Color.LightSkyBlue;
                        break;
                    case "2:System Error Finish":
                        color = Color.Red;
                        break;
                    default:
                        color = Color.White;
                        break;
                }
                tbRStatus.BackColor = color;
            }
            Control tbRSpeed = form.Controls.Find("tbRSpeed", true).FirstOrDefault() as Control;
            if (tbRSpeed != null)
            {
                tbRSpeed.Text = robot.Speed.Equals("00") ? "100" : robot.Speed;
            }
            Control tbRRwaferSensor = form.Controls.Find("tbRRwaferSensor", true).FirstOrDefault() as Control;
            if (tbRRwaferSensor != null)
            {
                tbRRwaferSensor.Text = robot.Present_R.Equals("1") ? "ON" : "OFF";
                Color color = new Color();
                switch (tbRRwaferSensor.Text)
                {
                    case "OFF":
                        color = Color.Gray;
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
            Control tbRRVacuSolenoid = form.Controls.Find("tbRRVacuSolenoid", true).FirstOrDefault() as Control;
            if (tbRRVacuSolenoid != null)
            {
                tbRRVacuSolenoid.Text = robot.Vacuum_R.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                switch (tbRRVacuSolenoid.Text)
                {
                    case "OFF":
                        color = Color.Gray;
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
            Control tbRLwaferSensor = form.Controls.Find("tbRLwaferSensor", true).FirstOrDefault() as Control;
            if (tbRLwaferSensor != null)
            {
                tbRLwaferSensor.Text = robot.Present_L.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                switch (tbRLwaferSensor.Text)
                {
                    case "OFF":
                        color = Color.Gray;
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
            Control tbRLVacuSolenoid = form.Controls.Find("tbRLVacuSolenoid", true).FirstOrDefault() as Control;
            if (tbRLVacuSolenoid != null)
            {
                tbRLVacuSolenoid.Text = robot.Vacuum_L.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                switch (tbRLVacuSolenoid.Text)
                {
                    case "OFF":
                        color = Color.Gray;
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
        }

        public static void UpdateGUIStatus(string name, string status)
        {
            StateUtil.Init();
            RobotState robot = StateUtil.GetDeviceState(name) != null ? (RobotState)StateUtil.GetDeviceState(name) : null;
            Form form = Application.OpenForms["FormManual"];
            if (form == null || robot == null)
            {
                return;
            }
            //update robot status           
            Control tbRStatus = form.Controls.Find("tbRStatus", true).FirstOrDefault() as Control;
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
