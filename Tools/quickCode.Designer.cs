namespace Tools
{
    partial class quickCode
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tools_repeater = new System.Windows.Forms.ToolStripMenuItem();
            this.tools_addfiled = new System.Windows.Forms.ToolStripMenuItem();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.type_select = new System.Windows.Forms.ComboBox();
            this.button3 = new System.Windows.Forms.Button();
            this.fileds_select = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txt_tabledesc = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txt_tablename = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.DropDownList3 = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.database_select = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_testlink = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.page_select = new System.Windows.Forms.ComboBox();
            this.txt_contentID = new System.Windows.Forms.RichTextBox();
            this.groupBox1.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.txt_contentID);
            this.groupBox1.Location = new System.Drawing.Point(12, 227);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(841, 520);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "代码区";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tools_repeater,
            this.tools_addfiled});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(130, 48);
            // 
            // tools_repeater
            // 
            this.tools_repeater.Name = "tools_repeater";
            this.tools_repeater.Size = new System.Drawing.Size(129, 22);
            this.tools_repeater.Text = "Repeater";
            this.tools_repeater.Click += new System.EventHandler(this.cesToolStripMenuItem_Click);
            // 
            // tools_addfiled
            // 
            this.tools_addfiled.Name = "tools_addfiled";
            this.tools_addfiled.Size = new System.Drawing.Size(129, 22);
            this.tools_addfiled.Text = "添加字段";
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.type_select);
            this.groupBox2.Controls.Add(this.button3);
            this.groupBox2.Controls.Add(this.fileds_select);
            this.groupBox2.Controls.Add(this.label7);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.txt_tabledesc);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.button1);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.txt_tablename);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.DropDownList3);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.database_select);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.button2);
            this.groupBox2.Controls.Add(this.btn_testlink);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.page_select);
            this.groupBox2.Location = new System.Drawing.Point(12, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(841, 218);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作区";
            // 
            // type_select
            // 
            this.type_select.AutoCompleteCustomSource.AddRange(new string[] {
            "单行文本框"});
            this.type_select.BackColor = System.Drawing.SystemColors.Window;
            this.type_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type_select.FormattingEnabled = true;
            this.type_select.Location = new System.Drawing.Point(564, 182);
            this.type_select.Name = "type_select";
            this.type_select.Size = new System.Drawing.Size(53, 20);
            this.type_select.TabIndex = 51;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(637, 181);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 50;
            this.button3.Text = "添加";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button3_Click);
            // 
            // fileds_select
            // 
            this.fileds_select.AutoCompleteCustomSource.AddRange(new string[] {
            "单行文本框"});
            this.fileds_select.BackColor = System.Drawing.SystemColors.Window;
            this.fileds_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.fileds_select.FormattingEnabled = true;
            this.fileds_select.Location = new System.Drawing.Point(383, 182);
            this.fileds_select.Name = "fileds_select";
            this.fileds_select.Size = new System.Drawing.Size(165, 20);
            this.fileds_select.TabIndex = 49;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(312, 186);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 48;
            this.label7.Text = "字段类型：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(11, 191);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 47;
            this.label6.Text = "模型描述：";
            // 
            // txt_tabledesc
            // 
            this.txt_tabledesc.Location = new System.Drawing.Point(80, 187);
            this.txt_tabledesc.Name = "txt_tabledesc";
            this.txt_tabledesc.Size = new System.Drawing.Size(111, 21);
            this.txt_tabledesc.TabIndex = 46;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(9, 21);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 45;
            this.label5.Text = "页面路径：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(197, 185);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 44;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 157);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 43;
            this.label4.Text = "模型名：";
            // 
            // txt_tablename
            // 
            this.txt_tablename.Location = new System.Drawing.Point(80, 153);
            this.txt_tablename.Name = "txt_tablename";
            this.txt_tablename.Size = new System.Drawing.Size(165, 21);
            this.txt_tablename.TabIndex = 42;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 123);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 41;
            this.label2.Text = "数据表：";
            // 
            // DropDownList3
            // 
            this.DropDownList3.BackColor = System.Drawing.SystemColors.Window;
            this.DropDownList3.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.DropDownList3.FormattingEnabled = true;
            this.DropDownList3.Location = new System.Drawing.Point(80, 121);
            this.DropDownList3.Name = "DropDownList3";
            this.DropDownList3.Size = new System.Drawing.Size(165, 20);
            this.DropDownList3.TabIndex = 40;
            this.DropDownList3.SelectedIndexChanged += new System.EventHandler(this.DropDownList3_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(27, 89);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 39;
            this.label3.Text = "数据库:";
            // 
            // database_select
            // 
            this.database_select.BackColor = System.Drawing.SystemColors.Window;
            this.database_select.DropDownHeight = 300;
            this.database_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.database_select.FormattingEnabled = true;
            this.database_select.IntegralHeight = false;
            this.database_select.Location = new System.Drawing.Point(80, 87);
            this.database_select.Name = "database_select";
            this.database_select.Size = new System.Drawing.Size(165, 20);
            this.database_select.TabIndex = 38;
            this.database_select.SelectedIndexChanged += new System.EventHandler(this.DropDownList2_SelectedIndexChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(80, 18);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(256, 21);
            this.textBox3.TabIndex = 37;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(342, 16);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 23);
            this.button2.TabIndex = 36;
            this.button2.Text = "静态页面路径";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_testlink
            // 
            this.btn_testlink.Location = new System.Drawing.Point(469, 16);
            this.btn_testlink.Name = "btn_testlink";
            this.btn_testlink.Size = new System.Drawing.Size(91, 23);
            this.btn_testlink.TabIndex = 35;
            this.btn_testlink.Text = "确定";
            this.btn_testlink.UseVisualStyleBackColor = true;
            this.btn_testlink.Click += new System.EventHandler(this.btn_testlink_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 55);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 10;
            this.label1.Text = "页面选择：";
            // 
            // page_select
            // 
            this.page_select.BackColor = System.Drawing.SystemColors.Window;
            this.page_select.DropDownHeight = 300;
            this.page_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.page_select.FormattingEnabled = true;
            this.page_select.IntegralHeight = false;
            this.page_select.Location = new System.Drawing.Point(80, 53);
            this.page_select.Name = "page_select";
            this.page_select.Size = new System.Drawing.Size(165, 20);
            this.page_select.TabIndex = 9;
            this.page_select.SelectedIndexChanged += new System.EventHandler(this.page_select_SelectedIndexChanged);
            // 
            // txt_contentID
            // 
            this.txt_contentID.ContextMenuStrip = this.contextMenuStrip1;
            this.txt_contentID.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txt_contentID.Location = new System.Drawing.Point(6, 20);
            this.txt_contentID.Name = "txt_contentID";
            this.txt_contentID.Size = new System.Drawing.Size(829, 494);
            this.txt_contentID.TabIndex = 9;
            this.txt_contentID.Text = "";
            // 
            // quickCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 759);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.KeyPreview = true;
            this.Name = "quickCode";
            this.Text = "quickCode";
            this.Load += new System.EventHandler(this.quickCode_Load);
            this.groupBox1.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.ComboBox page_select;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_testlink;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tools_repeater;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox DropDownList3;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox database_select;
        private System.Windows.Forms.ToolStripMenuItem tools_addfiled;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_tablename;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txt_tabledesc;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox fileds_select;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox type_select;
        private System.Windows.Forms.RichTextBox txt_contentID;
    }
}