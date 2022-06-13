using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using OpenQA.Selenium.Interactions;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCommon
{
    public class AutoChrome
    {
        public AutoChrome(Chrome chrome)
        {
            process = chrome.process;
            this.intPtrChrome = process.MainWindowHandle;
            this.chrome = chrome;
        }

        public Process process { get; set; }
        public IntPtr intPtrChrome { get; set; }
        public Chrome chrome { get; set; }
        public Image CaptureWindow(IntPtr handle)
        {
            IntPtr windowDC = User32.GetWindowDC(handle);
            User32.RECT rect = default(User32.RECT);
            User32.GetWindowRect(handle, ref rect);
            int nWidth = rect.right - rect.left;
            int nHeight = rect.bottom - rect.top;
            IntPtr intPtr = GDI32.CreateCompatibleDC(windowDC);
            IntPtr intPtr2 = GDI32.CreateCompatibleBitmap(windowDC, nWidth, nHeight);
            IntPtr hObject = GDI32.SelectObject(intPtr, intPtr2);
            GDI32.BitBlt(intPtr, 0, 0, nWidth, nHeight, windowDC, 0, 0, 13369376);
            GDI32.SelectObject(intPtr, hObject);
            GDI32.DeleteDC(intPtr);
            User32.ReleaseDC(handle, windowDC);
            Image result = Image.FromHbitmap(intPtr2);
            GDI32.DeleteObject(intPtr2);
            return result;
        }
        public bool ScreenCapture(string imagePathFolder, string fileName)
        {
            bool isSuccess = false;
            try
            {
                (CaptureWindow(intPtrChrome) as Bitmap).Save(imagePathFolder + (imagePathFolder.EndsWith(@"\") ? "" : @"\") + fileName + ".png", ImageFormat.Png);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.ScreenCapture({imagePathFolder},{fileName})");
            }
            return isSuccess;
        }
        public Bitmap ScreenCapture(int count = 1)
        {
            Bitmap bitmap = null;
            try
            {
                for (int i = 0; i < count; i++)
                {
                    try
                    {
                        bitmap = (Bitmap)CaptureWindow(intPtrChrome);
                        break;
                    }
                    catch (Exception ex)
                    {
                        ExportError(this, ex, "CaptureWindow");
                        MCommon.Common.DelayTime(1);
                    }
                }
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.ScreenCapture()");
            }
            return bitmap;
        }
        public void DelayTime(double timeDelay_Seconds)
        {
            try
            {
                Thread.Sleep(Convert.ToInt32(timeDelay_Seconds * 1000));
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"chrome.DelayTime({timeDelay_Seconds})");
            }
        }
        public bool CompareImage(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            if (subBitmap == null || mainBitmap == null)
                return false;
            Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
            Image<Bgr, byte> image2 = new Image<Bgr, byte>(subBitmap);
            Point? point = null;
            using (Image<Gray, float> image3 = image.MatchTemplate(image2, (TemplateMatchingType)5))
            {
                double[] array;
                double[] array2;
                Point[] array3;
                Point[] array4;
                image3.MinMax(out array, out array2, out array3, out array4);
                return array2[0] > percent;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="force"></param>
        /// <param name="delayPerTimes"></param>
        /// <param name="timeDelay"></param>
        /// <param name="type">0-scroll down, 1-scroll up</param>
        /// <returns></returns>
        public bool CheckCanScroll(int force, double delayPerTimes, int timeDelay = 0, int type = 0)
        {
            Bitmap btmB = ScreenCapture();
            if (type == 0)
                ScrollDown(force, delayPerTimes);
            else
                ScrollTop(force, delayPerTimes);
            DelayTime(timeDelay);
            Bitmap mainScreen = ScreenCapture();
            return !CompareImage(mainScreen, btmB, 0.9);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="force"></param>
        /// <param name="delayPerTimes"></param>
        /// <param name="timeDelay"></param>
        /// <param name="type">0-scroll down, 1-scroll up</param>
        /// <returns></returns>
        public bool CheckCanScrollChrome(int distance, int timeDelay = 0, int type = 0)
        {
            distance *= 100;
            Bitmap btmB = ScreenCapture();
            if (type == 0)
                ScrollChrome(distance);
            else
                ScrollChrome(-distance);
            DelayTime(timeDelay);
            Bitmap mainScreen = ScreenCapture();
            return !CompareImage(mainScreen, btmB, 0.9);
        }

        public Point? FindImage(string ImagePath, int timeSearchImage_Second = 0)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(ImagePath);
            FileInfo[] files = directoryInfo.GetFiles();
            Point? result;

            int timeStart = Environment.TickCount;
            do
            {
                //Capture screen
                Bitmap bitmap = ScreenCapture(3);
                if (bitmap == null)
                    break;

                //compare image
                result = null;
                foreach (FileInfo fileInfo in files)
                {
                    Bitmap subBitmap = (Bitmap)Image.FromFile(fileInfo.FullName);
                    result = ImageScanOpenCV.FindOutPoint(bitmap, subBitmap, 0.9);
                    if (result != null)
                    {
                        int posX = result.Value.X + new Random().Next(0, subBitmap.Width * 3 / 4);
                        int posY = result.Value.Y + new Random().Next(0, subBitmap.Height * 3 / 4);

                        return new Point(posX, posY);
                    }
                }

                MCommon.Common.DelayTime(1);
            } while (Environment.TickCount - timeStart <= timeSearchImage_Second * 1000);
            return null;
        }
        public bool CheckExistImage(string ImagePath, int timeSearchImage_Second = 0)
        {
            if (FindImage(ImagePath, timeSearchImage_Second) != null)
                return true;
            return false;
        }
        public int CheckExistImages(double timeWait_Second = 0, params string[] ImagePaths)
        {
            int result = 0;
            try
            {
                int timeStart = Environment.TickCount;
                while (true)
                {
                    for (int i = 0; i < ImagePaths.Length; i++)
                    {
                        if (CheckExistImage(ImagePaths[i]))
                        {
                            return (i + 1);
                        }
                    }

                    if (Environment.TickCount - timeStart > timeWait_Second * 1000)
                        break;
                    Thread.Sleep(1000);
                }
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.CheckExistImages({timeWait_Second},{string.Join("|", ImagePaths)})");
            }
            return result;
        }
        public bool Click(string ImagePath, int timeSearchImage_Second = 3)
        {
            bool isSuccess = false;
            try
            {
                Point? point = FindImage(ImagePath, timeSearchImage_Second);
                if (point != null)
                {
                    AutoControl.SendClickOnPosition(intPtrChrome, point.Value.X, point.Value.Y);
                    isSuccess = true;
                }
            }
            catch(Exception ex)
            {
            }
            return isSuccess;
        }

        public bool SendKeys(string content)
        {
            bool isSuccess = false;
            try
            {
                AutoControl.SendTextKeyBoard(intPtrChrome, content);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.SendKeys({content})");
            }
            return isSuccess;
        }
        public bool SendKeys(string content, double timeDelay_Second)
        {
            bool isSuccess = false;
            try
            {
                for (int i = 0; i < content.Length; i++)
                {
                    AutoControl.SendTextKeyBoard(intPtrChrome, content[i].ToString());
                    Thread.Sleep(Convert.ToInt32(timeDelay_Second * 1000));
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.SendKeys({content})");
            }
            return isSuccess;
        }
        public bool SendKeysChrome(string content)
        {
            bool isSuccess = false;
            try
            {
                new Actions(chrome.chrome).SendKeys(content).Perform();
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.SendKeys({content})");
            }
            return isSuccess;
        }
        public bool SendKeysChrome(string content, double timeDelay_Second)
        {
            bool isSuccess = false;
            try
            {
                for (int i = 0; i < content.Length; i++)
                {
                    new Actions(chrome.chrome).SendKeys(content[i].ToString()).Perform();

                    Thread.Sleep(Convert.ToInt32(timeDelay_Second * 1000));
                }
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.SendKeys({content},{timeDelay_Second})");
            }
            return isSuccess;
        }

        public bool SendEnter()
        {
            bool isSuccess = false;
            try
            {
                AutoControl.SendKeyBoardPress(intPtrChrome, VKeys.VK_RETURN);
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.SendEnter()");
            }
            return isSuccess;
        }

        public void Close()
        {
            try
            {
                process.Kill();
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"AutoChrome.Close()");
            }
        }

        public void ScrollChrome(int distance)
        {
            try
            {
                chrome.ExecuteScript("window.scrollBy({ top: " + distance + ",behavior: 'smooth'});");
            }
            catch { }
        }

        public bool Click(int X, int Y)
        {
            bool isSuccess = false;
            try
            {
                AutoControl.SendClickOnPosition(intPtrChrome, X, Y);
                isSuccess = true;
            }
            catch
            {
            }
            return isSuccess;
        }
        public bool Click(Point? point)
        {
            bool isSuccess = false;
            try
            {
                if (point != null)
                {
                    AutoControl.SendClickOnPosition(intPtrChrome, point.Value.X, point.Value.Y);
                    isSuccess = true;
                }
            }
            catch
            {
            }
            return isSuccess;
        }


        public void ScrollDown(int force = 10, double delay = 100)
        {
            try
            {
                for (int i = 0; i < force; i++)
                {
                    AutoControl.SendKeyBoardPress(intPtrChrome, VKeys.VK_DOWN);
                    DelayTime(delay);
                }
            }
            catch { }

        }
        public void ScrollTop(int force = 10, double delay = 100)
        {
            try
            {
                for (int i = 0; i < force; i++)
                {
                    AutoControl.SendKeyBoardPress(intPtrChrome, VKeys.VK_UP);
                    DelayTime(delay);
                }
            }
            catch { }

        }

        public static void ExportError(AutoChrome autoChrome, Exception ex, string error = "")
        {
            try
            {
                if (!Directory.Exists("log"))
                    Directory.CreateDirectory("log");
                if (!Directory.Exists("log\\images"))
                    Directory.CreateDirectory("log\\images");

                Random rrrd = new Random();
                string fileName = DateTime.Now.Day + "_" + DateTime.Now.Month + "_" + DateTime.Now.Year + "_" + DateTime.Now.Hour + "_" + DateTime.Now.Minute + "_" + DateTime.Now.Second + "_" + rrrd.Next(1000, 9999);

                if (autoChrome != null)
                {
                    autoChrome.ScreenCapture(@"log\images\", fileName);
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
    }
    class GDI32
    {
        // Token: 0x060000BE RID: 190
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hObject, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hObjectSource, int nXSrc, int nYSrc, int dwRop);

        // Token: 0x060000BF RID: 191
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleBitmap(IntPtr hDC, int nWidth, int nHeight);

        // Token: 0x060000C0 RID: 192
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateCompatibleDC(IntPtr hDC);

        // Token: 0x060000C1 RID: 193
        [DllImport("gdi32.dll")]
        public static extern bool DeleteDC(IntPtr hDC);

        // Token: 0x060000C2 RID: 194
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        // Token: 0x060000C3 RID: 195
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDC, IntPtr hObject);

        // Token: 0x040001BD RID: 445
        public const int SRCCOPY = 13369376;
    }
    class User32
    {
        // Token: 0x060000C5 RID: 197
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();

        // Token: 0x060000C6 RID: 198
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);

        // Token: 0x060000C7 RID: 199
        [DllImport("user32.dll")]
        public static extern IntPtr ReleaseDC(IntPtr hWnd, IntPtr hDC);

        // Token: 0x060000C8 RID: 200
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowRect(IntPtr hWnd, ref User32.RECT rect);

        // Token: 0x02000024 RID: 36
        public struct RECT
        {
            // Token: 0x040001C9 RID: 457
            public int left;

            // Token: 0x040001CA RID: 458
            public int top;

            // Token: 0x040001CB RID: 459
            public int right;

            // Token: 0x040001CC RID: 460
            public int bottom;
        }
    }
    class ImageScanOpenCV
    {
        // Token: 0x06000074 RID: 116 RVA: 0x00004460 File Offset: 0x00002660
        public static Bitmap GetImage(string path)
        {
            return new Bitmap(path);
        }

        // Token: 0x06000075 RID: 117 RVA: 0x00004478 File Offset: 0x00002678
        public static Bitmap Find(string main, string sub, double percent = 0.9)
        {
            Bitmap image = ImageScanOpenCV.GetImage(main);
            Bitmap image2 = ImageScanOpenCV.GetImage(sub);
            return ImageScanOpenCV.Find(main, sub, percent);
        }

        // Token: 0x06000076 RID: 118 RVA: 0x000044A4 File Offset: 0x000026A4
        public static Bitmap Find(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
            Image<Bgr, byte> image2 = new Image<Bgr, byte>(subBitmap);
            Image<Bgr, byte> image3 = image.Copy();
            using (Image<Gray, float> image4 = image.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed))
            {
                double[] array;
                double[] array2;
                Point[] array3;
                Point[] array4;
                image4.MinMax(out array, out array2, out array3, out array4);
                bool flag = array2[0] > percent;
                if (flag)
                {
                    Rectangle rect = new Rectangle(array4[0], image2.Size);
                    image3.Draw(rect, new Bgr(System.Drawing.Color.Red), 2, LineType.EightConnected, 0);
                }
                else
                {
                    image3 = null;
                }
            }
            return (image3 == null) ? null : image3.ToBitmap();
        }

        // Token: 0x06000077 RID: 119
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);

        // Token: 0x06000078 RID: 120 RVA: 0x00004550 File Offset: 0x00002750
        public static Point? FindOutPoint(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            bool flag = subBitmap == null || mainBitmap == null;
            Point? result;
            if (flag)
            {
                result = null;
            }
            else
            {
                bool flag2 = subBitmap.Width > mainBitmap.Width || subBitmap.Height > mainBitmap.Height;
                if (flag2)
                {
                    result = null;
                }
                else
                {
                    Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
                    Image<Bgr, byte> template = new Image<Bgr, byte>(subBitmap);
                    Point? point = null;
                    using (Image<Gray, float> image2 = image.MatchTemplate(template, TemplateMatchingType.CcoeffNormed))
                    {
                        double[] array;
                        double[] array2;
                        Point[] array3;
                        Point[] array4;
                        image2.MinMax(out array, out array2, out array3, out array4);
                        bool flag3 = array2[0] > percent;
                        if (flag3)
                        {
                            point = new Point?(array4[0]);
                        }
                    }
                    GC.Collect();
                    GC.WaitForPendingFinalizers();
                    result = point;
                }
            }
            return result;
        }

        // Token: 0x06000079 RID: 121 RVA: 0x00004634 File Offset: 0x00002834
        public static List<Point> FindOutPoints(Bitmap mainBitmap, Bitmap subBitmap, double percent = 0.9)
        {
            Image<Bgr, byte> image = new Image<Bgr, byte>(mainBitmap);
            Image<Bgr, byte> image2 = new Image<Bgr, byte>(subBitmap);
            List<Point> list = new List<Point>();
            for (; ; )
            {
                using (Image<Gray, float> image3 = image.MatchTemplate(image2, TemplateMatchingType.CcoeffNormed))
                {
                    double[] array;
                    double[] array2;
                    Point[] array3;
                    Point[] array4;
                    image3.MinMax(out array, out array2, out array3, out array4);
                    bool flag = array2[0] > percent;
                    if (!flag)
                    {
                        break;
                    }
                    Rectangle rect = new Rectangle(array4[0], image2.Size);
                    image.Draw(rect, new Bgr(System.Drawing.Color.Blue), -1, LineType.EightConnected, 0);
                    list.Add(array4[0]);
                }
            }
            return list;
        }

        // Token: 0x0600007A RID: 122 RVA: 0x000046EC File Offset: 0x000028EC
        public static List<Point> FindColor(Bitmap mainBitmap, System.Drawing.Color color)
        {
            int num = color.ToArgb();
            List<Point> list = new List<Point>();
            try
            {
                for (int i = 0; i < mainBitmap.Width; i++)
                {
                    for (int j = 0; j < mainBitmap.Height; j++)
                    {
                        bool flag = num.Equals(mainBitmap.GetPixel(i, j).ToArgb());
                        if (flag)
                        {
                            list.Add(new Point(i, j));
                        }
                    }
                }
            }
            finally
            {
                if (mainBitmap != null)
                {
                    ((IDisposable)mainBitmap).Dispose();
                }
            }
            return list;
        }

    }
}
