using HttpRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCommon
{
    public class RequestHttp
    {
        public RequestHttp(string cookie = "", string userAgent = "",string proxy="",int typeProxy=0)
        {
            //Set UserAgent
            if (userAgent == "")
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36";
            else
                UserAgent = userAgent;

            request = new RequestHTTP();
            request.SetSSL(System.Net.SecurityProtocolType.Tls12);
            request.SetKeepAlive(true);
            request.SetDefaultHeaders(new string[]
            {
                "content-type: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8",
                "user-agent: "+UserAgent
            });

            if (cookie != "")
            {
                AddCookie(cookie);
            }

            this.Proxy = proxy;
        }

        public RequestHTTP request;
        private string UserAgent;
        private string Proxy;

        public string RequestGet(string url)
        {
            try
            {
                if (Proxy != "")
                {
                    if (Proxy.Contains(":"))
                        return request.Request("GET", url, null, null, true, new System.Net.WebProxy(Proxy.Split(':')[0], Convert.ToInt32(Proxy.Split(':')[1]))).ToString();
                    else
                        return request.Request("GET", url, null, null, true, new System.Net.WebProxy("127.0.0.1", Convert.ToInt32(Proxy))).ToString();
                }
                else
                    return request.Request("GET", url).ToString();
            }
            catch
            {
            }
            return "";
        }
        public string RequestPost(string url, string data = "")
        {
            if (Proxy != "")
            {
                if (Proxy.Contains(":"))
                    return request.Request("POST", url, null, Encoding.ASCII.GetBytes(data), true, new System.Net.WebProxy(Proxy.Split(':')[0], Convert.ToInt32(Proxy.Split(':')[1]))).ToString();
                else
                    return request.Request("POST", url, null, Encoding.ASCII.GetBytes(data), true, new System.Net.WebProxy("127.0.0.1", Convert.ToInt32(Proxy))).ToString();
            }
            else
                return request.Request("POST", url, null, Encoding.ASCII.GetBytes(data)).ToString();
        }

        public void AddCookie(string cookie)
        {
            var temp = cookie.Split(';');
            string cookie_temp = "";
            foreach (var item in temp)
            {
                var temp2 = item.Split('=');
                if (temp2.Count() > 1)
                {
                    try
                    {
                        cookie_temp += temp2[0] + "=" + temp2[1] + ";";
                    }
                    catch 
                    {
                    }
                }
            }
            request.SetDefaultHeaders(new string[]
            {
                "content-type: text/html,application/xhtml+xml,application/xml;q=0.9,*/*;q=0.8;charset=UTF-8",
                "user-agent: "+UserAgent,
                "cookie: "+cookie_temp
            });
        }

        public string GetCookie()
        {
            return request.GetCookiesString();
        }
    }
}
