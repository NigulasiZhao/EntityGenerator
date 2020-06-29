namespace EntityGenerator.UI
{
    partial class EntityGenerator
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(EntityGenerator));
            this.tbcMain = new System.Windows.Forms.TabControl();
            this.tbpFromDB = new System.Windows.Forms.TabPage();
            this.lblIsGenerating = new System.Windows.Forms.Label();
            this.lblTabStructure = new System.Windows.Forms.Label();
            this.gpbOracle = new System.Windows.Forms.GroupBox();
            this.txtOraclePwd = new System.Windows.Forms.TextBox();
            this.txtOracleUserID = new System.Windows.Forms.TextBox();
            this.txtOracleServer = new System.Windows.Forms.TextBox();
            this.btnOracleConnectDb = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.lblTabList = new System.Windows.Forms.Label();
            this.btnGeneratedFromDb = new System.Windows.Forms.Button();
            this.txtTabControlOneClassRemark = new System.Windows.Forms.TextBox();
            this.txtTabControlOneFieldInfo = new System.Windows.Forms.TextBox();
            this.lblClassRemark = new System.Windows.Forms.Label();
            this.lblFieldRemark = new System.Windows.Forms.Label();
            this.dgvTableOrViewStructure = new System.Windows.Forms.DataGridView();
            this.clbTablesAndView = new System.Windows.Forms.CheckedListBox();
            this.txtTabControlOneNamespace = new System.Windows.Forms.TextBox();
            this.lblNamespace1 = new System.Windows.Forms.Label();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.pbGeneratorProgress = new System.Windows.Forms.ProgressBar();
            this.rdbOracle = new System.Windows.Forms.RadioButton();
            this.tbpSet = new System.Windows.Forms.TabPage();
            this.tbcDataTypeMapping = new System.Windows.Forms.TabControl();
            this.tbpOracle = new System.Windows.Forms.TabPage();
            this.dgvOracleMapping = new System.Windows.Forms.DataGridView();
            this.cmsEditDataTypeMapping = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiAdd = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiChange = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.btnApplySetting = new System.Windows.Forms.Button();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.txtSavePath = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.rtbReferencesList = new System.Windows.Forms.RichTextBox();
            this.label9 = new System.Windows.Forms.Label();
            this.txtTabSize = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtDotNetPostfix = new System.Windows.Forms.TextBox();
            this.lblDotNetPostfix = new System.Windows.Forms.Label();
            this.fbdSavePath = new System.Windows.Forms.FolderBrowserDialog();
            this.ofdSelectAccessDbFile = new System.Windows.Forms.OpenFileDialog();
            this.tbcMain.SuspendLayout();
            this.tbpFromDB.SuspendLayout();
            this.gpbOracle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableOrViewStructure)).BeginInit();
            this.groupBox3.SuspendLayout();
            this.tbpSet.SuspendLayout();
            this.tbcDataTypeMapping.SuspendLayout();
            this.tbpOracle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvOracleMapping)).BeginInit();
            this.cmsEditDataTypeMapping.SuspendLayout();
            this.SuspendLayout();
            // 
            // tbcMain
            // 
            this.tbcMain.Controls.Add(this.tbpFromDB);
            this.tbcMain.Controls.Add(this.tbpSet);
            this.tbcMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tbcMain.Location = new System.Drawing.Point(0, 0);
            this.tbcMain.Name = "tbcMain";
            this.tbcMain.SelectedIndex = 0;
            this.tbcMain.Size = new System.Drawing.Size(771, 532);
            this.tbcMain.TabIndex = 0;
            this.tbcMain.SelectedIndexChanged += new System.EventHandler(this.tbcMain_SelectedIndexChanged);
            // 
            // tbpFromDB
            // 
            this.tbpFromDB.Controls.Add(this.lblIsGenerating);
            this.tbpFromDB.Controls.Add(this.lblTabStructure);
            this.tbpFromDB.Controls.Add(this.gpbOracle);
            this.tbpFromDB.Controls.Add(this.lblTabList);
            this.tbpFromDB.Controls.Add(this.btnGeneratedFromDb);
            this.tbpFromDB.Controls.Add(this.txtTabControlOneClassRemark);
            this.tbpFromDB.Controls.Add(this.txtTabControlOneFieldInfo);
            this.tbpFromDB.Controls.Add(this.lblClassRemark);
            this.tbpFromDB.Controls.Add(this.lblFieldRemark);
            this.tbpFromDB.Controls.Add(this.dgvTableOrViewStructure);
            this.tbpFromDB.Controls.Add(this.clbTablesAndView);
            this.tbpFromDB.Controls.Add(this.txtTabControlOneNamespace);
            this.tbpFromDB.Controls.Add(this.lblNamespace1);
            this.tbpFromDB.Controls.Add(this.groupBox3);
            this.tbpFromDB.Location = new System.Drawing.Point(4, 22);
            this.tbpFromDB.Name = "tbpFromDB";
            this.tbpFromDB.Size = new System.Drawing.Size(763, 506);
            this.tbpFromDB.TabIndex = 2;
            this.tbpFromDB.Tag = "FromDB";
            this.tbpFromDB.Text = "从数据库";
            this.tbpFromDB.UseVisualStyleBackColor = true;
            // 
            // lblIsGenerating
            // 
            this.lblIsGenerating.AutoSize = true;
            this.lblIsGenerating.BackColor = System.Drawing.Color.Transparent;
            this.lblIsGenerating.ForeColor = System.Drawing.Color.Red;
            this.lblIsGenerating.Location = new System.Drawing.Point(378, 13);
            this.lblIsGenerating.Name = "lblIsGenerating";
            this.lblIsGenerating.Size = new System.Drawing.Size(149, 12);
            this.lblIsGenerating.TabIndex = 4;
            this.lblIsGenerating.Text = "正在生成实体类,请稍后...";
            // 
            // lblTabStructure
            // 
            this.lblTabStructure.AutoSize = true;
            this.lblTabStructure.ForeColor = System.Drawing.Color.Blue;
            this.lblTabStructure.Location = new System.Drawing.Point(207, 210);
            this.lblTabStructure.Name = "lblTabStructure";
            this.lblTabStructure.Size = new System.Drawing.Size(107, 12);
            this.lblTabStructure.TabIndex = 14;
            this.lblTabStructure.Text = "表相关实体类结构:";
            // 
            // gpbOracle
            // 
            this.gpbOracle.Controls.Add(this.txtOraclePwd);
            this.gpbOracle.Controls.Add(this.txtOracleUserID);
            this.gpbOracle.Controls.Add(this.txtOracleServer);
            this.gpbOracle.Controls.Add(this.btnOracleConnectDb);
            this.gpbOracle.Controls.Add(this.label7);
            this.gpbOracle.Controls.Add(this.label6);
            this.gpbOracle.Controls.Add(this.label1);
            this.gpbOracle.Location = new System.Drawing.Point(32, 69);
            this.gpbOracle.Name = "gpbOracle";
            this.gpbOracle.Size = new System.Drawing.Size(690, 91);
            this.gpbOracle.TabIndex = 12;
            this.gpbOracle.TabStop = false;
            this.gpbOracle.Text = "数据库参数";
            // 
            // txtOraclePwd
            // 
            this.txtOraclePwd.Location = new System.Drawing.Point(367, 55);
            this.txtOraclePwd.Name = "txtOraclePwd";
            this.txtOraclePwd.PasswordChar = '#';
            this.txtOraclePwd.Size = new System.Drawing.Size(177, 21);
            this.txtOraclePwd.TabIndex = 6;
            // 
            // txtOracleUserID
            // 
            this.txtOracleUserID.Location = new System.Drawing.Point(89, 57);
            this.txtOracleUserID.Name = "txtOracleUserID";
            this.txtOracleUserID.Size = new System.Drawing.Size(203, 21);
            this.txtOracleUserID.TabIndex = 5;
            // 
            // txtOracleServer
            // 
            this.txtOracleServer.Location = new System.Drawing.Point(89, 24);
            this.txtOracleServer.Name = "txtOracleServer";
            this.txtOracleServer.Size = new System.Drawing.Size(203, 21);
            this.txtOracleServer.TabIndex = 4;
            // 
            // btnOracleConnectDb
            // 
            this.btnOracleConnectDb.Location = new System.Drawing.Point(583, 53);
            this.btnOracleConnectDb.Name = "btnOracleConnectDb";
            this.btnOracleConnectDb.Size = new System.Drawing.Size(90, 23);
            this.btnOracleConnectDb.TabIndex = 3;
            this.btnOracleConnectDb.Text = "连接数据库";
            this.btnOracleConnectDb.UseVisualStyleBackColor = true;
            this.btnOracleConnectDb.Click += new System.EventHandler(this.btnOracleConnectDb_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(326, 60);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(35, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "密码:";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(29, 60);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(47, 12);
            this.label6.TabIndex = 1;
            this.label6.Text = "用户名:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(29, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "服务名:";
            // 
            // lblTabList
            // 
            this.lblTabList.AutoSize = true;
            this.lblTabList.ForeColor = System.Drawing.Color.Blue;
            this.lblTabList.Location = new System.Drawing.Point(31, 210);
            this.lblTabList.Name = "lblTabList";
            this.lblTabList.Size = new System.Drawing.Size(89, 12);
            this.lblTabList.TabIndex = 13;
            this.lblTabList.Text = "Oracle表/视图:";
            // 
            // btnGeneratedFromDb
            // 
            this.btnGeneratedFromDb.Location = new System.Drawing.Point(610, 451);
            this.btnGeneratedFromDb.Name = "btnGeneratedFromDb";
            this.btnGeneratedFromDb.Size = new System.Drawing.Size(113, 29);
            this.btnGeneratedFromDb.TabIndex = 10;
            this.btnGeneratedFromDb.Text = "生成实体类(&G)";
            this.btnGeneratedFromDb.UseVisualStyleBackColor = true;
            this.btnGeneratedFromDb.Click += new System.EventHandler(this.btnGeneratedFromDb_Click);
            // 
            // txtTabControlOneClassRemark
            // 
            this.txtTabControlOneClassRemark.BackColor = System.Drawing.Color.FloralWhite;
            this.txtTabControlOneClassRemark.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTabControlOneClassRemark.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTabControlOneClassRemark.Location = new System.Drawing.Point(526, 348);
            this.txtTabControlOneClassRemark.Multiline = true;
            this.txtTabControlOneClassRemark.Name = "txtTabControlOneClassRemark";
            this.txtTabControlOneClassRemark.ReadOnly = true;
            this.txtTabControlOneClassRemark.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txtTabControlOneClassRemark.Size = new System.Drawing.Size(197, 88);
            this.txtTabControlOneClassRemark.TabIndex = 9;
            this.txtTabControlOneClassRemark.Enter += new System.EventHandler(this.txtTabControlOneClassRemark_Enter);
            this.txtTabControlOneClassRemark.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTabControlOneClassRemark_KeyUp);
            // 
            // txtTabControlOneFieldInfo
            // 
            this.txtTabControlOneFieldInfo.BackColor = System.Drawing.Color.FloralWhite;
            this.txtTabControlOneFieldInfo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtTabControlOneFieldInfo.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTabControlOneFieldInfo.Location = new System.Drawing.Point(526, 225);
            this.txtTabControlOneFieldInfo.Multiline = true;
            this.txtTabControlOneFieldInfo.Name = "txtTabControlOneFieldInfo";
            this.txtTabControlOneFieldInfo.ReadOnly = true;
            this.txtTabControlOneFieldInfo.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtTabControlOneFieldInfo.Size = new System.Drawing.Size(197, 86);
            this.txtTabControlOneFieldInfo.TabIndex = 8;
            this.txtTabControlOneFieldInfo.Enter += new System.EventHandler(this.txtTabControlOneFieldInfo_Enter);
            this.txtTabControlOneFieldInfo.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtTabControlOneFieldInfo_KeyUp);
            // 
            // lblClassRemark
            // 
            this.lblClassRemark.AutoSize = true;
            this.lblClassRemark.ForeColor = System.Drawing.Color.Blue;
            this.lblClassRemark.Location = new System.Drawing.Point(524, 333);
            this.lblClassRemark.Name = "lblClassRemark";
            this.lblClassRemark.Size = new System.Drawing.Size(53, 12);
            this.lblClassRemark.TabIndex = 7;
            this.lblClassRemark.Text = "类注释：";
            // 
            // lblFieldRemark
            // 
            this.lblFieldRemark.AutoSize = true;
            this.lblFieldRemark.ForeColor = System.Drawing.Color.Blue;
            this.lblFieldRemark.Location = new System.Drawing.Point(524, 210);
            this.lblFieldRemark.Name = "lblFieldRemark";
            this.lblFieldRemark.Size = new System.Drawing.Size(65, 12);
            this.lblFieldRemark.TabIndex = 6;
            this.lblFieldRemark.Text = "字段注释：";
            // 
            // dgvTableOrViewStructure
            // 
            this.dgvTableOrViewStructure.AllowUserToAddRows = false;
            this.dgvTableOrViewStructure.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgvTableOrViewStructure.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTableOrViewStructure.Location = new System.Drawing.Point(209, 225);
            this.dgvTableOrViewStructure.MultiSelect = false;
            this.dgvTableOrViewStructure.Name = "dgvTableOrViewStructure";
            this.dgvTableOrViewStructure.ReadOnly = true;
            this.dgvTableOrViewStructure.RowHeadersWidth = 25;
            this.dgvTableOrViewStructure.RowTemplate.Height = 23;
            this.dgvTableOrViewStructure.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvTableOrViewStructure.Size = new System.Drawing.Size(297, 255);
            this.dgvTableOrViewStructure.TabIndex = 5;
            this.dgvTableOrViewStructure.SelectionChanged += new System.EventHandler(this.dgvTableOrViewStructure_SelectionChanged);
            this.dgvTableOrViewStructure.Click += new System.EventHandler(this.dgvTableOrViewStructure_Click);
            // 
            // clbTablesAndView
            // 
            this.clbTablesAndView.BackColor = System.Drawing.Color.FloralWhite;
            this.clbTablesAndView.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.clbTablesAndView.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.clbTablesAndView.FormattingEnabled = true;
            this.clbTablesAndView.Location = new System.Drawing.Point(32, 229);
            this.clbTablesAndView.Name = "clbTablesAndView";
            this.clbTablesAndView.Size = new System.Drawing.Size(159, 249);
            this.clbTablesAndView.TabIndex = 4;
            this.clbTablesAndView.SelectedIndexChanged += new System.EventHandler(this.clbTablesAndView_SelectedIndexChanged);
            // 
            // txtTabControlOneNamespace
            // 
            this.txtTabControlOneNamespace.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTabControlOneNamespace.Location = new System.Drawing.Point(121, 173);
            this.txtTabControlOneNamespace.Name = "txtTabControlOneNamespace";
            this.txtTabControlOneNamespace.Size = new System.Drawing.Size(385, 24);
            this.txtTabControlOneNamespace.TabIndex = 3;
            // 
            // lblNamespace1
            // 
            this.lblNamespace1.AutoSize = true;
            this.lblNamespace1.Location = new System.Drawing.Point(30, 179);
            this.lblNamespace1.Name = "lblNamespace1";
            this.lblNamespace1.Size = new System.Drawing.Size(59, 12);
            this.lblNamespace1.TabIndex = 2;
            this.lblNamespace1.Text = "命名空间:";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.pbGeneratorProgress);
            this.groupBox3.Controls.Add(this.rdbOracle);
            this.groupBox3.Location = new System.Drawing.Point(32, 13);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(691, 50);
            this.groupBox3.TabIndex = 0;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "数据库类型";
            // 
            // pbGeneratorProgress
            // 
            this.pbGeneratorProgress.BackColor = System.Drawing.SystemColors.MenuBar;
            this.pbGeneratorProgress.ForeColor = System.Drawing.Color.Red;
            this.pbGeneratorProgress.Location = new System.Drawing.Point(349, 20);
            this.pbGeneratorProgress.Name = "pbGeneratorProgress";
            this.pbGeneratorProgress.Size = new System.Drawing.Size(312, 16);
            this.pbGeneratorProgress.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            this.pbGeneratorProgress.TabIndex = 3;
            this.pbGeneratorProgress.Visible = false;
            // 
            // rdbOracle
            // 
            this.rdbOracle.AutoSize = true;
            this.rdbOracle.Location = new System.Drawing.Point(17, 20);
            this.rdbOracle.Name = "rdbOracle";
            this.rdbOracle.Size = new System.Drawing.Size(59, 16);
            this.rdbOracle.TabIndex = 2;
            this.rdbOracle.Text = "Oracle";
            this.rdbOracle.UseVisualStyleBackColor = true;
            this.rdbOracle.CheckedChanged += new System.EventHandler(this.radOracle_CheckedChanged);
            // 
            // tbpSet
            // 
            this.tbpSet.Controls.Add(this.tbcDataTypeMapping);
            this.tbpSet.Controls.Add(this.btnApplySetting);
            this.tbpSet.Controls.Add(this.btnBrowse);
            this.tbpSet.Controls.Add(this.txtSavePath);
            this.tbpSet.Controls.Add(this.label12);
            this.tbpSet.Controls.Add(this.label10);
            this.tbpSet.Controls.Add(this.rtbReferencesList);
            this.tbpSet.Controls.Add(this.label9);
            this.tbpSet.Controls.Add(this.txtTabSize);
            this.tbpSet.Controls.Add(this.label8);
            this.tbpSet.Controls.Add(this.txtDotNetPostfix);
            this.tbpSet.Controls.Add(this.lblDotNetPostfix);
            this.tbpSet.Location = new System.Drawing.Point(4, 22);
            this.tbpSet.Name = "tbpSet";
            this.tbpSet.Padding = new System.Windows.Forms.Padding(3);
            this.tbpSet.Size = new System.Drawing.Size(763, 506);
            this.tbpSet.TabIndex = 1;
            this.tbpSet.Tag = "Set";
            this.tbpSet.Text = "系统设置";
            this.tbpSet.UseVisualStyleBackColor = true;
            // 
            // tbcDataTypeMapping
            // 
            this.tbcDataTypeMapping.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tbcDataTypeMapping.Controls.Add(this.tbpOracle);
            this.tbcDataTypeMapping.Location = new System.Drawing.Point(394, 43);
            this.tbcDataTypeMapping.Name = "tbcDataTypeMapping";
            this.tbcDataTypeMapping.SelectedIndex = 0;
            this.tbcDataTypeMapping.Size = new System.Drawing.Size(330, 379);
            this.tbcDataTypeMapping.TabIndex = 17;
            // 
            // tbpOracle
            // 
            this.tbpOracle.Controls.Add(this.dgvOracleMapping);
            this.tbpOracle.Location = new System.Drawing.Point(4, 25);
            this.tbpOracle.Name = "tbpOracle";
            this.tbpOracle.Size = new System.Drawing.Size(322, 350);
            this.tbpOracle.TabIndex = 2;
            this.tbpOracle.Tag = "Oracle";
            this.tbpOracle.Text = "Oracle";
            this.tbpOracle.UseVisualStyleBackColor = true;
            // 
            // dgvOracleMapping
            // 
            this.dgvOracleMapping.AllowUserToAddRows = false;
            this.dgvOracleMapping.BackgroundColor = System.Drawing.Color.FloralWhite;
            this.dgvOracleMapping.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvOracleMapping.ContextMenuStrip = this.cmsEditDataTypeMapping;
            this.dgvOracleMapping.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvOracleMapping.Location = new System.Drawing.Point(0, 0);
            this.dgvOracleMapping.MultiSelect = false;
            this.dgvOracleMapping.Name = "dgvOracleMapping";
            this.dgvOracleMapping.ReadOnly = true;
            this.dgvOracleMapping.RowTemplate.Height = 23;
            this.dgvOracleMapping.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvOracleMapping.Size = new System.Drawing.Size(322, 350);
            this.dgvOracleMapping.TabIndex = 0;
            // 
            // cmsEditDataTypeMapping
            // 
            this.cmsEditDataTypeMapping.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiAdd,
            this.tsmiChange,
            this.tsmiDelete});
            this.cmsEditDataTypeMapping.Name = "cmsEditDataTypeMapping";
            this.cmsEditDataTypeMapping.Size = new System.Drawing.Size(118, 70);
            // 
            // tsmiAdd
            // 
            this.tsmiAdd.Name = "tsmiAdd";
            this.tsmiAdd.Size = new System.Drawing.Size(117, 22);
            this.tsmiAdd.Text = "添加(&A)";
            this.tsmiAdd.Click += new System.EventHandler(this.tsmiAdd_Click);
            // 
            // tsmiChange
            // 
            this.tsmiChange.Name = "tsmiChange";
            this.tsmiChange.Size = new System.Drawing.Size(117, 22);
            this.tsmiChange.Text = "修改(&E)";
            this.tsmiChange.Click += new System.EventHandler(this.tsmiChange_Click);
            // 
            // tsmiDelete
            // 
            this.tsmiDelete.Name = "tsmiDelete";
            this.tsmiDelete.Size = new System.Drawing.Size(117, 22);
            this.tsmiDelete.Text = "删除(&D)";
            this.tsmiDelete.Click += new System.EventHandler(this.tsmiDelete_Click);
            // 
            // btnApplySetting
            // 
            this.btnApplySetting.Location = new System.Drawing.Point(621, 440);
            this.btnApplySetting.Name = "btnApplySetting";
            this.btnApplySetting.Size = new System.Drawing.Size(96, 23);
            this.btnApplySetting.TabIndex = 16;
            this.btnApplySetting.Text = "应用设置(&S)";
            this.btnApplySetting.UseVisualStyleBackColor = true;
            this.btnApplySetting.Click += new System.EventHandler(this.btnApplySetting_Click);
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(511, 440);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 13;
            this.btnBrowse.Text = "浏览...";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // txtSavePath
            // 
            this.txtSavePath.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtSavePath.Location = new System.Drawing.Point(106, 439);
            this.txtSavePath.Name = "txtSavePath";
            this.txtSavePath.Size = new System.Drawing.Size(399, 24);
            this.txtSavePath.TabIndex = 12;
            this.txtSavePath.Text = "C:/实体类";
            this.txtSavePath.TextChanged += new System.EventHandler(this.rtbReferencesList_TextChanged_1);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(40, 445);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(59, 12);
            this.label12.TabIndex = 11;
            this.label12.Text = "存储路径:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(392, 23);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(89, 12);
            this.label10.TabIndex = 10;
            this.label10.Text = "数据类型映射：";
            // 
            // rtbReferencesList
            // 
            this.rtbReferencesList.AcceptsTab = true;
            this.rtbReferencesList.BackColor = System.Drawing.Color.FloralWhite;
            this.rtbReferencesList.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.rtbReferencesList.Location = new System.Drawing.Point(43, 142);
            this.rtbReferencesList.Name = "rtbReferencesList";
            this.rtbReferencesList.ScrollBars = System.Windows.Forms.RichTextBoxScrollBars.Vertical;
            this.rtbReferencesList.Size = new System.Drawing.Size(305, 186);
            this.rtbReferencesList.TabIndex = 9;
            this.rtbReferencesList.Text = "";
            this.rtbReferencesList.TextChanged += new System.EventHandler(this.rtbReferencesList_TextChanged_1);
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(41, 117);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(65, 12);
            this.label9.TabIndex = 8;
            this.label9.Text = "添加引用：";
            // 
            // txtTabSize
            // 
            this.txtTabSize.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtTabSize.Location = new System.Drawing.Point(177, 65);
            this.txtTabSize.Name = "txtTabSize";
            this.txtTabSize.Size = new System.Drawing.Size(107, 24);
            this.txtTabSize.TabIndex = 6;
            this.txtTabSize.Text = "8";
            this.txtTabSize.TextChanged += new System.EventHandler(this.txtDotNetPostfix_TextChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(41, 71);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(119, 12);
            this.label8.TabIndex = 5;
            this.label8.Text = "制表符大小(空格数):";
            // 
            // txtDotNetPostfix
            // 
            this.txtDotNetPostfix.Font = new System.Drawing.Font("宋体", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtDotNetPostfix.Location = new System.Drawing.Point(177, 17);
            this.txtDotNetPostfix.Name = "txtDotNetPostfix";
            this.txtDotNetPostfix.Size = new System.Drawing.Size(108, 24);
            this.txtDotNetPostfix.TabIndex = 3;
            this.txtDotNetPostfix.Text = "Entity";
            this.txtDotNetPostfix.TextChanged += new System.EventHandler(this.txtDotNetPostfix_TextChanged);
            // 
            // lblDotNetPostfix
            // 
            this.lblDotNetPostfix.AutoSize = true;
            this.lblDotNetPostfix.Location = new System.Drawing.Point(40, 23);
            this.lblDotNetPostfix.Name = "lblDotNetPostfix";
            this.lblDotNetPostfix.Size = new System.Drawing.Size(107, 12);
            this.lblDotNetPostfix.TabIndex = 1;
            this.lblDotNetPostfix.Text = ".Net实体类名后缀:";
            // 
            // ofdSelectAccessDbFile
            // 
            this.ofdSelectAccessDbFile.Filter = "Access2003(*.mdb)|*.mdb|Access2007(*.accdb)|*.accdb|所有文件(*.*)|(*.*)";
            this.ofdSelectAccessDbFile.InitialDirectory = "c:/";
            this.ofdSelectAccessDbFile.Title = "请选择Access数据库文件";
            // 
            // EntityGenerator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(771, 532);
            this.Controls.Add(this.tbcMain);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "EntityGenerator";
            this.Text = "实体类生成器";
            this.Load += new System.EventHandler(this.EntityClassGenerator_Load);
            this.tbcMain.ResumeLayout(false);
            this.tbpFromDB.ResumeLayout(false);
            this.tbpFromDB.PerformLayout();
            this.gpbOracle.ResumeLayout(false);
            this.gpbOracle.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTableOrViewStructure)).EndInit();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.tbpSet.ResumeLayout(false);
            this.tbpSet.PerformLayout();
            this.tbcDataTypeMapping.ResumeLayout(false);
            this.tbpOracle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvOracleMapping)).EndInit();
            this.cmsEditDataTypeMapping.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tbcMain;
        private System.Windows.Forms.TabPage tbpSet;
        private System.Windows.Forms.TabPage tbpFromDB;
        private System.Windows.Forms.Label lblDotNetPostfix;
        private System.Windows.Forms.TextBox txtDotNetPostfix;
        private System.Windows.Forms.TextBox txtTabSize;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.RichTextBox rtbReferencesList;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.TextBox txtSavePath;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.RadioButton rdbOracle;
        private System.Windows.Forms.TextBox txtTabControlOneNamespace;
        private System.Windows.Forms.Label lblNamespace1;
        private System.Windows.Forms.TextBox txtTabControlOneClassRemark;
        private System.Windows.Forms.TextBox txtTabControlOneFieldInfo;
        private System.Windows.Forms.Label lblClassRemark;
        private System.Windows.Forms.Label lblFieldRemark;
        private System.Windows.Forms.DataGridView dgvTableOrViewStructure;
        private System.Windows.Forms.CheckedListBox clbTablesAndView;
        private System.Windows.Forms.Button btnGeneratedFromDb;
        private System.Windows.Forms.Button btnApplySetting;
        private System.Windows.Forms.TabControl tbcDataTypeMapping;
        private System.Windows.Forms.TabPage tbpOracle;
        private System.Windows.Forms.DataGridView dgvOracleMapping;
        private System.Windows.Forms.GroupBox gpbOracle;
        private System.Windows.Forms.TextBox txtOraclePwd;
        private System.Windows.Forms.TextBox txtOracleUserID;
        private System.Windows.Forms.TextBox txtOracleServer;
        private System.Windows.Forms.Button btnOracleConnectDb;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FolderBrowserDialog fbdSavePath;
        private System.Windows.Forms.ContextMenuStrip cmsEditDataTypeMapping;
        private System.Windows.Forms.ToolStripMenuItem tsmiAdd;
        private System.Windows.Forms.ToolStripMenuItem tsmiChange;
        private System.Windows.Forms.ToolStripMenuItem tsmiDelete;
        private System.Windows.Forms.OpenFileDialog ofdSelectAccessDbFile;
        private System.Windows.Forms.Label lblTabStructure;
        private System.Windows.Forms.Label lblTabList;
        private System.Windows.Forms.ProgressBar pbGeneratorProgress;
        private System.Windows.Forms.Label lblIsGenerating;
    }
}

