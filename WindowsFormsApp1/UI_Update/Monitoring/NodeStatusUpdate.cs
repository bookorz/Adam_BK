using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.UI_Update.Monitoring
{
    class NodeStatusUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(ConnectionStatusUpdate));
        delegate void UpdateNode(string Device_ID, string State);
        delegate void UpdateState();

        public static void UpdateNodeState(string Device_ID, string State)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                TextBox State_tb;
                if (form == null)
                    return;

                State_tb = form.Controls.Find(Device_ID+"_State", true).FirstOrDefault() as TextBox;
                if (State_tb == null)
                    return;

                if (State_tb.InvokeRequired)
                {
                    UpdateNode ph = new UpdateNode(UpdateNodeState);
                    State_tb.BeginInvoke(ph, Device_ID, State);
                }
                else
                {
                    State_tb.Text = State;
                    switch (State)
                    {
                        case "RUN":
                            State_tb.BackColor = Color.Lime;
                            break;
                        case "IDLE":
                            State_tb.BackColor = Color.Yellow;
                            break;
                    }
                    UpdateCurrentState();
                }


            }
            catch
            {
                logger.Error("UpdateControllerStatus: Update fail.");
            }
        }

        public static void UpdateCurrentState()
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];

                if (form == null)
                    return;

                Button state = form.Controls.Find("CurrentSate_btn", true).FirstOrDefault() as Button;

                if (state == null)
                    return;

                if (state.InvokeRequired)
                {
                    UpdateState ph = new UpdateState(UpdateCurrentState);
                    state.BeginInvoke(ph);
                }
                else
                {
                   
                    state.Text = NodeManagement.GetCurrentState();
                }


            }
            catch
            {
                logger.Error("UpdateDIOStatus: Update fail.");
            }
        }

    }
}
