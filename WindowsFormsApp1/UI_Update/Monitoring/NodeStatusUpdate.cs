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
                        case "Run":
                            State_tb.BackColor = Color.Lime;
                            UpdateCurrentState();
                            break;
                        case "Idle":
                            State_tb.BackColor = Color.Yellow;
                            UpdateCurrentState();
                            break;
                        case "Ready To Load":
                            State_tb.BackColor = Color.DarkGray;                          
                            break;
                        case "Transfer Ready":
                            State_tb.BackColor = Color.Green;
                            break;
                        case "Transfer Blocked":
                            State_tb.BackColor = Color.DarkOrange;
                            break;
                        case "Ready To Unload":
                            State_tb.BackColor = Color.Blue;
                            break;
                        case "Alarm":
                            State_tb.BackColor = Color.Red;
                            break;
                    }
                    
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
                    Dictionary<string, string> Params = new Dictionary<string, string>();
                    switch (state.Text)
                    {
                        case "Run":
                            state.BackColor = Color.Lime;
                            
                            Params.Add("Red", "False");
                            Params.Add("Orange", "False");
                            Params.Add("Green", "True");
                            Params.Add("Blue", "False");
                            Params.Add("Buzzer1", "False");
                            Params.Add("Buzzer2", "False");
                            FormMain.DIO.SetIO(Params);

                            break;
                        case "Idle":
                            state.BackColor = Color.Yellow;
                            
                            Params.Add("Red", "False");
                            Params.Add("Orange", "False");
                            Params.Add("Green", "False");
                            Params.Add("Blue", "True");
                            Params.Add("Buzzer1", "False");
                            Params.Add("Buzzer2", "False");
                            FormMain.DIO.SetIO(Params);
                            break;
                        case "Pause":
                           
                            Params.Add("Red", "False");
                            Params.Add("Orange", "True");
                            Params.Add("Green", "False");
                            Params.Add("Blue", "False");
                            Params.Add("Buzzer1", "False");
                            Params.Add("Buzzer2", "False");
                            FormMain.DIO.SetIO(Params);
                            break;
                        case "Alarm":
                            state.BackColor = Color.Red;
                            FormMain.DIO.SetIO("Red", "True");
                            
                            break;
                    }
                }


            }
            catch
            {
                logger.Error("UpdateDIOStatus: Update fail.");
            }
        }

    }
}
