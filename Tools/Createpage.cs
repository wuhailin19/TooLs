using System;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;
using NPinyin;
using System.Xml;
using System.Threading.Tasks;
using System.Threading;

namespace Tools
{
    public partial class Createpage : Form
    {
        public Createpage()
        {
            InitializeComponent();
            label6.Text = "注意事项：" +
                "\r\n<head>中内容标记为<tl:Header></tl:Header>" +
                "\r\n<body>中头部内容标记为<tl:Head></tl:Head>" +
                "\r\n<body>中脚部内容标记为<tl:Foot></tl:Foot>";
        }

        private void Createpage_Load(object sender, EventArgs e)
        {
            readtextfrompath();
        }
        public void createnewwebsite(string path, string htmlpath)
        {
            int count = 0;
            if (!Directory.Exists(path))
            {
                return;
            }
            else
            {
                string[] filenames = Directory.GetFiles(htmlpath);//得到完整路径文件名
                string extension = string.Empty;//扩展名
                string filename = string.Empty;//完整文件名
                string name = string.Empty;//文件名
                string filenameandpath = string.Empty;
                for (int i = 0; i < filenames.Length; i++)
                {
                    filename = filenames[i].Substring(filenames[i].LastIndexOf("\\") + 1);
                    extension = Path.GetExtension(filename);
                    name = filename.Split('.')[0];
                    string Js = null;
                    string Content = null;
                    if (chb_checkpagec.Checked)
                    {
                        if (extension == ".html" || extension == ".htm")
                        {
                            //获取Js
                            Js = JSFormHtml(htmlpath + "\\" + filename);
                            //获取Content
                            Content = GetContentFormHtml(htmlpath + "\\" + filename);
                        }
                    }
                    name = name.Replace("-", "_");
                    if (!string.IsNullOrEmpty(name))
                    {
                        Regex reg = new Regex("^[0-9]*$");
                        if (reg.Match(name[0].ToString()).Success)
                        {
                            name = "_" + name;
                        }
                        if (name.ToLower() == "new")
                        {
                            name = "news";
                        }
                        if (name.ToLower() == "base")
                        {
                            name = "bases";
                        }
                    }
                    if (extension == ".html" || extension == ".htm")
                    {
                        filenameandpath = path + "\\" + name + ".aspx";
                        string pageheader = "<%@ Page Title=\"\" Language=\"C#\" MasterPageFile=\"~/comm.master\" AutoEventWireup=\"true\" CodeFile=\"" + name + ".aspx.cs\" Inherits=\"" + name + "\" %>\r\n\r\n";
                        string pagecontent = "<asp:Content ID=\"Content1\" ContentPlaceHolderID=\"ContentID\" Runat=\"Server\">" +
                            "\r\n" +
                            Content +
                            "\r\n" +
                            "</asp:Content>\r\n" +
                            "<asp:Content ID=\"Content2\" ContentPlaceHolderID=\"FooterID\" Runat=\"Server\">" +
                            "\r\n" +
                            Js +
                            "\r\n</asp:Content>\r\n";
                        if (name.Contains("ajax"))
                        {
                            pageheader = "<%@ Page Title=\"\" Language=\"C#\" AutoEventWireup=\"true\" CodeFile=\"" + name + ".aspx.cs\" Inherits=\"" + name + "\" %>\r\n";
                            pagecontent = Content;
                        }
                        FileStreamCreatefile(
                            filenameandpath,
                            pageheader + pagecontent);

                        filenameandpath = path + "\\" + name.Replace("-", "_") + ".aspx.cs";
                        FileStreamCreatefile(filenameandpath,
                        "using System;\n" +
                        "using System.Collections.Generic;\n" +
                        "using System.Web;\n" +
                        "using System.Web.UI;\n" +
                        "using System.Web.UI.WebControls;\n" +
                        "using System.Text;\n" +
                        "using System.Data;\n\n" +
                        "public partial class " + name + " : PageDataBind\n" +
                        "{\n" +
                        "    public string firstPbanner = string.Empty, firstPageName = string.Empty, firstEnglishName = string.Empty, nowPbanner = string.Empty, nowPageName = string.Empty, nowPageEnglishName = string.Empty;\n" +
                        "    public StringBuilder Sbr_Page_Navbar = new StringBuilder();\n" +
                        "    public DataRow PageDr = null;\n" +
                        "\n" +
                        "    protected void Page_LoadComplete(object sender, EventArgs e)\n" +
                        "    {\n" +
                        "        firstPbanner = ((comm)Page.Master).firstPbanner;\n" +
                        "        firstPageName = ((comm)Page.Master).firstPageName;\n" +
                        "        firstEnglishName = ((comm)Page.Master).firstEnglishName;\n" +
                        "        nowPbanner = ((comm)Page.Master).nowPbanner;\n" +
                        "        nowPageName = ((comm)Page.Master).nowPageName;\n" +
                        "        nowPageEnglishName = ((comm)Page.Master).nowPageEnglishName;\n" +
                        "        Sbr_Page_Navbar = ((comm)Page.Master).Sbr_Page_Navbar;\n" +
                        "     }\n" +
                        "protected void Page_Load(object sender, EventArgs e)\n" +
                        "{\n" +
                        "   try\n" +
                        "        {\n" +
                        "            int t = WebUtility.getparam(\"t\");\n" +
                        "            if (t != 0)\n" +
                        "            {\n" +
                        "                setkeyword(t);\n" +
                        "                DataTable dt = null;\n" +
                        "            }\n" +
                        "        }\n" +
                        "        catch\n" +
                        "        {" +
                        "\n" +
                        "        }\n" +
                        "}\n" +
                        "}\n");

                        //filenameandpath = path + "\\" + name.Replace("-", "_") + ".aspx.designer.cs";
                        //FileStreamCreatefile(filenameandpath,
                        //"//------------------------------------------------------------------------------\r\n" +
                        //"// <auto-generated>\r\n" +
                        //"//     此代码由工具生成。\r\n" +
                        //"//\r\n" +
                        //"//     对此文件的更改可能会导致不正确的行为，并且如果\r\n" +
                        //"//     重新生成代码，这些更改将会丢失。\r\n" +
                        //"// </auto-generated>\r\n\r\n" +
                        //"//------------------------------------------------------------------------------\r\n" +
                        //"namespace WebApplication1\r\n" +
                        //"{\r\n" +
                        //"       public partial class WebForm1\r\n" +
                        //"        {\r\n" +
                        //"        }\r\n" +
                        //"}\r\n");
                    }
                    count++;
                }
            }
            #region 母版页

            string masterpath = path + "\\comm.master";
            FileStreamCreatefile(masterpath, "<%@ Master Language=\"C#\" AutoEventWireup=\"true\" CodeFile=\"comm.master.cs\" Inherits=\"comm\" %>\r\n" +
                "<!DOCTYPE html>\r\n" +
                "<html xmlns=\"http://www.w3.org/1999/xhtml\">\r\n" +
                "<head runat=\"server\">\r\n" +
                Header +
                "\r\n" +
                "</head>\r\n" +
                "<body>\r\n" +
                Head +
                "\r\n" +
                "<asp:ContentPlaceHolder ID=\"ContentID\" runat=\"server\">\r\n</asp:ContentPlaceHolder>\r\n" +
                Footer + "\r\n" +
                "<asp:ContentPlaceHolder ID=\"FooterID\" runat=\"server\">\r\n</asp:ContentPlaceHolder>\r\n" +
                "\r\n" +
                "<div style=\"display: none\"><%=tongji %></div>" +
                "</body>\r\n" +
                "</html>\r\n");
            #endregion
            copyfirstfiles(htmlpath, path);
        }

