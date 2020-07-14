using EntityGenerator.SystemSetting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EntityGenerator.GeneratorMethod
{
    class EntityDALGenrator : System.IDisposable
    {
        //以下声明代表了实体类的各个部分.
        private string _classHeader;
        private string _classEnder;
        private ArrayList _methods = new ArrayList();
        //下面变量代表了自定义制表符.
        private string _tab = GeneratorTool.GetTabSymbol();
        /// <summary>
        /// 构造方法.
        /// </summary>
        /// <param name="namespaceName">命名空间名</param>
        /// <param name="refList">引用列表</param>
        /// <param name="claName">类名</param>
        /// <param name="claRemark">类注释</param>
        /// <param name="fieldInfo">字段信息表</param>
        public EntityDALGenrator(string namespaceName, string[] refList, string claName, string claRemark, DataTable fieldInfo)
        {
            claName = GeneratorTool.ChartConversion(GeneratorTool.FormatTableOrFieldName(claName) + ToolSetting.Postfix);
            this.GetClassHeader(namespaceName, refList, claName, claRemark);
            this.CURD(claName, claRemark, fieldInfo);
            this.GetClassEnder();
        }
        /// <summary>
        /// 保存实体类.
        /// </summary>
        /// <param name="path">保存目录</param>
        /// <param name="fileName">文件名称</param>
        /// <returns>是否保存成功</returns>
        public bool Save(string path, string fileName)
        {
            fileName = Regex.Replace(fileName, "[^A-Za-z0-9_.]", "_");
            bool succ = false;
            try
            {
                //若没有存在指定的目录,则创建之.
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                //若文件已存在,则删除之.
                if (File.Exists(path + @"\" + fileName))
                {
                    File.Delete(path + @"\" + fileName);
                }
                //创建文件并得到文件流对象.
                FileStream str = new FileStream(path + @"\" + fileName, FileMode.Create);
                //得到写入流对象.
                StreamWriter stream = new StreamWriter(str, System.Text.Encoding.UTF8);
                //将实体类的内容写到文件流中.
                stream.Write(this._classHeader);
                foreach (object field in this._methods)
                {
                    stream.Write(field.ToString());
                }
                stream.Write(this._classEnder);
                //清空并关闭流对象.
                stream.Flush();
                stream.Close();
                succ = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("保存失败!" + ex.Message);
            }
            return succ;
        }
        /// <summary>
        /// 得到.Net实体类的类头.
        /// </summary>
        /// <param name="namespaceName">命名空间名</param>
        /// <param name="refList">引用列表</param>
        /// <param name="claName">类名</param>
        /// <param name="claRemark">类注释</param>
        private void GetClassHeader(string namespaceName, string[] refList, string claName, string claRemark)
        {
            //claName = GeneratorTool.CapFirstLetter(claName);
            claName = GeneratorTool.ChartConversion(claName);

            string str = "";
            str += GeneratorTool.GetEntityClassHeader();
            str += "\n";
            for (int i = 0; i < refList.Length; i++)
            {
                str += refList[i] + "\n";
            }
            str += "\nnamespace " + namespaceName + "\n{\n";

            str += GeneratorTool.ForwardIndentCodeBlock(this.FormatClassRemark(claRemark + "DAL层"), 1);
            str += "\n" + this._tab + "public class " + claName + "DAL:I" + claName + "DAL\n{\n";
            this._classHeader = str;
        }
        /// <summary>
        /// 得到实体的尾部代码.
        /// </summary>
        private void GetClassEnder()
        {
            this._classEnder = "\n" + this._tab + "}\n}";
        }
        /// <summary>
        /// 本方法用于格式化类注释.
        /// </summary>
        /// <param name="remark">待格式化注释</param>
        /// <returns>已格式化注释</returns>
        private string FormatClassRemark(string remark)
        {
            string temp = Regex.Replace(GeneratorTool.FormatRemark(remark), "\n", "\n/// ");
            return "/// <summary>\n///" + temp + "\n/// </summary>";
        }
        #region CURD方法构造
        private void CURD(string claName, string claRemark, DataTable fieldInfo)
        {
            this._methods.Add(this.GetAddMethods(claName, claRemark));
            this._methods.Add(this.GetUpdateMethods(claName, claRemark));
            this._methods.Add(this.GetDeleteMethods(claName, claRemark));
            this._methods.Add(this.GetInfoMethods(claName, claRemark, fieldInfo));
            this._methods.Add(this.GetListMethods(claName, claRemark, fieldInfo));

        }
        /// <summary>
        /// 获取添加方法
        /// </summary>
        /// <param name="claName"></param>
        /// <param name="claRemark"></param>
        /// <returns></returns>
        private string GetAddMethods(string claName, string claRemark)
        {
            string AddSql = "/// <summary>\n/// 添加" + claRemark + "\n/// </summary>\n/// <param name=\"model\">模型</param>\n/// <returns></returns>\npublic MessageEntity Add(" + claName + " model)\n{\nusing (var conn = ConnectionFactory.GetDBConn(ConnectionFactory.DBConnNames.ORCL))\n{\nStringBuilder SqlInsertEqu = new StringBuilder();\nvar rows = 0;\nvar insertSql = DapperExtentions.MakeInsertSql(model);\nif (string.IsNullOrEmpty(insertSql))\n{\nreturn MessageEntityTool.GetMessage(ErrorType.SqlError, \"请检查实体类\");\n}\ntry\n{\nrows = conn.Execute(insertSql, model);\nreturn MessageEntityTool.GetMessage(rows);\n}\ncatch (Exception e)\n{\nreturn MessageEntityTool.GetMessage(ErrorType.SqlError, e.Message);\n}\n}\n}\n";
            return AddSql;
        }
        /// <summary>
        /// 获取更新方法
        /// </summary>
        /// <param name="claName"></param>
        /// <param name="claRemark"></param>
        /// <returns></returns>
        private string GetUpdateMethods(string claName, string claRemark)
        {
            string UpdateSql = "/// <summary>\n///修改" + claRemark + "\n/// </summary>\n/// <param name=\"model\">模型</param>\n/// <returns></returns>\npublic MessageEntity Update(" + claName + " model)\n{\nusing (var conn = ConnectionFactory.GetDBConn(ConnectionFactory.DBConnNames.ORCL))\n{\nvar rows = 0;\nvar insertSql = DapperExtentions.MakeUpdateSql(model);\nif (string.IsNullOrEmpty(insertSql))\n{\nreturn MessageEntityTool.GetMessage(ErrorType.SqlError, \"请检查实体类\");\n}\ntry\n{\nrows = conn.Execute(insertSql, model);\nreturn MessageEntityTool.GetMessage(rows);\n}\ncatch (Exception e)\n{\nreturn MessageEntityTool.GetMessage(ErrorType.SqlError, e.Message);\n}\n}\n}\n";
            return UpdateSql;
        }
        /// <summary>
        /// 获取删除方法
        /// </summary>
        /// <param name="claName"></param>
        /// <param name="claRemark"></param>
        /// <returns></returns>
        private string GetDeleteMethods(string claName, string claRemark)
        {

            string DeleteSql = "/// <summary>\n/// 删除" + claRemark + "\n/// </summary>\n/// <param name=\"model\">模型</param>\n/// <returns></returns>\npublic MessageEntity Delete(" + claName + " model)\n{\nusing (var conn = ConnectionFactory.GetDBConn(ConnectionFactory.DBConnNames.ORCL))\n{\nvar rows = 0;\nvar excSql = DapperExtentions.MakeDeleteSql(model);\nif (string.IsNullOrEmpty(excSql))\n{\nreturn MessageEntityTool.GetMessage(ErrorType.SqlError, \"请检查实体类\");\n}\ntry\n{\nrows = conn.Execute(excSql, model);\nreturn MessageEntityTool.GetMessage(rows);\n}\ncatch (Exception e)\n{\nreturn MessageEntityTool.GetMessage(ErrorType.SqlError, e.Message);\n}\n}\n}\n";
            return DeleteSql;
        }
        /// <summary>
        /// 获取单个实体方法
        /// </summary>
        /// <param name="claName"></param>
        /// <param name="claRemark"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        private string GetInfoMethods(string claName, string claRemark, DataTable fieldInfo)
        {
            string PKey = "";
            Field field = new Field();
            for (int i = 0; i < fieldInfo.Rows.Count; i++)
            {
                field.constrainttype = fieldInfo.Rows[i][3].ToString();
                if (field.constrainttype.Contains("P"))
                {
                    PKey = GeneratorTool.ChartConversion(GeneratorTool.FormatTableOrFieldName(fieldInfo.Rows[i][0].ToString()));
                }
            }
            string GetInfoSql = "/// <summary>\n/// 根据ID获取" + claRemark + "\n/// </summary>\n/// <param name=\"ID\"></param>\n/// <returns></returns>\npublic " + claName + " GetInfo(string ID)\n{\nList<" + claName + "> _ListField = new List<" + claName + ">();\nusing (var conn = ConnectionFactory.GetDBConn(ConnectionFactory.DBConnNames.ORCL))\n{\n_ListField = conn.Query<" + claName + ">(\"select * from " + claName + " t where " + PKey + "='\" + ID + \"'\").ToList();\n}\nif (_ListField.Count > 0)\n{\nreturn _ListField[0];\n}\nelse\n{\nreturn null;\n}\n}\n";
            return GetInfoSql;

        }
        /// <summary>
        /// 获取list方法
        /// </summary>
        /// <param name="claName"></param>
        /// <param name="claRemark"></param>
        /// <param name="fieldInfo"></param>
        /// <returns></returns>
        private string GetListMethods(string claName, string claRemark, DataTable fieldInfo)
        {
            StringBuilder Fileds = new StringBuilder("");
            for (int i = 0; i < fieldInfo.Rows.Count; i++)
            {
                Fileds.Append(GeneratorTool.ChartConversion(GeneratorTool.FormatTableOrFieldName(fieldInfo.Rows[i][0].ToString())) + ",");
            }
            string Filed = Fileds.ToString().TrimEnd(',');
            string GetListSql = "/// <summary>\n/// 获得" + claRemark + "列表\n/// </summary>\n/// <param name=\"parInfo\">参数信息</param>/// <param name=\"sort\">排序字段</param>\n/// <param name=\"ordering\">升序/降序</param>/// <param name=\"num\">当前页</param>/// <param name=\"page\">每页数据行数</param>\n/// <returns></returns>\npublic MessageEntity GetList(List<ParameterInfo> parInfo, string sort, string ordering, int num, int page, string sqlCondition)\n{string sql = @\"   select " + Filed + " from " + claName + " ipc \n\" + sqlCondition + \" and ipc.DeleteState=0\";\nvar ResultList = DapperExtentions.EntityForSqlToPager<" + claName + ">(sql, sort, ordering, num, page, out MessageEntity result, ConnectionFactory.DBConnNames.ORCL);\nreturn result;\n}\n";
            return GetListSql;
        }
        #endregion
        /// <summary>
        /// 释放本类所占用的资源.
        /// </summary>
        public void Dispose()
        {
            this._methods = null;
            this._classEnder = null;
            this._classHeader = null;
        }
    }
}
