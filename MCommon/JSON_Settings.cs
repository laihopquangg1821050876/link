using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCommon
{
    class JSON_Settings
    {
        private string PathFileSetting;
        JObject json;

        public JSON_Settings(string jsonStringOrPathFile, bool isJsonString=false)
        {
            if (isJsonString)
            {
                if (jsonStringOrPathFile.Trim() == "")
                    jsonStringOrPathFile = "{}";
                json = JObject.Parse(jsonStringOrPathFile);
            }
            else
            {
                try
                {
                    if(jsonStringOrPathFile.Contains("\\")|| jsonStringOrPathFile.Contains("/"))
                        this.PathFileSetting = jsonStringOrPathFile;
                    else
                        this.PathFileSetting = "settings\\" + jsonStringOrPathFile + ".json";

                    if (!File.Exists(PathFileSetting))
                        using (StreamWriter w = File.AppendText(PathFileSetting)) { }

                    json = JObject.Parse(File.ReadAllText(PathFileSetting));
                }
                catch
                {
                    json = new JObject();
                }
            }
        }
        public static Dictionary<string, object> ConvertToDictionary(JObject jObject)
        {
            Dictionary<string, object> dic = new Dictionary<string, object>();
            try
            {
                dic = jObject.ToObject<Dictionary<string, object>>();

                var JObjectKeys = (from r in dic
                                   let key = r.Key
                                   let value = r.Value
                                   where value.GetType() == typeof(JObject)
                                   select key).ToList();

                var JArrayKeys = (from r in dic
                                  let key = r.Key
                                  let value = r.Value
                                  where value.GetType() == typeof(JArray)
                                  select key).ToList();

                JArrayKeys.ForEach(key => dic[key] = ((JArray)dic[key]).Values().Select(x => ((JValue)x).Value).ToArray());
                JObjectKeys.ForEach(key => dic[key] = ConvertToDictionary(dic[key] as JObject));
            }
            catch
            {
            }

            return dic;
        }
        public JSON_Settings()
        {
            json = new JObject();
        }
        public string GetValue(string key, string valueDefault = "")
        {
            string output = valueDefault;
            try
            {
                output = json[key] == null ? valueDefault : json[key].ToString();
            }
            catch
            { }
            return output;
        }
        ///// <summary>
        ///// 
        ///// </summary>
        ///// <param name="key"></param>
        ///// <param name="splitString"></param>
        ///// <returns></returns>
        //public List<string> GetValueList(string key, string splitString="\n")
        //{
        //    List<string> output = new List<string>();
        //    try
        //    {
        //        output = GetValue(key).Split(new string[] { splitString }, StringSplitOptions.RemoveEmptyEntries).ToList();
        //        output = MCommon.Common.RemoveEmptyItems(output);
        //    }
        //    catch
        //    { }
        //    return output;
        //}
        public List<string> GetValueList(string key, int typeSplitString=0)
        {
            List<string> output = new List<string>();
            try
            {
                if (typeSplitString == 0)
                    output = GetValue(key).Split('\n').ToList();
                else
                    output = GetValue(key).Split(new string[] { "\n|\n" }, StringSplitOptions.RemoveEmptyEntries).ToList();

                output = MCommon.Common.RemoveEmptyItems(output);
            }
            catch
            { }
            return output;
        }
        public int GetValueInt(string key, int valueDefault = 0)
        {
            int output = valueDefault;
            try
            {
                output = json[key] == null ? valueDefault : Convert.ToInt32(json[key].ToString());
            }
            catch
            { }
            return output;
        }
        public bool GetValueBool(string key, bool valueDefault = false)
        {
            bool output = valueDefault;
            try
            {
                output = json[key] == null ? valueDefault : Convert.ToBoolean(json[key].ToString());
            }
            catch
            { }
            return output;
        }
        public void Add(string key, string value)
        {
            try
            {
                if (!json.ContainsKey(key))
                    json.Add(key, value);
                else
                    json[key] = value;
            }
            catch (Exception ex)
            { }
        }
        public void Update(string key, object value)
        {
            try
            {
                json[key] = value.ToString();
            }
            catch
            { }
        }
        public void Update(string key, List<string> lst)
        {
            try
            {
                bool isCheckMultiRow = false;
                foreach (var item in lst)
                {
                    if (item.Contains("\n"))
                    {
                        isCheckMultiRow = true;
                        break;
                    }
                }
                if (isCheckMultiRow)
                    json[key] = string.Join("\n|\n", lst).ToString();
                else
                    json[key] = string.Join("\n", lst).ToString();
            }
            catch
            { }
        }
        public void Remove(string key)
        {
            try
            {
                json.Remove(key);
            }
            catch
            { }
        }
        public void Save(string pathFileSetting = "")
        {
            try
            {
                if (pathFileSetting == "")
                    pathFileSetting = PathFileSetting;
                File.WriteAllText(pathFileSetting, json.ToString());
            }
            catch
            {
            }
        }
        public string GetFullString()
        {
            string output = "";
            try
            {
                output = json.ToString().Replace("\r\n", "");
            }
            catch (Exception ex)
            {
            }
            return output;
        }
    }
}
