using System;
using System.Data;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using System.Reflection;

namespace Tools {
	public class CreateDataBase {

		/// <summary>
		/// 生成数据库
		/// </summary>
		/// <param name="database">数据库名</param>
		/// <param name="root">路径地址</param>
		/// <returns></returns>
		#region--------------生成数据库-----------------
		public static bool CreateDataBaseDB(string database, string root) {
			string filepath = null;//储存路径
			string sql = null;//sql语句,添加数据库
			//string filename = null;
			string ofilepath = null;
			bool result = false;
			if (root == string.Empty) {
				//string nroot = AppDomain.CurrentDomain.BaseDirectory;
				//if (nroot != string.Empty) {
				//	string[] roots = nroot.Split('\\');
				//	if (roots.Length > 0 && roots != null) {
				//		for (int i = 0; i < roots.Length; i++) {
				//			if (i < roots.Length - 2) {
				//				filename += roots[i] + "\\";
				//			}
				//		}
				//	}
				//}
				//ofilepath = filename + "数据库";
				result = false;
			}
			else {
				ofilepath = @root + "\\";
				if (!Directory.Exists(ofilepath)) {
					Directory.CreateDirectory(ofilepath);
					filepath = ofilepath;
				}
				else {
					filepath = ofilepath;
				}
				if (Directory.Exists(filepath)) {
					string newdatbase = null;//格式化后的新数据库名
					string dbdata = null;//数据库data名
					string dblog = null;//数据库日志名

					newdatbase = newdatabase(database);
					dbdata = filepath + "\\" + newdatbase + "_data.mdf";
					dblog = filepath + "\\" + newdatbase + "_log.ldf";

					sql = "create database " + newdatbase + " on primary(name=" + newdatbase + "_data,filename='" + dbdata + "',size=5mb,maxsize = 100mb,filegrowth = 15%) log on (name=" + newdatbase + "_log,filename='" + dblog + "',size=2mb,filegrowth = 1mb)";

					if (!boolDataExists(newdatbase)) {
						DBHelper.newExecuteCommand(sql);
						result = true;
					}
					else {
						result = false;
					}
				}
			}
			return result;
		}
		#endregion

		//#region--------------生成数据表-----------------
		//public static bool CreateDataTable(Models.Model model, string root, string database)
		//{
		//	bool result = false;

		//	if (!boolDataExists(database))
		//	{
		//		if (CreateDataBaseDB(database, root))
		//		{
		//			if (createnewTable(database, model))
		//			{
		//				result = true;
		//			}
		//			else
		//			{
		//				result = false;
		//			}
		//		}
		//	}
		//	else
		//	{
		//		if (createnewTable(database, model))
		//		{
		//			result = true;
		//		}
		//		else
		//		{
		//			result = false;
		//		}
		//	}
		//	return result;
		//}
		//#endregion

		//#region--------------生成数据表-----------------
		//public static bool createnewTable(string database, Models.Model model)
		//{
		//	bool result = false;
		//	Type type = model.GetType();
		//	StringBuilder sbr = new StringBuilder();
		//	sbr.Append("use " + newdatabase(database) + " CREATE TABLE ");
		//	sbr.Append("["+type.Name+"]");
		//	sbr.Append(" (");
		//	foreach (PropertyInfo pro in type.GetProperties())
		//	{
		//		if ((getDescription(pro) != string.Empty))
		//		{
		//			if (getDescription(pro) == "Key")
		//			{
		//				sbr.Append(pro.Name + " int PRIMARY KEY identity(1,1)  not null,");
		//			}
		//			else
		//			{
		//				sbr.Append(pro.Name + " " + getDescription(pro) + ",");
		//			}
		//		}
		//	}

		//	sbr.Append(")");
		//	if (!boolTableExists(type.Name, database))
		//	{
		//		DBHelper.newExecuteCommand(sbr.ToString());
		//		result = true;
		//	}
		//	else
		//	{
		//		result = false;
		//	}
		//	return result;
		//}
		//#endregion


		//// 获取字段的属性
		//public static string getDescription(PropertyInfo field)
		//{
		//	string result = string.Empty;
		//	var dbKey = (Models.DbKey)Attribute.GetCustomAttribute(field, typeof(Models.DbKey));
		//	if (dbKey != null)
		//		result = dbKey.Description;
		//	return result;
		//}
		//判断数据库是否存在
		public static bool boolDataExists(string database) {
			string sql = "select * From master.dbo.sysdatabases where name='" + newdatabase(database) + "'";
			DataTable dt = DBHelper.GetDataSet(sql);
			if (dt.Rows.Count > 0) {
				return true;
			}
			else {
				return false;
			}
		}
		//判断数据库是否为空
		public static bool boolDataIsNullorNot(string database) {
			string sql = "use " + database + " select ROW_NUMBER() over(order by name asc) as OrderId,name from sys.tables where is_ms_shipped = 0 order by name asc";
			DataTable dt = DBHelper.GetDataSet(sql);
			if (dt.Rows.Count > 0) {
				return true;
			}
			else {
				return false;
			}
		}
		//判断数据表是否存在
		public static bool boolTableExists(string table, string database) {
			string sql = "use " + newdatabase(database) + " select * From sysobjects where id = object_id(N'" + table + "')";
			DataTable dt = DBHelper.GetDataSet(sql);
			if (dt.Rows.Count > 0) {
				return true;
			}
			else {
				return false;
			}
		}
		//创建数据库文件夹
		public static void directory(string root) {
			if (root == string.Empty) {
				root = AppDomain.CurrentDomain.BaseDirectory;
				string filename = null;
				if (root != string.Empty) {
					string[] roots = root.Split('\\');
					if (roots.Length > 0 && roots != null) {
						for (int i = 0; i < roots.Length; i++) {
							if (i < roots.Length - 2) {
								filename += roots[i] + "\\";
							}
						}
					}
				}
				Directory.CreateDirectory(@filename + "数据库");
			}
			else {
				Directory.CreateDirectory(@root + "\\数据库");
			}
		}
		//判断输入数据库名是否合理
		public static string newdatabase(string database) {
			string newkey = null;
			string newdatabase = null;
			Regex regUpEnglish = new Regex("^[0-9]");

			newkey = database[0].ToString();
			if (regUpEnglish.IsMatch(newkey)) {
				newdatabase = "db_" + database;
			}
			else {
				newdatabase = database;
			}
			return newdatabase;
		}
	}
}
