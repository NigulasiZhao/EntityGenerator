using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using System.Collections;
using System.Text.RegularExpressions;
using System.Runtime.InteropServices;
using System.Diagnostics;
using EntityGenerator.SystemSetting;
using EntityGenerator.DataBaseType;
using EntityGenerator.GeneratorMethod;

namespace EntityGenerator.UI
{
    /// <summary>
    /// 本类实体类生成器主窗体.
    /// </summary>
    public partial class EntityGenerator : Form
    {
        //包含手动添加的字段信息.
        private System.Data.DataTable _fieldsInfo;

        //标志系统设置是否改变.
        private bool _settingsChanged = false;

        //包含从数据库中得到的所有的类信息及相关的类注释的信息.
        private Hashtable _clasInfo = new Hashtable();
        private Hashtable _clasRemarkInfo = new Hashtable();

        //标志应该保存的字段注释的位置,以便于定位保存.
        private int _index;
        private string _tabName2;
        private bool _saveFieldRemark;

        //标志应该保存的类注释的位置,以便于定位保存.
        private string _tabName;
        private bool _saveClassRemark;

        /// <summary>
        /// 构造方法.
        /// </summary>
        public EntityGenerator()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体加载事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EntityClassGenerator_Load(object sender, EventArgs e)
        {
            //控制数据库参数输入控件的外观.
            this.gpbOracle.Visible = true;
            this.lblIsGenerating.Visible = false;
            this.gpbOracle.Location = new Point(32, 69);

            //加载用户设置信息.
            ToolSetting.LoadUserSettings();
            //初始化用户设置控件.
            this.InitUserSettingControl();
            this.ApplySettingsWarning();

            //使初始状态显示第一个选项卡.
            this.tbcMain.SelectedIndex = 0;
            this.rdbOracle.Checked = true;
        }

        /// <summary>
        /// 单选按钮'Oracle'的选择改变事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void radOracle_CheckedChanged(object sender, EventArgs e)
        {
            if (this.rdbOracle.Checked != true)
            {
                return;
            }
            this.gpbOracle.Visible = true;
        }

        /// <summary>
        /// 保存路径'浏览'按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            DialogResult result = this.fbdSavePath.ShowDialog();
            if (result == DialogResult.OK)
            {
                this.txtSavePath.Text = this.fbdSavePath.SelectedPath;
            }
        }

        /// <summary>
        /// ＇添加＇菜单项事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiAdd_Click(object sender, EventArgs e)
        {
            string dbType = null;
            DataTable data = null;

            dbType = "Oracle";
            data = ToolSetting.OracleDataMapping;


            EditDataTypeMapping add = new EditDataTypeMapping(dbType);
            add.Text = "添加数据映射信息";
            add._useTo = "add";
            add._source = data;
            add.ShowDialog();
            this.SettingsChangedWarning();
        }

        /// <summary>
        /// ＇修改＇菜单项事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiChange_Click(object sender, EventArgs e)
        {
            string dbType = null;
            DataTable data = null;
            DataGridView view = null;
            dbType = "Oracle";
            data = ToolSetting.OracleDataMapping;
            view = this.dgvOracleMapping;
            EditDataTypeMapping add = new EditDataTypeMapping(dbType);
            add.Text = "添加数据映射信息";
            add._useTo = "change";
            add._source = data;
            add._rowIndex = view.CurrentRow.Index;
            add.ShowDialog();
            this.SettingsChangedWarning();
        }

        /// <summary>
        /// ＇删除＇菜单项事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiDelete_Click(object sender, EventArgs e)
        {
            DataTable data = null;
            DataGridView view = null;
            data = ToolSetting.OracleDataMapping;
            view = this.dgvOracleMapping;
            if (data.Rows.Count == 0)
            {
                return;
            }

            int index = view.CurrentRow.Index;
            data.Rows.RemoveAt(index);
            this.SettingsChangedWarning();
        }

