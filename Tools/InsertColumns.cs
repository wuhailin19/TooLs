using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;
using System.Collections;
using System.Threading;

namespace Tools
{
    public partial class InsertColumns : Form, INotifyPropertyChanged
    {
        public InsertColumns()
        {
            InitializeComponent();
            BindDropDownList();
            lbl_mark.Text = "Ctrl+1为增加选中\r\n区域栏目等级;\r\n\r\nCtrl+2为减小选中\r\n区域栏目等级;";
        }

        //禁用Tab键
        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Tab)
            {
                TextBox tb = this.ActiveControl as TextBox;
                if (tb != null && tb.Name == "txt_content")
                    return false;
            }
            return base.ProcessDialogKey(keyData);
        }
        public void BindDropDownList()
        {
            DataTable dt = null;
            dt = DBHelper.GetDataSet("select name,database_id from sys.databases order by create_date desc");
            if (dt != null && dt.Rows.Count > 0)
            {
                dt_datasource.DataSource = dt;
                dt_datasource.DisplayMember = "name";
                dt_datasource.ValueMember = "database_id";
                dt_datasource.Text = dt.Rows[0]["name"].ToString();
            }
        }
        private void btn_submit_Click(object sender, EventArgs e)
        {
            string database = null;
            if (dt_datasource.Text != "")
                database = dt_datasource.Text;
            if (string.IsNullOrEmpty(database))
                return;
            if (string.IsNullOrEmpty(txt_content.Text))
                return;
            string[] strs = null;
            Hashtable hash = new Hashtable();
            string content_strs = txt_content.Text;
            strs = content_strs.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (strs != null)
            {
                string guidid = null;
                int parentid = 0;
                int oldparentid = 0;
                int thridparentid = 0;
                string sql = null;
                for (int i = 0; i < strs.Length; i++)
                {
                    guidid = strs[i][0].ToString();

                    if (guidid == "1")
                    {
                        sql = "INSERT INTO [" + database + "].[dbo].[ColumnCategory]([ParentId],[SystemId],[ModelType],[Name],[EnglishName],[ColumnImage],[opentype],[OrderId],[Family],[KeyTitle],[SearchKey],[Description],[ColumnUrl],[IsShow],[Level],[ContentNum],[AddTime],[ChildrenCount],[IsMutiImages],[Url]) Values " +
                            "(0,1,-3,'" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','','_self'," + i + ",'','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','True'," + (Convert.ToInt32(guidid) - 1) + ",0,'" + DateTime.Now + "',0,'False','')";
                        if (DBHelper.ExecuteCommand(sql) > 0)
                        {
                            parentid = Convert.ToInt32(DBHelper.GetDataSet("select ColumnId from  [" + database + "].[dbo].[ColumnCategory] where Name='" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "'").Rows[0]["ColumnId"]);
                        }
                    }
                    else if (guidid == "2")
                    {
                        sql = "INSERT INTO [" + database + "].[dbo].[ColumnCategory]([ParentId],[SystemId],[ModelType],[Name],[EnglishName],[ColumnImage],[opentype],[OrderId],[Family],[KeyTitle],[SearchKey],[Description],[ColumnUrl],[IsShow],[Level],[ContentNum],[AddTime],[ChildrenCount],[IsMutiImages],[Url]) Values " +
                            "(" + parentid + ",1,-3,'" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','','_self'," + i + ",'" + "," + parentid + "," + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','True'," + (Convert.ToInt32(guidid) - 1) + ",0,'" + DateTime.Now + "',0,'False','')";
                        if (DBHelper.ExecuteCommand(sql) > 0)
                        {
                            oldparentid = Convert.ToInt32(DBHelper.GetDataSet("select ColumnId from  [" + database + "].[dbo].[ColumnCategory] where Name='" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "'").Rows[0]["ColumnId"]);
                            DBHelper.ExecuteCommand("update [" + database + "].[dbo].[ColumnCategory] set ChildrenCount=ChildrenCount+1 where ColumnId=" + parentid + "");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else if (guidid == "3")
                    {
                        sql = "INSERT INTO [" + database + "].[dbo].[ColumnCategory]([ParentId],[SystemId],[ModelType],[Name],[EnglishName],[ColumnImage],[opentype],[OrderId],[Family],[KeyTitle],[SearchKey],[Description],[ColumnUrl],[IsShow],[Level],[ContentNum],[AddTime],[ChildrenCount],[IsMutiImages],[Url]) Values " +
                            "(" + oldparentid + ",1,-3,'" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','','_self'," + i + ",'" + "," + parentid + "," + oldparentid + "," + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','True'," + (Convert.ToInt32(guidid) - 1) + ",0,'" + DateTime.Now + "',0,'False','')";
                        if (DBHelper.ExecuteCommand(sql) > 0)
                        {
                            thridparentid = Convert.ToInt32(DBHelper.GetDataSet("select ColumnId from  [" + database + "].[dbo].[ColumnCategory] where Name='" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "'").Rows[0]["ColumnId"]);
                            DBHelper.ExecuteCommand("update [" + database + "].[dbo].[ColumnCategory] set ChildrenCount=ChildrenCount+1 where ColumnId=" + oldparentid + "");
                        }
                        else
                        {
                            break;
                        }
                    }
                    else
                    {
                        sql = "INSERT INTO [" + database + "].[dbo].[ColumnCategory]([ParentId],[SystemId],[ModelType],[Name],[EnglishName],[ColumnImage],[opentype],[OrderId],[Family],[KeyTitle],[SearchKey],[Description],[ColumnUrl],[IsShow],[Level],[ContentNum],[AddTime],[ChildrenCount],[IsMutiImages],[Url]) Values " +
                            "(" + thridparentid + ",1,-3,'" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','','_self'," + i + ",'" + "," + parentid + "," + oldparentid + "," + thridparentid + "," + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','" + strs[i].Substring(strs[i].LastIndexOf('-') + 1) + "','','True'," + (Convert.ToInt32(guidid) - 1) + ",0,'" + DateTime.Now + "',0,'False','')";
                        if (DBHelper.ExecuteCommand(sql) > 0)
                        {
                            DBHelper.ExecuteCommand("update [" + database + "].[dbo].[ColumnCategory] set ChildrenCount=ChildrenCount+1 where ColumnId=" + oldparentid + "");
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                DBHelper.ExecuteCommand("update [" + database + "].[dbo].[ColumnCategory] set Url='?t='+cast(ColumnId as nvarchar)");
            }
        }

        public class family
        {
            //父级Id
            public int ParentId { set; get; }
            //Family
            public string Value { set; get; }
            //Name
            public string Name { set; get; }
            //子级
            public Hashtable Children { set; get; }
            //子级个数
            public int Childrennum { set; get; }
        }


        private void button1_Click(object sender, EventArgs e)
        {
            string content_strs = txt_content.Text;
            Hashtable newhash = new Hashtable();
            Hashtable namehash = new Hashtable();
            string[] strs = content_strs.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
            if (strs != null)
            {
                string guidid = null;
                string name = null;
                for (int i = 0; i < strs.Length; i++)
                {
                    guidid = strs[i][0].ToString();
                    name = strs[i].Substring(strs[i].LastIndexOf('-') + 1);
                    newhash.Add(i, guidid);
                    namehash.Add(i, name);
                }
                string newstring = null;
                for (int i = 0; i < newhash.Count; i++)
                {
                    if (newhash[i].ToString() == "1")
                    {

                    }
                }
                txt_content.Text = newstring;
            }
        }

        //int val = 1;
        //string family = null;
        //string lastfamily = null;
        //string lastguid = null;
        //string dlastfamily = null;
        //string guidid = strs[i][0].ToString();
        //int num = Convert.ToInt32(guidid);
        //if (guidid == "1") {
        //	family += "" + "\n";
        //}
        ////else if (guidid != lastguid) {
        ////	dlastfamily = "," + GetFamilyValue(val, num, i, null);
        ////	family += dlastfamily + "\n";
        ////}
        //else if (guidid == lastguid) {
        //	family += lastfamily + "\n";
        //}
        //else if (Convert.ToInt32(guidid) > Convert.ToInt32(lastguid)) {
        //	dlastfamily = "," + GetFamilyValue(val, num, i, null);
        //	family += dlastfamily + "\n";
        //}
        //else if (Convert.ToInt32(guidid) < Convert.ToInt32(lastguid)) {
        //	dlastfamily = "," + GetFamilyValue(val, num, i, null);
        //	family += dlastfamily + "\n";
        //}
        //lastguid = guidid;
        //lastfamily = dlastfamily;
        //txt_content.Text = content_strs + "\n" + family;
        //字符串翻转
        public string getnewstring(string strs)
        {
            string newstring = null;
            if (!string.IsNullOrEmpty(strs))
            {
                char[] array = strs.ToArray();
                string cval = null;
                for (int i = array.Length - 1; i >= 0; i--)
                {
                    cval = array[i].ToString();
                    newstring += cval;
                }
            }
            return newstring;
        }
        //递归生成Family字段
        public string GetFamilyValue(int val, int num, int family, string strings)
        {
            if (val < num - 1)
            {
                strings += family + ",";
                val++;
                family = family - 1;
                return GetFamilyValue(val, num, family, strings);
            }
            else
            {
                return strings;
            }
        }



        private void rchtxt_Content_SelectionChanged(object sender, EventArgs e)
        {

        }
        private void txt_content_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.Control && e.KeyCode == Keys.D1)//按下ctrl+1
                {
                    string selectcont = txt_content.SelectedText;
                    string str = GetNewStringUp(selectcont);
                    txt_content.SelectedText="";
                    txt_content.SelectedText = str;
                }
                if (e.Control && e.KeyCode == Keys.D2)//按下ctrl+2
                {
                    string selectcont = txt_content.SelectedText;
                    string str = GetNewStringDown(selectcont);
                    txt_content.SelectedText = "";
                    txt_content.SelectedText = str;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        //文本框
        public event PropertyChangedEventHandler PropertyChanged;
        private string bindingValue;
        public string BindingValue {
            get { return bindingValue; }
            set {
                bindingValue = value;
                if (PropertyChanged != null)
                    PropertyChanged(this, new PropertyChangedEventArgs("BindingValue"));
            }
        }
        private void btn_getchinese_Click(object sender, EventArgs e)
        {
            string str = getchinesestring().ToString();
            txt_content.Text = str;
        }
        //增加栏目等级
        public string GetNewStringUp(string newstring)
        {
            string[] newcontent = null;
            string strs = null;
            if (!string.IsNullOrEmpty(newstring))
            {
                string relcontent = newstring;
                newcontent = relcontent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (newcontent != null)
                {
                    string newstr = null;
                    string first = null;
                    Regex r2 = new Regex("[0-9]");
                    for (int i = 0; i < newcontent.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(newcontent[i].ToString()))
                        {
                            first = newcontent[i][0].ToString();
                            if (r2.Match(first).Success)
                            {
                                newcontent[i] = newcontent[i].Remove(0, 1);
                            }
                            newstr = newcontent[i].Replace(newcontent[i], "-" + newcontent[i]);
                            newstr = GetStringCount(newstr, '-') + newstr + System.Environment.NewLine;
                        }
                        else
                        {
                            newstr = "";
                        }
                        strs += newstr;
                    }
                    strs = strs.Remove(strs.LastIndexOf("\r\n"));
                }
            }
            return strs;
        }
        //减小栏目等级
        public string GetNewStringDown(string newstring)
        {
            string[] newcontent = null;
            string strs = null;
            if (!string.IsNullOrEmpty(newstring))
            {
                string relcontent = newstring;
                newcontent = relcontent.Split(new string[] { "\n" }, StringSplitOptions.RemoveEmptyEntries);
                if (newcontent != null)
                {
                    string newstr = null;
                    string first = null;
                    Regex r2 = new Regex("[0-9]");
                    for (int i = 0; i < newcontent.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(newcontent[i].ToString()))
                        {
                            first = newcontent[i][0].ToString();
                            if (r2.Match(first).Success)
                            {
                                newcontent[i] = newcontent[i].Remove(0, 1);
                                if (newcontent[i].Split('-').Length > 1)
                                {
                                    newstr = newcontent[i].Remove(newcontent[i].IndexOf('-'), 1);
                                    newstr = GetStringCount(newstr, '-') + newstr + System.Environment.NewLine;
                                }
                            }
                            else
                            {
                                newstr = newcontent[i] + System.Environment.NewLine;
                            }
                        }
                        else
                        {
                            newstr = "";
                        }
                        strs += newstr;
                    }
                    strs = strs.Remove(strs.LastIndexOf("\r\n"));
                }
            }
            return strs;
        }
        //获取字符串中字符的个数
        public string GetStringCount(string strs, char check)
        {
            string count = null;
            if (!string.IsNullOrEmpty(strs))
            {
                if (strs.Split(check).Length > 1)
                {
                    count = (strs.Split(check).Length - 1).ToString();
                }
                else
                {
                    return count;
                }
            }
            return count;
        }
        //筛选中文字符
        public string getchinesestring()
        {
            Regex r = new Regex("([\u4e00-\u9fa5])");
            Regex r3 = new Regex("[A-Z]");
            string newstring = null;
            string str = txt_content.Text.Trim();
            if (!string.IsNullOrEmpty(str))
            {
                for (int i = 0; i < str.Length; i++)
                {
                    if (r.Match(str[i].ToString()).Success)
                        newstring += str[i].ToString();
                    else if (r3.Match(str[i].ToString()).Success)
                        newstring += str[i].ToString();
                    else
                        newstring += "  ";
                }
            }
            else
            {
                return " ";
            }
            newstring = Regex.Replace(newstring, "\\s{2,}", System.Environment.NewLine);
            //newstring = newstring.Substring(0, newstring.LastIndexOf("\n"));
            newstring = newstring.TrimStart('\r').TrimStart('\n').TrimEnd('\r').TrimEnd('\n');
            return newstring;
        }
    }
}
