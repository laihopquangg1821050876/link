using maxcare;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MCommon
{
    class CommonSQL
    {

        #region File
        public static bool CheckExitsFile(string name)
        {
            return Connector.Instance.ExecuteScalar($"SELECT COUNT(*) FROM files WHERE name='{name}' AND active=1;") > 0;
        }
        public static DataTable GetAllFilesFromDatabase(bool isShowAll = false)
        {
            DataTable data = new DataTable();
            try
            {
                string sql = "";
                if (!isShowAll)
                    sql = "select id, name from files where active=1";
                else
                    sql = "select id, name from files where active=1 UNION SELECT -1 AS id, '"+Language.GetValue("[Tất cả thư mục]") + "' AS name UNION SELECT 999999 AS id, '" + Language.GetValue("[Chọn nhiều thư mục]") + "' AS name ORDER BY id ASC";
                data = Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
            }
            return data;
        }
        public static DataTable GetAllFilesFromDatabaseForBin(bool isShowAll = false)
        {
            DataTable data = new DataTable();
            try
            {
                string sql = "";
                if (!isShowAll)
                    sql = "select id, name from files WHERE id IN (SELECT DISTINCT idfile FROM accounts WHERE active=0)";
                else
                    sql = "select id, name from files WHERE id IN (SELECT DISTINCT idfile FROM accounts WHERE active=0) UNION SELECT -1 AS id, '" + Language.GetValue("[Tất cả thư mục]") + "' AS name UNION SELECT 999999 AS id, '" + Language.GetValue("[Chọn nhiều thư mục]") + "' AS name ORDER BY id ASC";
                data = Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
            }
            return data;
        }
        public static bool InsertFileToDatabase(string namefile)
        {
            bool isSuccess = true;
            try
            {
                string sql = "insert into files values(null,'" + namefile + "','" + DateTime.Now.ToString() + "',1)";
                MCommon.Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        public static bool UpdateFileNameToDatabase(string idFile, string nameFile)
        {
            try
            {
                string sql = "UPDATE files SET name='" + nameFile + "' where id=" + idFile;
                return MCommon.Connector.Instance.ExecuteNonQuery(sql) > 0;
            }
            catch
            {
            }
            return false;
        }

        public static bool DeleteFileToDatabase(string idFile)
        {
            bool isSuccess = false;
            try
            {
                bool isCanRemove = Connector.Instance.ExecuteScalar("SELECT COUNT(idfile) FROM accounts WHERE idfile=" + idFile) == 0;

                if (isCanRemove)
                {
                    isSuccess = Connector.Instance.ExecuteNonQuery("delete from files where id=" + idFile) > 0;
                }
                else
                {
                    if (Connector.Instance.ExecuteNonQuery("UPDATE files SET active=0 where id=" + idFile) > 0)
                        isSuccess = DeleteAccountByIdFile(idFile);
                }
            }
            catch
            {
            }
            return isSuccess;
        }

        public static bool UpdateMultiField(string field, List<string> lstId_FieldValue, string table = "accounts")
        {
            List<string> lstId = new List<string>();
            string id = "", value = "";
            string data = "";
            for (int i = 0; i < lstId_FieldValue.Count; i++)
            {
                id = lstId_FieldValue[i].Split('|')[0];
                value = lstId_FieldValue[i].Split('|')[1];
                if (!string.IsNullOrEmpty(id))
                {
                    lstId.Add(id);
                    data += "WHEN '" + id + "' THEN '" + value + "' ";
                }
            }

            string sql = "UPDATE " + table + " " +
                                "SET " + field + " = CASE id " +
                                    data +
                                    "END " +
                            "WHERE id IN('" + string.Join("','", lstId) + "'); ";

            return Connector.Instance.ExecuteNonQuery(sql) > 0;
        }

        public static bool DeleteFileToDatabaseIfEmptyAccount()
        {
            bool isSuccess = false;
            try
            {
                isSuccess = Connector.Instance.ExecuteNonQuery("delete from files where id NOT IN (SELECT DISTINCT idfile FROM accounts)") > 0;
            }
            catch
            {
            }
            return isSuccess;
        }
        #endregion

        #region Account
        public static DataTable GetAllInfoFromAccount(List<string> lstIdFile, bool isGetActive = true)
        {
            DataTable dt = new DataTable();
            try
            {
                string where = "";
                if (lstIdFile == null || lstIdFile.Count == 0)
                    where = "where active=" + (isGetActive == true ? 1 : 0);
                else
                    where = "where idfile IN (" + string.Join(",", lstIdFile) + ") AND active=" + (isGetActive == true ? 1 : 0);
                string sql = "SELECT '-1' as id, '"+ Language.GetValue("[Tất cả tình trạng]") + "' AS name UNION select DISTINCT '0' as id,info from accounts " + where + " ORDER BY id ASC";
                dt = MCommon.Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
            }
            return dt;
        }
        public static bool InsertAccountToDatabase(string uid, string pass, string token, string cookie, string email, string phone, string name, string friends, string groups, string birthday, string gender, string info, string backup, string fa2, string idFile, string emaiRecovery = "", string passMail = "", string useragent = "", string proxy = "")
        {
            bool isSuccess = true;
            try
            {
                string sql = "INSERT INTO accounts(uid, pass,token,cookie1,email,phone,name,friends,groups,birthday,gender,info,fa2,backup,idfile,passmail,useragent,proxy,dateImport,active) VALUES('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}',1)";
                sql = string.Format(sql, uid, pass.Replace("'", "''"), token, cookie, email,phone, name, friends, groups, birthday, gender, info, fa2, backup, idFile, passMail, useragent, proxy, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                MCommon.Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }
        public static List<string> ConvertToSqlInsertAccount(List<string> lstSqlStatement)
        {
            List<string> lstQuery = new List<string>();
            try
            {
                int soLuongAccMoiLan = 100;
                int soLan = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((lstSqlStatement.Count) * 1.0 / soLuongAccMoiLan)));

                string query = "";
                for (int i = 0; i < soLan; i++)
                {
                    query = "INSERT INTO accounts(uid, pass,token,cookie1,email,phone,name,friends,groups,birthday,gender,info,fa2,idfile,passmail,useragent,proxy,dateImport,active, device) VALUES " + string.Join(",", lstSqlStatement.GetRange(soLuongAccMoiLan * i, soLuongAccMoiLan * i + soLuongAccMoiLan <= lstSqlStatement.Count ? soLuongAccMoiLan : lstSqlStatement.Count % soLuongAccMoiLan));
                    lstQuery.Add(query);
                }
            }
            catch
            {
            }
            return lstQuery;
        }
        public static string ConvertToSqlInsertAccount(string uid, string pass, string token, string cookie, string email,string phone, string name, string friends, string groups, string birthday, string gender, string info, string fa2, string idFile, string passMail, string useragent, string proxy, string ldIndex)
        {
            string sql = "";
            try
            {
                sql = "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}',1,'{18}')";
                sql = string.Format(sql, uid, pass.Replace("'", "''"), token, cookie, email, phone,name.Replace("'", "''"), friends, groups, birthday, gender, info, fa2, idFile, passMail, useragent, proxy, DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"), ldIndex);
            }
            catch
            {
            }
            return sql;
        }
        //public static bool UpdateAccountToDatabase(string id, string idFileTo, string uid = "", string pass = "", string token = "", string cookie = "", string email = "", string phone = "", string name = "", string friends = "", string groups = "", string birthday = "", string gender = "", string info = "", string backup = "", string fa2 = "", string mailRecovery = "", string passMailRecovery = "", bool isXoa2fa = false, bool isXoaPass = false, bool isXoaToken = false, bool isXoaCookie = false, bool isXoaMail = false, string ngaytao = "")
        //{
        //    bool isSuccess = true;
        //    try
        //    {
        //        List<string> listPara = new List<string>();
        //        listPara.Add(uid != "" ? "uid|" + uid : "");
        //        listPara.Add(phone != "" ? "phone|" + phone : "");
        //        listPara.Add(name != "" ? "name|" + name.Replace("'", "''") : "");
        //        listPara.Add(friends != "" ? "friends|" + friends : "");
        //        listPara.Add(groups != "" ? "groups|" + groups : "");
        //        listPara.Add(birthday != "" ? "birthday|" + birthday : "");
        //        listPara.Add(gender != "" ? "gender|" + gender : "");
        //        listPara.Add(info != "" ? "info|" + info : "");
        //        listPara.Add(backup != "" ? "backup|" + backup : "");
        //        listPara.Add(ngaytao != "" ? "dateCreateAcc|" + ngaytao : "");

        //        if (isXoa2fa)
        //            listPara.Add("fa2|" + fa2);
        //        else
        //            listPara.Add(fa2 != "" ? "fa2|" + fa2 : "");

        //        if (isXoaToken)
        //            listPara.Add("token|" + token);
        //        else
        //            listPara.Add(token != "" ? "token|" + token : "");

        //        if (isXoaCookie)
        //            listPara.Add("cookie1|" + cookie);
        //        else
        //            listPara.Add(cookie != "" ? "cookie1|" + cookie : "");

        //        if (isXoaPass)
        //            listPara.Add("pass|" + pass.Replace("'", "''"));
        //        else
        //            listPara.Add(pass != "" ? "pass|" + pass.Replace("'", "''") : "");

        //        if (isXoaMail)
        //        {
        //            listPara.Add("email|" + email);
        //            listPara.Add("passmail|" + passMailRecovery);
        //        }
        //        else
        //        {
        //            listPara.Add(email != "" ? "email|" + email : "");
        //            listPara.Add(passMailRecovery != "" ? "passmail|" + passMailRecovery : "");
        //        }

        //        listPara.Add(idFileTo != "" ? "idfile|" + idFileTo : "");
        //        string sql = "update accounts set";
        //        string temp = "";
        //        foreach (string item in listPara)
        //        {
        //            if (item != "")
        //                temp += " " + item.Split('|')[0] + "='" + item.Split('|')[1] + "',";
        //        }
        //        temp = temp.TrimEnd(',');
        //        if (temp.Trim() != "")
        //        {
        //            sql += temp + " where id=" + id;
        //            if (MCommon.Connector.Instance.ExecuteNonQuery(sql) > 0)
        //                isSuccess = true;
        //            else
        //                isSuccess = false;
        //        }
        //    }
        //    catch
        //    {
        //    }

        //    return isSuccess;
        //}

        public static bool UpdateFieldToAccount(string id, string fieldName, string fieldValue)
        {
            bool isSuccess = false;

            try
            {
                string sql = $"update accounts set {fieldName} = '{fieldValue.Replace("'", "''")}' where id=" + id;
                if (Connector.Instance.ExecuteNonQuery(sql) > 0)
                    isSuccess = true;
                else
                    isSuccess = false;
            }
            catch
            {
            }

            return isSuccess;
        }
        public static bool UpdateMultiFieldToAccount(string id, string lstFieldName, string lstFieldValue, bool isAllowEmptyValue = true)
        {
            bool isSuccess = false;

            try
            {
                if (lstFieldName.Split('|').Length == lstFieldValue.Split('|').Length)
                {
                    int dem = lstFieldName.Split('|').Length;
                    string set = "";
                    for (int i = 0; i < dem; i++)
                    {
                        if (!isAllowEmptyValue && lstFieldValue.Split('|')[i].Trim() == "")
                            continue;
                        set += lstFieldName.Split('|')[i] + "='" + lstFieldValue.Split('|')[i].Replace("'", "''") + "',";
                    }

                    set = set.TrimEnd(',');
                    string sql = $"update accounts set {set} where id=" + id;
                    isSuccess = MCommon.Connector.Instance.ExecuteNonQuery(sql) > 0;
                }
            }
            catch
            {
            }

            return isSuccess;
        }
        public static bool UpdateMultiFieldToAccount(List<string> lstId, string lstFieldName, string lstFieldValue)
        {
            bool isSuccess = false;

            try
            {
                if (lstFieldName.Split('|').Length == lstFieldValue.Split('|').Length)
                {
                    int dem = lstFieldName.Split('|').Length;
                    string set = "";
                    for (int i = 0; i < dem; i++)
                        set += lstFieldName.Split('|')[i] + "='" + lstFieldValue.Split('|')[i].Replace("'", "''") + "',";
                    set = set.TrimEnd(',');

                    int soLuongAccMoiLan = 100;
                    int soLan = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((lstId.Count) * 1.0 / soLuongAccMoiLan)));

                    List<string> lstQuery = new List<string>();
                    string query = "";
                    for (int i = 0; i < soLan; i++)
                    {
                        query = $"update accounts set {set} where id IN (" + string.Join(",", lstId.GetRange(soLuongAccMoiLan * i, soLuongAccMoiLan * i + soLuongAccMoiLan <= lstId.Count ? soLuongAccMoiLan : lstId.Count % soLuongAccMoiLan)) + ")";
                        lstQuery.Add(query);
                    }

                    if (MCommon.Connector.Instance.ExecuteNonQuery(lstQuery) > 0)
                        isSuccess = true;
                    else
                        isSuccess = false;
                }
            }
            catch
            {
            }

            return isSuccess;
        }
        public static bool UpdateFieldToFile(string idFile, string fieldName, string fieldValue)
        {
            bool isSuccess = false;

            try
            {
                string sql = $"update files set {fieldName} = '{fieldValue.Replace("'", "''")}' where id=" + idFile;
                if (MCommon.Connector.Instance.ExecuteNonQuery(sql) > 0)
                    isSuccess = true;
                else
                    isSuccess = false;
            }
            catch
            {
            }

            return isSuccess;
        }
        public static bool UpdateFieldToAccount(List<string> lstId, string fieldName, string fieldValue)
        {
            bool isSuccess = false;

            try
            {
                int soLuongAccMoiLan = 100;
                int soLan = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((lstId.Count) * 1.0 / soLuongAccMoiLan)));

                List<string> lstQuery = new List<string>();
                string query = "";
                for (int i = 0; i < soLan; i++)
                {
                    query = $"update accounts set {fieldName} = '{fieldValue.Replace("'", "''")}' where id IN (" + string.Join(",", lstId.GetRange(soLuongAccMoiLan * i, soLuongAccMoiLan * i + soLuongAccMoiLan <= lstId.Count ? soLuongAccMoiLan : lstId.Count % soLuongAccMoiLan)) + ")";
                    lstQuery.Add(query);
                }

                if (MCommon.Connector.Instance.ExecuteNonQuery(lstQuery) > 0)
                    isSuccess = true;
                else
                    isSuccess = false;
            }
            catch
            {
            }

            return isSuccess;
        }
        public static bool UpdateFieldToFile(List<string> lstId, string fieldName, string fieldValue)
        {
            bool isSuccess = true;
            try
            {
                string sql = $"update files set {fieldName} = '{fieldValue}' where id IN (" + string.Join(",", lstId) + ")";
                if (MCommon.Connector.Instance.ExecuteNonQuery(sql) > 0)
                    isSuccess = true;
                else
                    isSuccess = false;

            }
            catch
            {
            }
            return isSuccess;
        }

        public static DataTable GetAccFromFile(List<string> lstIdFile = null, string info = "", bool isGetActive = true)
        {
            DataTable dt = new DataTable();
            try
            {
                string where = "WHERE ";
                string where_idFile = (lstIdFile != null && lstIdFile.Count > 0) ? "t1.idFile IN (" + string.Join(",", lstIdFile) + ")" : "";
                if (where_idFile != "")
                    where += where_idFile + " AND ";
                string where_info = (info != "") ? $"t1.info = '{info}'" : "";
                if (where_info != "")
                    where += where_info + " AND ";
                string where_active = $"t1.active = '{(isGetActive == true ? 1 : 0)}'";
                where += where_active;

                string sql = "SELECT t1.*, t2.name AS nameFile FROM accounts t1 JOIN files t2 ON t1.idfile=t2.id " + where + " ORDER BY t1.idfile";
                dt = MCommon.Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
            }
            return dt;
        }

        public static DataTable GetAccFromUid(List<string> lstUid)
        {
            DataTable dt = new DataTable();

            try
            {
                int soLuongAccMoiLan = 100;
                int soLan = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((lstUid.Count) * 1.0 / soLuongAccMoiLan)));

                List<string> lstQuery = new List<string>();
                string query = "";
                for (int i = 0; i < soLan; i++)
                {
                    query = "SELECT t1.*, t2.name AS nameFile FROM accounts t1 JOIN files t2 ON t1.idfile=t2.id WHERE t1.uid IN ('" + string.Join("','", lstUid.GetRange(soLuongAccMoiLan * i, soLuongAccMoiLan * i + soLuongAccMoiLan <= lstUid.Count ? soLuongAccMoiLan : lstUid.Count % soLuongAccMoiLan)) + "') and t1.active=1 ORDER BY t1.uid";
                    lstQuery.Add(query);
                }

                dt = MCommon.Connector.Instance.ExecuteQuery(lstQuery);
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "GetAccFromFile");
            }
            return dt;
        }



        public static DataTable GetAllAccountFromDatabase(string field)
        {
            DataTable data = new DataTable();
            try
            {
                string sql = $"select "+ field + " from accounts where active = 1";
                data = Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
            }
            return data;
        }
        public static bool DeleteAccountByIdFile(string idFile)
        {
            bool isSuccess = true;
            try
            {
                //if (MCommon.Connector.Instance.ExecuteNonQuery("delete from accounts where idfile=" + idFile) > 0)
                if (MCommon.Connector.Instance.ExecuteNonQuery("UPDATE accounts SET active=0, dateDelete='" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") + "' where idfile=" + idFile) > 0)
                    isSuccess = true;
                else
                    isSuccess = false;
            }
            catch
            {
            }

            return isSuccess;
        }
        public static bool DeleteAccountToDatabase(string id)
        {

            //if (MCommon.Connector.Instance.ExecuteNonQuery("delete from accounts where id=" + id) > 0)
            try
            {
                return MCommon.Connector.Instance.ExecuteNonQuery("UPDATE accounts SET active=0, dateDelete='" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") + "' where id=" + id) > 0;
            }
            catch
            {
            }
            return false;
        }

        public static DataTable GetAccFromId(List<string> lstId)
        {
            DataTable dt = new DataTable();

            try
            {
                int soLuongAccMoiLan = 100;
                int soLan = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal(lstId.Count * 1.0 / soLuongAccMoiLan)));

                List<string> lstQuery = new List<string>();
                string query = "";
                for (int i = 0; i < soLan; i++)
                {
                    query = "SELECT uid, pass, token, cookie1,email, passmail, fa2 FROM accounts WHERE id IN ('" + string.Join("','", lstId.GetRange(soLuongAccMoiLan * i, soLuongAccMoiLan * i + soLuongAccMoiLan <= lstId.Count ? soLuongAccMoiLan : lstId.Count % soLuongAccMoiLan)) + "')";
                    lstQuery.Add(query);
                }

                dt = MCommon.Connector.Instance.ExecuteQuery(lstQuery);
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "GetAccFromFile");
            }
            return dt;
        }

        public static bool DeleteAccountToDatabase(List<string> lstId, bool isReallyDelete = false)
        {
            //Ghi ra file
            if (isReallyDelete)
            {
                List<string> lst = new List<string>();
                DataTable dt = GetAccFromId(lstId);
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string text = "";
                    for (int j = 0; j < dt.Columns.Count; j++)
                        text += dt.Rows[i][j].ToString() + "|";
                    text = text.Substring(0, text.Length - 1);
                    lst.Add(text);
                }

                File.AppendAllText("bin.txt", "======" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") + "======\r\n");
                File.AppendAllLines("bin.txt", lst);
            }

            bool isSuccess = true;

            try
            {
                int soLuongAccXoaMoiLan = 100;
                int soLan = Convert.ToInt32(Math.Ceiling(Convert.ToDecimal((lstId.Count) * 1.0 / soLuongAccXoaMoiLan)));

                List<string> lstQuery = new List<string>();
                string query = "";
                for (int i = 0; i < soLan; i++)
                {
                    if (isReallyDelete)
                        query = "delete from accounts where id IN (" + string.Join(",", lstId.GetRange(soLuongAccXoaMoiLan * i, soLuongAccXoaMoiLan * i + soLuongAccXoaMoiLan <= lstId.Count ? soLuongAccXoaMoiLan : lstId.Count % soLuongAccXoaMoiLan)) + ")";
                    else
                        query = "UPDATE accounts SET active=0, dateDelete='" + DateTime.Now.ToString("HH:mm:ss dd/MM/yyyy") + "' where id IN (" + string.Join(",", lstId.GetRange(soLuongAccXoaMoiLan * i, soLuongAccXoaMoiLan * i + soLuongAccXoaMoiLan <= lstId.Count ? soLuongAccXoaMoiLan : lstId.Count % soLuongAccXoaMoiLan)) + ")";
                    lstQuery.Add(query);
                }

                for (int i = 0; i < lstQuery.Count; i++)
                    isSuccess = MCommon.Connector.Instance.ExecuteNonQuery(lstQuery[i]) > 0;
            }
            catch (Exception ex)
            {
                MCommon.Common.ExportError(null, ex, "DeleteAccountToDatabase");
            }
            return isSuccess;
        }
        public static bool UpdateAccountByUid(string account)
        {
            string[] arrPara = account.Split('|');
            string uid = "", pass = "", token = "", cookie = "", email = "", passmail = "", fa2 = "", useragent = "", proxy = "";
            uid = arrPara[0];
            if (uid.Trim() == "")
                return false;
            pass = arrPara[1];
            token = arrPara[2];
            cookie = arrPara[3];
            email = arrPara[4];
            passmail = arrPara[5];
            fa2 = arrPara[6];
            useragent = arrPara[7];
            proxy = arrPara[8];

            List<string> listPara = new List<string>();
            listPara.Add(pass != "" ? "pass|" + pass : "");
            listPara.Add(token != "" ? "token|" + token : "");
            listPara.Add(cookie != "" ? "cookie1|" + cookie : "");
            listPara.Add(email != "" ? "email|" + email : "");
            listPara.Add(passmail != "" ? "passmail|" + passmail : "");
            listPara.Add(fa2 != "" ? "fa2|" + fa2 : "");
            listPara.Add(useragent != "" ? "useragent|" + useragent : "");
            listPara.Add(proxy != "" ? "proxy|" + proxy : "");

            string sql = "update accounts set";
            foreach (string item in listPara)
            {
                if (item != "")
                {
                    sql += " " + item.Split('|')[0] + "='" + item.Split('|')[1] + "',";
                }
            }
            sql = sql.TrimEnd(',');
            sql += " where uid='" + uid + "'";
            return MCommon.Connector.Instance.ExecuteNonQuery(sql) > 0;
        }
        #endregion

        #region Other
        public static string GetIdFileFromIdAccount(string id)
        {
            try
            {
                return Connector.Instance.ExecuteScalar($"SELECT idFile FROM accounts WHERE id='{id}'").ToString();
            }
            catch
            {
            }
            return "";
        }
        public static bool CheckColumnIsExistInTable(string table, string column)
        {
            return Connector.Instance.ExecuteScalar($"SELECT COUNT(*) AS count FROM pragma_table_info('{table}') WHERE name='{column}'") > 0;
        }
        public static bool CheckExistTable(string table)
        {
            return Connector.Instance.ExecuteScalar($"SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name='{table}';") > 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="table"></param>
        /// <param name="columnName"></param>
        /// <param name="typeColumnData">0-INT, 1-TEXT</param>
        /// <returns></returns>
        public static bool AddColumnsIntoTable(string table, string columnName, int typeColumnData)
        {
            bool isHave = false;
            try
            {
                if (Connector.Instance.ExecuteNonQuery($"ALTER TABLE {table} ADD COLUMN '{columnName}' {(typeColumnData == 0 ? "INT" : "TEXT")};") > 0)
                    isHave = true;
            }
            catch
            {
            }
            return isHave;
        }
        #endregion


        public static bool InsertInteractToDatabase(string uid, string hanhDong, string cauHinh)
        {
            bool isSuccess = true;
            try
            {
                string sql = $"INSERT INTO interacts(uid, timeInteract,hanhDong,cauHinh) VALUES ('{uid}','{DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss")}','{hanhDong}','{cauHinh}')";
                MCommon.Connector.Instance.ExecuteQuery(sql);
            }
            catch
            {
                isSuccess = false;
            }
            return isSuccess;
        }
    }
}
