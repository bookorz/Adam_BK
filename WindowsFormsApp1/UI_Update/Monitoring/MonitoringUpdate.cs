using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adam.UI_Update.Monitoring
{
    public class MonitoringUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(MonitoringUpdate));

        delegate void UpdatePortUsed(string PortName, bool Used);

        public static void UpdateUseState(string PortName, bool used)
        {
            try
            {
                Form form = Application.OpenForms["FormMonitoring"];
                TextBox Used;
                if (form == null)
                    return;

                Used = form.Controls.Find(PortName + "_FID", true).FirstOrDefault() as TextBox;
                if (Used == null)
                    return;

                if (Used.InvokeRequired)
                {
                    UpdatePortUsed ph = new UpdatePortUsed(UpdateUseState);
                    Used.BeginInvoke(ph, PortName, used);
                }
                else
                {
                    if (used)
                    {
                        //Used.Text = "Used";
                        Used.BackColor = Color.Green;
                        Used.ForeColor = Color.White;
                    }
                    else
                    {
                        //Used.Text = "Not Used";
                        Used.BackColor = Color.White;
                        Used.ForeColor = Color.Black;
                    }

                }
            }
            catch
            {
                logger.Error("UpdateUseState: Update fail.");
            }
        }
    }
}
