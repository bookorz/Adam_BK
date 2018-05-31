using log4net;
using System;
using System.Collections.Generic;
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
                        case Transaction.Command.AlignerType.GetStatus:
                            break;
                        case Transaction.Command.AlignerType.GetSpeed:
                            break;
                        case Transaction.Command.AlignerType.GetRIO:
                            break;
                    }

                }
            }
            catch
            {
                logger.Error("Aligner Manual Function: fail.");
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
        //            MessageBox.Show(msg, "Information");
        //        }
        //    }
        //    catch
        //    {
        //        logger.Error("Aligner Manual ShowMsg: fail.");
        //    }
        //}

    }
}
