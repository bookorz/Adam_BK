using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using TransferControl.Management;

namespace Adam.Menu.OCR
{
    public partial class FormOCR : Adam.Menu.FormFrame
    {
        public FormOCR()
        {
            InitializeComponent();
        }

        private void OCRButton(object sender, EventArgs e)
        {
            Button TriggerBtn = sender as Button;
            Transaction txn = new Transaction();
            switch (TriggerBtn.Name)
            {
                case "OCR01Online_Btn":
                    OCR01Online_Btn.Enabled = false;
                    OCR01Offline_Btn.Enabled = true;
                    txn.Method = Transaction.Command.OCRType.Online;
                    NodeManagement.Get("OCR01").SendCommand(txn);
                    break;
                case "OCR01Offline_Btn":
                    OCR01Online_Btn.Enabled = true;
                    OCR01Offline_Btn.Enabled = false;
                    txn.Method = Transaction.Command.OCRType.Offline;
                    NodeManagement.Get("OCR01").SendCommand(txn);
                    break;
                case "OCR01Read_Bt":
                    txn.Method = Transaction.Command.OCRType.Read;
                    NodeManagement.Get("OCR01").SendCommand(txn);
                    break;
                case "OCR02Online_Btn":
                    OCR01Online_Btn.Enabled = false;
                    OCR01Offline_Btn.Enabled = true;
                    txn.Method = Transaction.Command.OCRType.Online;
                    NodeManagement.Get("OCR02").SendCommand(txn);
                    break;
                case "OCR02Offline_Btn":
                    OCR01Online_Btn.Enabled = true;
                    OCR01Offline_Btn.Enabled = false;
                    txn.Method = Transaction.Command.OCRType.Offline;
                    NodeManagement.Get("OCR02").SendCommand(txn);
                    break;
                case "OCR02Read_Bt":
                    txn.Method = Transaction.Command.OCRType.Read;
                    NodeManagement.Get("OCR02").SendCommand(txn);
                    break;
            }

        }

        private void FormOCR_Validated(object sender, EventArgs e)
        {

        }
    }
}
