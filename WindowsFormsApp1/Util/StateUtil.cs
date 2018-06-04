using log4net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Controller;
using TransferControl.Management;

namespace Adam.Util
{
    class StateUtil
    {
        class ConnectState
        {

            public string Device_ID { get; set; }
            public string Connection_Status { get; set; }
        }
        static ILog logger = LogManager.GetLogger(typeof(StateUtil));
        static public Dictionary<string, object> device = new Dictionary<string, object>();
        //static private Dictionary<string, AlignerState> aligner = new Dictionary<string, AlignerState>();
        //static private Dictionary<string, LoadPortState> port = new Dictionary<string, LoadPortState>();
        static private RobotState robot1 = new RobotState("Robot01");
        static private RobotState robot2 = new RobotState("Robot02");
        static private AlignerState aligner1 = new AlignerState("Aligner01");
        static private AlignerState aligner2 = new AlignerState("Aligner02");

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
                case "Aligner01":
                    aligner1.State = msg;
                    break;
                case "Aligner02":
                    aligner2.State = msg;
                    break;
            }
        }
        public static void UpdateError(string device, string msg)
        {
            switch (device)
            {
                case "Robot01":
                    robot1.Error = msg;
                    break;
                case "Robot02":
                    robot2.Error = msg;
                    break;
                case "Aligner01":
                    aligner1.Error = msg;
                    break;
                case "Aligner02":
                    aligner2.Error = msg;
                    break;
            }
        }

        public static void UpdateMode(string device, string msg)
        {
            switch (device)
            {
                case "Robot01":
                    robot1.Mode = msg;
                    break;
                case "Robot02":
                    robot2.Mode = msg;
                    break;
                case "Aligner01":
                    aligner1.Mode = msg;
                    break;
                case "Aligner02":
                    aligner2.Mode = msg;
                    break;
            }
        }

        public static void UpdateRIO(string device, string msg)
        {
            RobotState robot = null;
            AlignerState aligner = null;
            switch (device)
            {
                case "Robot01":
                    robot = robot1;
                    break;
                case "Robot02":
                    robot = robot2;
                    break;
                case "Aligner01":
                    aligner = aligner1;
                    break;
                case "Aligner02":
                    aligner = aligner2;
                    break;
                default:
                    return;
            }
            if (msg == null || msg.IndexOf(",") < 0)
                return;
            string[] result = msg.Split(',');
            switch (result[0])
            {
                case "004":
                    if (robot != null)
                        robot.Present_R = result[1];
                    if (aligner != null)
                        aligner.Present = result[1];
                    break;
                case "005":
                    if (robot != null)
                        robot.Present_L = result[1];
                    break;
            }
        }

        public static void UpdateSV(string device, string msg)
        {
            RobotState robot = null;
            AlignerState aligner = null;
            switch (device)
            {
                case "Robot01":
                    robot = robot1;
                    break;
                case "Robot02":
                    robot = robot2;
                    break;
                case "Aligner01":
                    aligner = aligner1;
                    break;
                case "Aligner02":
                    aligner = aligner2;
                    break;
                default:
                    return;
            }
            if (msg == null || msg.IndexOf(",") < 0)
                return;
            string[] result = msg.Split(',');
            switch (result[0])
            {
                case "01":
                    if(robot!=null)
                        robot.Vacuum_R = result[1];
                    if (aligner != null)
                        aligner.Vacuum = result[1];
                    break;
                case "02":
                    if (robot != null)
                        robot.Vacuum_L = result[1];
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
                case "Aligner01":
                    aligner1.Speed = msg;
                    break;
                case "Aligner02":
                    aligner2.Speed = msg;
                    break;
            }
        }

        public static object GetDeviceState(string name)
        {
            return device[name];
        }

    }
    class RobotState
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string Present_R { get; set; }
        public string Present_L { get; set; }
        public string Vacuum_L { get; set; }
        public string Vacuum_R { get; set; }
        public string Speed { get; set; }
        public string Mode { get; set; }
        public string Error { get; set; }

        public RobotState(string name)
        {
            Node robot = NodeManagement.Get(name);
            this.Name = name;
            this.Status = robot != null ? robot.State : "N/A";
            if (this.Status.Equals("N/A") && ControllerManagement.Get(name) != null)
                this.Status = ControllerManagement.Get(name).Status;// 如果 NODE 無狀態，改抓 Controller 的狀態
            this.State = "".PadLeft(32);
            this.Present_L = "";
            this.Present_R = "";
            this.Vacuum_L = "";
            this.Vacuum_R = "";
            this.Speed = "";
            this.Mode = "";
            this.Error = "";
        }
        
    }
    class AlignerState
    {
        public string Name { get; set; }
        public string Status { get; set; }
        public string State { get; set; }
        public string Present { get; set; }
        public string Vacuum { get; set; }
        public string Speed { get; set; }
        public string Mode { get; set; }
        public string Error { get; set; }
        public AlignerState(string name)
        {
            Node aligner = NodeManagement.Get(name);
            this.Name = name;
            this.Status = aligner != null ? aligner.State : "N/A";
            this.State = "".PadLeft(32);
            this.Present = "";
            this.Vacuum = "";
            this.Speed = "";
            this.Mode = "";
            this.Error = "";
        }
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
