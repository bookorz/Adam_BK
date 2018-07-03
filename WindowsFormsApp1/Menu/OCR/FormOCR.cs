using log4net;
using Newtonsoft.Json;
using SANWA.Utility.Config;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.Menu.OCR
{
    public partial class FormOCR : Adam.Menu.FormFrame
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern long SetWindowPos(IntPtr hwnd, long hWndInsertAfter, long x, long y, long cx, long cy, long wFlags);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool MoveWindow(IntPtr hwnd, int x, int y, int cx, int cy, bool repaint);

        delegate void AssignUI(object param);
        IntPtr appWin2;

        private static readonly ILog logger = LogManager.GetLogger(typeof(FormOCR));
        public FormOCR()
        {
            InitializeComponent();
        }

        private void FormOCR_Load(object sender, EventArgs e)
        {


            foreach (Process p in Process.GetProcessesByName("VB9BReaderForm"))
            {
                p.Kill();
            }
            foreach (Process p in Process.GetProcessesByName("WaferID"))
            {
                p.Kill();
            }

            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadHST));
            ThreadPool.QueueUserWorkItem(new WaitCallback(LoadCOGNEX));
        }

        private void LoadHST(object param)
        {
            try
            {

                SpinWait.SpinUntil(() => false, 1000);
                ProcessStartInfo ps2 = new ProcessStartInfo(SystemConfig.Get().OCR2ExePath, "0");
                Process p1 = Process.Start(ps2);
                SpinWait.SpinUntil(() => false, 1000);
                SpinWait.SpinUntil(() => !Process.GetProcessesByName("VB9BReaderForm")[0].MainWindowTitle.Equals("e-Reader8000Splash"), 60000);

                logger.Debug(Process.GetProcessesByName("VB9BReaderForm")[0].MainWindowTitle);
                logger.Debug("1");

                //SpinWait.SpinUntil(() => false, 3000);
                logger.Debug("2");


                // Put it into this form
                if (this.tabPage1.InvokeRequired)
                {
                    AssignUI ph = new AssignUI(AssignHSTUI);
                    this.tabPage1.BeginInvoke(ph, param);
                }
                else
                {
                    AssignHSTUI(param);
                }


            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
        }

        private void AssignHSTUI(object param)
        {
            Process p2 = Process.GetProcessesByName("VB9BReaderForm")[0];
            appWin2 = p2.MainWindowHandle;
            SetParent(p2.MainWindowHandle, this.tabPage1.Handle);
            MoveWindow(p2.MainWindowHandle, 0, -30, this.tabPage1.Width, this.tabPage1.Height + 30, true);

            ControllerManagement.Get("OCRController02").Connect();
        }

        private void AssignCOGNEXUI(object param)
        {
            Process p2 = Process.GetProcessesByName("WaferID")[0];
            appWin2 = p2.MainWindowHandle;
            SetParent(p2.MainWindowHandle, this.tabPage2.Handle);
            MoveWindow(p2.MainWindowHandle, 0, -30, this.tabPage2.Width, this.tabPage2.Height + 30, true);
        }

        private void LoadCOGNEX(object param)
        {
            try
            {
                SpinWait.SpinUntil(() => false, 1000);
                ProcessStartInfo ps2 = new ProcessStartInfo(SystemConfig.Get().OCR1ExePath);
                Process p2 = Process.Start(ps2);
                //Cognex Wafer ID - 4.5.0
                SpinWait.SpinUntil(() => Process.GetProcessesByName("WaferID")[0].MainWindowTitle.Equals("Cognex Wafer ID - 4.5.0"), 60000);
                //SpinWait.SpinUntil(() => Process.GetProcessesByName("WaferID").Length != 0, 60000);

                logger.Debug("1" + Process.GetProcessesByName("WaferID")[0].MainWindowTitle);
                //SpinWait.SpinUntil(() => false, 3000);
                logger.Debug("2");
                if (this.tabPage1.InvokeRequired)
                {
                    AssignUI ph = new AssignUI(AssignCOGNEXUI);
                    this.tabPage1.BeginInvoke(ph, param);
                }
                else
                {
                    AssignCOGNEXUI(param);
                }

            }
            catch (Exception e)
            {
                logger.Error(e.StackTrace);
            }
        }

        private void FormOCR_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (Process p in Process.GetProcessesByName("VB9BReaderForm"))
            {
                p.Kill();
            }
            foreach (Process p in Process.GetProcessesByName("WaferID"))
            {
                p.Kill();
            }
        }


    }
}
