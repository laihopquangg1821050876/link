using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace maxcare
{
    public class CommonIG
    {
        public static string GetUidFromCookie(string cookie)
        {
            try
            {
                return Regex.Match(cookie + ";", "ds_user_id=(.*?);").Groups[1].Value;
            }
            catch
            {
            }
            return "";
        }
    }
}
