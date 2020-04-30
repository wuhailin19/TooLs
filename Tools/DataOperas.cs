using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Collections;

namespace Tools {
	public partial class DataOperas : Form {
		public DataOperas() {
			InitializeComponent();

		}

		/// <summary>
		/// 执行.sql文件
		/// </summary>
		private void execfile(string database, string sqlroot) {
			try {
				System.Diagnostics.Process sqlProcess = new System.Diagnostics.Process();
				sqlProcess.StartInfo.FileName = "osql.exe ";
				string processsql = string.Format(" -S {0} -E -d {1} -i {2}", ".", database, @sqlroot);
				sqlProcess.StartInfo.Arguments = processsql;
				sqlProcess.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
				sqlProcess.Start();
				sqlProcess.WaitForExit();
				sqlProcess.Close();
			}
			catch (Exception ex) {
				throw ex;
			}
		}

		private void button1_Click_1(object sender, EventArgs e) {
			OpenFileDialog fileDialog = new OpenFileDialog();
			fileDialog.Multiselect = false;
			fileDialog.Title = "请选择文件";
			fileDialog.Filter = "sql文件(*.sql)|*.sql";
			if (fileDialog.ShowDialog() == DialogResult.OK) {
				string names = fileDialog.FileName;
				StreamReader sr = File.OpenText(names);
				while (sr.EndOfStream != true) {
					textBox1.Text = sr.ReadToEnd().ToString();
				}
				label4.Text = names;
				sr.Dispose();
			}
		}

		private void button2_Click(object sender, EventArgs e) {
			string database = txt_database.Text.Trim();
			string root = @txt_saveurl.Text.Trim();
			string contents = null;
			contents += DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss：");
			contents += "创建数据库" + database + ",";
			if (root != string.Empty) {
				if (database != "") {
					if (!CreateDataBase.boolDataExists(database)) {
						if (CreateDataBase.CreateDataBaseDB(database, root)) {
							MessageBox.Show("创建成功！");
							BindDropDownList();
							contents += "创建成功！";
						}
						else {
							MessageBox.Show("发生错误！");
							contents += "发生错误！";
						}
					}
					else {
						MessageBox.Show("已有同名数据库！");
						contents += "已有同名数据库！";
					}
					ouputtext(contents);
				}
				else {
					this.txt_database.Focus();
				}
				readtextfrompath();
			}
			else {
				MessageBox.Show("尚未选择路径！");
			}
		}

