using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Tools
{
    public class ModelFiled_dal
    {
        /// <summary>
        /// 转换数据类型(数据库数据类型)
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static string ConvertDataType(ModelFiled modey)
        {
            string returntype = string.Empty;
            switch (modey.Type)
            {

                case "TextType":
                    string other = WebUtility.GetFieldContent(modey.Validation, 1, 1);
                    if (other == "integer")
                    {
                        returntype = "int"; break;
                    }
                    else if (other == "currency")
                    {
                        returntype = "decimal(18,2)"; break;
                    }
                    else
                    {
                        returntype = "nvarchar(255)"; break;
                    }
                case "MultipleTextType": returntype = "ntext"; break;
                case "Editor": returntype = "ntext"; break;
                case "eWebEditor": returntype = "ntext"; break;
                case "ListBoxType": returntype = "nvarchar(255)"; break;
                case "PicType": returntype = "nvarchar(255)"; break;
                case "FileType": returntype = "nvarchar(255)"; break;
                case "FileUpload": returntype = "nvarchar(255)"; break;
                case "ColorPicker": returntype = "nvarchar(255)"; break;
                case "MutiImgSelect": returntype = "ntext"; break;
                case "TimerPicker": returntype = "datetime"; break;
                case "ProvincialLinkage": returntype = "nvarchar(255)"; break;
                case "Dictionary-1": returntype = "nvarchar(50)"; break;
                case "Dictionary-2": returntype = "nvarchar(50)"; break;
                case "Relevance": returntype = "nvarchar(255)"; break;

            }
            return returntype;
        }
        /// <summary>
        ///  创建模型ModelHTML
        /// </summary>
        public static void CreateModelHTML(int modeid,ref StringBuilder sbr)
        {
            //js
            bool isEditorscript = true, isFileUpload = true, isColorPicker = true, isProvincialLinkage = true, isDictionary = true, isRelevance = true; ;
            IList<ModelFiled> modellist = GetModelList(modeid);
            
            sbr.Append("目前字段：\r\n");
            foreach (ModelFiled model in modellist)
            {
                sbr.Append(model.FiledName + $"            {model.Alias}\r\n");
            }
            
            StringBuilder sb = new StringBuilder();
            foreach (ModelFiled model in modellist)
            {
                if (model.Type == "Editor")
                {
                    if (isEditorscript)
                    {
                        isEditorscript = false;
                        sb.Append("<script src=\"htmlEditor/_ueditor/editor_config.js\" type=\"text/javascript\"></script><script src=\"htmlEditor/_ueditor/editor_all.js\" type=\"text/javascript\"></script>");
                        sb.Append("<tr class=\"editor\"><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");

                    }
                    else
                    {
                        sb.Append("<tr class=\"editor\"><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                }
                else if (model.Type == "eWebEditor")
                {

                    sb.Append("<tr class=\"editor\"><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");

                }
                else if (model.Type == "MutiImgSelect")
                {
                    sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":<br/><input  type=\"button\"  value=\"批量添加图片\"  class=\"btn2\" onclick=\"OpenDialogPageForMutiImageSenond('customtxt_" + model.FiledName + "')\" /><br/></td>");
                }
                else if (model.Type == "FileUpload")
                {
                    if (isFileUpload)
                    {
                        isFileUpload = false;
                        sb.Append("<link href=\"css/uploadifyUploader.css\" rel=\"stylesheet\" type=\"text/css\" /><script type=\"text/javascript\" src=\"swfupload/jquery.uploadify2.1.4/swfobject.js\"></script><script type=\"text/javascript\" src=\"swfupload/jquery.uploadify2.1.4/jquery.uploadify.v2.1.4.min.js\"></script>");
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                    else
                    {
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                }
                else if (model.Type == "ColorPicker")
                {
                    if (isColorPicker)
                    {
                        isColorPicker = false;
                        sb.Append("<script src=\"js/colorpicker/colorpicker.js\" type=\"text/javascript\"></script>");
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                    else
                    {
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                }
                else if (model.Type == "ProvincialLinkage")
                {
                    if (isProvincialLinkage)
                    {
                        isProvincialLinkage = false;
                        sb.Append("<script src=\"js/ProvincialLinkage/PCASarea.js\"  type=\"text/javascript\"></script>");
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                    else
                    {
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                }
                else if (model.Type == "Dictionary-1" || model.Type == "Dictionary-2")
                {

                    if (isDictionary)
                    {
                        isDictionary = false;
                        sb.Append("<script src=\"js/fancybox/jquery.fancybox-1.3.4.pack.js\" type=\"text/javascript\"></script>");
                        sb.Append("<script type=\"text/javascript\">var wherefunciont;jQuery(function () {$(\"a[rel='adddictionaryManage']\").fancybox({'width': '60%','height': '85%', 'autoScale': true,'transitionIn': 'elastic','transitionOut': 'elastic','type': 'iframe', 'overlayOpacity': 0.3, 'enableEscapeButton': false, 'enableKeyboardNav': false,'margin': 0, 'showNavArrows': false,'titleShow': false, 'onClosed': function () { eval(wherefunciont + '()'); }});$(\"a[rel='adddictionaryManage']\").live(\"click\", function () { wherefunciont = jQuery(this).attr(\"attrs\") });})</script>");
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                    else
                    {
                        sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                    }
                }
                else
                {
                    sb.Append("<tr><td width=\"18%\" style=\"text-align:right; padding-right:10px;\">" + model.Alias + ":</td>");
                }
                switch (model.Type)
                {
                    case "TextType": sb.Append("<td>" + CommonClass.GetTextTypeShowStyle(model) + "</td>"); break;
                    case "MultipleTextType": sb.Append("<td>" + CommonClass.GetMutiTxtShowStyle(model) + "</td>"); break;
                    case "Editor": sb.Append("<td>" + CommonClass.GetEditorTxtShowStyle(model) + "</td>"); break;
                    case "eWebEditor": sb.Append("<td>" + CommonClass.GeteWebEditorTxtShowStyle(model) + "</td>"); break;
                    case "ListBoxType": sb.Append("<td>" + CommonClass.GetSelectListShowStyle(model) + "</td>"); break;
                    case "PicType": sb.Append("<td>" + CommonClass.GetPicShowStyle(model) + "</td>"); break;
                    case "FileType": sb.Append("<td>" + CommonClass.GetFileShowStyle(model) + "</td>"); break;
                    case "FileUpload": sb.Append("<td>" + CommonClass.GetFileUploadStyle(model) + "</td>"); break;
                    case "ColorPicker": sb.Append("<td>" + CommonClass.GetColorPickerStyle(model) + "</td>"); break;
                    case "MutiImgSelect": sb.Append("<td><ul class=\"customtxt_" + model.FiledName + "\"></ul></td>"); break;
                    case "TimerPicker": sb.Append("<td><input id=\"customtxt_" + model.FiledName + "\"  name=\"customtxt_" + model.FiledName + "\" type=\"text\" maxlength=\"25\" style=\"width:120px;\" class=\"input-txt\"   onfocus=\"WdatePicker({isShowClear:true,readOnly:true,dateFmt:'yyyy-MM-dd HH:mm:ss'});\"  />&nbsp;<span class=\"prompttext\">" + model.Description + "</span></td>"); break;
                    case "ProvincialLinkage": sb.Append("<td>" + CommonClass.GetProvincialLinkageStyle(model) + "</td>"); break;
                    case "Dictionary-1": sb.Append("<td>" + CommonClass.GetDictionary1LinkageStyle(model) + "</td>"); break;
                    case "Dictionary-2": sb.Append("<td>" + CommonClass.GetDictionary2LinkageStyle(model) + "</td>"); break;
                        //case "Relevance": sb.Append("<td>" + CommonClass.GetRelevanceShowStyle(model) + "</td>"); break;
                }
                sb.Append("</tr>");

            }

            ContentModel model2 = new ContentModel();
            model2.Modelhtml = System.Web.HttpUtility.HtmlEncode(sb.ToString());
            model2.ModelId = modeid;

            UpdateModelHTML(model2);
        }
        /// <summary>
        /// 修改模型
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool UpdateModelHTML(ContentModel model)
        {
            string sql = "update ContentModel set Modelhtml='" + WebUtility.CheckStr(model.Modelhtml) + "' where ModelId=" + model.ModelId;
            return DBHelper.ExecuteCommand(sql) >= 1;
        }
        //获取字段的数量
        public static int GetFiledCount(int modelid, string filedname)
        {
            string sql = $"select count(FiledId) Fcount from ModelFiled where FiledName like'%{filedname}%' and ModelId={modelid}";
            DataTable dt = DBHelper.GetDataSet(sql);
            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                return Convert.ToInt32(dt.Rows[0]["Fcount"]);
            }
            else
            {
                return 0;
            }
        }
        //根据表名获取ContentModel字段
        public static DataRow GetModelIdByExpression(string swhere, string filed)
        {
            string sql = $"select {filed} from ContentModel where {swhere}";
            DataTable dt = DBHelper.GetDataSet(sql);
            if (dt.Rows.Count > 0 && dt.Rows != null)
            {
                return dt.Rows[0];
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// 获得希望能最新排序号
        /// </summary>
        /// <returns>int</returns>
        public static int GetTopOrderID(string tablename)
        {
            string sql = "";

            sql = "SELECT top 1 orderid from " + tablename + " order by orderid desc";
            DataTable dt = DBHelper.GetDataSet(sql);
            if (dt.Rows.Count > 0)
            {
                return Convert.ToInt32(dt.Rows[0][0]) + 1;
            }
            else { return 0; }

        }
        /// <summary>
        /// 添加模型字段
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static bool Add(ModelFiled model)
        {
            try
            {
                string tablename = GetModelIdByExpression("ModelId=" + model.ModelId, "TableName")["TableName"].ToString();
                string sqlf = "ALTER TABLE [" + WebUtility.CheckStr(tablename) + "]  ADD [" + WebUtility.CheckStr(model.FiledName) + "]  " + ConvertDataType(model) + "";
                DBHelper.ExecuteCommand(sqlf);

                string sql = "insert into ModelFiled (  " +
                           "[FiledName]," +
                           "[Alias]," +
                           "[Description]," +
                           "[Type]," +
                           "[Content]," +
                           "[OrderId]," +
                           "[Validation]," +
                           "[ModelID]" +
                           " )   " +
                           "values( " +
                           "'" + WebUtility.CheckStr(model.FiledName) + "', " +
                           "'" + WebUtility.CheckStr(model.Alias) + "', " +
                           "'" + WebUtility.CheckStr(model.Description) + "', " +
                           "'" + WebUtility.CheckStr(model.Type) + "', " +
                           "'" + WebUtility.CheckStr(model.Content) + "', " +
                           "" + model.OrderId + "," +
                           "'" + WebUtility.CheckStr(model.Validation) + "', " +
                           "" + model.ModelId + "" +
                           " )";

                DBHelper.ExecuteCommand(sql);
                return true;

            }
            catch (Exception ex)
            {
                return false;
            }
        }
        /// <summary>
        /// 获取字段列表
        /// </summary>
        /// <param name="modeid"></param>
        /// <returns></returns>
        public static IList<ModelFiled> GetModelList(int modeid)
        {

            List<ModelFiled> modellist = new List<ModelFiled>();
            string sql = "select * from ModelFiled where ModelID=" + modeid + "  order by OrderId Asc";
            using (DataTable dt = DBHelper.GetDataSet(sql))
            {

                if (dt != null && dt.Rows.Count >= 1)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {

                        ModelFiled model = new ModelFiled();
                        model.FiledId = Convert.ToInt32(dt.Rows[i]["FiledId"]);
                        model.Alias = dt.Rows[i]["Alias"].ToString();
                        model.FiledName = dt.Rows[i]["FiledName"].ToString();
                        model.Description = dt.Rows[i]["Description"].ToString();
                        model.ModelId = Convert.ToInt32(dt.Rows[i]["ModelID"]);
                        model.Content = dt.Rows[i]["Content"].ToString();
                        model.OrderId = Convert.ToInt32(dt.Rows[i]["OrderId"]);
                        model.Type = dt.Rows[i]["Type"].ToString();
                        model.Validation = dt.Rows[i]["Validation"].ToString();
                        model.AddTime = Convert.ToDateTime(dt.Rows[i]["AddTime"]);
                        modellist.Add(model);
                    }

                }

            }
            return modellist;

        }
    }
}
