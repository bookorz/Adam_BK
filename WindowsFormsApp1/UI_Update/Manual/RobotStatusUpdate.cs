using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adam.UI_Update.Manual
{
    class ManualRobotStatusUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(ManualRobotStatusUpdate));
        delegate void UpdateFuncEnable(string Group);
        delegate void UpdateLogin(string Id, string Name, string Group);
        delegate void UpdateLogout();
        delegate void ShowMessage(string str);

        public static void ShowMsg(string msg)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form == null)
                    return;

                if (form.InvokeRequired)
                {
                    ShowMessage ph = new ShowMessage(ShowMsg);
                    form.BeginInvoke(ph, msg);
                }
                else
                {
                    Form manual = Application.OpenForms["FormManual"];
                    manual.Cursor = Cursors.Default;
                    manual.Enabled = true;
                    MessageBox.Show(msg);
                }
            }
            catch
            {
                logger.Error("Robot Manual ShowMsg: fail.");
            }
        }

        public static void UpdateRobotInfo(Dictionary<string,string> args)
        {
            // robot status , speed , wafer sensor, speed, vaccum 
            try
            {
                //Form form = Application.OpenForms["FormMain"];

                //if (form == null)
                //    return;

                //if (form.InvokeRequired)
                //{
                //    UpdateLogin ph = new UpdateLogin(UpdateLoginInfo);
                //    form.BeginInvoke(ph, Id, Name, Group);
                //}
                //else
                //{
                //    //lbl_login_name
                //    Label lbl_login_id = form.Controls.Find("lbl_login_id", true).FirstOrDefault() as Label;
                //    if (lbl_login_id != null)
                //        lbl_login_id.Text = Id;
                //}
            }
            catch
            {
                logger.Error("Robot Manual - UpdateRobotInfo: Update fail.");
            }
        }

    }
}
