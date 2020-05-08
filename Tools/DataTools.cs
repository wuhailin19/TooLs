using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;

namespace Tools
{
    public partial class DataTools : Form
    {
        public DataTools()
        {
            InitializeComponent();
            BindDropDownList();
            comboBox3.Text = "dataTable";
        }
        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            string str = DropDownList2.Text;
            if (str == "System.Data.DataRowView")
            {
                str = "master";
            }
            BindDropDounlists(str);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            switch (comboBox3.SelectedIndex.ToString())
            {
                case "0":
                    txt_resultID.Text = getDataTableRowString(DropDownList3.Text);
                    break;
                case "1":
                    txt_resultID.Text = GetEasyFieldsNewString(DropDownList3.Text);
                    break;
                case "2":
                    txt_resultID.Text = GetFieldsNewString(DropDownList3.Text);
                    break;
                case "3":
                    txt_resultID.Text = Htmlstringtojsstring();
                    break;
                case "4":
                    txt_resultID.Text = getaspstring(DropDownList3.Text);
                    break;
                case "5":
                    txt_resultID.Text = getRepeaterstring(DropDownList3.Text);
                    break;
                case "6":
                    txt_resultID.Text = getjquerystr();
                    break;
                case "7":
                    txt_resultID.Text = quickrolePromession();
                    break;
                case "8":
                    txt_resultID.Text = Htmlstringtosbrstring();
                    break;
                case "9":
                    txt_resultID.Text = getstringlength();
                    break;
                case "10":
                    txt_resultID.Text = getPageDrstring(DropDownList3.Text);
                    break;
                case "11":
                    txt_resultID.Text = getPageDrValueToHtmlstring(DropDownList3.Text);
                    break;
                case "12":
                    txt_resultID.Text = getMatherPageDrstring(DropDownList3.Text);
                    break;
                case "13":
                    txt_resultID.Text = CopyTableData(DropDownList3.Text, txt_contentID.Text);
                    break;
                case "14":
                    txt_resultID.Text = AddTimeRandom(DropDownList3.Text);
                    break;
                case "15":
                    txt_resultID.Text = TittleOrderIdRandom(DropDownList3.Text);
                    break;
            }
        }
        //重命名标题以及格式化OrderId
        private string TittleOrderIdRandom(string Tablename)
        {
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
            string sql = $"Update {Tablename} set {title}=(select Name from ColumnCategory where ColumnCategory.ColumnId={Tablename}.ParentId)+'_'+Cast({Id} as nvarchar),OrderId={Id}-1";
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
        //随机化添加时间
        private string AddTimeRandom(string Tablename)
        {
            string sql = $"Update {Tablename} set AddTime = dateadd(minute, abs(checksum(newid())) % (datediff(minute, '2018-06-01', getdate()) + 1), '2018-06-01')";
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
        //复制表数据
        private string CopyTableData(string Tablename, string ParentId)
        {
            string swhere = null;
            if (!string.IsNullOrEmpty(ParentId))
            {
                swhere = " where ParentId=" + ParentId;
            }
            string insertsql = null;
            DataTable dt = null;
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            if (dt.Rows.Count > 0 && dt != null)
            {
                insertsql = $"INSERT INTO [{Tablename}](";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Name"].ToString() != "Id" && dr["Name"].ToString() != "ArticleId")
                        insertsql += $"[{dr["Name"]}],";
                }
                insertsql = insertsql.Remove(insertsql.LastIndexOf(","), 1);
                insertsql += ")";
                insertsql += "Select ";
                foreach (DataRow dr in dt.Rows)
                {
                    if (dr["Name"].ToString() != "Id" && dr["Name"].ToString() != "ArticleId")
                        insertsql += $"[{dr["Name"]}],";
                }
                insertsql = insertsql.Remove(insertsql.LastIndexOf(","), 1);
                insertsql += $" FROM [{Tablename}] {swhere}";
            }
            try
            {
                int count = DBHelper.ExecuteCommand(insertsql);
                return $"复制成功,已复制{count}条数据";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #region 海林方法

        public void BindDropDownList()
        {
            DataTable dt = null;
            dt = DBHelper.GetDataSet("select name,database_id from sys.databases order by create_date desc");
            if (dt != null && dt.Rows.Count > 0)
            {
                DropDownList2.DataSource = dt;
                DropDownList2.DisplayMember = "name";
                DropDownList2.ValueMember = "database_id";
                DropDownList2.Text = dt.Rows[0]["name"].ToString();
                BindDropDounlists(DropDownList2.Text);
            }
        }
        public void BindDropDounlists(string database)
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
        /// <summary>
        /// case "0"：获取dataTable行内元素
        /// </summary>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public string getDataTableRowString(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = null;
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            if (dt.Rows.Count > 0 && dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    stringBuilder.Append("" + getNewtablename(Tablename) + "_" + dr["Name"] + " = dt.Rows[0][\"" + dr["Name"] + "\"].ToString();\r\n");
                }
            }
            return stringBuilder.ToString();
        }
        public string getNewtablename(string Tablename)
        {
            string newtbn = null;
            if (!string.IsNullOrEmpty(Tablename))
            {
                string[] newstring = Tablename.Split('_');
                if (newstring.Length == 2)
                {
                    newtbn = newstring[1];
                }
                else
                {
                    newtbn = Tablename;
                }
            }
            return newtbn;
        }
        /// <summary>
        /// case "1"：生成精简字段字符串
        /// </summary>
        /// <summary>
        /// <returns></returns>
        public string GetEasyFieldsNewString(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            string swhere = null;//读取数据条件
            string orderby = null;//排序方式
            string helperclass = null;//使用方法类
            DataTable dt = null;
            if (Tablename == "Article")
            {
                stringBuilder.Append("int recount=0;\r\n");
                stringBuilder.Append("int pageindex=1;\r\n");
                stringBuilder.Append("int pagesize=8;\r\n");
                stringBuilder.Append("if(Request[\"page\"]!=null);\r\n");
                stringBuilder.Append("{\r\n");
                stringBuilder.Append("page=WebUtility.getparam(\"page\");\r\n");
                stringBuilder.Append("}\r\n");
                helperclass = "Commonoperate.";
            }
            string filds = "SystemId,Hits,KeyWord,Description,IsTop,IsRecommend,IsHot,IsSlide,IsColor,OrderId,AddUserName,LastEditUserName,LastEditDate,CheckedLevel";
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
                    swhere = " and ParentId=\"+t";
                }
                else
                {
                    swhere = " and ParentId=" + dt.Rows[0]["pid"].ToString() + "\"";
                }
            }
            stringBuilder.Append($"\", \"IsColor=0{swhere}, \"IsTop desc,IsRecommend desc,IsHot desc,IsSlide desc,{orderby}\r\n");
            stringBuilder.Append("if(dt!=null&&dt.Rows.Count>0)\r\n");
            stringBuilder.Append("{\r\n");
            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }

        //public string GetPublicNews(string Tablename) {
        //StringBuilder stringBuilder = new StringBuilder();
        //DataTable dt = null;
        //dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
        //         if (dt != null && dt.Rows.Count > 0)
        //         {
        //             stringBuilder.Append("public string ");
        //             for (int i = 0; i < dt.Rows.Count; i++)
        //             {
        //                 if (i != dt.Rows.Count - 1)
        //                 {
        //                     stringBuilder.Append(getNewtablename(Tablename) + "_" + dt.Rows[i]["Name"] + " = null,");
        //                 }
        //                 else
        //                 {
        //                     stringBuilder.Append(getNewtablename(Tablename) + "_" + dt.Rows[i]["Name"] + " = null;");
        //                 }
        //             }


        //         }

        //string test = "[Id],[ParentId],[SystemId],[Tittle],[Hits],[KeyWord],[Description],[AddTime],[IsTop],[IsRecommend],[IsHot],[IsSlide],[IsColor],[OrderId],[AddUserName],[LastEditUserName],[LastEditDate],[CheckedLevel],[Img],[summary],[contents],[videofile]";
        //string[] tests = test.Split(',');
        //foreach (string str in tests) {
        //	stringBuilder.Append("<%=specials_" + str + "%>\r\n");
        //}
        //	return stringBuilder.ToString();
        //}

        /// <summary>
        /// case "2"：生成字段字符串
        /// </summary>
        /// <returns></returns>
        public string GetFieldsNewString(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = null;
            stringBuilder.Append("dt = GetDataList(\"" + Tablename + "\", \"");
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            foreach (DataRow dr in dt.Rows)
            {
                stringBuilder.Append("" + dr["Name"] + ",");
            }
            stringBuilder.Remove(stringBuilder.Length - 1, 1);

            stringBuilder.Append("\", \"IsColor=0 and ParentId=\", \"IsTop desc,IsRecommend desc,IsHot desc,IsSlide desc,OrderId desc\");\r\n");
            stringBuilder.Append("if(dt!=null&&dt.Rows.Count>0)\r\n");
            stringBuilder.Append("{\r\n");
            stringBuilder.Append("}\r\n");
            return stringBuilder.ToString();
        }
        /// <summary>
        /// case "3"：html字符串转换
        /// </summary>
        /// <returns></returns>
        public string Htmlstringtojsstring()
        {
            string newstring = null;
            string str = txt_contentID.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                newstring = str.Replace("\"", "\\\"");
                newstring = newstring.Replace("\r\n", "");
                newstring = Regex.Replace(newstring, "\\s{2,}", "");
            }
            return newstring;
        }
        /// <summary>
        /// case "9"：html字符串转换2
        /// </summary>
        /// <returns></returns>

        public string Htmlstringtosbrstring()
        {
            string newstring = null;
            string str = txt_contentID.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                newstring = str.Replace("\"", "\\\"");
                //newstring = Regex.Replace(newstring, "\\s{15,}", "");
                newstring = "sbr.Append(\"" + newstring.Replace("\r\n", "\");\r\nsbr.Append(\"") + "\");";
                //newstring =  newstring.Replace("\r\n", "\\r\\n\"+\r\n\"");
            }
            return newstring;
        }
        /// <summary>
        /// case "4"：aspx服务器端代码
        /// </summary>
        /// <returns></returns>
        public string getaspstring(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = null;
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            foreach (DataRow dr in dt.Rows)
            {
                stringBuilder.Append("<%=" + getNewtablename(Tablename) + "_" + dr["Name"] + "%>\r\n");
            }
            return stringBuilder.ToString();
        }


        /// <summary>
        /// case "5"：Repeater代码
        /// </summary>
        /// <returns></returns>
        public string getRepeaterstring(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = null;
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            foreach (DataRow dr in dt.Rows)
            {
                stringBuilder.Append("<%#Eval(\"" + dr["Name"] + "\")%>\r\n");
            }
            //string test = "[Id],[ParentId],[SystemId],[Tittle],[Hits],[KeyWord],[Description],[AddTime],[IsTop],[IsRecommend],[IsHot],[IsSlide],[IsColor],[OrderId],[AddUserName],[LastEditUserName],[LastEditDate],[CheckedLevel],[Img],[summary],[contents],[videofile]";
            //string[] tests = test.Split(',');
            //foreach (string str in tests) {
            //	stringBuilder.Append("<%#Eval(\"" + str + "\")%>\r\n");
            //}
            return stringBuilder.ToString();
        }

        /// <summary>
        /// case "7"：快速分配权限
        /// </summary>
        /// <returns></returns>
        public string quickrolePromession()
        {
            DataTable dt = null;
            DataTable dt2 = null;
            int count = 0;
            DataTable model = null;
            string tablename = null;
            try
            {
                dt = DBHelper.GetDataSet("select ModelType,ColumnId from ColumnCategory order by OrderId asc,ColumnId desc");
                List<string> promissionstring = new List<string>();
                if (dt != null && dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["ModelType"].ToString() != "-3")
                        {
                            if (dr["ModelType"].ToString() != "-1")
                            {
                                model = DBHelper.GetDataSet("select TableName from ContentModel where ModelId=" + dr["ModelType"]);
                                if (model != null)
                                {
                                    tablename = model.Rows[0]["TableName"].ToString();
                                    count = Convert.ToInt32(DBHelper.GetDataSet("select count(*)as Count_Filed from(select distinct(ParentId) from " + tablename + ") as T").Rows[0][0]);
                                    if (count > 1)
                                    {
                                        dt2 = DBHelper.GetDataSet("select distinct(ParentId)as Count_Filed,count(ParentId)as count_ParentId from  " + tablename + " group by ParentId");
                                        if (dt2.Rows != null && dt2.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr2 in dt2.Rows)
                                            {
                                                if (Convert.ToInt32(dr2["count_ParentId"]) > 1)
                                                {
                                                    if (!promissionstring.Contains($"{dr2["Count_Filed"]}|30"))
                                                        promissionstring.Add($"{dr2["Count_Filed"]}|30");
                                                }
                                                else
                                                {
                                                    if (!promissionstring.Contains($"{dr2["Count_Filed"]}|10"))
                                                        promissionstring.Add(dr2["Count_Filed"] + "|10");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!promissionstring.Contains($"{dr["ColumnId"]}|10"))
                                                promissionstring.Add($"{dr["ColumnId"]}|10");
                                        }
                                    }
                                    else if (count == 1)
                                    {
                                        dt2 = DBHelper.GetDataSet("select distinct(ParentId)as Count_Filed,count(ParentId)as count_ParentId from  " + tablename + " group by ParentId");
                                        if (dt2.Rows != null && dt2.Rows.Count > 0)
                                        {
                                            foreach (DataRow dr2 in dt2.Rows)
                                            {
                                                if (Convert.ToInt32(dr2["count_ParentId"]) > 1)
                                                {
                                                    if (!promissionstring.Contains($"{dr2["Count_Filed"]}|30"))
                                                        promissionstring.Add($"{dr2["Count_Filed"]}|30");
                                                }
                                                else
                                                {
                                                    if (!promissionstring.Contains($"{dr2["Count_Filed"]}|10"))
                                                        promissionstring.Add($"{dr2["Count_Filed"]}|10");
                                                }
                                            }
                                        }
                                        else
                                        {
                                            if (!promissionstring.Contains($"{dr["ColumnId"]}|10"))
                                                promissionstring.Add($"{dr["ColumnId"]}|10");
                                        }
                                    }
                                    else
                                    {
                                        if (!promissionstring.Contains($"{dr["ColumnId"]}|10"))
                                            promissionstring.Add($"{dr["ColumnId"]}|10");
                                    }
                                }
                            }
                            else
                            {
                                if (!promissionstring.Contains($"{dr["ColumnId"]}|30"))
                                    promissionstring.Add($"{dr["ColumnId"]}|30");
                            }
                        }
                        else
                        {
                            if (!promissionstring.Contains($"{dr["ColumnId"]}|10"))
                                promissionstring.Add($"{dr["ColumnId"]}|10");
                        }
                    }
                }
                string str = null;
                string strs = "ColumnShow,ColumnEditor,PictureShow,PictureAdd,PictureEditor,PictureDelete,PictureTypeShow,PictureTypeAdd,PictureTypeEditor,PictureTypeDelete,FilesShow,FilesAdd,FilesEditor,FilesDelete,FileTypeShow,FileTypeAdd,FileTypeEditor,FileTypeDelete,webConfigShow,AdministrationShow,";
                for (int i = 0; i < promissionstring.Count; i++)
                {
                    str += promissionstring[i] + ",";
                }
                str = str.Remove(str.Length - 1, 1);
                str += "&";

                int result = DBHelper.ExecuteCommand($"update RolesPermissions set NodesPermissions='{str}',websitePermissions='{strs}' where RolesID=3");
                if (result > 0)
                    return "修改成功";
                else
                    return "修改失败";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        ////string newstr = null;
        //if (!string.IsNullOrEmpty(newstring)) {
        //	for (int i = 0; i < newstring.Length; i++) {
        //		if ((i + 1) % 4 == 0) {
        //			newstr += newstring[i] + " " + "\r\n";
        //		}
        //		else {
        //			newstr += newstring[i];
        //		}
        //	}
        //}
        //else {
        //	return "";
        //}


        ///// <summary>
        ///// case "7"：筛选英文字符
        ///// </summary>
        ///// <returns></returns>
        //public string getRepeaterstring(string Tablename) {
        //	StringBuilder stringBuilder = new StringBuilder();
        //	DataTable dt = null;
        //	dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "')");
        //	foreach (DataRow dr in dt.Rows) {
        //		stringBuilder.Append("<%#Eval(\"" + dr["Name"] + "\")%>\r\n");
        //	}
        //	return stringBuilder.ToString();
        //}
        /// <summary>
        /// 命名jquery变量
        /// </summary>
        /// <returns></returns>
        public string getjquerystr()
        {
            string str = txt_contentID.Text.Trim();
            string newstring = null;
            if (!string.IsNullOrEmpty(str))
            {
                string[] strs = str.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                if (strs.Length > 0 && strs != null)
                {
                    string stri = null;
                    string newi = null;
                    string newstrs = null;
                    string varstrs = null;
                    for (int i = 0; i < strs.Length; i++)
                    {
                        stri = strs[i][0].ToString();
                        newi = stri.ToUpper();
                        newstrs = strs[i].Remove(0, 1);
                        varstrs += "var " + strs[i] + "=jQuery(\"#" + strs[i] + "\");\r\n";
                        //newstring = "var " + strs[i] + "=$(\"input[name='" + strs[i] + "']:checked\").next('label').html();\r\n";//示例：city.val(),
                        //newstring += "s_lists.Add(\"" + strs[i] + "\");\r\n";//示例：city.val(),

                        //newstring += "var " + strs[i].Replace("-", "_") + "=$(\"input[name='" + strs[i] + "']:checked\").next('label').html();\r\n";//示例：city.val(),
                        //newstring += strs[i] + ".val(),";//示例：city.val(),
                        //newstring += strs[i] + ".val(\"\"),";//示例：city.val(""),
                        //newstring += newi + newstrs + ":"+strs[i] + ".val(),";//示例：City:city.val(),
                        //newstring += "string " + newi + newstrs + " =WebUtility.FilterInputText(WebUtility.GetRequest(\"" + newi + newstrs + "\", \"1\"), 10);\r\n";
                        //newstring += "\"" + newi + newstrs + "：\" + " + newi + newstrs + " + \"<br/>\" +\r\n";//示例："固定电话：" + Tel + "<br/>" +
                        //newstring = varstrs;//示例：var city = jquery("#city");
                        //string strss = Regex.Replace(strs[i].ToString(), "\\s{2,}", "");
                        //newstring += strss.Substring(4, strss.IndexOf('=') - 4).Replace(" ","") + "\r\n";
                        //newstring += newi + newstrs + ":" + strs[i].ToString()+",";
                    }
                }
            }
            return newstring;
        }

        //获取字符串长度
        public string getstringlength()
        {
            string str = txt_contentID.Text;
            return str.Length.ToString();
        }

        private void txt_resultID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txt_resultID.SelectAll();
        }

        private void txt_contentID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.A)
                txt_contentID.SelectAll();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            BindDropDounlists(DropDownList2.Text);
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void 开启ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = true;
        }

        private void 关闭ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TopMost = false;
        }

        private void 添加数据库ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DataOperas dataop = new DataOperas();
            dataop.Show();
        }

        private void 处理图片ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            testimg testimg = new testimg();
            testimg.Show();
        }

        private void 创建动态页面ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Createpage create = new Createpage();
            create.Show();
        }

        private void 处理编译网站ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CompileWebsite com = new CompileWebsite();
            com.Show();
        }

        private void 添加栏目ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            InsertColumns insert = new InsertColumns();
            insert.Show();
        }
        #endregion
        #region 我自己添加的方法
        public string getPageDrstring(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = null;
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            foreach (DataRow dr in dt.Rows)
            {
                stringBuilder.Append("<%=" + "GetDataRowsValue(PageDr,\"" + dr["Name"] + "\")%>\r\n");
            }
            return stringBuilder.ToString();
        }

        public string getPageDrValueToHtmlstring(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = null;
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            foreach (DataRow dr in dt.Rows)
            {
                stringBuilder.Append("<%=" + "GetDataRowEditorValue(PageDr,\"" + dr["Name"] + "\")%>\r\n");
            }
            return stringBuilder.ToString();
        }

        public string getMatherPageDrstring(string Tablename)
        {
            StringBuilder stringBuilder = new StringBuilder();
            DataTable dt = null;
            dt = DBHelper.GetDataSet("Select Name FROM SysColumns Where id=Object_Id('" + Tablename + "') order by colid asc");
            foreach (DataRow dr in dt.Rows)
            {
                stringBuilder.Append("<%=" + "WebUtility.GetDataRowsValue(PageDr,\"" + dr["Name"] + "\")%>\r\n");
            }
            return stringBuilder.ToString();
        }

        #endregion

        private void 快速开发ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            quickCode quickCode = new quickCode();
            quickCode.Show();
        }
    }
}
