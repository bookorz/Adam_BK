using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adam.UI_Update.Authority
{
    class AuthorityUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(AuthorityUpdate));
        delegate void UpdateFuncEnable(string Group);
        delegate void UpdateLogin(string Id, string Name, string Group);
        delegate void UpdateLogout();

        public static void UpdateLoginInfo(string Id, string Name, string Group)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form == null)
                    return;
                
                if (form.InvokeRequired)
                {
                    UpdateLogin ph = new UpdateLogin(UpdateLoginInfo);
                    form.BeginInvoke(ph, Id, Name, Group);
                }
                else
                {
                    //lbl_login_name
                    Label lbl_login_id = form.Controls.Find( "lbl_login_id", true).FirstOrDefault() as Label;
                    Label lbl_login_name = form.Controls.Find("lbl_login_name", true).FirstOrDefault() as Label;
                    Label lbl_login_group = form.Controls.Find("lbl_login_group", true).FirstOrDefault() as Label;
                    Label lbl_login_date = form.Controls.Find("lbl_login_date", true).FirstOrDefault() as Label;
                    if (lbl_login_id != null)
                        lbl_login_id.Text = Id;
                    if (lbl_login_name != null)
                        lbl_login_name.Text = Name;
                    if (lbl_login_group != null)
                        lbl_login_group.Text = Group;
                    if (lbl_login_date != null)
                        lbl_login_date.Text = System.DateTime.Now.ToString("yyyy-MM-dd HH：mm：ss");
                    Button btn = form.Controls.Find("btnLogInOut", true).FirstOrDefault() as Button;
                    btn.Text = "Logout";
                    AuthorityUpdate.UpdateFuncAssign(Group);
                }


            }
            catch
            {
                logger.Error("UpdateLoginInfo: Update fail.");
            }
        }

        public static void UpdateLogoutInfo()
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form == null)
                    return;

                if (form.InvokeRequired)
                {
                    UpdateLogout ph = new UpdateLogout(UpdateLogoutInfo);
                    form.BeginInvoke(ph);
                }
                else
                {
                    //lbl_login_name
                    Label lbl_login_id = form.Controls.Find("lbl_login_id", true).FirstOrDefault() as Label;
                    Label lbl_login_name = form.Controls.Find("lbl_login_name", true).FirstOrDefault() as Label;
                    Label lbl_login_group = form.Controls.Find("lbl_login_group", true).FirstOrDefault() as Label;
                    Label lbl_login_date = form.Controls.Find("lbl_login_date", true).FirstOrDefault() as Label;
                    if (lbl_login_id != null)
                        lbl_login_id.Text = "ID";
                    if (lbl_login_name != null)
                        lbl_login_name.Text = "Name";
                    if (lbl_login_group != null)
                        lbl_login_group.Text = "Group";
                    if (lbl_login_date != null)
                        lbl_login_date.Text = "";
                    Button btn = form.Controls.Find("btnLogInOut", true).FirstOrDefault() as Button;
                    btn.Text = "Login";
                }


            }
            catch
            {
                logger.Error("UpdateLoginInfo: Update fail.");
            }
        }

        public static void UpdateFuncInit(string Group)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form == null)
                    return;



                if (form.InvokeRequired)
                {
                    UpdateFuncEnable ph = new UpdateFuncEnable(UpdateFuncInit);
                    form.BeginInvoke(ph, Group);
                }
                else
                {
                    Control tabMonitor = form.Controls.Find("tabMonitor", true).FirstOrDefault() as Control;
                    Control tabComm = form.Controls.Find("tabComm", true).FirstOrDefault() as Control;
                    Control tabMapping = form.Controls.Find("tabMapping", true).FirstOrDefault() as Control;
                    Control tabStatus = form.Controls.Find("tabStatus", true).FirstOrDefault() as Control;
                    Control tabOCR = form.Controls.Find("tabOCR", true).FirstOrDefault() as Control;
                    Control tabSetting = form.Controls.Find("tabSetting", true).FirstOrDefault() as Control;
                    Control btnTeach = form.Controls.Find("btnTeach", true).FirstOrDefault() as Control;
                    Control btnMaintence = form.Controls.Find("btnMaintence", true).FirstOrDefault() as Control;
                    Control btnAlarm = form.Controls.Find("btnAlarm", true).FirstOrDefault() as Control;
                    Control btnMessage = form.Controls.Find("btnMessage", true).FirstOrDefault() as Control;
                    Control btnSysLog = form.Controls.Find("btnSysLog", true).FirstOrDefault() as Control;

                    switch (Group)
                    {
                        case "":
                            //瀏覽功能
                            if (tabMonitor != null)
                                tabMonitor.Enabled = false;
                            if (tabComm != null)
                                tabComm.Enabled = false;
                            if (tabMapping != null)
                                tabMapping.Enabled = false;
                            if (tabStatus != null)
                                tabStatus.Enabled = false;
                            if (tabOCR != null)
                                tabOCR.Enabled = false;
                            if (tabSetting != null)
                                tabSetting.Enabled = false;
                            //獨立功能
                            if (btnTeach != null)
                                btnTeach.Enabled = false;
                            if (btnMaintence != null)
                                btnMaintence.Enabled = false;
                            if (btnAlarm != null)
                                btnAlarm.Enabled = false;
                            if (btnMessage != null)
                                btnMessage.Enabled = false;
                            if (btnSysLog != null)
                                btnSysLog.Enabled = false;
                            break;
                    }
                }
            }
            catch
            {
                logger.Error("UpdateFuncAssign: Update fail.");
            }
        }

        public static void UpdateFuncAssign(string Group)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form == null)
                    return;



                if (form.InvokeRequired)
                {
                    UpdateFuncEnable ph = new UpdateFuncEnable(UpdateFuncAssign);
                    form.BeginInvoke(ph, Group);
                }
                else
                {
                    Control tabMonitor = form.Controls.Find("tabMonitor", true).FirstOrDefault() as Control;
                    Control tabComm = form.Controls.Find("tabComm", true).FirstOrDefault() as Control;
                    Control tabMapping = form.Controls.Find("tabMapping", true).FirstOrDefault() as Control;
                    Control tabStatus = form.Controls.Find("tabStatus", true).FirstOrDefault() as Control;
                    Control tabOCR = form.Controls.Find("tabOCR", true).FirstOrDefault() as Control;
                    Control tabSetting = form.Controls.Find("tabSetting", true).FirstOrDefault() as Control;
                    Control btnTeach = form.Controls.Find("btnTeach", true).FirstOrDefault() as Control;
                    Control btnMaintence = form.Controls.Find("btnMaintence", true).FirstOrDefault() as Control;
                    Control btnAlarm = form.Controls.Find("btnAlarm", true).FirstOrDefault() as Control;
                    Control btnMessage = form.Controls.Find("btnMessage", true).FirstOrDefault() as Control;
                    Control btnSysLog = form.Controls.Find("btnSysLog", true).FirstOrDefault() as Control;

                    switch (Group)
                    {
                        case "OP":
                            //瀏覽功能
                            if (tabMonitor != null)
                                tabMonitor.Enabled = true;
                            if (tabComm != null)
                                tabComm.Enabled = false;
                            if (tabMapping != null)
                                tabMapping.Enabled = true;
                            if (tabStatus != null)
                                tabStatus.Enabled = true;
                            if (tabOCR != null)
                                tabOCR.Enabled = true;
                            if (tabSetting != null)
                                tabSetting.Enabled = false;
                            //獨立功能
                            if (btnTeach != null)
                                btnTeach.Enabled = false;
                            if (btnMaintence != null)
                                btnMaintence.Enabled = false;
                            if (btnAlarm != null)
                                btnAlarm.Enabled = true;
                            if (btnMessage != null)
                                btnMessage.Enabled = true;
                            if (btnSysLog != null)
                                btnSysLog.Enabled = false;
                            break;
                        case "ADMIN":
                        case "ENG":
                            if (tabMonitor != null)
                                tabMonitor.Enabled = true;
                            if (tabComm != null)
                                tabComm.Enabled = true;
                            if (tabMapping != null)
                                tabMapping.Enabled = true;
                            if (tabStatus != null)
                                tabStatus.Enabled = true;
                            if (tabOCR != null)
                                tabOCR.Enabled = true;
                            if (tabSetting != null)
                                tabSetting.Enabled = true;
                            if (btnTeach != null)
                                btnTeach.Enabled = true;
                            if (btnMaintence != null)
                                btnMaintence.Enabled = true;
                            if (btnAlarm != null)
                                btnAlarm.Enabled = true;
                            if (btnMessage != null)
                                btnMessage.Enabled = true;
                            if (btnSysLog != null)
                                btnSysLog.Enabled = true;
                            break;

                    }
                }
            }
            catch
            {
                logger.Error("UpdateFuncAssign: Update fail.");
            }
        }
    }
}
