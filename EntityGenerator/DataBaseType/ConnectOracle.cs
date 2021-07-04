using EntityGenerator.SystemSetting;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace EntityGenerator.DataBaseType
{
    public class ConnectOracle
    {
        //Oracle数据库连接.
        private static Oracle.ManagedDataAccess.Client.OracleConnection _connection;

        /// <summary>
        /// 打开数据连接.	
        /// </summary>
        /// <param name="conStr">数据库连接字符串</param>
        public static void OpenConnection(string conStr)
        {
            try
            {
                ConnectOracle._connection = new Oracle.ManagedDataAccess.Client.OracleConnection(conStr);
                ConnectOracle._connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 打开数据连接.	
        /// </summary>
        public static void OpenConnection()
        {
            if (ConnectOracle._connection == null)
            {
                MessageBox.Show("尚未得到数据库连接对象,\n请先调用方法void OpenConnection(string conStr)!");
                return;
            }

            try
            {
                ConnectOracle._connection.Open();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 得到Oracle数据库的联接字符串.
        /// </summary>
        /// <param name="server">服务器名</param>        
        /// <param name="uid">用户名</param>
        /// <param name="pwd">密码</param>
        /// <returns>联接字符串</returns>
        public static string GetConnectionStr(string server, string uid, string pwd)
        {
            return "data source=" + server + ";user id=" + uid + ";password=" + pwd;
        }

        /// <summary>
        /// 关闭数据库连接.
        /// </summary>
        public static void CloseConnection()
        {
            try
            {
                if (ConnectOracle._connection.State == ConnectionState.Open)
                {
                    ConnectOracle._connection.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 执行返回一个表的相关数据的查询.
        /// </summary>
        /// <param name="sql">SQL命令</param>
        /// <returns>数据表对象</returns>
        public static DataTable GetDataTable(String sql)
        {
            //实例化一个表对象.
            DataTable dataTable = new DataTable();
            try
            {
                new Oracle.ManagedDataAccess.Client.OracleDataAdapter(sql, ConnectOracle._connection).Fill(dataTable);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
            //返回表对象.
            return dataTable;
        }

        /// <summary>
        /// 得到所有的表名和视图名.
        /// </summary>        
        /// <returns>表对象</returns>
        public static DataTable GetAllTableAndViewName()
        {
            try
            {
                DataTable dt = ConnectOracle.GetDataTable("select TNAME,TABTYPE from tab where TABTYPE='TABLE' or TABTYPE='VIEW' order by TABTYPE");
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["TNAME"].ToString().StartsWith("BIN$"))
                    {
                        dt.Rows.Remove(dt.Rows[i]);
                        i--;
                    }
                }
                return dt;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 得到表或视图的结构.
        /// </summary>        
        /// <param name="tableName">表或视图名</param>
        /// <returns>表对象</returns>
        public static DataTable GetTableOrViewStructure(string tableOrViewName)
        {
            try
            {
                //                return ConnectOracle.GetDataTable(@"select 
                //      ut.COLUMN_NAME,
                //      ut.DATA_TYPE,
                //      ut.DATA_LENGTH,
                //      uc.comments,
                //      ut.NULLABLE,
                //      null as constraint_type
                //from user_tab_columns ut
                //inner JOIN user_col_comments uc
                //on ut.TABLE_NAME = uc.table_name and ut.COLUMN_NAME = uc.column_name
                //where ut.Table_Name = '" + tableOrViewName + "' order by ut.column_name");
                return ConnectOracle.GetDataTable(string.Format(@"SELECT
                                                                    	ut.COLUMN_NAME,
                                                                    	ut.DATA_TYPE,
                                                                    	ut.DATA_LENGTH,
                                                                    	uc.comments,
                                                                    	ut.NULLABLE,
                                                                    	A.constraint_type 
                                                                    FROM
                                                                    	user_tab_columns ut
                                                                    	INNER JOIN user_col_comments uc ON ut.TABLE_NAME = uc.table_name 
                                                                    	AND ut.COLUMN_NAME = uc.column_name
                                                                    	LEFT JOIN (
                                                                    	SELECT
                                                                    		cu.COLUMN_NAME,
                                                                    		au.constraint_type 
                                                                    	FROM
                                                                    		user_cons_columns cu,
                                                                    		user_constraints au 
                                                                    	WHERE
                                                                    		cu.constraint_name = au.constraint_name 
                                                                    		AND au.table_name = '{0}' 
                                                                    		AND au.constraint_type = 'P' 
                                                                    	) A ON ut.COLUMN_NAME = A.COLUMN_NAME 
                                                                    WHERE
                                                                    	ut.Table_Name = '{0}' 
                                                                    ORDER BY
                                                                    	ut.column_name", tableOrViewName));
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return null;
        }

        /// <summary>
        /// 将表或视图的结构转换为字段信息.
        /// </summary>
        /// <param name="stru">表或视图的结构信息</param>
        /// <returns>字段信息</returns>
        public static DataTable ConvertTableOrViewStructure(DataTable stru)
        {
            DataTable fields = FieldMethods.GetFieldsTable();
            string fieName = null;
            string fieType = null;
            string comments = null;
            string constrainttype = null;
            string dbfieType = null;
            int filedlenth = 0;
            for (int i = 0; i < stru.Rows.Count; i++)
            {
                fieName = stru.Rows[i][0].ToString();
                fieType = ToolSetting.GetMapping(SystemSetting.DbType.Oracle, stru.Rows[i][1].ToString());
                comments = stru.Rows[i]["comments"].ToString();
                constrainttype = stru.Rows[i]["constraint_type"].ToString();
                dbfieType = stru.Rows[i][1].ToString();
                filedlenth = string.IsNullOrEmpty(stru.Rows[i]["DATA_LENGTH"].ToString()) ? 0 : Convert.ToInt32(stru.Rows[i]["DATA_LENGTH"].ToString());
                fields.Rows.Add(new object[] { fieName, fieType, comments, constrainttype, dbfieType, filedlenth });
            }
            return fields;
        }
    }
}