        /// <summary>
        /// 主选项卡的选择改变事件．
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tbcMain_SelectedIndexChanged(object sender, EventArgs e)
        {
            //标签文本的改变.

            this.lblNamespace1.Text = "命名空间名:";


            //其它改变.
            if (this.tbcMain.SelectedTab.Tag.Equals("FromDB"))
            {
                ;
            }
            else if (this.tbcMain.SelectedTab.Tag.Equals("Manual"))
            {

            }
            else
            {
                ;
            }
        }

        /// <summary>
        /// 当'.Net后缀','Java后缀','制表符大小'输入框的文本改变时的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtDotNetPostfix_TextChanged(object sender, EventArgs e)
        {
            this.SettingsChangedWarning();
        }

        /// <summary>
        /// 当'引用','存储路径'输入框的文本改变时的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rtbReferencesList_TextChanged_1(object sender, EventArgs e)
        {
            this.SettingsChangedWarning();
        }

        /// <summary>
        /// '应用设置'按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplySetting_Click(object sender, EventArgs e)
        {
            /*
             * * 改变内存中的用户设置信息
             * **/

            ToolSetting.Language = ".Net";
            ToolSetting.Postfix = this.txtDotNetPostfix.Text.Trim();

            ToolSetting.TabSize = Convert.ToInt32(this.txtTabSize.Text.Trim());
            ToolSetting.SavePath = this.txtSavePath.Text.Trim();
            ToolSetting.References = ToolSetting.FormatStringArray(
                    Regex.Split(this.rtbReferencesList.Text.Trim(), "\n+", RegexOptions.None));
            ToolSetting.OracleDataMapping = (DataTable)this.dgvOracleMapping.DataSource;

            /*
             * 保存用户设置信息
             * ***/
            if (ToolSetting.SaveUserSetting())
            {
                MessageBox.Show("用户设置信息保存成功!", "保存信息");
                this.ApplySettingsWarning();
            }
            else
            {
                MessageBox.Show("用户设置信息保存失败!", "保存信息");
                //重新加载用户设置信息到内存.
                ToolSetting.LoadUserSettings();
            }
        }
        /// <summary>
        /// Oracle'连接数据库'按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOracleConnectDb_Click(object sender, EventArgs e)
        {
            string server = this.txtOracleServer.Text.Trim();
            string uid = this.txtOracleUserID.Text.Trim();
            string pwd = this.txtOraclePwd.Text.Trim();

            //ToolSetting.Language = ".Net";
            //ToolSetting.Postfix = this.txtDotNetPostfix.Text.Trim();

            //ToolSetting.TabSize = Convert.ToInt32(this.txtTabSize.Text.Trim());
            //ToolSetting.SavePath = this.txtSavePath.Text.Trim();
            //ToolSetting.References = ToolSetting.FormatStringArray(
            //        Regex.Split(this.rtbReferencesList.Text.Trim(), "\n+", RegexOptions.None));
            //ToolSetting.OracleDataMapping = (DataTable)this.dgvOracleMapping.DataSource;
            ToolSetting.ServiceName = server;
            ToolSetting.UserName = uid;
            ToolSetting.UserPassWord = pwd;
            /*
             * 保存用户设置信息
             * ***/
            ToolSetting.SaveUserSetting();
            //得到数据库参数.


            //相关验证.
            if (server.Equals("") || uid.Equals(""))
            {
                MessageBox.Show("服务名和用户名均不能为空!"
                                , "警告"
                                , MessageBoxButtons.OK
                                , MessageBoxIcon.Warning);
                return;
            }

            //得到连接字符串.
            string conStr = ConnectOracle.GetConnectionStr(server, uid, pwd);

            //访问数据库.
            ConnectOracle.OpenConnection(conStr);
            DataTable tabs = ConnectOracle.GetAllTableAndViewName();
            ConnectOracle.CloseConnection();

            //必要的验证.
            if (tabs == null)
            {
                return;
            }

            //得到所有表和视图的结构信息.
            //this.GetOracleTablesStructure(tabs);

            //显示所有的表和视图的名称.
            if (tabs != null)
            {
                this.clbTablesAndView.Items.Clear();
                for (int i = 0; i < tabs.Rows.Count; i++)
                {
                    string type = tabs.Rows[i][1].ToString().Trim().ToUpper();
                    string temp = null;
                    if (type.Equals("VIEW"))
                    {
                        temp = "@ ";
                    }
                    else
                    {
                        temp = "";
                    }
                    this.clbTablesAndView.Items
                        .Add(temp + tabs.Rows[i][0].ToString(), false);
                }
            }

            //显示第一个表或视图的结构信息.
            if (this.clbTablesAndView.Items.Count > 0)
            {
                this.clbTablesAndView.SelectedIndex = 0;
                string tabName = this.GetTableOrViewName
                                (this.clbTablesAndView.Items[0].ToString());
                this.lblTabStructure.Text = "表" + tabName + "相关实体类结构:";
                this.FormatDgvTableStructure();
            }
            this.lblTabList.Text = "Oracle表/视图:";

        }

