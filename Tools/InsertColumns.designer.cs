namespace Tools {
	partial class InsertColumns {
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
            this.btn_submit = new System.Windows.Forms.Button();
            this.dt_datasource = new System.Windows.Forms.ComboBox();
            this.btn_getchinese = new System.Windows.Forms.Button();
            this.lbl_mark = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.txt_content = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // btn_submit
            // 
            this.btn_submit.Location = new System.Drawing.Point(551, 142);
            this.btn_submit.Name = "btn_submit";
            this.btn_submit.Size = new System.Drawing.Size(121, 23);
            this.btn_submit.TabIndex = 1;
            this.btn_submit.Text = "开始";
            this.btn_submit.UseVisualStyleBackColor = true;
            this.btn_submit.Click += new System.EventHandler(this.btn_submit_Click);
            // 
            // dt_datasource
            // 
            this.dt_datasource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dt_datasource.FormattingEnabled = true;
            this.dt_datasource.Location = new System.Drawing.Point(551, 49);
            this.dt_datasource.Name = "dt_datasource";
            this.dt_datasource.Size = new System.Drawing.Size(121, 20);
            this.dt_datasource.TabIndex = 2;
            // 
            // btn_getchinese
            // 
            this.btn_getchinese.Location = new System.Drawing.Point(551, 93);
            this.btn_getchinese.Name = "btn_getchinese";
            this.btn_getchinese.Size = new System.Drawing.Size(121, 23);
            this.btn_getchinese.TabIndex = 3;
            this.btn_getchinese.Text = "筛选中文";
            this.btn_getchinese.UseVisualStyleBackColor = true;
            this.btn_getchinese.Click += new System.EventHandler(this.btn_getchinese_Click);
            // 
            // lbl_mark
            // 
            this.lbl_mark.AutoSize = true;
            this.lbl_mark.Font = new System.Drawing.Font("微软雅黑", 12F);
            this.lbl_mark.Location = new System.Drawing.Point(551, 200);
            this.lbl_mark.Name = "lbl_mark";
            this.lbl_mark.Size = new System.Drawing.Size(55, 21);
            this.lbl_mark.TabIndex = 4;
            this.lbl_mark.Text = "label1";
            this.lbl_mark.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(551, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(121, 23);
            this.button1.TabIndex = 1;
            this.button1.Text = "测试";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // txt_content
            // 
            this.txt_content.Location = new System.Drawing.Point(12, 9);
            this.txt_content.Name = "txt_content";
            this.txt_content.Size = new System.Drawing.Size(525, 405);
            this.txt_content.TabIndex = 55;
            this.txt_content.Text = "";
            this.txt_content.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txt_content_KeyDown);
            // 
            // InsertColumns
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(690, 438);
            this.Controls.Add(this.txt_content);
            this.Controls.Add(this.lbl_mark);
            this.Controls.Add(this.btn_getchinese);
            this.Controls.Add(this.dt_datasource);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btn_submit);
            this.Name = "InsertColumns";
            this.Text = "InsertColumns";
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion
		private System.Windows.Forms.Button btn_submit;
		private System.Windows.Forms.ComboBox dt_datasource;
		private System.Windows.Forms.Button btn_getchinese;
		private System.Windows.Forms.Label lbl_mark;
		private System.Windows.Forms.Button button1;
        private System.Windows.Forms.RichTextBox txt_content;
    }
}