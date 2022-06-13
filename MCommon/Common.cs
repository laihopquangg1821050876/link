using AE.Net.Mail;
using IWshRuntimeLibrary;
using maxcare;
using Newtonsoft.Json.Linq;
using OtpNet;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using File = System.IO.File;

namespace MCommon
{
    public class Common
    {
        public static void UpdateCountLine(RichTextBox txt, Label lbl)
        {
            try
            {
                string content = lbl.Text;
                List<string> lstAcc = txt.Lines.ToList();
                lstAcc = RemoveEmptyItems(lstAcc);
                lbl.Text = content.Replace(Regex.Match(content, "\\((.*?)\\)").Value, "(" + lstAcc.Count + ")");
            }
            catch { }
        }
        public static bool CheckFormIsOpenning(string nameForm)
        {
            try
            {
                FormCollection fc = Application.OpenForms;

                foreach (Form frm in fc)
                {
                    if (frm.Name == nameForm)
                        return true;
                }
            }
            catch
            {
            }
            
            return false;
        }
        public static bool CloseForm(string nameForm)
        {
            try
            {
                FormCollection fc = Application.OpenForms;
                foreach (Form frm in fc)
                {
                    if (frm.Name == nameForm)
                    {
                        frm.Invoke((MethodInvoker)delegate ()
                        {
                            frm.Close();
                        });

                        break;
                    }
                }
            }
            catch
            {
            }
            
            return false;
        }
        public static string ConvertListParamsToString(object lstParams)
        {
            string param = "";
            try
            {
                foreach (PropertyInfo pi in lstParams.GetType().GetProperties())
                    param += pi.GetValue(lstParams) + ",";
                param = param.TrimEnd(',');
            }
            catch
            {
            }

            return param;
        }

        static Random rd = new Random();
        public static string GetFbDtag(string cookie, string useragent, string proxy, int typeProxy)
        {
            try
            {
                string rq = new RequestXNet(cookie, useragent, proxy, typeProxy).RequestGet("https://m.facebook.com/help/");
                return Regex.Match(rq, MCommon.Common.Base64Decode("ImR0c2dfYWciOnsidG9rZW4iOiIoLio/KSI=")).Groups[1].Value;
            }
            catch
            {
                return "";
            }
        }
        /// <summary>
        /// Lấy phần tử giống nhau
        /// </summary>
        /// <param name="lstRoot"></param>
        /// <param name="lstCompare"></param>
        /// <returns></returns>
        public static List<string> GetIntersectItemBetweenTwoList(List<string> lstRoot, List<string> lstCompare)
        {
            List<string> lst = new List<string>();
            try
            {
                lst = lstRoot.Intersect(lstCompare).ToList();
            }
            catch
            {
            }
            return lst;
        }
        /// <summary>
        /// Lấy phần tử khác nhau
        /// </summary>
        /// <param name="lstRoot"></param>
        /// <param name="lstCompare"></param>
        /// <returns></returns>
        public static List<string> GetExceptItemBetweenTwoList(List<string> lstRoot, List<string> lstCompare)
        {
            List<string> lst = new List<string>();
            try
            {
                lst = lstRoot.Except(lstCompare).ToList();
            }
            catch
            {
            }
            return lst;
        }
        static void Enable(string interfaceName)
        {
            System.Diagnostics.ProcessStartInfo psi =
                   new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" enable");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = psi;
            p.Start();
        }

