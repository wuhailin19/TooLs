using ICSharpCode.TextEditor;
using ICSharpCode.TextEditor.Document;
using ICSharpCode.TextEditor.Util;
using NPinyin;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

/// <summary>
/// 快速开发
/// </summary>
namespace Tools
{

    public partial class quickCode : Form
    {
        #region Filed
        private Dictionary<string, string> default_fileds = new Dictionary<string, string>();//数据表默认数据
        private Dictionary<string, string> insert_fileds = new Dictionary<string, string>();//数据表新增数据
        private bool IsBehideCode = false;//全局变量指示现在的代码是不是后台代码
        bool panelIsShow = false;//是否显示数据框标识
        //private int pagedrcount = 0;//PageDr数量标识
        #endregion

        public quickCode()
        {
            InitializeComponent();
            InitTextEditor();

        }
        /// <summary>
        /// 初始化控件
        /// </summary>
        private void InitTextEditor()
        {
            txt_contentID.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML");
            txt_contentID.Encoding = Encoding.UTF8;
            txt_contentID.Font = new System.Drawing.Font("微软雅黑", 12);
            txt_contentID.Document.FoldingManager.FoldingStrategy = new MingHTMLFolding();
            txt_contentID.ActiveTextAreaControl.TextArea.KeyDown += new System.Windows.Forms.KeyEventHandler(TextArea_KeyDown); ;

            txt_contentextension.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txt_contentextension.Encoding = System.Text.Encoding.UTF8;
        }

        #region 页面事件
        /// <summary>
        /// 重命名标题
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button14_Click(object sender, EventArgs e)
        {
            MessageBox.Show(TittleRandom(table_select.SelectedValue.ToString()));
        }
        /// <summary>
        /// 格式化OrderId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button16_Click(object sender, EventArgs e)
        {
            MessageBox.Show(OrderIdRandom(table_select.SelectedValue.ToString()));
        }
        /// <summary>
        /// 随机时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            MessageBox.Show(AddTimeRandom(table_select.SelectedValue.ToString()));
        }
        /// <summary>
        /// 栏目选择项改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void column_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            default_fileds["ParentId"] = column_select.SelectedValue.ToString();

