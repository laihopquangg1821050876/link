using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using maxcare.Properties;

namespace MCommon
{
    class XproxyProxy_
    {
        object k = new object();

        object k1 = new object();
        public int typeProxy;

        public XproxyProxy_(string ServicesURL, string proxy, int typeProxy, int limit_theads_use)
        {
            this.ServicesURL = ServicesURL;
            this.proxy = proxy;
            this.limit_theads_use = limit_theads_use;
            this.ip = "";
            this.typeProxy = typeProxy;
        }

        public bool ChangeProxy()
        {
            bool success = false;
            try
            {
                ServicesURL = ServicesURL.TrimEnd('/');
                string url = ServicesURL + "/reset?proxy=" + proxy;
                RequestXNet request = new RequestXNet("", "", "", 0);
                string html = request.RequestGet(url);

                if (JObject.Parse(html)["msg"].ToString() == "command_sent" || JObject.Parse(html)["msg"].ToString() == "OK" || JObject.Parse(html)["msg"].ToString().ToLower() == "ok")
                {
                    for (int i = 0; i < 120; i++)
                    {
                        if (CheckLiveProxy())
                        {
                            Thread.Sleep(1000);
                            //for (int j = 0; j < 20; j++)
                            //{
                            //    string ipt = Common.CheckProxy(proxy, typeProxy);
                            //    if (ipt != "")
                            //    {
                            //        this.ip = ipt;
                            //        break;
                            //    }
                            //    else
                            //    {
                            //        Thread.Sleep(1000);
                            //    }
                            //}
                            return true;
                        }
                        Thread.Sleep(1000);
                    }
                }
            }
            catch
            {
                success = false;
            }
            return success;
        }

        public void DecrementDangSuDung()
        {
            lock (k)
            {
                dangSuDung--;
                if (dangSuDung == 0 && daSuDung == limit_theads_use)
                    daSuDung = 0;
            }
        }

        public bool CheckLiveProxy()
        {
            bool isSuccess = false;
            try
            {
                ServicesURL = ServicesURL.TrimEnd('/');
                string url = ServicesURL + "/status?proxy=" + proxy;
                RequestXNet request = new RequestXNet("", "", "", 0);
                string html = request.RequestGet(url);
                isSuccess = Convert.ToBoolean(JObject.Parse(html)["status"].ToString());
            }
            catch
            {
            }
            return isSuccess;
        }

        private string ServicesURL;
        public string proxy;
        public string ip = "";

        //số lượng đang sử dụng
        public int dangSuDung = 0;

        //đã sử dụng
        public int daSuDung = 0;

        //số lượng tối đa cùng lúc
        public int limit_theads_use = 3;
    }
}
