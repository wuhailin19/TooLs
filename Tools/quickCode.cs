using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NPinyin;

/// <summary>
/// 快速开发
/// </summary>
namespace Tools
{
    
    public partial class quickCode : Form
    {
        public quickCode()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.ShowDialog();
            textBox3.Text = openfolder.SelectedPath;
            try
            {
                string websitename = textBox3.Text.Substring(textBox3.Text.LastIndexOf("\\") + 1);
                string datatbasename = getdatabasename(websitename);
                database_select.Text = datatbasename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void btn_testlink_Click(object sender, EventArgs e)
        {
            DataTable dt = ListToDataTable(readHtmlPageByPath(textBox3.Text));
            page_select.DataSource = dt;
            page_select.DisplayMember = "Value";
            page_select.ValueMember = "Key";
            page_select.Text = dt.Rows[0]["Value"].ToString();
            try
            {
                string websitename = textBox3.Text.Substring(textBox3.Text.LastIndexOf("\\") + 1);
                string datatbasename = getdatabasename(websitename);
                database_select.Text = datatbasename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void page_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            string names = textBox3.Text + "\\" + page_select.Text;
            StreamReader sr = File.OpenText(names);
            while (sr.EndOfStream != true)
            {
                txt_contentID.Text = sr.ReadToEnd().ToString();
            }
            sr.Dispose();
        }
        private void cesToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
        private void quickCode_Load(object sender, EventArgs e)
        {
            BindDropDownList();
            Hashtable hash = new Hashtable();
            hash.Add("TextType", "单行文本框");
            hash.Add("MultipleTextType", "多行文本框");
            hash.Add("Editor", "编辑器");
            hash.Add("TimerPicker", "时间选择器");
            hash.Add("PicType", "单图选择器");
            hash.Add("FileType", "文件选择器");
            hash.Add("MutiImgSelect", "多图选择器");

            fileds_select.DataSource = HashToDataTable(hash);
            fileds_select.DisplayMember = "Value";
            fileds_select.ValueMember = "Key";
            fileds_select.Text = "单行文本框";

            Hashtable hashs = new Hashtable();
            hashs.Add("", "普通");
            hashs.Add("en", "英文");
            hashs.Add("linkurl_", "链接");
            type_select.DataSource = HashToDataTable(hashs);
            type_select.DisplayMember = "Value";
            type_select.ValueMember = "Key";
            type_select.Text = "普通";

        }
        private void button1_Click(object sender, EventArgs e)
        {
            string chinesenm = txt_tablename.Text.Trim();
            string database = database_select.Text;
            string tabledesc = txt_tabledesc.Text.Trim();
            if (string.IsNullOrEmpty(tabledesc))
            {
                tabledesc = chinesenm;
            }
            string tablename = "Custom_" + ChineseToEn(chinesenm);
            //添加数据表
            if (Add(database, tablename, chinesenm, tabledesc))
            {
                string str = database_select.Text;
                if (str == "System.Data.DataRowView")
                {
                    str = "master";
                }
                BindDropDounlists(str);
                DropDownList3.Text = tablename;
            }
        }
        private void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = database_select.Text;
            if (str == "System.Data.DataRowView")
            {
                str = "master";
            }
            BindDropDounlists(str);
        }
        //快速添加字段
        private void button3_Click(object sender, EventArgs e)
        {
            ModelFiled model = new ModelFiled();
            int count = 0;
            string filedtype = fileds_select.SelectedValue.ToString();
            int modelid = Convert.ToInt32(ModelFiled_dal.GetModelIdByExpression("TableName='" + DropDownList3.Text + "'", "ModelId")["ModelId"]);
            try
            {
                #region 控件判断
                switch (filedtype)
                {
                    case "TextType":
                        model.Content = "MaxLength=300,DfaulteValue=,TextLength=200";
                        model.Type = "TextType";
                        if (type_select.Text == "普通")
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "title");
                            model.FiledName = "title_" + NumberToEnglish(count + 1);
                            model.Alias = "标题" + NumberToChinese(count + 1);
                        }
                        else if (type_select.Text == "英文")
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "entitle");
                            model.FiledName = type_select.SelectedValue + "title_" + NumberToEnglish(count + 1);
                            model.Alias = "英文标题" + NumberToChinese(count + 1);
                        }
                        else
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "linkurl");
                            model.FiledName = type_select.SelectedValue + NumberToEnglish(count + 1);
                            model.Alias = "链接" + NumberToChinese(count + 1);
                        }
                        model.Validation = "Isrequired=,IsOther=";
                        break;
                    case "MultipleTextType":
                        model.Content = "Width=400,Height=120";
                        model.Type = "MultipleTextType";
                        if (type_select.Text == "英文")
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "enword");
                            model.Alias = "英文文字" + NumberToChinese(count + 1);
                            model.FiledName = type_select.SelectedValue + "word_" + NumberToEnglish(count + 1);
                        }
                        else
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "word");
                            model.Alias = "文字" + NumberToChinese(count + 1);
                            model.FiledName = "word_" + NumberToEnglish(count + 1);
                        }

                        model.Validation = "";
                        break;
                    case "Editor":
                        model.Content = "Width=750,Height=420";
                        model.Type = "Editor";
                        count = ModelFiled_dal.GetFiledCount(modelid, "desc");
                        model.Alias = "详细信息" + NumberToChinese(count + 1);
                        model.FiledName = "desc_" + NumberToEnglish(count + 1);
                        model.Validation = "";
                        break;
                    case "PicType":
                        model.Content = "";
                        model.Type = "PicType";
                        count = ModelFiled_dal.GetFiledCount(modelid, "img");
                        model.Alias = "图片" + NumberToChinese(count + 1);
                        model.FiledName = "img_" + NumberToEnglish(count + 1);
                        model.Validation = "";
                        break;
                    case "FileType":
                        model.Content = "";
                        model.Type = "FileType";
                        count = ModelFiled_dal.GetFiledCount(modelid, "files");
                        model.Alias = "文件" + NumberToChinese(count + 1);
                        model.FiledName = "files_" + NumberToEnglish(count + 1);
                        model.Validation = "";
                        break;
                    case "MutiImgSelect":
                        model.Content = "";
                        model.Type = "MutiImgSelect";
                        count = ModelFiled_dal.GetFiledCount(modelid, "imglist");
                        model.Alias = "图集" + NumberToChinese(count + 1);
                        model.Validation = "";
                        model.FiledName = "imglist_" + NumberToEnglish(count + 1);
                        break;
                    case "TimerPicker":
                        model.Content = "";
                        model.Type = "TimerPicker";
                        count = ModelFiled_dal.GetFiledCount(modelid, "datetime");
                        model.Alias = "选择时间" + NumberToChinese(count + 1);
                        model.Validation = "";
                        model.FiledName = "datetime_" + NumberToEnglish(count + 1);
                        break;
                }
                #endregion
                model.Description = "";
                model.AddTime = DateTime.Now;
                model.ModelId = modelid;
                model.OrderId = ModelFiled_dal.GetTopOrderID("ModelFiled");
                ModelFiled_dal.Add(model);
                //生成modelhtml
                StringBuilder sbr = new StringBuilder();
                ModelFiled_dal.CreateModelHTML(model.ModelId, ref sbr);
                txt_contentID.Text = sbr.ToString();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        #region 页面所用方法
        /// <summary>
        /// 数字转中文
        /// </summary>
        /// <param name="number">eg: 22</param>
        /// <returns></returns>
        public string NumberToChinese(int number)
        {
            string res = string.Empty;
            string str = number.ToString();
            string schar = str.Substring(0, 1);
            switch (schar)
            {
                case "1":
                    res = "一";
                    break;
                case "2":
                    res = "二";
                    break;
                case "3":
                    res = "三";
                    break;
                case "4":
                    res = "四";
                    break;
                case "5":
                    res = "五";
                    break;
                case "6":
                    res = "六";
                    break;
                case "7":
                    res = "七";
                    break;
                case "8":
                    res = "八";
                    break;
                case "9":
                    res = "九";
                    break;
                default:
                    res = "";
                    break;
            }
            if (str.Length > 1)
            {
                switch (str.Length)
                {
                    case 2:
                    case 6:
                        res += "十";
                        break;
                    case 3:
                    case 7:
                        res += "百";
                        break;
                    case 4:
                        res += "千";
                        break;
                    case 5:
                        res += "万";
                        break;
                    default:
                        res += "";
                        break;
                }
                res += NumberToChinese(int.Parse(str.Substring(1, str.Length - 1)));
            }
            return res;
        }/// <summary>
         /// 数字转英文
         /// </summary>
         /// <param name="number">eg: 22</param>
         /// <returns></returns>
        public string NumberToEnglish(int number)
        {
            string res = string.Empty;
            string str = number.ToString();
            string schar = str.Substring(0, 1);
            switch (schar)
            {
                case "1":
                    res = "f";
                    break;
                case "2":
                    res = "s";
                    break;
                case "3":
                    res = "t";
                    break;
                case "4":
                    res = "fo";
                    break;
                case "5":
                    res = "fif";
                    break;
                case "6":
                    res = "six";
                    break;
                case "7":
                    res = "sev";
                    break;
                case "8":
                    res = "eig";
                    break;
                case "9":
                    res = "nine";
                    break;
                default:
                    res = "";
                    break;
            }
            return res;
        }
        //汉译英(获取首字母)
        protected string ChineseToEn(string chinesestr)
        {
            string newenname = null;
            if (!string.IsNullOrEmpty(chinesestr))
            {
                for (int i = 0; i < chinesestr.Length; i++)
                {
                    newenname += Pinyin.GetPinyin(chinesestr[i]).Replace(" ", "")[0];
                }
            }
            return newenname;
        }
        /// <summary>
        /// 生成拼音数据库名
        /// </summary>
        /// <param name="chname"></param>
        /// <returns></returns>
        public string getdatabasename(string chname)
        {
            string newenname = null;
            if (!string.IsNullOrEmpty(chname))
            {
                string newwebsite = Pinyin.GetPinyin(chname).Replace(" ", "");
                newenname = newwebsite + "db";
            }
            return newenname;
        }
        /// <summary>
        /// 绑定数据表
        /// </summary>
        /// <param name="database"></param>
        public void BindDropDounlists(string database)
        {
            try
            {
                DataTable dt = null;
                dt = DBHelper.GetDataSet("use " + database + " select ROW_NUMBER() over(order by name asc) as OrderId,name from sys.tables where is_ms_shipped = 0 order by name asc");
                if (dt != null && dt.Rows.Count > 0)
                {
                    DropDownList3.DataSource = dt;
                    DropDownList3.DisplayMember = "name";
                    DropDownList3.ValueMember = "OrderId";
                    DropDownList3.Text = dt.Rows[0]["name"].ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 绑定数据库
        /// </summary>
        public void BindDropDownList()
        {
            try
            {
                DataTable dt = null;
                dt = DBHelper.GetDataSet("select name,database_id from sys.databases");
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.database_select.DataSource = dt;
                    this.database_select.DisplayMember = "name";
                    this.database_select.ValueMember = "database_id";
                    this.database_select.Text = dt.Rows[0]["name"].ToString();
                    BindDropDounlists(database_select.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public bool Add(string database, string tablename, string modelName, string modeldesc)
        {
            try
            {
                ///默认
                string addtablesql = $"use {database} " +
                                     "CREATE TABLE " + tablename + "" +
                                     "(" +
                                      "[Id] [int] IDENTITY (1, 1) PRIMARY Key NOT NULL," +
                                      "[ParentId] [int] NOT NULL," +
                                      "[SystemId] [int] NOT NULL," +
                                      "[Tittle] [nvarchar](255) NOT NULL," +
                                      "[Hits] [int] NOT NULL default 1," +
                                      "[KeyWord] [nvarchar](255) NOT NULL," +
                                      "[Description] [nvarchar](255) NOT NULL," +
                                      "[AddTime] [datetime] NOT NULL default getdate()," +
                                      "[IsTop] [bit] NOT NULL," +
                                      "[IsRecommend] [bit] NOT NULL," +
                                      "[IsHot] [bit] NOT NULL," +
                                      "[IsSlide] [bit] NOT NULL," +
                                      "[IsColor] [bit] NOT NULL," +
                                      "[OrderId] [int] NOT NULL," +
                                      "[AddUserName] [nvarchar](50) NOT NULL," +
                                      "[LastEditUserName] [nvarchar](50) NOT NULL," +
                                      "[LastEditDate] [datetime] NOT NULL default getdate()," +
                                      "[CheckedLevel] [int] NOT NULL default -88 " +
                                      " )";
                DBHelper.ExecuteCommand(addtablesql);

                string sql = "insert into ContentModel(" +
                    "ModelName," +
                    "ModelDesc," +
                    "TableName," +
                    "IsSystem," +
                    "Modelhtml," +
                    "IsShow) " +
                    "values('" +
                    "" + modelName + "'," +
                    "'" + modeldesc + "'," +
                    "'" + tablename + "'," +
                    "" + 0 + "," +
                    "'" + string.Empty + "'," +
                    "" + 1 + ")";

                DBHelper.ExecuteCommand(sql);
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// ListToDataTable
        /// </summary>
        /// <param name="s_list"></param>
        /// <returns></returns>
        protected DataTable ListToDataTable(List<string> s_list)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("Value");
            if (s_list.Count > 0)
            {
                for (int i = 0; i < s_list.Count; i++)
                {
                    DataRow dr = dt.NewRow();
                    dr["Key"] = i;
                    dr["Value"] = s_list[i];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        /// <summary>
        /// HashToDataTable
        /// </summary>
        /// <param name="s_list"></param>
        /// <returns></returns>
        protected DataTable HashToDataTable(Hashtable hash)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("Key");
            dt.Columns.Add("Value");
            if (hash.Count > 0)
            {
                foreach (string keys in hash.Keys)
                {
                    DataRow dr = dt.NewRow();
                    dr["Key"] = keys;
                    dr["Value"] = hash[keys];
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
        /// <summary>
        /// 获取目录下所有的页面
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        protected List<string> readHtmlPageByPath(string path)
        {
            List<string> s_list = new List<string>();
            try
            {

                string[] filenames = Directory.GetFiles(path);//得到完整路径文件名
                string filename = string.Empty;
                string extension = string.Empty;
                string name = string.Empty;
                for (int i = 0; i < filenames.Length; i++)
                {
                    filename = filenames[i].ToString();
                    extension = Path.GetExtension(filename);
                    name = filename.Substring(filename.LastIndexOf("\\") + 1).ToLower();
                    if (extension == ".aspx" || extension == ".html")
                    {
                        s_list.Add(name);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return s_list;
        }
        #endregion

        private void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                tools_addfiled.DropDownItems.Clear();
                ToolStripItem toolStripItemf = new ToolStripMenuItem("标题");
                toolStripItemf.Name = "Tittle";
                toolStripItemf.Text = "标题";
                toolStripItemf.Click += contextMenuStrip1_ItemClick;
                tools_addfiled.DropDownItems.Add(toolStripItemf);

                int modelid = Convert.ToInt32(ModelFiled_dal.GetModelIdByExpression("TableName='" + DropDownList3.Text + "'", "ModelId")["ModelId"]);
                IList<ModelFiled> modellist = ModelFiled_dal.GetModelList(modelid);
                foreach (ModelFiled model in modellist)
                {
                    ToolStripItem toolStripItem = new ToolStripMenuItem(model.Alias);
                    toolStripItem.Name = model.FiledName;
                    toolStripItem.Text = model.Alias;
                    toolStripItem.Click += contextMenuStrip1_ItemClick;

                    tools_addfiled.DropDownItems.Add(toolStripItem);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void contextMenuStrip1_ItemClick(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            string selectcont = txt_contentID.SelectedText;
            try
            {
                if (item.Name.Contains("title_"))
                {
                    txt_contentID.SelectedText = $"<%=GetDataRowEditorValue(PageDr,\"{item.Name}\")%>";
                }
                else if (item.Name.Contains("word_"))
                {
                    txt_contentID.SelectedText = $"<%=GetDataRowEditorValue(PageDr,\"{item.Name}\").Replace(\"\r\n\",\"<br/>\")%>";
                }
                else if (item.Name.Contains("desc_"))
                {
                    txt_contentID.SelectedText = $"<%=Server.HtmlDecode(GetDataRowEditorValue(PageDr,\"{item.Name}\"))%>";
                }
                else {
                    txt_contentID.SelectedText = $"<%=GetDataRowEditorValue(PageDr,\"{item.Name}\")%>";
                }
                //tools_addfiled.DropDownItems.Remove((ToolStripItem)sender);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void quickCode_KeyDown(object sender, KeyEventArgs e)
        {
            //if (e.Control && e.KeyCode == Keys.Z)
            //{
            //    txt_contentID.Text=txt_contentID.Text.Remove(txt_contentID.Text.Length - 1);
            //}
        }
    }
    public static class RichTextBoxExtension
    {
        public static void AppendTextColorful(this RichTextBox rtBox, string text, Color color, bool addNewLine = true)
        {
            if (addNewLine)
            {
                text += Environment.NewLine;
            }
            rtBox.SelectionStart = rtBox.TextLength;
            rtBox.SelectionLength = 0;
            rtBox.SelectionColor = color;
            rtBox.AppendText(text);
            rtBox.SelectionColor = rtBox.ForeColor;
        }
    }
}
