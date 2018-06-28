using Adam.Util;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.UI_Update.Running
{
    class RunningUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(RunningUpdate));

        delegate void UpdatePortDest(string PortName, string Dest);
        delegate void UpdatePresent(string JobId);
        delegate void UpdatePortUsed(string PortName, bool Used);

        public static void UpdateRunningInfo(string Param, string Value)
        {
            Form form = Application.OpenForms["FormRunning"];
            Button Start_btn;
            if (form == null)
                return;

            Start_btn = form.Controls.Find("Start_btn", true).FirstOrDefault() as Button;
            if (Start_btn == null)
                return;

            if (Start_btn.InvokeRequired)
            {
                UpdatePortDest ph = new UpdatePortDest(UpdateRunningInfo);
                Start_btn.BeginInvoke(ph, Param, Value);
            }
            else
            {
                TextBox tb = form.Controls.Find(Param+"_tb", true).FirstOrDefault() as TextBox;
                if (tb == null)
                    return;
                if (Param.Equals("TransCount"))
                {
                    tb.Text = (Convert.ToInt32(tb.Text) - 1).ToString();
                    
                }
                else
                {
                    tb.Text = Value;
                }
            }
        }

        public static void ReverseRunning(string FinishPort)
        {
            
            Form form = Application.OpenForms["FormRunning"];
            Button Start_btn;
            if (form == null)
                return;

            Start_btn = form.Controls.Find("Start_btn", true).FirstOrDefault() as Button;
            if (Start_btn == null)
                return;

            if (Start_btn.InvokeRequired)
            {
                UpdatePresent ph = new UpdatePresent(ReverseRunning);
                Start_btn.BeginInvoke(ph, FinishPort);
            }
            else
            {
                Node FinPort = NodeManagement.Get(FinishPort);
                if (FinPort != null)
                {
                    Start_btn = form.Controls.Find(FinPort.Name + "_Dest", true).FirstOrDefault() as Button;
                    if (Start_btn == null)
                        return;

                    Start_btn.Text = "";
                    Node DestPort = NodeManagement.Get(FinPort.DestPort);
                    if (DestPort != null)
                    {
                        Start_btn = form.Controls.Find(DestPort.Name + "_Dest", true).FirstOrDefault() as Button;
                        if (Start_btn == null)
                            return;

                        Start_btn.Text = FinPort.Name;
                        foreach (Job job in DestPort.JobList.Values)
                        {
                            if (job.MapFlag)
                            {
                                job.ProcessFlag = false;
                                job.Destination = FinPort.Name;
                                job.DestinationSlot = job.Slot;
                                FinPort.ReserveList.TryAdd(job.Slot,job);
                            }
                        }
                        FinPort.DestPort = "Assign";
                        if (FinPort.Name.Equals(DestPort.Name))
                        {
                            DestPort.Mode = "LU";
                        }
                        else
                        {
                            FinPort.Mode = "UD";
                            DestPort.Mode = "LD";
                        }
                        DestPort.DestPort = FinPort.Name;
                        DestPort.ReserveList.Clear();
                        TextBox tb = form.Controls.Find("TransCount_tb", true).FirstOrDefault() as TextBox;
                        if (Convert.ToInt32(tb.Text) <= 1)//次數歸零 停止DEMO
                        {
                            DestPort.Available = false;
                        }
                        else
                        {
                            DestPort.Available = true;
                        }
                        FinPort.Used = false;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    return;
                }

                

               
            }
        }

        public static void UpdateNodesJob(string NodeName)
        {
            Form form = Application.OpenForms["FormRunning"];
            Button Start_btn;
            if (form == null)
                return;

            Start_btn = form.Controls.Find("Start_btn", true).FirstOrDefault() as Button;
            if (Start_btn == null)
                return;

            if (Start_btn.InvokeRequired)
            {
                UpdatePresent ph = new UpdatePresent(UpdateNodesJob);
                Start_btn.BeginInvoke(ph, NodeName);
            }
            else
            {
                Node node = NodeManagement.Get(NodeName);
                for (int i = 1; i <= Tools.GetSlotCount(node.Type); i++)
                {
                    Label present = form.Controls.Find(node.Name + "_Slot_" + i.ToString(), true).FirstOrDefault() as Label;
                    if (present != null)
                    {
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

        public static void UpdateLoadPortMapping(string PortName, string Mapping)
        {
            Form form = Application.OpenForms["FormRunning"];
            Button Start_btn;
            if (form == null)
                return;

            Start_btn = form.Controls.Find("Start_btn", true).FirstOrDefault() as Button;
            if (Start_btn == null)
                return;

            if (Start_btn.InvokeRequired)
            {
                UpdatePortDest ph = new UpdatePortDest(UpdateLoadPortMapping);
                Start_btn.BeginInvoke(ph, PortName, Mapping);
            }
            else
            {
                Node port = NodeManagement.Get(PortName);
                //List<Job> MappingData = new List<Job>();
                port.IsMapping = true;
                if (Mapping.Equals(""))
                {
                    foreach (Job eachJob in port.JobList.Values)
                    {
                        JobManagement.Remove(eachJob.Job_Id);
                    }
                    port.JobList.Clear();
                    port.ReserveList.Clear();
                    JobManagement.ClearAssignJobByPort(port.Name);
                }
                else
                {
                    int currentIdx = 1;
                    for (int i = Mapping.Length - 1; i >= 0; i--)
                    {
                        Job wafer = new Job();
                        wafer.Slot = (i + 1).ToString();
                        wafer.FromPort = PortName;
                        wafer.Position = PortName;
                        string Slot = (i + 1).ToString("00");
                        switch (Mapping[i])
                        {
                            case '0':
                                wafer.Job_Id = "No wafer";
                                wafer.Host_Job_Id = wafer.Job_Id;
                                //MappingData.Add(wafer);
                                break;
                            case '1':
                                while (true)
                                {
                                    wafer.Job_Id = "Wafer" + currentIdx.ToString("00");
                                    wafer.Host_Job_Id = wafer.Job_Id;
                                    wafer.MapFlag = true;
                                    if (JobManagement.Add(wafer.Job_Id, wafer))
                                    {

                                        //MappingData.Add(wafer);
                                        break;
                                    }
                                    currentIdx++;
                                }

                                break;
                            case '2':
                                wafer.Job_Id = "Crossed";
                                wafer.Host_Job_Id = wafer.Job_Id;
                                //MappingData.Add(wafer);
                                break;
                            case '?':
                                wafer.Job_Id = "Undefined";
                                wafer.Host_Job_Id = wafer.Job_Id;
                                //MappingData.Add(wafer);
                                break;
                            case 'W':
                                wafer.Job_Id = "Double";
                                wafer.Host_Job_Id = wafer.Job_Id;
                                //MappingData.Add(wafer);
                                break;
                        }
                        if (!port.AddJob(wafer.Slot, wafer))
                        {
                            Job org = port.GetJob(wafer.Slot);
                            JobManagement.Remove(org.Job_Id);
                            port.RemoveJob(wafer.Slot);
                            port.AddJob(wafer.Slot, wafer);
                        }
                    }
                }

                foreach (Job tmp in port.JobList.Values.ToList())
                {
                    Label present = form.Controls.Find(port.Name + "_Slot_" + tmp.Slot, true).FirstOrDefault() as Label;
                    if (present != null)
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
                        present.ForeColor = Color.White;

                    }
                }

            }
        }

        public static void UpdateUseState(string PortName, bool used)
        {
            try
            {
                Form form = Application.OpenForms["FormRunning"];
                Button Used_btn;
                if (form == null)
                    return;

                Used_btn = form.Controls.Find(PortName + "_Used", true).FirstOrDefault() as Button;
                if (Used_btn == null)
                    return;

                if (Used_btn.InvokeRequired)
                {
                    UpdatePortUsed ph = new UpdatePortUsed(UpdateUseState);
                    Used_btn.BeginInvoke(ph, PortName, used);
                }
                else
                {
                    if (used)
                    {
                        Used_btn.Text = "Used";
                        Used_btn.BackColor = Color.Green;
                        Used_btn.ForeColor = Color.White;
                    }
                    else
                    {
                        Used_btn.Text = "Not Used";
                        Used_btn.BackColor = Color.DimGray;
                        Used_btn.ForeColor = Color.Black;
                    }

                }
            }
            catch
            {
                logger.Error("UpdateDest: Update fail.");
            }
        }

        public static void UpdateDestCaption(string PortName, string Dest)
        {
            try
            {
                Form form = Application.OpenForms["FormRunning"];
                Button Dest_btn;
                if (form == null)
                    return;

                Dest_btn = form.Controls.Find(PortName + "_Dest", true).FirstOrDefault() as Button;
                if (Dest_btn == null)
                    return;

                if (Dest_btn.InvokeRequired)
                {
                    UpdatePortDest ph = new UpdatePortDest(UpdateDestCaption);
                    Dest_btn.BeginInvoke(ph, PortName, Dest_btn);
                }
                else
                {
                    Dest_btn.Text = Dest;
                }
            }
            catch
            {
                logger.Error("UpdateDest: Update fail.");
            }
        }

        public static void UpdateJobMove(string JobId)
        {
            try
            {
                Form form = Application.OpenForms["FormRunning"];
                Button Start_btn;
                if (form == null)
                    return;

                Start_btn = form.Controls.Find("Start_btn", true).FirstOrDefault() as Button;
                if (Start_btn == null)
                    return;

                if (Start_btn.InvokeRequired)
                {
                    UpdatePresent ph = new UpdatePresent(UpdateJobMove);
                    Start_btn.BeginInvoke(ph, JobId);
                }
                else
                {
                    Job Job = JobManagement.Get(JobId);

                    if (Job != null)
                    {
                        //logger.Debug(JsonConvert.SerializeObject(Job));
                        Node LastNode = NodeManagement.Get(Job.LastNode);
                        // logger.Debug(JsonConvert.SerializeObject(LastNode.JobList));
                        Node CurrentNode = NodeManagement.Get(Job.Position);
                        // logger.Debug(JsonConvert.SerializeObject(CurrentNode.JobList));
                        if (LastNode != null && CurrentNode != null)
                        {
                            for (int i = 1; i <= Tools.GetSlotCount(LastNode.Type); i++)
                            {
                                Label present = form.Controls.Find(LastNode.Name + "_Slot_" + i.ToString(), true).FirstOrDefault() as Label;
                                if (present != null)
                                {
                                    Job tmp;
                                    if (LastNode.JobList.TryGetValue(i.ToString(), out tmp))
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

                            for (int i = 1; i <= Tools.GetSlotCount(CurrentNode.Type); i++)
                            {
                                Label present = form.Controls.Find(CurrentNode.Name + "_Slot_" + i.ToString(), true).FirstOrDefault() as Label;
                                if (present != null)
                                {
                                    Job tmp;
                                    if (CurrentNode.JobList.TryGetValue(i.ToString(), out tmp))
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
                }


            }
            catch
            {
                logger.Error("UpdateJobMove: Update fail.");
            }
        }


    }
}