		private void button4_Click(object sender, EventArgs e) {
			FolderBrowserDialog openfolder = new FolderBrowserDialog();
			openfolder.ShowDialog();
			txt_saveurl.Text = openfolder.SelectedPath;
		}
		public void ouputtext(string content) {
			string filename = DateTime.Now.ToString("yyyy_MM_dd");

			//if (!File.Exists(System.Web.HttpContext.Current.Server.MapPath("~/DateInfo/" + filename + ".txt"))) {
			//	File.Create(System.Web.HttpContext.Current.Server.MapPath("~/DateInfo/" + filename + ".txt"));
			//}
			if (!Directory.Exists(Application.StartupPath + @"\DateInfo")) {
				Directory.CreateDirectory(Application.StartupPath + @"\DateInfo");
				ouputtext(content);
			}
			else if (!File.Exists(Application.StartupPath + @"\DateInfo\" + filename + ".txt")) {
				FileStream fs = File.Create(Application.StartupPath + @"\DateInfo\" + filename + ".txt");
				fs.Dispose();
				ouputtext(content);
			}
			else {
				if (File.Exists(Application.StartupPath + @"\DateInfo\" + filename + ".txt")) {
					StreamWriter sw = File.AppendText(Application.StartupPath + @"\DateInfo\" + filename + ".txt");
					sw.WriteLine(content);
					sw.Flush();//清理缓冲区
					sw.Close();//关闭文件
				}
			}
		}

		private void button3_Click(object sender, EventArgs e) {
			execfile(comboBox1.Text, label4.Text);
			string contents = null;
			contents += DateTime.Now.ToString("yyyy.MM.dd-HH:mm:ss：");
			contents += "对数据库" + comboBox1.Text + ",执行了" + label4.Text + "文件";
			if (CreateDataBase.boolDataIsNullorNot(comboBox1.Text)) {
				MessageBox.Show("执行成功!");
				contents += "执行成功。";
			}
			else {
				MessageBox.Show("执行失败!");
				contents += "执行失败。";
			}
			ouputtext(contents);

			readtextfrompath();
		}

		private void DataOperas_Load(object sender, EventArgs e) {
			BindDropDownList();
			readtextfrompath();
		}

		public void BindDropDownList() {
			DataTable dt = null;
			dt = DBHelper.GetDataSet("select name,database_id from sys.databases");
			if (dt != null && dt.Rows.Count > 0) {
				comboBox1.DataSource = dt;
				comboBox1.DisplayMember = "name";
				comboBox1.ValueMember = "database_id";
				comboBox1.Text = dt.Rows[0]["name"].ToString();
			}
		}
		public void readtextfrompath() {
			string path = Application.StartupPath + @"\DateInfo\";
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
				readtextfrompath();
			}
			else {
				string[] filenames = Directory.GetFiles(path);//得到完整路径文件名
				string filename = string.Empty;
				if (filenames.Length > 0) {
					for (int i = 0; i < filenames.Length; i++) {
						StreamReader sr = File.OpenText(filenames[i]);
						while (sr.EndOfStream != true) {
							filename += sr.ReadToEnd().ToString();
						}
						sr.Dispose();
						sr.Close();
					}
					textBox2.Text = filename;
				}
			}
		}
		/// <summary>
		/// 执行.sql文件
		/// </summary>
		private bool execfiles() {
			bool result = false;
			string username = null; string linkurl = null; string password = null;
			linkurl = "211.149.170.53";
			username = "xihandb";
			password = "N7c3A8W3";
			//linkurl = txt_Linkurl.Text.Trim();
			//username = txt_username.Text.Trim();
			//password = txt_password.Text.Trim();
			try {
				System.Diagnostics.Process sqlProcess = new System.Diagnostics.Process();
				sqlProcess.StartInfo.FileName = "osql.exe ";
				string processsql = string.Format(" -S {0} -U {1} -P {2} ", linkurl, username, password);
				sqlProcess.StartInfo.Arguments = processsql;
				sqlProcess.StartInfo.UseShellExecute = false;
				sqlProcess.StartInfo.CreateNoWindow = false;//是否显示DOS窗口，true代表隐藏;
				sqlProcess.StartInfo.RedirectStandardInput = true;
				sqlProcess.StartInfo.RedirectStandardOutput = true;
				sqlProcess.StartInfo.RedirectStandardError = true;
				sqlProcess.Start();
				StreamWriter sIn = sqlProcess.StandardInput;//标准输入流 
				sIn.AutoFlush = true;
				StreamReader sOut = sqlProcess.StandardOutput;//标准输入流 
				//StreamReader sErr = sqlProcess.StandardError;//标准错误流 
				sIn.Write("select name,database_id from sys.databases  where name like '%" + username + "%'" + System.Environment.NewLine);
				sIn.Write("go" + System.Environment.NewLine);
				//string s = sOut.ReadToEnd();//读取执行DOS命令后输出信息 
				//string er = sErr.ReadToEnd();//读取执行DOS命令后错误信息 
				//textBox1.Text = sOut.ReadToEnd();
				if (sOut.ReadToEnd() != null) {
					MessageBox.Show("连接成功");
					result = true;
				}
				sOut.Close();
				if (sqlProcess.HasExited == false) {
					sqlProcess.Kill();
				}
				sqlProcess.Close();
				sIn.Dispose();
				sIn.Flush();
				sIn.Close();
				//sErr.Close();
			}
			catch {
				result = false;
			}
			return result;
		}
		private void btn_testlink_Click(object sender, EventArgs e) {
			if (execfiles()) {
				MessageBox.Show("连接成功");
			}
			else {
				MessageBox.Show("连接失败");
			}
		}

		private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
		{

		}
	}
}
