using m.fb.Properties;
using MCommon;
using OpenQA.Selenium.Chrome;
using OtpNet;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.IO;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace m.fb
{
    public partial class Dangbai : Form
    {




        public Dangbai()
        {
            InitializeComponent();
            CheckForIllegalCrossThreadCalls = false;
        }
        ChromeDriver drv; Thread Th;

        private void Form1_Load(object sender, EventArgs e)
        {
            //if (taikhoan.Text != "")
            //{
            //    login.ForeColor = Color.Linen;
            //    login.Cursor = Cursors.Hand;
            //}

            //else
            //{
            //    login.ForeColor = Color.Red;
            //    login.Cursor = Cursors.No;
            //}

        }
        private string[] path;
        private object contents;
        List<Thread> lstThread = null;
        private object dtgvAcc;

        


        string GetTotp(string input)
        {
            try
            {
                byte[] array = Base32Encoding.ToBytes(input.Trim().Replace(" ", ""));
                Totp totp = new Totp(array, 30, 0, 6, null);
                string otp = totp.ComputeTotp(DateTime.UtcNow);
                return otp;
            }
            catch (Exception ex)
            {
            }
            return "";
        }
        
        
        private void login_Click(object sender, EventArgs e)
        {
            //Th = new Thread(Result); Th.Start();
            int maxThread = 15;
            List<int> lstPossition = new List<int>();
            for (int i = 0; i < maxThread; i++)
                lstPossition.Add(0);
            lstThread = new List<Thread>();
            new Thread(() =>
            {
                try
                {

                    for (int i = 0; i < dgvDatalst.RowCount;)
                    {

                        if (Convert.ToBoolean(dgvDatalst.Rows[i].Cells["cChose"].Value))
                        {
                            if (lstThread.Count < maxThread)
                            {
                                int row = i++;
                                if (row != 0)
                                    Thread.Sleep(1000);
                                Thread thread = new Thread(() =>
                                {
                                    int indexPos = GetIndexOfPossitionApp(ref lstPossition);
                                    try
                                    {
                                        ExcuteOneThread(row, indexPos);
                                        FillIndexPossition(ref lstPossition, indexPos);
                                        SetCellAccount(row, "cChose", false);

                                    }
                                    catch (Exception ex)
                                    {

                                    }
                                });
                                lstThread.Add(thread);
                                thread.Start();
                            }
                            else
                            {

                                for (int j = 0; j < lstThread.Count; j++)
                                {
                                    if (!lstThread[j].IsAlive)
                                        lstThread.RemoveAt(j--);
                                }

                            }
                        }
                        else
                        {
                            i++;
                        }    
                    }

                    for (int i = 0; i < lstThread.Count; i++)
                    {
                        lstThread[i].Join();
                    }

                }
                catch (Exception ex)
                {

                }


            }).Start();
        }

        public static void FillIndexPossition(ref List<int> lstPossition, int indexPos)
        {
            lock (lstPossition)
            {
                lstPossition[indexPos] = 0;
            }
        }

        public static int GetIndexOfPossitionApp(ref List<int> lstPossition)
        {
            int indexPos = 0;
            lock (lstPossition)
            {
                for (int i = 0; i < lstPossition.Count; i++)
                {
                    if (lstPossition[i] == 0)
                    {
                        indexPos = i;
                        lstPossition[i] = 1;
                        break;
                    }
                }
            }
            return indexPos;
        }

        public void SetCellAccount(int indexRow, string column, object value, bool isAllowEmptyValue = true)
        {
            if (column == "cUid" && value.ToString().Trim() == "")
                return;
            if (!isAllowEmptyValue && value.ToString().Trim() == "")
                return;
            //if (column == "cPassword")
            //{
            //    lock (o)
            //    {
            //        File.AppendAllText("changelog.txt", GetCellAccount(indexRow, "cUid") + "|" + GetCellAccount(indexRow, "cPassword") + "|" + value + Environment.NewLine);
            //    }
            //}
            SetStatusDataGridView(dgvDatalst, indexRow, column, value);
        }
        private void ExcuteOneThread(int row, int indexPos)
        {
            //string uid = GetCellAccount(row, "cUid");
            //string pass = GetCellAccount(row, "cPass");
            //string fa2 = GetCellAccount(row, "c2Fa");



            try
            {
                Point PositionChrome = GetPointFromIndexPosition(indexPos, 5, 2);
                Point SizeChrome = GetSizeChrome(5, 2);

                login.ForeColor = Color.Gold;
                login.UseWaitCursor = true; 
                login.Text = "logining...";
                string app = "data:,";
                Chrome chrome = null;
                chrome = new Chrome()
                {
                    UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/100.0.4896.127 Safari/537.36",
                    //ProfilePath = @"C:\TestFb\TestFb\bin\Debug\profiles\100080077767502",
                    Size = SizeChrome,
                    Position = PositionChrome,
                    TimeWaitForSearchingElement = 3,
                    TimeWaitForLoadingPage = 120,
                    //Proxy = proxy,
                    //TypeProxy = typeProxy,
                    DisableSound = true,
                    App = app,

                };
                chrome.Open();
                chrome.GotoURL("https://m.facebook.com/login/");
                chrome.DelayTime(0.5);

                string Tukhoa = Settings.Default.txtTukhoa;
                int Soluong = Settings.Default.Number;
                string Idbv = Settings.Default.txtTukhoabaiviet;
                int Soluogbaiviet = Settings.Default.txtSoluongbaiviet;
                string Coment = Settings.Default.txtComent;
                string tukhoabaiviet = Settings.Default.txtTukhoabaiviet;
                int sobbtt = Settings.Default.txtbb;
                string Dangbai = Settings.Default.txtdangbaibanbe;
                string cmt = Settings.Default.txtcmt;
                int sobaicmt = Settings.Default.soluongcmt;
                string Idbaiviet = Settings.Default.txtIdbaiviet;              
                int Sobaituongtacnewfeed = Settings.Default.txtsobaituongtacnewfeed;
                string Commentnewfeed = Settings.Default.txtComment;
                int soluongcmt = Settings.Default.soluongcmt;
                int soluongnhom = Settings.Default.Slnhom;
                string tukhoanhom = Settings.Default.txtTukhoanhom;
                string answer = Settings.Default.txttraloi;
                string Idnhom = Settings.Default.txtIdnhom;
                


                Loginn(chrome, row);
                Newfeed(chrome, Sobaituongtacnewfeed, Commentnewfeed);
                Banbe(chrome, sobbtt, Dangbai, cmt, soluongcmt);
                Ketban(chrome, Tukhoa, Soluong);
                Status(chrome, tukhoabaiviet, Soluogbaiviet, Coment);
                Thamgianhom(chrome, tukhoanhom, soluongnhom, answer );
                checkid(chrome, Idbaiviet, Idnhom);



            }
            catch
            {

            }

        }

        

        public static Point GetSizeChrome(int column, int row)
        {
            int getWidthChrome = getWidthScreen / column + 15;
            int getHeightChrome = getHeightScreen / row + 10;
            return new Point(getWidthChrome, getHeightChrome);
        }

        public static Point GetPointFromIndexPosition(int indexPos, int column, int row)
        {
            Point location = new Point();
            if (indexPos >= column * row)
                indexPos -= column * row;

            switch (row)
            {
                case 1:
                    location.Y = 0;
                    break;
                case 2:
                    if (indexPos < column)
                    {
                        location.Y = 0;
                    }
                    else if (indexPos < column * 2)
                    {
                        int x = indexPos / column;
                        location.Y = getHeightScreen / 2;
                        indexPos -= column;
                    }
                    break;
                case 3:
                    if (indexPos < column)
                    {
                        location.Y = 0;
                    }
                    else if (indexPos < column * 2)
                    {
                        location.Y = getHeightScreen / 3 * 1;
                        indexPos -= column;
                    }
                    else if (indexPos < column * 3)
                    {
                        location.Y = getHeightScreen / 3 * 2;
                        indexPos -= column * 2;
                    }
                    break;
                case 4:
                    if (indexPos < column)
                    {
                        location.Y = 0;
                    }
                    else if (indexPos < column * 2)
                    {
                        location.Y = getHeightScreen / 4 * 1;
                        indexPos -= column;
                    }
                    else if (indexPos < column * 3)
                    {
                        location.Y = getHeightScreen / 4 * 2;
                        indexPos -= column * 2;
                    }
                    else if (indexPos < column * 4)
                    {
                        location.Y = getHeightScreen / 4 * 3;
                        indexPos -= column * 3;
                    }
                    break;
                case 5:
                    if (indexPos < column)
                    {
                        location.Y = 0;
                    }
                    else if (indexPos < column * 2)
                    {
                        location.Y = getHeightScreen / 5 * 1;
                        indexPos -= column;
                    }
                    else if (indexPos < column * 3)
                    {
                        location.Y = getHeightScreen / 5 * 2;
                        indexPos -= column * 2;
                    }
                    else if (indexPos < column * 4)
                    {
                        location.Y = getHeightScreen / 5 * 3;
                        indexPos -= column * 3;
                    }
                    else
                    {
                        location.Y = getHeightScreen / 5 * 4;
                        indexPos -= column * 4;
                    }
                    break;
                default:
                    break;
            }

            //location.Y = 0;
            //if (indexPos > 0)
            //{
            //    int x = column / indexPos;
            //    location.Y = getHeightScreen / x;
            //    indexPos -= column * (x-1);
            //}


            int widthWindowChrome = getWidthScreen / column;
            location.X = (indexPos) * (widthWindowChrome) - 10;
            return location;
        }
        public static int getWidthScreen = Screen.PrimaryScreen.Bounds.Width;
        public static int getHeightScreen = Screen.PrimaryScreen.Bounds.Height;
        private void Loginn(Chrome chrome, int row)
        {
            try
            {
                SetCellAccount(row, "cStatus", "Đang tương tác...");
                string uid = GetCellAccount(row, "cUid");
                string pass = GetCellAccount(row, "cPass");
                string fa2 = GetCellAccount(row, "c2Fa");
                bool isAccount = true;
                {
                    if (isAccount)
                    {
                        if (chrome.CheckExistElement("#m_login_email", 10) == 1)
                        {
                            chrome.SendKeys(1, "m_login_email", uid);
                            chrome.DelayTime(1);
                            chrome.SendKeys(4, "[type=\"password\"]", pass);
                            chrome.DelayTime(1);
                            chrome.Click(2, "login");
                            chrome.DelayTime(1);
                            if (chrome.CheckExistElement("#approvals_code", 5) == 1)
                            {
                                string otp = GetTotp(fa2);
                                chrome.SendKeys(1, "approvals_code", otp);
                                chrome.DelayTime(1);
                            }
                        }
                        chrome.Click(4, "[type=\"submit\"]", 0);
                        chrome.DelayTime(2);

                        chrome.Click(4, "[type=\"submit\"]", 0);
                        chrome.DelayTime(2);
                    }
                    else
                    {

                    }

                }
            }
            catch
            {

            }
        }

        // tương tác newfeed

        private void Newfeed(Chrome chrome, int Sobaituongtacnewfeed, string commentnewfeed)
         
        {


            int i = 0;
            for (i = 1; i <= Sobaituongtacnewfeed; i++)
            {


                chrome.Click(4, "[data-sigil=\"feed-ufi-focus feed-ufi-trigger ufiCommentLink mufi-composer-focus\"]", i);
                chrome.DelayTime(2);

                chrome.Click(4, "[data-sigil=\"touchable ufi-inline-like like-reaction-flyout\"]");
                chrome.DelayTime(2);

                chrome.SendKeys(1, "composerInput", commentnewfeed);
                chrome.DelayTime(2);

                chrome.Click(4, "[name=\"submit\"]");
                chrome.DelayTime(2);

                chrome.Click(4, "[data-sigil=\"MBackNavBarClick\"]");
                chrome.DelayTime(2);



            }
           
        }


        private void Banbe(Chrome chrome, int sobbtt, string dangbai, string cmt, int soluongcmt)
        {
            int j;
            for (j = 1; j <= sobbtt; j++)
            {
                chrome.GotoURL("https://m.facebook.com/friends/center/friends/?fb_ref=fbm");
                chrome.DelayTime(2);

                chrome.Click(4, "[class=\"darkTouch\"]", j);
                chrome.DelayTime(3);

                chrome.Click(4, "[role=\"button\"]", 11);
                chrome.DelayTime(3);


                chrome.SendKeys(4, "[data-sigil=\"composer-textarea m-textarea-input\"]", dangbai);
                chrome.DelayTime(4);

                chrome.Click(4, "[data-sigil=\"touchable submit_composer\"]", 1);
                chrome.DelayTime(2);

                int a;
                for (a = 0; a <= soluongcmt; a++)
                {
                    chrome.Click(4, "[data-sigil=\"feed-ufi-focus feed-ufi-trigger ufiCommentLink mufi-composer-focus\"]", a-1);
                    chrome.DelayTime(3);

                    chrome.Click(4, "[data-sigil=\"touchable ufi-inline-like like-reaction-flyout\"]");
                    chrome.DelayTime(2);

                    chrome.Click(1, "composerInput");
                    chrome.DelayTime(2);

                    
                    chrome.SendKeys(1, "composerInput", cmt);
                    chrome.DelayTime(3);

                    chrome.Click(4, "[type=\"submit\"]", 0);
                    chrome.DelayTime(2);

                    chrome.GotoBackPage();
                }
                Console.ReadKey();
            }
            Console.ReadKey();





        }
        private void Ketban(Chrome chrome, string tukhoa, int soluong)
        {
            //chrome.Click(4, "[href*=\"/friends/center/requests/?rfj&refid=7\"]");
            //chrome.DelayTime(2);

            //chrome.Click(4, "[href=\"/friends/center/suggestions/?mff_nav=1\"]");
            //chrome.DelayTime(2);

            //chrome.Click(4, "[data-sigil=\"touchable m-add-friend\"]>button", 1);
            //chrome.DelayTime(2);

            chrome.Click(2, "Tìm kiếm");
            chrome.DelayTime(2);

            chrome.SendKeys(4, "[data-sigil=\"search-small-box\"]", tukhoa);
            chrome.DelayTime(3);

            chrome.Click(4, "[class=\"touchable_io2_iop\"]");
            chrome.DelayTime(2);

            chrome.Click(4, "[href*=\"/search/people/?q=nam&source=filter&isTrending=0&tsid\"]");
            chrome.DelayTime(2);

            chrome.Click(4, "[aria-label=\"Xem tất cả\"]");
            chrome.DelayTime(2);




        }

        private void Status(Chrome chrome, string tukhoabaiviet, int Soluogbaiviet, string Coment)
        {

            chrome.Click(4, "[data-sigil=\"icon\"]", 1);
            chrome.DelayTime(2);

            chrome.SendKeys(4, "[data-sigil=\"search-small-box\"]", tukhoabaiviet);
            chrome.DelayTime(3);

            chrome.Click(4, "[class=\"touchable _io2 _iop\"]");
            chrome.DelayTime(2);

            chrome.Click(4, "[data-sigil=\"mlayer-hide-on-click search-tabbar-tab\"]", 1);
            chrome.DelayTime(2);

            string link = chrome.GetURL();

            bool isbaiviet = true;
            {
                if (isbaiviet)
                {
                    int i = 0;
                    while (i < Soluogbaiviet)
                    {
                        Console.WriteLine(" số i = isbaiviet  " + i);
                        i++;

                        chrome.ScrollSmooth(" document.queryselectorall('[aria-label=\"mở tin\"]')[" + i + "]");
                        chrome.DelayTime(1);

                        chrome.Click(4, "[aria-label=\"mở tin\"]", i);
                        chrome.DelayTime(3);

                        chrome.Click(4, "[data-sigil=\"touchable ufi-inline-like like-reaction-flyout\"]");
                        chrome.DelayTime(2);

                        chrome.Click(1, "composerinput");
                        chrome.DelayTime(2);

                        chrome.SendKeys(1, "composerinput", Coment);
                        chrome.DelayTime(2);

                        chrome.Click(4, "[type=\"submit\"]");
                        chrome.DelayTime(2);

                        chrome.GotoURL(link);
                    }
                }
                else
                {

                }
                chrome.GotoBackPage();

            }

        }

        private void checkid(Chrome chrome, string Idbaiviet, string Idnhom)
        {
            //try
            //{
            //    string IDBaiVet;
            //    List<string> lstIdBaiViet = Idbaiviet.Lines.ToList();

            //    for (int i = 0; i < lstIdBaiViet.Count; i++)
            //    {
            //        chrome.GotoURL("https://m.facebook.com/" + lstIdBaiViet[i]);
            //        chrome.DelayTime(1);

            //        if (chrome.CheckExistElement("[data-sigil=\"ufi-inline-actions\"] a") == 1)
            //        {
            //            chrome.DelayTime(1);






            //            string link = chrome.ExecuteScript("return document.querySelector('[name =\"apple-itunes-app\"]').getAttribute('content')").ToString();
            //            string id = Regex.Match(link, @"\?id=([0-9]{1,})").Groups[1].Value;


            //            File.AppendAllText(Text, lstIdBaiViet[i] + "|1|" + id + Environment.NewLine);
            //            Idnhom.Text += id + Environment.NewLine;
            //        }
            //        else
            //        {
            //            Idnhom.Text += (Text, lstIdBaiViet + "|0|" + Environment.NewLine);
            //            // textbox += hiển thị : nhập phần check vào form
            //        }

            //    }
            //    //string readText = File.ReadAllText("checkid.txt");
            //    //Console.WriteLine(readText);

            //}
            //catch
            //{

            //}

        }

        private void Thamgianhom(Chrome chrome, string tukhoanhom, int soluongnhom, string answer)
        {
            chrome.Click(2, "Tìm kiếm");
            chrome.DelayTime(2);

            chrome.SendKeys(4, "[data-sigil=\"search-small-box\"]", tukhoanhom);
            chrome.DelayTime(3);

            chrome.Click(4, "[class=\"touchable_io2_iop\"]");
            chrome.DelayTime(2);

            chrome.Click(4, "[href*=\"/search/groups\"]");
            chrome.DelayTime(2);









        }

        private string GetCellAccount(int row, string v)
        {
            return GetStatusDataGridView(dgvDatalst, row, v);
        }
        public static string GetStatusDataGridView(DataGridView dgv, int row, string colName)
        {
            string output = "";

            try
            {
                if (dgv.Rows[row].Cells[colName].Value != null)
                {
                    try
                    {
                        output = dgv.Rows[row].Cells[colName].Value.ToString();
                    }
                    catch
                    {
                        dgv.Invoke(new MethodInvoker(delegate ()
                        {
                            output = dgv.Rows[row].Cells[colName].Value.ToString();
                        }));
                    }
                }
            }
            catch
            {
            }

            return output;
        }
        private void uid_TextChanged(object sender, EventArgs e)
        {

        }

        private void ketban_Click(object sender, EventArgs e)
        {
            ShowForm(new Ketban());
        }

        private void Joingroup_Click(object sender, EventArgs e)
        {
            ShowForm(new Thamgianhom());
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ShowForm(new bàiviet());
        }
        public static void ShowForm(Form f)
        {
            f.ShowInTaskbar = false;
            f.ShowDialog();
        }

        private void ttbanbe_Click(object sender, EventArgs e)
        {
            ShowForm(new tuongtacbanbe());
        }

        private void ttnewfeed_Click(object sender, EventArgs e)
        {
            ShowForm(new mNewfeed());
        }

        private void thongbao_Click(object sender, EventArgs e)
        {
            ShowForm(new checkidbaiviet());
        }


        public static void SetStatusDataGridView(DataGridView dgv, int row, string colName, object status)
        {
            try
            {
                Application.DoEvents();
                dgv.Invoke(new MethodInvoker(delegate ()
                {
                    dgv.Rows[row].Cells[colName].Value = status;
                }));
            }
            catch { }
        }


        private void chọnTấtCảToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDatalst.Rows.Count; i++)
                {
                    SetStatusDataGridView(dgvDatalst, i, "cChose", true);
                }
            }
            catch
            { }
        }

        private void bỏChọnTấtẢToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                for (int i = 0; i < dgvDatalst.Rows.Count; i++)
                {
                    SetStatusDataGridView(dgvDatalst, i, "cChose", false);
                }
            }
            catch
            { }
        }

        private void pasteUidPass2FaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataObject o = (DataObject)Clipboard.GetDataObject();
                string[] pastedRows = Regex.Split(o.GetData(DataFormats.Text).ToString().TrimEnd("\r\n".ToCharArray()), "\r\n");
                foreach (string pastedRow in pastedRows)
                {
                    if (!pastedRow.Replace("\t", "").Equals(""))
                    {
                        string[] pastedRowCells = pastedRow.Split(new char[] { '|' });
                        int myRowIndex = dgvDatalst.Rows.Add();
                        SetStatusDataGridView(dgvDatalst, myRowIndex, "cChose", false);
                        //SetStatusDataGridView(dgvDatalst, myRowIndex, "cStt", myRowIndex + 1);
                        SetStatusDataGridView(dgvDatalst, myRowIndex, "cUid", pastedRowCells[0]);
                        SetStatusDataGridView(dgvDatalst, myRowIndex, "cPass", pastedRowCells[1]);
                        SetStatusDataGridView(dgvDatalst, myRowIndex, "c2fa", pastedRowCells[2]);
                        SetStatusDataGridView(dgvDatalst, myRowIndex, "cStatus", "");
                    }
                }
                //UpdateSTTOnDtgvAcc();
            }
            catch { }
        }

        private void copyUidPass2FaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                string textCopy = "";
                for (int i = 0; i < dgvDatalst.RowCount; i++)
                {
                    if (Convert.ToBoolean(dgvDatalst.Rows[i].Cells["cChose"].Value))
                    {
                        try
                        {
                            textCopy += (dgvDatalst.Rows[i].Cells["cUid"].Value == null ? "" : dgvDatalst.Rows[i].Cells["cUid"].Value.ToString()) + "|" + (dgvDatalst.Rows[i].Cells["cPass"].Value == null ? "" : dgvDatalst.Rows[i].Cells["cPass"].Value.ToString()) + "|" + (dgvDatalst.Rows[i].Cells["c2fa"].Value == null ? "" : dgvDatalst.Rows[i].Cells["c2fa"].Value.ToString()) + "\r\n";
                        }
                        catch { }
                    }
                }
                Clipboard.SetText(textCopy);
            }
            catch { }
        }

        private void ctms_Opening(object sender, CancelEventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            ShowForm(new DangbaiNF());
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {

        }
    }
}

