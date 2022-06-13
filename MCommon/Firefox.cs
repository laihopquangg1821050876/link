using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MCommon
{
    public class Firefox
    {
        private FirefoxDriver firefox;
        public bool isAlive = false;
        public bool HideBrowser { get; set; }
        //public bool Incognito { get; set; }
        /// <summary>
        /// Disable Image(default: 1)
        /// </summary>
        public bool DisableImage { get; set; }
        /// <summary>
        /// Disable Sound(default: 1)
        /// </summary>
        public bool DisableSound { get; set; }
        public bool AutoPlayVideo { get; set; }
        /// <summary>
        /// UserAgent(default: Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/74.0.3729.131 Safari/537.36)
        /// </summary>
        public string UserAgent { get; set; }
        public string ProfilePath { get; set; }
        /// <summary>
        /// Browser Size(default: (300,300))
        /// </summary>
        public Point Size { get; set; }
        /// <summary>
        /// Browser Heigh(default: 300)
        /// </summary>
        public int Size_Heigh { get; set; }
        /// <summary>
        /// Browser Width(default: 300)
        /// </summary>
        public int Size_Width { get; set; }
        /// <summary>
        /// Browser Position(default: (0,0))
        /// </summary>
        public Point Position { get; set; }
        /// <summary>
        /// Browser PositionX(default: 0)
        /// </summary>
        public int Position_X { get; set; }
        /// <summary>
        /// Browser PositionY(default: 0)
        /// </summary>
        public int Position_Y { get; set; }
        /// <summary>
        /// Time Wait For Searching Element (seconds - default: 0)
        /// </summary>
        public int TimeWaitForSearchingElement { get; set; }
        /// <summary>
        /// Time Wait For Loading Page (minutes - default: 5)
        /// </summary>
        public int TimeWaitForLoadingPage { get; set; }
        /// <summary>
        /// Socks5 proxy or Port 911
        /// </summary>
        public string Proxy { get; set; }
        public int TypeProxy { get; set; }
        public string App { get; set; }
        public string LinkToOtherBrowser { get; set; }
        public string PathExtension { get; set; }

        public Firefox()
        {
            DisableImage = true;
            DisableSound = false;
            UserAgent = "";
            ProfilePath = "";
            Size_Heigh = 300;
            Size_Width = 300;
            Size = new Point(Size_Width, Size_Heigh);
            Position_X = 300;
            Position_Y = 0;
            Proxy = "";
            TypeProxy = 0;
            Position = new Point(Position_X, Position_Y);
            TimeWaitForSearchingElement = 0;
            TimeWaitForLoadingPage = 5;
            App = "";
            AutoPlayVideo = false;
            LinkToOtherBrowser = "";
            PathExtension = "data\\extension";
        }
        public bool Open()
        {
            bool isSuccess = false;
            isAlive = true;
            try
            {
                FirefoxDriverService service = FirefoxDriverService.CreateDefaultService();
                service.HideCommandPromptWindow = true;

                //profile.SetPreference("javascript.enabled", true);
                FirefoxOptions options = new FirefoxOptions();
                options.SetPreference("security.notification_enable_delay", 0);
                options.SetPreference("dom.webnotifications.enabled", false);
                options.SetPreference("permissions.default.image", DisableImage == true ? 1 : 0);
                //options.SetPreference("general.useragent.override", UserAgent);
                options.SetPreference("browser.download.folderList", 2);
                //firefoxOptions.SetPreference("browser.download.dir", text);
                options.SetPreference("browser.download.manager.alertOnEXEOpen", false);
                options.SetPreference("browser.helperApps.neverAsk.saveToDisk", "application/msword, application/csv, application/ris, text/csv, image/png, application/pdf, text/html, text/plain, application/zip, application/x-zip, application/x-zip-compressed, application/download, application/octet-stream");
                options.SetPreference("browser.download.manager.showWhenStarting", false);
                options.SetPreference("browser.download.manager.focusWhenStarting", false);
                options.SetPreference("browser.download.useDownloadDir", true);
                options.SetPreference("browser.helperApps.alwaysAsk.force", false);
                options.SetPreference("browser.download.manager.alertOnEXEOpen", false);
                options.SetPreference("browser.download.manager.closeWhenDone", true);
                options.SetPreference("browser.download.manager.showAlertOnComplete", false);
                options.SetPreference("browser.download.manager.useWindow", false);
                options.SetPreference("services.sync.prefs.sync.browser.download.manager.showWhenStarting", false);
                options.SetPreference("pdfjs.disabled", true);
                options.AddArguments(new string[]
                {
                    "-width="+Size.X,
                    "-height="+Size.Y,
                });
                if (UserAgent != "")
                {
                    options.SetPreference("general.useragent.override", UserAgent);
                }
                else
                {
                    options.SetPreference("general.useragent.override", "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:82.0) Gecko/20100101 Firefox/82.0");
                }
                
                FirefoxProfileManager firefoxProfileManager = new FirefoxProfileManager();
                //FirefoxProfile profile = new FirefoxProfile(@"C:\Users\Nam\AppData\Roaming\Mozilla\Firefox\Profiles");
                //options.Profile = profile;
                //string dairong = SetPositionAndSizeFireFox(options, 6);
                firefox = new FirefoxDriver(service, options);
                firefox.Manage().Timeouts().PageLoad = TimeSpan.FromMinutes(TimeWaitForLoadingPage);
                firefox.Manage().Window.Position = Position;
                isSuccess = true;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, "firefox.Open()");
            }
            return isSuccess;
        }
        public static string SetPositionAndSizeFireFox(FirefoxOptions option, int i)
        {
            int chngang = 0;
            int chdoc = 0;
            int getWidthScreen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Width;
            int getHeightScreen = System.Windows.Forms.Screen.PrimaryScreen.Bounds.Height;
            int chieungang = getWidthScreen / 3;
            int chieudoc = getHeightScreen / 2;
            if (i < 3)
            {
                chngang = chieungang * i;
                chdoc = 0;
            }
            else
            {
                chngang = chieungang * (i % 3);
                int so = i / 2;
                if (so % 2 == 0)
                {
                    chdoc = 0;

                }
                else
                {
                    chdoc = chieudoc;
                }
            }
            option.AddArgument($"--width={chieungang}");
            option.AddArgument($"--height={chieudoc}");
            //option.AddArgument("--window-position=" + chngang.ToString() + "," + chdoc.ToString());
            return chngang + "|" + chdoc;
        }
        public string GetCssSelector(string querySelector, string attributeName, string attributeValue)
        {
            string output = "";
            if (isAlive)
            {
                try
                {
                    output = firefox.ExecuteScript("function GetSelector(el){let path=[],parent;while(parent=el.parentNode){path.unshift(`${el.tagName}:nth-child(${[].indexOf.call(parent.children, el)+1})`);el=parent}return `${path.join('>')}`.toLowerCase()}; function GetCssSelector(selector, attribute, value){var c = document.querySelectorAll(selector); for (i = 0; i < c.length; i++) { if (c[i].getAttribute(attribute)!=null && c[i].getAttribute(attribute).includes(value)) { return GetSelector(c[i])} }; return '';}; return GetCssSelector('" + querySelector + "','" + attributeName + "','" + attributeValue + "')").ToString();
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.GetCssSelector({querySelector},{attributeName},{attributeValue})");
                }
            }
            return output;
        }
        public string GetAttributeValue(string querySelector, string attributeName)
        {
            string output = "";
            if (isAlive)
            {
                try
                {
                    output = firefox.ExecuteScript("return document.querySelector('" + querySelector + "').getAttribute('" + attributeName + "')").ToString();
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.GetAttributeValue({querySelector},{attributeName})");
                }
            }
            return output;
        }

        public void ScrollSmooth(int distance)
        {
            if (isAlive)
            {
                try
                {
                    firefox.ExecuteScript("window.scrollBy({ top: " + distance + ",behavior: 'smooth'});");
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"chrome.ScrollSmooth({distance})");
                }
            }
        }
        public string GetUseragent()
        {
            string ua = "";
            {
                try
                {
                    ua = firefox.ExecuteScript("return navigator.userAgent").ToString();
                }
                catch
                {
                }
            }
            return ua;
        }
        public bool SendKeyDown(int typeAttribute, string attributeValue)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    switch (typeAttribute)
                    {
                        case 1:
                            firefox.FindElementById(attributeValue).SendKeys(Keys.ArrowDown);
                            break;
                        case 2:
                            firefox.FindElementByName(attributeValue).SendKeys(Keys.ArrowDown);
                            break;
                        case 3:
                            firefox.FindElementByXPath(attributeValue).SendKeys(Keys.ArrowDown);
                            break;
                        case 4:
                            firefox.FindElementByCssSelector(attributeValue).SendKeys(Keys.ArrowDown);
                            break;
                        default:
                            break;
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.SendKeyDown({typeAttribute},{attributeValue})");
                }
            }
            return isSuccess;
        }
        public string GetURL()
        {
            if (isAlive)
            {
                try
                {
                    return firefox.Url;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, "firefox.GetURL()");
                }
            }
            return "";
        }
        public bool GotoURL(string url)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    firefox.Navigate().GoToUrl(url);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.GotoURL({url})");
                }
            }
            return isSuccess;
        }
        public bool Refresh()
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    firefox.Navigate().Refresh();
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.Refresh()");
                }
            }
            return isSuccess;
        }
        public bool GotoBackPage()
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    firefox.Navigate().Back();
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.GotoBackPage()");
                }
            }
            return isSuccess;
        }
        public bool HoverElement(int typeAttribute, string attributeValue, double timeHover_second)
        {
            if (isAlive)
            {
                try
                {
                    switch (typeAttribute)
                    {
                        case 1:
                            new Actions(firefox).MoveToElement(firefox.FindElement(By.Id(attributeValue))).Perform();
                            break;
                        case 2:
                            new Actions(firefox).MoveToElement(firefox.FindElement(By.Name(attributeValue))).Perform();
                            break;
                        case 3:
                            new Actions(firefox).MoveToElement(firefox.FindElement(By.XPath(attributeValue))).Perform();
                            break;
                        case 4:
                            new Actions(firefox).MoveToElement(firefox.FindElement(By.CssSelector(attributeValue))).Perform();
                            break;
                        default:
                            break;
                    }
                    Thread.Sleep(Convert.ToInt32(timeHover_second * 1000));

                    return true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.HoverElement({typeAttribute}, {attributeValue}, {timeHover_second})");
                }
            }
            return false;
        }
        public bool Click(int typeAttribute, string attributeValue, int index = 0, int subTypeAttribute = 0, string subAttributeValue = "", int subIndex = 0)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    if (subTypeAttribute == 0)
                    {
                        switch (typeAttribute)
                        {
                            case 1:
                                firefox.FindElementsById(attributeValue)[index].Click();
                                break;
                            case 2:
                                firefox.FindElementsByName(attributeValue)[index].Click();
                                break;
                            case 3:
                                firefox.FindElementsByXPath(attributeValue)[index].Click();
                                break;
                            case 4:
                                firefox.FindElementsByCssSelector(attributeValue)[index].Click();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (typeAttribute)
                        {
                            case 1:
                                firefox.FindElementsById(attributeValue)[index].FindElements(By.Id(subAttributeValue))[subIndex].Click();
                                break;
                            case 2:
                                firefox.FindElementsByName(attributeValue)[index].FindElements(By.Name(subAttributeValue))[subIndex].Click();
                                break;
                            case 3:
                                firefox.FindElementsByXPath(attributeValue)[index].FindElements(By.XPath(subAttributeValue))[subIndex].Click();
                                break;
                            case 4:
                                firefox.FindElementsByCssSelector(attributeValue)[index].FindElements(By.CssSelector(subAttributeValue))[subIndex].Click();
                                break;
                            default:
                                break;
                        }
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.Click({typeAttribute},{attributeValue})");
                }
            }
            return isSuccess;
        }
        public bool ClickWithAction(int typeAttribute, string attributeValue, int index = 0, int subTypeAttribute = 0, string subAttributeValue = "", int subIndex = 0)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    if (subTypeAttribute != 0)
                    {
                        switch (typeAttribute)
                        {
                            case 1:
                                new Actions(firefox).Click(firefox.FindElementsById(attributeValue)[index].FindElements(By.Id(subAttributeValue))[subIndex]).Perform();
                                break;
                            case 2:
                                new Actions(firefox).Click(firefox.FindElementsByName(attributeValue)[index].FindElements(By.Name(subAttributeValue))[subIndex]).Perform();
                                break;
                            case 3:
                                new Actions(firefox).Click(firefox.FindElementsByXPath(attributeValue)[index].FindElements(By.XPath(subAttributeValue))[subIndex]).Perform();
                                break;
                            case 4:
                                new Actions(firefox).Click(firefox.FindElementsByCssSelector(attributeValue)[index].FindElements(By.CssSelector(subAttributeValue))[subIndex]).Perform();
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        switch (typeAttribute)
                        {
                            case 1:
                                new Actions(firefox).Click(firefox.FindElementsById(attributeValue)[index]).Perform();
                                break;
                            case 2:
                                new Actions(firefox).Click(firefox.FindElementsByName(attributeValue)[index]).Perform();
                                break;
                            case 3:
                                new Actions(firefox).Click(firefox.FindElementsByXPath(attributeValue)[index]).Perform();
                                break;
                            case 4:
                                new Actions(firefox).Click(firefox.FindElementsByCssSelector(attributeValue)[index]).Perform();
                                break;
                            default:
                                break;
                        }
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.ClickWithAction({typeAttribute},{attributeValue})");
                }
            }
            return isSuccess;
        }
        public bool SendKeys(int typeAttribute, string attributeValue, string content, bool isClick = true)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    if (isClick)
                        Click(typeAttribute, attributeValue);
                    switch (typeAttribute)
                    {
                        case 1:
                            firefox.FindElementById(attributeValue).SendKeys(content);
                            break;
                        case 2:
                            firefox.FindElementByName(attributeValue).SendKeys(content);
                            break;
                        case 3:
                            firefox.FindElementByXPath(attributeValue).SendKeys(content);
                            break;
                        case 4:
                            firefox.FindElementByCssSelector(attributeValue).SendKeys(content);
                            break;
                        default:
                            break;
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.SendKeys({typeAttribute},{attributeValue},{content},{isClick})");
                }
            }
            return isSuccess;
        }
        public bool SendKeys(int typeAttribute, string attributeValue, string content, double timeDelay_Second, bool isClick = true)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    if (isClick)
                        Click(typeAttribute, attributeValue);
                    for (int i = 0; i < content.Length; i++)
                    {
                        switch (typeAttribute)
                        {
                            case 1:
                                firefox.FindElementById(attributeValue).SendKeys(content[i].ToString());
                                break;
                            case 2:
                                firefox.FindElementByName(attributeValue).SendKeys(content[i].ToString());
                                break;
                            case 3:
                                firefox.FindElementByXPath(attributeValue).SendKeys(content[i].ToString());
                                break;
                            case 4:
                                firefox.FindElementByCssSelector(attributeValue).SendKeys(content[i].ToString());
                                break;
                            default:
                                break;
                        }

                        Thread.Sleep(Convert.ToInt32(timeDelay_Second * 1000));
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.SendKeys({typeAttribute},{attributeValue},{content},{timeDelay_Second},{isClick})");
                }
            }
            return isSuccess;
        }
        public bool SelectText(int typeAttribute, string attributeValue)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    switch (typeAttribute)
                    {
                        case 1:
                            firefox.FindElementById(attributeValue).SendKeys(Keys.Control + "a");
                            break;
                        case 2:
                            firefox.FindElementByName(attributeValue).SendKeys(Keys.Control + "a");
                            break;
                        case 3:
                            firefox.FindElementByXPath(attributeValue).SendKeys(Keys.Control + "a");
                            break;
                        case 4:
                            firefox.FindElementByCssSelector(attributeValue).SendKeys(Keys.Control + "a");
                            break;
                        default:
                            break;
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.SelectText({typeAttribute},{attributeValue})");
                }
            }
            return isSuccess;
        }
        public bool ClearText(int typeAttribute, string attributeValue)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    switch (typeAttribute)
                    {
                        case 1:
                            firefox.FindElementById(attributeValue).Clear();
                            break;
                        case 2:
                            firefox.FindElementByName(attributeValue).Clear();
                            break;
                        case 3:
                            firefox.FindElementByXPath(attributeValue).Clear();
                            break;
                        case 4:
                            firefox.FindElementByCssSelector(attributeValue).Clear();
                            break;
                        default:
                            break;
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.ClearText({typeAttribute},{attributeValue})");
                }
            }
            return isSuccess;
        }
        public bool CheckExistElement(string querySelector, double timeWait_Second = 0)
        {
            bool isExist = true;
            if (isAlive)
            {
                try
                {
                    int timeStart = Environment.TickCount;
                    while ((string)firefox.ExecuteScript("return document.querySelectorAll('" + querySelector + "').length+''") == "0")
                    {
                        if (Environment.TickCount - timeStart > timeWait_Second * 1000)
                        {
                            isExist = false;
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    isExist = false;
                    ExportError(null, ex, $"firefox.CheckExistElement({querySelector},{timeWait_Second})");
                }
            }
            return isExist;
        }
        public bool CheckExistElementv2(string JSPath, double timeWait_Second = 0)
        {
            bool isExist = true;
            if (isAlive)
            {
                try
                {
                    int timeStart = Environment.TickCount;
                    while ((string)firefox.ExecuteScript("return " + JSPath + ".length+''") == "0")
                    {
                        if (Environment.TickCount - timeStart > timeWait_Second * 1000)
                        {
                            isExist = false;
                            break;
                        }
                        Thread.Sleep(1000);
                    }
                }
                catch (Exception ex)
                {
                    isExist = false;
                    ExportError(null, ex, $"firefox.CheckExistElement({JSPath},{timeWait_Second})");
                }
            }
            return isExist;
        }
        public bool CheckChromeClosed()
        {
            bool isClosed = true;
            if (isAlive)
            {
                try
                {
                    var x = firefox.Title;
                    isClosed = false;
                }
                catch (Exception ex)
                {
                    isAlive = false;
                    ExportError(null, ex, $"firefox.CheckChromeClosed()");
                }
            }
            return isClosed;
        }
        public bool WaitForSearchElement(string querySelector, int typeSearch = 0, double timeWait_Second = 0)
        {
            bool isDone = true;
            if (isAlive)
            {
                try
                {
                    int timeStart = Environment.TickCount;
                    if (typeSearch == 0)
                    {
                        while ((string)firefox.ExecuteScript("return document.querySelectorAll('" + querySelector + "').length+''") == "0")
                        {
                            if (Environment.TickCount - timeStart > timeWait_Second * 1000)
                            {
                                isDone = false;
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                    }
                    else
                    {
                        while ((string)firefox.ExecuteScript("return document.querySelectorAll('" + querySelector + "').length+''") != "0")
                        {
                            if (Environment.TickCount - timeStart > timeWait_Second * 1000)
                            {
                                isDone = false;
                                break;
                            }
                            Thread.Sleep(1000);
                        }
                    }
                }
                catch (Exception ex)
                {
                    isDone = false;
                    ExportError(null, ex, $"firefox.WaitForSearchElement({querySelector},{typeSearch},{timeWait_Second})");
                }
            }
            return isDone;
        }
        public int CheckExistElements(double timeWait_Second = 0, params string[] querySelectors)
        {
            int result = 0;
            if (isAlive)
            {
                try
                {
                    int timeStart = Environment.TickCount;
                    while (true)
                    {
                        for (int i = 0; i < querySelectors.Length; i++)
                        {
                            if (CheckExistElement(querySelectors[i]))
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
                    ExportError(null, ex, $"firefox.CheckExistElements({timeWait_Second},{string.Join("|", querySelectors)})");
                }
            }
            return result;
        }
        public bool SendEnter(int typeAttribute, string attributeValue)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    switch (typeAttribute)
                    {
                        case 1:
                            firefox.FindElementById(attributeValue).SendKeys(Keys.Enter);
                            break;
                        case 2:
                            firefox.FindElementByName(attributeValue).SendKeys(Keys.Enter);
                            break;
                        case 3:
                            firefox.FindElementByXPath(attributeValue).SendKeys(Keys.Enter);
                            break;
                        case 4:
                            firefox.FindElementByCssSelector(attributeValue).SendKeys(Keys.Enter);
                            break;
                        default:
                            break;
                    }
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.SendEnter({typeAttribute},{attributeValue})");
                }
            }
            return isSuccess;
        }
        public bool Scroll(int x, int y)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    var js1 = String.Format("window.scrollTo({0}, {1})", x, y);
                    firefox.ExecuteScript(js1);
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.Scroll({x},{y})");
                }
            }
            return isSuccess;
        }
        public void ScrollSmooth(string JSpath)
        {
            if (isAlive)
            {
                try
                {
                    firefox.ExecuteScript(JSpath + ".scrollIntoView({ behavior: 'smooth', block: 'center'});");
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.ScrollSmooth({JSpath})");
                }
            }
        }
        public int CheckExistElementOnScreen(string JSpath)
        {
            int check = -2;
            if (isAlive)
            {
                try
                {
                    check = Convert.ToInt32(firefox.ExecuteScript("var check='';x=" + JSpath + ";if(x.getBoundingClientRect().top<=0) check='-1'; else if(x.getBoundingClientRect().top+x.getBoundingClientRect().height>window.innerHeight) check='1'; else check='0'; return check;"));
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.CheckExistElementOnScreen({JSpath})");
                }
            }
            return check;
        }
        public Point GetSizeChrome()
        {

            Point point = new Point(0, 0);
            if (isAlive)
            {
                try
                {
                    string temp = firefox.ExecuteScript("return window.innerHeight+'|'+window.innerWidth").ToString();
                    point.X = Convert.ToInt32(temp.Split('|')[1]);
                    point.Y = Convert.ToInt32(temp.Split('|')[0]);
                }
                catch
                {
                }
            }
            return point;
        }
        public void Close()
        {
            try
            {
                if (firefox != null)
                    firefox.Quit();
                isAlive = false;
            }
            catch (Exception ex)
            {
                ExportError(null, ex, $"firefox.Close()");
            }
        }
        public void AddCookieIntoFirefox(string cookie, string domain = ".facebook.com")
        {
            if (isAlive)
            {
                try
                {
                    string[] arrData = cookie.Split(';');
                    foreach (string item in arrData)
                    {
                        if (item.Trim() != "")
                        {
                            string[] pars = item.Split('=');
                            if (pars.Count() > 1 && pars[0].Trim() != "")
                            {
                                Cookie cok = new Cookie(pars[0].Trim(), item.Substring(item.IndexOf('=') + 1).Trim(), domain, "/", DateTime.Now.AddDays(10));
                                firefox.Manage().Cookies.AddCookie(cok);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.AddCookieIntoChrome({cookie},{domain})");
                }
            }
        }
        public string GetCookieFromChrome(string domain = "facebook")
        {
            string cookie = "";
            if (isAlive)
            {
                try
                {
                    var sess = firefox.Manage().Cookies.AllCookies.ToArray();
                    foreach (var item in sess)
                    {
                        if (item.Domain.Contains(domain))
                            cookie += item.Name + "=" + item.Value + ";";
                    }
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.GetCookieFromChrome({domain})");
                }
            }
            return cookie;
        }
        public void OpenNewTab(string url, bool switchToLastTab = true)
        {
            if (isAlive)
            {
                try
                {
                    firefox.ExecuteScript("window.open('" + url + "', '_blank').focus();");
                    if (switchToLastTab)
                        firefox.SwitchTo().Window(firefox.WindowHandles.Last());
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.OpenNewTab({url},{switchToLastTab})");
                }
            }
        }
        public void CloseCurrentTab()
        {
            if (isAlive)
            {
                try
                {
                    firefox.Close();
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.CloseCurrentTab()");
                }
            }
        }
        public void SwitchToFirstTab()
        {
            if (isAlive)
            {
                try
                {
                    firefox.SwitchTo().Window(firefox.WindowHandles.First());
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.SwitchToFirstTab()");
                }
            }
        }
        public void SwitchToLastTab()
        {
            if (isAlive)
            {
                try
                {
                    firefox.SwitchTo().Window(firefox.WindowHandles.Last());
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.SwitchToLastTab()");
                }
            }
        }
        public void DelayTime(double timeDelay_Seconds)
        {
            if (isAlive)
            {
                try
                {
                    Thread.Sleep(Convert.ToInt32(timeDelay_Seconds * 1000));
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.DelayTime({timeDelay_Seconds})");
                }
            }
        }
        public bool Select(int typeAttribute, string attributeValue, string value)
        {
            bool result = false;
            if (isAlive)
            {
                try
                {
                    switch (typeAttribute)
                    {
                        case 1:
                            new SelectElement(this.firefox.FindElementById(attributeValue)).SelectByValue(value);
                            break;
                        case 2:
                            new SelectElement(this.firefox.FindElementByName(attributeValue)).SelectByValue(value);
                            break;
                        case 3:
                            new SelectElement(this.firefox.FindElementByXPath(attributeValue)).SelectByValue(value);
                            break;
                        case 4:
                            new SelectElement(this.firefox.FindElementByCssSelector(attributeValue)).SelectByValue(value);
                            break;
                    }
                    result = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.Select({typeAttribute},{attributeValue},{value})");
                }
            }
            return result;
        }
        public bool ScreenCapture(string imagePath, string fileName)
        {
            bool isSuccess = false;
            if (isAlive)
            {
                try
                {
                    Screenshot image = ((ITakesScreenshot)firefox).GetScreenshot();
                    image.SaveAsFile(imagePath + (imagePath.EndsWith(@"\") ? "" : @"\") + fileName + ".png");
                    isSuccess = true;
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"chrome.ScreenCapture({imagePath},{fileName})");
                }
            }
            return isSuccess;
        }
        public object ExecuteScript(string script)
        {
            if (isAlive)
            {
                try
                {
                    return firefox.ExecuteScript(script);
                }
                catch (Exception ex)
                {
                    ExportError(null, ex, $"firefox.ExecuteScript({script})");
                }
            }
            return "";
        }
        public static void ExportError(Chrome firefox, Exception ex, string error = "")
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

                if (firefox != null)
                {
                    string html = firefox.ExecuteScript("var markup = document.documentElement.innerHTML;return markup;").ToString();
                    firefox.ScreenCapture(@"log\images\", fileName);
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
    }
}