        //修改Xml对应内容
        public void updatexmldoc(string fileroot, string connectionstr)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(fileroot);
            XmlNode node = xmlDoc.SelectSingleNode("//configuration/connectionStrings/add");//获取add节点
            XmlAttributeCollection xc = node.Attributes;
            foreach (XmlAttribute xa in xc)
            {
                if (xa.Name == "connectionString")
                {
                    xa.Value = connectionstr;
                    break;
                }
            }
            xmlDoc.Save(fileroot);//保存。
        }
        public void FileStreamCreatefile(string filenameandpath, string content)
        {
            FileStream fs = new FileStream(filenameandpath, FileMode.Create, FileAccess.Write);
            StreamWriter sw = new StreamWriter(fs, System.Text.UnicodeEncoding.UTF8);
            try
            {
                sw.Write(content);
            }
            catch
            {
            }
            finally
            {
                sw.Flush();
                fs.Flush();
                fs.Close();
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.ShowDialog();
            textBox3.Text = openfolder.SelectedPath;
        }
        //复制根目录文件
        public void copyfirstfiles(string oldpath, string path)
        {
            string[] filenames = Directory.GetFiles(oldpath);//得到完整路径文件名
            string filename = string.Empty;
            string extension = string.Empty;
            string name = string.Empty;
            for (int i = 0; i < filenames.Length; i++)
            {
                filename = filenames[i].ToString();
                extension = Path.GetExtension(filename);
                name = filename.Substring(filename.LastIndexOf("\\") + 1).ToLower();
                if (extension != ".html")
                {
                    File.Copy(filename, path + "\\" + name, true);
                }
            }
            copytonewdirectoy(oldpath, path);
            string htmlpath = path + "\\htmlpage(outside)";
            copyfiletonewdirectory(oldpath, htmlpath);
        }

        //通用复制根目录文件
        public void relcopyfirstfiles(string oldpath, string path)
        {
            string[] filenames = Directory.GetFiles(oldpath);//得到完整路径文件名
            string filename = string.Empty;
            string extension = string.Empty;
            string name = string.Empty;
            for (int i = 0; i < filenames.Length; i++)
            {
                filename = filenames[i].ToString();
                name = filename.Substring(filename.LastIndexOf("\\") + 1);
                File.Copy(filename, path + "\\" + name, true);
            }
            copytonewdirectoy(oldpath, path);
        }
        //遍历指定文件夹查找有无同名文件夹
        public void foreachdiectory(string path)
        {
            if (Directory.Exists(path))
            {
                DirectoryInfo dir = new DirectoryInfo(path);
                string dirname = null;
                string enname = getguiddirectoyname(txt_websitename.Text);
                string newpath = path + "\\" + enname;

                foreach (DirectoryInfo dirin in dir.GetDirectories())
                {
                    dirname = dirin.ToString();
                    if (dirname == textBox7.Text)
                    {
                        Directory.Move(path + "\\" + dirname, newpath);
                    }
                }
            }
        }
        //生成随机拼音文件夹名
        public string getguiddirectoyname(string chname)
        {
            string newenname = null;
            if (!string.IsNullOrEmpty(chname))
            {
                string newwebsite = Pinyin.GetPinyin(chname).Replace(" ", "");
                string newguid = Guid.NewGuid().ToString().Replace("-", "").Substring(0, 12);
                newenname = newwebsite + newguid;
            }
            return newenname;
        }
        //生成拼音数据库名
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
        //复制子目录的文件内容
        public void copytonewdirectoy(string oldpath, string path)
        {
            DirectoryInfo dir = new DirectoryInfo(oldpath);
            FileInfo[] files = null;
            if (dir.Exists)
            {
                string dirname = string.Empty;//文件夹名
                string path2 = null;
                foreach (DirectoryInfo dirin in dir.GetDirectories())
                {
                    dirname = path + "\\" + dirin.ToString();
                    files = dirin.GetFiles();
                    if (!Directory.Exists(dirname))
                    {
                        Directory.CreateDirectory(dirname);
                        copytonewdirectoy(oldpath, path);
                    }
                    else
                    {
                        if (files.Length > 0 && files != null)
                        {
                            foreach (FileInfo file in files)
                            {
                                path2 = file.DirectoryName;
                                file.CopyTo(dirname + "\\" + file, true);
                            }
                            copytonewdirectoy(path2, dirname);
                        }
                        else
                        {
                            copytonewdirectoy(oldpath + "\\" + dirin.ToString(), dirname);
                        }
                    }
                }
            }
        }
        //复制选定文件夹所有内容到指定位置
        public void copyfiletonewdirectory(string oldpath, string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
                copyfiletonewdirectory(oldpath, path);
            }
            else
            {
                //复制根目录文件
                string[] filenames = Directory.GetFiles(oldpath);//得到完整路径文件名
                string filename = string.Empty;
                string name = string.Empty;
                for (int i = 0; i < filenames.Length; i++)
                {
                    filename = filenames[i].ToString();
                    name = filename.Substring(filename.LastIndexOf("\\") + 1).ToLower();
                    File.Copy(filename, path + "\\" + name, true);
                }
                //复制子目录文件
                copytonewdirectoy(oldpath, path);
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            string projectpath = textBox2.Text;
            ReadXMLdoc xml = new ReadXMLdoc();
            if (string.IsNullOrEmpty(projectpath))
            {
                projectpath = "地址0";
            }
            if (xml.xmlisExists())
            {
                if (xml.updatexmldoc("Program", projectpath))
                {
                    MessageBox.Show("保存成功！");
                }
            }
        }
        public void readtextfrompath()
        {
            ReadXMLdoc xml = new ReadXMLdoc();

            textBox2.Text = xml.readtext("Program");

            textBox4.Text = xml.readtext("InvincibleCMS");

            textBox5.Text = xml.readtext("SqlUrl");

            textBox6.Text = xml.readtext("DataBase");

            textBox7.Text = xml.readtext("LoginSystem");

            textBox1.Text = xml.readtext("Localhost");
        }
        public void Preservationfile(string newpath)
        {
            string path = Application.StartupPath + @"\Config\config.txt";
            if (!File.Exists(path))
            {
                File.Create(path);
                Preservationfile(newpath);
            }
            else
            {
                FileStream fs = new FileStream(path, FileMode.Truncate, FileAccess.ReadWrite);
                fs.Close();
                StreamWriter sw = File.AppendText(path);
                sw.WriteLine(newpath);
                sw.Flush();//清理缓冲区
                sw.Close();//关闭文件
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.ShowDialog();
            textBox2.Text = openfolder.SelectedPath;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.ShowDialog();
            textBox4.Text = openfolder.SelectedPath;
        }

        //保存项目基本文件路径
        private void button8_Click(object sender, EventArgs e)
        {
            string Invincepath = textBox4.Text;
            ReadXMLdoc xml = new ReadXMLdoc();
            if (string.IsNullOrEmpty(Invincepath))
            {
                Invincepath = "地址1";
            }
            if (xml.xmlisExists())
            {
                if (xml.updatexmldoc("InvincibleCMS", Invincepath))
                {
                    MessageBox.Show("保存成功！");
                }
            }
        }

        //保存sql文件路径
        private void button4_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Multiselect = false;
            fileDialog.Title = "请选择文件";
            fileDialog.Filter = "sql文件(*.sql)|*.sql";
            if (fileDialog.ShowDialog() == DialogResult.OK)
            {
                string names = fileDialog.FileName;
                textBox5.Text = names;
            }
        }

        private void button10_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.ShowDialog();
            textBox6.Text = openfolder.SelectedPath;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            string Sqlpath = textBox5.Text;
            ReadXMLdoc xml = new ReadXMLdoc();
            if (string.IsNullOrEmpty(Sqlpath))
            {
                Sqlpath = "地址2";
            }
            if (xml.xmlisExists())
            {
                if (xml.updatexmldoc("SqlUrl", Sqlpath))
                {
                    MessageBox.Show("保存成功！");
                }
            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            string DataBasepath = textBox6.Text;
            ReadXMLdoc xml = new ReadXMLdoc();
            if (string.IsNullOrEmpty(DataBasepath))
            {
                DataBasepath = "地址3";
            }
            if (xml.xmlisExists())
            {
                if (xml.updatexmldoc("DataBase", DataBasepath))
                {
                    MessageBox.Show("保存成功！");
                }
            }
        }
        //生成项目路径
        public string getWebsitePath(string path)
        {
            string newpath = null;
            string websitename = txt_websitename.Text.Trim();
            if (websitename != string.Empty)
            {
                string pinyin = NPinyin.Pinyin.GetPinyin(websitename);
                string numf = pinyin[0].ToString().ToLower();
                newpath = path + "\\" + numf + websitename;
            }
            return newpath;
        }
        //创建项目路径
        public bool createWebsitePath(string path)
        {
            bool result = true;
            if (!Directory.Exists(path))
            {
                try
                {
                    Directory.CreateDirectory(path);
                }
                catch
                {
                    result = false;
                }
                finally
                {
                    createWebsitePath(path);
                }
            }
            else
            {
                result = true;
            }
            return result;
        }
        private string newwebpath = null;//传递窗体参数
        private void button12_Click(object sender, EventArgs e)
        {
            string websitename = getWebsitePath(textBox2.Text);
            string newpath = websitename + "\\" + (textBox4.Text.Substring(textBox4.Text.LastIndexOf("\\") + 1));
            if (!string.IsNullOrEmpty(newpath))
            {
                if (createWebsitePath(newpath))
                {
                    relcopyfirstfiles(textBox4.Text, newpath);
                    string webpath = newpath + "\\" + "web";
                    newwebpath = webpath;
                    if (Directory.Exists(webpath))
                    {
                        try
                        {
                            foreachdiectory(webpath);
                        }
                        catch
                        {
                            MessageBox.Show("操作失败");
                            return;
                        }
                        finally
                        {
                            if (chb_checkpagec.Checked)
                            {
                                //初始化静态网页参数
                                ContentFormHtml(textBox3.Text + @"\index.html");
                            }
                            createnewwebsite(webpath, textBox3.Text);

                            //修改端口号
                            //ReadXMLdoc xml = new ReadXMLdoc();
                            //int localhost = Convert.ToInt32(xml.readtext("Localhost"));
                            //localhost = localhost + 1;
                            //xml.updatexmldoc("Localhost", localhost.ToString());


                            //string strFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, newpath + @"\InvincibleCMS.sln");
                            //if (File.Exists(strFilePath))
                            //{
                            //    string strContent = File.ReadAllText(strFilePath);
                            //    strContent = Regex.Replace(strContent, "17987", localhost.ToString());
                            //    File.WriteAllText(strFilePath, strContent);
                            //}


                            string dataname = getdatabasename(txt_websitename.Text);
                            //修改数据库名
                            updatexmldoc(webpath + @"\web.config", "server=(local);DataBase=" + dataname + ";uid=sa;pwd=123456");

                            string root = null;
                            if (textBox6.Text == "地址3" || textBox6.Text == string.Empty)
                            {
                                root = websitename + "\\" + "数据库";
                                if (!Directory.Exists(root))
                                {
                                    Directory.CreateDirectory(root);
                                }
                            }
                            else
                            {
                                root = textBox6.Text;
                            }
                            if (CreateDataBase.CreateDataBaseDB(dataname, root))
                            {
                                execfile(dataname, textBox5.Text);


                                MessageBox.Show("操作成功！");
                            }
                            else
                            {
                                MessageBox.Show("操作失败！");
                            }
                        }
                    }
                }
                else
                {
                    MessageBox.Show("创建失败");
                }
            }
            else
            {
                MessageBox.Show("网站名不能为空");
            }
        }
        private void button13_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.ShowDialog();
            string path = openfolder.SelectedPath;
            textBox7.Text = path.Substring(path.LastIndexOf("\\") + 1);
            //textBox7.Text = path;
        }
        private void button14_Click(object sender, EventArgs e)
        {
            string LoginSystempath = textBox7.Text;
            ReadXMLdoc xml = new ReadXMLdoc();
            if (string.IsNullOrEmpty(LoginSystempath))
            {
                LoginSystempath = "地址4";
            }
            if (xml.xmlisExists())
            {
                if (xml.updatexmldoc("LoginSystem", LoginSystempath))
                {
                    MessageBox.Show("保存成功！");
                }
            }
        }
        /// <summary>
        /// 执行.sql文件
        /// </summary>
        private void execfile(string database, string sqlroot)
        {
            try
            {
                System.Diagnostics.Process sqlProcess = new System.Diagnostics.Process();
                sqlProcess.StartInfo.FileName = "osql.exe ";
                string processsql = string.Format(" -S {0} -E -d {1} -i {2}", ".", database, @sqlroot);
                sqlProcess.StartInfo.Arguments = processsql;
                sqlProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
                sqlProcess.Start();
                sqlProcess.WaitForExit();
                sqlProcess.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        private static string Header = null;//头部其他
        private static string Head = null;//头部
        private static string Footer = null;//底部
        private static string HeadFirstdiv = null;
        private static string FooterFirstdiv = null;
        //获取JS
        protected string JSFormHtml(string htmlpath)
        {
            string htmlcontent = ReadContentFromFile(htmlpath);
            string Js = null;
            //Js
            try
            {
                Js = htmlcontent.Substring(htmlcontent.Length / 2);
                int countf = Js.IndexOf("<script");
                Js = Js.Substring(countf);
                int counts = Js.LastIndexOf("</script>");
                Js = Js.Substring(0, counts + 9);
            }
            catch { }
            return Js;
        }
        //获取Content
        protected string GetContentFormHtml(string htmlpath)
        {
            string htmlcontent = ReadContentFromFile(htmlpath);
            htmlcontent = htmlcontent.Replace("\r\n", "");
            htmlcontent = Regex.Replace(htmlcontent, "\\s{2,}", "");
            htmlcontent = htmlcontent.Replace("\t", "");
            string Content = null;
            //Content
            try
            {
                int countf = htmlcontent.IndexOf("<body>");
                if (countf > 0)
                {
                    Content = htmlcontent.Substring(countf + 6);
                    int counts = Content.IndexOf("<script");
                    Content = Content.Substring(0, counts);
                    string newFooter = ""; string newHeader = "";
                    if (!string.IsNullOrEmpty(Footer))
                        newFooter = Footer.Replace("\r\n", "").Replace("\t", "").Replace("> <", "><").Replace(" />", "/>").Replace("><", ">\r\n<");
                    if (!string.IsNullOrEmpty(Head))
                        newHeader = Head.Replace("\r\n", "").Replace("\t", "").Replace("> <", "><").Replace(" />", "/>").Replace("><", ">\r\n<");
                    Content = Content.Replace("\t", "").Replace("> <", "><").Replace(" />", "/>");

                    //if (Content.Length > (newFooter.Length + newHeader.Length))
                    //{
                    //    if (Content.Contains(newHeader))
                    //        Content = Content.Replace(newHeader, "");
                    //    else if (Content.Contains(newFooter))
                    //        Content = Content.Replace(newFooter, "");
                    //    else
                    //    {
                    //        if (Content.Contains(FooterFirstdiv))
                    //        {
                    //            Content = Content.Replace(FooterFirstdiv, "");
                    //        }
                    //    }
                    //}
                    Content = Content.Replace("<tl:Head>", "");
                    Content = Content.Replace("</tl:Head>", "");
                    Content = Content.Replace("<tl:Foot>", "");
                    Content = Content.Replace("</tl:Foot>", "");
                }
                else
                {
                    Content = htmlcontent.Replace("\t", "").Replace("> <", "><");
                }
                Content = Content.Replace("</body>", "");
                Content = Content.Replace("><", ">\r\n<");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Content;
        }
        public static string RemoveEmptyLines(string lines)
        {
            return Regex.Replace(lines, @"^\s*$\n|\r", "", RegexOptions.Multiline).TrimEnd();
        }
        //格式化文件内容
        protected void ContentFormHtml(string htmlpath)
        {
            string htmlcontent = null;
            if (File.Exists(htmlpath))
            {
                htmlcontent = ReadContentFromFile(htmlpath);
            }
            else
            {
                MessageBox.Show("Index页面不存在");
                return;
            }
            htmlcontent = htmlcontent.Replace("\r\n", "");
            htmlcontent = Regex.Replace(htmlcontent, "\\s{2,}", "");
            htmlcontent = htmlcontent.Replace("\t", "");
            //<head>头部内容
            try
            {
                int countf = htmlcontent.IndexOf("<tl:Header>");
                if (countf > 0)
                {
                    Header = htmlcontent.Substring(countf + 11);
                    int counts = Header.IndexOf("</tl:Header>");
                    Header = RemoveEmptyLines(Header).Substring(0, counts).Replace("><", ">\r\n<");
                }
            }
            catch { }
            //content头部内容
            try
            {
                int countf = htmlcontent.IndexOf("<tl:Head>");
                if (countf > 0)
                {
                    Head = htmlcontent.Substring(countf + 9);
                    int counts = Head.IndexOf("</tl:Head>");
                    HeadFirstdiv = Head.Substring(0, Head.IndexOf(">") + 1);
                    Head = RemoveEmptyLines(Head).Substring(0, counts).Replace("><", ">\r\n<");
                }
            }
            catch { }
            //content底部内容
            try
            {
                int countf = htmlcontent.IndexOf("<tl:Foot>");
                if (countf > 0)
                {
                    Footer = htmlcontent.Substring(countf + 9);
                    int counts = Footer.IndexOf("</tl:Foot>");
                    FooterFirstdiv = Footer.Substring(0, Footer.IndexOf(">") + 1);
                    Footer = RemoveEmptyLines(Footer).Substring(0, counts).Replace("><", ">\r\n<");
                }
            }
            catch { }
        }
        //读取文件内容
        protected string ReadContentFromFile(string strFilePath)
        {
            try
            {
                string strContent = File.ReadAllText(strFilePath);
                return strContent;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string websitename = getWebsitePath(textBox2.Text);
            string newpath = websitename + "\\" + (textBox4.Text.Substring(textBox4.Text.LastIndexOf("\\") + 1));
            if (string.IsNullOrEmpty(txt_websitename.Text) || string.IsNullOrEmpty(textBox3.Text))
            {
                MessageBox.Show("网站名或路径不能为空");
                return;
            }
            button1.Enabled = false;
            label4.Text = "开始生成";

            string dataname = getdatabasename(txt_websitename.Text);
            string root = null;
            if (textBox6.Text == "地址3" || textBox6.Text == string.Empty)
                root = websitename + "\\" + "数据库";
            else
                root = textBox6.Text;

            #region 创建新的网站路径并复制网站到新地址
            //创建新的网站路径
            var createWebsite = Task<int>.Factory.StartNew(() =>
            {
                if (!string.IsNullOrEmpty(newpath))
                {
                    try
                    {
                        createWebsitePath(newpath);
                        return 1;
                    }
                    catch (Exception)
                    {
                        return 0;
                    }
                }
                else
                {
                    return -1;
                }
            });

            //复制网站到新地址
            var relcopy = createWebsite.ContinueWith(t =>
            {//F:\工作\前端页面\玖点
                try
                {
                    relcopyfirstfiles(textBox4.Text, newpath);
                    return 1;
                }
                catch (Exception)
                {
                    return 0;
                }
            });
            #endregion

            #region 生成数据库
            var createDatabase = new Task<int>(() =>
            {
                try
                {
                    Directory.CreateDirectory(root);
                    try
                    {
                        CreateDataBase.CreateDataBaseDB(dataname, root);
                        execfile(dataname, textBox5.Text);
                        return 1;
                    }
                    catch (Exception ex)
                    {
                        string s = ex.Message;
                        return 0;
                    }
                }
                catch { return 0; }
            });
            #endregion
            #region 创建动态页面
            var createHtmlContent = new Task<int>(() =>
            {
                try
                {
                    if (chb_checkpagec.Checked)
                    {
                        //初始化静态网页参数
                        ContentFormHtml(textBox3.Text + @"\index.html");
                        return 1;
                    }
                    else
                    {
                        return 0;
                    }
                }
                catch { return 0; }
            });
            //生成动态页面
            var createNewWebsites = createHtmlContent.ContinueWith(c =>
            {
                string webpath = newpath + "\\" + "web";
                try
                {
                    foreachdiectory(webpath);
                }
                catch
                {
                    return 0;
                }
                try
                {
                    createnewwebsite(webpath, textBox3.Text);
                    updatexmldoc(webpath + @"\web.config", "server=(local);DataBase=" + dataname + ";uid=sa;pwd=123456;");

                    //修改端口号
                    ReadXMLdoc xml = new ReadXMLdoc();
                    int localhost = Convert.ToInt32(xml.readtext("Localhost"));
                    localhost = localhost + 1;
                    xml.updatexmldoc("Localhost", localhost.ToString());


                    string strFilePath = Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, newpath + @"\InvincibleCMS.sln");
                    if (File.Exists(strFilePath))
                    {
                        string strContent = File.ReadAllText(strFilePath);
                        strContent = Regex.Replace(strContent, "18000", localhost.ToString());
                        File.WriteAllText(strFilePath, strContent);
                    }

                    return 1;
                }
                catch
                {
                    return 0;
                }
            });

            #endregion
            //进度条线程
            var progresstask = Task.Factory.StartNew(() =>
            {
                createWebsite.Wait();//等待网站创建完成
                if (createWebsite.Result == 1)
                {
                    Init(1, 16, "网站新路径创建成功", new Random().Next(50, 100));
                    Thread.Sleep(500);

                    Inits(16, 70, "正在复制文件", new Random().Next(50, 100), relcopy);

                    relcopy.Wait();//等待复制文件完成
                    if (relcopy.Result == 1)
                    {
                        createDatabase.Start();//开始创建数据库
                        createHtmlContent.Start();//开始生成页面

                        Inits(70, 75, "初始化静态网页参数中", new Random().Next(50, 100), createDatabase);
                        if (createDatabase.Result == 1)
                        {
                            Init(74, 76, "数据库创建成功", new Random().Next(50, 100));
                        }
                        Init(75, 77, "复制文件成功", new Random().Next(100, 200));
                        Thread.Sleep(500);

                        Inits(76, 90, "初始化静态网页参数中", new Random().Next(50, 100), createHtmlContent);

                        createHtmlContent.Wait();//初始化静态网页参数完成
                        Init(89, 91, "初始化静态网页参数完成", new Random().Next(50, 100));
                        Thread.Sleep(500);

                        Inits(91, 100, "动态网页生成中", new Random().Next(50, 100), createNewWebsites);

                        createNewWebsites.Wait();//动态网页生成完成
                        Init(99, 101, "动态网页生成完成", new Random().Next(50, 100));
                        button1.Enabled = true;

                    }
                    else
                    {
                        MessageBox.Show("发生错误");
                    }
                }
                else
                {
                    MessageBox.Show("发生错误");
                }
            });
        }
        /// <summary>
        /// 初始化进程说明
        /// </summary>
        /// <param name="start"></param>
        /// <param name="next"></param>
        /// <param name="message"></param>
        /// <param name="speed"></param>
        /// <param name="task"></param>
        public void Inits(int start, int next, string message, int speed, Task task)
        {
            for (int i = start; i < next; i++)
            {
                progressBar1.Value = i;
                label3.Text = $"{i}%";
                label4.Text = $"{message}.";
                Thread.Sleep(speed);
                label4.Text = $"{message}..";
                Thread.Sleep(speed);
                label4.Text = $"{message}...";
                Thread.Sleep(speed);
                if (task.IsCompleted)
                {
                    break;
                }
            }
        }
        public void Init(int start, int next, string message, int speed)
        {
            for (int i = start; i < next; i++)
            {
                Thread.Sleep(speed);
                progressBar1.Value = i;
                label3.Text = $"{i}%";
            }
            label4.Text = message;
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
