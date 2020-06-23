using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Tools
{
    public partial class CompileWebsite : Form
    {
        public CompileWebsite()
        {
            InitializeComponent();
        }
        protected int count = 0;
        protected int dcount = 0;
        private void button2_Click(object sender, EventArgs e)
        {
            string path = textBox1.Text;
            GetAllfileanddirectory(path);
            DeleteDirectoryinfo(path);
            CheckDirectoryIsEmptyorNot(path);
            MessageBox.Show("共删除" + count + "个文件," + dcount + "个文件夹");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog openfolder = new FolderBrowserDialog();
            openfolder.ShowDialog();
            textBox1.Text = openfolder.SelectedPath;

        }

        //处理根目录的文件和文件夹
        protected void GetAllfileanddirectory(string path)
        {
            (string[] filenames, string filename, string extension, string name)
                = (Directory.GetFiles(path), null, null, null);
            for (int i = 0; i < filenames.Length; i++)
            {
                filename = filenames[i].ToString();
                extension = Path.GetExtension(filename);
                name = filename.Substring(filename.LastIndexOf("\\") + 1).ToLower();
                if (extension == ".ashx" || extension == ".aspx" || extension == ".cs")
                {
                    if (name.Split('.')[0] != "index")
                    {
                        File.Delete(filename);
                        count++;
                    }
                }
            }
        }
        private static Tuple<string, int, DataTable> getAllFiles()
        {
            return new Tuple<string, int, DataTable>("", 0, new DataTable());
        }

        //处理子文件夹和子目录
        protected void DeleteDirectoryinfo(string path)
        {
            (DirectoryInfo dir, FileInfo[] files, string ext, string path2, string dirname, string filename)
                = (new DirectoryInfo(path), null, null, null, null, null);

            foreach (DirectoryInfo dirInfo in dir.GetDirectories())
            {
                dirname = dirInfo.ToString();
                if (dirname.ToLower() != "bin")
                {
                    files = dirInfo.GetFiles();
                    if (files.Length > 0 && files != null)
                    {
                        foreach (FileInfo file in files)
                        {
                            ext = file.Extension.ToLower();//扩展名
                            filename = file.Name;//文件名
                            path2 = file.DirectoryName;//
                            if (ext == ".ashx" || ext == ".aspx" || ext == ".cs")
                            {
                                File.Delete(path2 + "\\" + filename);
                                count++;
                            }
                        }
                        DeleteDirectoryinfo(path2);
                    }
                    else
                    {
                        DeleteDirectoryinfo(path + "\\" + dirname);
                    }
                }
            }
        }

        //判断文件夹是否为空
        protected void CheckDirectoryIsEmptyorNot(string path)
        {
            DirectoryInfo dir = new DirectoryInfo(path);
            string path2 = null;
            foreach (DirectoryInfo dirInfo in dir.GetDirectories())
            {
                path2 = path + "\\" + dirInfo.ToString();
                if (dirInfo.GetFiles().Length > 0 || dirInfo.GetDirectories().Length > 0)
                {
                    CheckDirectoryIsEmptyorNot(path2);
                }
                else
                {
                    Directory.Delete(path2);
                    dcount++;
                }
            }
        }
    }
}
