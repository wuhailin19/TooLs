using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
	public class ContentModel
	{
		/// <summary>
		/// 构造函数(不带参数)
		/// </summary>
		public ContentModel()
		{ }

		/// <summary>
		/// 构造函数(带参数)
		/// </summary>
		/// <param name="modelid">模型ID</param>
		/// <param name="modelname">模型名称</param>
		/// <param name="modedesc">模型描述</param>
		/// <param name="tablename">数据表</param>
		/// <param name="issystem">是否是系统模型</param>
		/// <param name="addtime">添加时间(模型)</param>
		public ContentModel(int modelid, string modelname, string modedesc, string tablename, int issystem, DateTime addtime)
		{

			this._modelid = modelid;
			this._modelname = modelname;
			this._tablename = tablename;
			this._issystem = issystem;
			this._addtime = addtime;

		}
		#region Model
		private int _modelid;
		private string _modelname;
		private string _modeldesc;
		private string _tablename;
		private int _issystem;
		private DateTime _addtime;
		private string modelhtml;
		private int isshow;


		/// <summary>
		/// 模型自定义 是否显示模型
		/// </summary>
		public int Isshow {
			get { return isshow; }
			set { isshow = value; }
		}

		/// <summary>
		/// 模型自定义html
		/// </summary>
		public string Modelhtml {
			get { return modelhtml; }
			set { modelhtml = value; }
		}
		/// <summary>
		/// 模型ID
		/// </summary>
		public int ModelId {
			set { _modelid = value; }
			get { return _modelid; }
		}
		/// <summary>
		/// 模型名称
		/// </summary>
		public string ModelName {
			set { _modelname = value; }
			get { return _modelname; }
		}
		/// <summary>
		/// 模型描述
		/// </summary>
		public string ModelDesc {
			set { _modeldesc = value; }
			get { return _modeldesc; }
		}
		/// <summary>
		/// 数据表
		/// </summary>
		public string TableName {
			set { _tablename = value; }
			get { return _tablename; }
		}
		/// <summary>
		/// 是否是系统模型
		/// </summary>
		public int IsSystem {
			set { _issystem = value; }
			get { return _issystem; }
		}
		/// <summary>
		/// 添加时间（模型）
		/// </summary>
		public DateTime AddTime {
			set { _addtime = value; }
			get { return _addtime; }
		}
		#endregion Model

	}
}
