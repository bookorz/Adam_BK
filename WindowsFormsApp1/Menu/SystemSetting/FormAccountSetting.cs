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
            string strUserID = string.Empty;
            string strUserName = string.Empty;
            string strUserGroup = string.Empty;
            StringBuilder sbAllGroup = new StringBuilder();
            StringBuilder sbLowerGroup = new StringBuilder();
            Form form = null;
            Label Signal = null;

            try
            {
                form = Application.OpenForms["FormMain"];
                Signal = form.Controls.Find("lbl_login_id", true).FirstOrDefault() as Label;
                strUserID = Signal.Text;
                Signal = form.Controls.Find("lbl_login_name", true).FirstOrDefault() as Label;
                strUserName = Signal.Text;
                Signal = form.Controls.Find("lbl_login_group", true).FirstOrDefault() as Label;
                strUserGroup = Signal.Text;

                UpdateUser(strUserID, null);
                UpdateGroup();

                if (dtAccountGroup.Rows.Count > 0)
                {
                    foreach (DataRow item in dtAccountGroup.Rows)
                    {
                        if (!trvAccount.Nodes.ContainsKey(item["user_group_id"].ToString()) && item["user_group_id"].ToString().Length > 0)
                        {
                            if (dtAccountUser.Rows[0]["user_group_id"].ToString().Equals(item["user_group_id"].ToString()))
                            {
                                trvAccount.Nodes.Add(item["user_group_id"].ToString(), item["user_group_id"].ToString());
                                trvAccount.Nodes[item["user_group_id"].ToString()].Tag = 0;

                                trvGroupCondition.Nodes.Add(item["user_group_id"].ToString(), item["user_group_id"].ToString());
                                trvGroupCondition.Nodes[item["user_group_id"].ToString()].Tag = 0;
                            }
                        }

                        if (!trvAccount.Nodes.ContainsKey(item["lower_level_group_id"].ToString()) && item["lower_level_group_id"].ToString().Length > 0)
                        {
                            if (strUserGroup.Equals(item["user_group_id"].ToString()))
                            {
                                trvAccount.Nodes.Add(item["lower_level_group_id"].ToString(), item["lower_level_group_id"].ToString());
                                trvAccount.Nodes[item["lower_level_group_id"].ToString()].Tag = 0;

                                trvGroupCondition.Nodes.Add(item["lower_level_group_id"].ToString(), item["lower_level_group_id"].ToString());
                                trvGroupCondition.Nodes[item["lower_level_group_id"].ToString()].Tag = 0;

                                sbAllGroup.Append(item["user_group_id"].ToString());
                                sbAllGroup.Append(",");
                                sbAllGroup.Append(item["lower_level_group_id"].ToString());
                                sbAllGroup.Append(",");

                                sbLowerGroup.Append(item["lower_level_group_id"].ToString());
                                sbLowerGroup.Append(",");
                            }
                        }
                    }

                    if (strUserGroup.Equals("ADMIN"))
                    {
                        UpdateUser();
                        trvAccount.Nodes.Add("CreateUser", "Create User");
                    }
                    else
                    {
                        UpdateUser(strUserID, "'" + sbLowerGroup.ToString().TrimEnd(',').Replace(",", "','") + "'");

                        if (!strUserGroup.Equals("OP"))
                        {
                            trvAccount.Nodes.Add("CreateUser", "Create User");
                        }
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


                    if (strUserGroup.Equals("ADMIN"))
                    {
                        cmbGroup.DataSource = dtAccountGroup.DefaultView.ToTable(true, new string[] { "user_group_id" });
                        cmbGroup.DisplayMember = "user_group_id";
                        cmbGroup.ValueMember = "user_group_id";
                        cmbGroup.SelectedIndex = -1;
                    }
                    else if (strUserGroup.Equals("OP"))
                    {
                        cmbGroup.DataSource = dtAccountGroup.Select("user_group_id = 'OP'").CopyToDataTable().DefaultView.ToTable(true, new string[] { "user_group_id" });
                        cmbGroup.DisplayMember = "user_group_id";
                        cmbGroup.ValueMember = "user_group_id";
                        cmbGroup.SelectedIndex = -1;
                    }
                    else
                    {
                        cmbGroup.DataSource = dtAccountGroup.Select("user_group_id in ('" + sbAllGroup.ToString().Replace(",", "','") + "')").CopyToDataTable().DefaultView.ToTable(true, new string[] { "user_group_id" });
                        cmbGroup.DisplayMember = "user_group_id";
                        cmbGroup.ValueMember = "user_group_id";
                        cmbGroup.SelectedIndex = -1;
                    }
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

        private void UpdateUser(string UserID, string Group)
        {
            string strSqlAccount = string.Empty;
            Dictionary<string, object> keyValues = new Dictionary<string, object>();

            try
            {
                if (UserID == null)
                    return;

                if (Group != null)
                {
                    strSqlAccount = "select * from account where active = 'Y' and user_id = @user_id " +
                        "UNION " +
                        "select * from account where active = 'Y' and user_group_id IN (" + Group + ")";
                    keyValues.Add("@user_id", UserID);
                }
                else
                {
                    strSqlAccount = "select * from account where active = 'Y' and user_id = @user_id";
                    keyValues.Add("@user_id", UserID);
                }

                dtAccountUser = dBUtil.GetDataTable(strSqlAccount, keyValues);
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
                strSqlGroup = "SELECT a.user_group_id, a.user_group_name, b.lower_level_group_id " +
                    "FROM user_group a " +
                    "LEFT JOIN user_group_level b ON a.user_group_id = b.user_group_id " +
                    "WHERE a.active = 'Y'";
                dtAccountGroup = dBUtil.GetDataTable(strSqlGroup, null);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void UIChange(string Mode)
        {
            try
            {
                //switch (Mode)
                //{
                //    default:
                //}
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

        }
    }
}
