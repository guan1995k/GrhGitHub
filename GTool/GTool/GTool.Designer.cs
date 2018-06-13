namespace GTool
{
    partial class GTool
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
            this.BT_Browse = new System.Windows.Forms.Button();
            this.TB_BrowseFileName = new System.Windows.Forms.TextBox();
            this.TB_Name = new System.Windows.Forms.TextBox();
            this.LB_BrowseFile = new System.Windows.Forms.Label();
            this.LB_Name = new System.Windows.Forms.Label();
            this.BT_BuildSql = new System.Windows.Forms.Button();
            this.BT_BuildCs = new System.Windows.Forms.Button();
            this.DS_Excel = new System.Data.DataSet();
            this.GB_SqlFun = new System.Windows.Forms.GroupBox();
            this.GB_WebServiceFun = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BT_NotNull = new System.Windows.Forms.Button();
            this.BT_SqlDelete = new System.Windows.Forms.Button();
            this.BT_SqlSearch = new System.Windows.Forms.Button();
            this.BT_SqlUpdate = new System.Windows.Forms.Button();
            this.BT_SqlInsert = new System.Windows.Forms.Button();
            this.GB_ExcelUpload = new System.Windows.Forms.GroupBox();
            this.GB_ReportFun = new System.Windows.Forms.GroupBox();
            this.RTB_Content = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.DS_Excel)).BeginInit();
            this.GB_SqlFun.SuspendLayout();
            this.GB_WebServiceFun.SuspendLayout();
            this.GB_ExcelUpload.SuspendLayout();
            this.SuspendLayout();
            // 
            // BT_Browse
            // 
            this.BT_Browse.Location = new System.Drawing.Point(201, 16);
            this.BT_Browse.Name = "BT_Browse";
            this.BT_Browse.Size = new System.Drawing.Size(46, 23);
            this.BT_Browse.TabIndex = 1;
            this.BT_Browse.Text = "浏览";
            this.BT_Browse.UseVisualStyleBackColor = true;
            this.BT_Browse.Click += new System.EventHandler(this.BT_Browse_Click);
            // 
            // TB_BrowseFileName
            // 
            this.TB_BrowseFileName.Location = new System.Drawing.Point(75, 18);
            this.TB_BrowseFileName.Name = "TB_BrowseFileName";
            this.TB_BrowseFileName.Size = new System.Drawing.Size(120, 21);
            this.TB_BrowseFileName.TabIndex = 0;
            this.TB_BrowseFileName.Text = "E:\\Other\\TestExcel1.xlsx";
            // 
            // TB_Name
            // 
            this.TB_Name.Location = new System.Drawing.Point(75, 19);
            this.TB_Name.Name = "TB_Name";
            this.TB_Name.Size = new System.Drawing.Size(160, 21);
            this.TB_Name.TabIndex = 2;
            this.TB_Name.Text = "Test1";
            // 
            // LB_BrowseFile
            // 
            this.LB_BrowseFile.AutoSize = true;
            this.LB_BrowseFile.BackColor = System.Drawing.SystemColors.ControlText;
            this.LB_BrowseFile.ForeColor = System.Drawing.Color.Cornsilk;
            this.LB_BrowseFile.Location = new System.Drawing.Point(4, 21);
            this.LB_BrowseFile.Name = "LB_BrowseFile";
            this.LB_BrowseFile.Size = new System.Drawing.Size(65, 12);
            this.LB_BrowseFile.TabIndex = 2;
            this.LB_BrowseFile.Text = "浏览文件：";
            // 
            // LB_Name
            // 
            this.LB_Name.AutoSize = true;
            this.LB_Name.BackColor = System.Drawing.SystemColors.ControlText;
            this.LB_Name.ForeColor = System.Drawing.Color.Cornsilk;
            this.LB_Name.Location = new System.Drawing.Point(6, 22);
            this.LB_Name.Name = "LB_Name";
            this.LB_Name.Size = new System.Drawing.Size(65, 12);
            this.LB_Name.TabIndex = 2;
            this.LB_Name.Text = "名    称：";
            // 
            // BT_BuildSql
            // 
            this.BT_BuildSql.Location = new System.Drawing.Point(6, 46);
            this.BT_BuildSql.Name = "BT_BuildSql";
            this.BT_BuildSql.Size = new System.Drawing.Size(229, 23);
            this.BT_BuildSql.TabIndex = 3;
            this.BT_BuildSql.Text = "生成SQL表(SQL表生成语句)";
            this.BT_BuildSql.UseVisualStyleBackColor = true;
            this.BT_BuildSql.Click += new System.EventHandler(this.BT_BuildSql_Click);
            // 
            // BT_BuildCs
            // 
            this.BT_BuildCs.Location = new System.Drawing.Point(6, 75);
            this.BT_BuildCs.Name = "BT_BuildCs";
            this.BT_BuildCs.Size = new System.Drawing.Size(229, 23);
            this.BT_BuildCs.TabIndex = 4;
            this.BT_BuildCs.Text = "生成cs类(C#数据库方法类)";
            this.BT_BuildCs.UseVisualStyleBackColor = true;
            this.BT_BuildCs.Click += new System.EventHandler(this.BT_BuildCs_Click);
            // 
            // DS_Excel
            // 
            this.DS_Excel.DataSetName = "NewDataSet";
            // 
            // GB_SqlFun
            // 
            this.GB_SqlFun.BackColor = System.Drawing.SystemColors.ControlText;
            this.GB_SqlFun.Controls.Add(this.BT_BuildSql);
            this.GB_SqlFun.Controls.Add(this.BT_BuildCs);
            this.GB_SqlFun.Controls.Add(this.LB_Name);
            this.GB_SqlFun.Controls.Add(this.TB_Name);
            this.GB_SqlFun.ForeColor = System.Drawing.Color.Crimson;
            this.GB_SqlFun.Location = new System.Drawing.Point(6, 45);
            this.GB_SqlFun.Name = "GB_SqlFun";
            this.GB_SqlFun.Size = new System.Drawing.Size(241, 107);
            this.GB_SqlFun.TabIndex = 4;
            this.GB_SqlFun.TabStop = false;
            this.GB_SqlFun.Text = "Visual Studio";
            // 
            // GB_WebServiceFun
            // 
            this.GB_WebServiceFun.Controls.Add(this.label2);
            this.GB_WebServiceFun.Controls.Add(this.label1);
            this.GB_WebServiceFun.Controls.Add(this.BT_NotNull);
            this.GB_WebServiceFun.Controls.Add(this.BT_SqlDelete);
            this.GB_WebServiceFun.Controls.Add(this.BT_SqlSearch);
            this.GB_WebServiceFun.Controls.Add(this.BT_SqlUpdate);
            this.GB_WebServiceFun.Controls.Add(this.BT_SqlInsert);
            this.GB_WebServiceFun.ForeColor = System.Drawing.Color.Crimson;
            this.GB_WebServiceFun.Location = new System.Drawing.Point(6, 158);
            this.GB_WebServiceFun.Name = "GB_WebServiceFun";
            this.GB_WebServiceFun.Size = new System.Drawing.Size(241, 200);
            this.GB_WebServiceFun.TabIndex = 6;
            this.GB_WebServiceFun.TabStop = false;
            this.GB_WebServiceFun.Text = "MH_SCADA";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 180);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(167, 12);
            this.label2.TabIndex = 8;
            this.label2.Text = "且控件名称为空，需自行填写!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(4, 162);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(203, 12);
            this.label1.TabIndex = 8;
            this.label1.Text = "温馨提示：空间类型默认为TextBox，";
            // 
            // BT_NotNull
            // 
            this.BT_NotNull.Location = new System.Drawing.Point(6, 136);
            this.BT_NotNull.Name = "BT_NotNull";
            this.BT_NotNull.Size = new System.Drawing.Size(229, 23);
            this.BT_NotNull.TabIndex = 7;
            this.BT_NotNull.Text = "输入不能为空";
            this.BT_NotNull.UseVisualStyleBackColor = true;
            this.BT_NotNull.Click += new System.EventHandler(this.BT_NotNull_Click);
            // 
            // BT_SqlDelete
            // 
            this.BT_SqlDelete.Location = new System.Drawing.Point(6, 107);
            this.BT_SqlDelete.Name = "BT_SqlDelete";
            this.BT_SqlDelete.Size = new System.Drawing.Size(229, 23);
            this.BT_SqlDelete.TabIndex = 7;
            this.BT_SqlDelete.Text = "SQL删除语句";
            this.BT_SqlDelete.UseVisualStyleBackColor = true;
            this.BT_SqlDelete.Click += new System.EventHandler(this.BT_SqlDelete_Click);
            // 
            // BT_SqlSearch
            // 
            this.BT_SqlSearch.Location = new System.Drawing.Point(6, 78);
            this.BT_SqlSearch.Name = "BT_SqlSearch";
            this.BT_SqlSearch.Size = new System.Drawing.Size(229, 23);
            this.BT_SqlSearch.TabIndex = 7;
            this.BT_SqlSearch.Text = "SQL查询语句";
            this.BT_SqlSearch.UseVisualStyleBackColor = true;
            this.BT_SqlSearch.Click += new System.EventHandler(this.BT_SqlSearch_Click);
            // 
            // BT_SqlUpdate
            // 
            this.BT_SqlUpdate.Location = new System.Drawing.Point(6, 49);
            this.BT_SqlUpdate.Name = "BT_SqlUpdate";
            this.BT_SqlUpdate.Size = new System.Drawing.Size(229, 23);
            this.BT_SqlUpdate.TabIndex = 6;
            this.BT_SqlUpdate.Text = "SQL修改语句";
            this.BT_SqlUpdate.UseVisualStyleBackColor = true;
            this.BT_SqlUpdate.Click += new System.EventHandler(this.BT_SqlUpdate_Click);
            // 
            // BT_SqlInsert
            // 
            this.BT_SqlInsert.Location = new System.Drawing.Point(6, 20);
            this.BT_SqlInsert.Name = "BT_SqlInsert";
            this.BT_SqlInsert.Size = new System.Drawing.Size(229, 23);
            this.BT_SqlInsert.TabIndex = 5;
            this.BT_SqlInsert.Text = "SQL添加语句";
            this.BT_SqlInsert.UseVisualStyleBackColor = true;
            this.BT_SqlInsert.Click += new System.EventHandler(this.BT_SqlInsert_Click);
            // 
            // GB_ExcelUpload
            // 
            this.GB_ExcelUpload.Controls.Add(this.TB_BrowseFileName);
            this.GB_ExcelUpload.Controls.Add(this.GB_ReportFun);
            this.GB_ExcelUpload.Controls.Add(this.GB_WebServiceFun);
            this.GB_ExcelUpload.Controls.Add(this.LB_BrowseFile);
            this.GB_ExcelUpload.Controls.Add(this.BT_Browse);
            this.GB_ExcelUpload.Controls.Add(this.GB_SqlFun);
            this.GB_ExcelUpload.ForeColor = System.Drawing.Color.Crimson;
            this.GB_ExcelUpload.Location = new System.Drawing.Point(517, 12);
            this.GB_ExcelUpload.Name = "GB_ExcelUpload";
            this.GB_ExcelUpload.Size = new System.Drawing.Size(255, 540);
            this.GB_ExcelUpload.TabIndex = 7;
            this.GB_ExcelUpload.TabStop = false;
            this.GB_ExcelUpload.Text = "Excel上传";
            // 
            // GB_ReportFun
            // 
            this.GB_ReportFun.ForeColor = System.Drawing.Color.Crimson;
            this.GB_ReportFun.Location = new System.Drawing.Point(6, 364);
            this.GB_ReportFun.Name = "GB_ReportFun";
            this.GB_ReportFun.Size = new System.Drawing.Size(241, 170);
            this.GB_ReportFun.TabIndex = 6;
            this.GB_ReportFun.TabStop = false;
            this.GB_ReportFun.Text = "报表";
            // 
            // RTB_Content
            // 
            this.RTB_Content.BackColor = System.Drawing.SystemColors.WindowText;
            this.RTB_Content.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.RTB_Content.Location = new System.Drawing.Point(12, 12);
            this.RTB_Content.Name = "RTB_Content";
            this.RTB_Content.ReadOnly = true;
            this.RTB_Content.Size = new System.Drawing.Size(499, 540);
            this.RTB_Content.TabIndex = 8;
            this.RTB_Content.Text = "";
            // 
            // GTool
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlText;
            this.ClientSize = new System.Drawing.Size(784, 564);
            this.Controls.Add(this.RTB_Content);
            this.Controls.Add(this.GB_ExcelUpload);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Name = "GTool";
            this.Text = "GTool";
            ((System.ComponentModel.ISupportInitialize)(this.DS_Excel)).EndInit();
            this.GB_SqlFun.ResumeLayout(false);
            this.GB_SqlFun.PerformLayout();
            this.GB_WebServiceFun.ResumeLayout(false);
            this.GB_WebServiceFun.PerformLayout();
            this.GB_ExcelUpload.ResumeLayout(false);
            this.GB_ExcelUpload.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button BT_Browse;
        private System.Windows.Forms.TextBox TB_BrowseFileName;
        private System.Windows.Forms.TextBox TB_Name;
        private System.Windows.Forms.Label LB_BrowseFile;
        private System.Windows.Forms.Label LB_Name;
        private System.Windows.Forms.Button BT_BuildSql;
        private System.Windows.Forms.Button BT_BuildCs;
        private System.Data.DataSet DS_Excel;
        private System.Windows.Forms.GroupBox GB_SqlFun;
        private System.Windows.Forms.GroupBox GB_WebServiceFun;
        private System.Windows.Forms.GroupBox GB_ExcelUpload;
        private System.Windows.Forms.RichTextBox RTB_Content;
        private System.Windows.Forms.GroupBox GB_ReportFun;
        private System.Windows.Forms.Button BT_SqlSearch;
        private System.Windows.Forms.Button BT_SqlUpdate;
        private System.Windows.Forms.Button BT_SqlInsert;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button BT_NotNull;
        private System.Windows.Forms.Button BT_SqlDelete;

    }
}

