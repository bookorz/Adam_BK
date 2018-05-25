using log4net;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TransferControl.Engine;

namespace Adam.UI_Update.OCR
{
    class OCRUpdate
    {
        static ILog logger = LogManager.GetLogger(typeof(OCRUpdate));
        delegate void UpdateOCR(string OCRName, string In);


        public static void UpdateOCRRead(string OCRName,string WaferID)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                TextBox Tb_OCRRead;
                if (form == null)
                    return;

                Tb_OCRRead = form.Controls.Find(OCRName + "Read_Tb", true).FirstOrDefault() as TextBox;
                if (Tb_OCRRead == null)
                    return;

                if (Tb_OCRRead.InvokeRequired)
                {
                    UpdateOCR ph = new UpdateOCR(UpdateOCRRead);
                    Tb_OCRRead.BeginInvoke(ph, OCRName, WaferID);
                }
                else
                {
                    string save= "";
                    string src="";
                    if (WaferID.IndexOf("*")!=-1)
                    {
                        WaferID = "辨識失敗";
                    }
                    switch (OCRName)
                    {
                        case "OCR01":
                            save = RouteControl.SysConfig.OCR1ImgToJpgPath;
                            src = RouteControl.SysConfig.OCR1ImgSourcePath;
                            break;
                        case "OCR02":
                            save = RouteControl.SysConfig.OCR2ImgToJpgPath;
                            src = RouteControl.SysConfig.OCR2ImgSourcePath;
                            break;
                    }
                     

                    string savePath = save + "/" + WaferID + "_" + DateTime.Now.ToString("yyyy_mm_dd_HH_MM_ss") + ".jpg";
                    WaferID = WaferID.Replace("[", "").Replace("]", "").Split(',')[0];
                    Tb_OCRRead.Text = WaferID;
                    string[] files = Directory.GetFiles(src);
                    List<string> fileList = files.ToList();
                    if (fileList.Count != 0)
                    {
                        fileList.Sort((x, y) => { return -File.GetLastWriteTime(x).CompareTo(File.GetLastWriteTime(y)); });

                        Image bmp = Image.FromFile(fileList[0]);
                        bmp.Save(savePath, System.Drawing.Imaging.ImageFormat.Jpeg);
                        bmp.Dispose();
                        PictureBox Pic_OCR = form.Controls.Find(OCRName + "_Pic", true).FirstOrDefault() as PictureBox;
                        if (Pic_OCR == null)
                            return;
                        Pic_OCR.Image = Image.FromFile(savePath);
                    }
                }


            }
            catch
            {
                logger.Error("UpdateOCRRead: Update fail.");
            }
        }

        public static void UpdateOCRStatus(string OCRName, string Status)
        {
            try
            {
                Form form = Application.OpenForms["FormMain"];
                Button Btn_OCROnline;
                if (form == null)
                    return;

                Btn_OCROnline = form.Controls.Find(OCRName+"Online_Btn", true).FirstOrDefault() as Button;
                if (Btn_OCROnline == null)
                    return;
                Button Btn_OCROffline = form.Controls.Find(OCRName + "Offline_Btn", true).FirstOrDefault() as Button;
                if (Btn_OCROffline == null)
                    return;
                if (Btn_OCROnline.InvokeRequired)
                {
                    UpdateOCR ph = new UpdateOCR(UpdateOCRStatus);
                    Btn_OCROnline.BeginInvoke(ph, OCRName, Status);
                }
                else
                {
                    switch (Status)
                    {
                        case "1":
                            Btn_OCROnline.Enabled = false;
                            Btn_OCROffline.Enabled = true;
                            break;
                        case "0":
                            Btn_OCROnline.Enabled = true;
                            Btn_OCROffline.Enabled = false;
                            break;
                    }
                }


            }
            catch
            {
                logger.Error("UpdateOCRStatus: Update fail.");
            }
        }
    }
}
