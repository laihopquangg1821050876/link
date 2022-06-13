using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

namespace MCommon
{
    class TMProxy
    {
        Random rd = new Random();
        public string api_key { get; set; }
        public string proxy { get; set; }
        public int typeProxy { get; set; }
        public string ip { get; set; }
        public int timeout { get; set; }
        public int port { get; set; }
        public int next_change { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="api_key"></param>
        /// <param name="typeProxy">0-http,1-sock5</param>
        /// <param name="limit_theads_use"></param>
        public TMProxy(string api_key, int typeProxy, int limit_theads_use)
        {
            this.api_key = api_key;
            this.proxy = "";
            this.ip = "";
            this.port = 0;
            this.next_change = 0;
            this.typeProxy = typeProxy;

            this.limit_theads_use = limit_theads_use;
            dangSuDung = 0;
            daSuDung = 0;
        }

        public static bool CheckApiProxy(string apiProxy)
        {
            string data = "{\"api_key\": \"" + apiProxy + "\"}";

            string svcontent = RequestPost("https://tmproxy.com/api/proxy/stats", data);
            if (svcontent != "")
            {
                try
                {
                    JObject jobject = JObject.Parse(svcontent);
                    string date_expired = jobject["data"]["expired_at"].ToString();
                    DateTime date = MCommon.Common.ConvertStringToDatetime(date_expired, "HH:mm:ss dd/MM/yyyy");
                    if (DateTime.Compare(date, DateTime.Now) > 0)
                        return true;
                }
                catch
                {
                }
            }
            else
            {
            }

            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>0 không đổi được proxy, 1 đổi thành công, -1 đạt số lượng tối đa + co the doi proxy, -2 đạt số lượng tối đa + dang dc su dung</returns>
        /// 

        object k1 = new object();
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
        public int GetTimeOut()
        {
            CheckStatusProxy();
            return timeout;
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

        public string Md5Encode(string text)
        {
            MD5 obj = MD5.Create();
            byte[] data = obj.ComputeHash(System.Text.Encoding.UTF8.GetBytes(text));
            StringBuilder s = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                s.Append(data[i].ToString("x2"));
            }
            return s.ToString();
        }
        public bool ChangeProxy()
        {
            this.next_change = 0;
            this.proxy = "";
            this.ip = "";
            this.port = 0;

            string secret_key = "abccd9f3bf38f38414cb87e36f76c8e4";
            int secret_code = 8989;

            string timeNow = GetSVContent("https://tmproxy.com/api/proxy/current-time");

            long iTimeNow = 0;
            try
            {
                iTimeNow = Convert.ToInt64(timeNow);
            }
            catch
            {
                iTimeNow = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
            }
            long compose = Convert.ToInt64(iTimeNow / 60) + secret_code;
            string sign = $"{secret_key}{api_key}{compose}";
            string signMD5 = Md5Encode(sign);
            string data = "{\"api_key\": \"" + api_key + "\",\"sign\": \"" + signMD5 + "\"}";

            string svcontent = RequestPost("https://tmproxy.com/api/proxy/get-new-proxy", data);

            //MCommon.Common.ExportError(null,
            //    "timeNow:" + timeNow + "\r\n" +
            //    "iTimeNow:" + iTimeNow + "\r\n" +
            //    "compose:" + compose + "\r\n" +
            //    "sign:" + sign + "\r\n" +
            //    "signMD5:" + signMD5 + "\r\n" +
            //    "Respond TMProxy" + "\r\n" + svcontent);

            if (svcontent != "")
            {
                try
                {
                    JObject jobject = JObject.Parse(svcontent);
                    string next_change_temp = Regex.Match(JObject.Parse(svcontent)["message"].ToString(), "\\d+").Value;
                    this.next_change = next_change_temp == "" ? 0 : int.Parse(next_change_temp);

                    if (next_change == 0)
                    {
                        if (this.typeProxy == 0)
                        {
                            this.proxy = jobject["data"]["https"].ToString();
                            string[] array = this.proxy.Split(new char[]
                            {
                                ':'
                            });
                            this.ip = array[0];
                            this.port = int.Parse(array[1]);
                        }
                        else
                        {
                            this.proxy = jobject["data"]["socks5"].ToString();
                            string[] array = this.proxy.Split(new char[]
                            {
                                ':'
                            });
                            this.ip = array[0];
                            this.port = int.Parse(array[1]);
                        }
                        return true;
                    }

                }
                catch
                {
                }
            }

            return false;
        }
        private string GetSVContent(string url)
        {
            string text = "";
            try
            {
                //using (WebClient webClient = new WebClient())
                //{
                //    ServicePointManager.Expect100Continue = true;
                //    ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                //    webClient.Headers.Add("user-agent", SetupFolder.GetUseragentIPhone(rd));
                //    text = webClient.DownloadString(url);
                //}

                text = new RequestHttp().RequestGet(url);
                if (string.IsNullOrEmpty(text))
                    text = "";
            }
            catch
            {
            }
            return text;
        }
        public bool CheckStatusProxy()
        {
            this.next_change = 0;
            this.proxy = "";
            this.ip = "";
            this.port = 0;
            this.timeout = 0;

            string data = "{\"api_key\": \"" + api_key + "\"}";

            string svcontent = RequestPost("https://tmproxy.com/api/proxy/get-current-proxy", data);
            if (svcontent != "")
            {
                try
                {
                    JObject jobject = JObject.Parse(svcontent);
                    if (jobject["code"].ToString() == "0")
                    {
                        this.next_change = Convert.ToInt32(jobject["data"]["next_request"].ToString());
                        this.timeout = Convert.ToInt32(jobject["data"]["timeout"].ToString());

                        if (this.typeProxy == 0)
                        {
                            this.proxy = jobject["data"]["https"].ToString();
                            string[] array = this.proxy.Split(':');
                            this.ip = array[0];
                            this.port = int.Parse(array[1]);
                        }
                        else
                        {
                            this.proxy = jobject["data"]["socks5"].ToString();
                            string[] array = this.proxy.Split(':');
                            this.ip = array[0];
                            this.port = int.Parse(array[1]);
                        }
                        return true;
                    }
                }
                catch
                {
                }
            }

            return false;
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

        //private static string RequestPost(string url, string data)
        //{
        //    string text = "";
        //    try
        //    {
        //        var cli = new WebClient();
        //        ServicePointManager.Expect100Continue = true;
        //        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
        //        cli.Headers[HttpRequestHeader.ContentType] = "application/json";
        //        text = cli.UploadString(url, data);
        //        if (string.IsNullOrEmpty(text))
        //            text = "";
        //    }
        //    catch(Exception ex)
        //    {
        //        MCommon.Common.ExportError(null, ex, "Request Post");
        //        text = "";
        //    }
        //    return text;
        //}
        private static string RequestPost(string url, string data)
        {
            string text = "";
            try
            {
                HttpClient httpClient = new HttpClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                HttpContent c = new StringContent(data, Encoding.UTF8, "application/json");
                var t = Task.Run(() => PostURI(new Uri(url), c));
                t.Wait();
                text = t.Result;
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "Request Post");
                text = "";
            }
            return text;
        }
        public static string RequestGet(string url)
        {
            string text = "";
            try
            {
                HttpClient httpClient = new HttpClient();
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                var t = Task.Run(() => GetURI(new Uri(url)));
                t.Wait();
                text = t.Result;
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "Request get");
                text = "";
            }
            return text;
        }
        static async Task<string> PostURI(Uri u, HttpContent c)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.PostAsync(u, c);
                if (result.IsSuccessStatusCode)
                    response = await result.Content.ReadAsStringAsync();
            }
            return response;
        }
        static async Task<string> GetURI(Uri u)
        {
            var response = string.Empty;
            using (var client = new HttpClient())
            {
                HttpResponseMessage result = await client.GetAsync(u);
                if (result.IsSuccessStatusCode)
                    response = await result.Content.ReadAsStringAsync();
            }
            return response;
        }

        //số lượng đang sử dụng
        public int dangSuDung = 0;

        //đã sử dụng
        public int daSuDung = 0;

        //số lượng tối đa cùng lúc
        public int limit_theads_use = 3;

    }
}
