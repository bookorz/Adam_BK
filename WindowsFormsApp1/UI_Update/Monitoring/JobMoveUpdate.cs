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

namespace Adam.UI_Update.Monitoring
{
    class JobMoveUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(JobMoveUpdate));
        delegate void UpdateNode(string JobId);

        public static void UpdateNodesJob(string NodeName)
        {
            try
            {
                Form form = Application.OpenForms["FormMonitoring"];
                TextBox tb;

                if (form == null)
                    return;

                tb = form.Controls.Find("LoadPort01_State", true).FirstOrDefault() as TextBox;

                if (tb == null)
                    return;

                if (tb.InvokeRequired)
                {
                    UpdateNode ph = new UpdateNode(UpdateNodesJob);
                    tb.BeginInvoke(ph, NodeName);
                }
                else
                {
                    Node node = NodeManagement.Get(NodeName);
                    for (int i = 1; i <= Tools.GetSlotCount(node.Type); i++)
                    {
                        Label present = form.Controls.Find(node.Name + "_Slot_" + i.ToString(), true).FirstOrDefault() as Label;
                        if (present != null)
                        {
                            present.ForeColor = Color.White;
                            Job tmp;
                            if (node.JobList.TryGetValue(i.ToString(), out tmp))
                            {
                                present.Text = tmp.Host_Job_Id;
                                switch (present.Text)
                                {
                                    case "No wafer":
                                        present.BackColor = Color.DimGray;
                                        break;
                                    case "Crossed":
                                    case "Undefined":
                                    case "Double":
                                        present.BackColor = Color.Red;
                                        break;
                                    default:
                                        present.BackColor = Color.Green;
                                        break;
                                }

                            }
                            else
                            {
                                present.Text = "No wafer";
                                present.BackColor = Color.DimGray;
                            }
                        }
                    }

                }


            }
            catch
            {
                logger.Error("UpdateNodesJob: Update fail.");
            }
        }

        public static void UpdateJobMove(string JobId)
        {
            try
            {
                Form form = Application.OpenForms["FormMonitoring"];
                TextBox tb;
                
                if (form == null)
                    return;

                tb = form.Controls.Find("LoadPort01_State", true).FirstOrDefault() as TextBox;
                
                if (tb == null)
                    return;

                if (tb.InvokeRequired)
                {
                    UpdateNode ph = new UpdateNode(UpdateJobMove);
                    tb.BeginInvoke(ph, JobId);
                }
                else
                {
                    Job Job = JobManagement.Get(JobId);
                    if (Job != null)
                    {
                        Node LastNode = NodeManagement.Get(Job.LastNode);
                        Node CurrentNode = NodeManagement.Get(Job.Position);
                        if (LastNode != null && CurrentNode != null)
                        {
                            for (int i = 1; i <= Tools.GetSlotCount(LastNode.Type); i++)
                            {
                                Label present = form.Controls.Find(LastNode.Name + "_Slot_" + i.ToString(), true).FirstOrDefault() as Label;
                                if (present != null)
                                {
                                    present.ForeColor = Color.White;
                                    Job tmp;
                                    if (LastNode.JobList.TryGetValue(i.ToString(), out tmp))
                                    {
                                        present.Text = tmp.Host_Job_Id;
                                        switch (present.Text)
                                        {
                                            case "No wafer":
                                                present.BackColor = Color.Gray;
                                                break;
                                            case "Crossed":
                                            case "Undefined":
                                            case "Double":
                                                present.BackColor = Color.Red;
                                                break;
                                            default:
                                                present.BackColor = Color.Green;
                                                break;
                                        }

                                    }
                                    else
                                    {
                                        present.Text = "";
                                        present.BackColor = Color.DimGray;
                                    }
                                }
                            }

                            for (int i = 1; i <= Tools.GetSlotCount(CurrentNode.Type); i++)
                            {
                                Label present = form.Controls.Find(CurrentNode.Name + "_Slot_" + i.ToString(), true).FirstOrDefault() as Label;
                                if (present != null)
                                {
                                    present.ForeColor = Color.White;
                                    Job tmp;
                                    if (CurrentNode.JobList.TryGetValue(i.ToString(), out tmp))
                                    {
                                        present.Text = tmp.Host_Job_Id;
                                        switch (present.Text)
                                        {
                                            case "No wafer":
                                                present.BackColor = Color.Gray;
                                                break;
                                            case "Crossed":
                                            case "Undefined":
                                            case "Double":
                                                present.BackColor = Color.Red;
                                                break;
                                            default:
                                                present.BackColor = Color.Green;
                                                break;
                                        }
                                    }
                                    else
                                    {
                                        present.Text = "";
                                        present.BackColor = Color.DimGray;
                                    }
                                }
                            }
                        }
                    }
                }


            }
            catch
            {
                logger.Error("UpdateJobMove: Update fail.");
            }
        }

    }
}