        /// <summary>
        /// '显示用户表'按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShowTables_Click(object sender, EventArgs e)
        {

        }
        /// <summary>
        /// '表和视图列表框'的选择改变事件．
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void clbTablesAndView_SelectedIndexChanged(object sender, EventArgs e)
        {
            string name = this.clbTablesAndView
                       .Items[this.clbTablesAndView.SelectedIndex].ToString();
            string tabName = this.GetTableOrViewName(name);
            if (name.StartsWith("@"))
            {
                this.lblTabStructure.Text = "视图" + tabName + "相关实体类结构:";
            }
            else
            {
                this.lblTabStructure.Text = "表" + tabName + "相关实体类结构:";
            }
            if (!this._clasInfo.Contains(tabName))
            {
                DataTable tabStructure = null;
                ConnectOracle.OpenConnection();
                tabStructure = ConnectOracle.ConvertTableOrViewStructure
                               (ConnectOracle.GetTableOrViewStructure(tabName));
                ConnectOracle.CloseConnection();
                this._clasInfo.Add(tabName, tabStructure);
                DataTable tabNameDt = ConnectOracle.GetDataTable(String.Format(@"select COMMENTS from user_tab_comments WHERE TABLE_NAME = '{0}'", tabName));
                if (tabNameDt.Rows.Count > 0)
                {
                    this._clasRemarkInfo.Add(tabName, tabNameDt.Rows[0]["COMMENTS"].ToString());

                }
                else
                {
                    this._clasRemarkInfo.Add(tabName, "");
                }

            }
            DataTable data = (DataTable)this._clasInfo[tabName];
            this.dgvTableOrViewStructure.DataSource = data;

            //决定是否保存类注释.
            if (this._saveClassRemark)
            {
                this._clasRemarkInfo[this._tabName] = this.txtTabControlOneClassRemark.Text;
                this._saveClassRemark = false;
            }

            //决定是否保存字段注释.
            if (this._saveFieldRemark)
            {
                DataTable data2 = (DataTable)this._clasInfo[this._tabName2];
                data2.Rows[this._index][2] = this.txtTabControlOneFieldInfo.Text;
                this._saveFieldRemark = false;
            }

            this.txtTabControlOneClassRemark.Text = this._clasRemarkInfo[tabName].ToString();
            int index = this.dgvTableOrViewStructure.CurrentRow.Index;
            if (data.Rows.Count > 0)
            {
                this.txtTabControlOneFieldInfo.Text = data.Rows[index][2].ToString();
            }

            //更改类注释标签文本.
            string className = GeneratorTool.CapFirstLetter(tabName)
                             + ToolSetting.Postfix;
            this.lblClassRemark.Text = className + "类注释:";
        }

