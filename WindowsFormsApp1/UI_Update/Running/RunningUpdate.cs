﻿using Adam.UI_Update.WaferMapping;
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
            Form form = Application.OpenForms["FormRunningScreen"];
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
            
            Form form = Application.OpenForms["FormRunningScreen"];
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
                   
                    Node DestPort = NodeManagement.Get(FinPort.DestPort);
                    if (DestPort != null)
                    {
                        int StartSlot = 1;
                        List<Job> DestPortJobs = DestPort.JobList.Values.ToList();
                        DestPortJobs.Sort((x, y) => { return Convert.ToInt16(x.Slot).CompareTo(Convert.ToInt16(y.Slot)); });
                        foreach (Job job in DestPortJobs)
                        {
                            if (job.MapFlag)
                            {
                                while (StartSlot <= 25)
                                {
                                    if (FinPort.GetJob(StartSlot.ToString()).MapFlag == false)
                                    {
                                        job.ProcessFlag = false;
                                        job.Destination = FinPort.Name;
                                        job.DestinationSlot = StartSlot.ToString();
                                        FinPort.ReserveList.TryAdd(job.Slot, job);
                                        StartSlot++;
                                        break;
                                    }
                                    StartSlot++;
                                }
                            }
                            if (StartSlot > 25)
                            {
                                break;
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
                            WaferAssignUpdate.UpdateLoadPortMode(FinPort.Name, FinPort.Mode);
                            WaferAssignUpdate.UpdateLoadPortMode(DestPort.Name, DestPort.Mode);
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

        
       
    }
}
