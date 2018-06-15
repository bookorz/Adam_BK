using log4net;
using System;
using System.Collections.Generic;
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
                DataGridView CurrNode;
               
                if (form == null)
                    return;
                
                CurrNode = form.Controls.Find(NodeName, true).FirstOrDefault() as DataGridView;
                
                if (CurrNode == null )
                    return;

                if (CurrNode.InvokeRequired)
                {
                    UpdateNode ph = new UpdateNode(UpdateNodesJob);
                    CurrNode.BeginInvoke(ph, NodeName);
                }
                else
                {
                    List<Job> tmp = NodeManagement.Get(CurrNode.Name).JobList.Values.ToList();
                    tmp.Sort((x, y) => { return -Convert.ToInt16(x.Slot).CompareTo(Convert.ToInt16(y.Slot)); });
                    CurrNode.DataSource = tmp;
                    CurrNode.Columns["Slot"].Width = 30;
                    CurrNode.Columns["Slot"].HeaderText = "";
                    CurrNode.Columns["Job_Id"].Width = 96;
                    CurrNode.Columns["Destination"].Visible = false;
                    CurrNode.Columns["ProcessFlag"].Visible = false;
                    CurrNode.Columns["Piority"].Visible = false;
                    CurrNode.Columns["AlignerFlag"].Visible = false;
                    CurrNode.Columns["OCRFlag"].Visible = false;
                    CurrNode.Columns["AlignerFinished"].Visible = false;
                    CurrNode.Columns["OCRFinished"].Visible = false;
                    CurrNode.Columns["Position"].Visible = false;
                    CurrNode.Columns["FromPort"].Visible = false;
                    CurrNode.Columns["LastNode"].Visible = false;
                    CurrNode.Columns["CurrentState"].Visible = false;
                    CurrNode.Columns["WaitToDo"].Visible = false;
                    CurrNode.Columns["FetchRobot"].Visible = false;
                    CurrNode.Columns["ProcessNode"].Visible = false;
                    CurrNode.Columns["DisplayDestination"].Visible = false;
                    CurrNode.Columns["DestinationSlot"].Visible = false;
                    CurrNode.Columns["MapFlag"].Visible = false;
                    CurrNode.ClearSelection();
              
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
                DataGridView CurrNode;
                DataGridView LastNode;
                if (form == null)
                    return;
                Job jb = JobManagement.Get(JobId);
                if (jb == null)
                {
                    return;
                }

                CurrNode = form.Controls.Find(jb.Position, true).FirstOrDefault() as DataGridView;
                LastNode = form.Controls.Find(jb.LastNode, true).FirstOrDefault() as DataGridView;
                if (CurrNode == null || LastNode == null)
                    return;

                if (CurrNode.InvokeRequired)
                {
                    UpdateNode ph = new UpdateNode(UpdateJobMove);
                    CurrNode.BeginInvoke(ph, JobId);
                }
                else
                {
                    List<Job> tmp = NodeManagement.Get(CurrNode.Name).JobList.Values.ToList();
                    tmp.Sort((x, y) => { return -Convert.ToInt16(x.Slot).CompareTo(Convert.ToInt16(y.Slot)); });
                    CurrNode.DataSource = tmp;
                    //CurrNode.DataSource = NodeManagement.Get(CurrNode.Name).JobList.Values.ToList();
                    CurrNode.Columns["Job_Id"].Visible = false;
                    CurrNode.Columns["Destination"].Visible = false;
                    CurrNode.Columns["ProcessFlag"].Visible = false;
                    CurrNode.Columns["Piority"].Visible = false;
                    CurrNode.Columns["AlignerFlag"].Visible = false;
                    CurrNode.Columns["OCRFlag"].Visible = false;
                    CurrNode.Columns["AlignerFinished"].Visible = false;
                    CurrNode.Columns["OCRFinished"].Visible = false;
                    CurrNode.Columns["Position"].Visible = false;
                    CurrNode.Columns["FromPort"].Visible = false;
                    CurrNode.Columns["LastNode"].Visible = false;
                    CurrNode.Columns["CurrentState"].Visible = false;
                    CurrNode.Columns["WaitToDo"].Visible = false;
                    CurrNode.Columns["FetchRobot"].Visible = false;
                    CurrNode.Columns["ProcessNode"].Visible = false;
                    CurrNode.Columns["DisplayDestination"].Visible = false;
                    CurrNode.Columns["DestinationSlot"].Visible = false;
                    tmp = NodeManagement.Get(LastNode.Name).JobList.Values.ToList();
                    tmp.Sort((x, y) => { return -Convert.ToInt16(x.Slot).CompareTo(Convert.ToInt16(y.Slot)); });
                    LastNode.DataSource = tmp;
                    //LastNode.DataSource = NodeManagement.Get(LastNode.Name).JobList.Values.ToList();
                    LastNode.Columns["Job_Id"].Visible = false;
                    LastNode.Columns["Destination"].Visible = false;
                    LastNode.Columns["ProcessFlag"].Visible = false;
                    LastNode.Columns["Piority"].Visible = false;
                    LastNode.Columns["AlignerFlag"].Visible = false;
                    LastNode.Columns["OCRFlag"].Visible = false;
                    LastNode.Columns["AlignerFinished"].Visible = false;
                    LastNode.Columns["OCRFinished"].Visible = false;
                    LastNode.Columns["Position"].Visible = false;
                    LastNode.Columns["FromPort"].Visible = false;
                    LastNode.Columns["LastNode"].Visible = false;
                    LastNode.Columns["CurrentState"].Visible = false;
                    LastNode.Columns["WaitToDo"].Visible = false;
                    LastNode.Columns["FetchRobot"].Visible = false;
                    LastNode.Columns["ProcessNode"].Visible = false;
                    LastNode.Columns["DisplayDestination"].Visible = false;
                    LastNode.Columns["DestinationSlot"].Visible = false;
                    CurrNode.ClearSelection();
                    LastNode.ClearSelection();
                }


            }
            catch
            {
                logger.Error("UpdateJobMove: Update fail.");
            }
        }
    }
}
