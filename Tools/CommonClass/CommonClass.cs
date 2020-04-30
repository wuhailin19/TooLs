
using System;
using System.Collections;

using System.Data;
using System.Drawing;

using System.IO;
using System.Resources;
using System.Runtime.Serialization.Formatters.Binary;

using System.Text;
using System.Text.RegularExpressions;
using System.Threading;

using System.Xml.Serialization;
using System.Web;
using System.Web.Security;
using System.Web.UI.WebControls;
using System.Xml;
using System.Drawing.Imaging;
using System.Data.SqlClient;
using System.Net;

namespace Tools
{
    /// <summary>
    /// 对web层字符的操作 公共方法 此类不能继承 
    /// </summary>
    public sealed class CommonClass
    {

        public static bool CheckIsHasModel(int modeid)
        {
            string sql = "select count(ModelId) from ContentModel where ModelId=" + modeid;
            return DBHelper.Exists(sql);
        }
        /// <summary>
        /// 替换文本样式 <p></p><br>
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        public static string GetFontStyle(string Value)
        {
            StringBuilder stringBuilder = new StringBuilder();
            Value = Value.Replace("\r\n", "|").Replace("||", "→");
            string[] Arry = Value.Split('→');
            for (int i = 0; i < Arry.Length; i++)
            {
                string[] Arry2 = Arry[i].Split('|');
                for (int x = 0; x < Arry2.Length; x++)
                {
                    stringBuilder.Append("<p>" + Arry2[x] + "</p>");
                }
                if (i != Arry.Length - 1)
                {
                    stringBuilder.Append("<br>");
                }
            }

            return stringBuilder.ToString();
        }
        
