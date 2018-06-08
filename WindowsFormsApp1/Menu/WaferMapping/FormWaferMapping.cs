using Adam.UI_Update.Monitoring;
using Adam.UI_Update.WaferMapping;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.Menu.WaferMapping
{
    public partial class FormWaferMapping : Adam.Menu.FormFrame
    {
        object CurrentSelected = null;

        public FormWaferMapping()
        {
            InitializeComponent();
        }

        private void FormWaferMapping_Load(object sender, EventArgs e)
        {
            InitialForm(null);
            //ThreadPool.QueueUserWorkItem(new WaitCallback(InitialForm));
        }

        private void InitialForm(object input)
        {
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort01", "111211111?111110111W11111");
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort02", "11WW111112111111011110111");
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort03", "0000000000000000000000000");
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort04", "0000000000000000000000000");
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort05", "11WW111112111111011110111");
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort06", "0000000000000000000000000");
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort07", "0000000000000000000000000");
            //WaferAssignUpdate.UpdateLoadPortMapping("LoadPort08", "0000000000000000000000000");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort01", "LD");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort02", "LD");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort03", "UD");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort04", "UD");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort05", "LD");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort06", "UD");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort07", "UD");
            WaferAssignUpdate.UpdateLoadPortMode("LoadPort08", "UD");
        }

        private void Assign_Gv_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    switch (e.Value)
                    {
                        case "No wafer":
                            e.CellStyle.BackColor = Color.Gray;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        case "Crossed":
                        case "Undefined":                           
                        case "Double":
                            e.CellStyle.BackColor = Color.Red;
                            e.CellStyle.ForeColor = Color.White;
                            break;
                        default:
                            e.CellStyle.BackColor = Color.Green;
                            e.CellStyle.ForeColor = Color.White;
                            break;

                    }
                    break;

            }
        }

        private void Assign_Gv_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ContextMenu m = new ContextMenu();


                string PortName = (sender as DataGridView).Name.Replace("Assign_Gv", "");
                if (NodeManagement.Get(PortName).Mode.Equals("LD"))
                {

                    CurrentSelected = sender;
                    foreach (Node eachPort in NodeManagement.GetLoadPortList("UD"))
                    {
                        List<MenuItem> tmpAry = new List<MenuItem>();
                        for (int i = 1; i <= 25; i++)
                        {
                            MenuItem tmp;
                            if (!eachPort.ReserveList.ContainsKey(i.ToString()))
                            {
                                tmp = new MenuItem(eachPort.Name + "-" + i.ToString(), AssignPort);

                            }
                            else
                            {
                                tmp = new MenuItem(eachPort.Name + "-" + i.ToString(), AssignPort);
                                tmp.Enabled = false;
                            }
                            tmpAry.Add(tmp);
                        }
                        m.MenuItems.Add(eachPort.Name, tmpAry.ToArray());
                    }
                    m.MenuItems.Add(new MenuItem("取消", UnAssignPort));

                }

                m.Show((DataGridView)sender, new Point(e.X, e.Y));

            }
        }

        private void UnAssignPort(object sender, EventArgs e)
        {
            if ((CurrentSelected as DataGridView).SelectedRows.Count == 0)
            {
                MessageBox.Show("請選擇來源Slot");
            }
            else if ((CurrentSelected as DataGridView).SelectedRows.Count == 1)
            {
                string waferId = (CurrentSelected as DataGridView).SelectedRows[0].Cells["Job_Id"].Value.ToString();
                string OrgDest = (CurrentSelected as DataGridView).SelectedRows[0].Cells["Destination"].Value.ToString();
                string OrgDestSlot = (CurrentSelected as DataGridView).SelectedRows[0].Cells["DestinationSlot"].Value.ToString();
                Job wafer = JobManagement.Get(waferId);
                if (wafer != null)
                {
                    wafer.Destination = "";
                    wafer.DisplayDestination = "";
                    wafer.DestinationSlot = "";
                    wafer.ProcessFlag = true;
                    //wafer.Position = PortName;
                    if (!OrgDest.Equals(""))
                    {
                        NodeManagement.Get(OrgDest).RemoveReserve(OrgDestSlot);
                    }

                    (CurrentSelected as DataGridView).Refresh();
                }
                else
                {
                    MessageBox.Show("找不到此Wafer資料:" + wafer.Job_Id);
                }

            }
            else if ((CurrentSelected as DataGridView).SelectedRows.Count > 1)
            {

                foreach (DataGridViewRow each in (CurrentSelected as DataGridView).SelectedRows)
                {
                    string waferId = each.Cells["Job_Id"].Value.ToString();
                    string OrgDest = each.Cells["Destination"].Value.ToString();
                    string OrgDestSlot = each.Cells["DestinationSlot"].Value.ToString();
                    Job wafer = JobManagement.Get(waferId);
                    if (wafer != null)
                    {
                        wafer.Destination = "";
                        wafer.DisplayDestination = "";
                        wafer.DestinationSlot = "";
                        wafer.ProcessFlag = true;
                        if (!OrgDest.Equals(""))
                        {
                            NodeManagement.Get(OrgDest).RemoveReserve(OrgDestSlot);
                        }

                    }
                }
                 (CurrentSelected as DataGridView).Refresh();
                JobMoveUpdate.UpdateNodesJob((CurrentSelected as DataGridView).Name.Replace("Assign_Gv", ""));
            }
        }

        private void AssignPort(object sender, EventArgs e)
        {
            string PortName = (sender as MenuItem).Text.Split('-')[0];
            string Slot = (sender as MenuItem).Text.Split('-')[1];
            if ((CurrentSelected as DataGridView).SelectedRows.Count == 0)
            {
                MessageBox.Show("請選擇來源Slot");
            }
            else if ((CurrentSelected as DataGridView).SelectedRows.Count == 1)
            {
                string waferId = (CurrentSelected as DataGridView).SelectedRows[0].Cells["Job_Id"].Value.ToString();
                string OrgDest = (CurrentSelected as DataGridView).SelectedRows[0].Cells["Destination"].Value.ToString();
                string OrgDestSlot = (CurrentSelected as DataGridView).SelectedRows[0].Cells["DestinationSlot"].Value.ToString();

                Job UDSlot = NodeManagement.Get(PortName).GetJob(Slot);
                    if(UDSlot == null)
                {
                    MessageBox.Show(PortName+"沒有FOUP或是尚未進行Mapping");
                    return;
                }
                if (UDSlot.MapFlag == false)
                {
                    Job wafer = JobManagement.Get(waferId);
                    if (wafer != null)
                    {
                        wafer.Destination = PortName;
                        wafer.DisplayDestination = PortName.Replace("Load", "");
                        wafer.DestinationSlot = Slot;
                        wafer.ProcessFlag = false;
                        //wafer.Position = PortName;
                        if (!OrgDest.Equals(""))
                        {
                            NodeManagement.Get(OrgDest).RemoveReserve(OrgDestSlot);
                        }
                        NodeManagement.Get(PortName).AddReserve(Slot, wafer);
                        (CurrentSelected as DataGridView).Refresh();
                        JobMoveUpdate.UpdateNodesJob((CurrentSelected as DataGridView).Name.Replace("Assign_Gv", ""));
                    }
                    else
                    {
                        MessageBox.Show("找不到此Wafer資料");
                    }
                }
            }
            else if ((CurrentSelected as DataGridView).SelectedRows.Count > 1)
            {
                int StartSlot = Convert.ToInt32(Slot);
                foreach (DataGridViewRow each in (CurrentSelected as DataGridView).SelectedRows)
                {
                    string waferId = each.Cells["Job_Id"].Value.ToString();
                    string OrgDest = each.Cells["Destination"].Value.ToString();
                    string OrgDestSlot = each.Cells["DestinationSlot"].Value.ToString();
                    Job wafer = JobManagement.Get(waferId);
                    if (wafer != null)
                    {
                        while (true)
                        {
                            if (NodeManagement.Get(PortName).GetJob(StartSlot.ToString()).MapFlag == false)
                            {
                                wafer.Destination = PortName;
                                wafer.DisplayDestination = PortName.Replace("Load", "");
                                wafer.DestinationSlot = StartSlot.ToString();
                                //wafer.Position = PortName;
                                wafer.ProcessFlag = false;
                                if (!OrgDest.Equals(""))
                                {
                                    NodeManagement.Get(OrgDest).RemoveReserve(OrgDestSlot);
                                }
                                NodeManagement.Get(PortName).AddReserve(StartSlot.ToString(), wafer);

                                break;
                            }
                            else
                            {
                                StartSlot++;
                                if (StartSlot > 25)
                                {
                                    break;
                                }
                            }
                        }
                        StartSlot++;
                        if (StartSlot > 25)
                        {
                            break;
                        }
                    }
                }
                 (CurrentSelected as DataGridView).Refresh();
                JobMoveUpdate.UpdateNodesJob((CurrentSelected as DataGridView).Name.Replace("Assign_Gv", ""));
                
            }


        }

        private void State_lb_Click(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                CurrentSelected = sender;
                ContextMenu m = new ContextMenu();
                m.MenuItems.Add(new MenuItem("Change to LD", PortModeChange));
                m.MenuItems.Add(new MenuItem("Change to UD", PortModeChange));
                m.MenuItems.Add(new MenuItem("Fake Data(Full)", PortModeChange));
                m.MenuItems.Add(new MenuItem("Fake Data(Empty)", PortModeChange));
                m.Show((Label)sender, new Point(e.X, e.Y));
            }
        }

        private void PortModeChange(object sender, EventArgs e)
        {
            string Name = (CurrentSelected as Label).Name.Replace("State_lb", "");
            switch (((MenuItem)sender).Text)
            {
                case "Change to LD":
                    NodeManagement.Get(Name).Mode = "LD";
                    WaferAssignUpdate.UpdateLoadPortMode(Name, "LD");
                    break;
                case "Change to UD":
                    NodeManagement.Get(Name).Mode = "UD";
                    WaferAssignUpdate.UpdateLoadPortMode(Name, "UD");
                    break;
                case "Fake Data(Full)":
                    WaferAssignUpdate.UpdateLoadPortMapping(Name, "111211111?111110111W11111");
                    break;
                case "Fake Data(Empty)":
                    WaferAssignUpdate.UpdateLoadPortMapping(Name, "0000000000000000000000000");
                    break;
            }

        }

        private void PortStart_Btn_Click(object sender, EventArgs e)
        {
            string PortName = (sender as Button).Name.Replace("_Start_Btn", "");
            Node port = NodeManagement.Get(PortName);
            if (port != null)
            {
                if (port.IsMapping)
                {
                    port.Available = true;
                }
                else
                {
                    MessageBox.Show(PortName + " 尚未Mapping");
                }
            }
            else
            {
                MessageBox.Show(PortName + " 不存在");
            }
        }
    }
}
