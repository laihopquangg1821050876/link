using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCommon
{
    class TinsoftProxy
    {
        public string api_key { get; set; }

        public string proxy { get; set; }
        public string ip { get; set; }
        public int port { get; set; }
        public int timeout { get; set; }
        public int next_change { get; set; }
        public int location { get; set; }

        public TinsoftProxy(string api_key, int limit_theads_use, int location = 0)
        {
            this.api_key = api_key;
            this.proxy = "";
            this.ip = "";
            this.port = 0;
            this.timeout = 0;
            this.next_change = 0;
            this.location = location;

            this.limit_theads_use = limit_theads_use;
            dangSuDung = 0;
            daSuDung = 0;
            canChangeIP = true;
        }

        object k1 = new object();



        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 Đang đợi reset proxy, 1 Có thể sử dụng, 2 Đạt max sử dụng</returns>
        /// 
        public string TryToGetMyIP()
        {
            lock (k1)
            {
                if (dangSuDung == 0)
                {
                    if (daSuDung > 0 && daSuDung < limit_theads_use)
                    {
                        if (GetTimeOut() < 30)
                        {
                            if (ChangeProxy())
                                goto success;
                            return "0";
                        }
                        goto success;
                    }
                    else
                    {
                        if (ChangeProxy())
                            goto success;
                        return "0";
                    }
                }
                else
                {
                    if (daSuDung < limit_theads_use)
                    {
                        if (GetTimeOut() < 30)
                        {
                            if (ChangeProxy())
                                goto success;
                            return "0";
                        }
                        goto success;
                    }
                    return "2";
                }

            success:
                daSuDung++;
                dangSuDung++;
                return "1";
            }
        }


        object k = new object();
        public void DecrementDangSuDung()
        {
            lock (k)
            {
                dangSuDung--;
                if (dangSuDung == 0 && daSuDung == limit_theads_use)
                    daSuDung = 0;
            }
        }


        public bool ChangeProxy()
        {
            lock (k)
            {
                if (this.checkLastRequest())
                {
                    this.errorCode = "";
                    this.next_change = 0;
                    this.proxy = "";
                    this.ip = "";
                    this.port = 0;
                    this.timeout = 0;
                    string svcontent = this.getSVContent(string.Concat(new object[]
                    {
                    this.svUrl,
                    "/api/changeProxy.php?key=",
                    this.api_key,
                    "&location=",
                    this.location
                    }));
                    if (svcontent != "")
                    {
                        try
                        {
                            JObject jobject = JObject.Parse(svcontent);
                            if (bool.Parse(jobject["success"].ToString()))
                            {
                                this.proxy = jobject["proxy"].ToString();
                                string[] array = this.proxy.Split(':');
                                this.ip = array[0];
                                this.port = int.Parse(array[1]);
                                this.timeout = int.Parse(jobject["timeout"].ToString());
                                this.next_change = int.Parse(jobject["next_change"].ToString());
                                this.errorCode = "";
                                return true;
                            }
                            this.errorCode = jobject["description"].ToString();
                            this.next_change = int.Parse(jobject["next_change"].ToString());
                        }
                        catch
                        {
                        }
                    }
                    else
                    {
                        this.errorCode = "request server timeout!";
                    }
                }
                else
                {
                    this.errorCode = "Request so fast!";
                }
                return false;
            }
        }

        public string GetProxy()
        {
            bool done = false;
            do
            {
                done = CheckStatusProxy();
            } while (!done);

            return proxy;
        }
        public int GetTimeOut()
        {
            bool done = false;
            do
            {
                done = CheckStatusProxy();
            } while (!done);
            return timeout;
        }
        public int GetNextChange()
        {
            bool done = false;
            do
            {
                done = CheckStatusProxy();
            } while (!done);
            return next_change;
        }
        public bool CheckStatusProxy()
        {
            lock (k)
            {
                this.errorCode = "";
                this.next_change = 0;
                this.proxy = "";
                this.ip = "";
                this.port = 0;
                this.timeout = 0;
                string svcontent = this.getSVContent(string.Concat(new object[]
                {
                    this.svUrl,
                    "/api/getProxy.php?key=",
                    this.api_key
                }));
                if (svcontent != "")
                {
                    try
                    {
                        JObject jobject = JObject.Parse(svcontent);
                        if (bool.Parse(jobject["success"].ToString()))
                        {
                            this.proxy = jobject["proxy"].ToString();
                            string[] array = this.proxy.Split(':');
                            this.ip = array[0];
                            this.port = int.Parse(array[1]);
                            this.timeout = int.Parse(jobject["timeout"].ToString());
                            this.next_change = int.Parse(jobject["next_change"].ToString());
                            this.errorCode = "";
                            return true;
                        }
                        else
                        {

                        }
                        this.errorCode = jobject["description"].ToString();
                        if ((JToken)jobject["next_change"] != null)
                            this.next_change = int.Parse(jobject["next_change"].ToString());
                    }
                    catch (Exception ex)
                    {
                    }
                }
                else
                {
                    this.errorCode = "request server timeout!";
                }

                return false;
            }
        }

        private bool checkLastRequest()
        {
            try
            {
                DateTime dateTime = new DateTime(2001, 1, 1);
                long ticks = DateTime.Now.Ticks - dateTime.Ticks;
                TimeSpan timeSpan = new TimeSpan(ticks);
                int num = (int)timeSpan.TotalSeconds;
                if (num - this.lastRequest >= 10)
                {
                    this.lastRequest = num;
                    return true;
                }
            }
            catch
            {
            }
            return false;
        }

        private string getSVContent(string url)
        {
            Console.WriteLine(url);
            string text = "";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    text = webClient.DownloadString(url);
                    //File.AppendAllText("check.txt", "["+DateTime.Now.ToString("HH:mm:ss")+"]: "+url + " => " + text+Environment.NewLine);
                }
                if (string.IsNullOrEmpty(text))
                {
                    text = "";
                }
            }
            catch
            {
                text = "";
            }
            return text;
        }

        public string errorCode = "";

        private string svUrl = "http://proxy.tinsoftsv.com";

        private int lastRequest = 0;

        //có thể đổi
        public bool canChangeIP = true;

        //số lượng đang sử dụng
        public int dangSuDung = 0;

        //đã sử dụng
        public int daSuDung = 0;

        //số lượng tối đa cùng lúc
        public int limit_theads_use = 3;




        private static string GetSVContent(string url)
        {
            Console.WriteLine(url);
            string text = "";
            try
            {
                using (WebClient webClient = new WebClient())
                {
                    text = webClient.DownloadString(url);
                }
                if (string.IsNullOrEmpty(text))
                {
                    text = "";
                }
            }
            catch
            {
                text = "";
            }
            return text;
        }
        public static bool CheckApiProxy(string apiProxy)
        {
            string svcontent = GetSVContent("http://proxy.tinsoftsv.com/api/getKeyInfo.php?key=" + apiProxy);
            if (svcontent != "")
            {
                JObject jobject = JObject.Parse(svcontent);
                if (bool.Parse(jobject["success"].ToString()))
                    return true;
            }
            else
            {
            }

            return false;
        }
        public static List<string> GetListKey(string api_user)
        {
            List<string> lstApiKey = new List<string>();
            try
            {
                RequestXNet request = new RequestXNet("", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)", "", 0);
                string rq = request.RequestGet("http://proxy.tinsoftsv.com/api/getUserKeys.php?key=" + api_user);
                JObject json = JObject.Parse(rq);
                foreach (var item in json["data"])
                {
                    if (Convert.ToBoolean(item["success"].ToString()))
                        lstApiKey.Add(item["key"].ToString());
                }
            }
            catch
            {
            }
            return lstApiKey;
        }
    }
}
