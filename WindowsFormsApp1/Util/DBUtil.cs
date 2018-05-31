using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam.Util
{
    class DBUtil
    {
        MySqlConnection Connection_;

        private void open_Conn()
        {
            //string connectionStr = "server=192.168.5.127;user id=sanwa;password=sanwa_adam;database=adam";
            string connectionStr = "server=192.168.5.126;port=3307;user id=sanwa;password=sanwa_adam;database=adam";
            Connection_ = new MySqlConnection(connectionStr);
            Connection_.Open();
            //MessageBox.Show("Connect OK!");
        }

        private void close_Conn()
        {
            if (Connection_ != null)
            {
                Connection_.Close();
                //MessageBox.Show("Connect Close!");
            }
        }

        /// <summary>
        /// 取得 MySqlDataReader 
        /// while (data.Read())
        ///    {
        ///     //以欄位名稱取得資料並列出
        ///      Console.WriteLine("id={0} , name={1}", data["list_id"], data["list_name"]);
        ///     //以欄位順序取得資料並列出
        ///        Console.WriteLine("id={0} , name={1}", data[0], data[1]);
        ///    }
        ///data.Close();
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">參數</param>
        /// <returns></returns>
        public DataTableReader GetDataReader(string sql, Dictionary<string, object> parameters)
        {
            DataTableReader reader = null;
            try
            {
                //sql = "SELECT * FROM list_item";
                open_Conn();
                MySqlCommand command = new MySqlCommand(sql, Connection_);
                // set parameters
                foreach (KeyValuePair<string, object> param in parameters)
                {
                    command.Parameters.AddWithValue(param.Key, param.Value);
                }
                //get query result
                MySqlDataReader rs = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(rs);
                reader =  dt.CreateDataReader();
                close_Conn();
            }
            catch(Exception e)
            {
                Console.Write(sql);
            }
            return reader;
        }

        /// <summary>
        /// 取得 DataAdapter , 可做為dataGridView 的 source
        /// DataTable dataTable = new DataTable();
        /// adapter.Fill(dataTable);
        /// dataGridViewMariaDB.DataSource = dataTable;
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public MySqlDataAdapter GetDataAdapter(string sql)
        {
            //sql = "SELECT * FROM list_item";
            open_Conn();
            MySqlDataAdapter adapter = new MySqlDataAdapter(sql, Connection_);
            close_Conn();
            return adapter;
        }

        /// <summary>
        /// 執行非 Query 類 SQL
        /// </summary>
        /// <param name="sql">SQL</param>
        /// <param name="parameters">參數</param>
        /// <returns>影響筆數</returns>
        public int ExecuteNonQuery(string sql, Dictionary<string, object> parameters)
        {
            //sql = string.Format("UPDATE list_item SET modify_timestamp = NOW()");
            open_Conn();
            MySqlCommand command = new MySqlCommand(sql, Connection_);
            // set parameters
            foreach (KeyValuePair<string, object> param in parameters)
            {
                command.Parameters.AddWithValue(param.Key, param.Value);
            }
            int affectLines = command.ExecuteNonQuery();
            close_Conn();
            return affectLines;
        }

    }
}
