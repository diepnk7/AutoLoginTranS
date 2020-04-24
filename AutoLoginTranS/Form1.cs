using KAutoHelper;
using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using System.Timers;
using System.Windows.Forms;


namespace AutoLoginTranS
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnLazy_Click(object sender, EventArgs e)
        {
            timer.Start();
            WindowState = FormWindowState.Minimized;
        }

        #region LoginTrans

        public void LoginTranS()
        {
            string strCmdText;
            strCmdText = @"/C C:\Users\yoyal\AppData\Roaming\TranS\TranS.exe";

            Process p = new Process();

            p.StartInfo.FileName = "CMD.exe";
            p.StartInfo.Arguments = strCmdText;

            p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;

            p.Start();

            Thread.Sleep(5000);

            //click Student
            var screen = CaptureHelper.CaptureScreen();
            screen.Save("mainScreen.png");
            var subBitmap = ImageScanOpenCV.GetImage("hocVien.bmp");
            var resBitmap = ImageScanOpenCV.FindOutPoint((Bitmap)screen, subBitmap);
            if (resBitmap != null)
            {
                string x = (resBitmap.ToString()).Remove(0, 3).Remove(3, 7);
                string y = (resBitmap.ToString()).Remove(0, 9).Remove(3, 1);
                AutoControl.MouseClick(int.Parse(x) + 20, int.Parse(y) + 20, EMouseKey.LEFT);
            }

            Thread.Sleep(500);

            //Send Enter ID
            try
            {
                AutoControl.SendStringFocus(txtBoxID.Text);
            }
            catch (Exception)
            {
                MessageBox.Show("Bạn chưa nhập ID");
            }
            

            //Click Join now
            var screenID = CaptureHelper.CaptureScreen();
            screenID.Save("mainScreen.png");
            var subBitmapID = ImageScanOpenCV.GetImage("btnJoinnow.bmp");
            var resBitmapID = ImageScanOpenCV.FindOutPoint((Bitmap)screenID, subBitmapID);
            if (resBitmapID != null)
            {
                string x1 = (resBitmapID.ToString()).Remove(0, 3).Remove(3, 7);
                string y1 = (resBitmapID.ToString()).Remove(0, 9).Remove(3, 1);
                AutoControl.MouseClick(int.Parse(x1) + 20, int.Parse(y1) + 20, EMouseKey.LEFT);
            }
            Application.Exit();
        }
        #endregion //LoginTranS

        #region CheckTime
        private void timer_Tick(object sender, EventArgs e)
        {
            string strHour = null;

            if (cbHour.Text != "")
            {

                if (cbMinute.Text != "")
                {
                    strHour = cbHour.Text + ":" + cbMinute.Text + "";
                }
                else
                {
                    strHour = cbHour.Text + ":" + "00" + "";
                }

                //Lấy Giờ Hệ Thống

                string strSystemHour = DateTime.Now.ToShortTimeString();

                if (strHour == strSystemHour)
                {
                    timer.Stop();
                    LoginTranS();
                    //AutoLogin.LoginTranS(txtBoxID.Text.ToString());
                }
                //DateTime someFutureTime = new DateTime();
                //someFutureTime = ConvertDateTime();
                //AlarmClock clock = new AlarmClock(someFutureTime);
                //clock.Alarm += (sender, e) => MessageBox.Show("Wake up!");
            }
        }
        #endregion //CheckTime

    }
}