        static void Disable(string interfaceName)
        {
            System.Diagnostics.ProcessStartInfo psi =
                new System.Diagnostics.ProcessStartInfo("netsh", "interface set interface \"" + interfaceName + "\" disable");
            System.Diagnostics.Process p = new System.Diagnostics.Process();
            p.StartInfo = psi;
            p.Start();
        }
        public static string GetDateCreatFolder(string pathFolder)
        {
            try
            {
                return Directory.GetCreationTime(pathFolder).ToString("yyyy/MM/dd HH:mm:ss");
            }
            catch (Exception)
            {
            }
            return "";
        }
        public static string GetDateCreatFile(string pathFile)
        {
            try
            {
                return File.GetCreationTime(pathFile).ToString("yyyy/MM/dd HH:mm:ss");
            }
            catch (Exception)
            {
            }
            return "";
        }
        public static string GetRandomItemFromList(ref List<string> lst, Random rd)
        {
            string item = "";
            try
            {
                item = lst[rd.Next(0, lst.Count)];
                lst.Remove(item);
            }
            catch (Exception)
            {

                throw;
            }
            return item;
        }
        public static string CheckAccountHotmail(string username, string password)
        {
            int dem = 0;
        start:
            try
            {
                string imap = "";
                if (username.EndsWith("@hotmail.com") || username.EndsWith("@outlook.com"))
                    imap = "outlook.office365.com";
                else if (username.EndsWith("@yandex.com"))
                    imap = "imap.yandex.com";

                if (imap == "")
                    return "0";
                AE.Net.Mail.ImapClient client = new AE.Net.Mail.ImapClient(imap, username, password, AuthMethods.Login, 993, true);
                client.Dispose();
                return "1";
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("The remote certificate is invalid according to the validation procedure"))
                {
                    dem++;
                    if (dem < 10)
                        goto start;
                }
                return "0";
            }
        }
        public static string CheckAccountEmail(string username, string password, string imap)
        {
            int dem = 0;
        start:
            try
            {

                AE.Net.Mail.ImapClient client = new AE.Net.Mail.ImapClient(imap, username, password, AuthMethods.Login, 993, true);
                client.Dispose();
                return "1";
            }
            catch (Exception ex)
            {
                if (ex.ToString().Contains("The remote certificate is invalid according to the validation procedure"))
                {
                    dem++;
                    if (dem < 10)
                        goto start;
                }
                return "0";
            }
        }

        public static string ConvertSecondsToTime(int seconds)
        {
            try
            {
                TimeSpan time = TimeSpan.FromSeconds(seconds);
                if (seconds < 60)
                    return TimeSpan.FromSeconds(seconds).ToString(@"ss");
                else if (seconds < 3600)
                    return TimeSpan.FromSeconds(seconds).ToString(@"mm\:ss");
                else
                    return TimeSpan.FromSeconds(seconds).ToString(@"hh\:mm\:ss");
            }
            catch
            {
                return "";
            }
        }
        public static bool CreateShortcutChrome(string shortcutName, string shortcutPath, string profilePath, string icon = @"C:\Users\Xuan Tung\Desktop\MaxUid\images\icon_64.ico", string targetFileLocation = "\"C:\\Program Files (x86)\\Google\\Chrome\\Application\\chrome.exe\"")
        {
            bool isSuccess = false;
            try
            {
                MCommon.Common.CreateShortcut(shortcutName, shortcutPath, targetFileLocation, "--user-data-dir=\"" + profilePath + "\"", targetFileLocation.Substring(0, targetFileLocation.LastIndexOf("\\")), icon);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"chrome.CreateShortcut({shortcutName},{shortcutPath},{targetFileLocation})select");
            }
            return isSuccess;
        }
        public static void CreateShortcut(string shortcutName, string shortcutPath, string targetFileLocation, string arg, string WorkingDirectory = "C:\\Program Files (x86)\\Google\\Chrome\\Application", string icon = @"C:\Users\Xuan Tung\Desktop\MaxUid\images\icon_64.ico")
        {
            string shortcutLocation = System.IO.Path.Combine(shortcutPath, shortcutName + ".lnk");
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(shortcutLocation);

            shortcut.Description = "Shortcut Chrome MIN Software";
            shortcut.WorkingDirectory = WorkingDirectory;
            shortcut.IconLocation = icon;
            shortcut.TargetPath = targetFileLocation;
            shortcut.Arguments = arg;
            shortcut.Save();
        }



        #region Compare Image
        public static int CompareImage(string pathFile1, string pathFile2)
        {
            int result = 0;
            try
            {
                List<bool> iHash1 = GetHash(new Bitmap(pathFile1));
                List<bool> iHash2 = GetHash(new Bitmap(pathFile1));
                result = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq);

            }
            catch
            {
            }
            return result;
        }

        public static bool SetTextToClipboard(string content)
        {
            bool isSuccess = false;
            try
            {
                Thread t = new Thread((ThreadStart)(() =>
                {
                    try
                    {
                        Clipboard.SetText(content);
                        isSuccess = true;
                    }
                    catch
                    {
                    }
                }));

                // Run your code from a thread that joins the STA Thread
                t.SetApartmentState(ApartmentState.STA);
                t.Start();
                t.Join();
            }
            catch
            {
            }
            return isSuccess;
        }
        public static List<bool> GetHash(Bitmap bmpSource)
        {
            List<bool> lResult = new List<bool>();
            //create new image with 16x16 pixel
            Bitmap bmpMin = new Bitmap(bmpSource, new Size(16, 16));
            for (int j = 0; j < bmpMin.Height; j++)
            {
                for (int i = 0; i < bmpMin.Width; i++)
                {
                    //reduce colors to true / false                
                    lResult.Add(bmpMin.GetPixel(i, j).GetBrightness() < 0.5f);
                }
            }
            return lResult;
        }
        #endregion

        public static List<string> CloneList(List<string> lstFrom)
        {
            List<string> lstOutput = new List<string>();
            try
            {
                for (int i = 0; i < lstFrom.Count; i++)
                {
                    lstOutput.Add(lstFrom[i]);
                }
            }
            catch
            {

            }
            return lstOutput;
        }

        public static string SpinText(string text, Random rand)
        {
            int i, j, e = -1;
            char[] curls = new char[] { '{', '}' };
            text += '~';
            string[] parts;
            do
            {
                i = e;
                e = -1;
                while ((i = text.IndexOf('{', i + 1)) != -1)
                {
                    j = i;
                    while ((j = text.IndexOfAny(curls, j + 1)) != -1 && text[j] != '}')
                    {
                        if (e == -1) e = i;
                        i = j;
                    }
                    if (j != -1)
                    {
                        parts = text.Substring(i + 1, (j - 1) - (i + 1 - 1)).Split('|');
                        text = text.Remove(i, j - (i - 1)).Insert(i, parts[rand.Next(parts.Length)]);
                    }
                }
            }
            while (e-- != -1);

            return text.Remove(text.Length - 1);
        }
        public static void OpenFileAndPressData(string linkPathFile, string title = "Nhập danh sách Uid cần clone", string status = "Danh sách Uid", string footer = "(Mỗi nội dung 1 dòng, spin nội dung {a|b|c})")
        {
            try
            {
                if (!System.IO.File.Exists(linkPathFile))
                    MCommon.Common.CreateFile(linkPathFile);
                MCommon.Common.ShowForm(new fNhapDuLieu1(linkPathFile, title, status, footer));
            }
            catch
            {
            }
        }
        public static DateTime ConvertTimeStampToDateTime(double timestamp)
        {
            // Unix timestamp is seconds past epoch
            System.DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, System.DateTimeKind.Utc);
            dtDateTime = dtDateTime.AddSeconds(timestamp).ToLocalTime();
            return dtDateTime;
        }

        public static Form GetFormByName(string name, string para)
        {
            System.Reflection.Assembly myAssembly = System.Reflection.Assembly.GetExecutingAssembly();
            foreach (Type type in myAssembly.GetTypes())
            {
                if (type.BaseType != null && type.BaseType.FullName == "System.Windows.Forms.Form")
                {
                    if (type.FullName == name)
                    {
                        var form = Activator.CreateInstance(Type.GetType(name), new object[] { "", 1, para }) as Form;
                        return form;
                    }
                }
            }
            return null;
        }
        public static void CreateFile(string pathFile)
        {
            try
            {
                if (!System.IO.File.Exists(pathFile))
                    System.IO.File.AppendAllText(pathFile, "");
            }
            catch
            {
            }
        }

        public static void ClearSelectedOnDatagridview(DataGridView dtgv)
        {
            for (int i = 0; i < dtgv.RowCount; i++)
            {
                dtgv.Rows[i].Selected = false;
            }
        }
        public static void CreateFolder(string pathFolder)
        {
            try
            {
                if (!Directory.Exists(pathFolder))
                    Directory.CreateDirectory(pathFolder);
            }
            catch
            {
            }
        }

        public static void ShowForm(Form f)
        {
            try
            {
                f.ShowInTaskbar = false;
                f.ShowDialog();
            }
            catch
            {
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="type">1-success, 2-error, 3-warning</param>
        public static void ShowMessageBox(object s, int type)
        {
            switch (type)
            {
                case 1:
                    MessageBox.Show(s.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                    break;
                case 2:
                    MessageBox.Show(s.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Stop);
                    break;
                case 3:
                    MessageBox.Show(s.ToString(), "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                    break;
                default:
                    break;
            }
        }

        public static List<int> ShuffleList(List<int> lst)
        {
            int temp = 0;
            int currentIndex = lst.Count;
            int randomIndex = 0;
            while (currentIndex != 0)
            {
                randomIndex = Base.rd.Next(0, lst.Count);
                currentIndex -= 1;
                temp = lst[currentIndex];
                lst[currentIndex] = lst[randomIndex];
                lst[randomIndex] = temp;
            }
            return lst;
        }
        public static List<string> ShuffleList(List<string> lst)
        {
            string temp = "";
            int currentIndex = lst.Count;
            int randomIndex = 0;
            while (currentIndex != 0)
            {
                randomIndex = Base.rd.Next(0, lst.Count);
                currentIndex -= 1;
                temp = lst[currentIndex];
                lst[currentIndex] = lst[randomIndex];
                lst[randomIndex] = temp;
            }
            return lst;
        }

        /// <summary>
        /// Remove empty item of List
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static List<string> RemoveEmptyItems(List<string> lst)
        {
            List<string> lstOutput = new List<string>();
            string item = "";
            for (int i = 0; i < lst.Count; i++)
            {
                item = lst[i].Trim();
                if (item != "")
                    lstOutput.Add(item);
            }
            return lstOutput;
        }

        public static string RunCMD(string fileName, string cmd, int timeout = 10)
        {
            Process process = new Process();
            process.StartInfo.FileName = fileName;
            process.StartInfo.Arguments = cmd;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.StartInfo.StandardOutputEncoding = Encoding.UTF8;
            process.StartInfo.StandardErrorEncoding = Encoding.UTF8;
            StringBuilder output = new StringBuilder();
            process.OutputDataReceived += delegate (object sender, DataReceivedEventArgs e)
            {
                if (!string.IsNullOrEmpty(e.Data))
                    output.Append(e.Data + "\n");
            };
            process.Start();
            process.BeginOutputReadLine();
            if (timeout < 0)
                process.WaitForExit();
            else
                process.WaitForExit(timeout * 1000);
            process.Close();
            return output.ToString();
        }


        /// <summary>
        /// Auto change ip Dcom
        /// </summary>
        /// <param name="profileDcom"></param>
        public static bool ResetDcom(string profileDcom)
        {
            bool isSuccess = false;

            string rq = RunCMD("rasdial.exe", "\"" + profileDcom + "\"", 3);
            if (rq.Contains("Successfully connected to "))//vừa connect thành công
            {
                isSuccess = true;
            }
            else if (rq.Contains("You are already connected to "))//Đã connect trước đó
            {
                //disconnect
                for (int i = 0; i < 3; i++)
                {
                    rq = RunCMD("rasdial.exe", "\"" + profileDcom + "\" /disconnect", 3);
                    if (rq.Trim() == "Command completed successfully.")
                    {
                        isSuccess = true;
                        break;
                    }
                    DelayTime(1);
                }
                DelayTime(1);

                //connect
                if (isSuccess)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        rq = RunCMD("rasdial.exe", "\"" + profileDcom + "\"", 3);
                        if (rq.Contains("Successfully connected to "))
                        {
                            isSuccess = true;
                            break;
                        }
                        DelayTime(1);
                    }
                }
                DelayTime(1);
            }
            else
            {
                isSuccess = false;
            }

            return isSuccess;
        }

        /// <summary>
        /// Auto change ip Dcom
        /// </summary>
        /// <param name="profileDcom"></param>
        //public static void ResetDcom(string profileDcom)
        //{
        //    Process process = new Process();
        //    process.StartInfo.FileName = "rasdial.exe";
        //    process.StartInfo.Arguments = "\"" + profileDcom + "\" /disconnect";
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.CreateNoWindow = true;
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.StartInfo.RedirectStandardError = true;
        //    process.Start();
        //    process.WaitForExit();

        //    Thread.Sleep(3000);
        //    process = new Process();
        //    process.StartInfo.FileName = "rasdial.exe";
        //    process.StartInfo.Arguments = "\"" + profileDcom + "\"";
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.CreateNoWindow = true;
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.StartInfo.RedirectStandardError = true;
        //    process.Start();
        //    process.WaitForExit();
        //    Thread.Sleep(1500);
        //}
        ///// <summary>
        ///// Auto change ip Dcom
        ///// </summary>
        ///// <param name="profileDcom"></param>
        //public static void ResetDcom(string profileDcom)
        //{
        //    Process process = new Process();
        //    process.StartInfo.FileName = "rasdial.exe";
        //    process.StartInfo.Arguments = "\"" + profileDcom + "\" /disconnect";
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.StartInfo.RedirectStandardError = true;
        //    process.Start();
        //    process.WaitForExit();

        //    Thread.Sleep(3000);
        //    process = new Process();
        //    process.StartInfo.FileName = "rasdial.exe";
        //    process.StartInfo.Arguments = "\"" + profileDcom + "\"";
        //    process.StartInfo.UseShellExecute = false;
        //    process.StartInfo.RedirectStandardOutput = true;
        //    process.StartInfo.RedirectStandardError = true;
        //    process.Start();
        //    process.WaitForExit();
        //    Thread.Sleep(1500);
        //}
        public static string TrimEnd(string text, string value)
        {
            if (!text.EndsWith(value))
                return text;

            return text.Remove(text.LastIndexOf(value));
        }
        public static void SaveDatagridview(DataGridView dgv, string FilePath, char splitChar = '|')
        {
            List<string> list = new List<string>();
            string row = "";
            object r = null;
            for (int j = 0; j < dgv.RowCount; j++)
            {
                row = "";
                for (int i = 0; i < dgv.ColumnCount; i++)
                {
                    r = dgv.Rows[j].Cells[i].Value;
                    row += r == null ? splitChar.ToString() : (r + splitChar.ToString());
                }
                row = row.TrimEnd(splitChar);
                list.Add(row);
            }
            File.WriteAllLines(FilePath, list);
        }

        public static void LoadDatagridview(DataGridView dgv, string namePath, char splitChar = '|')
        {
            if (!File.Exists(namePath))
                MCommon.Common.CreateFile(namePath);
            List<string> list = File.ReadAllLines(namePath).ToList();
            string row = "";
            if (list.Count > 0)
            {
                for (int i = 0; i < list.Count; i++)
                {
                    row = list[i];
                    dgv.Rows.Add(row.Split(splitChar));
                }
            }
        }
        public static string SelectFolder()
        {
            string path = "";
            try
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    DialogResult result = fbd.ShowDialog();

                    if (result == DialogResult.OK && !string.IsNullOrWhiteSpace(fbd.SelectedPath))
                        path = fbd.SelectedPath;
                }
            }
            catch { }
            return path;
        }
        public static string SelectFile(string title= "Chọn File txt",string typeFile= "txt Files (*.txt)|*.txt|")
        {
            string path = "";
            try
            {
                using (var dialog = new OpenFileDialog())
                {
                    dialog.Filter = typeFile+"All files (*.*)|*.*";
                    dialog.InitialDirectory = "C:\\";
                    dialog.Title = title;
                    if (dialog.ShowDialog() == DialogResult.OK)
                        path = dialog.FileName;
                }
            }
            catch { }
            return path;
        }

        public static void KillProcess(string nameProcess)
        {
            try
            {
                foreach (Process proc in Process.GetProcessesByName(nameProcess))
                {
                    proc.Kill();
                }
            }
            catch
            {
            }
        }

        /// <summary>
        /// Check string is contain latinh char?
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static bool CheckBasicString(string text)
        {
            bool result = true;
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if (!((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z') || c == '.'))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        /// <summary>
        /// Remove char is not latin in string
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string RemoveCharNotLatin(string text)
        {
            string result = "";
            for (int i = 0; i < text.Length; i++)
            {
                char c = text[i];
                if ((c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z'))
                {
                    result += c;
                }
            }
            return result;
        }

        /// <summary>
        /// Convert Text to UTF-8
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string ConvertToUTF8(string text)
        {
            byte[] bytes = Encoding.Default.GetBytes(text);
            text = Encoding.UTF8.GetString(bytes);
            return text;
        }
        public static bool IsNumber(string pValue)
        {
            if (pValue == "")
                return false;
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public static bool IsContainNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (Char.IsDigit(c))
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Open browser with html text
        /// </summary>
        /// <param name="text"></param>
        public static void ReadHtmlText(string html)
        {
            string path = "zzz999.html";
            File.WriteAllText(path, html);
            Process.Start(path);
        }
        /// <summary>
        /// Download string from Url
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        public static string ReadHTMLCode(string Url)
        {
            try
            {
                return new RequestXNet("", "", "", 0).RequestGet(Url);
                WebClient webClient = new WebClient();
                byte[] reqHTML = webClient.DownloadData(Url);
                UTF8Encoding objUTF8 = new UTF8Encoding();
                return objUTF8.GetString(reqHTML);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// Check is a address mail?
        /// </summary>
        /// <param name="emailaddress"></param>
        /// <returns></returns>
        public static bool IsValidMail(string emailaddress)
        {
            try
            {
                System.Net.Mail.MailAddress m = new System.Net.Mail.MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="type">X2-ToUpper</param>
        /// <returns></returns>
        public static string Md5Encode(string text, string type = "X2")
        {
            MD5 obj = MD5.Create();
            byte[] data = obj.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
                s.Append(data[i].ToString(type));
            return s.ToString();
        }
        public static string Base64Encode(string base64Decoded)
        {
            var plainTextBytes = System.Text.Encoding.UTF8.GetBytes(base64Decoded);
            return System.Convert.ToBase64String(plainTextBytes);
        }
        public static string Base64Decode(string base64Encoded)
        {
            var base64EncodedBytes = System.Convert.FromBase64String(base64Encoded);
            return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
        }
        public static string CreateRandomStringNumber(int lengText, Random rd = null)
        {
            string outPut = "";
            if (rd == null)
                rd = new Random();
            string validChars = "abcdefghijklmnopqrstuvwxyz0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            for (int i = 0; i < lengText; i++)
                outPut += validChars[rd.Next(0, validChars.Length)];
            return outPut;
        }
        public static string CreateRandomString(int lengText, Random rd = null)
        {
            string outPut = "";
            if (rd == null)
                rd = new Random();
            string validChars = "abcdefghijklmnopqrstuvwxyz";
            for (int i = 0; i < lengText; i++)
            {
                outPut += validChars[rd.Next(0, validChars.Length)];
            }
            return outPut;
        }
        public static string CreateRandomNumber(int leng, Random rd = null)
        {
            string outPut = "";
            if (rd == null)
                rd = new Random();
            string validChars = "0123456789";
            for (int i = 0; i < leng; i++)
            {
                outPut += validChars[rd.Next(0, validChars.Length)];
            }
            return outPut;
        }

        /// <summary>
        /// Convert to Unsigned String
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ConvertToUnSign(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string RunCMD(string cmd)
        {
            Process cmdProcess;
            cmdProcess = new Process();
            cmdProcess.StartInfo.FileName = "cmd.exe";
            cmdProcess.StartInfo.Arguments = "/c " + cmd;
            cmdProcess.StartInfo.RedirectStandardOutput = true;
            cmdProcess.StartInfo.UseShellExecute = false;
            cmdProcess.StartInfo.CreateNoWindow = true;
            cmdProcess.Start();
            string output = cmdProcess.StandardOutput.ReadToEnd();
            cmdProcess.WaitForExit();
            if (String.IsNullOrEmpty(output))
                return "";
            return output;
        }

        public static void DelayTime(double second)
        {
            Application.DoEvents();
            Thread.Sleep(Convert.ToInt32(second * 1000));
        }

        public static string HtmlDecode(string text)
        {
            return WebUtility.HtmlDecode(text);
        }
        public static string HtmlEncode(string text)
        {
            return WebUtility.HtmlEncode(text);
        }
        public static string UrlDecode(string text)
        {
            return WebUtility.UrlDecode(text);
        }
        public static string UrlEncode(string text)
        {
            return WebUtility.UrlEncode(text);
        }

        #region Chia Màn hình 3x2
        private static int getWidthScreen = Screen.PrimaryScreen.WorkingArea.Width;
        private static int getHeightScreen = Screen.PrimaryScreen.WorkingArea.Height;
        //public static int getWidthScreen = Screen.PrimaryScreen.Bounds.Width;
        //public static int getHeightScreen = Screen.PrimaryScreen.Bounds.Height;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">3x2; 4x2; 5x2; 6x2; 4x3</param>
        /// <returns></returns>
        public static Point GetSizeChrome(int column, int row)
        {
            JSON_Settings settings = new JSON_Settings("configChrome");
            if (settings.GetValueInt("width") == 0)
            {
                settings.Update("width", getWidthScreen);
                settings.Update("heigh", getHeightScreen);
                settings.Save();
            }
            getWidthScreen = settings.GetValueInt("width");
            getHeightScreen = settings.GetValueInt("heigh");


            int getWidthChrome = getWidthScreen / column + 15;
            int getHeightChrome = getHeightScreen / row + 10;
            return new Point(getWidthChrome, getHeightChrome);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">1-3x2; 2-4x2; 3-5x2; 4-6x2; 5-4x3</param>
        /// <returns></returns>
        public static Point GetPointFromIndexPosition(int indexPos, int column, int row)
        {
            JSON_Settings settings = new JSON_Settings("configChrome");
            if (settings.GetValueInt("width") == 0)
            {
                settings.Update("width", getWidthScreen);
                settings.Update("heigh", getHeightScreen);
                settings.Save();
            }
            getWidthScreen = settings.GetValueInt("width");
            getHeightScreen = settings.GetValueInt("heigh");


            Point location = new Point();
            while (indexPos >= column * row)
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
        public static void FillIndexPossition(ref List<int> lstPossition, int indexPos)
        {
            lock (lstPossition)
            {
                lstPossition[indexPos] = 0;
            }
        }
        #endregion
        public static double ConvertDatetimeToTimestamp(DateTime value)
        {
            TimeSpan span = value - new DateTime(1970, 1, 1, 0, 0, 0, 0).ToLocalTime();
            return (double)span.TotalSeconds;
        }
        static object k = new object();
        public static string CheckProxy(string proxy, int typeProxy)
        {
            string ip = "";

            try
            {
                RequestXNet request = new RequestXNet("", SetupFolder.GetUseragentIPhone(rd), proxy, typeProxy);
                //ip = request.RequestGet("https://whatismyv6.com/api/");
                //ip = ip.Split(',')[1];
                //ip = request.RequestGet("http://v4v6.ipv6-test.com/api/myip.php");
                //ip = request.RequestGet("https://api64.ipify.org/");
                //ip = request.RequestGet("http://app.minsoftware.xyz/api/ip").Trim();
                ip = Regex.Match(request.RequestGet("https://api.myip.com/"), @"ip"":""(.*?)""").Groups[1].Value;
            }
            catch (Exception ex)
            {
                ip = CheckProxy2(proxy, typeProxy);
            }

            return ip;
        }
        public static string CheckProxy2(string proxy, int typeProxy)
        {
            string ip = "";

            try
            {
                RequestXNet request = new RequestXNet("", SetupFolder.GetUseragentIPhone(rd), proxy, typeProxy);
                string rq = request.RequestGet("https://showip.net/");
                ip = Regex.Match(rq, "value=\"(.*?)\"").Groups[1].Value;            }
            catch (Exception ex)
            {
                ExportError(null, ex, "Check Proxy2");
            }

            return ip;
        }
        public static string CheckProxyOnlyV4(string proxy, int typeProxy)
        {
            string ip = "";
            try
            {
                RequestXNet request = new RequestXNet("", "", proxy, typeProxy);
                string rq = request.RequestGet("http://lumtest.com/myip.json");
                ip = JObject.Parse(rq)["ip"].ToString();
                //ip = request.RequestGet("http://app.minsoftware.xyz/api/ip").Trim();
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "Check Proxy");
            }
            return ip;
        }
        public static string CheckIP()
        {
            string ip = "";

            try
            {
                RequestXNet request = new RequestXNet("", "", "", 0);
                string rq = "";
                rq = request.RequestGet("http://lumtest.com/myip.json");
                ip = JObject.Parse(rq)["ip"].ToString();
                //ip = request.RequestGet("http://icanhazip.com/").Trim();
            }
            catch
            {
            }
            return ip;
        }

        /// <summary>
        /// Create 1 folder name "log" and create 2 folder name "images" and "html" into "log"
        /// </summary>
        public static void ExportError(Chrome chrome, Exception ex, string error = "")
        {
            try
            {
                if (!Directory.Exists("log"))
                    Directory.CreateDirectory("log");
                if (!Directory.Exists("log\\html"))
                    Directory.CreateDirectory("log\\html");
                if (!Directory.Exists("log\\images"))
                    Directory.CreateDirectory("log\\images");

                Random rrrd = new Random();
                string fileName = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + rrrd.Next(1000, 9999);

                if (chrome != null)
                {
                    string html = chrome.ExecuteScript("var markup = document.documentElement.innerHTML;return markup;").ToString();
                    chrome.ScreenCapture(@"log\images\", fileName);
                    File.WriteAllText(@"log\html\" + fileName + ".html", html);
                }

                using (StreamWriter writer = new StreamWriter(@"log\log.txt", true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    writer.WriteLine("File: " + fileName);
                    if (error != "")
                        writer.WriteLine("Error: " + error);
                    writer.WriteLine();

                    if (ex != null)
                    {
                        writer.WriteLine("Type: " + ex.GetType().FullName);
                        writer.WriteLine("Message: " + ex.Message);
                        writer.WriteLine("StackTrace: " + ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
            }
            catch { }
        }

        public static void ExportError(Exception ex, string error = "")
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(@"log\log.txt", true))
                {
                    writer.WriteLine("-----------------------------------------------------------------------------");
                    writer.WriteLine("Date: " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    if (error != "")
                        writer.WriteLine("Error: " + error);
                    writer.WriteLine();

                    if (ex != null)
                    {
                        writer.WriteLine("Type: " + ex.GetType().FullName);
                        writer.WriteLine("Message: " + ex.Message);
                        writer.WriteLine("StackTrace: " + ex.StackTrace);
                        ex = ex.InnerException;
                    }
                }
            }
            catch { }
        }
        public static int ResetHilink(string urlHilink)
        {
            //-1 error, 0 off, 1 on
            int resetCheck = -1;
            try
            {
                //string link = urlHilink.Replace("/html/home.html", "");
                string link = "http" + Regex.Match(urlHilink, "http(.*?)/html").Groups[1].Value;

                RequestHttp request = new RequestHttp("", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36", "");
                string html = request.RequestGet(urlHilink);
                string csrf_token = "";

                try
                {
                    csrf_token = Regex.Matches(html, "csrf_token\" content=\"(.*?)\"")[1].Groups[1].Value;
                }
                catch
                {
                    csrf_token = Regex.Match(request.RequestGet(link + "/api/webserver/token"), "<token>(.*?)</token>").Groups[1].Value;
                }

                html = request.RequestGet(link + "/api/dialup/mobile-dataswitch");
                request.request.SetDefaultHeaders(
                new string[]
                    {
                        "__RequestVerificationToken: "+ csrf_token,
                        "Accept: */*",
                        "Accept-Encoding: gzip, deflate",
                        "Accept-Language: vi-VN,vi;q=0.9,fr-FR;q=0.8,fr;q=0.7,en-US;q=0.6,en;q=0.5",
                        "Content-Type: application/x-www-form-urlencoded; charset=UTF-8",
                        "X-Requested-With: XMLHttpRequest",
                        "content-type: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                        "user-agent: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0.4389.90 Safari/537.36"
                    }
                 );
                string dataPost = "";
                if (html.Contains("dataswitch>1"))
                    dataPost = html.Replace("response", "request").Replace("dataswitch>1", "dataswitch>0");
                else if (html.Contains("dataswitch>0"))
                    dataPost = html.Replace("response", "request").Replace("dataswitch>0", "dataswitch>1");
                else
                    return -1;

                string trave = request.RequestPost(link + "/api/dialup/mobile-dataswitch", dataPost);
                if (trave.Contains("OK"))
                {
                    //Thread.Sleep(2000);
                    html = request.RequestGet(link + "/api/dialup/mobile-dataswitch");
                    if (html.Contains("dataswitch>1<"))
                    {
                        for (int i = 0; i < 10; i++)
                        {
                            string html1 = request.RequestGet(link + "/api/monitoring/traffic-statistics");
                            if (!html1.Contains("<CurrentUpload>0"))
                                break;
                            else
                                Thread.Sleep(1000);
                        }
                    }
                    return Convert.ToInt32(Regex.Match(html, "dataswitch>(.*?)<").Groups[1].Value);
                }
                else
                {
                    resetCheck = -1;
                }
            }
            catch
            {
            }

            return resetCheck;
        }
        public static bool ChangeIP(int typeChangeIP, int typeDcom, string profileDcom, string urlHilink, int iTypeHotspot, string sLinkNord)
        {
            bool isSuccess = false;
            string ip_new = "";

            try
            {
                if (typeChangeIP == 0)
                {
                    return true;
                }
                else if (typeChangeIP == 1)
                {
                    string ip_old = CheckIP();

                    IntPtr app = AutoControl.FindWindowHandle(null, "HMA VPN");
                    AutoControl.BringToFront(app);
                    AutoControl.SendClickOnPosition(AutoControl.FindHandle(app, "Chrome_RenderWidgetHostHWND", "Chrome Legacy Window"), 356, 286);
                    Thread.Sleep(5000);
                    string ipLan = CheckIP();
                    AutoControl.SendClickOnPosition(AutoControl.FindHandle(app, "Chrome_RenderWidgetHostHWND", "Chrome Legacy Window"), 356, 286);
                    //Thread.Sleep(15000);
                    int timeStart = Environment.TickCount;
                    do
                    {
                        ip_new = CheckIP();
                        if (Environment.TickCount - timeStart > 20000)
                            break;
                    } while (ip_new == ip_old || ip_new == ipLan);

                    if (ip_new != ip_old)
                        isSuccess = true;
                }
                else if (typeChangeIP == 2)
                {
                    if (typeDcom == 0)
                    {
                        isSuccess = ResetDcom(profileDcom);
                    }
                    else
                    {
                        int check = ResetHilink(urlHilink);
                        if (check == 0)
                        {
                            Thread.Sleep(2000);
                            check = ResetHilink(urlHilink);
                        }

                        isSuccess = check == 1;
                    }
                }
                else if (typeChangeIP == 4)
                {
                    //if (Auto.ClickControlImageFind("ExpressVPN", "mainScreen-vpn.PNG", "images//disconnect-express.PNG"))
                    //    Thread.Sleep(5000);
                    //if (Auto.ClickControlImageFind("ExpressVPN", "mainScreen-vpn.PNG", "images//connect-express.PNG"))
                    //    Thread.Sleep(10000);
                    //else
                    //{
                    //    Auto.ClickControlImageFind("ExpressVPN", "mainScreen-vpn.PNG", "images//connect-express.PNG");
                    //    Thread.Sleep(10000);
                    //}
                    //ip_new = CheckIP();
                    //if (ip_new != ip_old)
                    //    isSuccess = true;
                }
                else if (typeChangeIP == 5)
                {
                    //if (iTypeHotspot == 0)
                    //{
                    //    //hostpot
                    //    int timeStart = Environment.TickCount;
                    //    if (Auto.ClickControlImageFind("Hotspot Shield", "images//disconnect-hotspot.PNG"))
                    //        do
                    //        {
                    //            if (Environment.TickCount - timeStart > 20000)
                    //                return false;
                    //            Thread.Sleep(1000);
                    //        } while (Auto.ImageFind("Hotspot Shield", "images//connect-hotspot.PNG") == false);
                    //    Auto.ClickControlImageFind("Hotspot Shield", "images//connect-hotspot.PNG");
                    //    timeStart = Environment.TickCount;
                    //    do
                    //    {
                    //        if (Environment.TickCount - timeStart > 20000)
                    //            return false;
                    //        Thread.Sleep(1000);
                    //    } while (Auto.ImageFind("Hotspot Shield", "images//disconnect-hotspot.PNG") == false);
                    //}
                    //else if (iTypeHotspot == 1)
                    //{
                    //    Random rd = new Random();
                    //    //hostpot
                    //    int timeStart = Environment.TickCount;
                    //    string pointChange = Auto.ImageFind1("Hotspot Shield", "images//change-country-hotspot.PNG");
                    //    //WriteError(pointChange + "|" + DateTime.Now.ToString());
                    //    Point pointcc = new Point();
                    //    if (pointChange != "")
                    //        pointcc = new Point(Convert.ToInt32(pointChange.Split('|')[0]), Convert.ToInt32(pointChange.Split('|')[1]));
                    //    if (Auto.ImageFind("Hotspot Shield", "images//disconnect-hotspot.PNG") && Auto.ClickControlImageFind("Hotspot Shield", "images//change-country-hotspot.PNG", 0, 0, 20) && pointChange != null)
                    //    {
                    //        Thread.Sleep(2000);
                    //        KAutoHelper.AutoControl.MouseScroll(pointcc, rd.Next(-10, 10), true);
                    //        Thread.Sleep(1000);
                    //        KAutoHelper.AutoControl.MouseClick(pointcc.X, pointcc.Y);
                    //        timeStart = Environment.TickCount;
                    //        do
                    //        {
                    //            if (Environment.TickCount - timeStart > 20000)
                    //                return false;
                    //            Thread.Sleep(1000);
                    //        } while (Auto.ImageFind("Hotspot Shield", "images//disconnect-hotspot.PNG") == false);
                    //    }
                    //}
                    //ip_new = CheckIP();
                    //if (ip_new != ip_old)
                    //    isSuccess = true;
                }
                else if (typeChangeIP == 6)
                {
                    //nordvpn
                    //Random rd = new Random();
                    //string[] arrGroupName = new string[]
                    //{
                    //"Albania",
                    //"Argentina",
                    //"Australia",
                    //"Austria",
                    //"Belgium",
                    //"Bosnia and Herzegovina",
                    //"Brazil",
                    //"Bulgaria",
                    //"Canada",
                    //"Chile",
                    //"Costa Rica",
                    //"Croatia",
                    //"Cyprus",
                    //"Czech Republic",
                    //"Denmark",
                    //"Estonia",
                    //"Finland",
                    //"France",
                    //"Georgia",
                    //"Germany",
                    //"Greece",
                    //"Hong Kong",
                    //"Hungary",
                    //"Iceland",
                    //"India",
                    //"Indonesia",
                    //"Ireland",
                    //"Israel",
                    //"Italy",
                    //"Japan",
                    //"Latvia",
                    //"Luxembourg",
                    //"Mexico",
                    //"Moldova",
                    //"Netherlands",
                    //"New Zealand",
                    //"North Macedonia",
                    //"Norway",
                    //"Poland",
                    //"Portugal",
                    //"Romania",
                    //"Serbia",
                    //"Singapore",
                    //"Slovakia",
                    //"Slovenia",
                    //"South Africa",
                    //"South Korea",
                    //"Spain",
                    //"Sweden",
                    //"Switzedland",
                    //"Taiwan",
                    //"Thailand",
                    //"Turkey",
                    //"Ukraine",
                    //"United Kingdom",
                    //"United States",
                    //"Vietnam"
                    //};
                    //RunCMD("\"" + sLinkNord + "\\NordVPN.exe\" -d");
                    //Thread.Sleep(10000);
                    //RunCMD("\"" + sLinkNord + "\\NordVPN.exe\" -c -g" + arrGroupName[rd.Next(0, arrGroupName.Length - 1)]);
                    //int timeStart = Environment.TickCount;
                    //Thread.Sleep(5000);
                    //do
                    //{
                    //    Thread.Sleep(1000);
                    //    if (Environment.TickCount - timeStart > 20000)
                    //        return false;
                    //} while (Auto.ImageFind("", "images//disconnect-nord.PNG", "HwndWrapper[hsscp.exe;;34304629-92de-4140-a008-a2b4910674c9]") == false);

                    //ip_new = CheckIP();
                    //if (ip_new != ip_old)
                    //    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                ExportError(null, ex, "Error ChangeIP");
            }
            return isSuccess;
        }
        public static DateTime ConvertStringToDatetime(string datetime, string format = "dd/MM/yyyy HH:mm:ss")
        {
            return DateTime.ParseExact(datetime, format, System.Globalization.CultureInfo.InvariantCulture);
        }


        #region Get Totp
        public static string GetTotp(string input)
        {
            string otp = GetTotpServer(input);
            if (otp == "")
                otp = GetTotpClient(input);
            return otp;
        }
        public static string GetTotpServer(string input)
        {
            string otp = "";
            try
            {
                string html = "";

                input = input.Replace(" ", "").Trim();
                string url1 = "http://app.minsoftware.xyz/api/2fa1?secret=" + input;
                string url2 = "http://2fa.live/tok/" + input;

                for (int i = 0; i < 10; i++)
                {
                    otp = "";
                    try
                    {
                        html = ReadHTMLCode(url2);
                        if (html.Contains("token"))
                        {
                            JObject json = JObject.Parse(html);
                            otp = json["token"].ToString().Trim();
                        }
                    }
                    catch (Exception ex)
                    {
                        MCommon.Common.ExportError(ex, url2);
                    }

                    try
                    {
                        if (otp.Trim() == "")
                            otp = ReadHTMLCode(url1);
                    }
                    catch (Exception ex)
                    {
                        MCommon.Common.ExportError(ex, url1);
                    }

                    if (otp != "" && IsNumber(otp))
                    {
                        for (int j = otp.Length; j < 6; j++)
                            otp = "0" + otp;
                        break;
                    }

                    DelayTime(1);
                }
            }
            catch
            {
            }
            return otp;
        }
        public static string GetTotpClient(string input)
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
                MCommon.Common.ExportError(ex, $"GetTotp({input})");
            }

            return "";
        }

        private static int RemainingSeconds()
        {
            return 30 - (int)(((DateTime.UtcNow.Ticks - 621355968000000000L) / 10000000L) % 30);
        }

        private static byte[] GetBigEndianBytes(long input)
        {
            // Since .net uses little endian numbers, we need to reverse the byte order to get big endian.
            var data = BitConverter.GetBytes(input);
            Array.Reverse(data);
            return data;
        }

        private static long CalculateTime30FromTimestamp(DateTime timestamp)
        {
            var unixTimestamp = (timestamp.Ticks - 621355968000000000L) / 10000000L;
            var window = unixTimestamp / (long)30;
            return window;
        }

        private static string Digits(long input, int digitCount)
        {
            var truncatedValue = ((int)input % (int)Math.Pow(10, digitCount));
            return truncatedValue.ToString().PadLeft(digitCount, '0');
        }

        private static byte[] ToBytes(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                throw new ArgumentNullException("input");
            }

            input = input.TrimEnd('='); //remove padding characters
            int byteCount = input.Length * 5 / 8; //this must be TRUNCATED
            byte[] returnArray = new byte[byteCount];

            byte curByte = 0, bitsRemaining = 8;
            int mask = 0, arrayIndex = 0;

            foreach (char c in input)
            {
                int cValue = CharToValue(c);

                if (bitsRemaining > 5)
                {
                    mask = cValue << (bitsRemaining - 5);
                    curByte = (byte)(curByte | mask);
                    bitsRemaining -= 5;
                }
                else
                {
                    mask = cValue >> (5 - bitsRemaining);
                    curByte = (byte)(curByte | mask);
                    returnArray[arrayIndex++] = curByte;
                    curByte = (byte)(cValue << (3 + bitsRemaining));
                    bitsRemaining += 3;
                }
            }

            //if we didn't end with a full byte
            if (arrayIndex != byteCount)
            {
                returnArray[arrayIndex] = curByte;
            }

            return returnArray;
        }

        private static int CharToValue(char c)
        {
            int value = (int)c;

            //65-90 == uppercase letters
            if (value < 91 && value > 64)
            {
                return value - 65;
            }
            //50-55 == numbers 2-7
            if (value < 56 && value > 49)
            {
                return value - 24;
            }
            //97-122 == lowercase letters
            if (value < 123 && value > 96)
            {
                return value - 97;
            }

            throw new ArgumentException("Character is not a Base32 character.", "c");
        }

        public static DataTable ShuffleDataTable(DataTable dt)
        {
            DataTable dt1 = new DataTable();
            try
            {
                //string temp = "";
                //for (int i = 0; i < dt.Rows.Count; i++)
                //{
                //    for (int j = 0; j < dt.Columns.Count; j++)
                //    {
                //        temp += dt.Rows[i][j].ToString();
                //    }
                //    dt1.Rows.Add(temp.Split('|'));
                //    temp = "";
                //}

                dt1 = dt.Rows.Cast<DataRow>().OrderBy(r => Base.rd.Next()).CopyToDataTable();
            }
            catch
            {
            }
            return dt1;
        }
        #endregion

        public static bool CopyFolder(string pathFrom, string pathTo)
        {
            try
            {
                MCommon.Common.CreateFolder(pathTo);
                //Now Create all of the directories
                foreach (string dirPath in Directory.GetDirectories(pathFrom, "*",
                    SearchOption.AllDirectories))
                    Directory.CreateDirectory(dirPath.Replace(pathFrom, pathTo));

                //Copy all the files & Replaces any files with the same name
                foreach (string newPath in Directory.GetFiles(pathFrom, "*.*",
                    SearchOption.AllDirectories))
                    File.Copy(newPath, newPath.Replace(pathFrom, pathTo), true);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public static bool MoveFolder(string pathFrom, string pathTo)
        {
            try
            {
                Directory.Move(pathFrom, pathTo);
                return true;
            }
            catch (Exception ex)
            {
            }
            return false;
        }
        public static bool DeleteFolder(string pathFolder)
        {
            try
            {
                Directory.Delete(pathFolder, true);
                return true;
            }
            catch
            {
            }
            return false;
        }

        public static int GetRandInt(int min, int max)
        {
            if (min > max)
            {
                max = min;
            }
            return new Random().Next(min, max);
        }

    }
}
