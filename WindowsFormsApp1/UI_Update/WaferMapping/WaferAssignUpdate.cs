using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.UI_Update.WaferMapping
{
    class WaferAssignUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(WaferAssignUpdate));
        delegate void UpdatePort(string PortName, string Mapping);

        public static void UpdateLoadPort(string PortName, string Mapping)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                DataGridView Port_gv;
                if (form == null)
                    return;

                Port_gv = form.Controls.Find(PortName+"Assign_Gv", true).FirstOrDefault() as DataGridView;
                if (Port_gv == null)
                    return;

                if (Port_gv.InvokeRequired)
                {
                    UpdatePort ph = new UpdatePort(UpdateLoadPort);
                    Port_gv.BeginInvoke(ph, PortName, Mapping);
                }
                else
                {
                    Node port = NodeManagement.Get(PortName);
                    List<Job> MappingData = new List<Job>();
                    
                    for (int i = Mapping.Length - 1; i >= 0; i--)
                    {
                        string Slot = (i + 1).ToString("00");


                    }
                }


            }
            catch
            {
                logger.Error("UpdateControllerStatus: Update fail.");
            }
        }
    }
}
