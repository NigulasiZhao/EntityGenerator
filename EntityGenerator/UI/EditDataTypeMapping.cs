using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using EntityGenerator.SystemSetting;

namespace EntityGenerator.UI
{
    public partial class EditDataTypeMapping : Form
    {
        //以下代表所操作的数据库类型.
        private string _dbType;

        //标识当前窗体的作用(add,change).
        public string _useTo;

        //标识当前窗体编辑的信息源．
        public DataTable _source;

        //标识个体的数据行.
        public int _rowIndex;

        /// <summary>
        /// 构造方法.
        /// </summary>
        /// <param name="dbType">数据库类型</param>
        public EditDataTypeMapping(string dbType)
        {
            InitializeComponent();
            this._dbType = dbType;
        }

        /// <summary>
        /// ＇取消＇按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// 窗体加载事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EditDataTypeMapping_Load(object sender, EventArgs e)
        {
            DataTable table = null;
            if (this._dbType.Equals("SqlServer"))
            {
                table = ToolSetting.SqlDataMapping;
            }
            else if (this._dbType.Equals("Access"))
            {
                table = ToolSetting.AccessDataMapping;
            }
            else 
            {
                table = ToolSetting.OracleDataMapping;
            }
         
            this.cmbDbType.DataSource = table;
            this.cmbDbType.DisplayMember = "数据库中数据类型";

            ArrayList codeType = new ArrayList();
            for (int i = 0; i < table.Rows.Count; i++)
            {
                //lable变量表示'添加标志'.
                bool lable = true;
                
                string temp=table.Rows[i][1].ToString();

                //条件满足时，将＇添加标志＇设为false.
                foreach (object type in codeType)
                {
                    if (temp.Equals(type.ToString()))
                    {
                        lable=false;
                        break;
                    }                    
                }

                //根据＇添加标志＇决定是否添加新项.
                if (lable == true)
                {
                    codeType.Add(temp);
                }
            }
            this.cmbCodeType.DataSource = codeType;
                     
        }

        /// <summary>
        /// ＇确定＇按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (this._useTo.Equals("add"))
            {
                this._source.Rows.Add(new object[] { this.cmbDbType.Text.Trim(),this.cmbCodeType.Text.Trim()});                
            }
            else 
            {
                this._source.Rows[this._rowIndex][0]=this.cmbDbType.Text.Trim();
                this._source.Rows[this._rowIndex][1]=this.cmbCodeType.Text.Trim();
            }
            this.Close();
        }
    }
}