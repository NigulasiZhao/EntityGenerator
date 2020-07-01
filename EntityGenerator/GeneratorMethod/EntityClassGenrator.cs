using EntityGenerator.SystemSetting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace EntityGenerator.GeneratorMethod
{
    class EntityClassGenrator : System.IDisposable
    {
        //以下声明代表了实体类的各个部分.
        private string _classHeader;
        private ArrayList _fieldList = new ArrayList();
        private string _structure;
        private string _struWithParams;
        private ArrayList _attrList = new ArrayList();
        private string _classEnder;

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
        public EntityClassGenrator(string namespaceName, string[] refList, string claName, string claRemark, DataTable fieldInfo)
        {
            claName = GeneratorTool.FormatTableOrFieldName(claName) + ToolSetting.Postfix;
            this.GetClassHeader(namespaceName, refList, claName, claRemark);
            //this.GetStructureMethod(claName);
            this.GetStruMethodWithParams(claName, fieldInfo);
            this.GetFieldList(fieldInfo);
            this.GetAttrList(fieldInfo);
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
                foreach (object field in this._fieldList)
                {
                    stream.Write(field.ToString());
                }
                stream.Write(this._structure);
                stream.Write(this._struWithParams);
                foreach (object attr in this._attrList)
                {
                    stream.Write(attr.ToString());
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
            str += "\nnamespace " + namespaceName + "\n{\n[Serializable]\n[DataContract]\n[System.ComponentModel.DataAnnotations.Schema.Table(\"" + claName + "\")]\n";

            str += GeneratorTool.ForwardIndentCodeBlock(this.FormatClassRemark(claRemark), 1);
            str += "\n" + this._tab + "public class " + claName + "{\n";
            this._classHeader = str;
        }

        /// <summary>
        /// 得到.Net实体类的字段代码列表.
        /// </summary>
        /// <param name="fieldInfo">字段信息表</param>
        private void GetFieldList(DataTable fieldInfo)
        {
            Field field = new Field();
            for (int i = 0; i < fieldInfo.Rows.Count; i++)
            {
                field.name = GeneratorTool.FormatTableOrFieldName(fieldInfo.Rows[i][0].ToString());
                field.type = fieldInfo.Rows[i][1].ToString();
                field.remark = fieldInfo.Rows[i][2].ToString();
                //this._fieldList.Add(this.GetFieldItem(field));
            }
        }

        /// <summary>
        /// 得到.Net实体类的属性列表．
        /// </summary>
        /// <param name="fieldInfo">字段信息表</param>
        private void GetAttrList(DataTable fieldInfo)
        {
            Field field = new Field();
            for (int i = 0; i < fieldInfo.Rows.Count; i++)
            {
                field.name = GeneratorTool.ChartConversion(GeneratorTool.FormatTableOrFieldName(fieldInfo.Rows[i][0].ToString()));
                field.type = fieldInfo.Rows[i][1].ToString();
                field.remark = fieldInfo.Rows[i][2].ToString();
                field.constrainttype = fieldInfo.Rows[i][3].ToString();
                this._attrList.Add(this.GetAttrItem(field));
            }
        }

        /// <summary>
        /// 根据某个字段信息,得到.Net字段项的代码.
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <returns>字段项代码</returns>
        private string GetFieldItem(Field field)
        {
            string fieldName = GeneratorTool.CS_FormatFieldName(field.name);
            return GeneratorTool.ForwardIndentCodeBlock("\nprivate " + field.type + " " + fieldName + ";", 2);
        }

        /// <summary>
        /// 根据某个字段信息,得到.Net属性项的代码.
        /// </summary>
        /// <param name="field">字段信息</param>
        /// <returns>属性项代码</returns>
        private string GetAttrItem(Field field)
        {
            string fieldName = GeneratorTool.CS_FormatFieldName(field.name);
            string attrName = GeneratorTool.CS_FormatAttributeName(field.name);
            string result = string.Empty;
            if (field.constrainttype.Contains("P"))
            {
                string PrimaryKeyType = string.Empty;
                if (field.type == "int")
                {
                    PrimaryKeyType = "Identity";
                }
                else if (field.type == "string")
                {
                    PrimaryKeyType = "Guid";
                }
                result = "\n\n/// <summary>\n///" + field.remark + "\n/// </summary>\n[DataMember]\n[Column(FilterType = FilterType.IsPrimaryKey, PrimaryKeyType = PrimaryKeyType." + PrimaryKeyType + ")]\npublic " + field.type + " " + attrName + "{\tget;\tset;\t}";
            }
            else
            {
                result = "\n\n/// <summary>\n///" + field.remark + "\n/// </summary>\n[DataMember]\npublic " + field.type + " " + attrName + "{\tget;\tset;\t}";
            }
            result = Regex.Replace(result, "\t", this._tab);
            return GeneratorTool.ForwardIndentCodeBlock(result, 2);
        }

        /// <summary>
        /// 根据类名,得到构造方法的代码.
        /// </summary>
        /// <param name="claName">类名</param>       
        private void GetStructureMethod(string claName)
        {
            this._structure = GeneratorTool.ForwardIndentCodeBlock("\n\n/// <summary>\n/// 默认构造方法.\n/// </summary>\npublic " + GeneratorTool.ChartConversion(claName) + "()\n{\n}", 2);
        }

        /// <summary>
        /// 得到含参构造方法的代码.
        /// </summary>
        /// <param name="claName">类名</param>
        /// <param name="fieldInfo">字段信息</param>
        private void GetStruMethodWithParams(string claName, DataTable fieldInfo)
        {
            //claName = GeneratorTool.CapFirstLetter(claName);
            ////临时存储含参构造方法代码块的三个主要部分.
            //System.Collections.Generic.List<String> temp1 = new System.Collections.Generic.List<string>();
            //System.Collections.Generic.List<String> temp2 = new System.Collections.Generic.List<string>();
            //System.Collections.Generic.List<String> temp3 = new System.Collections.Generic.List<string>();

            ////组织部分代码.
            //string name = "";
            //string type = "";
            //string remark = "";
            //for (int i = 0; i < fieldInfo.Rows.Count; i++)
            //{
            //    DataRow row = fieldInfo.Rows[i];
            //    name = GeneratorTool.CS_FormatFieldName(row[0].ToString());
            //    type = row[1].ToString();
            //    remark = row[2].ToString();
            //    temp1.Add("/// <param name=\"" + name + "\">" + remark + "</param>");
            //    temp2.Add(type + " " + name);
            //    temp3.Add("this." + name + " = " + name + ";");
            //}


            ////组织含参构造的整体的代码.
            //this._struWithParams = "\n\n/// <summary>\n/// 含参构造方法.\n/// </summary>\n";

            //foreach (string t1 in temp1)
            //{
            //    this._struWithParams += t1 + "\n";
            //}

            //this._struWithParams += "public " + claName + "(";

            //foreach (string t2 in temp2)
            //{
            //    this._struWithParams += t2 + ",\n" + this._tab + this._tab;
            //}

            //this._struWithParams = Regex.Replace(this._struWithParams, ",\n[^,]*$", ")\n{\n", RegexOptions.Singleline);

            //foreach (string t3 in temp3)
            //{
            //    this._struWithParams += this._tab + t3 + "\n";
            //}
            //this._struWithParams += "}";

            //this._struWithParams = GeneratorTool.ForwardIndentCodeBlock(this._struWithParams, 2);
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

        /// <summary>
        /// 释放本类所占用的资源.
        /// </summary>
        public void Dispose()
        {
            this._attrList = null;
            this._classEnder = null;
            this._classHeader = null;
            this._fieldList = null;
            this._structure = null;
        }
    }
}
