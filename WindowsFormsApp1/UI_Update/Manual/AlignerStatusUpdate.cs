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
    class ManualAlignerStatusUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(ManualAlignerStatusUpdate));
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
                        case Transaction.Command.AlignerType.GetStatus:
                            StateUtil.UpdateSTS(name, msg);
                            break;
                        case Transaction.Command.AlignerType.GetSpeed:
                            StateUtil.UpdateSP(name, msg);
                            break;
                        case Transaction.Command.AlignerType.GetRIO:
                            StateUtil.UpdateRIO(name, msg);
                            break;
                        case Transaction.Command.AlignerType.GetError:
                            StateUtil.UpdateError(name, msg);
                            break;
                    }
                    updateStatus(name);
                    if (method.Equals(Transaction.Command.AlignerType.GetRIO))
                    {
                        Transaction next_txn = new Transaction();
                        next_txn = new Transaction();
                        switch (txn.Value)
                        {
                            case "4":
                                next_txn.Method = Transaction.Command.RobotType.GetRIO;
                                next_txn.Value = "8"; //8 Present ¦^õX¦b®u Sensor ªºª¬ºA
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
            catch(Exception e)
            {
                logger.Error("Aligner Manual Function: fail.");
                Console.WriteLine("Exception caught: {0}", e);
            }
        }
        private static void updateStatus(string device)
        {
            StateUtil.Init();
            AlignerState aligner = StateUtil.GetDeviceState(device) != null ? (AlignerState)StateUtil.GetDeviceState(device) : null;
            Form form = Application.OpenForms["FormManual"];
            if (form == null || aligner == null)
            {
                return;
            }
            string servo = "";
            string state = aligner.State != null? aligner.State.Trim():"";
            if (state.Length == 32)
            {
                servo = state.Substring(10 - 1, 1).Equals("1")?"ON":"OFF";// 10 Servo On 0 = Servo off 1 = Servo On
            }

            Control tbStatus = null;
            Control tbServo = null;
            Control tbWaferSensor = null;
            Control tbVacSolenoid = null;
            Control tbSpeed = null;
            Control tbError = null;

            if (device.Equals("Aligner01"))
            {
                tbStatus = form.Controls.Find("tbA1Status", true).FirstOrDefault() as Control;
                tbServo = form.Controls.Find("tbA1Servo", true).FirstOrDefault() as Control;
                tbWaferSensor = form.Controls.Find("tbA1WaferSensor", true).FirstOrDefault() as Control;
                tbVacSolenoid = form.Controls.Find("tbA1VacSolenoid", true).FirstOrDefault() as Control;
                tbSpeed = form.Controls.Find("tbA1Speed", true).FirstOrDefault() as Control;
                tbError = form.Controls.Find("tbA1Error", true).FirstOrDefault() as Control;
            }
            else if(device.Equals("Aligner02"))
            {
                tbStatus = form.Controls.Find("tbA2Status", true).FirstOrDefault() as Control;
                tbServo = form.Controls.Find("tbA2Servo", true).FirstOrDefault() as Control;
                tbWaferSensor = form.Controls.Find("tbA2WaferSensor", true).FirstOrDefault() as Control;
                tbVacSolenoid = form.Controls.Find("tbA2VacSolenoid", true).FirstOrDefault() as Control;
                tbSpeed = form.Controls.Find("tbA2Speed", true).FirstOrDefault() as Control;
                tbError = form.Controls.Find("tbA2Error", true).FirstOrDefault() as Control;
            }         

            if (tbStatus != null)
            {
               // tbStatus.Text = aligner.Status;
                tbStatus.Text = aligner.State;
                Color color = new Color();
                switch (tbStatus.Text)
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
                tbStatus.BackColor = color;
            }            
            if (tbServo != null)
            {
                tbServo.Text = aligner.Servo.Equals("1") ? "ON" : "OFF";
                Color color = new Color();
                switch (tbServo.Text)
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
                tbServo.BackColor = color;
            }
            if (tbWaferSensor != null)
            {
                tbWaferSensor.Text = aligner.Present.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                switch (tbWaferSensor.Text)
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
                tbWaferSensor.BackColor = color;
            }
            if (tbVacSolenoid != null)
            {
                tbVacSolenoid.Text = aligner.Vacuum.Equals("1") ? "ON" : "OFF"; Color color = new Color();
                switch (tbVacSolenoid.Text)
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
                tbVacSolenoid.BackColor = color;
            }
            if (tbSpeed != null)
            {
                tbSpeed.Text = aligner.Speed.Equals("00") ? "100" : aligner.Speed;
            }
            if (tbError != null)
            {
                tbError.Text = aligner.Error;
            }

        }

        public static void UpdateGUIStatus(string name, string status)
        {
            StateUtil.Init();
            AlignerState aligner = StateUtil.GetDeviceState(name) != null ? (AlignerState)StateUtil.GetDeviceState(name) : null;
            Form form = Application.OpenForms["FormManual"];
            if (form == null || aligner == null)
            {
                return;
            }
            //update robot status
            switch (name)
            {
                case "Aligner01":
                    Control tbA1Status = form.Controls.Find("tbRStatus", true).FirstOrDefault() as Control;
                    tbA1Status.Text = status;
                    switch (status)
                    {
                        case "RUN":
                            tbA1Status.BackColor = Color.Lime;
                            break;
                        case "IDLE":
                            tbA1Status.BackColor = Color.Yellow;
                            break;
                    }
                    break;
                case "Aligner02":
                    Control tbA2Status = form.Controls.Find("tbRStatus", true).FirstOrDefault() as Control;
                    tbA2Status.Text = status;
                    switch (status)
                    {
                        case "RUN":
                            tbA2Status.BackColor = Color.Lime;
                            break;
                        case "IDLE":
                            tbA2Status.BackColor = Color.Yellow;
                            break;
                    }
                    break;
            }
        }

    }
}
