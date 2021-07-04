using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace EntityGenerator.DataBaseType
{
    public class FieldMethods
    {
        public static DataTable GetFieldsTable()
        {
            DataTable fields = new DataTable("FieldsTable");
            fields.Columns.Add("字段名", typeof(string));
            fields.Columns.Add("字段类型", typeof(string));
            fields.Columns.Add("注释", typeof(string));
            fields.Columns.Add("是否主键", typeof(string));
            fields.Columns.Add("数据库字段类型", typeof(string));
            fields.Columns.Add("字段长度", typeof(int));
            return fields;
        }
    }
}
