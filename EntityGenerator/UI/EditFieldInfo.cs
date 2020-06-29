using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace EntityGenerator.UI
{
    public partial class EditFieldInfo : Form
    {
        //数据源.
        public DataTable _source = null;
        //数据行索引.
        public int _index = -1;

        public EditFieldInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// '取消＇按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCalcel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// ＇确定＇按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string name = this.txtFieldName.Text.Trim();
            string type = this.cmbFieldType.Text.Trim();
            string remark = this.txtFieldRemark.Text.Trim();
            this._source.Rows[this._index][0] = name;
            this._source.Rows[this._index][1]=type;
            this._source.Rows[this._index][2]=remark;
            this.Close();
        }
    }
}