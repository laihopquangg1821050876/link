using maxcare;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace MCommon
{
    class CommonRequest
    {

        public static string GetUidIg(string username)
        {
            try
            {
                RequestXNet request = new RequestXNet("", "", "", 0);
                string rq = request.RequestPost("https://findidfb.com/find-instagram-id/", "user_name=" + username);
                string uid = Regex.Match(rq, "User ID: <b>(.*?)</b>").Groups[1].Value;
                string name = Regex.Match(rq, "Full Name(.*?)</b>").Value;
                name = Regex.Match(name, "<b>(.*?)</b>").Groups[1].Value;
                return uid + "|" + name;
            }
            catch
            {
            }
            return "|";
        }

        //public static void JoinGroup(string cookie, string group_id)
        //{
        //    string ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
        //    string fb_dtsg = MCommon.CommonRequest.GetFbDtsg(cookie);

        //    string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
        //    RequestXNet request = new RequestXNet(cookie, ua, "", 0);

        //    string url = "https://www.facebook.com/groups/membership/r2j/";
        //    string data = "group_id=" + group_id + "&ref=group_jump_header&client_custom_questions=1&__user=" + uid + "&__a=1&__dyn=7AgNe-4amaAxd2u6aJGeFxqeCwDKEKEW6qrWo8ovxGdwIhE98nwgUaoeo9qUC3eEbbyEjKewXwgUOdwJKdwVxCu58O5U7S4E9ohwoU8-1rG0HFU20wADx6q7ooxu6U6O5ovyUvwHwrEsxeEgy9E6aEymu4EhwIXwABojUa8gzaz88U8K1lwLx21ygG4equV8y1kyE4G4UO68pwAwhVKcxp2Utwwx-2y8w9m6E&__req=m&__be=1&__pc=PHASED%3Aufi_home_page_pkg&dpr=1&__rev=1000587207&__comet_req=false&fb_dtsg=" + fb_dtsg + "&jazoest=22048&__spin_r=1000587207&__spin_b=trunk&__spin_t=1554886381";
        //    string html = request.RequestPost(url, data);
        //}


        public static string RandomText(int length = 16)
        {
            Random rd = new Random();
            string x = "abcdef1234567890";
            string output = "";
            for (int i = 0; i < length; i++)
            {
                output += Convert.ToString(x[rd.Next(0, x.Length)]);
            }
            return output;
        }

        //public static void AddFriend(string cookie, string uid_to)
        //{
        //    string _gencsid = RandomText(8) + "-" + RandomText(4) + "-" + RandomText(4) + "-" + RandomText(4) + "-" + RandomText(12);
        //    string ua = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/86.0.4240.75 Safari/537.36";
        //    string fb_dtsg = MCommon.CommonRequest.GetFbDtsg(cookie);

        //    string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
        //    RequestXNet request = new RequestXNet(cookie, ua, "", 0);

        //    string variables = "{\"0\":{\"source\":\"profile_button\",\"friend_requestee_ids\":[\"" + uid_to + "\"],\"client_mutation_id\":\"" + _gencsid + "\",\"actor_id\":\"" + uid + "\"}}";
        //    string url = "https://www.facebook.com/api/graphql/";
        //    string data = "__a=1&fb_dtsg="+fb_dtsg+ "&variables="+ variables + "&doc_id=1577255185642828";
        //    string html = request.RequestPost(url, data);
        //}

        public static string CheckLiveCookie(string cookie, string userAgent, string proxy, int typeProxy)
        {
            string output = "0|0";
            string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
            try
            {
                RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
                if (uid != "")
                {
                    //if (currentUrl.Contains("facebook.com/checkpoint/") || currentUrl.Contains("facebook.com/nt/screen/") || currentUrl.Contains("facebook.com/x/checkpoint/"))
                    string html = request.RequestGet("https://m.facebook.com/me").ToString();
                    if (html.Contains("id=\"code_in_cliff\"") || html.Contains("name=\"new\"") || html.Contains("name=\"c\"") || html.Contains("changeemail"))
                        output = "1|0";
                    else if (Regex.Match(html, "\"USER_ID\":\"(.*?)\"").Groups[1].Value.Trim() == uid.Trim() && html.Contains(@"/friends/") && !html.Contains("checkpointSubmitButton") && !html.Contains("/checkpoint/dyi") && !html.Contains("checkpointBottomBar") && !html.Contains("captcha_response") && !html.Contains("https://www.facebook.com/communitystandards/") && !html.Contains("/help/203305893040179") && !html.Contains("FB:ACTION:OPEN_NT_SCREEN"))
                        output = "1|1";
                }
            }
            catch
            { }

            return output;
        }
        //public static List<string> GetMyListUidNameFriend(string cookie, string token, string userAgent, string proxy, int typeProxy)
        //{
        //    List<string> listFriend = new List<string>();
        //    try
        //    {
        //        RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
        //        string getListFriend = request.RequestGet("https://graph.facebook.com/me/friends?limit=5000&fields=id,name&access_token=" + token);

        //        JObject objFriend = JObject.Parse(getListFriend);

        //        if (objFriend["data"].Count() > 0)
        //        {
        //            for (int i = 0; i < objFriend["data"].Count(); i++)
        //            {
        //                string uidFr = objFriend["data"][i]["id"].ToString();
        //                string nameFr = objFriend["data"][i]["name"].ToString();
        //                listFriend.Add(uidFr + "|" + nameFr);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return listFriend;
        //}


        public static List<string> GetMyListUidNameFriend(string cookie, string token, string userAgent, string proxy, int typeProxy)
        {
            List<string> listFriend = new List<string>();
            try
            {
                string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
                RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
                request.request.AddHeader("Authorization", "OAuth " + token);
                //string getListFriend = request.RequestGet("https://graph.facebook.com/me/friends?pretty=0&limit=5000&fields=id,name&access_token=" + token);
                string getListFriend = request.RequestGet("https://graph.facebook.com/?ids=" + uid + "&fields=friends{id,name}");
                JObject objFriend = JObject.Parse(getListFriend);

                var temp = objFriend[uid]["friends"];
                if (temp["data"].Count() > 0)
                {
                    for (int i = 0; i < temp["data"].Count(); i++)
                    {
                        string uidFr = temp["data"][i]["id"].ToString();
                        //string nameFr = temp["data"][i]["name"].ToString();
                        //listFriend.Add(uidFr + "|" + nameFr);
                        listFriend.Add(uidFr);
                    }
                }
            }
            catch (Exception ex)
            {
            }

            return listFriend;
        }

        //public static List<string> GetListUidNameFriendOfUid(string cookie, string token, string uid, string userAgent, string proxy, int typeProxy)
        //{
        //    List<string> listFriend = new List<string>();
        //    try
        //    {
        //        RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
        //        string getListFriend = request.RequestGet("https://graph.facebook.com/" + uid + "/friends?limit=5000&fields=id,name&access_token=" + token);

        //        JObject objFriend = JObject.Parse(getListFriend);
        //        string uidFr = "", nameFr = "";
        //        if (objFriend["data"].Count() > 0)
        //        {
        //            for (int i = 0; i < objFriend["data"].Count(); i++)
        //            {
        //                uidFr = objFriend["data"][i]["id"].ToString();
        //                nameFr = objFriend["data"][i]["name"].ToString();
        //                listFriend.Add(uidFr + "|" + nameFr);
        //            }
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return listFriend;
        //}
        //public static List<string> BackupImageOne(string uidFr, string nameFr, string cookie, string token, string userAgent, string proxy, int typeProxy, int countImage = 20)
        //{
        //    List<string> listImageBackup = new List<string>();
        //    try
        //    {
        //        RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
        //        string htmlImage = request.RequestGet("https://graph.facebook.com/" + uidFr + "/photos?fields=images&limit=" + countImage + "&access_token=" + token);
        //        JObject objImg = JObject.Parse(htmlImage);
        //        int stt = 0;
        //        if (objImg != null && htmlImage.Contains("images"))
        //        {
        //            for (int j = 0; j < objImg["data"].Count(); j++)
        //            {
        //                stt = objImg["data"][j]["images"].ToList().Count - 1;
        //                listImageBackup.Add(uidFr + "*" + nameFr + "*" + objImg["data"][j]["images"][stt]["source"] + "|" + objImg["data"][j]["images"][stt]["width"] + "|" + objImg["data"][j]["images"][stt]["height"]);
        //            }
        //        }
        //    }
        //    catch { }

        //    return listImageBackup;
        //}
        public static List<string> BackupImageOne(string uids, string cookie, string token, string userAgent, string proxy, int typeProxy, int countImage = 20, bool isBackupNangCao = false)
        {
            List<string> listImageBackup = new List<string>();
            try
            {
                Dictionary<string, List<string>> dicImage = new Dictionary<string, List<string>>();
                {
                    var lstUid = uids.Split(',');
                    for (int i = 0; i < lstUid.Length; i++)
                        dicImage.Add(lstUid[i], new List<string>());
                }//khai báo dictionary


                {
                    RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
                    request.request.AddHeader("Authorization", "OAuth " + token);
                    string url = "https://graph.facebook.com/?ids=" + uids + "&pretty=0&fields=id,name,photos.limit(" + countImage + "){images}";
                    string htmlImage = request.RequestGet(url);
                    JObject objImg = JObject.Parse(htmlImage);

                    if (objImg != null && htmlImage.Contains("images"))
                    {
                        var lstUid = uids.Split(',');
                        for (int i = 0; i < lstUid.Length; i++)
                        {
                            string uidFr = lstUid[i];
                            string nameFr = objImg[uidFr]["name"].ToString();

                            try
                            {
                                foreach (var photos in objImg[uidFr]["photos"]["data"])
                                {
                                    try
                                    {
                                        int stt = photos["images"].ToList().Count - 1;
                                        dicImage[uidFr].Add(uidFr + "*" + nameFr + "*" + photos["images"][stt]["source"] + "|" + photos["images"][stt]["width"] + "|" + photos["images"][stt]["height"]);
                                    }
                                    catch
                                    {
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                }//Backup ảnh bình thường

                if (isBackupNangCao)
                {
                    RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
                    request.request.AddHeader("Authorization", "OAuth " + token);
                    string url = "https://graph.facebook.com/?ids=" + uids + "&pretty=0&fields=name,albums.limit(3){photos.limit(10){width,height,images}}";
                    string htmlImage = request.RequestGet(url);
                    JObject objImg = JObject.Parse(htmlImage);

                    if (objImg != null && htmlImage.Contains("images"))
                    {
                        var lstUid = uids.Split(',');
                        for (int i = 0; i < lstUid.Length; i++)
                        {
                            string uidFr = lstUid[i];
                            string nameFr = objImg[uidFr]["name"].ToString();

                            foreach (var albums in objImg[uidFr]["albums"]["data"])
                            {
                                try
                                {
                                    foreach (var photos in albums["photos"]["data"])
                                    {
                                        try
                                        {
                                            int stt = photos["images"].ToList().Count - 1;
                                            if (dicImage[uidFr].Count >= countImage)
                                                goto Continue;
                                            dicImage[uidFr].Add(uidFr + "*" + nameFr + "*" + photos["images"][stt]["source"] + "|" + photos["images"][stt]["width"] + "|" + photos["images"][stt]["height"]);
                                        }
                                        catch (Exception ex)
                                        {
                                        }
                                    }
                                }
                                catch (Exception ex)
                                {
                                }
                            }
                        Continue:
                            continue;
                        }
                    }
                }//Backup ảnh từ album

                {
                    foreach (var item in dicImage)
                    {
                        if (item.Value.Count > 0)
                        {
                            listImageBackup.AddRange(item.Value);
                            listImageBackup.Add("");
                        }
                    }
                }//Nhập danh sách link ảnh từ dicImage vào list
            }
            catch (Exception ex)
            {
            }

            return listImageBackup;
        }

        public static List<string> GetMyListComments(string cookie, string userAgent, string proxy, int typeProxy)
        {
            List<string> lstComment = new List<string>();
            try
            {
                RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
                string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
                string link_mau = "https://mbasic.facebook.com/{0}/allactivity/?category_key=commentscluster&timestart={1}&timeend={2}";

                string timeStart = "";
                string timeEnd = "";

                string link = "";
                string htmlActivity = "";

                MatchCollection matchCollection = null;

                List<string> lstLink = new List<string>();
                for (int i = 0; i < 5; i++)
                {
                    DateTime dateFrom = DateTime.Now.AddMonths(2 - i);
                    DateTime dateTo = DateTime.Now.AddMonths(1 - i);
                    timeStart = Common.ConvertDatetimeToTimestamp(new DateTime(dateFrom.Year, dateFrom.Month, 1)).ToString();
                    timeEnd = Common.ConvertDatetimeToTimestamp(new DateTime(dateTo.Year, dateTo.Month, 1)).ToString();
                    link = string.Format(link_mau, uid, timeStart, timeEnd);
                    lstLink.Add(link);
                }

                for (int k = 0; k < lstLink.Count; k++)
                {
                    link = lstLink[k];

                    bool isContinue = false;
                    do
                    {
                        isContinue = false;
                        //chrome.GotoURL(link);
                        htmlActivity = request.RequestGet(link);

                        htmlActivity = WebUtility.HtmlDecode(htmlActivity);
                        matchCollection = Regex.Matches(htmlActivity, "<span>(.*?)</h4>");
                        for (int i = 0; i < matchCollection.Count; i++)
                        {
                            string text = matchCollection[i].Groups[1].Value;
                            text = text.Substring(0, text.LastIndexOf('<'));
                            MatchCollection match = Regex.Matches(text, "<(.*?)>");
                            for (int j = 0; j < match.Count; j++)
                                text = text.Replace(match[j].Value, "");
                            if (text != "" && !lstComment.Contains(text))
                                lstComment.Add(text);
                        }

                        if (Regex.IsMatch(htmlActivity, $"/{uid}/allactivity/\\?category_key=commentscluster&timeend(.*?)\""))
                        {
                            link = "https://mbasic.facebook.com" + Regex.Match(htmlActivity, $"/{uid}/allactivity/\\?category_key=commentscluster&timeend(.*?)\"").Value.Replace("\"", "");
                            isContinue = true;
                        }
                    } while (isContinue);
                }
            }
            catch
            { }
            return lstComment;
        }
        //public static List<string> GetMyListComments(string cookie, string userAgent, string proxy, int typeProxy)
        //{
        //    List<string> lstComment = new List<string>();
        //    try
        //    {
        //        RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
        //        string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
        //        string htmlActivity = request.RequestGet($"https://mbasic.facebook.com/{uid}/allactivity/?category_key=commentscluster");
        //        string text = "";
        //        string linkReadCmt = "";
        //        MatchCollection matchCollection = null;
        //        do
        //        {
        //            htmlActivity = WebUtility.HtmlDecode(htmlActivity);
        //            matchCollection = Regex.Matches(htmlActivity, "<span>(.*?)</h4>");
        //            for (int i = 0; i < matchCollection.Count; i++)
        //            {
        //                text = matchCollection[i].Groups[1].Value;
        //                text = text.Substring(0, text.LastIndexOf('<'));
        //                MatchCollection match = Regex.Matches(text, "<(.*?)>");
        //                for (int j = 0; j < match.Count; j++)
        //                {
        //                    text = text.Replace(match[j].Value, "");
        //                }
        //                if (!lstComment.Contains(text))
        //                    lstComment.Add(text);
        //            }
        //            linkReadCmt = Regex.Match(htmlActivity, "/allactivity.category_key(.*?)more_\\d").Value;
        //            htmlActivity = request.RequestGet("http://mbasic.facebook.com/me" + linkReadCmt);
        //        } while (linkReadCmt != "");
        //    }
        //    catch
        //    { }
        //    return lstComment;
        //}
        public static List<string> GetMyListUidMessage(string cookie, string userAgent, string proxy, int typeProxy)
        {
            List<string> lstMessage = new List<string>();
            try
            {
                RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
                int moreAcc = 1;
                string htmlMessage = request.RequestGet("https://mbasic.facebook.com/messages/");

                string linkReadMes = "";
                string uid = "";
                do
                {
                    MatchCollection matchComments = Regex.Matches(htmlMessage, "#fua\">(.*?)<");
                    for (int c = 0; c < matchComments.Count; c++)
                    {
                        try
                        {
                            uid = matchComments[c].Groups[1].Value.Replace("\"", "");
                            uid = MCommon.Common.HtmlDecode(uid);
                            if (!lstMessage.Contains(uid))
                                lstMessage.Add(uid);
                        }
                        catch { }
                    }
                    linkReadMes = Regex.Match(htmlMessage, "/messages/.pageNum=(.*?)\"").Value.Replace("amp;", "");
                    htmlMessage = request.RequestGet("https://mbasic.facebook.com" + linkReadMes);
                    moreAcc++;
                    if (moreAcc >= 5)
                    {
                        break;
                    }
                } while (linkReadMes != "");
            }
            catch
            { }
            return lstMessage;
        }
        public static string GetMyBirthday(string cookie, string token, string userAgent, string proxy, int typeProxy)
        {
            string output = "";
            try
            {
                RequestXNet request = new RequestXNet(cookie, userAgent, proxy, typeProxy);
                string rq = request.RequestGet("https://graph.facebook.com/me?fields=id,name,birthday&access_token=" + token);
                JObject json = JObject.Parse(rq);
                return json["id"].ToString() + "|" + json["birthday"].ToString() + "|" + json["name"].ToString();
            }
            catch
            {
                if (!CheckLiveToken(cookie, token, userAgent, proxy, typeProxy))
                    output = "-1";
            }

            return output;
        }


        #region Get Fb_dtsg
        //public static string GetFbDtsg(string cookie, string useragent, string proxy, int typeProxy)
        //{
        //    try
        //    {
        //        string rq = new RequestXNet(cookie, "", "", 0).RequestGet("https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed");
        //        return Regex.Match(rq, MCommon.Common.Base64Decode("bmFtZT1cXCJmYl9kdHNnXFwiIHZhbHVlPVxcIiguKj8pXFwi")).Groups[1].Value;
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}
        public static string GetFbDtsg(string cookie, string useragent, string proxy, int typeProxy)
        {
            try
            {
                string rq = new RequestXNet(cookie, useragent, proxy, typeProxy).RequestGet("https://m.facebook.com/help/");
                return Regex.Match(rq, MCommon.Common.Base64Decode("ZHRzZyI6eyJ0b2tlbiI6IiguKj8pIg==")).Groups[1].Value;
            }
            catch
            {
                return "";
            }
        }
        #endregion


        #region Check avatar
        public static bool CheckAvatarFromUid(string uid, string token, string filePath = @"mau.jpg")
        {
            bool isHave = false;

            try
            {
                List<bool> iHash1 = GetHash(new Bitmap(filePath));
                List<bool> iHash2 = GetHash(GetImageFromUid(uid, token));

                //determine the number of equal pixel (x of 256)
                double equalElements = iHash1.Zip(iHash2, (i, j) => i == j).Count(eq => eq) / 256;
                isHave = equalElements == 0;
            }
            catch
            {
            }

            return isHave;
        }
        static Bitmap GetImageFromUid(string uid, string token)
        {
            RequestXNet request = new RequestXNet("", "", "", 0);

            string url = $"https://graph.facebook.com/{uid}/picture?access_token=" + token;
            byte[] image = request.GetBytes(url);

            MemoryStream mStream = new MemoryStream();
            mStream.Write(image, 0, Convert.ToInt32(image.Length));
            Bitmap bm = new Bitmap(mStream, false);
            mStream.Dispose();
            return bm;
        }
        public static bool DownLoadImageByUid(string uid, string token, string pathFolder)
        {
            try
            {
                string url = $"https://graph.facebook.com/{uid}/picture?width=736&access_token=" + token;

                using (WebClient webClient = new WebClient())
                {
                    byte[] data = webClient.DownloadData(url);

                    using (MemoryStream mem = new MemoryStream(data))
                    {
                        using (var yourImage = Image.FromStream(mem))
                        {
                            string linkfile = pathFolder + "\\" + uid;

                            // If you want it as Png
                            try
                            {
                                yourImage.Save(linkfile + ".png", ImageFormat.Png);
                            }
                            catch
                            {
                                yourImage.Save(linkfile + ".jpg", ImageFormat.Jpeg);
                            }
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "Error DownLoadImageByUid");
                return false;
            }
        }
        static List<bool> GetHash(Bitmap bmpSource)
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
        //public static bool CheckLiveWall(string token, string uid)
        //{
        //    RequestXNet request = new RequestXNet();

        //    try
        //    {
        //        request.RequestGet("https://graph.facebook.com/" + uid + "?access_token=" + token + "&fields=id");
        //        return true;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}


        /// <summary>
        /// 0-die, 1-live, 2-k check đc
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string CheckLiveWall(string uid)
        {
            RequestXNet request = new RequestXNet("", MCommon.SetupFolder.GetUseragentIPhone(Base.rd), "", 0);
            string rq = "";
            try
            {
                rq = request.RequestGet("https://graph.facebook.com/" + uid + "/picture?redirect=false");

                if (!string.IsNullOrEmpty(rq))
                {
                    if (rq.Contains("height") && rq.Contains("width"))
                        return "1|";
                    else
                        return "0|";
                }


                //string data = "fb_dtsg=&q=node(" + uid + "){name}";
                //rq = request.RequestPost("https://www.facebook.com/api/graphql", data);

                //if (!string.IsNullOrEmpty(rq))
                //{
                //    JObject json = JObject.Parse(rq);
                //    if (string.IsNullOrEmpty(json[uid].ToString()))
                //        return "0|";
                //    else if (json[uid]["name"] != null)
                //        return "1|" + json[uid]["name"].ToString();
                //}
            }
            catch (Exception ex)
            {
            }
            return "2|";
        }

        /// <summary>
        /// 1|username|name|gender|birthday|friends|groups|email_addresses
        /// </summary>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string CheckInfoUsingUid(string uid)
        {
            RequestHttp request = new RequestHttp("", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0", "");
            string rq = "";
            try
            {
                string friends = "", groups = "", name = "", gender = "", username = "", birthday = "", email_addresses = "";
                rq = request.RequestPost("https://www.facebook.com/api/graphql", "q=user(" + uid + "){friends{count},groups{count},id,name,gender,birthday,email_addresses,username}");
                if (!string.IsNullOrEmpty(rq))
                {
                    JObject json = JObject.Parse(rq);
                    if (string.IsNullOrEmpty(json[uid].ToString()))
                        return "0|";
                    else if (json[uid]["name"] != null)
                    {
                        if (json[uid]["friends"]["count"] != null)
                            friends = json[uid]["friends"]["count"].ToString();
                        if (json[uid]["groups"]["count"] != null)
                            groups = json[uid]["groups"]["count"].ToString();
                        if (json[uid]["name"] != null)
                            name = json[uid]["name"].ToString();
                        if (json[uid]["gender"] != null)
                            gender = json[uid]["gender"].ToString();
                        if (json[uid]["username"] != null)
                            username = json[uid]["username"].ToString();
                        if (json[uid]["birthday"] != null)
                            birthday = json[uid]["birthday"].ToString();
                        if (json[uid]["email_addresses"].ToString() != "[]")
                            email_addresses = json[uid]["email_addresses"].ToString();

                        return $"1|{username}|{name}|{gender}|{birthday}|{friends}|{groups}|{email_addresses}";
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return "2|";
        }


        public static bool CheckLiveToken(string cookie, string token, string useragent, string proxy, int typeProxy = 0)
        {
            bool isLive = false;


            try
            {
                RequestXNet request = new RequestXNet(cookie, useragent, proxy, typeProxy);
                string infor = request.RequestGet("https://graph.facebook.com/me?access_token=" + token);
                isLive = true;
            }
            catch
            {
            }

            return isLive;
        }
        public static string GetTokenEAAAAZ(string cookie, string useragent, string proxy, int typeProxy = 0)
        {
            string token = "";
            try
            {
                string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
                string fb_dtsg = GetFbDtsg(cookie, useragent, proxy, typeProxy);
                RequestXNet request = new RequestXNet(cookie, useragent, proxy, typeProxy);
                //string url = "https://m.facebook.com/composer/mbasic/?av=" + uid + "&eav=&refid=8&fb_dtsg=" + fb_dtsg + "&privacyx=291667064279714&target=" + uid + "&c_src=feed&cwevent=composer_entry&referrer=feed&ctype=inline&cver=amber&rst_icv=&view_overview=&xc_message=&__spin_r=&__spin_b=&__spin_t=";
                string url = "https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed";
                string rq = request.RequestGet(url);
                token = Regex.Match(rq, "EAAAAZ(.*?)\"").Value.Replace("\"", "").Replace(@"\", "");
            }
            catch
            {
                if (!CheckLiveCookie(cookie, useragent, proxy, typeProxy).StartsWith("1|"))
                    return "-1";
            }

            if (token == "")
                if (!CheckLiveCookie(cookie, useragent, proxy, typeProxy).StartsWith("1|"))
                    return "-1";

            return token;
        }
        public static string GetTokenEAAG(string cookie, string userAgent, string proxy, int typeProxy)
        {
            string token = "";
            try
            {
                RequestXNet request = new RequestXNet(cookie, "", proxy, typeProxy);
                string rq = request.RequestGet("https://business.facebook.com/business_locations/");
                token = Regex.Match(rq, "EAAG(.*?)\"").Value.Replace("\"", "").Replace(@"\", "");
            }
            catch
            {
                if (!CheckLiveCookie(cookie, userAgent, proxy, typeProxy).StartsWith("1|"))
                    return "-1";
            }
            if (token == "")
                if (!CheckLiveCookie(cookie, userAgent, proxy, typeProxy).StartsWith("1|"))
                    return "-1";

            return token;
        }

        public static string CheckCheckpoint(string idMethod)
        {
            string stt = "";
            int typeLag = 0;
            switch (idMethod)
            {
                case "3":
                    if (typeLag == 0)
                        stt = "Ảnh";
                    else
                        stt = "Image";
                    break;
                case "2":
                    if (typeLag == 0)
                        stt = "Ngày sinh";
                    else
                        stt = "Birthday";
                    break;
                case "20":
                    if (typeLag == 0)
                        stt = "Tin nhắn";
                    else
                        stt = "Message";
                    break;
                case "4":
                case "34":
                    stt = "Otp";
                    break;
                case "37":
                    stt = "Gửi OTP về mail";
                    break;
                case "35":
                    stt = "Login Google";
                    break;
                case "14":
                    if (typeLag == 0)
                        stt = "Thiết bị";
                    else
                        stt = "device";
                    break;
                case "26":
                    if (typeLag == 0)
                        stt = "Nhờ bạn bè";
                    else
                        stt = "Friend";
                    break;
                case "18":
                    if (typeLag == 0)
                        stt = "Bình luận";
                    else
                        stt = "comment";
                    break;
                case "72h":
                    if (typeLag == 0)
                        stt = "72h";
                    else
                        stt = "72 hours";
                    break;
                case "vhh":
                    if (typeLag == 0)
                        stt = "Vô hiệu hóa";
                    else
                        stt = "disable";
                    break;
                case "id_upload":
                    stt = "Up ảnh";
                    break;
                case "2fa":
                    stt = "Có 2fa";
                    break;
                default:
                    File.AppendAllText(@"data\dangcp.txt", idMethod);
                    break;
            }
            return stt;
        }

        /// <summary>
        /// 0-Không xác định, 1-live, 2-checkpoint, 3-sai pass, 4-sai email, 5-có 2fa
        /// </summary>
        /// <param name="email"></param>
        /// <param name="pass"></param>
        /// <returns>0-Không xác định, 1-live, 2-checkpoint, 3-sai pass, 4-sai email, 5-có 2fa</returns>
        public static string CheckFacebookAccount(string email, string pass, string userAgent, string proxy, int typeProxy)
        {
            string output = "";
            try
            {
                string str = "email=" + WebUtility.UrlEncode(email) + "&pass=" + WebUtility.UrlEncode(pass);

                RequestXNet request = new RequestXNet("", userAgent, proxy, typeProxy);
                string html = request.RequestPost("https://mbasic.facebook.com/login/device-based/regular/login/?refsrc=https%3A%2F%2Fmbasic.facebook.com%2F&lwv=100&refid=8", str).ToString();


                if (html.Contains("id=\"checkpointSubmitButton\""))
                {
                    if (html.Contains("id=\"approvals_code\""))
                    {
                        output = "5|";
                        //Gửi request nhập mã 2fa
                    }
                    else
                    {
                        output = "2|";

                        //Check dạng cp
                        request = new RequestXNet("", userAgent, proxy, typeProxy);
                        request.RequestGet("https://www.facebook.com").ToString();
                        html = request.RequestPost("https://www.facebook.com/login/device-based/regular/login/?login_attempt=1&lwv=100", str).ToString();

                        string fb_dtsg = Regex.Match(html, "name=\"fb_dtsg\" value=\"(.*?)\"").Groups[1].Value;
                        string jazoest = Regex.Match(html, "name=\"jazoest\" value=\"(.*?)\"").Groups[1].Value;
                        string nh = Regex.Match(html, "name=\"nh\" value=\"(.*?)\"").Groups[1].Value;
                        string __rev = Regex.Match(html, "\"__spin_r\":(.*?),").Groups[1].Value;
                        string __spin_t = Regex.Match(html, "\"__spin_t\":(.*?),").Groups[1].Value;

                        string data = "jazoest=" + jazoest + "&fb_dtsg=" + fb_dtsg + "&nh=" + nh + "&submit[Continue]=Ti%E1%BA%BFp%20t%E1%BB%A5c&__user=0&__a=1&__dyn=7xe6Fo4OQ1PyUhxOnFwn84a2i5U4e1Fx-ewSwMxW0DUeUhw5cx60Vo1upE4W0OE2WxO0SobEa87i0n2US1vw4Ugao881FU3rw&__csr=&__req=5&__beoa=0&__pc=PHASED%3ADEFAULT&dpr=1&__rev=" + __rev + "&__s=op5tkm%3A2d4a9m%3A37z92b&__hsi=6789153697588537525-0&__spin_r=" + __rev + "&__spin_b=trunk&__spin_t=" + __spin_t;
                        html = request.RequestPost("https://www.facebook.com/checkpoint/async?next=https%3A%2F%2Fwww.facebook.com%2F", data);
                        html = request.RequestGet("https://www.facebook.com/checkpoint/?next");

                        var coll = Regex.Matches(html, "verification_method\" value=\"(.*?)\"");
                        if (coll.Count > 0)
                        {
                            for (int i = 0; i < coll.Count; i++)
                            {
                                output += CheckCheckpoint(coll[i].Groups[1].Value) + "-";
                            }
                            output = output.TrimEnd('-');
                        }
                        else
                        {
                            if (html.Contains("/checkpoint/dyi/?referrer=disabled_checkpoint"))
                                output += CheckCheckpoint("vhh");
                            else if (html.Contains("captcha-recaptcha"))
                                output += CheckCheckpoint("72h");
                            else if (html.Contains("name=\"submit[Log Out]\"") || html.Contains("name=\"submit[__placeholder__]\""))
                                output += "không thể xmdt";
                            else if (html.Contains("name=\"submit[Continue]\""))
                                output += "Thiết bị";
                        }
                    }
                }
                else if (html.Contains("login_error"))
                {
                    if (html.Contains("m_login_email"))
                    {
                        output = "3|";
                    }
                    else
                    {
                        output = "0|";
                    }
                }
                else if (html.Contains("action_set_contact_point"))
                {
                    output = "2|" + CheckCheckpoint("34");
                }
                else
                {
                    string cookie = request.GetCookie();
                    if (CheckLiveCookie(cookie, userAgent, proxy, typeProxy).StartsWith("1|"))
                        output += "1|" + cookie;
                    else
                        output = "2|";
                }
            }
            catch
            {
                output = "0|";
            }

            return output;
        }

        /// <summary>
        /// Wall, tên, giới tính, ngày sinh, phone, email, bạn bè, nhóm
        /// </summary>
        /// <param name="tokenTrungGian"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetInfoAccountFromUidUsingToken(string tokenTrungGian, string uid, string useragent, string proxy, int typeProxy)
        {
            string result = "";
            bool isWallLive = false;
            string name = "", gender = "", birthday = "", phone = "", email = "", friends = "", groups = "";

            try
            {
                RequestXNet request = new RequestXNet("", useragent, proxy, typeProxy);

                if (uid == "")
                    uid = "me";
                string infor = request.RequestGet("https://graph.facebook.com/v2.11/" + uid + "?fields=name,email,gender,birthday,friends.limit(0)&access_token=" + tokenTrungGian);
                JObject objUser = JObject.Parse(infor);

                isWallLive = true;
                name = objUser["name"].ToString();
                try
                {
                    gender = objUser["gender"].ToString();
                }
                catch { }
                try
                {
                    birthday = objUser["birthday"].ToString();
                }
                catch { }
                try
                {
                    email = objUser["email"].ToString();
                }
                catch { }

                try
                {
                    friends = objUser["friends"]["summary"]["total_count"].ToString();
                }
                catch
                {
                }

                if (friends == "")
                    friends = "0";


                infor = request.RequestGet("https://graph.facebook.com/v2.11/" + uid + "/groups?fields=id&limit=5000&access_token=" + tokenTrungGian);
                objUser = JObject.Parse(infor);
                try
                {
                    groups = "" + objUser["data"].Count();
                }
                catch
                {
                }

                if (groups == "")
                    groups = "0";
            }
            catch
            {
                if (!CheckLiveToken("", tokenTrungGian, "", ""))
                    return "-1";
            }

            result = $"{isWallLive}|{name}|{gender}|{birthday}|{phone}|{email}|{friends}|{groups}";
            return result;
        }

        /// <summary>
        /// Wall, tên, giới tính, ngày sinh, phone, email, bạn bè, nhóm
        /// </summary>
        /// <param name="tokenTrungGian"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetInfoAccountFromUidUsingCookie(string cookie, string useragent, string proxy, int typeProxy)
        {
            string result = "";
            bool isWallLive = false;
            string name = "", gender = "", birthday = "", phone = "", email = "", friends = "", groups = "", token = "", dateCreate = "";

            try
            {
                string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;
                RequestXNet request = new RequestXNet(cookie, useragent, proxy, typeProxy);
                string rq = request.RequestGet("https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed");
                string fb_dtsg = Regex.Match(rq, MCommon.Common.Base64Decode("bmFtZT1cXCJmYl9kdHNnXFwiIHZhbHVlPVxcIiguKj8pXFwi")).Groups[1].Value;
                token = Regex.Match(rq, "EAAA(.*?)\"").Value.TrimEnd('"', '\\');
                name = Regex.Match(rq, MCommon.Common.Base64Decode("cHJvZnBpY1xcIiBhcmlhLWxhYmVsPVxcIiguKj8pLA==")).Groups[1].Value;
                name = WebUtility.HtmlDecode(name);

                string data = MCommon.Common.Base64Decode("LS0tLS0tV2ViS2l0Rm9ybUJvdW5kYXJ5MnlnMEV6RHBTWk9DWGdCUgpDb250ZW50LURpc3Bvc2l0aW9uOiBmb3JtLWRhdGE7IG5hbWU9ImZiX2R0c2ciCgp7e2ZiX2R0c2d9fQotLS0tLS1XZWJLaXRGb3JtQm91bmRhcnkyeWcwRXpEcFNaT0NYZ0JSCkNvbnRlbnQtRGlzcG9zaXRpb246IGZvcm0tZGF0YTsgbmFtZT0icSIKCm5vZGUoe3t1aWR9fSl7ZnJpZW5kc3tjb3VudH0sc3Vic2NyaWJlcnN7Y291bnR9LGdyb3VwcyxjcmVhdGVkX3RpbWV9Ci0tLS0tLVdlYktpdEZvcm1Cb3VuZGFyeTJ5ZzBFekRwU1pPQ1hnQlItLQ==");
                data = data.Replace("{{fb_dtsg}}", fb_dtsg);
                data = data.Replace("{{uid}}", uid);
                rq = request.RequestPost("https://www.facebook.com/api/graphql/", data, "multipart/form-data; boundary=----WebKitFormBoundary2yg0EzDpSZOCXgBR");

                //{"100041145020686":{"friends":{"count":333},"subscribers":{"count":646},"groups":{"count":57},"created_time":1568008638}}
                JObject json = JObject.Parse(rq);
                friends = json[uid]["friends"]["count"].ToString();
                groups = json[uid]["groups"]["count"].ToString();
                dateCreate = json[uid]["created_time"].ToString();

                if (friends == "")
                    friends = "0";
                if (groups == "")
                    groups = "0";
            }
            catch
            {
                if (!CheckLiveCookie(cookie, useragent, proxy, typeProxy).Contains("1|"))
                    return "-1";
            }

            result = $"{isWallLive}|{name}|{gender}|{birthday}|{phone}|{email}|{friends}|{groups}|{token}|{dateCreate}";
            return result;
        }

        /// <summary>
        /// Wall, tên, giới tính, ngày sinh, phone, email, bạn bè, nhóm
        /// </summary>
        /// <param name="tokenTrungGian"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetInfoAccountFromUidUsingCookieTrungGian(string uid, string cookie_ori)
        {
            string result = "";
            bool isWallLive = false;
            string name = "", gender = "", birthday = "", phone = "", email = "", friends = "", groups = "", token = "", dateCreate = "";

            try
            {
                string cookie = "c_user=" + Regex.Match(cookie_ori + ";", "c_user=(.*?);").Groups[1].Value + "; xs=xs=" + Regex.Match(cookie_ori + ";", "xs=(.*?);").Groups[1].Value + ";";
                RequestXNet request = new RequestXNet(cookie + " useragent=TW96aWxsYS81LjAgKFdpbmRvd3MgTlQgMTAuMDsgV2luNjQ7IHg2NCkgQXBwbGVXZWJLaXQvNTM3LjM2IChLSFRNTCwgbGlrZSBHZWNrbykgQ2hyb21lLzc0LjAuMjMwMi42MSBTYWZhcmkvNTM3LjM2", "Mozilla/5.0 (Windows NT 10.0; WOW64; rv:50.0) Gecko/20100101 Firefox/50.0", "", 0);
                string url = "https://www.facebook.com/api/graphql";
                string data = "q=user(" + uid + ")%7Bfriends%7Bcount%7D%2Cgroups%7Bcount%7D%2Cid%2Cname%2Cgender%2Cbirthday%2Cemail_addresses%2Cusername%7D";
                string rq = request.RequestPost(url, data);

                JObject json = JObject.Parse(rq);
                friends = json[uid]["friends"]["count"].ToString();
                groups = json[uid]["groups"]["count"].ToString();
                name = json[uid]["name"].ToString();
                birthday = ((JToken)json[uid]["birthday"] != null) ? json[uid]["birthday"].ToString() : "";
                gender = json[uid]["gender"].ToString().ToLower();

                if (friends == "")
                    friends = "0";
                if (groups == "")
                    groups = "0";
                isWallLive = true;
            }
            catch
            {
                if (!CheckLiveCookie(cookie_ori, "", "", 0).StartsWith("1|"))
                    return "-1";
                else
                    isWallLive = false;
            }

            result = $"{isWallLive}|{name}|{gender}|{birthday}|{phone}|{email}|{friends}|{groups}|{token}|{dateCreate}";
            return result;
        }

    }
}
