using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.Xml;

namespace EntityGenerator.SystemSetting
{
    class ToolSetting
    {
        //以下代表了用户设置项.
        private static string _language;
        private static string _postfix;
        private static string[] _references;
        private static string[] _dalreferences;
        private static string[] _idalreferences;
        private static int _tabsize;
        private static string _savePath;
        private static string _serviceName;
        private static string _userName;
        private static string _userPassWord;
        private static DataTable _sqlDataMapping;
        private static DataTable _accessDataMapping;
        private static DataTable _oracleDataMapping;

        //以下变量存储数据映射信息,以提高访问速度.
        private static Hashtable _sqlDataMappingCache = new Hashtable();
        private static Hashtable _accessDataMappingCache = new Hashtable();
        private static Hashtable _oracleDataMappingCache = new Hashtable();

        /// <summary>
        /// 本方法用于加载用户设置．
        /// </summary>
        /// <returns>是否成功加载</returns>
        public static bool LoadUserSettings()
        {
            bool success = false;
            XmlDocument settings = new XmlDocument();
            try
            {
                settings.Load("UserSetting.xml");
                ToolSetting._language = settings.SelectSingleNode("/Setting/Language").InnerText;
                ToolSetting._postfix = settings.SelectSingleNode("/Setting/Postfix").InnerText;
                ToolSetting._tabsize = Convert.ToInt32(settings.SelectSingleNode("/Setting/TabSize").InnerText);
                ToolSetting._savePath = settings.SelectSingleNode("/Setting/SavePath").InnerText;
                ToolSetting._serviceName = settings.SelectSingleNode("/Setting/ServiceName").InnerText;
                ToolSetting._userName = settings.SelectSingleNode("/Setting/UserName").InnerText;
                ToolSetting._userPassWord = settings.SelectSingleNode("/Setting/UserPassWord").InnerText;
                ToolSetting._references = ToolSetting.GetReferences(settings);
                ToolSetting._dalreferences = ToolSetting.GetDALReferences(settings);
                ToolSetting._idalreferences = ToolSetting.GetIDALReferences(settings);
                ToolSetting._sqlDataMapping = ToolSetting.GetDataTypeMapping(settings, "SqlServer");
                ToolSetting._accessDataMapping = ToolSetting.GetDataTypeMapping(settings, "Access");
                ToolSetting._oracleDataMapping = ToolSetting.GetDataTypeMapping(settings, "Oracle");
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n加载配置文件失败,应用程序将退出!");
                Application.Exit();
            }
            return success;
        }

