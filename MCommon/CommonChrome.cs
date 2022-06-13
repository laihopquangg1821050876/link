using maxcare;
using maxcare.Enum;
using Newtonsoft.Json.Linq;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MCommon
{
    public class CommonChrome
    {
        public static bool CheckFacebookBlocked(Chrome chrome)//Check fb block tính năng
        {
            if (chrome.GetURL().StartsWith("https://m.facebook.com/feature_limit_notice/") || chrome.CheckExistElements(0, "[href=\"https://www.facebook.com/help/177066345680802\"]", "[href*=\"https://m.facebook.com/help/contact/\"]") != 0)
                return true;
            return false;
        }
        public static void AnswerQuestionWhenJoinGroup(Chrome chrome, List<string> lstCauTraLoi)
        {
            List<string> lstTemp = new List<string>();
            string answer = "";
            int dem = chrome.CountElement("textarea");
            chrome.ScrollSmooth("document.querySelector('textarea')");
            chrome.DelayThaoTacNho();
            for (int j = 0; j < dem; j++)
            {
                if (lstTemp.Count == 0)
                    lstTemp = MCommon.Common.CloneList(lstCauTraLoi);
                answer = lstTemp[Base.rd.Next(0, lstTemp.Count)];
                answer = MCommon.Common.SpinText(answer, Base.rd);

                lstTemp.Remove(answer);

                chrome.SendKeys(4, "textarea", j, answer, 0.1);
                chrome.DelayThaoTacNho();
            }
        }
        public static bool IsCheckpointWhenLoginWhenGiaiCP(Chrome chrome)
        {
            try
            {
                return chrome.CheckExistElement("#captcha_response") == 1 || chrome.CheckExistElement("[name=\"captcha_response\"]") == 1 || chrome.CheckExistElement("[name=\"verification_method\"]") == 1 || chrome.CheckExistElement("[name=\"password_new\"]") == 1 || chrome.CheckExistElement("[href=\"https://www.facebook.com/communitystandards/\"]") == 1;
            }
            catch
            {
                return false;
            }
        }


        public static int LoginFacebookUsingUidPassWhenGiaiCP(Chrome chrome, string uid, string pass, string fa2 = "", string URL = "https://www.facebook.com")
        {
            Random rd = new Random();
            int result = 0;
            try
            {
                int typeWeb = 0;//1-www, 2-m, 0-ko co
                typeWeb = CheckTypeWebFacebookFromUrl(chrome.GetURL());

                if (typeWeb == 0)
                {
                    if (chrome.GotoURL(URL) == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                    typeWeb = CheckTypeWebFacebookFromUrl(chrome.GetURL());
                }

                //check save cookie
                if (chrome.CheckExistElement("[data-cookiebanner=\"accept_button\"]") == 1)
                {
                    chrome.Click(4, "[data-cookiebanner=\"accept_button\"]");
                    chrome.DelayTime(1);
                }

                if (typeWeb == 1)
                {
                    chrome.GotoURLIfNotExist("https://www.facebook.com/login");
                    //check save cookie
                    if (chrome.CheckExistElement("[data-cookiebanner=\"accept_button\"]") == 1)
                    {
                        chrome.Click(4, "[data-cookiebanner=\"accept_button\"]");
                        chrome.DelayTime(1);
                    }

                    if (chrome.SendKeys(1, "email", uid, 0.1) == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                    chrome.DelayTime(1);

                    if (chrome.SendKeys(1, "pass", pass, 0.1) == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                    chrome.DelayTime(1);

                    if (chrome.Click(1, "loginbutton") == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                    chrome.DelayTime(1);

                    if (chrome.CheckExistElement("#approvals_code", 5) == 1 && fa2 != "")
                    {
                        string input = Common.GetTotp(fa2.Replace(" ", "").Replace("\n", ""));
                        if (input != "")
                        {
                            chrome.SendKeys(1, "approvals_code", input, 0.1);
                            chrome.DelayTime(1);

                            chrome.Click(1, "checkpointSubmitButton");
                            chrome.DelayTime(1);
                        }
                    }

                    int stt = 0;
                    while (chrome.CheckExistElement("#checkpointSubmitButton", 3) == 1)
                    {

                        if (!chrome.CheckIsLive())
                        {
                            result = -2;
                            goto Xong;
                        }
                        if (IsCheckpointWhenLoginWhenGiaiCP(chrome) || stt == 7)
                            break;
                        chrome.Click(1, "checkpointSubmitButton");
                        chrome.DelayTime(1);
                        stt++;
                    }
                }
                else
                {
                    //m.fb
                    int check = chrome.GotoURLIfNotExist("https://m.facebook.com/login");
                    //check save cookie
                    if (chrome.CheckExistElement("[data-cookiebanner=\"accept_button\"]") == 1)
                    {
                        chrome.Click(4, "[data-cookiebanner=\"accept_button\"]");
                        chrome.DelayTime(1);
                    }

                    check = chrome.CheckExistElement("[data-sigil=\"login_profile_form\"] div[role=\"button\"]", 1);
                    switch (check)
                    {
                        case -2:
                            result = -2;
                            goto Xong;
                        case 1://Đã lưu tài khoản từ trước
                            chrome.DelayThaoTacNho();
                            if (chrome.Click(4, "[data-sigil=\"login_profile_form\"] div[role=\"button\"]") == -2)
                            {
                                result = -2;
                                goto Xong;
                            }

                            chrome.DelayThaoTacNho(2);
                            check = chrome.CheckExistElements(10, "[name=\"pass\"]", "#approvals_code");
                            if (check == -2)
                            {
                                result = -2;
                                goto Xong;
                            }

                            if (check == 1)
                            {
                                if (chrome.SendKeys(2, "pass", pass, 0.1) == 1)
                                {
                                    chrome.DelayThaoTacNho();
                                    if (chrome.Click(4, chrome.GetCssSelector("button", "data-sigil", "password_login_button")) == 1)
                                        chrome.DelayTime(1);
                                }
                            }
                            break;
                        default:
                            // login mới
                            //check = chrome.CheckExistElement("[data-sigil=\"touchable\"]");
                            //if (check == -2)
                            //{
                            //    result = -2;
                            //    goto Xong;
                            //}
                            //else if (check == 1)
                            //{
                            //    chrome.Click(4, "[data-sigil=\"touchable\"]");
                            //    chrome.DelayThaoTacNho();
                            //}

                            if (chrome.SendKeys(1, "m_login_email", uid, 0.1) == 1)
                            {
                                chrome.DelayThaoTacNho();

                                string element_pass = chrome.CheckExistElements(3, "#m_login_password", "[name=\"pass\"]") == 1 ? "#m_login_password" : "[name=\"pass\"]";
                                chrome.SendKeys(4, element_pass, pass, 0.1);
                                chrome.DelayThaoTacNho();

                                chrome.Click(2, "login");
                                chrome.DelayThaoTacNho();
                            }
                            break;
                    }

                    //if (Convert.ToBoolean(chrome.ExecuteScript("var check='false'; if(document.querySelector('#login_error')!=null) check=(document.querySelector('#login_error').innerText!='')+''; return check;")))
                    //    goto Xong;
                    check = chrome.CheckExistElement("#approvals_code", 5);
                    if (check == -2)
                    {
                        result = -2;
                        goto Xong;
                    }

                    if (check == 1 && fa2.Trim() != "")
                    {
                        string input = Common.GetTotp(fa2.Replace(" ", "").Replace("\n", ""));

                        if (input != "")
                        {
                            chrome.SendKeys(1, "approvals_code", input, 0.1);
                            chrome.DelayThaoTacNho(-1);

                            chrome.Click(1, "checkpointSubmitButton-actual-button");
                            chrome.DelayThaoTacNho();
                        }
                    }

                    int stt = 0;
                    while (chrome.CheckExistElement("#checkpointSubmitButton-actual-button", 3) == 1)
                    {
                        if (chrome.CheckExistElement("[name=\"password_new\"]") == 1)
                            break;
                        if (!chrome.CheckIsLive())
                        {
                            result = -2;
                            goto Xong;
                        }
                        if (IsCheckpointWhenLogin(chrome) || stt == 7)
                            break;
                        chrome.Click(1, "checkpointSubmitButton-actual-button");
                        chrome.DelayThaoTacNho();
                        stt++;
                    }
                }
                chrome.DelayTime(1);
                return CheckLiveCookie(chrome);
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(chrome, ex, "Login Uid Pass Fail");
            }
        Xong:
            return result;
        }

        public static string GetNameFromPost(Chrome chrome)
        {
            return chrome.ExecuteScript("var x='';document.querySelectorAll('[property=\"og:title\"]').length>0&&(x=document.querySelector('[property=\"og:title\"]').getAttribute('content')),''==x&&document.querySelectorAll('[data-gt] a').length>0&&(x=document.querySelector('[data-gt] a').innerText),''==x&&document.querySelectorAll('.actor').length>0&&(x=document.querySelector('.actor').innerText), x+''; return x;").ToString();
        }
        public static string GetNameFromStory(Chrome chrome)
        {
            return chrome.ExecuteScript("var x='';document.querySelectorAll('.overflowText').length>0&&(x=document.querySelector('.overflowText').innerText), x+''; return x;").ToString();
        }


        #region GoToFacebook
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToHome(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (!(chrome.GetURL() == "https://m.facebook.com/home.php" || chrome.GetURL() == "https://m.facebook.com"))
                    {
                        if (chrome.CheckExistElement("#feed_jewel a") == 1)
                        {
                            chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('#feed_jewel a')");
                            chrome.DelayThaoTacNho();
                        }
                        if (chrome.Click(4, "#feed_jewel a") != 1)
                            chrome.GotoURL("https://m.facebook.com/home.php");

                        chrome.DelayTime(1);
                        if (chrome.CheckExistElement("#nux-nav-button", 2) == 1)
                        {
                            chrome.ClickWithAction(1, "nux-nav-button");
                            chrome.DelayTime(2);
                        }
                    }

                    if (chrome.CheckChromeClosed()) return -2;
                    else if (chrome.GetURL() == "https://m.facebook.com/home.php" || chrome.GetURL() == "https://m.facebook.com/home.php?ref=wizard&_rdr" || chrome.GetURL() == "https://m.facebook.com") return 1;
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToFriend(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("#requests_jewel a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('#requests_jewel a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "#requests_jewel a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        chrome.DelayThaoTacNho(2);
                        check = chrome.Click(4, "#requests_jewel a");
                    }

                    if (check == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        if (chrome.Click(4, "[href=\"/friends/center/friends/?mff_nav=1\"]") == 1)
                        {
                            chrome.DelayThaoTacNho();
                            return 1;
                        }
                    }

                    return chrome.GotoURL("https://m.facebook.com/friends/center/friends/?mff_nav=1");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToFriendSuggest(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("#requests_jewel a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('#requests_jewel a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "#requests_jewel a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        chrome.DelayThaoTacNho(2);
                        check = chrome.Click(4, "#requests_jewel a");
                    }

                    if (check == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        if (chrome.Click(4, "[href=\"/friends/center/suggestions/?mff_nav=1&ref=bookmarks\"]") == 1)
                        {
                            chrome.DelayThaoTacNho();
                            return 1;
                        }
                    }

                    return chrome.GotoURL("https://m.facebook.com/friends/center/suggestions/?mff_nav=1&ref=bookmarks");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToGroup(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("[data-sigil=\"nav-popover bookmarks\"]>a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('[data-sigil=\"nav-popover bookmarks\"]>a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "[data-sigil=\"nav-popover bookmarks\"]>a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        chrome.DelayThaoTacNho(2);
                        check = chrome.Click(4, "[data-sigil=\"nav-popover bookmarks\"]>a");
                    }

                    if (check == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        string elGr = chrome.GetCssSelector("a", "href", "/groups/");
                        if (elGr != "")
                        {
                            chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('" + elGr + "')");
                            chrome.DelayThaoTacNho();

                            if (chrome.Click(4, elGr) == 1)
                            {
                                chrome.DelayThaoTacNho(2);
                                if (chrome.Click(4, "[href=\"/groups_browse/your_groups/\"]") == 1)
                                {
                                    chrome.DelayThaoTacNho();
                                    return 1;
                                }
                            }
                        }
                    }

                    return chrome.GotoURL("https://m.facebook.com/groups_browse/your_groups/");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToWatch(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("[data-sigil=\"nav-popover bookmarks\"]>a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('[data-sigil=\"nav-popover bookmarks\"]>a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "[data-sigil=\"nav-popover bookmarks\"]>a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        chrome.DelayThaoTacNho(2);
                        check = chrome.Click(4, "[data-sigil=\"nav-popover bookmarks\"]>a");
                    }

                    if (check == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        string elGr = chrome.GetCssSelector("a", "href", "/watch/");
                        if (elGr != "")
                        {
                            chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('" + elGr + "')");
                            chrome.DelayThaoTacNho();

                            if (chrome.Click(4, elGr) == 1)
                            {
                                chrome.DelayThaoTacNho();
                                return 1;
                            }
                        }
                    }

                    return chrome.GotoURL("https://m.facebook.com/watch/?ref=bookmarks");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToSetting(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("[data-sigil=\"nav-popover bookmarks\"]>a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('[data-sigil=\"nav-popover bookmarks\"]>a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "[data-sigil=\"nav-popover bookmarks\"]>a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        chrome.DelayThaoTacNho(2);
                        check = chrome.Click(4, "[data-sigil=\"nav-popover bookmarks\"]>a");
                    }

                    if (check == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        string elGr = chrome.GetCssSelector("a", "href", "/settings/");
                        if (elGr != "")
                        {
                            chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('" + elGr + "')");
                            chrome.DelayThaoTacNho();

                            if (chrome.Click(4, elGr) == 1)
                            {
                                chrome.DelayThaoTacNho();
                                return 1;
                            }
                        }
                    }

                    return chrome.GotoURL("https://m.facebook.com/settings/?entry_point=bookmark");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToSetting_TimelineAndTagging(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    GoToSetting(chrome);

                    string el = chrome.GetCssSelector("a", "href", "/privacy/touch/timeline_and_tagging/");
                    if (el != "")
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('" + el + "')");
                        chrome.DelayThaoTacNho();

                        if (chrome.Click(4, el) == 1)
                        {
                            chrome.DelayThaoTacNho();
                            return 1;
                        }
                    }

                    return chrome.GotoURL("https://m.facebook.com/privacy/touch/timeline_and_tagging/");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToNotifications(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("#notifications_jewel a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('#notifications_jewel a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "#notifications_jewel a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        check = chrome.Click(4, "#notifications_jewel a");
                    }

                    if (check == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        return 1;
                    }

                    return chrome.GotoURL("https://m.facebook.com/notifications.php?ref=bookmarks");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToMessages(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("#messages_jewel a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('#messages_jewel a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "#messages_jewel a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        chrome.DelayThaoTacNho(2);
                        check = chrome.Click(4, "#messages_jewel a");
                    }

                    if (check == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        return 1;
                    }

                    return chrome.GotoURL("https://m.facebook.com/messages/?entrypoint=jewel&no_hist=1&ref=bookmarks");
                }
            }
            catch
            {
            }

            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToMessagesUnread(Chrome chrome)
        {
            try
            {

                if (chrome != null)
                {
                    if (GoToMessages(chrome) == 1)
                    {
                        if (chrome.Click(4, "[href=\"/messages/?folder=unread&refid=11&ref=bookmarks\"]") == 1)
                        {
                            chrome.DelayRandom(3, 5);
                            return 1;
                        }
                    }

                    return chrome.GotoURL("https://m.facebook.com/messages/?folder=unread&ref=bookmarks");
                }
            }
            catch
            {
            }
            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToSearch(Chrome chrome)
        {
            try
            {

                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (chrome.CheckExistElement("#notifications_jewel a") == 1)
                    {
                        chrome.ScrollSmoothIfNotExistOnScreen("document.querySelector('#search_jewel a')");
                        chrome.DelayThaoTacNho();
                    }

                    int check = chrome.Click(4, "#search_jewel a");
                    if (check != 1)
                    {
                        GoToHome(chrome);
                        chrome.DelayThaoTacNho(2);
                        check = chrome.Click(4, "#search_jewel a");
                    }

                    if (check == 1 && chrome.CheckExistElement("#main-search-input") == 1)
                    {
                        chrome.DelayThaoTacNho(1);
                        return 1;
                    }
                }
            }
            catch
            {
            }
            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToSearchGroup(Chrome chrome, string tuKhoa, int tocDoGoVanBan = 0)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (GoToSearch(chrome) == 1)
                    {
                        bool isDone = false;
                        if (chrome.CheckExistElement("#main-search-input") == 1)
                        {
                            tuKhoa = MCommon.Common.SpinText(tuKhoa, Base.rd);

                            switch (3)
                            {
                                case 0://Chậm
                                    chrome.SendKeys(Base.rd, 1, "main-search-input", tuKhoa, 0.1);
                                    break;
                                case 1://Bình thường
                                    chrome.SendKeys(1, "main-search-input", tuKhoa, 0.1);
                                    break;
                                case 2://Nhanh
                                    chrome.SendKeys(1, "main-search-input", tuKhoa);
                                    break;
                                default:
                                    break;
                            }

                            chrome.DelayThaoTacNho();
                            chrome.SendEnter(1, "main-search-input");
                            chrome.DelayThaoTacNho(2);

                            string el = chrome.GetCssSelector("[data-sigil=\"mlayer-hide-on-click search-tabbar-tab\"]", "href", "/search/groups");
                            if (el != "")
                            {
                                if (chrome.Click(4, el) == 0)
                                {
                                    if (chrome.Click(4, "[data-sigil=\" flyout-causal\"]") == 1)
                                    {
                                        chrome.DelayThaoTacNho();
                                        el = chrome.GetCssSelector("[data-sigil=\"mlayer-hide-on-click search-tabbar-tab\"]", "href", "/search/groups");
                                        if (el != "")
                                        {
                                            isDone = true;
                                            chrome.Click(4, el);
                                            chrome.DelayThaoTacNho(2);
                                        }
                                    }
                                }
                                else
                                {
                                    isDone = true;
                                    chrome.DelayThaoTacNho(2);
                                }
                            }
                        }

                        if (!isDone)
                        {
                            chrome.GotoURL("https://m.facebook.com/search/groups/?q=" + tuKhoa);
                            chrome.DelayThaoTacNho(2);
                            isDone = true;
                        }

                        if (isDone)
                        {
                            chrome.DelayThaoTacNho(1);
                            return 1;
                        }
                    }
                }
            }
            catch
            {
            }
            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToSearchFriend(Chrome chrome, string tuKhoa, int tocDoGoVanBan = 0)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed()) return -2;

                    if (GoToSearch(chrome) == 1)
                    {
                        bool isDone = false;
                        if (chrome.CheckExistElement("#main-search-input") == 1)
                        {
                            tuKhoa = MCommon.Common.SpinText(tuKhoa, Base.rd);

                            switch (3)
                            {
                                case 0://Chậm
                                    chrome.SendKeys(Base.rd, 1, "main-search-input", tuKhoa, 0.1);
                                    break;
                                case 1://Bình thường
                                    chrome.SendKeys(1, "main-search-input", tuKhoa, 0.1);
                                    break;
                                case 2://Nhanh
                                    chrome.SendKeys(1, "main-search-input", tuKhoa);
                                    break;
                                default:
                                    break;
                            }


                            chrome.DelayThaoTacNho();
                            chrome.SendEnter(1, "main-search-input");
                            chrome.DelayThaoTacNho(2);

                            string el = chrome.GetCssSelector("[data-sigil=\"mlayer-hide-on-click search-tabbar-tab\"]", "href", "/search/people");
                            if (el != "")
                            {
                                if (chrome.Click(4, el) == 0)
                                {
                                    if (chrome.Click(4, "[data-sigil=\" flyout-causal\"]") == 1)
                                    {
                                        chrome.DelayThaoTacNho();
                                        el = chrome.GetCssSelector("[data-sigil=\"mlayer-hide-on-click search-tabbar-tab\"]", "href", "/search/people");
                                        if (el != "")
                                        {
                                            isDone = true;
                                            chrome.Click(4, el);
                                            chrome.DelayThaoTacNho(2);
                                        }
                                    }
                                }
                                else
                                {
                                    isDone = true;
                                    chrome.DelayThaoTacNho(2);
                                }
                            }
                        }

                        if (!isDone)
                        {
                            chrome.GotoURL("https://m.facebook.com/search/people/?q=" + tuKhoa + "&source=filter&isTrending=0");
                            chrome.DelayThaoTacNho(2);
                            isDone = true;
                        }

                        if (isDone)
                        {
                            chrome.DelayThaoTacNho(1);
                            return 1;
                        }
                    }
                }
            }
            catch
            {
            }
            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToBirthday(Chrome chrome)
        {
            try
            {

                if (chrome != null)
                {
                    if (chrome.GotoURL("https://m.facebook.com/browse/birthdays/") != -2)
                    {
                        chrome.DelayRandom(2, 5);
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }
            }
            catch
            {
            }
            return 0;
        }
        /// <summary>
        /// 0: error, -2: chrome closed, 1-success
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int GoToPoke(Chrome chrome)
        {
            try
            {

                if (chrome != null)
                {
                    if (chrome.GotoURL("https://m.facebook.com/pokes/") != -2)
                    {
                        chrome.DelayRandom(2, 5);
                        return 1;
                    }
                    else
                    {
                        return -2;
                    }
                }

            }
            catch
            {
            }
            return 0;
        }

        public static int ScrollRandom(Chrome chrome, int from = 3, int to = 5)
        {
            try
            {
                if (chrome.CheckChromeClosed()) return -2;
                int soLuot = maxcare.Base.rd.Next(from, to + 1);
                int start = Convert.ToInt32(chrome.ExecuteScript("return document.querySelector('html').getBoundingClientRect().y+''").ToString());
                if (chrome.ScrollSmooth(maxcare.Base.rd.Next(chrome.GetSizeChrome().Y / 2, chrome.GetSizeChrome().Y)) == 1)
                {
                    chrome.DelayRandom(1, 3);
                    int check = Convert.ToInt32(chrome.ExecuteScript("return document.querySelector('html').getBoundingClientRect().y+''").ToString());
                    if (start != check)
                    {
                        for (int j = 0; j < soLuot - 1; j++)
                        {
                            start = Convert.ToInt32(chrome.ExecuteScript("return document.querySelector('html').getBoundingClientRect().y+''").ToString());
                            if (chrome.ScrollSmooth((maxcare.Base.rd.Next(1, 1000) % 5 != 0 ? 1 : -1) * maxcare.Base.rd.Next(chrome.GetSizeChrome().Y / 2, chrome.GetSizeChrome().Y)) == -2)
                                return -2;
                            chrome.DelayRandom(1, 3);
                            check = Convert.ToInt32(chrome.ExecuteScript("return document.querySelector('html').getBoundingClientRect().y+''").ToString());
                            if (start != check)
                                chrome.DelayRandom(1, 2);
                            else
                                break;
                        }
                    }
                    return 1;
                }
            }
            catch
            {
            }
            return 0;
        }
        #endregion

        /// <summary>
        /// 0: nothing, -2: chrome closed, -3: disconnect internet, -1: checkpoint
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int CheckStatusChrome(Chrome chrome)
        {
            try
            {
                if (chrome != null)
                {
                    if (chrome.CheckChromeClosed())
                        return -2;
                    int check = chrome.CheckExistElement("[jsselect=\"suggestionsSummaryList\"]");
                    switch (check)
                    {
                        case 1:
                            return -3;
                        case -2:
                            return -2;
                        default:
                            break;
                    }
                    if (IsCheckpoint(chrome))
                        return -1;
                }
            }
            catch
            {
            }
            return 0;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="type">0-Giao diện cũ, 1-Giao diện mới</param>
        public static int ConvertInterfaceFacebook(Chrome chrome, int type)
        {
            int result = 0;
            int check = 0;
            try
            {
                string url = "https://www.facebook.com";
                if (!chrome.GetURL().StartsWith(url))
                {
                    check = chrome.GotoURL(url);
                    if (check == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                }

                object o = chrome.ExecuteScript("async function ConvertFacebook(e) { var t = require([\"DTSGInitData\"]).token, a = require([\"CurrentUserInitialData\"]).USER_ID, r = \"\", o = \"\"; 0 == e ? (r = \"https://www.facebook.com/api/graphql/\", o = \"av=\" + a + \"&__user=\" + a + \"&__a=1&dpr=1&fb_dtsg=\" + t + \"&fb_api_caller_class=RelayModern&fb_api_req_friendly_name=CometTrialParticipationChangeMutation&variables=%7B%22input%22%3A%7B%22change_type%22%3A%22OPT_OUT%22%2C%22source%22%3A%22SETTINGS_MENU%22%2C%22actor_id%22%3A%22\" + a + \"%22%2C%22client_mutation_id%22%3A%221%22%7D%7D&server_timestamps=true&doc_id=2317726921658975\") : (r = \"https://www.facebook.com/comet/try/\", o = \"source=SETTINGS_MENU&nctr[_mod]=pagelet_bluebar&__user=\" + a + \"&__a=1dpr=1&fb_dtsg=\" + t); var output = ''; try { var response = await fetch(r, { method: 'POST', body: o, headers: { 'Content-Type': 'application/x-www-form-urlencoded' } }); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; } var c = await ConvertFacebook(" + type + ");");
                if (o != null && o.ToString() == "-2")
                {
                    result = -2;
                    goto Xong;
                }

                if (chrome.Refresh() == -2)
                {
                    result = -2;
                    goto Xong;
                }

                check = chrome.CheckExistElement("[role=\"navigation\"]", 10);
                switch (check)
                {
                    case -2:
                        result = -2;
                        goto Xong;
                    case 1:
                        if ((type == 0 && chrome.CountElement("[role=\"navigation\"]") < 3) || (type == 1 && chrome.CountElement("[role=\"navigation\"]") == 3))
                            result = 1;
                        break;
                    default:
                        break;
                }
            }
            catch (Exception ex)
            {
            }
        Xong:
            return result;
        }
        public static string GetUserAgentDefault()
        {
            string useragent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/90.0.4430.212 Safari/537.36";
            try
            {
                JSON_Settings setting_general = new JSON_Settings("configGeneral");
                Chrome chrome = new Chrome()
                {
                    HideBrowser = true,
                    PathExtension = ""
                };

                if (setting_general.GetValueInt("typeBrowser") != 0)
                    chrome.LinkToOtherBrowser = setting_general.GetValue("txtLinkToOtherBrowser");

                if (chrome.Open())
                {
                    useragent = chrome.GetUseragent();
                    useragent = useragent.Replace("Headless", "");
                    chrome.Close();
                }
            }
            catch
            {
            }
            return useragent;
        }

        public static bool CheckInvalidChrome()
        {
            try
            {
                JSON_Settings setting_general = new JSON_Settings("configGeneral");
                Chrome chrome = new Chrome()
                {
                    HideBrowser = true,
                    PathExtension = ""
                };

                if (setting_general.GetValueInt("typeBrowser") != 0)
                    chrome.LinkToOtherBrowser = setting_general.GetValue("txtLinkToOtherBrowser");

                if (chrome.Open())
                    return true;

            }
            catch
            {
            }
            return false;
        }

        public static bool ConnectProxy(Chrome chrome, string username, string password)
        {
            bool isSuccess = false;
            try
            {
                chrome.GotoURL("chrome-extension://ggmdpepbjljkkkdaklfihhngmmgmpggp/options.html");
                chrome.SendKeys(1, "login", username);
                chrome.SendKeys(1, "password", password);
                chrome.ClearText(1, "retry");
                chrome.SendKeys(1, "retry", "2");
                chrome.Click(1, "save");
                chrome.DelayTime(2);

                chrome.GotoURL("http://lumtest.com/myip.json");
                string html = chrome.ExecuteScript("return document.documentElement.innerText;").ToString();
                string ip = JObject.Parse(html)["ip"].ToString();
                if (ip != "")
                    isSuccess = true;
            }
            catch
            {
            }
            return isSuccess;
        }


        /// <summary>
        /// 0-ko có, 1-www, 2-m, 3-mbasic
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static int CheckTypeWebFacebookFromUrl(string url)
        {
            int typeWeb = 0;
            if (url.StartsWith("https://www.facebook") || url.StartsWith("https://facebook") || url.StartsWith("https://web.facebook"))
                typeWeb = 1;
            else if (url.StartsWith("https://m.facebook") || url.StartsWith("https://d.facebook") || url.StartsWith("https://mobile.facebook"))
                typeWeb = 2;
            else if (url.StartsWith("https://mbasic.facebook"))
                typeWeb = 3;
            return typeWeb;
        }

        /// <summary>
        /// -2:chrome closed,0-ko co, 1-www, 2-m, 3-mbasic
        /// </summary>
        /// <param name="chrome"></param>
        /// <returns></returns>
        public static int CheckFacebookWebsite(Chrome chrome, string url)
        {
            if (!chrome.CheckIsLive())
                return -2;

            int typeWeb = 0;//0-ko co, 1-www, 2-m, 3-mbasic

            for (int i = 0; i < 2; i++)
            {
                if (chrome.GetURL().StartsWith("https://www.facebook") || chrome.GetURL().StartsWith("https://facebook") || chrome.GetURL().StartsWith("https://web.facebook"))
                    typeWeb = 1;
                else if (chrome.GetURL().StartsWith("https://m.facebook") || chrome.GetURL().StartsWith("https://mobile.facebook"))
                    typeWeb = 2;
                else if (chrome.GetURL().StartsWith("https://mbasic.facebook"))
                    typeWeb = 3;

                if (typeWeb != 0 && typeWeb == CheckTypeWebFacebookFromUrl(url))
                    break;
                else
                {
                    chrome.GotoURL(url);
                    chrome.DelayTime(1);
                }
            }

            return typeWeb;
        }
        public static List<string> GetListLinkFromWebsite(Chrome chrome)
        {
            List<string> lstLink = new List<string>();
            try
            {
                lstLink = chrome.ExecuteScript("var arr=[]; document.querySelectorAll('a').forEach(e=>{arr.push(e.href)});var s=arr.join('|'); return s").ToString().Split('|').ToList();
            }
            catch
            {
            }

            return lstLink;
        }
        public static string LoginInstagram(Chrome chrome, string cookie, string URL = "https://www.twitter.com")
        {
            try
            {
                if (CheckLoginSuccess(chrome))
                    return "1";

                if (!chrome.GetURL().StartsWith(URL))
                    chrome.GotoURL(URL);
                chrome.AddCookieIntoChrome(cookie, ".twitter.com");
                chrome.Refresh();

                if (CheckCheckpoint(chrome))
                    return "2";
                else if (CheckLoginSuccess(chrome))
                    return "1";
            }
            catch
            {
            }
            return "0";
        }
        public static string LoginTwitter(Chrome chrome, string user, string pass, string fa2 = "",string email = "",string phone = "", int timeOut = 120)
        {
            int result = 0;
            int check = 0;
            int timeStart = Environment.TickCount;
            try
            {
                string eUser = "[name=\"username\"]";
                string eUser2 = "[autocomplete=\"username\"]";
                string ePass = "[name=\"password\"]";
                string eLogin = "[data-testid=\"controlView\"] [role=\"button\"]";
                string eLogin2 = "[role=\"group\"]>div:nth-child(2) [role=\"button\"]:nth-child(6)";
                string e2Fa = "[name=\"text\"]";

                chrome.GotoURL("https://twitter.com/login");
                chrome.DelayThaoTacNho(3);
                //if (CheckCheckpoint(chrome))
                //{
                //    result = 2;
                //    goto Xong;
                //}

                if (chrome.GetURL() == "https://twitter.com/logout/error")
                {
                    chrome.Click(4, "[role=\"button\"]>[dir=\"auto\"]", 1);
                    chrome.DelayTime(3);
                    chrome.GotoURL("https://twitter.com/login");
                }

                int checkLuuTaiKhoan = chrome.CheckExistElements(10, "[data-sigil=\"login_profile_form\"] div[role=\"button\"]", "[action=\"/login/device-based/validate-pin/\"] input[type=\"submit\"]");
                if (checkLuuTaiKhoan > 0)//luu tai khoan tu truoc
                {
                    switch (checkLuuTaiKhoan)
                    {
                        case 1:
                            chrome.ExecuteScript("document.querySelector('[data-sigil=\"login_profile_form\"] div[role=\"button\"]').click()");
                            break;
                        case 2:
                            chrome.ExecuteScript("document.querySelector('[action=\"/login/device-based/validate-pin/\"] input[type=\"submit\"]').click()");
                            break;
                    }

                    chrome.DelayTime(1);
                    check = chrome.CheckExistElements(5, "[name=\"pass\"]", "#approvals_code");
                    if (check == 1)
                    {
                        if (chrome.SendKeys(2, "pass", pass, 0.1) == 1)
                        {
                            chrome.DelayTime(1);
                            if (chrome.Click(4, chrome.GetCssSelector("button", "data-sigil", "password_login_button")) == 1)
                                chrome.DelayTime(1);
                        }
                    }
                }
                else
                {
                    {
                        int step = 1;
                        bool isDone = false;
                        do
                        {
                            switch (step)
                            {
                                case 1:
                                    if (chrome.CheckExistElements(5,eUser2,eLogin2) == 0)
                                    {
                                        check = chrome.SendKeys(4, eUser, user, 0.1);
                                        chrome.DelayTime(1);
                                        check = chrome.Click(4, eLogin);
                                    }
                                    else
                                    {
                                        check = chrome.SendKeys(4, eUser2, user, 0.1);
                                        chrome.DelayTime(1);
                                        check = chrome.Click(4, eLogin2);
                                    }

                                    if (chrome.CheckExistElement("[inputmode=\"text\"]", 5) == 1)
                                    {
                                        check = chrome.SendKeys(4, "[inputmode=\"text\"]", email, 0.1);
                                        chrome.DelayTime(1);
                                        check = chrome.Click(4, "[data-testid=\"ocfEnterTextNextButton\"]",1);
                                    }
                                    
                                    break;
                                case 2:
                                    if (chrome.CheckExistElement(ePass,5) == 0)
                                    {
                                        isDone = true;
                                    }else
                                        check = chrome.SendKeys(4, ePass, pass, 0.1);

                                    

                                    break;
                                case 3:
                                    check = chrome.Click(4, eLogin);
                                    chrome.DelayTime(3);
                                    if (chrome.CheckExistElement("[action=\"/account/access\"]", 10) == 1)
                                    {
                                        chrome.ExecuteScript("document.querySelectorAll('[action=\"/account/access\"] [type=\"submit\"]')[0].click()");
                                    }
                                    chrome.DelayTime(3);

                                    if (chrome.CheckExistElement("[inputmode=\"tel\"]", 5) == 1)
                                    {
                                        check = chrome.SendKeys(4, "[inputmode=\"tel\"]", phone, 0.1);
                                        chrome.DelayTime(1);
                                        check = chrome.Click(4, "[data-testid=\"ocfEnterTextNextButton\"]", 1);
                                    }

                                    if (chrome.CheckExistElement("[inputmode=\"email\"]", 5) == 1)
                                    {
                                        check = chrome.SendKeys(4, "[inputmode=\"email\"]", email, 0.1);
                                        chrome.DelayTime(1);
                                        check = chrome.Click(4, "[data-testid=\"ocfEnterTextNextButton\"]");
                                    }
                                    isDone = true;
                                    break;
                                default:
                                    isDone = true;
                                    break;
                            }

                            if (check == -2)
                            {
                                result = -2;
                                goto Xong;
                            }

                            chrome.DelayTime(1);
                            step++;
                        } while (!isDone);
                    }//Nhập tk, mk
                }

                Dictionary<int, List<string>> dicCheck = new Dictionary<int, List<string>>()
                {
                    //{1,new List<string>(){ eLogin} },
                    {2,new List<string>(){ e2Fa } },
                    {3,new List<string>(){ "#checkpointSubmitButton", "#checkpointBottomBar" } },
                    {4,new List<string>(){ "#qf_skip_dialog_skip_link" } },
                    {5,new List<string>(){ "#nux-nav-button" } },
                    {6,new List<string>(){ "[name=\"n\"]" } },
                    {7,new List<string>(){ "[autocomplete=\"username\"]" } },
                    //{8,new List<string>(){ "[data-testid=\"inlinePrompt\"]" } },
                };
              
                int soLanNhap2Fa = 0;
                int soLanClickNutCheckpoint = 0;
                do
                {
                    if (Environment.TickCount - timeStart >= timeOut * 1000)
                        break;

                    check = chrome.CheckExistElements(0, dicCheck);
                    switch (check)
                    {
                        case 2://Bắt nhập 2fa
                            if (fa2 == "")
                            {
                                if (chrome.CheckExistElement("[autocomplete=\"username\"]",5)==1)
                                {
                                    result = 7;
                                }
                                else
                                {
                                    result = 3;
                                }
                                //k có 2fa
                                
                            }
                            else
                            {
                                soLanNhap2Fa++;
                                if (soLanNhap2Fa > 2)
                                {
                                    result = 6;
                                    goto Xong;
                                }

                                string input = Common.GetTotp(fa2.Replace(" ", "").Replace("\n", "").Trim());
                                if (string.IsNullOrEmpty(input))
                                {
                                    result = 6;
                                    goto Xong;
                                }

                                chrome.SendKeys(4, e2Fa, input, 0.1);
                                chrome.DelayTime(1);
                                chrome.SendEnter(4, e2Fa);
                                chrome.DelayTime(1);
                            }
                            break;
                        case 3:
                            if (soLanNhap2Fa > 1)
                                result = 6;
                            else
                            {
                                if (CheckCheckpoint(chrome))
                                {
                                    //checkpoint
                                    result = 2;
                                    goto Xong;
                                }

                                check = chrome.CheckExistElements(0, "button#checkpointSubmitButton", "#checkpointSubmitButton [type=\"submit\"]");
                                soLanClickNutCheckpoint++;
                                if (soLanClickNutCheckpoint > 10)
                                {//login fail
                                    goto Xong;
                                }

                                if (chrome.CheckExistElement("[value=\"dont_save\"]") == 1)
                                    chrome.Click(4, "[value=\"dont_save\"]");

                                if (check == 1)//www
                                    chrome.Click(4, "button#checkpointSubmitButton");
                                else//m, mbasic
                                    chrome.Click(4, "#checkpointSubmitButton [type=\"submit\"]");
                                chrome.DelayTime(1);
                            }
                            break;
                        case 4:
                            chrome.ClickWithAction(1, "qf_skip_dialog_skip_link");
                            chrome.DelayTime(2);
                            break;
                        case 5:
                            chrome.Click(1, "nux-nav-button");
                            chrome.DelayTime(2);
                            break;
                        case 6://sai pass
                            result = 5;
                            break;
                        case 7://sai username
                            result = 7;
                            break;
                        //case 8://sai username
                        //    result = 8;
                        //    break;
                        //case 7://nick bi han che tuong tac
                        //    chrome.Click(4, "[action=\"/account/access\"] [type=\"submit\"]");
                        //    chrome.DelayTime(3);
                        //    result = 7;
                        //    break;
                        default:
                            if (chrome.GetURL().StartsWith("https://twitter.com/login/error"))
                            {
                                string error = "";
                                if (chrome.CheckExistElement("[role=\"alert\"]") == 1)
                                {
                                    error = chrome.ExecuteScript("var out='';var x=document.querySelector('[role=\"alert\"]');if(x!=null) out=x.innerText;return out;").ToString();
                                    if (error != "")
                                        return error;
                                }
                            }

                            if (chrome.GetURL().StartsWith("https://twitter.com/account/access"))
                            {
                                if (chrome.CheckExistElements(0, "#recaptcha_element", "[href=\"https://www.google.com/policies/privacy/\"]") > 0)
                                {
                                    result = 2;
                                    goto Xong;
                                }
                            }


                            if (CheckLoginSuccess(chrome))
                                result = 1;
                            break;
                    }
                } while (result == 0);
            }
            catch (Exception ex)
            {
                Common.ExportError(chrome, ex, "Error Login Uid Pass");
            }
        Xong:
            return result + "";
        }
        public static string LoginInstagram(Chrome chrome, string user, string pass, string fa2, int timeOut = 120)
        {
            int result = 0;
            int check = 0;
            int timeStart = Environment.TickCount;
            int soLanNhap2fa = 0;
            try
            {
                chrome.GotoURL("https://twitter.com/home");
                chrome.DelayTime(5);
                for (int i = 0; i < 10; i++)
                {
                    if (!chrome.CheckIsLive())
                        break;

                    if (CheckLoginSuccess(chrome))
                    {
                        chrome.GotoURL("https://twitter.com/i/flow/login");
                        return "1";
                    }
                    else if (chrome.CheckExistElement("[name=\"username\"]",10) == 1)
                    {
                        chrome.SendKeys(2, "username", user);
                        chrome.DelayTime(1);
                        
                        //chrome.ExecuteScript("var x=document.querySelector('[type=\"submit\"]');if(x.hasAttribute('disabled')) x.removeAttribute('disabled')");
                        chrome.Click(4, "[data-testid=\"controlView\"] [role=\"button\"]");
                        chrome.DelayTime(1);

                        chrome.SendKeys(2, "password", pass);
                        chrome.DelayTime(1);

                        //chrome.ExecuteScript("var x=document.querySelector('[type=\"submit\"]');if(x.hasAttribute('disabled')) x.removeAttribute('disabled')");
                        chrome.Click(4, "[data-testid=\"controlView\"] [role=\"button\"]");
                        chrome.DelayTime(1);
                        break;
                    }
                    else if (chrome.CheckExistElement("button[type=\"button\"]") == 1)
                    {
                        chrome.Click(4, "button[type=\"button\"]");
                        break;
                    }
                    chrome.DelayTime(1);
                }

                do
                {
                    if (Environment.TickCount - timeStart >= timeOut * 1000)
                        break;

                    if (!chrome.CheckIsLive())
                        break;

                    check = chrome.CheckExistElements(0, "[name=\"username\"]", "[role=\"dialog\"]>div>div>div:nth-child(3)>button:nth-child(2)", "[name=\"verificationCode\"]");
                    switch (check)
                    {
                        case 1://sai thông tin login
                            string error = chrome.ExecuteScript("var out='';var x=document.querySelector('#slfErrorAlert');if(x!=null) out=x.innerText;return out;").ToString();
                            if (error != "")
                                return error;
                            break;
                        case 2:
                            chrome.Click(1, "[role=\"dialog\"]>div>div>div:nth-child(3)>button:nth-child(2)");
                            chrome.DelayTime(1);
                            break;
                        case 3:
                            //2fa
                            if (string.IsNullOrEmpty(fa2))
                            {
                                result = 3;
                            }
                            else
                            {
                                if (chrome.CheckExistElement("[data-visualcompletion=\"loading-state\"]") == 1)
                                    continue;

                                soLanNhap2fa++;
                                if (soLanNhap2fa > 2)
                                {
                                    result = 6;
                                    goto Xong;
                                }

                                //if (CheckExistText("dismiss", html))
                                //{
                                //    TapByText("dismiss", html);
                                //    html = GetHtml();
                                //}

                                string input = MCommon.Common.GetTotp(fa2);
                                if (string.IsNullOrEmpty(input))
                                {
                                    result = 6;
                                    goto Xong;
                                }

                                if (Convert.ToBoolean(chrome.ExecuteScript("return document.querySelector('[name=\"verificationCode\"]').value!=''")))
                                {
                                    chrome.SelectText(2, "verificationCode");
                                    chrome.SendBackspace(2, "verificationCode");
                                    chrome.DelayTime(1);
                                }
                                chrome.SendKeys(2, "verificationCode", input);
                                chrome.SendEnter(2, "verificationCode");
                            }
                            break;
                        default:
                            if (chrome.GetURL().StartsWith("https://www.instagram.com/accounts/onetap/"))
                                chrome.Click(4, "button[type=\"button\"]");
                            else if (CheckCheckpoint(chrome))
                                result = 2;
                            else if (CheckLoginSuccess(chrome))
                                result = 1;
                            break;
                    }
                } while (result == 0);
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(chrome, ex, "Error Login Uid Pass");
            }
        Xong:
            return result + "";
        }

        public static bool CheckLoginSuccess(Chrome chrome, string currentUrl = "", string html = "")
        {
            if (!chrome.GetURL().StartsWith("https://twitter.com"))
            {
                chrome.GotoURL("https://twitter.com/login");
                chrome.DelayTime(5);
            }

            if (currentUrl == "") currentUrl = chrome.GetURL();
            List<string> lstUrl = new List<string>()
            {
                "https://twitter.com/home"
            };

            if (CheckStringContainKeyword(currentUrl, lstUrl))
                return true;

            List<string> lstElements = new List<string>()
            {
                "[data-testid=\"SideNav_AccountSwitcher_Button\"]",
            };
            if (chrome.CheckExistElements(0, lstElements.ToArray()) == 1)
                return true;
            return false;
        }

        public static bool CheckCheckpoint(Chrome chrome)
        {
            if (chrome.GetURL().StartsWith("https://www.instagram.com/challenge"))
            {
                if (chrome.CheckExistElement("[name=\"choice\"][value=\"0\"]") == 1)
                {
                    chrome.Click(4, "[name=\"choice\"][value=\"0\"]");
                    if (chrome.GetURL().StartsWith("https://www.instagram.com/challenge"))
                        return true;
                }
                else
                {
                    return true;
                }
            }

            return chrome.CheckExistElements(0, "#recaptcha-input") > 0;
        }

        public static int LoginFacebookUsingUidPass(Chrome chrome, string uid, string pass, string fa2 = "", string URL = "https://www.facebook.com", int tocDoGoVanBan = 0)
        {
            Random rd = new Random();
            int result = 0;
            int checker = 0;
            try
            {
                int typeWeb = 0;//1-www, 2-m, 0-ko co
                if (chrome.GetURL().StartsWith("https://www.facebook") || chrome.GetURL().StartsWith("https://facebook") || chrome.GetURL().StartsWith("https://web.facebook"))
                    typeWeb = 1;
                else if (chrome.GetURL().StartsWith("https://m.facebook") || chrome.GetURL().StartsWith("https://mobile.facebook"))
                    typeWeb = 2;

                if (typeWeb == 0)
                {
                    if (chrome.GotoURL(URL) == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                    if (chrome.GetURL().StartsWith("https://www.facebook") || chrome.GetURL().StartsWith("https://facebook"))
                        typeWeb = 1;
                    else if (chrome.GetURL().StartsWith("https://m.facebook"))
                        typeWeb = 2;
                }

                //check save cookie
                if (chrome.CheckExistElement("[data-cookiebanner=\"accept_button\"]") == 1)
                {
                    chrome.Click(4, "[data-cookiebanner=\"accept_button\"]");
                    chrome.DelayTime(1);
                }

                if (typeWeb == 1)
                {
                    chrome.GotoURLIfNotExist("https://www.facebook.com/login");
                    //check save cookie
                    if (chrome.CheckExistElement("[data-cookiebanner=\"accept_button\"]") == 1)
                    {
                        chrome.Click(4, "[data-cookiebanner=\"accept_button\"]");
                        chrome.DelayTime(1);
                    }

                    chrome.DelayTime(1);

                    switch (3)
                    {
                        case 0://Chậm
                            checker = chrome.SendKeys(Base.rd, 1, "email", uid, 0.1);
                            break;
                        case 1://Bình thường
                            checker = chrome.SendKeys(1, "email", uid, 0.1);
                            break;
                        case 2://Nhanh
                            checker = chrome.SendKeys(1, "email", uid);
                            break;
                        default:
                            break;
                    }
                    if (checker == -2)
                    {
                        result = -2;
                        goto Xong;
                    }

                    chrome.DelayTime(1);

                    switch (3)
                    {
                        case 0://Chậm
                            checker = chrome.SendKeys(Base.rd, 1, "pass", pass, 0.1);
                            break;
                        case 1://Bình thường
                            checker = chrome.SendKeys(1, "pass", pass, 0.1);
                            break;
                        case 2://Nhanh
                            checker = chrome.SendKeys(1, "pass", pass);
                            break;
                        default:
                            break;
                    }

                    if (checker == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                    chrome.DelayTime(1);

                    if (chrome.Click(1, "loginbutton") == -2)
                    {
                        result = -2;
                        goto Xong;
                    }
                    chrome.DelayTime(1);

                    if (chrome.CheckExistElement("#approvals_code", 5) == 1 && fa2 != "")
                    {
                        string input = Common.GetTotp(fa2.Replace(" ", "").Replace("\n", ""));
                        if (input != "")
                        {
                            chrome.SendKeys(1, "approvals_code", input, 0.1);
                            chrome.DelayTime(1);

                            chrome.Click(1, "checkpointSubmitButton");
                            chrome.DelayTime(1);
                        }
                    }

                    int stt = 0;
                    while (chrome.CheckExistElement("#checkpointSubmitButton", 3) == 1)
                    {
                        if (!chrome.CheckIsLive())
                        {
                            result = -2;
                            goto Xong;
                        }
                        if (IsCheckpointWhenLogin(chrome) || stt == 7)
                            break;
                        chrome.Click(1, "checkpointSubmitButton");
                        chrome.DelayTime(1);
                        stt++;
                    }
                }
                else
                {
                    //m.fb
                    int check = chrome.GotoURLIfNotExist("https://m.facebook.com/login");
                    //check save cookie
                    if (chrome.CheckExistElement("[data-cookiebanner=\"accept_button\"]") == 1)
                    {
                        chrome.Click(4, "[data-cookiebanner=\"accept_button\"]");
                        chrome.DelayTime(1);
                    }

                    check = chrome.CheckExistElement("[data-sigil=\"login_profile_form\"] div[role=\"button\"]", 1);
                    switch (check)
                    {
                        case -2:
                            result = -2;
                            goto Xong;
                        case 1://Đã lưu tài khoản từ trước
                            chrome.DelayThaoTacNho();
                            if (chrome.Click(4, "[data-sigil=\"login_profile_form\"] div[role=\"button\"]") == -2)
                            {
                                result = -2;
                                goto Xong;
                            }

                            chrome.DelayThaoTacNho(2);
                            check = chrome.CheckExistElements(10, "[name=\"pass\"]", "#approvals_code");
                            if (check == -2)
                            {
                                result = -2;
                                goto Xong;
                            }

                            if (check == 1)
                            {
                                switch (3)
                                {
                                    case 0://Chậm
                                        checker = chrome.SendKeys(rd, 2, "pass", pass, 0.1);
                                        break;
                                    case 1://Bình thường
                                        checker = chrome.SendKeys(2, "pass", pass, 0.1);
                                        break;
                                    case 2://Nhanh
                                        checker = chrome.SendKeys(2, "pass", pass);
                                        break;
                                    default:
                                        break;
                                }

                                if (checker == 1)
                                {
                                    chrome.DelayThaoTacNho();
                                    if (chrome.Click(4, chrome.GetCssSelector("button", "data-sigil", "password_login_button")) == 1)
                                        chrome.DelayTime(1);
                                }

                            }
                            break;
                        default:
                            // login mới
                            //check = chrome.CheckExistElement("[data-sigil=\"touchable\"]");
                            //if (check == -2)
                            //{
                            //    result = -2;
                            //    goto Xong;
                            //}
                            //else if (check == 1)
                            //{
                            //    chrome.Click(4, "[data-sigil=\"touchable\"]");
                            //    chrome.DelayThaoTacNho();
                            //}

                            switch (3)
                            {
                                case 0://Chậm
                                    checker = chrome.SendKeys(rd, 1, "m_login_email", uid, 0.1);
                                    break;
                                case 1://Bình thường
                                    checker = chrome.SendKeys(1, "m_login_email", uid, 0.1);
                                    break;
                                case 2://Nhanh
                                    checker = chrome.SendKeys(1, "m_login_email", uid);
                                    break;
                                default:
                                    break;
                            }

                            if (checker == 1)
                            {
                                chrome.DelayThaoTacNho();

                                string element_pass = chrome.CheckExistElements(3, "#m_login_password", "[name=\"pass\"]") == 1 ? "#m_login_password" : "[name=\"pass\"]";
                                switch (3)
                                {
                                    case 0://Chậm
                                        chrome.SendKeys(rd, 4, element_pass, pass, 0.1);
                                        break;
                                    case 1://Bình thường
                                        chrome.SendKeys(4, element_pass, pass, 0.1);
                                        break;
                                    case 2://Nhanh
                                        chrome.SendKeys(4, element_pass, pass);
                                        break;
                                    default:
                                        break;
                                }

                                chrome.DelayThaoTacNho();

                                chrome.Click(2, "login");
                                chrome.DelayThaoTacNho();
                            }
                            break;
                    }

                    //if (Convert.ToBoolean(chrome.ExecuteScript("var check='false'; if(document.querySelector('#login_error')!=null) check=(document.querySelector('#login_error').innerText!='')+''; return check;")))
                    //    goto Xong;

                    int stt = 0;
                    while (chrome.CheckExistElement("#checkpointSubmitButton-actual-button", 3) == 1)
                    {
                        check = chrome.CheckExistElements(2, "#approvals_code", "[name=\"approvals_code\"]");
                        if (check != 0)
                        {
                            string el = (check == 1) ? "#approvals_code" : "[name=\"approvals_code\"]";
                            if (fa2.Trim() != "")
                            {
                                string input = "";

                                for (int i = 0; i < 10; i++)
                                {
                                    input = Common.GetTotp(fa2);
                                    if (input != "")
                                        break;
                                    MCommon.Common.DelayTime(1);
                                }

                                if (input != "")
                                {
                                    chrome.SendKeys(4, el, input, 0.1);
                                    chrome.DelayThaoTacNho(-1);

                                    chrome.Click(1, "checkpointSubmitButton-actual-button");
                                    chrome.DelayThaoTacNho();
                                }
                                else
                                {
                                    MCommon.Common.ExportError(null, "Khong Lay Duoc 2FA: " + fa2);
                                }
                                stt = 0;
                            }
                        }

                        if (!chrome.CheckIsLive())
                        {
                            result = -2;
                            goto Xong;
                        }
                        if (IsCheckpointWhenLogin(chrome) || stt == 10)
                            break;
                        chrome.Click(1, "checkpointSubmitButton-actual-button");
                        chrome.DelayThaoTacNho();
                        stt++;
                    }
                }
                chrome.DelayTime(1);

                return CheckLiveCookie(chrome);
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(chrome, ex, "Login Uid Pass Fail");
            }
        Xong:
            return result;
        }

        //public static int LoginFacebookUsingUidPass(Chrome chrome, string uid, string pass, string fa2 = "", string URL = "https://www.facebook.com")
        //{
        //    int result = 0;
        //    try
        //    {
        //        int typeWeb = 0;//1-www, 2-m, 0-ko co
        //        if (chrome.GetURL().StartsWith("https://www.facebook") || chrome.GetURL().StartsWith("https://facebook"))
        //            typeWeb = 1;
        //        else if (chrome.GetURL().StartsWith("https://m.facebook"))
        //            typeWeb = 2;

        //        if (typeWeb == 0)
        //        {
        //            if (chrome.GotoURL(URL) == -2)
        //            {
        //                result = -2;
        //                goto Xong;
        //            }
        //            if (chrome.GetURL().StartsWith("https://www.facebook") || chrome.GetURL().StartsWith("https://facebook"))
        //                typeWeb = 1;
        //            else if (chrome.GetURL().StartsWith("https://m.facebook"))
        //                typeWeb = 2;
        //        }

        //        if (typeWeb == 1)
        //        {
        //            chrome.GotoURLIfNotExist("https://www.facebook.com/login");

        //            if (chrome.SendKeys(1, "email", uid, 0.1) == -2)
        //            {
        //                result = -2;
        //                goto Xong;
        //            }
        //            chrome.DelayTime(1);

        //            if (chrome.SendKeys(1, "pass", pass, 0.1) == -2)
        //            {
        //                result = -2;
        //                goto Xong;
        //            }
        //            chrome.DelayTime(1);

        //            if (chrome.Click(1, "loginbutton") == -2)
        //            {
        //                result = -2;
        //                goto Xong;
        //            }
        //            chrome.DelayTime(1);

        //            if (chrome.CheckExistElement("#approvals_code", 5) == 1 && fa2 != "")
        //            {
        //                string input = Common.GetTotp(fa2.Replace(" ", "").Replace("\n", ""));
        //                if (input != "")
        //                {
        //                    chrome.SendKeys(1, "approvals_code", input, 0.1);
        //                    chrome.DelayTime(1);

        //                    chrome.Click(1, "checkpointSubmitButton");
        //                    chrome.DelayTime(1);

        //                    //Check xem otp có đúng ko?
        //                    int check = chrome.CheckExistElement("#approvals_code",3);
        //                    if (check == -2)
        //                    {
        //                        result = -2;
        //                        goto Xong;
        //                    }
        //                    else if (check == 1)
        //                    {
        //                        if (chrome.Click(1, "checkpointSubmitButton") == -2)
        //                        {
        //                            result = -2;
        //                            goto Xong;
        //                        }
        //                        chrome.DelayTime(1);

        //                        int stt = 0;
        //                        while (chrome.CheckExistElement("#checkpointSubmitButton", 3) == 1)
        //                        {
        //                            if (!chrome.CheckIsLive())
        //                            {
        //                                result = -2;
        //                                goto Xong;
        //                            }
        //                            if (IsCheckpointWhenLogin(chrome) || stt == 7)
        //                                break;
        //                            chrome.Click(1, "checkpointSubmitButton");
        //                            chrome.DelayTime(1);
        //                            stt++;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            //m.fb
        //            int check = chrome.GotoURLIfNotExist("https://m.facebook.com/login");


        //            check = chrome.CheckExistElement("[data-sigil=\"login_profile_form\"] div[role=\"button\"]", 1);
        //            switch (check)
        //            {
        //                case -2:
        //                    result = -2;
        //                    goto Xong;
        //                case 1://Đã lưu tài khoản từ trước
        //                    chrome.DelayTime(1);
        //                    if (chrome.Click(4, "[data-sigil=\"login_profile_form\"] div[role=\"button\"]") == -2)
        //                    {
        //                        result = -2;
        //                        goto Xong;
        //                    }

        //                    chrome.DelayTime(3);
        //                    check = chrome.CheckExistElements(10, "[name=\"pass\"]", "#approvals_code");
        //                    if (check == -2)
        //                    {
        //                        result = -2;
        //                        goto Xong;
        //                    }

        //                    if (check == 1)
        //                    {
        //                        if (chrome.SendKeys(2, "pass", pass, 0.1) == 1)
        //                        {
        //                            chrome.DelayTime(1);
        //                            if (chrome.Click(4, chrome.GetCssSelector("button", "data-sigil", "password_login_button")) == 1)
        //                                chrome.DelayTime(1);
        //                        }
        //                    }
        //                    break;
        //                default:
        //                    // login mới
        //                    check = chrome.CheckExistElement("[data-sigil=\"touchable\"]");
        //                    if (check == -2)
        //                    {
        //                        result = -2;
        //                        goto Xong;
        //                    }
        //                    else if (check == 1)
        //                    {
        //                        chrome.Click(4, "[data-sigil=\"touchable\"]");
        //                        chrome.DelayTime(1);
        //                    }

        //                    if (chrome.SendKeys(1, "m_login_email", uid, 0.1) == 1)
        //                    {
        //                        chrome.DelayTime(1);

        //                        string element_pass = chrome.CheckExistElements(3, "#m_login_password", "[name=\"pass\"]") == 1 ? "#m_login_password" : "[name=\"pass\"]";
        //                        chrome.SendKeys(4, element_pass, pass, 0.1);
        //                        chrome.DelayTime(1);

        //                        chrome.Click(2, "login");
        //                        chrome.DelayTime(1);
        //                    }
        //                    break;
        //            }

        //            //if (Convert.ToBoolean(chrome.ExecuteScript("var check='false'; if(document.querySelector('#login_error')!=null) check=(document.querySelector('#login_error').innerText!='')+''; return check;")))
        //            //    goto Xong;
        //            check = chrome.CheckExistElement("#approvals_code",5);
        //            if (check == -2)
        //            {
        //                result = -2;
        //                goto Xong;
        //            }

        //            if (check == 1 && fa2.Trim() != "")
        //            {
        //                string input = Common.GetTotp(fa2.Replace(" ", "").Replace("\n", ""));

        //                if (input != "")
        //                {
        //                    chrome.SendKeys(1, "approvals_code", input, 0.1);
        //                    chrome.DelayTime(1);

        //                    chrome.Click(1, "checkpointSubmitButton-actual-button");
        //                    chrome.DelayTime(1);
        //                }
        //            }

        //            int stt = 0;
        //            while (chrome.CheckExistElement("#checkpointSubmitButton-actual-button", 3) == 1)
        //            {
        //                if (!chrome.CheckIsLive())
        //                {
        //                    result = -2;
        //                    goto Xong;
        //                }
        //                if (IsCheckpointWhenLogin(chrome) || stt == 7)
        //                    break;
        //                chrome.Click(1, "checkpointSubmitButton-actual-button");
        //                chrome.DelayTime(1);
        //                stt++;
        //            }
        //        }
        //        chrome.DelayTime(1);
        //        return CheckLiveCookie(chrome);
        //    }
        //    catch (Exception ex)
        //    {
        //        MCommon.Common.ExportError(chrome, ex, "Login Uid Pass Fail");
        //    }
        //Xong:
        //    return result;
        //}

        /// <summary>
        /// Wall, tên, giới tính, ngày sinh, phone, email, bạn bè, nhóm, follow
        /// </summary>
        public static string GetInfoAccountFromUidUsingCookie(Chrome chrome)
        {
            string result = "";
            bool isWallLive = false;
            string name = "", gender = "", birthday = "", phone = "", email = "", friends = "", groups = "", token = "", dateCreate = "", follow = "";

            try
            {
                string cookie = chrome.GetCookieFromChrome();
                string uid = Regex.Match(cookie + ";", "c_user=(.*?);").Groups[1].Value;

                string rq = RequestGet(chrome, "https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed", "https://m.facebook.com/");
                string fb_dtsg = Regex.Match(rq, MCommon.Common.Base64Decode("bmFtZT1cXCJmYl9kdHNnXFwiIHZhbHVlPVxcIiguKj8pXFwi")).Groups[1].Value;
                token = Regex.Match(rq, "EAAA(.*?)\"").Value.TrimEnd('"', '\\');
                name = Regex.Match(rq, MCommon.Common.Base64Decode("cHJvZnBpY1xcIiBhcmlhLWxhYmVsPVxcIiguKj8pLA==")).Groups[1].Value;
                name = WebUtility.HtmlDecode(name);

                string data = MCommon.Common.Base64Decode("LS0tLS0tV2ViS2l0Rm9ybUJvdW5kYXJ5MnlnMEV6RHBTWk9DWGdCUgpDb250ZW50LURpc3Bvc2l0aW9uOiBmb3JtLWRhdGE7IG5hbWU9ImZiX2R0c2ciCgp7e2ZiX2R0c2d9fQotLS0tLS1XZWJLaXRGb3JtQm91bmRhcnkyeWcwRXpEcFNaT0NYZ0JSCkNvbnRlbnQtRGlzcG9zaXRpb246IGZvcm0tZGF0YTsgbmFtZT0icSIKCm5vZGUoe3t1aWR9fSl7ZnJpZW5kc3tjb3VudH0sc3Vic2NyaWJlcnN7Y291bnR9LGdyb3Vwc3tjb3VudH0sY3JlYXRlZF90aW1lfQotLS0tLS1XZWJLaXRGb3JtQm91bmRhcnkyeWcwRXpEcFNaT0NYZ0JSLS0=");
                data = data.Replace("{{fb_dtsg}}", fb_dtsg);
                data = data.Replace("{{uid}}", uid);
                rq = RequestPost(chrome, "https://www.facebook.com/api/graphql/", data, "https://www.facebook.com/api/graphql/", "multipart/form-data; boundary=----WebKitFormBoundary2yg0EzDpSZOCXgBR");

                string infoAcc = GetInfoAccountFromUidUsingToken(chrome, token);
                string[] temp = infoAcc.Split('|');
                gender = temp[2];
                birthday = temp[3];
                email = temp[5];

                JObject json = JObject.Parse(rq);
                try
                {
                    follow = json[uid]["subscribers"]["count"].ToString();
                }
                catch
                {
                }

                try
                {
                    friends = json[uid]["friends"]["count"].ToString();
                }
                catch
                {
                }

                try
                {
                    groups = json[uid]["groups"]["count"].ToString();
                }
                catch
                {
                }

                try
                {
                    dateCreate = json[uid]["created_time"].ToString();
                    if (!dateCreate.Contains("/"))
                        dateCreate = MCommon.Common.ConvertTimeStampToDateTime(Convert.ToDouble(dateCreate)).ToString("dd/MM/yyyy HH:mm:ss");
                }
                catch
                {
                }

                if (follow == "")
                    follow = "0";
                if (friends == "")
                    friends = "0";
                if (groups == "")
                    groups = "0";
            }
            catch
            {
                if (CheckLiveCookie(chrome, "https://m.facebook.com/") == 0)
                    return "-1";
            }

            result = $"{isWallLive}|{name}|{gender}|{birthday}|{phone}|{email}|{friends}|{groups}|{token}|{dateCreate}|{follow}";
            return result;
        }
        /// <summary>
        /// Wall, tên, giới tính, ngày sinh, phone, email, bạn bè, nhóm
        /// </summary>
        /// <param name="tokenTrungGian"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        public static string GetInfoAccountFromUidUsingToken(Chrome chrome, string token)
        {
            string result = "";
            bool isWallLive = false;
            string name = "", gender = "", birthday = "", phone = "", email = "", friends = "", groups = "";

            try
            {
                string infor = RequestGet(chrome, "https://graph.facebook.com/v2.11/me?fields=name,email,gender,birthday&access_token=" + token, "https://graph.facebook.com/");
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
            }
            catch
            {
            }

            result = $"{isWallLive}|{name}|{gender}|{birthday}|{phone}|{email}|{friends}|{groups}";
            return result;
        }
        public static string GetTokenEAAAAZ(Chrome chrome)
        {
            string token = "";
            try
            {
                string rq = RequestGet(chrome, "https://m.facebook.com/composer/ocelot/async_loader/?publisher=feed", "https://m.facebook.com");
                token = Regex.Match(rq, "EAAAAZ(.*?)\"").Value.Replace("\"", "").Replace(@"\", "");
            }
            catch
            {
            }
            return token;
        }

        /// <summary>
        /// -2: chrome closed, -3: no internet, 0-cookie die, 1: cookie live, 2: checkpoint
        /// </summary>
        /// <param name="chrome"></param>
        /// <param name="URL"></param>
        /// <returns></returns>
        public static int CheckLiveCookie(Chrome chrome, string url = "https://m.facebook.com")
        {
            try
            {
                if (chrome.CheckChromeClosed())
                    return -2;

                if (CheckTypeWebFacebookFromUrl(chrome.GetURL()) == 0)
                    chrome.GotoURL(url);

                string currentUrl = chrome.GetURL();
                if (currentUrl.Contains("facebook.com/checkpoint/") || currentUrl.Contains("facebook.com/nt/screen/?params=%7B%22token") || currentUrl.Contains("facebook.com/x/checkpoint/"))
                    return 2;

                int typeWeb = CheckFacebookWebsite(chrome, url);

                if (typeWeb == 2)
                {
                    if (chrome.GetURL().StartsWith("https://m.facebook.com/zero/policy/optin"))
                    {
                        chrome.DelayTime(1);
                        chrome.ExecuteScript("document.querySelector('a[data-sigil=\"touchable\"]').click()");
                        chrome.DelayTime(3);
                        if (chrome.CheckExistElement("button[data-sigil=\"touchable\"]", 10) == 1)
                        {
                            chrome.DelayTime(1);
                            chrome.ExecuteScript("document.querySelector('button[data-sigil=\"touchable\"]').click()");
                            chrome.DelayTime(3);
                        }
                    }

                    if (Convert.ToBoolean(chrome.ExecuteScript("var check='false';var x=document.querySelectorAll('a');for(i=0;i<x.length;i++){if(x[i].href.includes('legal_consent/basic/?consent_step=1')){x[i].click();break;check='true'}} return check")))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            MCommon.Common.DelayTime(2);
                            if (!Convert.ToBoolean(chrome.ExecuteScript("var check='false';var x=document.querySelectorAll('a');for(i=0;i<x.length;i++){if(x[i].href.includes('legal_consent/basic/?consent_step=1')){x[i].click();break;check='true'}} return check")))
                                break;
                        }
                        for (int i = 0; i < 5; i++)
                        {
                            MCommon.Common.DelayTime(2);
                            if (!Convert.ToBoolean(chrome.ExecuteScript("var check='false';var x=document.querySelectorAll('a');for(i=0;i<x.length;i++){if(x[i].href.includes('consent/basic/log')){x[i].click();break;check='true'}} return check")))
                                break;
                        }
                        if (chrome.CheckExistElement("[href=\"/home.php\"]") == 1)
                            chrome.Click(4, "[href=\"/home.php\"]");
                    }

                    //
                    if (chrome.GetURL().StartsWith("https://m.facebook.com/legal_consent"))
                    {
                        chrome.ExecuteScript("document.querySelector('button').click()");
                        chrome.DelayTime(1);
                        chrome.ExecuteScript("document.querySelectorAll('button')[1].click()");
                        chrome.DelayTime(1);
                        chrome.ExecuteScript("document.querySelector('button').click()");
                        chrome.DelayTime(1);
                        chrome.ExecuteScript("document.querySelectorAll('button')[1].click()");
                        chrome.DelayTime(1);
                    }

                    //
                    if (chrome.GetURL().StartsWith("https://m.facebook.com/si/actor_experience/actor_gateway"))
                    {
                        chrome.Click(4, "[data-sigil=\"touchable\"]");
                        chrome.DelayTime(1);
                    }

                    //
                    if (chrome.CheckExistElement("button[value=\"OK\"]") == 1)
                    {
                        chrome.Click(4, "button[value=\"OK\"]");
                        chrome.DelayTime(1);
                    }

                    //
                    if (chrome.CheckExistElement("[data-store-id=\"2\"]>span") == 1)
                    {
                        chrome.Click(4, "[data-store-id=\"2\"]>span");
                        chrome.DelayTime(1);
                    }

                    //
                    if (chrome.CheckExistElement("[data-nt=\"FB:HEADER_FOOTER_VIEW\"]>div>div>div>span>span") == 1)
                    {
                        chrome.Click(4, "[data-nt=\"FB:HEADER_FOOTER_VIEW\"]>div>div>div>span>span");
                        chrome.DelayTime(3);
                    }
                }
                else if (typeWeb == 1)
                {
                    if (chrome.GetURL().StartsWith("https://www.facebook.com/legal_consent"))
                    {
                        for (int i = 0; i < 5; i++)
                        {
                            if (chrome.CheckExistElement("button") != 1)
                                break;
                            chrome.ExecuteScript("document.querySelector('button').click()");
                            chrome.DelayTime(1);
                        }
                    }
                }

                //string body = "";
                //if (typeWeb == 1)
                //    body = (string)chrome.ExecuteScript("async function CheckLiveCookie() { var output = '0|0'; try { var response = await fetch('https://www.facebook.com/me'); if (response.ok) { var body = await response.text(); if (body.includes('id=\"code_in_cliff\"')||body.includes('name=\"new\"')||body.includes('name=\"c\"')) output = '1|0'; else if (!body.includes('checkpointSubmitButton') && !body.includes('checkpointBottomBar') && !body.includes('https://www.facebook.com/communitystandards/') && !body.includes('captcha_response') && !body.includes('FB:ACTION:OPEN_NT_SCREEN') && body.match(new RegExp('\"USER_ID\":\"(.*?)\"'))[1] == (document.cookie + ';').match(new RegExp('c_user=(.*?);'))[1]) output = '1|1'; } } catch {} return output; }; var c = await CheckLiveCookie(); return c");
                //else
                //{
                //    body = (string)chrome.ExecuteScript("async function CheckLiveCookie() { var output = '0|0'; try { var response = await fetch('https://m.facebook.com/me'); if (response.ok) { var body = await response.text(); if (body.includes('id=\"code_in_cliff\"')||body.includes('name=\"new\"')||body.includes('changeemail')||body.includes('name=\"c\"')) output = '1|0'; else if (!body.includes('checkpointSubmitButton') && !body.includes('checkpointBottomBar') && !body.includes('captcha_response') && !body.includes('FB:ACTION:OPEN_NT_SCREEN') && body.match(new RegExp('\"USER_ID\":\"(.*?)\"'))[1] == (document.cookie+';').match(new RegExp('c_user=(.*?);'))[1]) output = '1|1'; } } catch {} return output; }; var c = await CheckLiveCookie();  return c;");
                //    if (body.Split('|')[0] == "0")
                //        body = (string)chrome.ExecuteScript("async function CheckLiveCookie() { var output = '0|0'; try { var response = await fetch('https://www.facebook.com/me'); if (response.ok) { var body = await response.text(); if (body.includes('id=\"code_in_cliff\"')||body.includes('name=\"new\"')||body.includes('name=\"c\"')) output = '1|0'; else if (!body.includes('checkpointSubmitButton') && !body.includes('checkpointBottomBar') && !body.includes('https://www.facebook.com/communitystandards/') && !body.includes('captcha_response') && !body.includes('FB:ACTION:OPEN_NT_SCREEN') && !body.includes('/help/203305893040179') && body.match(new RegExp('\"USER_ID\":\"(.*?)\"'))[1] == (document.cookie + ';').match(new RegExp('c_user=(.*?);'))[1]) output = '1|1'; } } catch {} return output; }; var c = await CheckLiveCookie(); return c");
                //}

                CheckStatusAccount(chrome, true);
                switch (chrome.Status)
                {
                    case StatusChromeAccount.ChromeClosed:
                        return -2;
                    case StatusChromeAccount.LoginWithUserPass:
                    case StatusChromeAccount.LoginWithSelectAccount:
                        return 0;
                    case StatusChromeAccount.Checkpoint:
                        return 2;
                    case StatusChromeAccount.Logined:
                        return 1;
                    case StatusChromeAccount.NoInternet:
                        return -3;
                    default:
                        break;
                }
            }
            catch
            {
            }
            return 0;
        }
        public static bool IsCheckpoint(Chrome chrome)
        {
            try
            {
                if (chrome.CheckExistElements(0, "#checkpointSubmitButton", "#captcha_response", "[name=\"verification_method\"]", "#checkpointBottomBar", "[href =\"https://www.facebook.com/communitystandards/\"]") > 0)
                    return true;
            }
            catch
            {
            }
            return false;
        }
        public static bool IsCheckpointWhenLogin(Chrome chrome)
        {
            try
            {
                if (chrome.CheckExistElements(0, "[name=\"captcha_response\"]", "#captcha_response", "[name=\"password_new\"]", "[name=\"verification_method\"]", "[href =\"https://www.facebook.com/communitystandards/\"]") > 0)
                    return true;
            }
            catch
            {
            }
            return false;
        }
        public static string GetTokenEAAG(Chrome chrome)
        {
            string token = "";
            try
            {
                if (!chrome.GetURL().Contains("https://business.facebook.com/"))
                    chrome.GotoURL("https://business.facebook.com/");
                token = (string)chrome.ExecuteScript("async function GetTokenEaag() { var output = ''; try { var response = await fetch('https://business.facebook.com/business_locations/'); if (response.ok) { var body = await response.text(); output=body.match(new RegExp('EAAG(.*?)\"'))[0].replace('\"',''); } } catch {} return output; }; var c = await GetTokenEaag(); return c;");
            }
            catch
            {
            }
            return token;
        }

        public static string RequestGet(Chrome chrome, string url, string website)
        {
            try
            {
                if (!chrome.GetURL().StartsWith(website))
                    chrome.GotoURL(website);
                string rq = (string)chrome.ExecuteScript("async function RequestGet() { var output = ''; try { var response = await fetch('" + url + "'); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await RequestGet(); return c;");
                return rq;
            }
            catch { }

            return "";
        }
        public static string RequestPost(Chrome chrome, string url, string data, string website, string contentType = "application/x-www-form-urlencoded")
        {
            try
            {
                if (!chrome.GetURL().StartsWith(website))
                    chrome.GotoURL(website);
                chrome.DelayTime(1);
                data = data.Replace("\n", "\\n").Replace("\"", "\\\"");
                string rq = (string)chrome.ExecuteScript("async function RequestPost() { var output = ''; try { var response = await fetch('" + url + "', { method: 'POST', body: '" + data + "', headers: { 'Content-Type': '" + contentType + "' } }); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await RequestPost(); return c;");
                return rq;
            }
            catch { }

            return "";
        }

        public static string GetBirthday(Chrome chrome, string token)
        {
            string output = "";
            try
            {
                if (!chrome.GetURL().Contains("https://graph.facebook.com/"))
                    chrome.GotoURL("https://graph.facebook.com/");
                string rq = (string)chrome.ExecuteScript("async function RequestGet() { var output = ''; try { var response = await fetch('" + "https://graph.facebook.com/me?fields=id,birthday,name&access_token=" + token + "'); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await RequestGet(); return c;");
                JObject json = JObject.Parse(rq);
                return json["id"].ToString() + "|" + json["birthday"].ToString() + "|" + json["name"].ToString();
            }
            catch
            {
            }

            return output;
        }

        public static List<string> GetMyListUidMessage(Chrome chrome)
        {
            List<string> lstMessage = new List<string>();
            try
            {
                if (!chrome.GetURL().Contains("https://mbasic.facebook.com/"))
                    chrome.GotoURL("https://mbasic.facebook.com/");
                string htmlMessage = (string)chrome.ExecuteScript("async function GetListUidNameFriend() { var output = ''; try { var response = await fetch('https://mbasic.facebook.com/messages/'); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await GetListUidNameFriend(); return c;");
                int moreAcc = 1;
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
                            uid = WebUtility.HtmlDecode(uid);
                            if (!lstMessage.Contains(uid))
                                lstMessage.Add(uid);
                        }
                        catch { }
                    }
                    linkReadMes = Regex.Match(htmlMessage, "/messages/.pageNum=(.*?)\"").Value.Replace("amp;", "");
                    htmlMessage = (string)chrome.ExecuteScript("async function GetListUidNameFriend() { var output = ''; try { var response = await fetch('https://mbasic.facebook.com" + linkReadMes + "'); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await GetListUidNameFriend(); return c;");

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

        public static List<string> GetMyListComments(Chrome chrome, int numberMonth)
        {
            List<string> lstComment = new List<string>();
            try
            {
                if (!chrome.GetURL().Contains("https://mbasic.facebook.com/"))
                    chrome.GotoURL("https://mbasic.facebook.com/");
                string link_mau = "https://mbasic.facebook.com/{0}/allactivity/?category_key=commentscluster&timestart={1}&timeend={2}";

                string uid = chrome.ExecuteScript("return (document.cookie + ';').match(new RegExp('c_user=(.*?);'))[1]").ToString();

                string timeStart = "";
                string timeEnd = "";

                string link = "";
                string htmlActivity = "";

                MatchCollection matchCollection = null;

                List<string> lstLink = new List<string>();
                for (int i = 0; i < numberMonth; i++)
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
                        htmlActivity = RequestGet(chrome, link, "https://mbasic.facebook.com/");

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

        public static List<string> GetMyListUidNameFriend(Chrome chrome, string token)
        {
            List<string> listFriend = new List<string>();

            try
            {
                if (!chrome.GetURL().Contains("https://graph.facebook.com/"))
                    chrome.GotoURL("https://graph.facebook.com/");
                string getListFriend = (string)chrome.ExecuteScript("async function GetListUidNameFriend() { var output = ''; try { var response = await fetch('https://graph.facebook.com/me/friends?limit=5000&fields=id,name&access_token=" + token + "'); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await GetListUidNameFriend(); return c;");

                JObject objFriend = JObject.Parse(getListFriend);
                if (objFriend["data"].Count() > 0)
                {
                    for (int i = 0; i < objFriend["data"].Count(); i++)
                    {
                        string uidFr = objFriend["data"][i]["id"].ToString();
                        string nameFr = objFriend["data"][i]["name"].ToString();
                        listFriend.Add(uidFr + "|" + nameFr);
                    }
                }
            }
            catch
            {
            }

            return listFriend;
        }

        internal static void CheckStatusAccount(Chrome chrome, bool isSendRequest)
        {
            try
            {
                if (chrome.CheckChromeClosed())
                {
                    chrome.Status = StatusChromeAccount.ChromeClosed;
                    return;
                }

                string html = "";
                if (isSendRequest)
                    html = RequestGet(chrome, "https://m.facebook.com/login", "https://m.facebook.com/");

                if (html == "") html = chrome.GetPageSource();
                if (html == null || html == "" || html == "-2")
                {
                    chrome.Status = StatusChromeAccount.ChromeClosed;
                    return;
                }

                if (html.Contains("error-information-popup-content") || html.Contains("suggestionsSummaryList"))
                {
                    chrome.Status = StatusChromeAccount.NoInternet;
                    return;
                }

                if (Regex.IsMatch(html, "login_form"))
                {
                    chrome.Status = StatusChromeAccount.LoginWithUserPass;
                    return;
                }

                if (Regex.IsMatch(html, "login_profile_form") || Regex.IsMatch(html, "/login/device-based/validate-pin"))
                {
                    chrome.Status = StatusChromeAccount.LoginWithSelectAccount;
                    return;
                }

                //logout
                if (Convert.ToBoolean(chrome.ExecuteScript("var kq=false;if(document.querySelector('#mErrorView')!=null && !document.querySelector('#mErrorView').getAttribute('style').includes('display:none')) kq=true;return kq+''")) || Regex.IsMatch(html, "href=\"https://m.facebook.com/login.php"))
                {
                    chrome.Status = StatusChromeAccount.LoginWithSelectAccount;
                    return;
                }

                string currentUrl = chrome.GetURL();
                if (currentUrl.Contains("facebook.com/checkpoint/") || currentUrl.Contains("facebook.com/nt/screen/?params=%7B%22token") || currentUrl.Contains("facebook.com/x/checkpoint/") || CheckStringContainKeyword(html, new List<string>() { "verification_method", "checkpointBottomBar", "submit[Yes]", "password_new", "send_code", "/checkpoint/dyi", "captcha_response", "https://www.facebook.com/communitystandards/", "help/121104481304395" }))
                {
                    chrome.Status = StatusChromeAccount.Checkpoint;
                    return;
                }

                if (Regex.IsMatch(html, "/friends/") || chrome.CheckExistElement("a[href*='/friends/']") == 1 || currentUrl == "https://m.facebook.com/home.php?ref=wizard&_rdr")
                    chrome.Status = StatusChromeAccount.Logined;
            }
            catch
            {
            }
        }
        static bool CheckStringContainKeyword(string content, List<string> lstKerword)
        {
            for (int i = 0; i < lstKerword.Count; i++)
            {
                if (Regex.IsMatch(content, lstKerword[i]))
                    return true;
            }
            return false;
        }


        public static List<string> BackupImageOne(Chrome chrome, string uidFr, string nameFr, string token, int countImage = 20)
        {
            List<string> listImageBackup = new List<string>();
            try
            {
                if (!chrome.GetURL().Contains("https://graph.facebook.com/"))
                    chrome.GotoURL("https://graph.facebook.com/");
                string htmlImage = (string)chrome.ExecuteScript("async function GetListUidNameFriend() { var output = ''; try { var response = await fetch('https://graph.facebook.com/" + uidFr + "/photos?fields=images&limit=" + countImage + "&access_token=" + token + "'); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await GetListUidNameFriend(); return c;");
                JObject objImg = JObject.Parse(htmlImage);
                int stt = 0;
                if (objImg != null && htmlImage.Contains("images"))
                {
                    for (int j = 0; j < objImg["data"].Count(); j++)
                    {
                        stt = objImg["data"][j]["images"].ToList().Count - 1;
                        listImageBackup.Add(uidFr + "*" + nameFr + "*" + objImg["data"][j]["images"][stt]["source"] + "|" + objImg["data"][j]["images"][stt]["width"] + "|" + objImg["data"][j]["images"][stt]["height"]);
                    }
                }
            }
            catch { }

            return listImageBackup;
        }

        public static List<string> GetMyListUidFriend(Chrome chrome)
        {
            List<string> listFriend = new List<string>();

            try
            {
                string token = GetTokenEAAAAZ(chrome);
                if (!chrome.GetURL().Contains("https://graph.facebook.com/"))
                    chrome.GotoURL("https://graph.facebook.com/");
                string getListFriend = (string)chrome.ExecuteScript("async function GetListUidNameFriend() { var output = ''; try { var response = await fetch('https://graph.facebook.com/me/friends?limit=5000&fields=id&access_token=" + token + "'); if (response.ok) { var body = await response.text(); return body; } } catch {} return output; }; var c = await GetListUidNameFriend(); return c;");

                JObject objFriend = JObject.Parse(getListFriend);
                if (objFriend["data"].Count() > 0)
                {
                    for (int i = 0; i < objFriend["data"].Count(); i++)
                    {
                        string uidFr = objFriend["data"][i]["id"].ToString();
                        listFriend.Add(uidFr);
                    }
                }
            }
            catch
            {
            }

            return listFriend;
        }

        public static bool SkipNotifyWhenAddFriend(Chrome chrome)
        {
            bool isHave = true;
            string el_error = "";

            int checkerorr = chrome.CheckExistElements(2, "[data-sigil=\" m-overlay-layer\"] button", "[data-sigil=\" m-overlay-layer\"] [value=\"OK\"]", "[data-sigil=\"touchable m-error-overlay-done\"]", "[data-sigil=\"touchable m-overlay-layer\"]", "[data-sigil=\"touchable m-error-overlay-cancel\"]");
            switch (checkerorr)
            {
                case 0:
                    isHave = false;
                    break;
                case 1:
                    el_error = "[data-sigil=\" m-overlay-layer\"] button";
                    break;
                case 2:
                    el_error = "[data-sigil=\" m-overlay-layer\"] [value=\"OK\"]";
                    break;
                case 3:
                    el_error = "[data-sigil=\"touchable m-error-overlay-done\"]";
                    break;
                case 4:
                    el_error = "[data-sigil=\"touchable m-overlay-layer\"]";
                    break;
                case 5:
                    el_error = "[data-sigil=\"touchable m-error-overlay-cancel\"]";
                    break;
            }
            if (el_error != "")
                chrome.ExecuteScript("document.querySelector('" + el_error + "').click();");
            return isHave;
        }
        public static string GetFbDtag(Chrome chrome)
        {
            try
            {
                string rq = RequestGet(chrome, "https://m.facebook.com/help/", "https://m.facebook.com");
                return Regex.Match(rq, MCommon.Common.Base64Decode("ImR0c2dfYWciOnsidG9rZW4iOiIoLio/KSI=")).Groups[1].Value;
            }
            catch
            {
                return "";
            }
        }
        public static List<string> GetListGroup(Chrome chrome)
        {
            List<string> lstGroup = new List<string>();
            try
            {
                string fb_dtag = GetFbDtag(chrome);
                string uid = Regex.Match(chrome.GetCookieFromChrome(), "c_user=(.*?);").Groups[1].Value;
                string url = "https://www.facebook.com/ajax/typeahead/first_degree.php?fb_dtsg_ag=" + fb_dtag + "&filter%5B0%5D=group&viewer=" + uid + "&__user=" + uid + "&__a=1&__dyn=&__comet_req=0&jazoest=26581";
                string rq = RequestGet(chrome, url, "https://www.facebook.com/ajax/typeahead/first_degree.php").Replace("for (;;);", "");
                JObject json = JObject.Parse(rq);
                foreach (var item in json["payload"]["entries"])
                {
                    try
                    {
                        lstGroup.Add($"{item["uid"]}|{item["text"]}|{item["size"]}");
                    }
                    catch
                    {
                    }
                }
            }
            catch (Exception ex)
            {
            }
            return lstGroup;
        }
        public static List<string> GetListPage(Chrome chrome)
        {
            List<string> lstId = new List<string>();
            try
            {
                string html = "";

                string token = GetTokenEAAAAZ(chrome);
                html = RequestGet(chrome, "https://graph.facebook.com/v3.0/me/accounts?access_token=" + token + "&limit=5000&fields=id,name,like,country_page_likes", "https://graph.facebook.com").ToString();
                JObject json = JObject.Parse(html);
                foreach (var item in json["data"])
                {
                    lstId.Add(item["id"].ToString());
                }
            }
            catch
            {
            }
            return lstId;
        }
    }
}
