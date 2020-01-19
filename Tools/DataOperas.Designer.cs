namespace Tools {
	partial class DataOperas {
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing) {
			if (disposing && (components != null)) {
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
			this.panel1 = new System.Windows.Forms.Panel();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.textBox2 = new System.Windows.Forms.TextBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.textBox1 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.button4 = new System.Windows.Forms.Button();
			this.button3 = new System.Windows.Forms.Button();
			this.button2 = new System.Windows.Forms.Button();
			this.label4 = new System.Windows.Forms.Label();
			this.button1 = new System.Windows.Forms.Button();
			this.comboBox1 = new System.Windows.Forms.ComboBox();
			this.label3 = new System.Windows.Forms.Label();
			this.txt_saveurl = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.txt_database = new System.Windows.Forms.TextBox();
			this.label2 = new System.Windows.Forms.Label();
			this.txt_Linkurl = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.txt_username = new System.Windows.Forms.TextBox();
			this.btn_testlink = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.txt_password = new System.Windows.Forms.TextBox();
			this.panel1.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// panel1
			// 
			this.panel1.Controls.Add(this.groupBox3);
			this.panel1.Controls.Add(this.groupBox2);
			this.panel1.Controls.Add(this.groupBox1);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(949, 706);
			this.panel1.TabIndex = 0;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.textBox2);
			this.groupBox3.Location = new System.Drawing.Point(508, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(441, 706);
			this.groupBox3.TabIndex = 20;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "日志信息";
			// 
			// textBox2
			// 
			this.textBox2.AcceptsReturn = true;
			this.textBox2.Location = new System.Drawing.Point(6, 20);
			this.textBox2.Multiline = true;
			this.textBox2.Name = "textBox2";
			this.textBox2.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox2.Size = new System.Drawing.Size(435, 686);
			this.textBox2.TabIndex = 19;
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.textBox1);
			this.groupBox2.Location = new System.Drawing.Point(0, 340);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(515, 364);
			this.groupBox2.TabIndex = 19;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "数据展示";
			// 
			// textBox1
			// 
			this.textBox1.AcceptsReturn = true;
			this.textBox1.Location = new System.Drawing.Point(10, 30);
			this.textBox1.Multiline = true;
			this.textBox1.Name = "textBox1";
			this.textBox1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
			this.textBox1.Size = new System.Drawing.Size(490, 319);
			this.textBox1.TabIndex = 18;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.txt_password);
			this.groupBox1.Controls.Add(this.btn_testlink);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.txt_username);
			this.groupBox1.Controls.Add(this.label2);
			this.groupBox1.Controls.Add(this.txt_Linkurl);
			this.groupBox1.Controls.Add(this.button4);
			this.groupBox1.Controls.Add(this.button3);
			this.groupBox1.Controls.Add(this.button2);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.button1);
			this.groupBox1.Controls.Add(this.comboBox1);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txt_saveurl);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.txt_database);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(515, 347);
			this.groupBox1.TabIndex = 18;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "数据操作";
			// 
			// button4
			// 
			this.button4.Location = new System.Drawing.Point(43, 182);
			this.button4.Name = "button4";
			this.button4.Size = new System.Drawing.Size(91, 23);
			this.button4.TabIndex = 27;
			this.button4.Text = "保存路径";
			this.button4.UseVisualStyleBackColor = true;
			this.button4.Click += new System.EventHandler(this.button4_Click);
			// 
			// button3
			// 
			this.button3.Location = new System.Drawing.Point(147, 295);
			this.button3.Name = "button3";
			this.button3.Size = new System.Drawing.Size(91, 21);
			this.button3.TabIndex = 26;
			this.button3.Text = "执行sql文件";
			this.button3.UseVisualStyleBackColor = true;
			this.button3.Click += new System.EventHandler(this.button3_Click);
			// 
			// button2
			// 
			this.button2.Location = new System.Drawing.Point(43, 294);
			this.button2.Name = "button2";
			this.button2.Size = new System.Drawing.Size(91, 23);
			this.button2.TabIndex = 25;
			this.button2.Text = "创建数据库";
			this.button2.UseVisualStyleBackColor = true;
			this.button2.Click += new System.EventHandler(this.button2_Click);
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(147, 226);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(41, 12);
			this.label4.TabIndex = 24;
			this.label4.Text = "文件名";
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(43, 223);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(91, 23);
			this.button1.TabIndex = 23;
			this.button1.Text = "打开sql文件";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click_1);
			// 
			// comboBox1
			// 
			this.comboBox1.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.comboBox1.FormattingEnabled = true;
			this.comboBox1.Location = new System.Drawing.Point(149, 256);
			this.comboBox1.Name = "comboBox1";
			this.comboBox1.Size = new System.Drawing.Size(165, 20);
			this.comboBox1.TabIndex = 22;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(43, 264);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(77, 12);
			this.label3.TabIndex = 21;
			this.label3.Text = "现有数据库：";
			// 
			// txt_saveurl
			// 
			this.txt_saveurl.Location = new System.Drawing.Point(147, 184);
			this.txt_saveurl.Name = "txt_saveurl";
			this.txt_saveurl.ReadOnly = true;
			this.txt_saveurl.Size = new System.Drawing.Size(322, 21);
			this.txt_saveurl.TabIndex = 19;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(43, 152);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 18;
			this.label1.Text = "数据库名：";
			// 
			// txt_database
			// 
			this.txt_database.Location = new System.Drawing.Point(147, 149);
			this.txt_database.Name = "txt_database";
			this.txt_database.Size = new System.Drawing.Size(165, 21);
			this.txt_database.TabIndex = 17;
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(43, 23);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(77, 12);
			this.label2.TabIndex = 29;
			this.label2.Text = "服务器名称：";
			// 
			// txt_Linkurl
			// 
			this.txt_Linkurl.Location = new System.Drawing.Point(147, 20);
			this.txt_Linkurl.Name = "txt_Linkurl";
			this.txt_Linkurl.Size = new System.Drawing.Size(165, 21);
			this.txt_Linkurl.TabIndex = 28;
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(43, 56);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(41, 12);
			this.label5.TabIndex = 31;
			this.label5.Text = "账号：";
			// 
			// txt_username
			// 
			this.txt_username.Location = new System.Drawing.Point(147, 53);
			this.txt_username.Name = "txt_username";
			this.txt_username.Size = new System.Drawing.Size(165, 21);
			this.txt_username.TabIndex = 30;
			// 
			// btn_testlink
			// 
			this.btn_testlink.Location = new System.Drawing.Point(147, 117);
			this.btn_testlink.Name = "btn_testlink";
			this.btn_testlink.Size = new System.Drawing.Size(91, 23);
			this.btn_testlink.TabIndex = 32;
			this.btn_testlink.Text = "测试连接";
			this.btn_testlink.UseVisualStyleBackColor = true;
			this.btn_testlink.Click += new System.EventHandler(this.btn_testlink_Click);
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(43, 89);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(41, 12);
			this.label6.TabIndex = 34;
			this.label6.Text = "密码：";
			// 
			// txt_password
			// 
			this.txt_password.Location = new System.Drawing.Point(147, 86);
			this.txt_password.Name = "txt_password";
			this.txt_password.Size = new System.Drawing.Size(165, 21);
			this.txt_password.TabIndex = 33;
			// 
			// DataOperas
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.AutoScroll = true;
			this.AutoSize = true;
			this.ClientSize = new System.Drawing.Size(949, 706);
			this.Controls.Add(this.panel1);
			this.Name = "DataOperas";
			this.Text = "DataOperas";
			this.Load += new System.EventHandler(this.DataOperas_Load);
			this.panel1.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Panel panel1;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button button3;
		private System.Windows.Forms.Button button2;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Button button1;
		private System.Windows.Forms.ComboBox comboBox1;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.TextBox txt_saveurl;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txt_database;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.TextBox textBox1;
		private System.Windows.Forms.Button button4;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.TextBox textBox2;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txt_password;
		private System.Windows.Forms.Button btn_testlink;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.TextBox txt_username;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.TextBox txt_Linkurl;

	}
}