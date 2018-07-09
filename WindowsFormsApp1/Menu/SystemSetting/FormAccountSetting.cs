using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using SANWA.Utility;
using System.Linq;

namespace Adam.Menu.SystemSetting
{
    public partial class FormAccountSetting : Form
    {
        public FormAccountSetting()
        {
            InitializeComponent();
        }

        private string strUserGroup = string.Empty;
        private DataTable dtAccountGroup = new DataTable();
        private DataTable dtAccountUser = new DataTable();
        private DBUtil dBUtil = new DBUtil();

        private void FormAccountSetting_Load(object sender, EventArgs e)
        {
            try
            {
                UpdateUser();
                UpdateGroup();

                if (dtAccountGroup.Rows.Count > 0)
                {
                    foreach (DataRow item in dtAccountGroup.Rows)
                    {
                        trvAccount.Nodes.Add(item["user_group_id"].ToString(), item["user_group_id"].ToString());
                        trvAccount.Nodes[item["user_group_id"].ToString()].Tag = 0;
                    }

                    foreach (DataRow item in dtAccountGroup.Rows)
                    {
                        trvGroupCondition.Nodes.Add(item["user_group_id"].ToString(), item["user_group_id"].ToString());
                        trvGroupCondition.Nodes[item["user_group_id"].ToString()].Tag = 0;
                    }

                    if (dtAccountUser.Rows.Count > 0)
                    {
                        foreach (DataRow item in dtAccountUser.Rows)
                        {
                            if (trvAccount.Nodes.ContainsKey(item["user_group_id"].ToString()))
                            {
                                trvAccount.Nodes[item["user_group_id"].ToString()].Nodes.Add(item["user_id"].ToString(), item["user_id"].ToString() + " " + item["user_name"].ToString());
                                trvAccount.Nodes[item["user_group_id"].ToString()].Nodes[item["user_id"].ToString()].Tag = 1;
                            }
                        }
                    }

                    cmbGroup.DataSource = dtAccountGroup;
                    cmbGroup.DisplayMember = "user_group_id";
                    cmbGroup.ValueMember = "user_group_id";
                    cmbGroup.SelectedIndex = -1;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void trvAccount_AfterSelect(object sender, TreeViewEventArgs e)
        {
            string[] strsNodes = null;

            try
            {
                strsNodes = ((TreeView)sender).SelectedNode.FullPath.Split(Convert.ToChar(92));

                if (strsNodes.Length >= 2)
                {
                    var query = (from a in dtAccountUser.AsEnumerable()
                                 where a.Field<string>("user_group_id") == strsNodes[0].ToString()
                                   && a.Field<string>("user_id") == strsNodes[strsNodes.Length - 1].Split(' ')[0].ToString()
                                 select a).ToList();

                    if (query.Count > 0)
                    {
                        txbUserID.Text = query[0]["user_id"].ToString();
                        txbUserName.Text = query[0]["user_name"].ToString();
                        txbPassword.Text = query[0]["password"].ToString();
                        cmbGroup.SelectedValue = query[0]["user_group_id"].ToString();
                        chbActive.Checked = query[0]["active"].ToString() == "Y" ? true : false;
                    }
                    else
                    {
                        txbUserID.Text = string.Empty;
                        txbUserName.Text = string.Empty;
                        txbPassword.Text = string.Empty;
                        cmbGroup.SelectedIndex = -1;
                        chbActive.Checked = false;
                    }
                }
                else
                {
                    txbUserID.Text = string.Empty;
                    txbUserName.Text = string.Empty;
                    txbPassword.Text = string.Empty;
                    cmbGroup.SelectedIndex = -1;
                    chbActive.Checked = false;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void UpdateUser()
        {

            string strSqlAccount = string.Empty;

            try
            {
                strSqlAccount = "select * from account where active = 'Y'";
                dtAccountUser = dBUtil.GetDataTable(strSqlAccount, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void UpdateGroup()
        {
            string strSqlGroup = string.Empty;

            try
            {
                strSqlGroup = "select * from user_group where active = 'Y'";
                dtAccountGroup = dBUtil.GetDataTable(strSqlGroup, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }
    }
}
