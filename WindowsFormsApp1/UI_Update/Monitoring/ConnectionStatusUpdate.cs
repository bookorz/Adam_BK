﻿using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adam.UI_Update.Monitoring
{
    class ConnectionStatusUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(ConnectionStatusUpdate));
        class ConnectState
        {
            
            public string Device_ID { get; set; }
            public string Connection_Status { get; set; }
        }

        delegate void UpdateController(string Device_ID, string Status);
        delegate void UpdateOnline(string Status);

        public static void UpdateInitial(string Status)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                Button Init;
                if (form == null)
                    return;

                Init = form.Controls.Find("Initial_btn", true).FirstOrDefault() as Button;
                if (Init == null)
                    return;

                if (Init.InvokeRequired)
                {
                    UpdateOnline ph = new UpdateOnline(UpdateInitial);
                    Init.BeginInvoke(ph, Status);
                }
                else
                {
                    if (Status.ToUpper().Equals("TRUE"))
                    {
                        Init.BackColor = Color.Lime;
                    }
                    else
                    {
                        Init.BackColor = Color.Red;
                    }

                }


            }
            catch
            {
                logger.Error("UpdateInitial: Update fail.");
            }
        }

        public static void UpdateOnlineStatus(string Status)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                Button Online;
                if (form == null)
                    return;

                Online = form.Controls.Find("Connection_btn", true).FirstOrDefault() as Button;
                if (Online == null)
                    return;

                if (Online.InvokeRequired)
                {
                    UpdateOnline ph = new UpdateOnline(UpdateOnlineStatus);
                    Online.BeginInvoke(ph, Status);
                }
                else
                {
                    switch (Status)
                    {
                        case "Online":
                            Online.Tag = "Online";
                            Online.Text = "Online";
                            Online.BackColor = Color.Lime;
                            break;
                        case "Connecting":
                            Online.Tag = "Connecting";
                            Online.Text = "Connecting";
                            Online.BackColor = Color.Orange;
                            break;
                        case "Offline":
                            Online.Text = "Offline";
                            Online.Tag = "Offline";
                            Online.BackColor = Color.Red;
                            break;
                    }
                    
                }


            }
            catch
            {
                logger.Error("UpdateOnlineStatus: Update fail.");
            }
        }

        public static void UpdateControllerStatus(string Device_ID, string Status)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                DataGridView Conn_gv;
                if (form == null)
                    return;

                Conn_gv = form.Controls.Find("Conn_gv", true).FirstOrDefault() as DataGridView;
                if (Conn_gv == null)
                    return;

                if (Conn_gv.InvokeRequired)
                {
                    UpdateController ph = new UpdateController(UpdateControllerStatus);
                    Conn_gv.BeginInvoke(ph, Device_ID, Status);
                }
                else
                {
                    if(Conn_gv.DataSource == null)
                    {
                        Conn_gv.DataSource = new List<ConnectState>();
                    }
                    List<ConnectState> connList = (List<ConnectState>)Conn_gv.DataSource;

                    var find = from Ctrl in connList
                               where Ctrl.Device_ID.Equals(Device_ID)
                               select Ctrl;

                    if (find.Count() == 0)
                    {
                        ConnectState con = new ConnectState();
                        con.Device_ID = Device_ID;
                        con.Connection_Status = Status;
                        connList.Add(con);
                    }
                    else
                    {
                        find.First().Connection_Status = Status;
                    }
                    connList.Sort((x, y) => { return x.Device_ID.CompareTo(y.Device_ID); });
                    Conn_gv.DataSource = null;
                    Conn_gv.DataSource = connList;
                    //Conn_gv.Refresh();
                    Conn_gv.ClearSelection();
                }


            }
            catch
            {
                logger.Error("UpdateControllerStatus: Update fail.");
            }
        }

        public static void UpdateDIOStatus(string Device_ID, string Status)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                DataGridView Conn_gv;
                if (form == null)
                    return;

                Conn_gv = form.Controls.Find("DIO_gv", true).FirstOrDefault() as DataGridView;
                if (Conn_gv == null)
                    return;

                if (Conn_gv.InvokeRequired)
                {
                    UpdateController ph = new UpdateController(UpdateDIOStatus);
                    Conn_gv.BeginInvoke(ph, Device_ID, Status);
                }
                else
                {
                    if (Conn_gv.DataSource == null)
                    {
                        Conn_gv.DataSource = new List<ConnectState>();
                    }
                    List<ConnectState> connList = (List<ConnectState>)Conn_gv.DataSource;

                    var find = from Ctrl in connList
                               where Ctrl.Device_ID.Equals(Device_ID)
                               select Ctrl;

                    if (find.Count() == 0)
                    {
                        ConnectState con = new ConnectState();
                        con.Device_ID = Device_ID;
                        con.Connection_Status = Status;
                        connList.Add(con);
                    }
                    else
                    {
                        find.First().Connection_Status = Status;
                    }
                    connList.Sort((x, y) => { return x.Device_ID.CompareTo(y.Device_ID); });
                    Conn_gv.DataSource = null;
                    Conn_gv.DataSource = connList;
                    //Conn_gv.Refresh();
                    Conn_gv.ClearSelection();
                }


            }
            catch
            {
                logger.Error("UpdateControllerStatus: Update fail.");
            }
        }
    }
}
