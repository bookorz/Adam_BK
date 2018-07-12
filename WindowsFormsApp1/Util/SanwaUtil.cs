using log4net;
using SANWA.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam.Util
{
    static class SanwaUtil
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(SanwaUtil));
        static public string GetArmID(string arm)
        {
            Dictionary<string, string> armMap = new Dictionary<string, string>();
            armMap["Upper"] = "1";
            armMap["Lower"] = "2";
            armMap["Both"] = "3";
            armMap["R"] = "1";
            armMap["L"] = "2";
            return armMap[arm];
        }
        static public void addActionLog(string function_type, string action_type, string action_user)
        {
            try
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("\n INSERT INTO log_system_action");
                sql.Append("\n   (action_id, function_type, action_type, action_user, action_time)");
                sql.Append("\n  SELECT UUID(), @function_type, @action_type, @action_user, CURRENT_TIMESTAMP(6)");

                Dictionary<string, object> param = new Dictionary<string, object>();
                param.Add("@function_type", function_type);
                param.Add("@action_type", action_type);
                param.Add("@action_user", action_user);

                //Query
                DBUtil dBUtil = new DBUtil();
                dBUtil.ExecuteNonQuery(sql.ToString(), param);
                dBUtil = null;
            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
            

        }
    }
}
