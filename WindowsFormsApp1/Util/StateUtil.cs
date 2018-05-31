using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Adam.Util
{
    class StateUtil
    {
        static public Dictionary<string, object> device = new Dictionary<string, object>();
        //static private Dictionary<string, AlignerState> aligner = new Dictionary<string, AlignerState>();
        //static private Dictionary<string, LoadPortState> port = new Dictionary<string, LoadPortState>();
        static private RobotState robot1 = new RobotState("Robot01");
        static private RobotState robot2 = new RobotState("Robot02");
        static private AlignerState aligner1 = new AlignerState("Aligner01");
        static private AlignerState aligner2 = new AlignerState("Aligner02");

        //Robot   $1GET:RIO__:no[CR]   4 R-Hold Status 回饋 R 軸 Wafer/ Panel 保留狀態
        //                             5 L-Hold Status 回饋 L 軸 Wafer/ Panel 保留狀態
        //                             8:回饋 R 軸在席 Sensor 的狀態 
        //                             9:回饋 L 軸在席 Sensor 的狀態
        //Aligner $1GET:RIO__:no[CR]   4 Hold Status 回饋 Wafer/ Panel 保留狀態
        //                             8 Present 回饋在席 Sensor 的狀態
        public static void Init()
        {
            device.Clear();
            device.Add("Robot01", robot1);
            device.Add("Robot02", robot2);
            device.Add("Aligner01", aligner1);
            device.Add("Aligner02", aligner2);
        }
        public static void UpdateSTS(string device, string msg)
        {
            switch (device)
            {
                case "Robot01":
                    robot1.State = msg;
                    break;
                case "Robot02":
                    robot2.State = msg;
                    break;
            }
        }
        public static void UpdateRIO(string device, string msg)
        {
            RobotState robot = null;
            switch (device)
            {
                case "Robot01":
                    robot = robot1;
                    break;
                case "Robot02":
                    robot = robot2;
                    break;
                default:
                    return;
            }
            if(msg == null || msg.IndexOf(",") < 0)
                return;
            string[] result = msg.Split(',');
            switch (result[0])
            {
                case "004":
                    robot.Vacuum_R = result[1];
                    break;
                case "005":
                    robot.Vacuum_L = result[1];
                    break;
                case "008":
                    robot.Present_R = result[1];
                    break;
                case "009":
                    robot.Present_L = result[1];
                    break;
            }
        }
        public static void UpdateSP(string device, string msg)
        {
            switch (device)
            {
                case "Robot01":
                    robot1.Speed = msg;
                    break;
                case "Robot02":
                    robot2.Speed = msg;
                    break;
            }
        }
        public static object GetDeviceState(string name)
        {
            return device[name];
        }
        //public static void updateMode()
        //{

        //}
    }
    class RobotState
    {
        string _name;
        string _status;
        string _state;
        string _present_R;
        string _present_L;
        string _vacuum_L;
        string _vacuum_R;
        string _speed;
        string _mode;
        public RobotState(string name)
        {
            this.Name = name;
            this.Status = "N/A";
            this.State = "".PadLeft(32);
            this.Present_L = "";
            this.Present_R = "";
            this.Vacuum_L = "";
            this.Vacuum_R = "";
            this.Speed = "";
            this.Mode = "";
        }

        public string Name { get => _name; set => _name = value; }
        public string Status { get => _status; set => _status = value; }
        public string State { get => _state; set => _state = value; }
        public string Present_R { get => _present_R; set => _present_R = value; }
        public string Present_L { get => _present_L; set => _present_L = value; }
        public string Vacuum_L { get => _vacuum_L; set => _vacuum_L = value; }
        public string Vacuum_R { get => _vacuum_R; set => _vacuum_R = value; }
        public string Speed { get => _speed; set => _speed = value; }
        public string Mode { get => _mode; set => _mode = value; }
    }
    class AlignerState
    {
        string _name;
        string _status;
        string _state;
        string _present;
        string _vacuum;
        string _speed;
        string _mode;
        public AlignerState(string name)
        {
            this.Name = name;
            this.Status = "N/A";
            this.State = "".PadLeft(32);
            this.Present = "";
            this.Vacuum = "";
            this.Speed = "";
            this.Mode = "";
        }
        public string Name { get => _name; set => _name = value; }
        public string Status { get => _status; set => _status = value; }
        public string State { get => _state; set => _state = value; }
        public string Present { get => _present; set => _present = value; }
        public string Vacuum { get => _vacuum; set => _vacuum = value; }
        public string Speed { get => _speed; set => _speed = value; }
        public string Mode { get => _mode; set => _mode = value; }
    }
    class LoadPortState
    {
        string _name;
        string _status;
        string _state;
        public LoadPortState(string name)
        {
            _name = name;
        }
    }
}
