namespace Tools {
	partial class DataTools {
		/// <summary>
		/// 必需的设计器变量。
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// 清理所有正在使用的资源。
		/// </summary>
		/// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows 窗体设计器生成的代码

		/// <summary>
		/// 设计器支持所需的方法 - 不要
		/// 使用代码编辑器修改此方法的内容。
		/// </summary>
		private void InitializeComponent() {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button2 = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox3 = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DropDownList3 = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.DropDownList2 = new System.Windows.Forms.ComboBox();
            this.txt_resultID = new System.Windows.Forms.TextBox();
            this.txt_contentID = new System.Windows.Forms.TextBox();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.置于顶层ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.置于顶层ToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.开启ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.关闭ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.操作ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加数据库ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.处理图片ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.创建动态页面ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.处理编译网站ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加栏目ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.快速开发ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panel1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.Controls.Add(this.button2);
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label3);
            this.panel1.Controls.Add(this.comboBox3);
            this.panel1.Controls.Add(this.label2);
            this.panel1.Controls.Add(this.DropDownList3);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Controls.Add(this.DropDownList2);
            this.panel1.Controls.Add(this.txt_resultID);
            this.panel1.Controls.Add(this.txt_contentID);
            this.panel1.Controls.Add(this.menuStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(949, 746);
            this.panel1.TabIndex = 0;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(500, 365);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(49, 23);
            this.button2.TabIndex = 15;
            this.button2.Text = "刷新";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(823, 365);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 14;
            this.button1.Text = "提交";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(560, 370);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "操作：";
            // 
            // comboBox3
            // 
            this.comboBox3.BackColor = System.Drawing.SystemColors.Window;
            this.comboBox3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox3.FormattingEnabled = true;
            this.comboBox3.Items.AddRange(new object[] {
            "获取dataTable行内元素",
            "生成精简字段字符串",
            "父级Id字符串",
            "字符串转换(html-javascript)",
            "asp服务器代码",
            "Repeater代码",
            "命名Jquery变量",
            "快速分配权限",
            "字符串转换(html-stringbuilder)",
            "获取字符串长度",
            "DataRow",
            "转换HtmlDencode",
            "母版页DataRow",
            "复制本表数据",
            "生成随机时间",
            "重命名标题以及格式化OrderId"});
            this.comboBox3.Location = new System.Drawing.Point(607, 366);
            this.comboBox3.Name = "comboBox3";
            this.comboBox3.Size = new System.Drawing.Size(186, 20);
            this.comboBox3.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(254, 370);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "数据表：";
            // 
            // DropDownList3
            // 
            this.DropDownList3.BackColor = System.Drawing.SystemColors.Window;
            this.DropDownList3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DropDownList3.FormattingEnabled = true;
            this.DropDownList3.Location = new System.Drawing.Point(313, 366);
            this.DropDownList3.Name = "DropDownList3";
            this.DropDownList3.Size = new System.Drawing.Size(174, 20);
            this.DropDownList3.TabIndex = 10;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 370);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(47, 12);
            this.label1.TabIndex = 9;
            this.label1.Text = "数据库:";
            // 
            // DropDownList2
            // 
            this.DropDownList2.BackColor = System.Drawing.SystemColors.Window;
            this.DropDownList2.DropDownHeight = 300;
            this.DropDownList2.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DropDownList2.FormattingEnabled = true;
            this.DropDownList2.IntegralHeight = false;
            this.DropDownList2.Location = new System.Drawing.Point(103, 366);
            this.DropDownList2.Name = "DropDownList2";
            this.DropDownList2.Size = new System.Drawing.Size(139, 20);
            this.DropDownList2.TabIndex = 8;
            this.DropDownList2.SelectedIndexChanged += new System.EventHandler(this.comboBox1_SelectedIndexChanged);
            // 
            // txt_resultID
            // 
            this.txt_resultID.AcceptsReturn = true;
            this.txt_resultID.Location = new System.Drawing.Point(44, 395);
            this.txt_resultID.Margin = new System.Windows.Forms.Padding(0);
            this.txt_resultID.Multiline = true;
            this.txt_resultID.Name = "txt_resultID";
            this.txt_resultID.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_resultID.Size = new System.Drawing.Size(854, 327);
            this.txt_resultID.TabIndex = 7;
            this.txt_resultID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_resultID_KeyDown);
            // 
            // txt_contentID
            // 
            this.txt_contentID.AcceptsReturn = true;
            this.txt_contentID.Location = new System.Drawing.Point(44, 29);
            this.txt_contentID.Margin = new System.Windows.Forms.Padding(0);
            this.txt_contentID.Multiline = true;
            this.txt_contentID.Name = "txt_contentID";
            this.txt_contentID.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.txt_contentID.Size = new System.Drawing.Size(854, 327);
            this.txt_contentID.TabIndex = 6;
            this.txt_contentID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_contentID_KeyDown);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.置于顶层ToolStripMenuItem,
            this.操作ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(949, 25);
            this.menuStrip1.TabIndex = 16;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 置于顶层ToolStripMenuItem
            // 
            this.置于顶层ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.置于顶层ToolStripMenuItem1});
            this.置于顶层ToolStripMenuItem.Name = "置于顶层ToolStripMenuItem";
            this.置于顶层ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.置于顶层ToolStripMenuItem.Text = "视图";
            // 
            // 置于顶层ToolStripMenuItem1
            // 
            this.置于顶层ToolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.开启ToolStripMenuItem,
            this.关闭ToolStripMenuItem});
            this.置于顶层ToolStripMenuItem1.Name = "置于顶层ToolStripMenuItem1";
            this.置于顶层ToolStripMenuItem1.Size = new System.Drawing.Size(124, 22);
            this.置于顶层ToolStripMenuItem1.Text = "置于顶层";
            // 
            // 开启ToolStripMenuItem
            // 
            this.开启ToolStripMenuItem.Name = "开启ToolStripMenuItem";
            this.开启ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.开启ToolStripMenuItem.Text = "开启";
            this.开启ToolStripMenuItem.Click += new System.EventHandler(this.开启ToolStripMenuItem_Click);
            // 
            // 关闭ToolStripMenuItem
            // 
            this.关闭ToolStripMenuItem.Name = "关闭ToolStripMenuItem";
            this.关闭ToolStripMenuItem.Size = new System.Drawing.Size(100, 22);
            this.关闭ToolStripMenuItem.Text = "关闭";
            this.关闭ToolStripMenuItem.Click += new System.EventHandler(this.关闭ToolStripMenuItem_Click);
            // 
            // 操作ToolStripMenuItem
            // 
            this.操作ToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.添加数据库ToolStripMenuItem,
            this.处理图片ToolStripMenuItem,
            this.创建动态页面ToolStripMenuItem,
            this.处理编译网站ToolStripMenuItem,
            this.添加栏目ToolStripMenuItem,
            this.快速开发ToolStripMenuItem});
            this.操作ToolStripMenuItem.Name = "操作ToolStripMenuItem";
            this.操作ToolStripMenuItem.Size = new System.Drawing.Size(44, 21);
            this.操作ToolStripMenuItem.Text = "操作";
            // 
            // 添加数据库ToolStripMenuItem
            // 
            this.添加数据库ToolStripMenuItem.Name = "添加数据库ToolStripMenuItem";
            this.添加数据库ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加数据库ToolStripMenuItem.Text = "添加数据库";
            this.添加数据库ToolStripMenuItem.Click += new System.EventHandler(this.添加数据库ToolStripMenuItem_Click);
            // 
            // 处理图片ToolStripMenuItem
            // 
            this.处理图片ToolStripMenuItem.Name = "处理图片ToolStripMenuItem";
            this.处理图片ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.处理图片ToolStripMenuItem.Text = "处理图片";
            this.处理图片ToolStripMenuItem.Click += new System.EventHandler(this.处理图片ToolStripMenuItem_Click);
            // 
            // 创建动态页面ToolStripMenuItem
            // 
            this.创建动态页面ToolStripMenuItem.Name = "创建动态页面ToolStripMenuItem";
            this.创建动态页面ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.创建动态页面ToolStripMenuItem.Text = "创建动态页面";
            this.创建动态页面ToolStripMenuItem.Click += new System.EventHandler(this.创建动态页面ToolStripMenuItem_Click);
            // 
            // 处理编译网站ToolStripMenuItem
            // 
            this.处理编译网站ToolStripMenuItem.Name = "处理编译网站ToolStripMenuItem";
            this.处理编译网站ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.处理编译网站ToolStripMenuItem.Text = "处理编译网站";
            this.处理编译网站ToolStripMenuItem.Click += new System.EventHandler(this.处理编译网站ToolStripMenuItem_Click);
            // 
            // 添加栏目ToolStripMenuItem
            // 
            this.添加栏目ToolStripMenuItem.Name = "添加栏目ToolStripMenuItem";
            this.添加栏目ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.添加栏目ToolStripMenuItem.Text = "添加栏目";
            this.添加栏目ToolStripMenuItem.Click += new System.EventHandler(this.添加栏目ToolStripMenuItem_Click);
            // 
            // 快速开发ToolStripMenuItem
            // 
            this.快速开发ToolStripMenuItem.Name = "快速开发ToolStripMenuItem";
            this.快速开发ToolStripMenuItem.Size = new System.Drawing.Size(148, 22);
            this.快速开发ToolStripMenuItem.Text = "快速开发";
            this.快速开发ToolStripMenuItem.Click += new System.EventHandler(this.快速开发ToolStripMenuItem_Click);
            // 
            // DataTools
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(949, 746);
            this.Controls.Add(this.panel1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DataTools";
            this.Text = "DataTools";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.TextBox txt_resultID;
		private System.Windows.Forms.TextBox txt_contentID;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ComboBox comboBox3;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.ComboBox DropDownList3;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ComboBox DropDownList2;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.MenuStrip menuStrip1;
		private System.Windows.Forms.ToolStripMenuItem 置于顶层ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 置于顶层ToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem 开启ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 关闭ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 操作ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 添加数据库ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 处理图片ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 创建动态页面ToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem 处理编译网站ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加栏目ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 快速开发ToolStripMenuItem;
    }
}

