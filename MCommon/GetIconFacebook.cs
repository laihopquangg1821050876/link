using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace maxcare
{
    class GetIconFacebook
    {
        static string icon1 = "🙂|😀|😄|😆|😅|😂|🤣|😊|😌|😉|😏|😍|😘|😗|😙|😚|🤗|😳|🙃|😇|😈|😛|😝|😜|😋|🤤|🤓|😎|🤑|😒|🙁|☹️|😞|😔|😖|😓|😢|😢|😭|😟|😣|😩|😫|😕|🤔|🙄|😤|😠|😡|😶|🤐|😐|😑|😯|😲|😧|😨|😰|😱|😪|😴|😬|🤥|🤧|🤒|😷|🤕|😵|🤢|🤠|🤡|👿|👹|👺|👻|💀|👽|👾|🤖|💩|🎃";
        static string icon2 = "♥️|❤️|💛|💚|💙|💜|🖤|💖|💝|💔|❣️|💕|💞|💓|💗|💘|💟|💌|💋|👄|💄|💍|📿|🎁|👙|👗|👚|👕|👘|🎽|👘|👖|👠|👡|👢|👟|👞|👒|🎩|🎓|👑|⛑️|👓|🕶️|🌂|👛|👝|👜|💼|🎒|🛍️|🛒|🎭|🎦|🎨|🤹|🎊|🎉|🎈|🎧|🎷|🎺|🎸|🎻|🥁|🎹|🎤|🎵|🎶|🎼|⚽|🏀|🏈|⚾|🏐|🏉|🎱|🎾|🏸|🏓|🏏|🏑|🏒|🥅|⛸️|🎿|🥊|🥋|⛳|🎳|🏹|🎣|🎯|🚵|🎖️|🏅|🥇|🥈|🥉|🏆";
        static string icon3 = "🍏|🍎|🍐|🍊|🍋|🍌|🍉|🍇|🍓|🍈|🥝|🥑|🍍|🍒|🍑|🍆|🥒|🥕|🌶|🌽|🍅|🥔|🍠|🌰|🥜|🍯|🥐|🍞|🥖|🧀|🥚|🍳|🥓|🍤|🍗|🍖|🍕|🌭|🍔|🍟|🥙|🌮|🌯|🥗|🥘|🍝|🍜|🍲|🍣|🍱|🍛|🍚|🍙|🍘|🍢|🍡|🍧|🍨|🍦|🥞|🍰|🎂|🍮|🍭|🍥|🍬|🍫|🍿|🍩|🍪|🍼|🥛|☕|🍵|🍶|🍺|🍻|🥂|🍷|🥃|🍸|🍹|🍾|🥄|🍴|🍽";
        static string icon4 = "😺|😸|😹|😻|😼|😽|🙀|😿|😾|🐱|🐶|🐰|🐭|🐹|🦊|🐻|🐼|🐨|🐯|🦁|🐮|🐗|🐷|🐽|🐸|🐵|🙈|🙉|🙊|🦍|🐺|🐑|🐐|🐏|🐴|🦄|🦌|🦏|🦅|🐤|🐣|🐥|🐔|🐓|🦃|🐦|🦆|🦇|🦉|🕊️|🐧|🐕|🐩|🐈|🐇|🐁|🐀|🐿|🐒|🐖|🐆|🐅|🐃|🐂|🐄|🐎|🐪|🐫|🐘|🐊|🐢|🐠|🐟|🐡|🐬|🦈|🐳|🐋|🦑|🐙|🦐|🐚|🦀|🦂|🦎|🐍|🐛|🐜|🕷️|🕸️|🐞|🦋|🐝|🐌|🐲|🐉|🐾";
        static string icon5 = "🌼|🌸|🌺|🏵️|🌻|🌷|🌹|🥀|💐|🌾|🎋|☘|🍀|🍃|🍂|🍁|🌱|🌿|🎍|🌵|🌴|🌳|🌳|🎄|🍄|🌎|🌍|🌏|🌜|🌛|🌕|🌖|🌗|🌘|🌑|🌒|🌓|🌔|🌚|🌝|🌙|💫|⭐|🌟|✨|⚡|🔥|💥|☄️|🌞|☀️|🌤️|⛅|🌥️|🌦️|☁️|🌧️|⛈️|🌩️|🌨️|🌈|💧|💦|☂️|☔|🌊|🌫|🌪|💨|❄|🌬|⛄|☃️";
        static string icon6 = "🚗|🚕|🚙|🚌|🚎|🏎|🚓|🚑|🚒|🚐|🚚|🚛|🚜|🛴|🚲|🛵|🏍|🚘|🚖|🚍|🚔|🚨|💺|✈|🛫|🛬|🛩|🚁|🚀|🛰|🚡|🚠|🚟|🚃|🚋|🚞|🚝|🚄|🚅|🚈|🚂|🚆|🚊|🚇|🚉|🛶|⛵|🛥|🚤|🚢|⛴|🛳|⚓|🚧|⛽|🚏|🚦|🚥|🛣|🛤|🏗|🏭|🏠|🏡|🏘|🏚|🏢|🏬|🏤|🏣|🏥|🏦|🏪|🏫|🏨|🏩|🏛|🏰|🏯|🏟️|⛪|💒|🕌|🕍|🕋|⛩|🗼|🗿|🗽|🗺|🎪|🎠|🎡|🎢|⛲|⛱|🏖|🏝|🏕|⛺|🗾|⛰|🏔|🗻|🌋|🏞|🏜|🌅|🌄|🎑|🌠|🎇|🎆|🏙|🌇|🌆|🌃|🌌|🌉|🌁";
        static string icon7 = "📱|📲|💻|🖥|⌨|🖨|🖱|🖲|🕹|🎮|💽|💾|💿|📀|📼|📷|📸|📹|🎥|📽|🎞|🎬|📞|☎|📟|📠|📺|📻|🎙|🎚|🎛|📡|📢|📣|🔔|💡|🕯|🔦|🔋|🔌|⌚|⏱|⏲|⏰|🕰|⌛|⏳|🔮|💎|🎲|🎰|💸|💵|💴|💶|💷|💰|💳|💲|💱|⚖|🔫|💣|🔪|🗡|⚔|🛡|🚬|⚰|⚱|🗜️|🔧|🔨|⚒|🛠|⛏|🔩|⚙|⛓|💈|🌡|💊|💉|⚗|🔬|🔭|🚿|🛁|🚽|🛎|🔑|🗝|🚪|🛋|🛏|🖼|🏺|🗑|🛢|🕳|🏮|🎏|🎎|🎐|🎫|🎟️|🎀|🎗️|📯|✉|📩|📨|📧|📦|📪|📫|📬|📭|📮|📥|📤|📜|📃|📄|📑|📊|📈|📉|🗒|📅|📆|🗓|📇|🗃|🗳|🗄|📋|📁|📂|🗂|📓|📔|📒|📕|📗|📘|📙|📚|📖|🗞|📰|📝|✏|🖊|🖍|🖌|🖋|✒|📌|📍|📎|🖇|🔖|🏷|🔗|🔍|🔎|📐|📏|✂|🔒|🔓|🔏|🔐";
        static string icon8 = "🙂|😀|😄|😆|😅|😂|🤣|😊|😌|😉|😍|😘|😗|😙|😚|🤗|😳|🙃|😛|😝|😜|😋|🤤|🤓|😎";

        //số
        static string number = "0️⃣|1️⃣|2️⃣|3️⃣|4️⃣|5️⃣|6️⃣|7️⃣|8️⃣|9️⃣";

        private static string GetNumber(string input)
        {
            string output = "";
            try
            {
                string temp = "";
                List<string> lstNumber = number.Split('|').ToList();
                for (int i = 0; i < input.Length; i++)
                {
                    temp = input[i].ToString();
                    if (MCommon.Common.IsNumber(temp))
                        temp = lstNumber[Convert.ToInt32(temp)];
                    output += temp;
                }
            }
            catch
            {
            }
            return output;
        }
        private static string GetIcon(string type, Random rd)
        {
            string icon = "";
            List<string> lst = new List<string>();
            try
            {
                switch (type)
                {
                    case "[r1]":
                        lst = icon1.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[r2]":
                        lst = icon2.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[r3]":
                        lst = icon3.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[r4]":
                        lst = icon4.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[r5]":
                        lst = icon5.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[r6]":
                        lst = icon6.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[r7]":
                        lst = icon7.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[r8]":
                        lst = icon8.Split('|').ToList();
                        icon = lst[rd.Next(0, lst.Count)];
                        break;
                    case "[d]":
                        icon = DateTime.Now.ToString("dd/MM/yyyy");
                        break;
                    case "[t]":
                        icon = DateTime.Now.ToString("HH:mm:ss");
                        break;
                    default:
                        break;
                }
            }
            catch
            {
            }
            return icon;
        }

        static List<string> lstKey = new List<string>()
        {
            "[r1]",
            "[r2]",
            "[r3]",
            "[r4]",
            "[r5]",
            "[r6]",
            "[r7]",
            "[r8]",
            "[d]",
            "[t]"
        };

        public static string ProcessString(string input, Random rd)
        {
            string output = "";
            try
            {
                string type = "";
                for (int j = 0; j < lstKey.Count; j++)
                {
                    type = lstKey[j];
                    if (input.Contains(type))
                    {
                        var temp = input.Split(new string[] { type }, StringSplitOptions.None).ToList();
                        for (int i = 0; i < temp.Count - 1; i++)
                        {
                            output += temp[i] + GetIconFacebook.GetIcon(type, rd);
                        }
                        output += temp[temp.Count - 1];
                        input = output;
                        output = "";
                    }
                }

                //Xử lý chuỗi số
                MatchCollection collection = Regex.Matches(input, @"\[n(.*?)\]");
                for (int j = 0; j < collection.Count; j++)
                {
                    var temp = input.Split(new string[] { collection[j].Value }, StringSplitOptions.None).ToList();
                    for (int i = 0; i < temp.Count - 1; i++)
                    {
                        output += temp[i] + MCommon.Common.CreateRandomNumber(Convert.ToInt32(collection[j].Groups[1].Value), rd);
                    }
                    output += temp[temp.Count - 1];
                    input = output;
                    output = "";
                }

                //Xử lý chuỗi
                collection = Regex.Matches(input, @"\[s(.*?)\]");
                for (int j = 0; j < collection.Count; j++)
                {
                    var temp = input.Split(new string[] { collection[j].Value }, StringSplitOptions.None).ToList();
                    for (int i = 0; i < temp.Count - 1; i++)
                    {
                        output += temp[i] + MCommon.Common.CreateRandomString(Convert.ToInt32(collection[j].Groups[1].Value), rd);
                    }
                    output += temp[temp.Count - 1];
                    input = output;
                    output = "";
                }

                //Thay dãy số
                collection = Regex.Matches(input, @"\[q(.*?)\]");
                for (int j = 0; j < collection.Count; j++)
                {
                    var temp = input.Split(new string[] { collection[j].Value }, StringSplitOptions.None).ToList();
                    for (int i = 0; i < temp.Count - 1; i++)
                    {
                        output += temp[i] + GetNumber(collection[j].Groups[1].Value);
                    }
                    output += temp[temp.Count - 1];
                    input = output;
                    output = "";
                }
            }
            catch
            {
            }
            return input;
        }
    }
}
