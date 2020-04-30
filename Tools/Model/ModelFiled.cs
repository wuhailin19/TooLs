using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
	/// <summary>
	/// 实体类ModelFiled(字段模型)
	/// </summary>
	public class ModelFiled
	{

		/// <summary>
		/// 构造函数(不带参数)
		/// </summary>
		public ModelFiled()
		{ }
		/// <summary>
		/// 构造函数(带参数)
		/// </summary>
		/// <param name="filedid">字段ID</param>
		/// <param name="filedname">字段名称</param>
		/// <param name="alias">字段别名(标注)</param>
		/// <param name="description">(描述 提示)</param>
		/// <param name="type">字段类别</param>
		/// <param name="content">定义的控件的样式属性</param>
		/// <param name="orderid">排序ID</param>
		/// <param name="validation">验证信息</param>
		/// <param name="addtime">添加时间</param>
		public ModelFiled(int filedid, string filedname, string alias, string description, string type, string content, int orderid, string validation, DateTime addtime)
		{
			this._filedid = filedid;
			this._filedname = filedname;
			this._alias = alias;
			this._description = description;
			this._type = type;
			this._content = content;
			this._orderid = orderid;
			this._validation = validation;
			this._addtime = addtime;

		}

		#region Model

		private int _filedid;
		private string _filedname;
		private string _alias;
		private string _description;
		private string _type;
		private string _content;
		private int _orderid;
		private string _validation;
		private DateTime _addtime;
		private int modelid;

		/// <summary>
		/// 模型编号
		/// </summary>
		public int ModelId {
			get { return modelid; }
			set { modelid = value; }
		}
		/// <summary>
		/// 字段ID
		/// </summary>
		public int FiledId {
			set { _filedid = value; }
			get { return _filedid; }
		}
		/// <summary>
		/// 字段名称
		/// </summary>
		public string FiledName {
			set { _filedname = value; }
			get { return _filedname; }
		}
		/// <summary>
		/// 字段别名(标注)
		/// </summary>
		public string Alias {
			set { _alias = value; }
			get { return _alias; }
		}
		/// <summary>
		/// (描述 提示)
		/// </summary>
		public string Description {
			set { _description = value; }
			get { return _description; }
		}
		/// <summary>
		/// 字段类别
		/// </summary>
		public string Type {
			set { _type = value; }
			get { return _type; }
		}
		/// <summary>
		/// 定义的控件的样式属性
		/// </summary>
		public string Content {
			set { _content = value; }
			get { return _content; }
		}
		/// <summary>
		/// 排序ID
		/// </summary>
		public int OrderId {
			set { _orderid = value; }
			get { return _orderid; }
		}
		/// <summary>
		/// 验证信息
		/// </summary>
		public string Validation {
			set { _validation = value; }
			get { return _validation; }
		}
		/// <summary>
		/// 添加时间
		/// </summary>
		public DateTime AddTime {
			set { _addtime = value; }
			get { return _addtime; }
		}

		#endregion Model

	}
}
