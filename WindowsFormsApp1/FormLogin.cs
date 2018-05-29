﻿using Adam.Util;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GUI
{
    public partial class FormLogin : Form
    {
        public FormLogin()
        {
            InitializeComponent();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            
            Boolean result = false;
            //set SQL
            StringBuilder sql = new StringBuilder();
            sql.Append("\n SELECT user_id, user_name, user_group_id");
            sql.Append("\n   FROM account ");
            sql.Append("\n  WHERE user_id = @user_id ");
            sql.Append("\n    AND password = MD5(@password)");
            //sql.Append("\n    AND password = MD5('" + tbPassword.Text + "') OR TRUE");

            //sql.Append("\n    AND password = MD5(':password')");
            //set parameter
            Dictionary<string, object> param = new Dictionary<string, object>();
            param.Add("@user_id", tbUserID.Text);
            param.Add("password", tbPassword.Text);
            //Query
            DBUtil dBUtil = new DBUtil();
            DataTableReader rs = dBUtil.GetDataReader(sql.ToString(), param);
            if(rs != null)
            {
                //Console.Write("\n ID:" + rs["user_id"] + " Password:" + rs["password"] + " MD5:" + rs["md5"]);

                string user_id;
                string user_name;
                string user_group_id;
                while (rs.Read())
                {
                    user_id = (string) rs["user_id"];
                    user_name = (string)rs["user_name"];
                    user_group_id = (string)rs["user_group_id"];
                    MessageBox.Show(user_id + " " + user_name + " " + user_group_id + " ", "Message");
                    result = true;
                }
                rs.Close();
                if (result)
                {
                    MessageBox.Show(tbUserID.Text + "\n" + tbPassword.Text, "登入成功");
                }
                else
                {
                    MessageBox.Show(tbUserID.Text + "\n" + tbPassword.Text, "登入失敗");
                }
            }

        }
    }
}
