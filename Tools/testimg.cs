using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;


namespace Tools {
	public partial class testimg : Form {
		public testimg() {
			InitializeComponent();
			comboBox1.SelectedIndex = 0;
		}

		string path = string.Empty;
		string newpath = string.Empty;
		string picFix = string.Empty;

		/// <summary>   
		/// 生成缩略图   
		/// </summary>   
		/// <param   name= "originalImagePath ">源图路径（物理路径） </param>   
		/// <param   name= "thumbnailPath "> 缩略图路径（物理路径） </param>   
		/// <param   name= "width "> 缩略图宽度 </param>   
		/// <param   name= "height "> 缩略图高度 </param>   
		/// <param   name= "mode "> 生成缩略图的方式 </param>           
		public static void MakeThumbnail(string originalImagePath, string thumbnailPath, int width, int height, int mode) {
			System.Drawing.Image originalImage = System.Drawing.Image.FromFile(originalImagePath);

			int towidth = width;
			int toheight = height;

			int x = 0;
			int y = 0;
			int ow = originalImage.Width;
			int oh = originalImage.Height;

			switch (mode) {
				case 0://指定高宽缩放（可能变形）                                   
					break;
				case 1://指定宽，高按比例                                           
					toheight = originalImage.Height * width / originalImage.Width;
					break;
				case 2://指定高，宽按比例   
					towidth = originalImage.Width * height / originalImage.Height;
					break;
				case 3://指定高宽裁减（不变形）                                   
					if ((double)originalImage.Width / (double)originalImage.Height > (double)towidth / (double)toheight) {
						oh = originalImage.Height;
						ow = originalImage.Height * towidth / toheight;
						y = 0;
						x = (originalImage.Width - ow) / 2;
					}
					else {
						ow = originalImage.Width;
						oh = originalImage.Width * height / towidth;
						x = 0;
						y = (originalImage.Height - oh) / 2;
					}
					break;
				default:
					break;
			}
			//新建一个bmp图片   
			System.Drawing.Image bitmap = new System.Drawing.Bitmap(towidth, toheight);

			//新建一个画板   
			System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bitmap);

			//设置高质量插值法   
			g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.High;

			//设置高质量,低速度呈现平滑程度   
			g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;

			//清空画布并以透明背景色填充   
			g.Clear(System.Drawing.Color.Transparent);

			//在指定位置并且按指定大小绘制原图片的指定部分   
			g.DrawImage(originalImage, new System.Drawing.Rectangle(0, 0, towidth, toheight),
					new System.Drawing.Rectangle(x, y, ow, oh),
					System.Drawing.GraphicsUnit.Pixel);
			try {

				//以jpg格式保存缩略图   
				bitmap.Save(thumbnailPath, System.Drawing.Imaging.ImageFormat.Jpeg);
			}
			catch (System.Exception e) {
				throw e;
			}
			finally {
				originalImage.Dispose();
				bitmap.Dispose();
				g.Dispose();
			}
		}

		/// <summary>  
		/// 检测图片类型  
		/// </summary>  
		/// <param name="_fileExt"></param>  
		/// <returns>正确返回True</returns>  
		private bool CheckFileExt(string _fileExt) {
			string[] allowExt = new string[] { ".gif", ".jpg", ".png", ".bmp" };
			for (int i = 0; i < allowExt.Length; i++) {
				if (allowExt[i] == _fileExt) { return true; }
			}
			return false;
		}
		//保存路径
		private void button2_Click(object sender, EventArgs e) {
			FolderBrowserDialog openfolder = new FolderBrowserDialog();
			openfolder.ShowDialog();
			newpath = openfolder.SelectedPath;
			textBox2.Text = newpath;
		}
		//原图路径
		private void button1_Click(object sender, EventArgs e) {
			FolderBrowserDialog openfolder = new FolderBrowserDialog();
			openfolder.ShowDialog();
			path = openfolder.SelectedPath;
			textBox1.Text = path;
		}

		private void button3_Click(object sender, EventArgs e) {
			Getfilrimages();
		}
		public void Getfilrimages() {
			int picwidth = 0;//宽度
			int picheight = 0;//高度
			int count = 0;
			if (textBox4.Text != string.Empty || textBox3.Text != string.Empty) {
				try {
					picwidth = Convert.ToInt32(textBox4.Text.ToString());
					picheight = Convert.ToInt32(textBox3.Text.ToString());
					picFix = "_" + textBox4.Text + "-" + textBox3.Text;
					if (picFix.Trim() == "") {
						MessageBox.Show("处理的图片没有前缀名!", "提示");
						return;
					}
					if (path == string.Empty || path.Trim() == "" || newpath == string.Empty || newpath.Trim() == "") {
						MessageBox.Show("请选择路径!", "提示");
						return;
					}
				}
				catch {
					MessageBox.Show("宽度或者高宽填写有误", "提示");
					return;
				}
				#region 处理选择的文件夹数据
				string[] filenames = Directory.GetFiles(path);//得到完整路径文件名
				string filename = string.Empty;
				for (int i = 0; i < filenames.Length; i++) {
					filename = filenames[i].ToString();
					filename = filename.Substring(filename.LastIndexOf("\\") + 1).ToLower();//得到文件名
					if (filename.StartsWith(picFix)) { continue; }
					if (filename.EndsWith("gif") || filename.EndsWith("jpg") || filename.EndsWith("bmp") || filename.EndsWith("png")) {
						MakeThumbnail(path + "\\" + filename, newpath + "\\" + filename.Split('.')[0] + picFix + "." + filename.Split('.')[1], picwidth, picheight, comboBox1.SelectedIndex);
						count++;
					}
				}
				#endregion
				#region 处理子文件夹的数据
				DirectoryInfo dir = new DirectoryInfo(path);
				FileInfo[] files = null;
				string ext = null;
				string path2 = null;
				string dirname = null;
				foreach (DirectoryInfo dirInfo in dir.GetDirectories()) {
					dirname = dirInfo.ToString();
					if (!Directory.Exists(newpath + "\\" + dirname)) {
						Directory.CreateDirectory(newpath + "\\" + dirname);
					}
					files = dirInfo.GetFiles();
					foreach (FileInfo file in files) {
						ext = file.Extension.ToLower();//扩展名
						filename = file.Name;//文件名
						path2 = file.DirectoryName;//

						if (filename.StartsWith(picFix)) { continue; }
						if (ext == ".gif" || ext == ".jpg" || ext == ".bmp" || ext == ".png") {
							MakeThumbnail(path2 + "\\" + filename, newpath + "\\" + dirname + "\\" + filename.Split('.')[0] + picFix + "." + filename.Split('.')[1], picwidth, picheight, comboBox1.SelectedIndex);
							count++;
						} if (count > 200 && count % 200 == 0) {
							Thread.CurrentThread.Join(3000);
						}
					}
				}
				#endregion
				MessageBox.Show("共处理了 " + count + " 个图片", "提示");
			}
			else {
				MessageBox.Show("宽度或者高宽填写有误", "提示");
			}
		}
	}
}