        /// <summary>
        /// '表和视图结构显示＇控件的单击事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTableOrViewStructure_Click(object sender, EventArgs e)
        {
            if (this.clbTablesAndView.SelectedIndex < 0)
            {
                return;
            }
            string tabName = this.GetTableOrViewName
                  (this.clbTablesAndView.Items[this.clbTablesAndView.SelectedIndex].ToString());
            DataTable data = (DataTable)this._clasInfo[tabName];
            int index = this.dgvTableOrViewStructure.CurrentRow.Index;

            if (this._saveFieldRemark)
            {
                data.Rows[this._index][2] = this.txtTabControlOneFieldInfo.Text;
                this._saveFieldRemark = false;
            }
            this.txtTabControlOneFieldInfo.Text = data.Rows[index][2].ToString();

            //更改字段注释文本.

        }

        /// <summary>
        /// '字段注释输入框'获得焦点的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTabControlOneFieldInfo_Enter(object sender, EventArgs e)
        {
            if (this.dgvTableOrViewStructure.CurrentRow == null)
            {
                return;
            }

            if (this.clbTablesAndView.SelectedIndex < 0)
            {
                return;
            }

            //标示将要保存字段注释信息的位置.
            this._index = this.dgvTableOrViewStructure.CurrentRow.Index;
            this._tabName2 = this.GetTableOrViewName
               (this.clbTablesAndView.Items[this.clbTablesAndView.SelectedIndex].ToString());
            this._saveFieldRemark = true;
        }

        /// <summary>
        /// '类注释输入框'获得焦点的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTabControlOneClassRemark_Enter(object sender, EventArgs e)
        {
            if (this.clbTablesAndView.SelectedIndex < 0)
            {
                return;
            }
            //标示将要保存类注释信息的位置.
            this._tabName = this.GetTableOrViewName
                (this.clbTablesAndView.Items[this.clbTablesAndView.SelectedIndex].ToString());
            this._saveClassRemark = true;
        }