        /// <summary>
        /// 本方法用于保存用户设置．
        /// </summary>
        /// <returns>是否保存成功</returns>
        public static bool SaveUserSetting()
        {
            bool succ = false;
            XmlDocument settings = new XmlDocument();
            try
            {
                settings.Load("UserSetting.xml");
                settings.SelectSingleNode("/Setting/Language").InnerText = ToolSetting.Language;
                settings.SelectSingleNode("/Setting/Postfix").InnerText = ToolSetting._postfix;
                settings.SelectSingleNode("/Setting/TabSize").InnerText = ToolSetting._tabsize.ToString();
                settings.SelectSingleNode("/Setting/SavePath").InnerText = ToolSetting._savePath;
                settings.SelectSingleNode("/Setting/ServiceName").InnerText = ToolSetting._serviceName;
                settings.SelectSingleNode("/Setting/UserName").InnerText = ToolSetting._userName;
                settings.SelectSingleNode("/Setting/UserPassWord").InnerText = ToolSetting._userPassWord;
                ToolSetting.SaveReferenceList(settings);
                ToolSetting.SaveDataTypeMappingInfo(settings, "SqlServer");
                ToolSetting.SaveDataTypeMappingInfo(settings, "Access");
                ToolSetting.SaveDataTypeMappingInfo(settings, "Oracle");
                settings.Save("UserSetting.xml");
                succ = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return succ;
        }

        /// <summary>
        /// 得到数据库数据类型所对应的代码数据类型;
        /// 若没有,则返回"[Unknown]";
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        /// <param name="dbDataType">数据类型</param>
        /// <returns>对应的代码数据类型</returns>
        public static string GetMapping(DbType dbType, string dbDataType)
        {
            //确定所操作的数据源.
            DataTable data = null;
            Hashtable dataCache = null;
            if (dbType == DbType.SqlServer)
            {
                data = ToolSetting._sqlDataMapping;
                dataCache = ToolSetting._sqlDataMappingCache;
            }
            else
            {
                data = ToolSetting._oracleDataMapping;
                dataCache = ToolSetting._oracleDataMappingCache;
            }

            //检索缓存中是否有所需数据.
            Object codeType = dataCache[dbDataType];
            if (codeType != null)
            {
                return codeType.ToString();
            }

            //检索数据表格中是否有所需的数据.
            for (int i = 0; i < data.Rows.Count; i++)
            {
                string temp = data.Rows[i][0].ToString();
                if (dbDataType.Equals(temp))
                {
                    dataCache.Add(temp, data.Rows[i][1].ToString());
                    return data.Rows[i][1].ToString();
                }
            }

            //若以上两次都没有检索到数据,则返回"[Unknown]";
            return "[Unknown]";
        }

        /// <summary>
        /// 去掉字符串数组中的所有的字符串的两边的空字符,以及确保每个字符串以';'结束.
        /// </summary>
        /// <param name="strs">待处理字符串数组</param>
        /// <returns>已处理字符串数组</returns>
        public static string[] FormatStringArray(string[] strs)
        {
            string[] strArr = new string[strs.Length];
            string temp = null;
            for (int i = 0; i < strs.Length; i++)
            {
                temp = strs[i].Trim();
                if (!temp.EndsWith(";"))
                {
                    strArr[i] = temp + ";";
                }
                else
                {
                    strArr[i] = temp;
                }
            }
            return strArr;
        }

        /// <summary>                            
        /// 获取或设置实体类的语言.
        /// </summary>
        public static string Language
        {
            get
            {
                return ToolSetting._language;
            }
            set
            {
                ToolSetting._language = value;
            }
        }

        /// <summary>
        /// 获取或设置实体类名的后缀.
        /// </summary>
        public static string Postfix
        {
            get
            {
                return ToolSetting._postfix;
            }
            set
            {
                ToolSetting._postfix = value;
            }
        }

        /// <summary>
        /// 获取或设置所生成的实体类的存储路径.
        /// </summary>
        public static string SavePath
        {
            get
            {
                return ToolSetting._savePath;
            }
            set
            {
                ToolSetting._savePath = value;
            }
        }

        /// <summary>
        /// 获取或设置制表符的大小(空格的个数).
        /// </summary>
        public static int TabSize
        {
            get
            {
                return ToolSetting._tabsize;
            }
            set
            {
                ToolSetting._tabsize = value;
            }
        }

        /// <summary>
        /// 获取或设置实体类中所添加的引用列表.
        /// </summary>
        public static string[] References
        {
            get
            {
                return ToolSetting._references;
            }
            set
            {
                ToolSetting._references = value;
            }
        }
        public static string[] DALReferences
        {
            get
            {
                return ToolSetting._dalreferences;
            }
            set
            {
                ToolSetting._dalreferences = value;
            }
        }
        public static string[] IDALReferences
        {
            get
            {
                return ToolSetting._idalreferences;
            }
            set
            {
                ToolSetting._idalreferences = value;
            }
        }
        /// <summary>
        /// 获取或设置SqlServer数据库的数据类型映射信息.
        /// </summary>
        public static DataTable SqlDataMapping
        {
            get
            {
                return ToolSetting._sqlDataMapping;
            }
            set
            {
                ToolSetting._sqlDataMapping = value;
            }
        }

        /// <summary>
        /// 获取或设置Access数据库的数据类型映射信息.
        /// </summary>
        public static DataTable AccessDataMapping
        {
            get
            {
                return ToolSetting._accessDataMapping;
            }
            set
            {
                ToolSetting._accessDataMapping = value;
            }
        }

        /// <summary>
        /// 获取或设置Oracle数据库的数据类型映射信息.
        /// </summary>
        public static DataTable OracleDataMapping
        {
            get
            {
                return ToolSetting._oracleDataMapping;
            }
            set
            {
                ToolSetting._oracleDataMapping = value;
            }
        }

        /// <summary>
        /// 得到存储数据映射信息的表结构．
        /// </summary>
        /// <returns>表对象</returns>
        private static DataTable GetTableStructure()
        {
            DataTable table = new DataTable();
            table.Columns.Add("数据库中数据类型", typeof(string));
            table.Columns.Add("代码中数据类型", typeof(string));
            return table;
        }

        /// <summary>
        /// 本方法得到配置文件中的所有的引用项的值.
        /// </summary>
        /// <param name="doc">配置文件对象</param>
        /// <returns>引用值列表</returns>
        private static string[] GetReferences(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("/Setting/References/item");
            string[] refes = new string[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                refes[i] = nodes[i].InnerText;
            }
            return refes;
        }
        private static string[] GetDALReferences(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("/Setting/DALReferences/item");
            string[] refes = new string[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                refes[i] = nodes[i].InnerText;
            }
            return refes;
        }
        private static string[] GetIDALReferences(XmlDocument doc)
        {
            XmlNodeList nodes = doc.SelectNodes("/Setting/IDALReferences/item");
            string[] refes = new string[nodes.Count];
            for (int i = 0; i < nodes.Count; i++)
            {
                refes[i] = nodes[i].InnerText;
            }
            return refes;
        }
        /// <summary>
        /// 本方法可以得到配置文件中的数据类型映射信息.
        /// </summary>
        /// <param name="doc">配置文件对象</param>
        /// <param name="dbType">数据库类型</param>
        /// <returns>包含数据类型映射信息的表对象</returns>
        private static DataTable GetDataTypeMapping(XmlDocument doc, string dbType)
        {
            //得到xml数据查询字符串.
            string xpath = "";
            if (dbType.Equals("SqlServer"))
            {
                xpath = "/Setting/DataTypeMapping/SqlServer/item";
            }
            else if (dbType.Equals("Access"))
            {
                xpath = "/Setting/DataTypeMapping/MSAccess/item";
            }
            else
            {
                xpath = "/Setting/DataTypeMapping/Oracle/item";
            }

            //得到数据映射信息集合.
            XmlNodeList nodes = doc.SelectNodes(xpath);
            XmlNode node = null;

            //得到将包含数据类型映射信息的表对象.
            DataTable table = ToolSetting.GetTableStructure();

            //将数据类型映射信息填充到表对象中.
            for (int i = 0; i < nodes.Count; i++)
            {
                node = nodes[i];
                table.Rows.Add(new object[] { node.ChildNodes[0].InnerText, node.ChildNodes[1].InnerText });
            }

            //返回表对象.
            return table;
        }

        /// <summary>
        /// 本方法用于保存引用列表.
        /// </summary>
        /// <param name="settings">设置信息文档对象</param>
        private static void SaveReferenceList(XmlDocument settings)
        {
            XmlNode references = settings.SelectSingleNode("/Setting/References");
            references.RemoveAll();
            XmlElement elem = null;
            for (int i = 0; i < ToolSetting.References.Length; i++)
            {
                elem = settings.CreateElement("item");
                elem.InnerText = ToolSetting.References[i];
                references.AppendChild(elem);
            }
        }

        /// <summary>
        /// 保存数据类型映射信息.
        /// </summary>
        /// <param name="set">设置信息文档对象</param>
        /// <param name="dbType">数据库类型</param>
        private static void SaveDataTypeMappingInfo(XmlDocument set, string databaseType)
        {
            //确定操作的数据库类型.
            DataTable data = null;
            string xpath = null;
            if (databaseType.Equals("SqlServer"))
            {
                data = ToolSetting.SqlDataMapping;
                xpath = "/Setting/DataTypeMapping/SqlServer";
            }
            else if (databaseType.Equals("Access"))
            {
                data = ToolSetting.AccessDataMapping;
                xpath = "/Setting/DataTypeMapping/MSAccess";
            }
            else
            {
                data = ToolSetting.OracleDataMapping;
                xpath = "/Setting/DataTypeMapping/Oracle";
            }

            //保存信息.
            XmlNode typeMap = set.SelectSingleNode(xpath);
            typeMap.RemoveAll();
            XmlElement item = null;
            XmlElement dbType = null;
            XmlElement codeType = null;
            for (int i = 0; i < data.Rows.Count; i++)
            {
                item = set.CreateElement("item");
                dbType = set.CreateElement("DBType");
                dbType.InnerText = data.Rows[i][0].ToString();
                codeType = set.CreateElement("CodeType");
                codeType.InnerText = data.Rows[i][1].ToString();
                item.AppendChild(dbType);
                item.AppendChild(codeType);
                typeMap.AppendChild(item);
            }
        }

        /// <summary>
        /// 获取或设置所生成的实体类的存储路径.
        /// </summary>
        public static string ServiceName
        {
            get
            {
                return ToolSetting._serviceName;
            }
            set
            {
                ToolSetting._serviceName = value;
            }
        }

        /// <summary>
        /// 获取或设置所生成的实体类的存储路径.
        /// </summary>
        public static string UserName
        {
            get
            {
                return ToolSetting._userName;
            }
            set
            {
                ToolSetting._userName = value;
            }
        }

        /// <summary>
        /// 获取或设置所生成的实体类的存储路径.
        /// </summary>
        public static string UserPassWord
        {
            get
            {
                return ToolSetting._userPassWord;
            }
            set
            {
                ToolSetting._userPassWord = value;
            }
        }
    }
}
