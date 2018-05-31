using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.Menu.Monitoring
{
    public partial class FormMonitoring : Adam.Menu.FormFrame
    {
        public FormMonitoring()
        {
            InitializeComponent();
        }

       
        private void LoadPort_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            switch (e.ColumnIndex)
            {
                case 1:
                    List<Job> JobList = (sender as DataGridView).DataSource as List<Job>;
                    
                    switch (JobList[e.RowIndex].ProcessFlag)
                    {
                        case false:  
                            e.CellStyle.BackColor = Color.Green;
                            e.CellStyle.ForeColor = Color.White;
                            break;

                    }

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
                       

                    }
                    break;

            }
        }
    }
}