        /// <summary>
        /// '从数据库生成实体类'按钮的事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGeneratedFromDb_Click(object sender, EventArgs e)
        {
            //相关验证.
            if (this.txtTabControlOneNamespace.Text.Trim().Equals(""))
            {
                MessageBox.Show("命名空间名/包名不能为空!");
                return;
            }
            if (this._clasInfo.Count <= 0)
            {
                MessageBox.Show("请先选择数据库表或视图!");
                return;
            }

            //得到用户选择的所有的表或视图名.
            CheckedListBox.CheckedItemCollection checkedItems =
                                                 this.clbTablesAndView.CheckedItems;

            //得到用户输入的命名空间名.
            string namespaceName = this.txtTabControlOneNamespace.Text.Trim();

            //初始化进度条.
            this.pbGeneratorProgress.Minimum = 0;
            this.pbGeneratorProgress.Maximum = checkedItems.Count;
            this.pbGeneratorProgress.Step = 1;
            this.pbGeneratorProgress.Value = 0;
            this.lblIsGenerating.Visible = true; ;
            this.pbGeneratorProgress.Visible = true;


            bool DalCheck = this.DalcheckBox.Checked;
            bool ControllerCheck = this.ControllercheckBox.Checked;
            //生成实体类.
            foreach (object item in checkedItems)
            {
                string tabName = this.GetTableOrViewName(item.ToString());


                using (EntityClassGenrator dotNet =
                        new EntityClassGenrator(namespaceName,
                                                ToolSetting.References,
                                                tabName,
                                                this._clasRemarkInfo[tabName].ToString(),
                                                (DataTable)this._clasInfo[tabName]))
                {
                    dotNet.Save(ToolSetting.SavePath,
                        GeneratorTool.ChartConversion(tabName) + ToolSetting.Postfix + ".cs");
                }
                if (DalCheck)
                {
                    using (EntityDALGenrator dotNet =
                        new EntityDALGenrator(namespaceName,
                                                ToolSetting.DALReferences,
                                                tabName,
                                                this._clasRemarkInfo[tabName].ToString(),
                                                (DataTable)this._clasInfo[tabName]))
                    {
                        dotNet.Save(ToolSetting.SavePath,
                            GeneratorTool.ChartConversion(tabName) + ToolSetting.Postfix + "DAL.cs");
                    }
                    using (EntityIDALGenrator dotNet =
                        new EntityIDALGenrator(namespaceName,
                                                ToolSetting.IDALReferences,
                                                tabName,
                                                this._clasRemarkInfo[tabName].ToString(),
                                                (DataTable)this._clasInfo[tabName]))
                    {
                        dotNet.Save(ToolSetting.SavePath,
                            "I" + GeneratorTool.ChartConversion(tabName) + ToolSetting.Postfix + "DAL.cs");
                    }
                }
                if (ControllerCheck)
                {
                    using (ControllerGenrator dotNet =
                      new ControllerGenrator(namespaceName,
                                              ToolSetting.ControllerReferences,
                                              tabName,
                                              this._clasRemarkInfo[tabName].ToString(),
                                              (DataTable)this._clasInfo[tabName]))
                    {
                        dotNet.Save(ToolSetting.SavePath,
                            GeneratorTool.ChartConversion(tabName.Replace("_", "")) + ToolSetting.Postfix + "Controller.cs");
                    }
                }
                this.pbGeneratorProgress.Increment(1);
            }

            this.lblIsGenerating.Visible = false;
            this.pbGeneratorProgress.Visible = false;
            MessageBox.Show("实体类已全部生成成功!");


            //打开生成的实体类文件所在的文件夹.
            Process.Start(ToolSetting.SavePath);
        }
        /// <summary>
        /// 当手动添加字段名时,验证新的字段名是否已存在．
        /// </summary>
        /// <param name="newField">新字段名</param>
        /// <returns>是否存在（ture/false）</returns>
        private bool ExistNewField(string newField)
        {
            for (int i = 0; i < this._fieldsInfo.Rows.Count; i++)
            {
                DataRow row = this._fieldsInfo.Rows[i];
                if (row[0].ToString().Equals(newField))
                {
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// 本方法用于初始化用户设置的相关控件内容.
        /// </summary>
        private void InitUserSettingControl()
        {

            this.txtDotNetPostfix.Text = ToolSetting.Postfix;
            this.lblNamespace1.Text = "命名空间名:";


            this.txtTabSize.Text = ToolSetting.TabSize.ToString();

            for (int i = 0; i < ToolSetting.References.Length; i++)
            {
                this.rtbReferencesList.Text += ToolSetting.References[i] + "\n";
            }
            this.txtOracleServer.Text = ToolSetting.ServiceName;
            this.txtOracleUserID.Text = ToolSetting.UserName;
            this.txtOraclePwd.Text = ToolSetting.UserPassWord;

            this.txtSavePath.Text = ToolSetting.SavePath;


            this.dgvOracleMapping.DataSource = ToolSetting.OracleDataMapping;

            //控件样式的设置.


            this.dgvOracleMapping.Columns[0].Width = 145;
            this.dgvOracleMapping.Columns[1].Width = 150;
            this.dgvOracleMapping.RowHeadersWidth = 30;
            this.dgvOracleMapping.ColumnHeadersHeight = 30;
        }

        /// <summary>
        /// 当设置改变时,应该执行的代码.
        /// </summary>
        private void SettingsChangedWarning()
        {
            this._settingsChanged = true;
            this.btnApplySetting.ForeColor = Color.Red;
        }

        /// <summary>
        /// 当成功应用设置时,应该执行的代码.
        /// </summary>
        private void ApplySettingsWarning()
        {
            this._settingsChanged = false;
            this.btnApplySetting.ForeColor = Color.Black;
        }


        /// <summary>
        /// 得到SqlServer数据库中所有表的结构信息.
        /// </summary>
        /// <param name="dbName">数据库名称</param>
        /// <param name="tabs">数据库中的表信息</param>
        private void GetSqlServerTablesStructure(string dbName, DataTable tabs)
        {

        }

        /// <summary>
        /// 得到Oracle数据库中所有表的结构信息.
        /// </summary>        
        /// <param name="tabs">数据库中的表信息</param>
        private void GetOracleTablesStructure(DataTable tabs)
        {
            this._clasInfo.Clear();
            this._clasRemarkInfo.Clear();
            string tabName = null;
            DataTable tabStructure = null;
            //for (int i = 0; i < tabs.Rows.Count; i++)
            //{
            //    tabName = tabs.Rows[i][0].ToString();
            //    ConnectOracle.OpenConnection();
            //    tabStructure = ConnectOracle.ConvertTableOrViewStructure
            //                   (ConnectOracle.GetTableOrViewStructure(tabName));
            //    ConnectOracle.CloseConnection();
            //    this._clasInfo.Add(tabName, tabStructure);
            //    this._clasRemarkInfo.Add(tabName, "");
            //}
        }

        /// <summary>
        /// 要据列表项控件中的表或视图的名称,得到真实的表或视图的名称．
        /// </summary>
        /// <param name="source">列表项控件中的名称</param>
        /// <returns>实际名称</returns>
        private string GetTableOrViewName(string source)
        {
            if (source.StartsWith("@"))
            {
                return source.Substring(2);
            }
            else
            {
                return source;
            }
        }

        /// <summary>
        /// 控制第一个选项卡中'表结构信息显示控件'的外观.
        /// </summary>
        private void FormatDgvTableStructure()
        {
            this.dgvTableOrViewStructure.Columns[0].Width = 135;
            this.dgvTableOrViewStructure.Columns[1].Width = 138;
        }

        /// <summary>
        /// '全选'复选框的选择改变事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chbSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            //if (this.chbSelectAll.Checked)
            //{
            //    SelectOrUnselectAllTablesAndViews(true);
            //}
            //else
            //{
            //    SelectOrUnselectAllTablesAndViews(false);
            //}
        }

        /// <summary>
        /// 全选(true)或全不选(false)所有的表格和视图.
        /// </summary>
        /// <param name="select">选择状态</param>
        private void SelectOrUnselectAllTablesAndViews(bool select)
        {
            //得到列表中的所有的项.
            CheckedListBox.ObjectCollection collection = this.clbTablesAndView.Items;

            //将列表项内容缓存到数组中.
            string[] items = new string[collection.Count];
            int i = 0;
            foreach (Object item in collection)
            {
                items[i] = item.ToString();
                i++;
            }

            //清除列表中的内容.
            this.clbTablesAndView.Items.Clear();

            //将缓存的内容以指定的选中状态加载到列表中.
            foreach (Object item in items)
            {
                this.clbTablesAndView.Items.Add(item, select);
            }

            //默认选中第一项.
            if (this.clbTablesAndView.Items.Count > 0)
            {
                this.clbTablesAndView.SelectedIndex = 0;
            }
        }

        /// <summary>
        /// '表和视图结构'表格的选择改变事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dgvTableOrViewStructure_SelectionChanged(object sender, EventArgs e)
        {
            DataGridViewRow currentRow = this.dgvTableOrViewStructure.CurrentRow;
            if (currentRow == null)
            {
                return;
            }
            string fieldName = currentRow.Cells[0].Value.ToString();
            this.lblFieldRemark.Text = fieldName + "字段注释:";
        }

        /// <summary>
        /// 字段注释输入框的'Ctrl+上下键头(↑↓)'按下事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTabControlOneFieldInfo_KeyUp(object sender, KeyEventArgs e)
        {
            //必要的验证.
            if (this.dgvTableOrViewStructure.Rows == null ||
                this.dgvTableOrViewStructure.Rows.Count < 1 ||
                this.dgvTableOrViewStructure.CurrentRow == null)
            {
                return;
            }

            //得到当前选择行索引.
            int index = this.dgvTableOrViewStructure.CurrentRow.Index;
            if (index < 0)
            {
                return;
            }

            //得到当前选择行的上一行.
            int upIndex = 0;
            if (index > 0)
            {
                upIndex = index - 1;
            }

            //得到当前选择行的下一行.            
            int downIndex = this.dgvTableOrViewStructure.Rows.Count - 1;
            if (index != downIndex)
            {
                downIndex = index + 1;
            }

            //表示新行的矩形区域.
            Rectangle row = new Rectangle();

            //若用户按下Ctrl + ↑.
            if (e.Control == true && e.KeyCode == Keys.Up)
            {
                row = this.dgvTableOrViewStructure.GetRowDisplayRectangle(upIndex, true);
            }

            //若用户按下Ctrl + ↓.
            if (e.Control == true && e.KeyCode == Keys.Down)
            {
                row = this.dgvTableOrViewStructure.GetRowDisplayRectangle(downIndex, true);
            }


            //得到鼠标的点击位置.
            int x = this.Left + this.Padding.Left +
                    this.tbcMain.Left + this.tbcMain.Margin.Left +
                    this.dgvTableOrViewStructure.Left + this.dgvTableOrViewStructure.Margin.Left +
                    row.Left + 10;

            int y = this.Top + this.Padding.Top +
                    this.tbcMain.Top + this.tbcMain.Margin.Top +
                    this.dgvTableOrViewStructure.Top + this.dgvTableOrViewStructure.Margin.Top +
                    (row.Top + row.Bottom) / 2 + 35;

            //设置鼠标位置.
            WinAPIMethods.SetCursorPos(x, y);

            //鼠标左键按下.
            WinAPIMethods.mouse_event(WinAPIMethods.MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            //鼠标左键释放.
            WinAPIMethods.mouse_event(WinAPIMethods.MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);

            //模拟鼠标点击字段注释输入框.
            int xx = this.Left + this.Padding.Left +
                     this.tbcMain.Left + this.tbcMain.Margin.Left +
                     this.txtTabControlOneFieldInfo.Left + this.txtTabControlOneFieldInfo.Margin.Left +
                     30;
            int yy = this.Top + this.Padding.Top +
                     this.tbcMain.Top + this.tbcMain.Margin.Top +
                     this.txtTabControlOneFieldInfo.Top + this.txtTabControlOneFieldInfo.Margin.Top +
                     60;

            //设置鼠标位置.
            WinAPIMethods.SetCursorPos(xx, yy);

            //鼠标左键按下.
            WinAPIMethods.mouse_event(WinAPIMethods.MouseEventFlag.LeftDown, 0, 0, 0, UIntPtr.Zero);
            //鼠标左键释放.
            WinAPIMethods.mouse_event(WinAPIMethods.MouseEventFlag.LeftUp, 0, 0, 0, UIntPtr.Zero);

            //保存字段注释.
            this.txtTabControlOneFieldInfo_Enter(new object(), new EventArgs());

        }

        /// <summary>
        /// 类注释输入框的'Ctrl+上下键头(↑↓)'按下事件处理.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtTabControlOneClassRemark_KeyUp(object sender, KeyEventArgs e)
        {
            //必要的验证.
            if (this.clbTablesAndView.Items.Count <= 0)
            {
                return;
            }

            //得到当前选择项索引.
            int index = this.clbTablesAndView.SelectedIndex;
            if (index < 0)
            {
                return;
            }

            //得到当前选择项的上一项.
            int upIndex = 0;
            if (index > 0)
            {
                upIndex = index - 1;
            }

            //得到当前选择项的下一项.            
            int downIndex = this.clbTablesAndView.Items.Count - 1;
            if (index != downIndex)
            {
                downIndex = index + 1;
            }

            //若用户按下Ctrl + ↑.
            if (e.Control == true && e.KeyCode == Keys.Up)
            {
                this.clbTablesAndView.SelectedIndex = upIndex;
                //执行类注释输入框的获取焦点逻辑,以便于保存注释信息.
                this.txtTabControlOneClassRemark_Enter(new object(), new EventArgs());
                return;
            }

            //若用户按下Ctrl + ↓.
            if (e.Control == true && e.KeyCode == Keys.Down)
            {
                this.clbTablesAndView.SelectedIndex = downIndex;
                //执行类注释输入框的获取焦点逻辑,以便于保存注释信息.
                this.txtTabControlOneClassRemark_Enter(new object(), new EventArgs());
                return;
            }
        }

        private void txtOracleServer_TextChanged(object sender, EventArgs e)
        {

        }
    }
}