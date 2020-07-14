using EntityGenerator.SystemSetting;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace EntityGenerator.GeneratorMethod
{
    public class GeneratorTool
    {
        /// <summary>
        /// 获取用户设置的制表符.
        /// </summary>
        /// <returns>自定义制表符</returns>
        public static string GetTabSymbol()
        {
            string tab = "";
            for (int i = 0; i < ToolSetting.TabSize; i++)
            {
                tab += " ";
            }
            return tab;
        }

        /// <summary>
        /// 得实体类的文件头注释.
        /// </summary>
        /// <param name="classKind">实体类的类型</param>
        /// <returns>文件头注释</returns>
        public static string GetEntityClassHeader()
        {
            //组织并返回文件头注释.
            return @"/**********************************************
** Class_Kind:      DotNet Entity Class
** Creater:         Entity Class Generator
** Create Date:     " + DateTime.Now.ToString() + @"
** Description:     Entity Class
** Version:         Entity Class Generator 1.0.0
**********************************************/";
        }

        /// <summary>
        /// 格式化注释文本,即将注释文本合理折行.
        /// </summary>
        /// <param name="remark">待格式化注释文本</param>
        /// <returns>格式化后的注释文本</returns>
        public static string FormatRemark(string remark)
        {
            //去除注释中的换行符.
            remark = Regex.Replace(remark, "\n+", "");
            //在每一句的末尾添加换行符.
            remark = Regex.Replace(remark, "[.+．+。+。+.+]", ".\n");
            //组织格式化注释文本.
            return remark;
        }

        /// <summary>
        /// 向前缩进代码块.
        /// </summary>
        /// <param name="codeBolck">待缩进的代码块</param>
        /// <param name="IndentCount">缩进次数</param>
        /// <returns>缩进后的代码块</returns>
        public static string ForwardIndentCodeBlock(string codeBolck, int IndentCount)
        {
            //组织跳格符.
            string tab = "";
            for (int i = 0; i < IndentCount; i++)
            {
                tab += GeneratorTool.GetTabSymbol();
            }
            //返回缩进后的代码块.
            return tab + Regex.Replace(codeBolck, "\n", "\n" + tab, RegexOptions.Multiline);
        }

        /// <summary>
        /// 此方法可以确保字符串格式符合C#中属性命名法.
        /// </summary>
        /// <param name="field">待处理字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string CS_FormatAttributeName(string field)
        {
            char firstChar = Char.ToUpper(field[0]);
            StringBuilder strBlder = new StringBuilder(field);
            strBlder[0] = firstChar;
            return strBlder.ToString();
        }

        /// <summary>
        /// 将输入字符串,按C#中的字段命名规则格式化.
        /// </summary>
        /// <param name="field">待格式化字符串</param>
        /// <returns>格式化字符串</returns>
        public static string CS_FormatFieldName(string field)
        {
            char firstChar = Char.ToLower(field[0]);
            StringBuilder strBlder = new StringBuilder(field);
            strBlder[0] = firstChar;
            return "_" + strBlder.ToString();
        }

        /// <summary>
        /// 此方法可以确保字符串格式符合Java中属性命名法.
        /// </summary>
        /// <param name="field">待处理字符串</param>
        /// <returns>处理后的字符串</returns>
        public static string Java_FormatAttributeName(string field)
        {
            char firstChar = Char.ToLower(field[0]);
            StringBuilder strBlder = new StringBuilder(field);
            strBlder[0] = firstChar;
            return strBlder.ToString();
        }

        /// <summary>
        /// 将字符串首字母大写.
        /// </summary>
        /// <param name="word">待处理字符串</param>
        /// <returns>首字母大写的字符串</returns>
        public static string CapFirstLetter(string word)
        {
            return CS_FormatAttributeName(word);
        }

        /// <summary>
        /// 将字符串首字母小写.
        /// </summary>
        /// <param name="word">待处理字符串</param>
        /// <returns>首字母大写的字符串</returns>
        public static string LowerFirstLetter(string word)
        {
            return Java_FormatAttributeName(word);
        }
        public static string ChartConversion(string CharText)
        {
            string CompleteChar = string.Empty;
            string[] CharArr = CharText.ToLower().Split('_');
            foreach (var item in CharArr)
            {
                char firstChar = Char.ToUpper(item[0]);
                StringBuilder strBlder = new StringBuilder(item);
                strBlder[0] = firstChar;
                CompleteChar += strBlder.ToString() + "_";
            }
            return CompleteChar.TrimEnd('_');
        }



        /// <summary>
        /// 格式化表名或字段名.
        /// 去掉表名或字段名中的空字符或特殊字符.
        /// </summary>
        /// <param name="tableOrFieldName">待格式化表名或字段名</param>
        /// <returns>已格式化表名或字段名</returns>
        public static string FormatTableOrFieldName(string tableOrFieldName)
        {
            //去掉所有的非字字符.
            string temp = Regex.Replace(tableOrFieldName, @"\W", "_");

            //判断首字符是否数字.
            char first = temp[0];
            int ansi = Convert.ToInt32(first);
            if (ansi >= 48 && ansi <= 57)
            {
                temp = "_" + temp;
            }

            //将字符串中的过多的下划线用一个代替.
            return Regex.Replace(temp, "_+", "_");
        }

        /// <summary>
        /// 得DAL的文件头注释.
        /// </summary>
        /// <param name="classKind">实体类的类型</param>
        /// <returns>文件头注释</returns>
        public static string GetEntityDALHeader()
        {
            //组织并返回文件头注释.
            return @"/**********************************************
** Class_Kind:      DotNet Entity DAL
** Creater:         Entity DAL Generator
** Create Date:     " + DateTime.Now.ToString() + @"
** Description:     Entity DAL
** Version:         Entity DAL Generator 1.0.0
**********************************************/";
        }
    }
}

