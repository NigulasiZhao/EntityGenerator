using System;
using System.Collections.Generic;
using System.Text;

namespace EntityGenerator.GeneratorMethod
{
    public struct Field
    {
        public string name;    //字段名.
        public string type;    //字段类型.
        public string remark;  //字段注释.
        public string constrainttype;//是否主键
    }
}
