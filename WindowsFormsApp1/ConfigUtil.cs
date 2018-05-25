using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Adam
{
    static class ConfigUtil
    {
        public class digitState
        {
            public string state_name;
            public Dictionary<string, string> define;
            public digitState(string state_name, Dictionary<string, string> define)
            {
                this.state_name = state_name;
                this.define = define;
            }
        }
        static public Color GetStatusColor(string device_type, string vendor, string status)
        {
            Color color = Color.White;
            switch (status)
            {
                case "0":
                    color = Color.LightGray;
                    break;
                case "1":
                    color = Color.LightPink;
                    break;
                case "2":
                    color = Color.Yellow;
                    break;
                case "3":
                    color = Color.DimGray;
                    break;
                case "4":
                    color = Color.DarkViolet;
                    break;
                case "5":
                    color = Color.LightGreen;
                    break;
                case "?":
                    color = Color.Gray;
                    break;
            }
            return color;
        }
        //Combobox 使用
        static public List<KeyValuePair<string, int>> GetPointList()
        {
            var list = new List<KeyValuePair<string, int>>() {
                new KeyValuePair<string, int>("Port1", 1201),
                new KeyValuePair<string, int>("Port2", 1202),
                new KeyValuePair<string, int>("Port3", 1203),
                new KeyValuePair<string, int>("Port4", 1204),
                new KeyValuePair<string, int>("Port5", 1205),
                new KeyValuePair<string, int>("Port6", 1206),
                new KeyValuePair<string, int>("Port7", 1207),
                new KeyValuePair<string, int>("Port8", 1208),
                new KeyValuePair<string, int>("Aligner1", 121),
                new KeyValuePair<string, int>("Aligner2", 122),
            };
            return list;
        }

        static public string GetStagePoint(string stage)
        {
            Dictionary<string, string> pointMap = new Dictionary<string, string>();
            pointMap["Port1"] = "1201";
            pointMap["Port2"] = "1202";
            pointMap["Port3"] = "1203";
            pointMap["Port4"] = "1204";
            pointMap["Port5"] = "1205";
            pointMap["Port6"] = "1206";
            pointMap["Port7"] = "1207";
            pointMap["Port8"] = "1208";
            pointMap["LoadPort1"] = "1201";
            pointMap["LoadPort2"] = "1202";
            pointMap["LoadPort3"] = "1203";
            pointMap["LoadPort4"] = "1204";
            pointMap["LoadPort5"] = "1205";
            pointMap["LoadPort6"] = "1206";
            pointMap["LoadPort7"] = "1207";
            pointMap["LoadPort8"] = "1208";
            pointMap["Aligner1"] = "121";
            pointMap["Aligner2"] = "122";
            pointMap["Aligner01"] = "121";
            pointMap["Aligner02"] = "122";  
            return pointMap[stage];
        }
        static public string GetArmID(string arm)
        {
            Dictionary<string, string> armMap = new Dictionary<string, string>();
            armMap["Upper"] = "0";
            armMap["Lower"] = "1";
            armMap["R"] = "0";
            armMap["K"] = "1";
            return armMap[arm];
        }
        static public digitState[] GetRobotStatusItem(string vendor)
        {
            digitState[] states ; 
            switch (vendor){
                case "Status":
                    states = new digitState[32];
                    // 1 Boot up 0=Start, 1=Start finish
                    states[0] = new digitState("Boot up", new Dictionary<string, string>() { { "0", "Start" }, { "1", "Start finish" }});

                    // 2 Communication type 1=Serial, 2=PLC, 3=Teaching Pendant
                    states[1] = new digitState("Communication type", new Dictionary<string, string>() { { "1", "Serial" }, { "2", "PLC" }, { "3", "Teaching Pendant" } });

                    // 3 Busy state 0= Robot is busy, 1= Robot is at wait state
                    states[2] = new digitState("Busy stat", new Dictionary<string, string>() { { "0", "Busy" }, { "1", "Wait" }});

                    // 4 Robot can accept command 0= Robot can’t accept command, 1= Robot can accept command
                    states[3] = new digitState("Accept command", new Dictionary<string, string>() { { "0", "Can't accept" }, { "1", "Can accept" }});

                    // 5 Motion state 0=Stop, 1=Moving
                    states[4] = new digitState("Motion state", new Dictionary<string, string>() { { "0", "Stop" }, { "1", "Moving" } });

                    // 6 Pause state 0=Normal, 1= Paused
                    states[5] = new digitState("Pause state", new Dictionary<string, string>() { { "0", "Normal" }, { "1", "Paused" } });

                    // 7 Abnormal 0= Normal, 1=Abnormal
                    states[6] = new digitState("Abnormal", new Dictionary<string, string>() { { "0", "Normal" }, { "1", "Abnormal" } });

                    // 8 Step mode 0= Normal 1=Step Mode
                    states[7] = new digitState("Step mode", new Dictionary<string, string>() { { "0", "Normal" }, { "1", "Step Mode" } });

                    // 9 Manual/Auto mode 0=Auto Mode 1=Manual Mode
                    states[8] = new digitState("Manual/Auto", new Dictionary<string, string>() { { "0", "Auto" }, { "1", "Manual" } });

                    // 10 Servo State 0=Servo off 1=Servo On
                    states[9] = new digitState("Servo State", new Dictionary<string, string>() { { "0", "Servo off" }, { "1", "Servo On" } });

                    // 11 FAN Fail 0= Normal 1=FAN Fail
                    states[10] = new digitState("FAN state", new Dictionary<string, string>() { { "0", "FAN Normal" }, { "1", "FAN Fail" } });

                    // 12 Encoder power (ABS type) 0= Abnormal (need replace) 1= Normal
                    states[11] = new digitState("Encoder power (ABS type)", new Dictionary<string, string>() { { "0", "Abnormal" }, { "1", "Normal" } });

                    // 13 Arm at interfere zone 0= Normal 1=Arm at interfere zone
                    states[12] = new digitState("Arm at interfere zone", new Dictionary<string, string>() { { "0", "Normal" }, { "1", "Arm at interfere zone" } });

                    // 14 Solenoid valve operating 0= Normal 1= Solenoid valve operating
                    states[13] = new digitState("Solenoid valve operating", new Dictionary<string, string>() { { "0", "Normal" }, { "1", "Solenoid valve operating" } });

                    // 15 ORG search is finished 0= ORG serch is not finished, 1= ORG serch is finished
                    states[14] = new digitState("ORG search is finished", new Dictionary<string, string>() { { "0", "ORG serch is not finished" }, { "1", "ORG serch is finished" } });

                    // 16 Reserve N/A
                    states[15] = new digitState("Reserve N/A", new Dictionary<string, string>() {});

                    // 17 R axis retract to home position 0=R axis is not at home, 1= R axis retract to home
                    states[16] = new digitState(" R axis retract to home position", new Dictionary<string, string>() { { "0", "R axis is not at home" }, { "1", "R axis retract to home" } });
                    
                    // 18 Present Sensor of R axis 0=Present sensor OFF, 1=Present sensor is ON.
                    states[17] = new digitState("Present Sensor of R axis", new Dictionary<string, string>() { { "0", "Present sensor OFF" }, { "1", "Present sensor is ON" } });
                
                    // 19 Vacuum Sensor of R axis 0=Vacuum is not established, 1=Vacuum established
                    states[18] = new digitState("Vacuum Sensor of R axis", new Dictionary<string, string>() { { "0", "Vacuum is not established" }, { "1", "Vacuum established" } }); ;

                    // 20 R-End-EF Position 0 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[19] = new digitState("R-End-EF Position 0 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 21 R-End-EF Position 1 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[20] = new digitState("R-End-EF Position 1 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 22 R-End-EF Position 2 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[21] = new digitState("R-End-EF Position 2 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 23 R-End-EF Position 3 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[22] = new digitState("R-End-EF Position 3 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 24 SHOCK Sensor of R axis 0=R axis is not crashed, 1= R axis is crashed
                    states[23] = new digitState("SHOCK Sensor of R axis", new Dictionary<string, string>() { { "0", "R axis is not crashed" }, { "1", "R axis is crashed" } });

                    // 25 L axis retract to home position 0=L axis is not at home, 1= L axis retract to home
                    states[24] = new digitState("L axis retract to home position", new Dictionary<string, string>() { { "0", "L axis is not at home" }, { "1", "L axis retract to home" } });

                    // 26 Present Sensor of L axis 0=Present sensor OFF, 1=Present sensor is ON.
                    states[25] = new digitState("Present Sensor of L axis", new Dictionary<string, string>() { { "0", "Present sensor OFF" }, { "1", "Present sensor is ON" } });

                    // 27 Vacuum Sensor of L axis 0=Vacuum is not established, 1=Vacuum established
                    states[26] = new digitState("Vacuum Sensor of L axis", new Dictionary<string, string>() { { "0", "Vacuum is not established" }, { "1", "Vacuum established" } });

                    // 28 L-End-EF Position 0 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[27] = new digitState("L-End-EF Position 0 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 29 L-End-EF Position 1 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[28] = new digitState("L-End-EF Position 1 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 30 L-End-EF Position 2 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[29] = new digitState("L-End-EF Position 2 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 31 L-End-EF Position 3 Sensor 0= Position sensor is OFF, 1=Position sensor is ON
                    states[30] = new digitState("L-End-EF Position 3 Sensor", new Dictionary<string, string>() { { "0", "Position sensor is OFF" }, { "1", "Position sensor is ON" } });

                    // 32 SHOCK Sensor of L axis 0=L axis is not crashed, 1= L axis is crashed
                    states[31] = new digitState("SHOCK Sensor of L axis", new Dictionary<string, string>() { { "0", "L axis is not crashed" }, { "1", "L axis is crashed" } });

                    break;
                default:
                    states = new digitState[0];
                    break;
            }         
            return states;          
        }
        static public digitState[] GetLoadPortStatusItem(string vendor)
        {
            digitState[] states;
            switch (vendor)
            {
                case "TDK":
                    states = new digitState[20];

                    // Position Name Description
                    // (1) Equipment status 0 = Normal A = Recoverable error E = Fatal error
                    states[0] = new digitState("Equipment status", new Dictionary<string, string>() { { "0", "Normal" }, { "A", "Recoverable error" }, { "E", "Fatal error" } });

                    // (2) Mode 0 = Online 1 = Teaching
                    states[1] = new digitState("Mode", new Dictionary<string, string>() { { "0", "Online" }, { "1", "Teaching" } });

                    // (3) Initial position 0 = Unexecuted 1 = Executed
                    states[2] = new digitState("Initial position", new Dictionary<string, string>() { { "0", "Unexecuted" }, { "1", "Executed" } });

                    // (4) Operation status 0 = Stopped 1 = Operating
                    states[3] = new digitState("Operation status", new Dictionary<string, string>() { { "0", "Stopped" }, { "1", "Operating" } });

                    // (5) Error code Error code (upper)
                    states[4] = new digitState("Error code Error code (upper)", new Dictionary<string, string>() {});

                    // (6) Error code Error code (lower)
                    states[5] = new digitState("Error code Error code (lower)", new Dictionary<string, string>() {});

                    // (7) Cassette presence 0 = None 1 = Normal position 2 = Error load
                    states[6] = new digitState("Cassette presence", new Dictionary<string, string>() { { "0", "None" }, { "1", "Normal position" }, { "2", "Error load" } });

                    // (8) FOUP clamp status 0 = Open 1 = Close ? = Not defined
                    states[7] = new digitState("FOUP clamp status", new Dictionary<string, string>() { { "0", "Open" }, { "1", "Close" }, { "?", "Not defined" } });

                    // (9) Latch key status 0 = Open 1 = Close ? = Not defined
                    states[7] = new digitState("Latch key status", new Dictionary<string, string>() { { "0", "Open" }, { "1", "Close" }, { "?", "Not defined" } });

                    // (10) Vacuum 0 = OFF 1 = ON
                    states[9] = new digitState("Vacuum", new Dictionary<string, string>() { { "0", "OFF" }, { "1", "ON" } });

                    // (11) Door position 0 = Open position 1 = Close position ? = Not defined
                    states[7] = new digitState("Door position 0", new Dictionary<string, string>() { { "0", "Open position" }, { "1", "Close position" }, { "?", "Not defined" } });

                    // (12) Wafer protrusion sensor 0 = Blocked. 1 = Unblocked.
                    states[11] = new digitState("Wafer protrusion sensor", new Dictionary<string, string>() { { "0", "Blocked" }, { "1", "Unblocked" } });

                    // (13) Z-axis position(*3) 0 = Up position 1 = Down position ? = Not defined 2 = Start position 3 = End position
                    states[7] = new digitState("Z-axis position", new Dictionary<string, string>() { { "0", "Up position" }, { "1", "Down position" }, { "2", "Start position" }, { "3", "End position" }, { "?", "Not defined" } });

                    // (14) Y-axis position 0 = Undock position 1 = Dock position ? = Not defined
                    states[7] = new digitState("Y-axis position", new Dictionary<string, string>() { { "0", "Undock position" }, { "1", " Dock position" }, { "?", "Not defined" } });

                    // (15) Mapper arm position 0 = Open 1 = Close ? = Not defined
                    states[7] = new digitState("Mapper arm position", new Dictionary<string, string>() { { "0", "Open" }, { "1", "Close" }, { "?", "Not defined" } });

                    // (16) Mapper Z-axis (*2) 0 = Retract position 1 = Mapping position ? = Not defined
                    states[7] = new digitState("Mapper Z-axis", new Dictionary<string, string>() { { "0", "Retract position" }, { "1", "Mapping position" }, { "?", "Not defined" } });

                    // (17) Mapper stopper 0 = ON 1 = OFF ? = Not defined
                    states[7] = new digitState(" Mapper stopper", new Dictionary<string, string>() { { "0", "ON" }, { "1", "OFF" }, { "?", "Not defined" } });

                    // (18) Mapping status 0 = Unexecuted 1 = Normal end 2 = Abnormal end
                    states[7] = new digitState("Mapping status", new Dictionary<string, string>() { { "0", "Unexecuted" }, { "1", "Normal" }, { "2", "Abnormal end" } });

                    // (19) Interlock key 0 = Enable 1～3 = Disable
                    states[7] = new digitState("Interlock key", new Dictionary<string, string>() { { "0", "Enable" }, { "1", "Disable" }, { "2", "Disable" }, { "3", "Disable" } });

                    // (20) Info pad (*1) 0 = No input 1 = A-pin ON 2 = B-pin ON 3 = A-pin/B-pin ON
                    states[7] = new digitState("Info pad", new Dictionary<string, string>() { { "0", "No input" }, { "1", "A-pin ON" }, { "2", "B-pin ON" }, { "2", "A-pin/B-pin ON" } });
                    
                    break;
                default:
                    states = new digitState[0];
                    break;
            }
            return states;
        }
    }    
}
