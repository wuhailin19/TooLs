using System;
using System.Collections;
using System.IO;
using System.Windows.Forms;
using System.Xml;

namespace Tools
{

	public class ReadXMLdoc {
		public string fileroot = Application.StartupPath + @"\FileInfor.xml";
		//验证XML文档是否存在
		public bool xmlisExists() {
			bool result = false;
			if (!File.Exists(fileroot)) {
				result = false;
			}
			else {
				result = true;
			}
			return result;
		}
		//写入xml数据
		public bool LoadorCreatexml() {
			bool result = false;
			if (!File.Exists(fileroot)) {
				FileStream fs = File.Create(fileroot);
				fs.Dispose();
				fs.Close();
				CreateConfigFile();
				LoadorCreatexml();
			}
			else {
				result = true;
			}
			return result;
		}
		//创建xml文档并且向XML文档写入数据
		protected virtual void CreateConfigFile() {
			Hashtable hash = new Hashtable();
			hash.Add(0, "Program");
			hash.Add(1, "InvincibleCMS");
			hash.Add(2, "SqlUrl");
			hash.Add(3, "DataBase");
			hash.Add(4, "LoginSystem");
            hash.Add(5, "Localhost");
            try {
				XmlDocument doc = new XmlDocument();
				XmlDeclaration dec = doc.CreateXmlDeclaration("1.0", "utf-8", null);
				doc.AppendChild(dec);
				XmlElement root = doc.CreateElement("FirstConfig");

				doc.AppendChild(root);
				for (int i = 0; i < hash.Count; i++) {
					XmlElement item = doc.CreateElement("SecondConfig");
					item.SetAttribute("category", hash[i].ToString());
					XmlElement item_childf = doc.CreateElement("Title");
					item_childf.InnerText = hash[i].ToString();
					item.AppendChild(item_childf);
					XmlElement item_childs = doc.CreateElement("DataUrl");
                    if (i == 5)
                    {
                        item_childs.InnerText = "18000";
                    }
                    else
                    {
                        item_childs.InnerText = "地址" + i;
                    }
                    item.AppendChild(item_childs);
					root.AppendChild(item);
				}
				doc.Save(fileroot);
			}
			catch (Exception ex) {
				throw ex;
			}
		}
		//修改对应内容
		public bool updatexmldoc(string title, string newpath) {
			bool result = false;
			XmlDocument xmlDoc = new XmlDocument();
			xmlDoc.Load(fileroot);
			XmlNodeList nodeList = xmlDoc.SelectSingleNode("FirstConfig").ChildNodes;//获取FirstConfig节点的所有子节点
			foreach (XmlNode xn in nodeList)//遍历所有名字为FirstConfig的子节点
            {
				XmlElement xe = (XmlElement)xn;//将子节点类型转换为XmlElement类型
				if (xe.GetAttribute("category") == title)//如果category属性值为title
                {
					//xe.SetAttribute("genre", "update李赞红");//则修改该属性为“update李赞红”

					XmlNodeList nls = xe.ChildNodes;//继续获取xe(xn)子节点的所有子节点
					foreach (XmlNode xn1 in nls)//遍历
                    {
						XmlElement xe2 = (XmlElement)xn1;//转换类型
						if (xe2.Name == "DataUrl")//如果找到
                        {
							xe2.InnerText = newpath;//则修改
							result = true;
							break;//找到退出来
						}
					}
					break;
				}
			}
			xmlDoc.Save(fileroot);//保存。
			return result;
		}


		//读取XML文档
		public string readtext(string title) {
			if (LoadorCreatexml()) {
				string strs = null;
				XmlDocument xmlDoc = new XmlDocument();
				xmlDoc.Load(fileroot);
				XmlNode xn = xmlDoc.SelectSingleNode("FirstConfig");

				XmlNodeList xnl = xn.ChildNodes;
				foreach (XmlNode xnf in xnl) {
					XmlElement xe = (XmlElement)xnf;
					if (xe.GetAttribute("category") == title)//如果category属性值为title
					{
						//Console.WriteLine(xe.GetAttribute("DataBase"));//显示属性值
						XmlNodeList xnf1 = xe.ChildNodes;
						foreach (XmlNode xn2 in xnf1) {
							if (xn2.Name == "DataUrl") {
								strs = xn2.InnerText;
								break;
							}
						}
						break;
					}
				}
				return strs;
			}
			return readtext(title);
		}
	}
}