            string default_filedstr = null;
            foreach (string key in default_fileds.Keys)
            {
                default_filedstr += $"{key}:{default_fileds[key]}\r\n";
            }
            txt_contentextension.Text = default_filedstr;
            txt_contentextension.ActiveTextAreaControl.Caret.Line = txt_contentextension.ActiveTextAreaControl.Caret.Line;
            DataTable dt = DBHelper.GetDataSet($"select Name from ColumnCategory where ColumnId=(select ParentId from ColumnCategory where ColumnId={column_select.SelectedValue})");
            if (dt.Rows.Count > 0 && dt != null)
            {
                lbl_parentname.Text = $"父级【{dt.Rows[0]["Name"]}】";
            }
            else
            {
                lbl_parentname.Text = $"父级【系统栏目】";
            }
        }
        /// <summary>
        /// 编辑框按键触发事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextArea_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.G)
            {
                string inputtxt = InputBox.ShowInputBox("请输入行数：");
                int line = !string.IsNullOrEmpty(inputtxt) ? Convert.ToInt32(inputtxt) : 0;
                if (line != 0)
                {
                    txt_contentID.ActiveTextAreaControl.Caret.Line = Convert.ToInt32(inputtxt) - 1;
                }
            }
            else if ((int)e.Modifiers == ((int)Keys.Control + (int)Keys.Alt) && e.KeyCode == Keys.D)//格式化代码
            {
                button9_Click(sender, e);
            }
            else if ((int)e.Modifiers == ((int)Keys.Control + (int)Keys.Alt) && e.KeyCode == Keys.C)//注释
            {
                string selecttxt = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
                txt_contentID.ActiveTextAreaControl.TextArea.InsertString("");
                txt_contentID.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
                txt_contentID.ActiveTextAreaControl.TextArea.InsertString(notesCode(selecttxt));
            }
            else if ((int)e.Modifiers == ((int)Keys.Control + (int)Keys.Alt) && e.KeyCode == Keys.V)//取消注释
            {
                string selecttxt = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
                txt_contentID.ActiveTextAreaControl.TextArea.InsertString("");
                txt_contentID.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
                txt_contentID.ActiveTextAreaControl.TextArea.InsertString(deletenotesCode(selecttxt));
            }
        }

        /// <summary>
        /// 向数据表中添加字段PId
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button17_Click(object sender, EventArgs e)
        {
            string database = database_select.Text;//数据库名称
            string tablename = table_select.SelectedValue.ToString();//数据表名
            string sql = $"use {database} alter table {tablename} add PId int";
            try
            {
                DBHelper.ExecuteCommand(sql);
                MessageBox.Show("添加成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        public void myevent() => btn_testlink_Click(this, new EventArgs());
        /// <summary>
        /// 页面路径选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            if (openfolder.ShowDialog() == DialogResult.OK)
            {
                textBox3.Text = openfolder.SelectedPath;
                try
                {
                    DataTable dt = ListToDataTable(readHtmlPageByPath(textBox3.Text));
                    page_select.DataSource = dt;
                    page_select.DisplayMember = "Value";
                    page_select.ValueMember = "Key";
                    page_select.Text = dt.Rows[0]["Value"].ToString();

                    string websitename = textBox3.Text.Substring(textBox3.Text.LastIndexOf("\\") + 1);
                    string datatbasename = getdatabasename(websitename);
                    database_select.Text = datatbasename;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        /// <summary>
        /// 输入路径确定
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_testlink_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dt = ListToDataTable(readHtmlPageByPath(textBox3.Text));
                page_select.DataSource = dt;
                page_select.DisplayMember = "Value";
                page_select.ValueMember = "Key";
                page_select.Text = dt.Rows[0]["Value"].ToString();

                string websitename = textBox3.Text.Substring(textBox3.Text.LastIndexOf("\\") + 1);
                string datatbasename = getdatabasename(websitename);
                database_select.Text = datatbasename;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 页面选择下拉选项切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void page_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string names = textBox3.Text + "\\" + page_select.Text;
                StreamReader sr = File.OpenText(names);
                while (sr.EndOfStream != true)
                {
                    string srcont = sr.ReadToEnd().ToString();
                    txt_contentID.Text = srcont;
                }
                sr.Dispose();
                if (page_select.Text.ToLower().Contains("comm"))
                {
                    checkBox1.Checked = true;
                }
                else
                {
                    checkBox1.Checked = false;
                }
            }
            catch (Exception ex)
            {
            }
            IsBehideCode = false;
            button4_Click(sender, e);
        }
        /// <summary>
        /// 右键母版页字段菜单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            (sender as ToolStripMenuItem).Checked = true;
            //string selectcont = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
            txt_contentID.ActiveTextAreaControl.TextArea.InsertString("");
            txt_contentID.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            try
            {
                if (item.Name.Contains("Descr"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString($"<%={item.Text}.Replace(\"\\r\\n\",\"<br/>\")%>");
                }
                else
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString($"<%={item.Text}%>");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 页面加载触发
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quickCode_Load(object sender, EventArgs e)
        {
            BindDropDownList();
            Init();
        }
        /// <summary>
        /// 初始化窗体
        /// </summary>
        private void Init()
        {
            //绑定字段类别
            Hashtable hashs = new Hashtable();
            hashs.Add("", "普通");
            hashs.Add("en", "英文");
            hashs.Add("linkurl_", "链接");
            hashs.Add("bg", "背景图片");
            type_select.DataSource = HashToDataTable(hashs);
            type_select.DisplayMember = "Value";
            type_select.ValueMember = "Key";
            type_select.Text = "普通";

            //绑定控件名
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

            //设置页面DataRow变量
            pagedr_select.Text = "PageDr";

            //绑定母版页变量列表
            Dictionary<string, string> dic = new Dictionary<string, string>();

            dic.Add("comm_firstPbanner", "firstPbanner");
            dic.Add("comm_firstPageName", "firstPageName");
            dic.Add("comm_firstEnglishName", "firstEnglishName");
            dic.Add("comm_firstPageDescr", "firstPageDescr");
            dic.Add("comm_nowPbanner", "nowPbanner");
            dic.Add("comm_nowPageName", "nowPageName");
            dic.Add("comm_nowPageEnglishName", "nowPageEnglishName");
            dic.Add("comm_nowPageDescr", "nowPageDescr");
            dic.Add("comm_Sbr_Navbar", "Sbr_Navbar");
            dic.Add("comm_Sbr_Page_Navbar", "Sbr_Page_Navbar");
            dic.Add("comm_Sbr_BearNav", "Sbr_BearNav");

            foreach (string str in dic.Keys)
            {
                ToolStripItem toolStripItem = new ToolStripMenuItem(dic[str].ToString());
                toolStripItem.Name = str;
                toolStripItem.Text = dic[str].ToString();
                toolStripItem.Click += cesToolStripMenuItem_Click;

                tools_common.DropDownItems.Add(toolStripItem);
            }

            dic.Clear();
            //绑定新闻字段
            dic.Add("ArticleId", "ArticleId");
            dic.Add("ParentId", "ParentId");
            dic.Add("ArticleImg", "ArticleImg");
            dic.Add("ArticleTitle", "ArticleTitle");
            dic.Add("ArticleSummy", "ArticleSummy");
            dic.Add("ArticleContent", "ArticleContent");
            dic.Add("AddTime", "AddTime");
            foreach (string str in dic.Keys)
            {
                ToolStripItem toolStripItem = new ToolStripMenuItem(dic[str].ToString());
                toolStripItem.Name = str;
                toolStripItem.Text = dic[str].ToString();
                toolStripItem.Click += contextMenuStrip1_ItemClick;

                tool_article.DropDownItems.Add(toolStripItem);
            }
            //初始化模型
            BindColumnList();

            InitTools();
        }
        /// <summary>
        /// 添加模型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                    str = "";
                }
                bool result = false;
                BindDropDounlists(str, ref result);
                table_select.SelectedValue = tablename;
            }
        }
        /// <summary>
        /// 数据库下拉选项切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownList2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = database_select.Text;
            if (str == "System.Data.DataRowView")
            {
                str = "";
            }
            bool result = false;
            BindDropDounlists(str, ref result);
            BindColumnList();
        }
        //快速添加字段事件
        private void button3_Click(object sender, EventArgs e)
        {
            ModelFiled model = new ModelFiled();
            int count = 0;
            string filedtype = fileds_select.SelectedValue.ToString();
            string filedname = txt_filedname.Text.Trim();//字段名
            string selecttxt = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
            int modelid = Convert.ToInt32(ModelFiled_dal.GetModelIdByExpression("TableName='" + table_select.SelectedValue + "'", "ModelId")["ModelId"]);
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
                            count = ModelFiled_dal.GetFiledCount(modelid, "chtitle_");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "标题" + NumberToChinese(count + 1);
                            }
                            model.FiledName = "chtitle_" + NumberToEnglish(count + 1);
                            model.Alias = filedname;
                        }
                        else if (type_select.Text == "英文")
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "entitle_");
                            model.FiledName = type_select.SelectedValue + "title_" + NumberToEnglish(count + 1);
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "英文标题" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                        }
                        else
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "linkurl_");
                            model.FiledName = type_select.SelectedValue + NumberToEnglish(count + 1);
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "链接" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                        }
                        model.Validation = "Isrequired=,IsOther=";
                        break;
                    case "MultipleTextType":
                        model.Content = "Width=400,Height=120";
                        model.Type = "MultipleTextType";
                        if (type_select.Text == "英文")
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "enword_");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "英文文字" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                            model.FiledName = type_select.SelectedValue + "word_" + NumberToEnglish(count + 1);
                        }
                        else
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "chword_");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "文字" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                            model.FiledName = "chword_" + NumberToEnglish(count + 1);
                        }
                        selecttxt = selecttxt.Replace("<br/>", "\r\n").Replace("<br >", "\r\n").Replace("<br>", "\r\n").Replace("</p>", "\r\n").Replace("<p>", "");
                        model.Validation = "";
                        break;
                    case "Editor":
                        model.Content = "Width=750,Height=420";
                        model.Type = "Editor";
                        count = ModelFiled_dal.GetFiledCount(modelid, "desc");
                        if (string.IsNullOrEmpty(filedname))
                        {
                            filedname = "详细信息" + NumberToChinese(count + 1);
                        }
                        model.Alias = filedname;
                        model.FiledName = "desc_" + NumberToEnglish(count + 1);
                        model.Validation = "";
                        selecttxt = selecttxt.Replace("\r\n", "").Replace("\n", "").Replace("\t", "");
                        selecttxt = System.Web.HttpUtility.HtmlEncode(selecttxt);
                        break;
                    case "PicType":
                        model.Content = "";
                        model.Type = "PicType";
                        string img_w_h = null;//图片尺寸
                        string imgfile = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
                        try
                        {
                            img_w_h = getimgswidthandheight(textBox3.Text + "\\" + imgfile);
                            model.Description = $"图片尺寸({img_w_h})";
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message);
                        }
                        if (type_select.Text == "背景图片")
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "bgimg");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "背景图片" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                            model.FiledName = type_select.SelectedValue + "img_" + NumberToEnglish(count + 1);
                        }
                        else
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "normalimg");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "图片" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                            model.FiledName = "normalimg_" + NumberToEnglish(count + 1);
                        }
                        model.Validation = "";
                        break;
                    case "FileType":
                        model.Content = "";
                        model.Type = "FileType";
                        count = ModelFiled_dal.GetFiledCount(modelid, "files");
                        if (string.IsNullOrEmpty(filedname))
                        {
                            filedname = "文件" + NumberToChinese(count + 1);
                        }
                        model.Alias = filedname;
                        model.FiledName = "files_" + NumberToEnglish(count + 1);
                        model.Validation = "";
                        break;
                    case "MutiImgSelect":
                        model.Content = "";
                        model.Type = "MutiImgSelect";
                        count = ModelFiled_dal.GetFiledCount(modelid, "imglist");
                        string img_w_hs = null;//图片尺寸
                        string imgfiles = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
                        img_w_hs = getimgswidthandheight(textBox3.Text + "\\" + imgfiles);
                        if (string.IsNullOrEmpty(filedname))
                        {
                            filedname = "图集";
                        }
                        model.Alias = $"{filedname}({img_w_hs})";
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
                model.AddTime = DateTime.Now;
                model.ModelId = modelid;
                model.OrderId = ModelFiled_dal.GetTopOrderID("ModelFiled");
                if (!model.FiledName.Contains("imglist_"))
                    insert_fileds.Add(model.FiledName, $"'{selecttxt}'");
                ModelFiled_dal.Add(model);
                //生成modelhtml
                StringBuilder sbr = new StringBuilder();
                ModelFiled_dal.CreateModelHTML(model.ModelId, ref sbr);

                InitTools();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 数据表下拉选项切换
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void DropDownList3_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                insert_fileds.Clear();
                default_fileds.Clear();
                tools_addfiled.DropDownItems.Clear();
                #region 添加默认数据进入数据表
                if (table_select.Text.Contains("Article") && !table_select.Text.Contains("Single"))
                {
                    default_fileds.Add("Hits", "0");
                    default_fileds.Add("ArticleImg", "''");
                    default_fileds.Add("ArticleSummy", "''");
                    default_fileds.Add("ArticleKey", "");
                    default_fileds.Add("ArticleDescription", "''");
                    default_fileds.Add("ArticleContent", "''");
                    default_fileds.Add("ArticleTitle", "''");
                    default_fileds.Add("videourl", "''");
                    default_fileds.Add("Soruce", "''");
                    default_fileds.Add("Author", "''");
                    default_fileds.Add("SubTitle", "''");
                }
                else
                {
                    default_fileds.Add("KeyWord", "''");
                    default_fileds.Add("Description", "''");
                    default_fileds.Add("Tittle", "''");
                }
                default_fileds.Add("ParentId", column_select.SelectedValue == null ? "" : column_select.SelectedValue.ToString());
                default_fileds.Add("SystemId", "1");
                default_fileds.Add("IsTop", "'False'");
                default_fileds.Add("IsRecommend", "'False'");
                default_fileds.Add("IsHot", "'False'");
                default_fileds.Add("IsSlide", "'False'");
                default_fileds.Add("IsColor", "'False'");
                default_fileds.Add("OrderId", "0");
                default_fileds.Add("AddTime", $"'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}'");
                default_fileds.Add("LastEditDate", $"'{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")}'");
                default_fileds.Add("AddUserName", "'webmaster'");
                default_fileds.Add("LastEditUserName", "'webmaster'");
                default_fileds.Add("CheckedLevel", "-88");

                if (table_select.Text.Contains("SingleArticle"))
                {
                    default_fileds.Add("Hits", "0");
                    default_fileds.Add("Content", "''");
                    default_fileds.Add("ArticleKey", "''");
                    default_fileds.Add("ArticleDescription", "''");
                    default_fileds.Add("Summy", "''");
                    default_fileds.Remove("IsTop");
                    default_fileds.Remove("IsRecommend");
                    default_fileds.Remove("IsSlide");
                    default_fileds.Remove("IsColor");
                    default_fileds.Remove("IsHot");
                    default_fileds.Remove("OrderId");
                    default_fileds.Remove("KeyWord");
                    default_fileds.Remove("Description");
                }
                #endregion
                InitTools();
            }
            catch (Exception ex) { }
        }
        /// <summary>
        /// 初始化添加字段菜单
        /// </summary>
        private void InitTools()
        {
            try
            {
                tools_addfiled.DropDownItems.Clear();
                ToolStripItem toolStripItemf = new ToolStripMenuItem("标题");
                if (table_select.Text.Contains("Article") && !table_select.Text.Contains("Single"))
                {
                    toolStripItemf.Name = "ArticleTitle";
                }
                else
                {
                    toolStripItemf.Name = "Tittle";
                }
                toolStripItemf.Text = "标题";
                toolStripItemf.Click += contextMenuStrip1_ItemClick;
                tools_addfiled.DropDownItems.Add(toolStripItemf);

                ToolStripItem toolStripItems = new ToolStripMenuItem("时间");
                toolStripItems.Name = "AddTime";
                toolStripItems.Text = "时间";
                toolStripItems.Click += contextMenuStrip1_ItemClick;
                tools_addfiled.DropDownItems.Add(toolStripItems);

                int modelid = Convert.ToInt32(ModelFiled_dal.GetModelIdByExpression("TableName='" + table_select.SelectedValue + "'", "ModelId")["ModelId"]);
                IList<ModelFiled> modellist = ModelFiled_dal.GetModelList(modelid);
                foreach (ModelFiled model in modellist)
                {
                    ToolStripItem toolStripItem = new ToolStripMenuItem(model.Alias);
                    toolStripItem.Name = model.FiledName;
                    toolStripItem.Text = model.Alias;
                    if (insert_fileds.ContainsKey(model.FiledName))
                    {
                        if (!default_fileds.ContainsKey(model.FiledName))
                            default_fileds.Add(model.FiledName, insert_fileds[model.FiledName]);
                        else
                            default_fileds[model.FiledName] = insert_fileds[model.FiledName];
                    }
                    else if (!default_fileds.ContainsKey(model.FiledName))
                    {
                        default_fileds.Add(model.FiledName, "''");
                    }
                    toolStripItem.Click += contextMenuStrip1_ItemClick;
                    tools_addfiled.DropDownItems.Add(toolStripItem);
                }

                string default_filedstr = null;
                foreach (string key in default_fileds.Keys)
                {
                    default_filedstr += $"{key}:{default_fileds[key]}\r\n";
                }
                txt_contentextension.Text = default_filedstr;
                txt_contentextension.ActiveTextAreaControl.Caret.Line = default_filedstr.Length;
            }
            catch (Exception ex)
            {

            }
        }
        /// <summary>
        /// 修改对应字段内容
        /// </summary>
        /// <param name="filedname"></param>
        /// <param name="filedcont"></param>
        private void UpdateFiledCont(string filedname, string filedcont)
        {
            if (default_fileds.ContainsKey(filedname))
            {
                default_fileds[filedname] = $"'{filedcont}'";
            }
            default_fileds["ParentId"] = column_select.SelectedValue == null ? "" : column_select.SelectedValue.ToString();
            string default_filedstr = null;
            foreach (string key in default_fileds.Keys)
            {
                default_filedstr += $"{key}:{default_fileds[key]}\r\n";
            }
            txt_contentextension.Text = default_filedstr;
            txt_contentextension.ActiveTextAreaControl.Caret.Line = default_filedstr.Length;
        }
        /// <summary>
        /// 右键字段菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClick(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            (sender as ToolStripMenuItem).Checked = true;
            string selectcont = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
            txt_contentID.ActiveTextAreaControl.TextArea.InsertString("");
            txt_contentID.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            string pagedr = pagedr_select.Text;
            string masterpage = "";
            bool isrepeater = checkbox_repeater.Checked;
            if (checkBox1.Checked)
            {
                masterpage = "WebUtility.";
            }
            try
            {
                if (item.Name.Contains("word_") || item.Name.Contains("Summy"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString(isrepeater ? ($"<%#Eval(\"{item.Name}\").ToString().Replace(\"\\r\\n\",\"<br/>\")%>") : ($"<%={masterpage}GetDataRowsValue({pagedr},\"{item.Name}\").Replace(\"\\r\\n\",\"<br/>\")%>"));
                    selectcont = selectcont.Replace("<br/>", "\r\n").Replace("<br >", "\r\n").Replace("<br>", "\r\n").Replace("</p>", "\r\n").Replace("<p>", "");
                    UpdateFiledCont(item.Name, selectcont);
                }
                else if (item.Name.Contains("desc_") || item.Name.Contains("Content"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString(isrepeater ? ($"<%#Server.HtmlDecode(Eval(\"{item.Name}\").ToString())%>") : ($"<%={masterpage}GetDataRowEditorValue({pagedr},\"{item.Name}\")%>"));
                    selectcont = selectcont.Replace("\r\n", "").Replace("\n", "").Replace("\t", "");
                    selectcont = System.Web.HttpUtility.HtmlEncode(selectcont);
                    UpdateFiledCont(item.Name, selectcont);
                }
                else if (item.Name.Contains("imglist_"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString(isrepeater ? ($"<%#Eval(\"{item.Name}\")%>") : ($"<%=WebUtility.GetImgeList(GetDataRowsValue({pagedr},\"{item.Name}\"),1)%>"));
                }
                else if (item.Name.ToLower().Contains("time"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString(isrepeater ? ($"<%#Convert.ToDateTime(Eval(\"{item.Name}\")).ToString(\"yyyy-MM-dd\")%>") : ($"<%={masterpage}GetDataRowsValue({pagedr},\"{item.Name}\")%>"));
                }
                else
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString(isrepeater ? ($"<%#Eval(\"{item.Name}\")%>") : ($"<%={masterpage}GetDataRowsValue({pagedr},\"{item.Name}\")%>"));
                    UpdateFiledCont(item.Name, selectcont);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //切换文本框编码为Html
        private void button4_Click(object sender, EventArgs e)
        {
            txt_contentID.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML");
        }
        //切换文本框编码为C#
        private void button5_Click(object sender, EventArgs e)
        {
            txt_contentID.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
        }
        /// <summary>
        /// 返回到html页面按钮
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            IsBehideCode = false;
            button4_Click(sender, e);
            page_select_SelectedIndexChanged(sender, e);
            //DropDownList3_SelectedIndexChanged(sender, e);
        }
        /// <summary>
        /// 返回到页面后台代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button7_Click(object sender, EventArgs e)
        {
            try
            {
                IsBehideCode = true;
                string names = textBox3.Text + "\\" + page_select.Text + ".cs";
                if (File.Exists(names))
                {
                    StreamReader sr = File.OpenText(names);
                    while (sr.EndOfStream != true)
                    {
                        txt_contentID.Text = sr.ReadToEnd().ToString();
                    }
                    sr.Dispose();
                }
                button5_Click(sender, e);
            }
            catch
            {
                txt_contentID.Text = "";
            }
        }
        /// <summary>
        /// 生成后端查询语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button8_Click(object sender, EventArgs e)
        {
            string codecontent = txt_contentID.Text;
            string str = GetEasyFieldsNewString(table_select.SelectedValue.ToString());
            codecontent = codecontent.Replace("\r\n", "\n");
            codecontent = codecontent.Replace("}\n        }\n        catch\n        {\n        }\n    }\n}", str + "}\n        }\n        catch\n        {\n        }\n    }\n}");
            txt_contentID.Text = codecontent;
            txt_contentID.ActiveTextAreaControl.Caret.Line = codecontent.Length;
            txt_contentextension.Text = str;
        }
        /// <summary>
        /// Ctrl+s保存更改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void quickCode_KeyDown_1(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.S)//按下ctrl+s
            {
                string filenames = textBox3.Text + "\\" + page_select.Text;//文件路径
                string content = txt_contentID.Text;
                if (IsBehideCode)
                {
                    filenames = filenames + ".cs";
                }
                try
                {
                    if (File.Exists(filenames))
                    {
                        FileStream fs = new FileStream(filenames, FileMode.Create, FileAccess.Write);
                        StreamWriter sw = new StreamWriter(fs, Encoding.UTF8);
                        sw.WriteLine(content);
                        sw.Close();
                        fs.Close();
                        MessageBox.Show("保存成功");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        //模型栏目配对
        private void button10_Click(object sender, EventArgs e)
        {
            string modelid = ModelFiled_dal.GetModelIdByExpression($"TableName='{table_select.SelectedValue}'", "ModelId")["ModelId"].ToString();
            try
            {
                string sql = $"update ColumnCategory set ModelType={modelid} " +
                    $"where ColumnId={column_select.SelectedValue}";
                DBHelper.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 代码折叠
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_contentID_TextChanged(object sender, EventArgs e)
        {
            //更新，以便进行代码折叠
            txt_contentID.Document.FoldingManager.UpdateFoldings(null, null);
        }
        /// <summary>
        /// 格式化代码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button9_Click(object sender, EventArgs e)
        {
            int linecount = txt_contentID.ActiveTextAreaControl.Caret.Line;
            if (IsBehideCode)
                txt_contentID.Text = CSharpFormatHelper.FormatCSharpCode(txt_contentID.Text);
            else
                txt_contentID.Text = CSharpFormatHelper.FormatHtmlCode(txt_contentID.Text);
            txt_contentID.ActiveTextAreaControl.Caret.Line = linecount;
        }
        /// <summary>
        /// 查看字段
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button11_Click(object sender, EventArgs e)
        {
            int modelid = Convert.ToInt32(ModelFiled_dal.GetModelIdByExpression("TableName='" + table_select.SelectedValue + "'", "ModelId")["ModelId"]);
            StringBuilder sbr = new StringBuilder();
            ModelFiled_dal.CreateModelHTML(modelid, ref sbr);
            txt_contentextension.Text = sbr.ToString();
            txt_contentextension.ActiveTextAreaControl.Caret.Line = sbr.Length;
        }
        /// <summary>
        /// 嵌套Repeater在代码外层
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tools_repeater_Click(object sender, EventArgs e)
        {
            string Tablename = table_select.SelectedValue.ToString();
            string repeater_name = txt_repeatername.Text.Trim();
            if (string.IsNullOrEmpty(repeater_name))
            {
                repeater_name = Tablename.Contains('_') ? !string.IsNullOrEmpty(Tablename.Split('_')[1]) ? Tablename.Split('_')[1] : "" : Tablename;
            }
            StringBuilder sbr = new StringBuilder();
            string selectcont = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
            txt_contentID.ActiveTextAreaControl.TextArea.InsertString("");
            txt_contentID.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();

            sbr.Append($"<!--Repeater_{repeater_name}_Start-->\n");
            sbr.Append($"<asp:Repeater ID=\"Repeater_{repeater_name}\" runat=\"server\">\n");
            sbr.Append("                <ItemTemplate>\n");
            sbr.Append(selectcont + "\n");
            sbr.Append("                </ItemTemplate>\n");
            sbr.Append("   </asp:Repeater>\n");
            sbr.Append($"<!--Repeater_{repeater_name}_End-->");

            txt_contentID.ActiveTextAreaControl.TextArea.InsertString(sbr.ToString());
        }
        /// <summary>
        /// 置顶（开）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tools_open_Click(object sender, EventArgs e)
        {
            tools_close.Checked = false;
            tools_open.Checked = true;
            TopMost = true;
        }
        /// <summary>
        /// 置顶（关）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tools_close_Click(object sender, EventArgs e)
        {
            tools_open.Checked = false;
            tools_close.Checked = true;
            TopMost = false;
        }
        /// <summary>
        /// 切换数据显示框显示隐藏
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button12_Click(object sender, EventArgs e)
        {
            if (panelIsShow)
            {
                groupBox2.Hide();
                button12.Text = "显示数据窗口";
            }
            else
            {
                groupBox2.Show();
                button12.Text = "隐藏数据窗口";
            }
            panelIsShow = panelIsShow == false ? true : false;
        }
        /// <summary>
        /// 使用数据显示框内字段及内容进行添加操作
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button13_Click(object sender, EventArgs e)
        {
            try
            {
                string str = string.IsNullOrEmpty(txt_count.Text) ? "0" : txt_count.Text;
                int count = Convert.ToInt32(str) == 0 ? 1 : Convert.ToInt32(str);
                if (count != 0)
                {
                    AddInTable(default_fileds, table_select.SelectedValue.ToString(), count);
                }
                else
                {
                    AddInTable(default_fileds, table_select.SelectedValue.ToString());
                }
                MessageBox.Show("添加成功");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 右键生成查询语句
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tools_codesql_Click(object sender, EventArgs e)
        {
            string str = GetEasyFieldsNewString(table_select.SelectedValue.ToString());//字段字符串
            txt_contentID.ActiveTextAreaControl.TextArea.InsertString(str);
        }
        //生成网站目录
        private void tools_createpage_Click(object sender, EventArgs e)
        {
            Createpage create = new Createpage();
            create.Show();
        }
        /// <summary>
        /// 转换字符串为stringbuilder
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tools_stringbuilder_Click(object sender, EventArgs e)
        {
            string code = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
            string[] lines = code.Split('\n').Length > 1 ? code.Split('\n') : code.Split('\r');
            StringBuilder sbr = new StringBuilder();
            for (int i = 0; i < lines.Length; i++)
            {
                sbr.Append($"sbr.Append(\"{lines[i].Trim().Replace("\"", "\\\"")}\");\n");
            }
            txt_contentID.ActiveTextAreaControl.TextArea.InsertString("");
            txt_contentID.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            txt_contentID.ActiveTextAreaControl.TextArea.InsertString(sbr.ToString());
        }
        /// <summary>
        /// Tools页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tools_toolpage_Click(object sender, EventArgs e)
        {
            DataTools tools = new DataTools();
            tools.Show();
        }
        /// <summary>
        /// 添加当前栏目子栏目
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button18_Click(object sender, EventArgs e)
        {
            string database = database_select.Text;//数据库名称
            string sql;
            string columnname = txt_cloumnname.Text.Trim();//栏目名
            int maxorderid = Convert.ToInt32(DBHelper.GetDataSet("select max(orderid) as maxorderid from ColumnCategory").Rows[0]["maxorderid"]);
            string parentid = column_select.SelectedValue.ToString();
            try
            {
                if (checkbox_issystem.Checked)//系统栏目
                {
                    sql = $"INSERT INTO [" + database + "].[dbo].[ColumnCategory]([ParentId],[SystemId],[ModelType],[Name],[EnglishName],[ColumnImage],[opentype],[OrderId],[Family],[KeyTitle],[SearchKey],[Description],[ColumnUrl],[IsShow],[Level],[ContentNum],[AddTime],[ChildrenCount],[IsMutiImages],[Url]) Values " +
                                "(0,1,-3,'" + columnname + "','','','_self'," + (maxorderid + 1) + ",'','" + columnname + "','" + columnname + "','" + columnname + "','','" + !checkbox_isshow.Checked + "'," + 0 + ",0,'" + DateTime.Now + "',0,'False','')";

                    if (DBHelper.ExecuteCommand(sql) > 0)
                    {
                        BindColumnList();
                    }
                }
                else
                {
                    DataTable dt = DBHelper.GetDataSet("select orderid,Level,family from ColumnCategory where ColumnId=" + parentid);
                    int orderid = 0;
                    int Level = 0;
                    string family = null;
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        orderid = Convert.ToInt32(dt.Rows[0]["orderid"]);
                        Level = Convert.ToInt32(dt.Rows[0]["Level"]);
                        family = string.IsNullOrEmpty(dt.Rows[0]["family"].ToString()) ? $",{parentid}," : $"{dt.Rows[0]["family"].ToString()}{parentid},";
                    }
                    sql = $"INSERT INTO [" + database + "].[dbo].[ColumnCategory]([ParentId],[SystemId],[ModelType],[Name],[EnglishName],[ColumnImage],[opentype],[OrderId],[Family],[KeyTitle],[SearchKey],[Description],[ColumnUrl],[IsShow],[Level],[ContentNum],[AddTime],[ChildrenCount],[IsMutiImages],[Url]) Values " +
                              "(" + parentid + ",1,-3,'" + columnname + "','','','_self'," + (maxorderid + 1) + ",'" + family + "','" + columnname + "','" + columnname + "','" + columnname + "','','" + !checkbox_isshow.Checked + "'," + (Level + 1) + ",0,'" + DateTime.Now + "',0,'False','')";
                    if (DBHelper.ExecuteCommand(sql) > 0)
                    {
                        DBHelper.ExecuteCommand("update [" + database + "].[dbo].[ColumnCategory] set ChildrenCount=ChildrenCount+1 where ColumnId=" + parentid + "");
                        BindColumnList();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        /// <summary>
        /// 字段类型切换（初始化）
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void fileds_select_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                type_select.Text = "普通";
                txt_filedname.Text = "";
            }
            catch (Exception ex)
            {

            }
        }
        #endregion

        #region 页面所用方法
        /// <summary>
        /// 生成精简字段字符串
        /// </summary>
        /// <summary>
        /// <returns></returns>
        public string GetEasyFieldsNewString(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string swhere = null;//读取数据条件
            string orderby = null;//排序方式
            string helperclass = null;//使用方法类
            string pagehtml = null;//单行数据使用PageDr多行Repeater
            string repeater_name = txt_repeatername.Text.Trim();
            bool isrepeater = checkbox_repeater.Checked;
            bool IsPage = checkbox_pages.Checked;
            if (string.IsNullOrEmpty(repeater_name))
            {
                repeater_name = "Repeater_" + (Tablename.Contains('_')
                    ? !string.IsNullOrEmpty(Tablename.Split('_')[1])
                    ? Tablename.Split('_')[1]
                    : ""
                    : Tablename);
            }
            DataTable dt = null;

            orderby = Tablename == "Article"
                ? "AddTime desc\""
                : "OrderId desc\"";
            string pid = null;//ParentId
            dt = DBHelper.GetDataSet("select distinct(ParentId)pid from " + Tablename + "");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count == 1)
                {
                    pid = dt.Rows[0]["pid"].ToString();
                }
                else
                {
                    pid = "\"+t+\"";
                }
            }
            //如果分页
            if (IsPage)
            {
                stringBuilder.Append("int recount=0;\n");
                stringBuilder.Append("int pageindex=1;\n");
                stringBuilder.Append("int pagesize=8;\n");
                stringBuilder.Append("if(Request[\"page\"]!=null)\n");
                stringBuilder.Append("{\n");
                stringBuilder.Append("    pageindex=WebUtility.getparam(\"page\");\n");
                stringBuilder.Append("}\n");

                orderby += ", pagesize, pageindex, ref recount);";
                helperclass = "Commonoperate.";
                pagehtml =
                    $"{repeater_name}.DataSource = dt;\n" +
                    $"{repeater_name}.DataBind();\n";
            }
            else
            {
                if (isrepeater)
                {
                    pagehtml =
                        $"{repeater_name}.DataSource = dt;\n" +
                        $"{repeater_name}.DataBind();\n";
                }
                else
                {
                    pagehtml = $"{pagedr_select.Text}=dt.Rows[0];\n";
                }
                orderby += ");";
            }
            swhere = $" and ParentId={(string.IsNullOrEmpty(pid) ? column_select.SelectedValue.ToString() : pid)}";

            //筛选不需要的字段
            string fild = "SystemId,Hits,KeyWord,Description,IsTop,IsRecommend,IsHot,IsSlide,IsColor,OrderId,AddUserName,LastEditUserName,LastEditDate,CheckedLevel";
            string[] filds = fild.Split(',');
            stringBuilder.Append($"\n//{table_select.Text}表内数据\n");
            stringBuilder.Append($"dt = {helperclass}GetDataList(\"{Tablename}\",\n \"");

            dt = DBHelper.GetDataSet($"Select Name FROM SysColumns Where id=Object_Id('{Tablename}') order by colid asc");

            foreach (DataRow dr in dt.Rows)
            {
                if (!filds.Contains(dr["Name"].ToString()))
                {
                    stringBuilder.Append("" + dr["Name"] + ",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            stringBuilder.Append($"\",\n \"IsColor=0{swhere}\",\n \"IsTop desc,IsRecommend desc,IsHot desc,IsSlide desc,{orderby}\n");
            stringBuilder.Append("if(dt!=null&&dt.Rows.Count>0)\n");
            stringBuilder.Append("{\n");
            stringBuilder.Append(pagehtml);
            stringBuilder.Append("}\n");
            return stringBuilder.ToString();
        }
        /// <summary>
        /// 获取图片宽高像素
        /// </summary>
        /// <param name="filre"></param>
        /// <returns></returns>
        public static string getimgswidthandheight(string filre)
        {
            if (!string.IsNullOrEmpty(filre))
            {
                try
                {
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(filre);
                    string newstr = bitmap.Width + "*" + bitmap.Height;
                    return newstr;
                }
                catch
                {
                    return "";
                }
            }
            return "";
        }
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
            if (str.Length == 1)
            {
                switch (schar)
                {
                    case "1": res = "一"; break;
                    case "2": res = "二"; break;
                    case "3": res = "三"; break;
                    case "4": res = "四"; break;
                    case "5": res = "五"; break;
                    case "6": res = "六"; break;
                    case "7": res = "七"; break;
                    case "8": res = "八"; break;
                    case "9": res = "九"; break;
                    default: res = "一"; break;
                }
            }
            else if (str.Length == 2)
            {
                schar = str.Substring(0, 2);
                switch (schar)
                {
                    case "10": res = "十"; break;
                    case "11": res = "十一"; break;
                    case "12": res = "十二"; break;
                    case "13": res = "十三"; break;
                    case "14": res = "十四"; break;
                    case "15": res = "十五"; break;
                    default: res = "十"; break;
                }
            }
            return res;
        }
        /// <summary>
        /// 数字转英文
        /// </summary>
        /// <param name="number">eg: 22</param>
        /// <returns></returns>
        public string NumberToEnglish(int number)
        {
            string res = string.Empty;
            string str = number.ToString();
            string schar = str.Substring(0, 1);
            if (str.Length == 1)
            {
                switch (schar)
                {
                    case "1": res = "f"; break;
                    case "2": res = "s"; break;
                    case "3": res = "t"; break;
                    case "4": res = "fo"; break;
                    case "5": res = "fif"; break;
                    case "6": res = "six"; break;
                    case "7": res = "sev"; break;
                    case "8": res = "eig"; break;
                    case "9": res = "nine"; break;
                    default: res = "f"; break;
                }
            }
            else if (str.Length == 2)
            {
                schar = str.Substring(0, 2);
                switch (schar)
                {
                    case "10": res = "ten"; break;
                    case "11": res = "ele"; break;
                    case "12": res = "twe"; break;
                    case "13": res = "thirteen"; break;
                    case "14": res = "foteen"; break;
                    case "15": res = "fifteen"; break;
                    default: res = "ten"; break;
                }
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
        public void BindDropDounlists(string database, ref bool result)
        {
            try
            {
                if (database != "")
                {
                    DataTable dt = null;
                    dt = DBHelper.GetDataSet("use " + database + " select name,(name+'('+ISNULL(tablename,'')+')') tablename from (select create_date,name,(select ModelName from ContentModel where sys.tables.name=ContentModel.tablename)as tablename from sys.tables where is_ms_shipped = 0)a  order by create_date desc");
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        table_select.DataSource = dt;
                        table_select.DisplayMember = "tablename";
                        table_select.ValueMember = "name";
                        table_select.Text = dt.Rows[0]["tablename"].ToString();

                        result = true;
                    }
                }
            }
            catch
            {
                result = false;
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
                dt = DBHelper.GetDataSet("select name,database_id from sys.databases order by create_date desc");
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.database_select.DataSource = dt;
                    this.database_select.DisplayMember = "name";
                    this.database_select.ValueMember = "database_id";
                    bool result = false;
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        try
                        {
                            this.database_select.Text = dt.Rows[i]["name"].ToString();
                            BindDropDounlists(database_select.Text, ref result);
                            if (!result)
                                continue;
                            else
                                break;
                        }
                        catch { }
                    }
                }
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
            finally
            {
            }
        }
        // <summary>
        /// 绑定栏目列表
        /// </summary>
        public void BindColumnList()
        {
            try
            {
                DataTable dt = null;
                dt = DBHelper.GetDataSet($"use {database_select.Text} select ColumnId,(Name+'【'+cast(ColumnId as nvarchar)+'】')as CName from ColumnCategory order by OrderId desc");

                if (dt != null && dt.Rows.Count > 0)
                {
                    this.column_select.DataSource = dt;
                    this.column_select.DisplayMember = "CName";
                    this.column_select.ValueMember = "ColumnId";
                    this.column_select.Text = dt.Rows[0]["CName"].ToString();
                }
                dt = DBHelper.GetDataSet($"select Name from ColumnCategory where ColumnId=(select ParentId from ColumnCategory where ColumnId={column_select.SelectedValue})");
                if (dt.Rows.Count > 0 && dt != null)
                {
                    lbl_parentname.Text = $"父级【{dt.Rows[0]["Name"]}】";
                }
                else
                {
                    lbl_parentname.Text = $"父级【系统栏目】";
                }
            }
            catch (Exception ex)
            {
            }
        }
        /// <summary>
        /// 添加自定义模型数据
        /// </summary>
        /// <param name="table"></param>
        /// <param name="tablename"></param>
        /// <returns></returns>
        public bool Add(Hashtable table, string tablename)
        {

            StringBuilder builder = new StringBuilder();
            builder.Append("insert into " + tablename + " (");
            string key = "";
            string keyvar = "";
            foreach (DictionaryEntry myDE in table)
            {
                key += "[" + myDE.Key.ToString() + "],";
                keyvar += "@" + myDE.Key.ToString() + ",";

            }

            builder.Append(key.Substring(0, key.Length - 1) + " ) values(" + keyvar.Substring(0, keyvar.Length - 1) + " )");

            SqlParameter[] commandParameters = new SqlParameter[table.Count];
            int num = 0;
            foreach (DictionaryEntry myDE in table)
            {
                commandParameters[num] = new SqlParameter("@" + myDE.Key.ToString(), myDE.Value);
                num++;
            }

            return DBHelper.ExecuteCommand(builder.ToString()) >= 1;
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
                    extension = Path.GetExtension(filename).ToLower();
                    name = filename.Substring(filename.LastIndexOf("\\") + 1).ToLower();

                    if (extension == ".aspx" || extension == ".html" || extension == ".master")
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
        /// <summary>
        /// 注释代码
        /// </summary>
        private string notesCode(string code)
        {
            StringBuilder sbr = new StringBuilder();
            string[] lines = code.Split('\n').Length > 1 ? code.Split('\n') : code.Split('\r');
            if (IsBehideCode)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    if (i == 0)
                        sbr.Append($"//{lines[i].Trim()}\n");
                    else
                        sbr.Append($"//{lines[i].Trim()}\n");
                }
            }
            else
            {
                sbr.Append($"<!--{code}-->");
            }
            return sbr.ToString().TrimEnd('\n');
        }
        /// <summary>
        /// 取消注释代码
        /// </summary>
        private string deletenotesCode(string code)
        {
            StringBuilder sbr = new StringBuilder();
            string[] lines = code.Split('\n').Length > 1 ? code.Split('\n') : code.Split('\r');
            if (IsBehideCode)
            {
                for (int i = 0; i < lines.Length; i++)
                {
                    sbr.Append($"{lines[i].Trim().TrimStart('/')}\n");
                }
            }
            else
            {
                code = code.Replace("<!--", "").Replace("-->", "");
                sbr.Append(code);
            }
            return sbr.ToString().TrimEnd('\n');
        }

        /// <summary>
        /// 通用添加数据
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        public static int AddInTable(Dictionary<string, string> dicts, string TableName)
        {
            StringBuilder sbr = getInsertSqlStr(dicts, TableName, 0);

            return DBHelper.ExecuteCommand(sbr.ToString());
        }
        /// <summary>
        /// 通用添加指定条数据
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="TableName"></param>
        /// <param name="count">添加条数</param>
        /// <returns></returns>
        public static int AddInTable(Dictionary<string, string> dicts, string TableName, int count)
        {
            StringBuilder sbr = getInsertSqlStr(dicts, TableName, count);

            return DBHelper.ExecuteCommand(sbr.ToString());
        }
        /// <summary>
        /// 获取添加语句
        /// </summary>
        /// <param name="dicts"></param>
        /// <param name="TableName"></param>
        /// <returns></returns>
        private static StringBuilder getInsertSqlStr(Dictionary<string, string> dicts, string TableName, int? count)
        {
            StringBuilder sbr = new StringBuilder();
            for (int i = 0; i < count; i++)
            {
                sbr.Append("insert into ");
                sbr.Append(TableName);
                count = count == 0 ? 1 : count;
                sbr.Append(" (");
                foreach (string key in dicts.Keys)
                {
                    sbr.Append($"[{key}]");
                    sbr.Append(",");
                }
                sbr.Remove(sbr.Length - 1, 1);
                sbr.Append(") values (");
                //填充数据
                foreach (string str in dicts.Values)
                {
                    sbr.Append(str);
                    sbr.Append(",");
                }
                sbr.Remove(sbr.Length - 1, 1);
                sbr.Append(")");
            }
            return sbr;
        }
        //随机化添加时间
        private string AddTimeRandom(string Tablename)
        {
            string swhere = !checkbox_selectall.Checked ? "where ParentId=" + column_select.SelectedValue : "";
            string sql = $"Update {Tablename} set AddTime = dateadd(minute, abs(checksum(newid())) % (datediff(minute, '2018-06-01', getdate()) + 1), '2018-06-01') {swhere}";
            try
            {
                int count = DBHelper.ExecuteCommand(sql);
                return $"修改成功,已修改{count}条数据";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //重命名标题
        private string TittleRandom(string Tablename)
        {
            string swhere = !checkbox_selectall.Checked ? "where ParentId=" + column_select.SelectedValue : "";
            DataTable dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            string title = "Tittle";
            string Id = "Id";
            if (dt.Rows.Count > 0 && dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["Name"].ToString();
                    if (name.Equals("ArticleTitle"))
                    {
                        title = "ArticleTitle";
                        Id = "ArticleId";
                        break;
                    }
                    else if (name.Equals("Tittle"))
                    {
                        title = "Tittle";
                        Id = "Id";
                        break;
                    }
                }
            }
            string sql = $"Update {Tablename} set {title}=(select Name from ColumnCategory where ColumnCategory.ColumnId={Tablename}.ParentId)+'_'+Cast({Id} as nvarchar) {swhere}";
            try
            {
                int count = DBHelper.ExecuteCommand(sql);
                return $"修改成功,已修改{count}条数据";
                //return $"{sql}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        //格式化OrderId
        private string OrderIdRandom(string Tablename)
        {
            string swhere = !checkbox_selectall.Checked ? "where ParentId=" + column_select.SelectedValue : "";
            DataTable dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            string Id = "Id";
            if (dt.Rows.Count > 0 && dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    string name = dr["Name"].ToString();
                    if (name.Equals("ArticleTitle"))
                    {
                        Id = "ArticleId";
                        break;
                    }
                    else if (name.Equals("Tittle"))
                    {
                        Id = "Id";
                        break;
                    }
                }
            }
            string sql = $"Update {Tablename} set OrderId={Id}-1 {swhere}";
            try
            {
                int count = DBHelper.ExecuteCommand(sql);
                return $"修改成功,已修改{count}条数据";
                //return $"{sql}";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion
    }
}
