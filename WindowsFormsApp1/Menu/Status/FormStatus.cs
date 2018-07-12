using Adam.Util;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using TransferControl.Controller;
using TransferControl.Management;

namespace Adam.Menu.Status
{
    public partial class FormStatus : Adam.Menu.FormFrame
    {
        public FormStatus()
        {
            InitializeComponent();
            vSBRobotStatus.Maximum = pbRobotState.Height;
            vSBAlignerStatus.Maximum = pbAlignerState.Height;
            vSBPortStatus.Maximum = pbPortState.Height;
        }

        private void vSBRobotStatus_Scroll(object sender, ScrollEventArgs e)
        {
            pbRobotState.Top = -vSBRobotStatus.Value;
        }

        private void vSBAlignerStatus_Scroll(object sender, ScrollEventArgs e)
        {
            pbAlignerState.Top = -vSBAlignerStatus.Value;
        }

        private void vSBPortStatus_Scroll(object sender, ScrollEventArgs e)
        {
            pbPortState.Top = -vSBPortStatus.Value;
        }

        private void FormStatus_Leave(object sender, EventArgs e)
        {
            //MessageBox.Show("881 !","QQ");
        }

        private void FormStatus_Enter(object sender, EventArgs e)
        {
            //MessageBox.Show("Hi Enter!", "^^");
            getStatus();
        }

        static IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize).Select(i => str.Substring(i * chunkSize, chunkSize));
        }

        public void setStatus()
        {
            try
            {
                //clear old data
                dgvRstatus.Rows.Clear();
                dgvAstatus.Rows.Clear();
                dgvLstatus.Rows.Clear();
                foreach (Node each in NodeManagement.GetList())
                {
                    string ctrl_status = ControllerManagement.Get(each.Controller).Status;
                    if (ctrl_status.Equals("Connected"))
                    {
                        if (each.Brand.ToUpper().Equals("KAWASAKI"))
                        {                            
                        }
                        else
                        {                            
                        }
                        String state = "";
                        switch (each.Type)
                        {
                            case "Robot":
                                DataGridViewRow robotRow = (DataGridViewRow)dgvRstatus.Rows[0].Clone();
                                robotRow.Cells[0].Value = each.Name;
                                state = ((RobotState) StateUtil.device[each.Name]).State;
                                for (int i = 1; i <= state.Length; i++)
                                {
                                    robotRow.Cells[i].Value = state.Substring(i - 1, 1);
                                }
                                dgvRstatus.Rows.Add(robotRow);
                                break;
                            case "Aligner":
                                DataGridViewRow alignerRow = (DataGridViewRow)dgvAstatus.Rows[0].Clone();
                                alignerRow.Cells[0].Value = each.Name;
                                state = ((AlignerState)StateUtil.device[each.Name]).State;
                                for (int i = 1; i <= state.Length; i++)
                                {
                                    alignerRow.Cells[i].Value = state.Substring(i - 1, 1);
                                }
                                dgvAstatus.Rows.Add(alignerRow);
                                break;
                            case "LoadPort":
                                DataGridViewRow portRow = (DataGridViewRow)dgvLstatus.Rows[0].Clone();
                                portRow.Cells[0].Value = each.Name;
                                state = ((LoadPortState)StateUtil.device[each.Name]).State;
                                for (int i = 1; i <= state.Length; i++)
                                {
                                    portRow.Cells[i].Value = state.Substring(i - 1, 1);
                                }
                                dgvLstatus.Rows.Add(portRow);
                                break;
                        }
                    }
                    //else
                    //{
                    //    continue;
                    //    //do nothing 以下為假資料
                    //    switch (each.Type)
                    //    {
                    //        case "Robot":
                    //            DataGridViewRow robotRow = (DataGridViewRow)dgvRstatus.Rows[0].Clone();
                    //            //robotRow.Cells["Robot"].Value = each.Name;
                    //            robotRow.Cells[0].Value = each.Name;
                    //            for (int i = 1; i <= 32; i++)
                    //            {
                    //                robotRow.Cells[i].Value = i % 2;
                    //            }
                    //            dgvRstatus.Rows.Add(robotRow);
                    //            break;
                    //        case "Aligner":
                    //            DataGridViewRow alignerRow = (DataGridViewRow)dgvAstatus.Rows[0].Clone();
                    //            //alignerRow.Cells["Aligner"].Value = each.Name;
                    //            alignerRow.Cells[0].Value = each.Name;
                    //            for (int i = 1; i <= 32; i++)
                    //            {
                    //                alignerRow.Cells[i].Value = i / 2;
                    //            }
                    //            dgvAstatus.Rows.Add(alignerRow);
                    //            break;
                    //        case "LoadPort":
                    //            DataGridViewRow portRow = (DataGridViewRow)dgvLstatus.Rows[0].Clone();
                    //            //portRow.Cells["Load Port No"].Value = each.Name;
                    //            portRow.Cells[0].Value = each.Name;
                    //            for (int i = 1; i <= 20; i++)
                    //            {
                    //                portRow.Cells[i].Value = i % 2;
                    //            }
                    //            dgvLstatus.Rows.Add(portRow);
                    //            break;
                    //    }
                    //}
                }
            }
            catch (Exception)
            {

            }

        }

        public void getStatus()
        {
            try
            {                
                foreach (Node each in NodeManagement.GetList())
                {
                    IController Ctrl = ControllerManagement.Get(each.Controller);
                    string ctrl_status = ControllerManagement.Get(each.Controller).Status;
                    if (ctrl_status.Equals("Connected"))
                    {
                        string type = "";
                        string seq = "";
                        string result = "";
                        Transaction txn = new Transaction();
                        if (each.Brand.ToUpper().Equals("KAWASAKI"))
                        {
                            seq = Ctrl.GetNextSeq();
                        }
                        else
                        {
                            seq = "";
                        }
                        switch (each.Type)
                        {
                            case "Robot":
                                txn.Method = Transaction.Command.RobotType.GetStatus;
                                break;
                            case "Aligner":
                                txn.Method = Transaction.Command.AlignerType.GetStatus;
                                break;
                            case "LoadPort":
                                txn.Method = Transaction.Command.LoadPortType.ReadStatus;
                                break;
                        }
                        txn.FormName = "FormStatus";
                        txn.AdrNo = each.AdrNo;
                        txn.Seq = seq;
                        if (!txn.Method.Equals(""))
                        {
                            each.SendCommand(txn);
                        }
                        MessageBox.Show(type + each.Name + "-查詢狀態:" + result, "Info");
                    }
                }
            }
            catch (Exception)
            {

            }
        }
    }
}
