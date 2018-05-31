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
        delegate void UpdateGUI(string method, string name, string msg);

        public static void UpdateGUIInfo(string method, string name, string msg)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form == null)
                    return;

                if (form.InvokeRequired)
                {
                    UpdateGUI ph = new UpdateGUI(UpdateGUIInfo);
                    form.BeginInvoke(ph, method, name, msg);
                }
                else
                {
                    Form manual = Application.OpenForms["FormManual"];
                    manual.Cursor = Cursors.Default;
                    manual.Enabled = true;
                    switch (method)
                    {                        
                        case Transaction.Command.RobotType.GetStatus:
                            StateUtil.updateSTS(name, msg);
                            break;
                        case Transaction.Command.RobotType.GetSpeed:
                            StateUtil.updateSP(name, msg);
                            break;
                        case Transaction.Command.RobotType.GetRIO:
                            StateUtil.updateRIO(name, msg);
                            break;
                   }
                    updateStatus(name);
                }
            }
            catch
            {
                logger.Error("Robot Manual Function: fail.");
            }
        }
        private static void updateStatus(string device)
        {
            RobotState robot = (RobotState) StateUtil.getDeviceState(device);
            Form form = Application.OpenForms["FormManual"];
            if(form != null)
            {
                Control tbRStatus = form.Controls.Find("tbRStatus", true).FirstOrDefault() as Control;
                if (tbRStatus != null)
                {
                    tbRStatus.Text = robot.Status;
                    tbRStatus.BackColor = Color.Red;
                }                
                Control tbRSpeed = form.Controls.Find("tbRSpeed", true).FirstOrDefault() as Control;
                if (tbRSpeed != null)
                {
                    tbRSpeed.Text = robot.Status;
                    tbRStatus.BackColor = Color.Red;
                }
                Control tbRRwaferSensor = form.Controls.Find("tbRRwaferSensor", true).FirstOrDefault() as Control;
                if (tbRRwaferSensor != null)
                {
                    tbRRwaferSensor.Text = robot.Status;
                    tbRStatus.BackColor = Color.Red;
                }
                Control tbRRVacuSolenoid = form.Controls.Find("tbRRVacuSolenoid", true).FirstOrDefault() as Control;
                if (tbRRVacuSolenoid != null)
                {
                    tbRRVacuSolenoid.Text = robot.Status;
                    tbRStatus.BackColor = Color.Red;
                }
                Control tbRLwaferSensor = form.Controls.Find("tbRLwaferSensor", true).FirstOrDefault() as Control;
                if (tbRLwaferSensor != null)
                {
                    tbRLwaferSensor.Text = robot.Status;
                    tbRStatus.BackColor = Color.Red;
                }
                Control tbRLVacuSolenoid = form.Controls.Find("tbRLVacuSolenoid", true).FirstOrDefault() as Control;
                if (tbRLVacuSolenoid != null)
                {
                    tbRLVacuSolenoid.Text = robot.Status;
                    tbRStatus.BackColor = Color.Red;
                }

            }
        }
        //public static void ShowMsg(string msg)
        //{
        //    try
        //    {
        //        Form form = Application.OpenForms["FormMain"];

        //        if (form == null)
        //            return;

        //        if (form.InvokeRequired)
        //        {
        //            ShowMessage ph = new ShowMessage(ShowMsg);
        //            form.BeginInvoke(ph, msg);
        //        }
        //        else
        //        {
        //            Form manual = Application.OpenForms["FormManual"];
        //            manual.Cursor = Cursors.Default;
        //            manual.Enabled = true;
        //            MessageBox.Show(msg,"Information");
        //        }
        //    }
        //    catch
        //    {
        //        logger.Error("Robot Manual ShowMsg: fail.");
        //    }
        //}

    }
}
