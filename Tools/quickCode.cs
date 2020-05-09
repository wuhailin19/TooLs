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
using ICSharpCode.TextEditor.Document;
using NPinyin;

/// <summary>
/// 快速开发
/// </summary>
namespace Tools
{
    public partial class quickCode : AutoResizeForm
    {
        public quickCode()
        {
            InitializeComponent();
            txt_contentID.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("HTML");
            txt_contentID.Encoding = System.Text.Encoding.Default;

            txt_contentextension.Document.HighlightingStrategy = HighlightingStrategyFactory.CreateHighlightingStrategy("C#");
            txt_contentextension.Encoding = System.Text.Encoding.Default;
        }
        /// <summary>
        /// 静态页面路径选择
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
                BindDropDounlists(str);
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
            BindDropDounlists(str);
        }
        //快速添加字段
        private void button3_Click(object sender, EventArgs e)
        {
            ModelFiled model = new ModelFiled();
            int count = 0;
            string filedtype = fileds_select.SelectedValue.ToString();
            string filedname = txt_filedname.Text.Trim();//字段名

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
                            count = ModelFiled_dal.GetFiledCount(modelid, "title");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "标题" + NumberToChinese(count + 1);
                            }
                            model.FiledName = "title_" + NumberToEnglish(count + 1);
                            model.Alias = filedname;
                        }
                        else if (type_select.Text == "英文")
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "entitle");
                            model.FiledName = type_select.SelectedValue + "title_" + NumberToEnglish(count + 1);
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "英文标题" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                        }
                        else
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "linkurl");
                            model.FiledName = type_select.SelectedValue + NumberToEnglish(count + 1);
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "英文标题" + NumberToChinese(count + 1);
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
                            count = ModelFiled_dal.GetFiledCount(modelid, "enword");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "英文文字" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                            model.FiledName = type_select.SelectedValue + "word_" + NumberToEnglish(count + 1);
                        }
                        else
                        {
                            count = ModelFiled_dal.GetFiledCount(modelid, "word");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "文字" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                            model.FiledName = "word_" + NumberToEnglish(count + 1);
                        }

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
                            count = ModelFiled_dal.GetFiledCount(modelid, "img");
                            if (string.IsNullOrEmpty(filedname))
                            {
                                filedname = "图片" + NumberToChinese(count + 1);
                            }
                            model.Alias = filedname;
                            model.FiledName = "img_" + NumberToEnglish(count + 1);
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
                ModelFiled_dal.Add(model);
                //生成modelhtml
                StringBuilder sbr = new StringBuilder();
                ModelFiled_dal.CreateModelHTML(model.ModelId, ref sbr);
                txt_contentextension.Text = sbr.ToString();
                txt_contentextension.ActiveTextAreaControl.Caret.Line = sbr.Length;

                DropDownList3_SelectedIndexChanged(sender, e);
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
                tools_addfiled.DropDownItems.Clear();
                ToolStripItem toolStripItemf = new ToolStripMenuItem("标题");
                if (table_select.Text.Contains("Article"))
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

                int modelid = Convert.ToInt32(ModelFiled_dal.GetModelIdByExpression("TableName='" + table_select.SelectedValue + "'", "ModelId")["ModelId"]);
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
        /// <summary>
        /// 右键字段菜单点击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void contextMenuStrip1_ItemClick(object sender, EventArgs e)
        {
            ToolStripItem item = (ToolStripItem)sender;
            //string selectcont = txt_contentID.ActiveTextAreaControl.SelectionManager.SelectedText;
            txt_contentID.ActiveTextAreaControl.TextArea.InsertString("");
            txt_contentID.ActiveTextAreaControl.SelectionManager.RemoveSelectedText();
            string pagedr = pagedr_select.Text;
            string masterpage = "";
            if (checkBox1.Checked)
            {
                masterpage = "WebUtility.";
            }
            try
            {
                if (item.Name.Contains("title_"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString($"<%={masterpage}GetDataRowsValue({pagedr},\"{item.Name}\")%>");
                }
                else if (item.Name.Contains("word_"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString($"<%={masterpage}GetDataRowsValue({pagedr},\"{item.Name}\").Replace(\"\\r\\n\",\"<br/>\")%>");
                }
                else if (item.Name.Contains("desc_"))
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString($"<%={masterpage}GetDataRowEditorValue({pagedr},\"{item.Name}\")%>");
                }
                else
                {
                    txt_contentID.ActiveTextAreaControl.TextArea.InsertString($"<%={masterpage}GetDataRowsValue({pagedr},\"{item.Name}\")%>");
                }
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
            page_select_SelectedIndexChanged(sender, e);
            DropDownList3_SelectedIndexChanged(sender, e);
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
            codecontent = codecontent.Replace(" }\r\n        }\r\n        catch\r\n        {\r\n        }\r\n    }\r\n}\r\n", str + " }\r\n        }\r\n        catch\r\n        {\r\n        }\r\n    }\r\n}\r\n");
            txt_contentID.Text = codecontent;
            txt_contentID.ActiveTextAreaControl.Caret.Line = codecontent.Length;
            txt_contentextension.Text = str;
        }
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
            DataTable dt = null;
            if (Tablename == "Article")
            {
                stringBuilder.Append("int recount=0;\r\n");
                stringBuilder.Append("int pageindex=1;\r\n");
                stringBuilder.Append("int pagesize=8;\r\n");
                stringBuilder.Append("if(Request[\"page\"]!=null)\r\n");
                stringBuilder.Append("{\r\n");
                stringBuilder.Append("    pageindex=WebUtility.getparam(\"page\");\r\n");
                stringBuilder.Append("}\r\n");
                helperclass = "Commonoperate.";
            }
            //筛选不需要的字段
            string fild = "SystemId,Hits,KeyWord,Description,IsTop,IsRecommend,IsHot,IsSlide,IsColor,OrderId,AddUserName,LastEditUserName,LastEditDate,CheckedLevel";
            string[] filds = fild.Split(',');
            stringBuilder.Append($"dt = {helperclass}GetDataList(\"{Tablename}\", \"");

            dt = DBHelper.GetDataSet($"Select Name FROM SysColumns Where id=Object_Id('{Tablename}') order by colid asc");
            foreach (DataRow dr in dt.Rows)
            {
                if (!filds.Contains(dr["Name"].ToString()))
                {
                    stringBuilder.Append("" + dr["Name"] + ",");
                }
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);
            if (Tablename == "Article")
                orderby = "AddTime desc\", pagesize, pageindex, ref recount);";
            else
                orderby = "OrderId desc\");";
            dt = DBHelper.GetDataSet("select distinct(ParentId)pid from " + Tablename + "");
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows.Count > 1)
                {
                    swhere = " and ParentId=\"+t+\"";
                    pagehtml = "Repeater_swipers.DataSource = dt;\r\n" +
                    "Repeater_swipers.DataBind();\r\n";
                }
                else
                {
                    swhere = " and ParentId=" + dt.Rows[0]["pid"].ToString();
                    pagehtml = $"  {pagedr_select.Text}=dt.Rows[0];\r\n";
                }
            }
            stringBuilder.Append($"\", \"IsColor=0{swhere}\", \"IsTop desc,IsRecommend desc,IsHot desc,IsSlide desc,{orderby}\r\n");
            stringBuilder.Append("if(dt!=null&&dt.Rows.Count>0)\r\n");
            stringBuilder.Append("{\r\n");
            stringBuilder.Append(pagehtml);
            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }

        #region 页面所用方法
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
                catch (Exception ex)
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
                    }
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
                dt = DBHelper.GetDataSet("select name,database_id from sys.databases order by create_date desc");
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
        // <summary>
        /// 绑定栏目列表
        /// </summary>
        public void BindColumnList()
        {
            try
            {
                DataTable dt = null;
                dt = DBHelper.GetDataSet("select Id,Name from ColumnCategory order by OrderId asc");
                if (dt != null && dt.Rows.Count > 0)
                {
                    this.column_select.DataSource = dt;
                    this.column_select.DisplayMember = "Name";
                    this.column_select.ValueMember = "Id";
                    this.column_select.Text = dt.Rows[0]["Name"].ToString();
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
        #endregion
        private bool IsBehideCode = false;//全局变量指示现在的代码是不是后台代码
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
                        StreamWriter sw = new StreamWriter(fs);
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
            string modelid = ModelFiled_dal.GetModelIdByExpression($"TableName={table_select.SelectedValue}", "ModelId")["ModelId"].ToString();
            try
            {
                string sql = $"update ColumnCategory set ModelType={modelid} where ColumnId={column_select.SelectedValue}";
                DBHelper.ExecuteCommand(sql);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
    /// <summary>
    /// 自适应窗体
    /// </summary>
    public class AutoResizeForm : Form
    {
        const int WM_SYSCOMMAND = 0X112;//274
        const int SC_MAXIMIZE = 0XF030;//61488
        const int SC_MINIMIZE = 0XF020;//61472
        const int SC_RESTORE = 0XF120; //61728
        const int SC_CLOSE = 0XF060;//61536
        const int SC_RESIZE_Horizontal = 0XF002;//61442
        const int SC_RESIZE_Vertical = 0XF006;//61446
        const int SC_RESIZE_Both = 0XF008;//61448

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_SYSCOMMAND)
            {
                switch (m.WParam.ToInt32())
                {
                    case SC_MAXIMIZE:
                    case SC_RESTORE:
                    case SC_RESIZE_Horizontal:
                    case SC_RESIZE_Vertical:
                    case SC_RESIZE_Both:
                        if (WindowState == FormWindowState.Minimized)
                        {
                            base.WndProc(ref m);
                        }
                        else
                        {
                            Size beforeResizeSize = this.Size;
                            base.WndProc(ref m);
                            //窗口resize之后的大小
                            Size afterResizeSize = this.Size;
                            //获得变化比例
                            float percentWidth = (float)afterResizeSize.Width / beforeResizeSize.Width;
                            float percentHeight = (float)afterResizeSize.Height / beforeResizeSize.Height;
                            foreach (Control control in this.Controls)
                            {
                                //按比例改变控件大小
                                control.Width = (int)(control.Width * percentWidth);
                                control.Height = (int)(control.Height * percentHeight);
                                //为了不使控件之间覆盖 位置也要按比例变化
                                control.Left = (int)(control.Left * percentWidth);
                                control.Top = (int)(control.Top * percentHeight);
                                //改变控件字体大小
                                //control.Font = new Font(control.Font.Name, control.Font.Size * Math.Min(percentHeight, percentHeight), control.Font.Style, control.Font.Unit);
                            }
                        }
                        break;
                    default:
                        base.WndProc(ref m);
                        break;
                }
            }
            else
            {
                base.WndProc(ref m);
            }
        }
    }
}
