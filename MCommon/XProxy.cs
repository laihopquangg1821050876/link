using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCommon
{
    class XProxy
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServicesURL"></param>
        /// <param name="proxy"></param>
        /// <returns>-1: error, 0-fail, 1-success</returns>
        public static int ResetProxy(string ServicesURL, string proxy)
        {
            int success = 0;
            try
            {
                ServicesURL = ServicesURL.TrimEnd('/');
                string url = ServicesURL + "/reset?proxy=" + proxy;
                RequestXNet request = new RequestXNet("", "", "", 0);
                string html = request.RequestGet(url);

                if(JObject.Parse(html)["msg"].ToString() == "command_sent")
                {
                    for (int i = 0; i < 120; i++)
                    {
                        if(CheckLiveProxy(ServicesURL, proxy))
                            return 1;
                        MCommon.Common.DelayTime(1);
                    }
                }
            }
            catch
            {
                success = -1;
            }
            return success;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServicesURL"></param>
        /// <param name="proxy"></param>
        /// <returns>-1: error, 0-fail, 1-success</returns>
        public static int ResetAllProxy(string ServicesURL, List<string> lstProxy)
        {
            int success = 0;
            try
            {                
                ServicesURL = ServicesURL.TrimEnd('/');
                string url = ServicesURL + "/reset_all";
                RequestXNet request = new RequestXNet("","","",0);
                string html = request.RequestGet(url);

                if(Convert.ToBoolean(JObject.Parse(html)["status"].ToString()))
                {
                    string proxy = "";
                    for (int i = 0; i < 120; i++)
                    {
                        for (int j = 0; j < lstProxy.Count; j++)
                        {
                            proxy = lstProxy[j];
                            if (CheckLiveProxy(ServicesURL, proxy))
                                lstProxy.RemoveAt(j--);
                        }
                        if(lstProxy.Count==0)
                            return 1;
                        MCommon.Common.DelayTime(1);
                    }
                }
            }
            catch
            {
                success = -1;
            }
            return success;
        }
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
        /// <summary>
        /// 
        /// </summary>
        /// <param name="ServicesURL"></param>
        /// <param name="proxy"></param>
        /// <returns>-1: error, 0-fail, 1-success</returns>
        public static int ResetProxy(string ServicesURL, List<string> lstXProxy)
        {
            int success = 0;
            try
            {
                List<string> lstProxy = CloneList(lstXProxy);
                for (int i = 0; i < lstProxy.Count; i++)
                {
                    ResetProxy(ServicesURL, lstProxy[i]);
                }

                string proxy = "";
                for (int i = 0; i < 120; i++)
                {
                    for (int j = 0; j < lstProxy.Count; j++)
                    {
                        proxy = lstProxy[j];
                        if (CheckLiveProxy(ServicesURL, proxy))
                            lstProxy.RemoveAt(j--);
                    }
                    if (lstProxy.Count == 0)
                        return 1;
                    MCommon.Common.DelayTime(1);
                }
            }
            catch
            {
                success = -1;
            }
            return success;
        }
        public static bool CheckLiveProxy(string ServicesURL, string proxy)
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
    }
}
