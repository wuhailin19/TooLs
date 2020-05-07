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
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tools_repeater = new System.Windows.Forms.ToolStripMenuItem();
            this.tools_addfiled = new System.Windows.Forms.ToolStripMenuItem();
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
            this.table_select = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.database_select = new System.Windows.Forms.ComboBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.button2 = new System.Windows.Forms.Button();
            this.btn_testlink = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.page_select = new System.Windows.Forms.ComboBox();
            this.label8 = new System.Windows.Forms.Label();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.button8 = new System.Windows.Forms.Button();
            this.pagedr_select = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.txt_contentID = new ICSharpCode.TextEditor.TextEditorControl();
            this.txt_contentextension = new ICSharpCode.TextEditor.TextEditorControl();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
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
            // type_select
            // 
            this.type_select.AutoCompleteCustomSource.AddRange(new string[] {
            "单行文本框"});
            this.type_select.BackColor = System.Drawing.SystemColors.Window;
            this.type_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.type_select.FormattingEnabled = true;
            this.type_select.Location = new System.Drawing.Point(1082, 202);
            this.type_select.Name = "type_select";
            this.type_select.Size = new System.Drawing.Size(53, 20);
            this.type_select.TabIndex = 72;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(1155, 201);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 71;
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
            this.fileds_select.Location = new System.Drawing.Point(901, 202);
            this.fileds_select.Name = "fileds_select";
            this.fileds_select.Size = new System.Drawing.Size(165, 20);
            this.fileds_select.TabIndex = 70;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(830, 206);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(65, 12);
            this.label7.TabIndex = 69;
            this.label7.Text = "字段类型：";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(50, 187);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(65, 12);
            this.label6.TabIndex = 68;
            this.label6.Text = "模型描述：";
            // 
            // txt_tabledesc
            // 
            this.txt_tabledesc.Location = new System.Drawing.Point(119, 183);
            this.txt_tabledesc.Name = "txt_tabledesc";
            this.txt_tabledesc.Size = new System.Drawing.Size(111, 21);
            this.txt_tabledesc.TabIndex = 67;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(48, 17);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(65, 12);
            this.label5.TabIndex = 66;
            this.label5.Text = "页面路径：";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(236, 181);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 65;
            this.button1.Text = "添加";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(59, 153);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 64;
            this.label4.Text = "模型名：";
            // 
            // txt_tablename
            // 
            this.txt_tablename.Location = new System.Drawing.Point(119, 149);
            this.txt_tablename.Name = "txt_tablename";
            this.txt_tablename.Size = new System.Drawing.Size(165, 21);
            this.txt_tablename.TabIndex = 63;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(60, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 62;
            this.label2.Text = "数据表：";
            // 
            // table_select
            // 
            this.table_select.BackColor = System.Drawing.SystemColors.Window;
            this.table_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.table_select.FormattingEnabled = true;
            this.table_select.Location = new System.Drawing.Point(119, 116);
            this.table_select.Name = "table_select";
            this.table_select.Size = new System.Drawing.Size(165, 20);
            this.table_select.TabIndex = 61;
            this.table_select.SelectedIndexChanged += new System.EventHandler(this.DropDownList3_SelectedIndexChanged);
            this.table_select.SelectedValueChanged += new System.EventHandler(this.DropDownList3_SelectedIndexChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(66, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(47, 12);
            this.label3.TabIndex = 60;
            this.label3.Text = "数据库:";
            // 
            // database_select
            // 
            this.database_select.BackColor = System.Drawing.SystemColors.Window;
            this.database_select.DropDownHeight = 300;
            this.database_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.database_select.FormattingEnabled = true;
            this.database_select.IntegralHeight = false;
            this.database_select.Location = new System.Drawing.Point(119, 83);
            this.database_select.Name = "database_select";
            this.database_select.Size = new System.Drawing.Size(165, 20);
            this.database_select.TabIndex = 59;
            this.database_select.SelectedIndexChanged += new System.EventHandler(this.DropDownList2_SelectedIndexChanged);
            this.database_select.SelectedValueChanged += new System.EventHandler(this.DropDownList2_SelectedIndexChanged);
            // 
            // textBox3
            // 
            this.textBox3.Location = new System.Drawing.Point(119, 14);
            this.textBox3.Name = "textBox3";
            this.textBox3.Size = new System.Drawing.Size(256, 21);
            this.textBox3.TabIndex = 58;
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(381, 12);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(121, 23);
            this.button2.TabIndex = 57;
            this.button2.Text = "静态页面路径";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // btn_testlink
            // 
            this.btn_testlink.Location = new System.Drawing.Point(508, 12);
            this.btn_testlink.Name = "btn_testlink";
            this.btn_testlink.Size = new System.Drawing.Size(91, 23);
            this.btn_testlink.TabIndex = 56;
            this.btn_testlink.Text = "确定";
            this.btn_testlink.UseVisualStyleBackColor = true;
            this.btn_testlink.Click += new System.EventHandler(this.btn_testlink_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(48, 51);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 55;
            this.label1.Text = "页面选择：";
            // 
            // page_select
            // 
            this.page_select.BackColor = System.Drawing.SystemColors.Window;
            this.page_select.DropDownHeight = 300;
            this.page_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.page_select.FormattingEnabled = true;
            this.page_select.IntegralHeight = false;
            this.page_select.Location = new System.Drawing.Point(119, 49);
            this.page_select.Name = "page_select";
            this.page_select.Size = new System.Drawing.Size(165, 20);
            this.page_select.TabIndex = 54;
            this.page_select.SelectedIndexChanged += new System.EventHandler(this.page_select_SelectedIndexChanged);
            this.page_select.SelectedValueChanged += new System.EventHandler(this.page_select_SelectedIndexChanged);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(328, 52);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(59, 12);
            this.label8.TabIndex = 73;
            this.label8.Text = "切换编码:";
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(385, 46);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(91, 23);
            this.button4.TabIndex = 74;
            this.button4.Text = "HTML";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Click += new System.EventHandler(this.button4_Click);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(482, 46);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(91, 23);
            this.button5.TabIndex = 75;
            this.button5.Text = "C#";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Click += new System.EventHandler(this.button5_Click);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(133, 214);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 76;
            this.button6.Text = "返回到页面";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Click += new System.EventHandler(this.button6_Click);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(52, 214);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 77;
            this.button7.Text = "后端代码";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Click += new System.EventHandler(this.button7_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(290, 115);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(97, 23);
            this.button8.TabIndex = 78;
            this.button8.Text = "生成后端查询";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // pagedr_select
            // 
            this.pagedr_select.BackColor = System.Drawing.SystemColors.Window;
            this.pagedr_select.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.pagedr_select.FormattingEnabled = true;
            this.pagedr_select.Items.AddRange(new object[] {
            "PageDr",
            "PageDr2",
            "PageDr3",
            "PageDr4"});
            this.pagedr_select.Location = new System.Drawing.Point(393, 116);
            this.pagedr_select.Name = "pagedr_select";
            this.pagedr_select.Size = new System.Drawing.Size(92, 20);
            this.pagedr_select.TabIndex = 79;
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(302, 41);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(285, 38);
            this.panel1.TabIndex = 80;
            // 
            // txt_contentID
            // 
            this.txt_contentID.AllowDrop = true;
            this.txt_contentID.AutoSize = true;
            this.txt_contentID.ContextMenuStrip = this.contextMenuStrip1;
            this.txt_contentID.IsReadOnly = false;
            this.txt_contentID.Location = new System.Drawing.Point(12, 243);
            this.txt_contentID.Name = "txt_contentID";
            this.txt_contentID.Size = new System.Drawing.Size(840, 500);
            this.txt_contentID.TabIndex = 53;
            // 
            // txt_contentextension
            // 
            this.txt_contentextension.AllowDrop = true;
            this.txt_contentextension.AutoSize = true;
            this.txt_contentextension.ContextMenuStrip = this.contextMenuStrip1;
            this.txt_contentextension.IsReadOnly = false;
            this.txt_contentextension.Location = new System.Drawing.Point(858, 243);
            this.txt_contentextension.Name = "txt_contentextension";
            this.txt_contentextension.Size = new System.Drawing.Size(369, 500);
            this.txt_contentextension.TabIndex = 81;
            // 
            // quickCode
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1239, 764);
            this.Controls.Add(this.txt_contentextension);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.pagedr_select);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.type_select);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.fileds_select);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.txt_tabledesc);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txt_tablename);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.table_select);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.database_select);
            this.Controls.Add(this.textBox3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btn_testlink);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.page_select);
            this.Controls.Add(this.txt_contentID);
            this.KeyPreview = true;
            this.Name = "quickCode";
            this.Text = "quickCode";
            this.Load += new System.EventHandler(this.quickCode_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tools_repeater;
        private System.Windows.Forms.ToolStripMenuItem tools_addfiled;
        private System.Windows.Forms.ComboBox type_select;
        private System.Windows.Forms.Button button3;
        private System.Windows.Forms.ComboBox fileds_select;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txt_tabledesc;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txt_tablename;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox table_select;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox database_select;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.Button btn_testlink;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox page_select;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Button button4;
        private System.Windows.Forms.Button button5;
        private System.Windows.Forms.Button button6;
        private System.Windows.Forms.Button button7;
        private System.Windows.Forms.Button button8;
        private System.Windows.Forms.ComboBox pagedr_select;
        private System.Windows.Forms.Panel panel1;
        private ICSharpCode.TextEditor.TextEditorControl txt_contentID;
        private ICSharpCode.TextEditor.TextEditorControl txt_contentextension;
    }
}