        /// <summary>
        /// 获取图片长宽
        /// </summary>
        /// <param name="filre">图片路径</param>
        /// <returns></returns>
        public static string GetImgWidthAndheight(string filre)
        {
            if (!string.IsNullOrEmpty(filre))
            {
                try
                {
                    System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(System.Web.HttpContext.Current.Server.MapPath(filre));
                    string newstr = bitmap.Width + "x" + bitmap.Height;
                    return newstr;
                }
                catch
                {
                    return "";
                }
            }
            return "";
        }
        public static string GetSummylineCode(string str, string tag)
        {
            if (str.Length > 0)
            {
                return "<" + tag + ">" + str.Replace("\n", "</" + tag + "><" + tag + ">") + "</" + tag + ">";
            }
            return string.Empty;
        }
        /// <summary>
        /// 分页 多底标方法
        /// </summary>
        /// <param name="HtmlPre">上一页Html</param>
        /// <param name="HtmlNext">下一页Html</param>
        /// <param name="KeyWord">关键字</param>
        /// <param name="NowPageIndex">当前页码</param>
        /// <param name="AllCount">数据总条数</param>
        /// <param name="PageSize">页面显示条数</param>
        /// <returns></returns>
        public static string AllGetPage(string HtmlFirst, string HtmlPre, string LastHtml, string HtmlNext, string KeyWordName, string KeyWord, int NowPageIndex, int AllCount, int PageSize, string MD, string ChoiceStyle, string NotChoiceStyle)
        {
            string Page = HttpContext.Current.Request.Url.AbsolutePath;//获取当前页面地址
            StringBuilder Paging = new StringBuilder();
            StringBuilder PagingPre = new StringBuilder();
            StringBuilder PagingNext = new StringBuilder();

            int AllPageIndex = AllCount / PageSize;
            if (AllCount % PageSize != 0)
            {
                AllPageIndex += 1;
            }
            #region 上一页下一页
            if (NowPageIndex > 1)
            {
                PagingPre.AppendFormat(HtmlFirst, Page + "?" + KeyWordName + "=" + KeyWord + "&Page=1" + MD);
                PagingPre.AppendFormat(HtmlPre, Page + "?" + KeyWordName + "=" + KeyWord + "&Page=" + (NowPageIndex - 1) + MD);
            }
            else
            {
                PagingPre.AppendFormat(HtmlFirst, "javascript:void(0)");
                PagingPre.AppendFormat(HtmlPre, "javascript:void(0)");
            }


            if (NowPageIndex < AllPageIndex)
            {
                PagingNext.AppendFormat(LastHtml, Page + "?" + KeyWordName + "=" + KeyWord + "&Page=" + AllPageIndex + MD);
                PagingNext.AppendFormat(HtmlNext, Page + "?" + KeyWordName + "=" + KeyWord + "&Page=" + (NowPageIndex + 1) + MD);
            }
            else
            {
                PagingNext.AppendFormat(LastHtml, "javascript:void(0)");
                PagingNext.AppendFormat(HtmlNext, "javascript:void(0)");
            }
            #endregion

            #region 中间部分
            StringBuilder PagingCenter = new StringBuilder();
            //样式
            if (NowPageIndex <= 1)
            {
                //PagingCenter.Append("<a href=\"#news\" class=\"arrow prev\">上一页</a>")；
            }
            else
            {
                if (KeyWordName != "")
                {
                    if (NowPageIndex > 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                        PagingCenter.Append("<a href=\"Javascript:;\" " + NotChoiceStyle + "> ...</a> ");
                    }
                    if (NowPageIndex == 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                    }
                }
                else
                {
                    if (NowPageIndex > 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                        PagingCenter.Append("<a href=\"Javascript:;\" " + NotChoiceStyle + "> ... </a>");
                    }
                    if (NowPageIndex == 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                    }
                }
            }
            int j = 0;
            if (NowPageIndex <= 2)
            {
                j = 2;
            }
            else
            {
                j = NowPageIndex;
            }
            for (int i = (NowPageIndex - 1 > 0 ? NowPageIndex - 1 : 1); i <= ((j + 1) >= AllPageIndex ? AllPageIndex : (j + 1)); i++)
            {

                if (NowPageIndex == i)
                {
                    //选中样式
                    PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=" + (i) + "" + MD + "\" " + ChoiceStyle + ">" + (i) + "</a> ");
                }
                else
                {
                    if (KeyWordName != "")
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=" + (i) + "" + MD + "\"" + NotChoiceStyle + " >" + (i) + " </a>  ");
                    }
                    else
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?page=" + (i) + "" + MD + "\"" + NotChoiceStyle + " >" + (i) + "</a> ");
                    }
                }
            }
            if (AllPageIndex > 5 && NowPageIndex + 2 <= AllPageIndex)
            {
                if (KeyWordName != "")
                {
                    PagingCenter.Append("<a href=\"Javascript:;\" " + NotChoiceStyle + "> ... </a>");
                    PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=" + AllPageIndex + "" + MD + "\" " + NotChoiceStyle + ">" + AllPageIndex + "</a> ");
                }
                else
                {
                    PagingCenter.Append("<a href=\"Javascript:;\"" + NotChoiceStyle + " > ... </a>");
                    PagingCenter.Append("<a href=\"" + Page + "?page=" + AllPageIndex + "" + MD + "\"" + NotChoiceStyle + ">" + AllPageIndex + " </a> ");
                }
            }
            #endregion
            //拼写
            Paging.Append(PagingPre);
            Paging.Append(PagingNext);
            Paging.Append(PagingCenter);
            return Paging.ToString();
        }


        /// <summary>
        /// 分页 多底标方法
        /// </summary>
        /// <param name="HtmlPre">上一页Html</param>
        /// <param name="HtmlNext">下一页Html</param>
        /// <param name="KeyWord">关键字</param>
        /// <param name="NowPageIndex">当前页码</param>
        /// <param name="AllCount">数据总条数</param>
        /// <param name="PageSize">页面显示条数</param>
        /// <returns></returns>
        public static string AllGetPage(string HtmlPre, string HtmlNext, string KeyWordName, string KeyWord, int NowPageIndex, int AllCount, int PageSize, string MD, string ChoiceStyle, string NotChoiceStyle)
        {
            string Page = HttpContext.Current.Request.Url.AbsolutePath;//获取当前页面地址
            StringBuilder Paging = new StringBuilder();
            StringBuilder PagingPre = new StringBuilder();
            StringBuilder PagingNext = new StringBuilder();

            int AllPageIndex = AllCount / PageSize;
            if (AllCount % PageSize != 0)
            {
                AllPageIndex += 1;
            }
            #region 上一页下一页
            if (NowPageIndex > 1)
            {

                PagingPre.AppendFormat(HtmlPre, Page + "?" + KeyWordName + "=" + KeyWord + "&Page=" + (NowPageIndex - 1) + MD);
            }
            else
            {
                PagingPre.AppendFormat(HtmlPre, "javascript:void(0)");
            }


            if (NowPageIndex < AllPageIndex)
            {

                PagingNext.AppendFormat(HtmlNext, Page + "?" + KeyWordName + "=" + KeyWord + "&Page=" + (NowPageIndex + 1) + MD);
            }
            else
            {
                PagingNext.AppendFormat(HtmlNext, "javascript:void(0)");
            }
            #endregion

            #region 中间部分
            StringBuilder PagingCenter = new StringBuilder();
            //样式
            if (NowPageIndex <= 1)
            {
                //PagingCenter.Append("<a href=\"#news\" class=\"arrow prev\">上一页</a>")；
            }
            else
            {
                if (KeyWordName != "")
                {
                    if (NowPageIndex > 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                        PagingCenter.Append("<a href=\"Javascript:;\" " + NotChoiceStyle + "> ...</a> ");
                    }
                    if (NowPageIndex == 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                    }
                }
                else
                {
                    if (NowPageIndex > 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                        PagingCenter.Append("<a href=\"Javascript:;\" " + NotChoiceStyle + "> ... </a>");
                    }
                    if (NowPageIndex == 3)
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=1" + MD + "\" " + NotChoiceStyle + ">1</a>");
                    }
                }
            }
            int j = 0;
            if (NowPageIndex <= 2)
            {
                j = 2;
            }
            else
            {
                j = NowPageIndex;
            }
            for (int i = (NowPageIndex - 1 > 0 ? NowPageIndex - 1 : 1); i <= ((j + 1) >= AllPageIndex ? AllPageIndex : (j + 1)); i++)
            {

                if (NowPageIndex == i)
                {
                    //选中样式
                    PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=" + (i) + "" + MD + "\" " + ChoiceStyle + ">" + (i) + "</a> ");
                }
                else
                {
                    if (KeyWordName != "")
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=" + (i) + "" + MD + "\"" + NotChoiceStyle + " >" + (i) + " </a>  ");
                    }
                    else
                    {
                        PagingCenter.Append("<a href=\"" + Page + "?page=" + (i) + "" + MD + "\"" + NotChoiceStyle + " >" + (i) + "</a> ");
                    }
                }
            }
            if (AllPageIndex > 5 && NowPageIndex + 2 <= AllPageIndex)
            {
                if (KeyWordName != "")
                {
                    PagingCenter.Append("<a href=\"Javascript:;\" " + NotChoiceStyle + "> ... </a>");
                    PagingCenter.Append("<a href=\"" + Page + "?" + KeyWordName + "=" + KeyWord + "&page=" + AllPageIndex + "" + MD + "\" " + NotChoiceStyle + ">" + AllPageIndex + "</a> ");
                }
                else
                {
                    PagingCenter.Append("<a href=\"Javascript:;\"" + NotChoiceStyle + " > ... </a>");
                    PagingCenter.Append("<a href=\"" + Page + "?page=" + AllPageIndex + "" + MD + "\"" + NotChoiceStyle + ">" + AllPageIndex + " </a> ");
                }
            }
            #endregion
            //拼写
            Paging.Append(PagingPre);
            Paging.Append(PagingCenter);
            Paging.Append(PagingNext);
            return Paging.ToString();
        }
        public static string getStateName(string checkleve)
        {
            string name = "未知";
            switch (checkleve)
            {
                case "-99": name = "<span style=\"color:red\">投稿</span>"; break;
                case "-88": name = "<span style=\"color:red\">待审核</span>"; break;
                case "1": name = "<span style=\"color:red\">初审通过,待二审</span>"; break;
                case "-1": name = "<span style=\"color:red\">初审退稿</span>"; break;
                case "2": name = "<span style=\"color:red\">二审通过,待三审</span>"; break;
                case "-2": name = "<span style=\"color:red\">二审退稿</span>"; break;
                case "3": name = "<span style=\"color:red\">三审通过,待终审</span>"; break;
                case "-3": name = "<span style=\"color:red\">三审退稿</span>"; break;
                case "4": name = "<span style=\"color:green\">已审核</span"; break;
                case "-4": name = "<span style=\"color:red\">终审退稿</span>"; break;
            }
            return name;


        }
        
        /// <summary>
        /// 获取普通单行文本框显示样式
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetTextTypeShowStyle(ModelFiled model)
        {

            string inputshowstyle = string.Empty;
            string size = WebUtility.GetFieldContent(model.Content, 0, 1);
            string defaultvalue = WebUtility.GetFieldContent(model.Content, 1, 1);
            string textLength = WebUtility.GetFieldContent(model.Content, 2, 1);
            string request = WebUtility.GetFieldContent(model.Validation, 0, 1);
            string other = WebUtility.GetFieldContent(model.Validation, 1, 1);
            string validate = string.Empty;
            size = (size == "") ? "style=\"width:320px\"" : "style=\"width:" + size + "px\"";
            textLength = (textLength == "") ? "maxlength=\"100\"" : "maxlength=\"" + textLength + "\"";

            if (request == "" && other == "")
            {
                validate = string.Empty;
            }
            else
            {
                if (request != "" && other == "")
                {
                    validate = "validate[required]";
                }
                else if (request == "" && other != "")
                {
                    validate = "validate[custom[" + other + "]]";
                }
                else
                {
                    validate = "validate[required,custom[" + other + "]]";
                }

            }
            inputshowstyle = string.Format("<input id=\"customtxt_" + model.FiledName + "\"  name=\"customtxt_" + model.FiledName + "\"  {0}  type=\"text\"  class=\"{1} input-txt\"  {2}  value=\"" + defaultvalue + "\" />&nbsp;<span class=\"prompttext\">" + model.Description + "</span>", size, validate, textLength);
            return inputshowstyle;

        }
        public static int getmaxmovenumber(string ids, string tablename, string id, int pid, int sysid)
        {

            int number = DBHelper.ExecuteCommand("select count(" + id + ") from " + tablename + " where " + id + " not in(" + ids + ") and ParentId in(" + pid + ") and SystemId in(" + sysid + ")");
            if (number != 0)
            {
                return number;
            }
            else
            {
                return 1;
            }
        }
        /// <summary>
        ///  图片显示
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetPicShowStyle(ModelFiled model)
        {

            return "<input id=\"customtxt_" + model.FiledName + "\"  name=\"customtxt_" + model.FiledName + "\"  type=\"text\"  class=\"input-txt\"  maxlength=\"250\" />&nbsp;<input id=\"selectimg\" type=\"button\" value=\"选择图片\" class=\"btn2\" onclick=\"OpenPictureDialog('customtxt_" + model.FiledName + "','previw_" + model.FiledName + "')\" />&nbsp; &nbsp;<a id=\"previw_" + model.FiledName + "\" href=\"#\" rel=\"lightbox\">预览图片</a>&nbsp;&nbsp;<span class=\"prompttext\">" + model.Description + "</span>";

        }
        /// <summary>
        ///  附加选择
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetFileShowStyle(ModelFiled model)
        {

            return "<input id=\"customtxt_" + model.FiledName + "\"  name=\"customtxt_" + model.FiledName + "\"  type=\"text\"  class=\"input-txt\"  maxlength=\"250\" />&nbsp;<input id=\"selectimg\" type=\"button\" value=\"选择文件\" class=\"btn2\" onclick=\"OpenFileDialog('customtxt_" + model.FiledName + "')\" />&nbsp;<span class=\"prompttext\">" + model.Description + "</span>";

        }
        /// <summary>
        /// 文件上传器
        /// </summary>
        /// <param name="model"></param>
        public static string GetFileUploadStyle(ModelFiled model)
        {
            return "<div style=\"width:545px;padding:5px 0 0 0; overflow:hidden; position:relative;\"><input id=\"customtxt_" + model.FiledName + "\" type=\"text\"  name=\"customtxt_" + model.FiledName + "\" class=\"input-txt\" style=\"float:left;\"/><a href=\"javascript:void(0)\" onclick=\"$('#uploadify_" + model.FiledName + "').uploadifyUpload()\"  style=\"position:absolute;left:405px;top:10px;\"><img src=\"swfupload/jquery.uploadify2.1.4/uploadloding.gif\" /></a>&nbsp;&nbsp;<span class=\"prompttext\" style=\"position:absolute;left:480px;top:0px;\">" + model.Description + "</span><input type=\"file\" name=\"uploadify_" + model.FiledName + "\" id=\"uploadify_" + model.FiledName + "\"  style=\"display:none;\" /></div>{$" + model.FiledName + "$}";

        }
        /// <summary>
        ///  颜色选择
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetColorPickerStyle(ModelFiled model)
        {

            return "<input id=\"customtxt_" + model.FiledName + "\"  name=\"customtxt_" + model.FiledName + "\"  type=\"text\"  class=\"input-txt\" style=\"width:50px\"  maxlength=\"6\" />&nbsp;<span class=\"prompttext\">" + model.Description + "</span><script type=\"text/javascript\">$(\"#customtxt_" + model.FiledName + "\").ColorPicker({ onSubmit: function(hsb, hex, rgb, el) { $(el).val(hex); $(el).ColorPickerHide(); jQuery(\"#customtxt_" + model.FiledName + "\").css(\"background\",\"#\"+hex);},onBeforeShow: function () { $(this).ColorPickerSetColor(this.value);}}).bind(\"keyup\", function(){$(this).ColorPickerSetColor(this.value);jQuery(\"#customtxt_" + model.FiledName + "\").css(\"background\", \"#\" + this.value);});</script>";

        }
        /// <summary>
        ///  省级联动
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetProvincialLinkageStyle(ModelFiled model)
        {
            return "<select id=\"customtxt_" + model.FiledName + "_provice\" name=\"customtxt_" + model.FiledName + "_provice\"></select><select id=\"customtxt_" + model.FiledName + "_city\" name=\"customtxt_" + model.FiledName + "_city\"></select><select id=\"customtxt_" + model.FiledName + "_eara\" name=\"customtxt_" + model.FiledName + "_eara\"></select>&nbsp;&nbsp;<span class=\"prompttext\">" + model.Description + "</span><script type=\"text/javascript\">new PCAS(\"customtxt_" + model.FiledName + "_provice\",\"customtxt_" + model.FiledName + "_city\",\"customtxt_" + model.FiledName + "_eara\")</script>";
        }
        /// <summary>
        /// 生成js文件
        /// </summary>
        /// <param name="width">广告宽度</param>
        /// <param name="height"></param>
        /// <param name="path"></param>
        /// <param name="guid"></param>
        /// <param name="queryArray"></param>
        public static void CreateAdvertisement(string width, string height, string guid, string queryArrays, string adtype)
        {
            StringBuilder builder = new StringBuilder();
            string[] QueryArray;
            switch (adtype)
            {
                case "1":
                    QueryArray = queryArrays.Split(new string[] { "_mysplit_" }, StringSplitOptions.None);
                    //生成JS
                    if (QueryArray[1] != "")
                    {
                        builder.Append("document.write('<a href=\"" + QueryArray[1] + "\" title=\"" + QueryArray[2] + "\" target=\"_blank\"><img src=\"/" + QueryArray[0] + "\" width=\"" + width + "\" height=\"" + height + "\" border=\"0\"></a>');");
                    }
                    else
                    {
                        if (QueryArray[0] == "")
                        {
                            builder.Append("");
                        }
                        else
                        {
                            builder.Append("document.write('<img src=\"/" + QueryArray[0] + "\" width=\"" + width + "\" height=\"" + height + "\" border=\"0\">');");
                        }
                    }

                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/AdShows/") + guid + ".js", builder.ToString());

                    break;
                case "2":
                    QueryArray = queryArrays.Split(new string[] { "_mysplit_" }, StringSplitOptions.None);
                    //生成JS
                    if (QueryArray[1] != "")
                    {
                        builder.Append("document.write('<div style=\"width:" + width + "px; height:" + height + "px; display: block; position: relative;\"><a  style=\"left: 0px; top: 0px; width:" + width + "px; height:" + height + "px; filter: alpha(opacity=0); position: absolute; z-index:100; cursor: pointer; opacity: 0; background-color: rgb(255, 255, 255);\" href=\"" + QueryArray[1] + "\" title=\"" + QueryArray[2] + "\" target=\"_blank\"></a><embed type=\"application/x-shockwave-flash\" class=\"edui-faked-video\"  pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"/" + QueryArray[0] + "\"  width=\"" + width + "\" height=\"" + height + "\" wmode=\"transparent\"  play=\"true\" loop=\"false\" menu=\"false\"  allowscriptaccess=\"never\" allowfullscreen=\"true\"/></div>');");
                    }
                    else
                    {
                        if (QueryArray[0] == "")
                        {
                            builder.Append("");
                        }
                        else
                        {
                            builder.Append("document.write('<embed type=\"application/x-shockwave-flash\" class=\"edui-faked-video\"  pluginspage=\"http://www.macromedia.com/go/getflashplayer\" src=\"/" + QueryArray[0] + "\"  width=\"" + width + "\" height=\"" + height + "\" wmode=\"transparent\"  play=\"true\" loop=\"false\" menu=\"false\"  allowscriptaccess=\"never\" allowfullscreen=\"true\"/>');");
                        }
                    }

                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/AdShows/") + guid + ".js", builder.ToString());
                    break;
                case "3":
                    QueryArray = queryArrays.Split(new string[] { "_mysplit_" }, StringSplitOptions.None);
                    builder.Append("document.getElementById(\"" + QueryArray[0] + "\").style.backgroundImage=\"url(/" + QueryArray[1] + ")\";");
                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/AdShows/") + guid + ".js", builder.ToString());
                    break;
                case "4":
                    QueryArray = queryArrays.Split(new string[] { "_mysplit_" }, StringSplitOptions.None);
                    //生成JS
                    if (QueryArray[1] != "")
                    {
                        builder.Append("document.write('<div id=\"" + guid + "\"  style=\"position:absolute;z-index:999999\"><a href=\"" + QueryArray[1] + "\" target=\"_blank\"><img alt=\"" + QueryArray[2] + "\" src=\"/" + QueryArray[0] + "\" height=\"" + height + "\" width=\"" + width + "\" border=\"0\" /></a></div>')");
                    }
                    else
                    {
                        builder.Append("document.write('<div id=\"" + guid + "\"  style=\"position:absolute;z-index:999999\"><img alt=\"" + QueryArray[2] + "\" src=\"/" + QueryArray[0] + "\" height=\"" + height + "\" width=\"" + width + "\" border=\"0\" /></div>')");
                    }
                    builder.Append(@"
function addEvent(obj, evtType, func, cap) {
cap = cap || false;
if (obj.addEventListener) {
    obj.addEventListener(evtType, func, cap);
    return true;
} else if (obj.attachEvent) {
    if (cap) {
        obj.setCapture();
        return true;
    } else {
        return obj.attachEvent('on' + evtType, func);
    }
} else {
    return false;
}
}
function getPageScroll() {
var xScroll, yScroll;
if (self.pageXOffset) {
    xScroll = self.pageXOffset;
} else if (document.documentElement && document.documentElement.scrollLeft) {
    xScroll = document.documentElement.scrollLeft;
} else if (document.body) {
    xScroll = document.body.scrollLeft;
}
if (self.pageYOffset) {
    yScroll = self.pageYOffset;
} else if (document.documentElement && document.documentElement.scrollTop) {
    yScroll = document.documentElement.scrollTop;
} else if (document.body) {
    yScroll = document.body.scrollTop;
}
arrayPageScroll = new Array(xScroll, yScroll);
return arrayPageScroll;
}
function GetPageSize() {
var xScroll, yScroll;
if (window.innerHeight && window.scrollMaxY) {
    xScroll = document.body.scrollWidth;
    yScroll = window.innerHeight + window.scrollMaxY;
} else if (document.body.scrollHeight > document.body.offsetHeight) {
    xScroll = document.body.scrollWidth;
    yScroll = document.body.scrollHeight;
} else {
    xScroll = document.body.offsetWidth;
    yScroll = document.body.offsetHeight;
}
var windowWidth, windowHeight;
if (self.innerHeight) {
    windowWidth = self.innerWidth;
    windowHeight = self.innerHeight;
} else if (document.documentElement && document.documentElement.clientHeight) {
    windowWidth = document.documentElement.clientWidth;
    windowHeight = document.documentElement.clientHeight;
} else if (document.body) {
    windowWidth = document.body.clientWidth;
    windowHeight = document.body.clientHeight;
}
if (yScroll < windowHeight) {
    pageHeight = windowHeight;
} else {
    pageHeight = yScroll;
}
if (xScroll < windowWidth) {
    pageWidth = windowWidth;
} else {
    pageWidth = xScroll;
}
arrayPageSize = new Array(pageWidth, pageHeight, windowWidth, windowHeight)
return arrayPageSize;
}
var AdMoveConfig = new Object();
AdMoveConfig.IsInitialized = false;
AdMoveConfig.ScrollX = 0;
AdMoveConfig.ScrollY = 0;
AdMoveConfig.MoveWidth = 0;
AdMoveConfig.MoveHeight = 0;
AdMoveConfig.Resize = function () {
var winsize = GetPageSize();
AdMoveConfig.MoveWidth = winsize[2];
AdMoveConfig.MoveHeight = winsize[3];
AdMoveConfig.Scroll();
}
AdMoveConfig.Scroll = function () {
var winscroll = getPageScroll();
AdMoveConfig.ScrollX = winscroll[0];
AdMoveConfig.ScrollY = winscroll[1];
}
addEvent(window, 'resize', AdMoveConfig.Resize);
addEvent(window,'scroll', AdMoveConfig.Scroll);
function AdMove(id) {
if (!AdMoveConfig.IsInitialized) {
    AdMoveConfig.Resize();
    AdMoveConfig.IsInitialized = true;
}
var obj = document.getElementById(id);
obj.style.position = 'absolute';
var W = AdMoveConfig.MoveWidth - obj.offsetWidth;
var H = AdMoveConfig.MoveHeight - obj.offsetHeight;
var x = W * Math.random(), y = H * Math.random();
var rad = (Math.random() + 1) * Math.PI / 6;
var kx = Math.sin(rad), ky = Math.cos(rad);
var dirx = (Math.random() < 0.5 ? 1 : -1), diry = (Math.random() < 0.5 ? 1 : -1);
var step = 1;
var interval;
this.SetLocation = function (vx, vy) { x = vx; y = vy; }
this.SetDirection = function (vx, vy) { dirx = vx; diry = vy; }
obj.CustomMethod = function () {
    obj.style.left = (x + AdMoveConfig.ScrollX) + 'px';
    obj.style.top = (y + AdMoveConfig.ScrollY) + 'px';
    rad = (Math.random() + 1) * Math.PI / 6;
    W = AdMoveConfig.MoveWidth - obj.offsetWidth;
    H = AdMoveConfig.MoveHeight - obj.offsetHeight;
    x = x + step * kx * dirx;
    if (x < 0) { dirx = 1; x = 0; kx = Math.sin(rad); ky = Math.cos(rad); }
    if (x > W) { dirx = -1; x = W; kx = Math.sin(rad); ky = Math.cos(rad); }
    y = y + step * ky * diry;
    if (y < 0) { diry = 1; y = 0; kx = Math.sin(rad); ky = Math.cos(rad); }
    if (y > H) { diry = -1; y = H; kx = Math.sin(rad); ky = Math.cos(rad); }
}
this.Run = function () {
    var delay = 15;
    interval = setInterval(obj.CustomMethod, delay);
    obj.onmouseover = function () { clearInterval(interval); }
    obj.onmouseout = function () { interval = setInterval(obj.CustomMethod, delay); }
}
}
          ");
                    builder.Append("new AdMove('" + guid + "').Run();");
                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/AdShows/") + guid + ".js", builder.ToString());
                    break;
                case "5":
                    string[] list = queryArrays.Split(new string[] { "_bigsplit_" }, StringSplitOptions.None);
                    string imlist = string.Empty;
                    string linklist = string.Empty;
                    for (int i = 0; i < list.Length; i++)
                    {
                        builder.Append("var imgUrl_" + i + "=" + "'/" + list[i].Split(new string[] { "_smallsplit_" }, StringSplitOptions.None)[0] + "';");
                        builder.Append("var imgLink_" + i + "=escape('" + islinks(list[i].Split(new string[] { "_smallsplit_" }, StringSplitOptions.None)[1]) + "');");
                    }

                    for (int i = 0; i < list.Length; i++)
                    {
                        if (list.Length == (i + 1))
                        {
                            imlist += "imgUrl_" + i;
                            linklist += "imgLink_" + i;
                        }
                        else
                        {
                            imlist += "imgUrl_" + i + "+\"|\"+";
                            linklist += "imgLink_" + i + "+\"|\"+";
                        }

                    }

                    builder.Append(" var pics=" + imlist + "; var links=" + linklist + ";");


                    builder.Append("document.write('<embed menu=\"false\" type=\"application/x-shockwave-flash\"  src=\"/AdShows/flash/focus.swf\"  pluginspage=\"http://fpdownload.macromedia.com/pub/shockwave/cabs/flash/swflash.cab#version=6,0,0,0\"  width=\"" + width + "\" height=\"" + height + "\" wmode=\"transparent\"  play=\"true\" loop=\"false\" menu=\"false\"  flashvars=\"pics=' + pics + '&links=' + links + '&borderwidth=" + width + "&borderheight=" + height + "\"  quality=\"high\"  allowscriptaccess=\"sameDomain\" allowfullscreen=\"true\"/>');");

                    File.WriteAllText(HttpContext.Current.Server.MapPath("~/AdShows/") + guid + ".js", builder.ToString());
                    break;

            }

        }
        public static string islinks(string urls)
        {
            if (urls == "描述或链接" || urls == "")
                return string.Empty;
            else
                return urls;

        }
        /// <summary>
        /// 多行文本
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetMutiTxtShowStyle(ModelFiled model)
        {

            string width = WebUtility.GetFieldContent(model.Content, 0, 1) + "px";
            string height = WebUtility.GetFieldContent(model.Content, 1, 1) + "px";
            return "<textarea id=\"customtxt_" + model.FiledName + "\"   name=\"customtxt_" + model.FiledName + "\"  cols=\"20\" rows=\"2\" style=\"width:" + width + ";height:" + height + " \"></textarea>&nbsp;<span class=\"prompttext\">" + model.Description + "</span>";
        }
        /// <summary>
        /// 下拉框 三种情况 1 2 3
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetSelectListShowStyle(ModelFiled model)
        {

            string types = WebUtility.GetFieldContent(model.Content, 0);
            string values = WebUtility.GetFieldContent(model.Content, 1);
            if (types == "1")
            {
                string selectstr = "<select id=\"customtxt_" + model.FiledName + "\"  name=\"customtxt_" + model.FiledName + "\">";
                string[] option = values.Split('|');
                for (int i = 0; i < option.Length; i++)
                {
                    selectstr += "<option value=\"" + option[i] + "\">" + option[i] + "</option>";
                }
                selectstr += "</select>&nbsp;&nbsp;<span class=\"prompttext\">" + model.Description + "</span>";

                return selectstr;
            }
            else if (types == "2")
            {

                string selectstr = null;
                string[] option = values.Split('|');
                for (int i = 0; i < option.Length; i++)
                {
                    selectstr += "<input id=\"customtxt_" + model.FiledName + "_" + i + "\" type=\"radio\" name=\"customtxt_" + model.FiledName + "\" value=\"" + option[i] + "\"/><label for=\"customtxt_" + model.FiledName + "_" + i + "\">" + option[i] + "</label>";
                }
                selectstr += "&nbsp;&nbsp;<span class=\"prompttext\">" + model.Description + "</span>";

                return selectstr;

            }
            else
            {

                string selectstr = null;
                string[] option = values.Split('|');
                for (int i = 0; i < option.Length; i++)
                {
                    selectstr += "<input id=\"customtxt_" + model.FiledName + "_" + i + "\" type=\"checkbox\"  name=\"customtxt_" + model.FiledName + "\" value=\"" + option[i] + "\"/><label for=\"customtxt_" + model.FiledName + "_" + i + "\">" + option[i] + "</label>";
                }
                selectstr += "&nbsp;&nbsp;<span class=\"prompttext\">" + model.Description + "</span>";

                return selectstr;

            }

        }


        /// <summary>
        /// 数据字典02 PID=100,filedname=abc,name=娱乐频道
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetDictionary2LinkageStyle(ModelFiled model)
        {

            string twofiledname = WebUtility.GetFieldContent(model.Content, 1, 1);
            string values = WebUtility.GetFieldContent(model.Content, 0, 1);
            string selectstr = null;
            selectstr += "<span id=\"customtxt_" + model.FiledName + "_span\">{$" + model.FiledName + "$}</span>&nbsp;&nbsp;<span class=\"prompttext\">" + model.Description + "</span>&nbsp;&nbsp;<a  attrs=\"func" + model.FiledName + "\"  rel=\"adddictionaryManage\" href=\"diaologdictionaryManage.aspx?Type=" + values + "&action=2\">管理类别</a><script type=\"text/javascript\"> function  func" + model.FiledName + "() { GetDictionary2(\"customtxt_" + model.FiledName + "\",\"customtxt_" + twofiledname + "\",\"" + values + "\");}</script>";
            return selectstr;

        }
        /// <summary>
        /// 数据字典01 PID=101,type=2,name=电脑
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetDictionary1LinkageStyle(ModelFiled model)
        {

            string types = WebUtility.GetFieldContent(model.Content, 1, 1);
            string values = WebUtility.GetFieldContent(model.Content, 0, 1);
            string selectstr = null;
            selectstr += "<span id=\"customtxt_" + model.FiledName + "_span\">{$" + model.FiledName + "$}</span>&nbsp;&nbsp;<span class=\"prompttext\">" + model.Description + "</span>&nbsp;&nbsp;<a attrs=\"func" + model.FiledName + "\"  rel=\"adddictionaryManage\" href=\"diaologdictionaryManage.aspx?Type=" + values + "&action=1\">管理类别</a><script type=\"text/javascript\"> function func" + model.FiledName + "(){ GetDictionary1(\"customtxt_" + model.FiledName + "\",\"" + values + "\",\"" + types + "\");}</script>";
            return selectstr;

        }
        /// <summary>
        /// 编辑器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GetEditorTxtShowStyle(ModelFiled model)
        {

            string width = WebUtility.GetFieldContent(model.Content, 0, 1);
            string height = WebUtility.GetFieldContent(model.Content, 1, 1);

            StringBuilder builder = new StringBuilder();
            string str = null;
            if (!string.IsNullOrEmpty(model.Description))
            {
                str = " padding-top:10px; padding-bottom:12px;line-height:200%;";
            }
            builder.Append("&nbsp;&nbsp;<span class=\"prompttext\"  style=\"" + str + "\">" + model.Description + "</span><textarea id=\"customtxt_" + model.FiledName + "\" name=\"customtxt_" + model.FiledName + "\"></textarea><script type=\"text/javascript\"> var editorOption = {initialFrameWidth:" + width + ",initialFrameHeight:" + height + " };var ue = new UE.ui.Editor(editorOption);ue.render(\"customtxt_" + model.FiledName + "\");</script><br/><br/>");


            return builder.ToString();

        }
        /// <summary>
        /// eWebEditor编辑器
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        public static string GeteWebEditorTxtShowStyle(ModelFiled model)
        {

            string width = WebUtility.GetFieldContent(model.Content, 0, 1);
            string height = WebUtility.GetFieldContent(model.Content, 1, 1);

            StringBuilder builder = new StringBuilder();
            string str = null;
            if (!string.IsNullOrEmpty(model.Description))
            {
                str = "display:inline";
            }
            builder.Append("<input style=\"display:inline\" type=\"button\" value=\"插入图片\" onclick=\"OpenewebEditorDialog('customtxt_" + model.FiledName + "')\" class=\"btn2\" />&nbsp;&nbsp;<span class=\"prompttext\"  style=\"" + str + "\">" + model.Description + "</span><input id=\"customtxt_" + model.FiledName + "Content\"  name=\"customtxt_" + model.FiledName + "Content\" type=\"hidden\" /><iframe style=\"display:block\" id=\"customtxt_" + model.FiledName + "\" src=\"htmlEditor/ewebeditor/ewebeditor.htm?id=customtxt_" + model.FiledName + "Content&style=mini500&skin=light1\" frameborder=\"0\" scrolling=\"no\" width=\"" + width + "\" height=\"" + height + "\"></iframe><br/><br/>");


            return builder.ToString();

        }
        
        /// <summary>
        /// 获取网站真实路径
        /// </summary>
        /// <returns></returns>
        /// 
        public static string GetTrueVirtualPath()
        {
            string forumPath = HttpContext.Current.Request.Path;
            if (forumPath.LastIndexOf("/") != forumPath.IndexOf("/"))
            {
                forumPath = forumPath.Substring(forumPath.IndexOf("/"), forumPath.LastIndexOf("/") + 1);
            }
            else
            {
                forumPath = "/";
            }
            return forumPath;
        }
        /// <summary>
        /// 通过判断是否使用代理获取用户的真实IP地址
        /// </summary>
        /// <returns></returns>
        public static string GetTrueIp()
        {
            string result = String.Empty;

            result = HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
            if (result != null && result != String.Empty)
            {
                //可能有代理 //没有“.”肯定是非IPv4格式 
                if (result.IndexOf(".") == -1)
                    result = null;
                else
                {
                    if (result.IndexOf(",") != -1)
                    {
                        //有“,”，估计多个代理。取第一个不是内网的IP。 
                        result = result.Replace(" ", "").Replace("'", "");
                        string[] temparyip = result.Split(",;".ToCharArray());
                        for (int i = 0; i < temparyip.Length; i++)
                        {
                            if (IsIPAddress(temparyip[i])
                                && temparyip[i].Substring(0, 3) != "10."
                                && temparyip[i].Substring(0, 7) != "192.168"
                                && temparyip[i].Substring(0, 7) != "172.16.")
                            {
                                //找到不是内网的地址 
                                return temparyip[i];
                            }
                        }
                    }
                    else if (IsIPAddress(result))
                        //代理即是IP格式
                        return result;
                    else
                        //代理中的内容 非IP，取IP 
                        result = null;
                }

            }

            string IpAddress = (
                HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != null &&
                HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] != String.Empty
            ) ? HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] : HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (null == result || result == String.Empty)
                result = HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];

            if (result == null || result == String.Empty)
                result = HttpContext.Current.Request.UserHostAddress;

            return result;
        }
        /// <summary>
        /// 通过正则判断字符串是否为IP地址
        /// </summary>
        /// <param name="str1"></param>
        /// <returns></returns>
        private static bool IsIPAddress(string str1)
        {
            if (str1 == null || str1 == string.Empty || str1.Length < 7 || str1.Length > 15) return false;

            string regformat = @"^\d{1,3}[\.]\d{1,3}[\.]\d{1,3}[\.]\d{1,3}$";

            Regex regex = new Regex(regformat, RegexOptions.IgnoreCase);
            return regex.IsMatch(str1);
        }
        /// <summary>
        /// 获取一个文件夹的大小
        /// </summary>
        /// <param name="path">文件夹路径</param>
        /// <returns></returns>
        public static long FolderSize(string path)
        {
            long Fsize = 0;
            try
            {
                Fsize = FolderFileSize(path);
                DirectoryInfo[] folders = (new DirectoryInfo(path)).GetDirectories();
                foreach (DirectoryInfo folder in folders)
                {
                    Fsize += FolderSize(folder.FullName);
                }
            }
            catch
            {
                Fsize = 0;
            }
            return Fsize;
        }
        /// <summary>
        /// 获取一个文件下文件的大小
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public static long FolderFileSize(string path)
        {
            long size = 0;
            try
            {
                FileInfo[] files = (new DirectoryInfo(path)).GetFiles();
                foreach (FileInfo file in files)
                {
                    size += file.Length;
                }
            }
            catch
            {
                size = 0;
            }
            return size;
        }
        /// Make分隔符
        /// </summary>
        /// <param name="count"></param>
        /// <returns></returns>
        public static string MakeSplit(int count)
        {
            string Returnwords = string.Empty;
            if (count == 0)
            {
                Returnwords = "├";
            }
            else
            {
                Returnwords = ("│├").PadLeft(count + 1, '│');
            }
            return Returnwords;
        }
        /// <summary> 
        /// MD5 加密函数 
        /// </summary> 
        /// <param name="str"></param> 
        /// <param name="code"></param> 
        /// <returns></returns> 
        public static string MD5(string str)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(str, "MD5");
        }
        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="code">明文</param>
        /// <returns>密文</returns>
        public static string EncodeBase64(string code)
        {
            string encode = "";
            //try
            //{
            byte[] bytes = Encoding.GetEncoding(Encoding.UTF8.CodePage).GetBytes(code);
            encode = Convert.ToBase64String(bytes);
            //}
            //catch
            //{
            //    encode = code;
            //}
            return encode;
        }
        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="code">密文</param>
        /// <returns>明文</returns>
        public static string DecodeBase64(string code)
        {
            string decode = "";
            try
            {
                byte[] bytes = Convert.FromBase64String(code);
                decode = Encoding.GetEncoding(Encoding.UTF8.CodePage).GetString(bytes);
            }
            catch
            {
                decode = code;
            }
            return decode;
        }
    }
    /// <summary>
    /// 字符 与 十六进制转换
    /// </summary>
    public static class HexConvert
    {
        /// <summary>
        /// <函数：Encode>
        /// 作用：将字符串内容转化为16进制数据编码，其逆过程是Decode
        /// 参数说明：
        /// strEncode 需要转化的原始字符串
        /// 转换的过程是直接把字符转换成Unicode字符,比如数字"3"-->0033,汉字"我"-->U+6211
        /// 函数decode的过程是encode的逆过程.
        /// </summary>
        /// <param name="strEncode"></param>
        /// <returns></returns>
        public static string Encode(string strEncode)
        {
            string strReturn = "";//  存储转换后的编码
            foreach (short shortx in strEncode.ToCharArray())
            {
                strReturn += shortx.ToString("X4");
            }
            return strReturn;
        }

        /// <summary>
        /// <函数：Decode>
        ///作用：将16进制数据编码转化为字符串，是Encode的逆过程
        /// </summary>
        /// <param name="strDecode"></param>
        /// <returns></returns>
        public static string Decode(string strDecode)
        {
            string sResult = "";
            for (int i = 0; i < strDecode.Length / 4; i++)
            {
                sResult += (char)short.Parse(strDecode.Substring(i * 4, 4), global::System.Globalization.NumberStyles.HexNumber);
            }
            return sResult;
        }

        /// <summary>
        /// 生成16进制的
        /// </summary>
        /// <param name="aryBiye"></param>
        /// <returns></returns>
        public static string EncodeBytes(byte[] aryBiye)
        {
            string strReturn = "A";//  存储转换后的编码
            foreach (byte shortx in aryBiye)
            {
                strReturn += "," + shortx.ToString();
            }
            strReturn = strReturn.Replace("A,", "");
            strReturn = strReturn.Replace("A", "");
            return strReturn;
        }

        /// <summary>
        /// <函数：Decode>
        ///作用：将16进制数据编码转化为字符串，是Encode的逆过程
        /// </summary>
        /// <param name="strDecode"></param>
        /// <returns></returns>
        public static byte[] DecodeBytes(string strDecode)
        {
            byte[] ret = null;
            string[] spStr = strDecode.Split(',');

            ret = new byte[spStr.Length];
            for (int i = 0; i < spStr.Length; i++)
            {
                ret[i] = (byte)Convert.ToInt16(spStr[i]);
            }
            return ret;
        }
    }
}

