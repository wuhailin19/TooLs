using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tools
{
    public class WebUtility
    {
        /// <summary>
        /// 检查SQL参数中是否带有("'","[")
        /// </summary>
        /// <param name="str">传入SQL</param>
        /// <returns></returns>
        public static string CheckStr(object str)
        {
            string ret = "";
            try
            {
                if (str == null) ret = "";
                else
                {
                    ret = str.ToString();
                    ret = ret.Replace("'", "''");
                }
            }
            catch
            {
                ret = "";
            }
            return ret;
        }
        /// <summary>
        ///    内容"width=10,height=20"窃取具体值
        /// </summary>
        /// <param name="Content">内容"width=10,height=20"</param>
        /// <param name="PlaceId">0</param>
        /// <param name="TypeId">1</param>
        /// <returns></returns>
        public static string GetFieldContent(string Content, int PlaceId, int TypeId)
        {
            string x = Content.Split(new char[] { ',' })[PlaceId].Split(new char[] { '=' })[TypeId].ToString();
            return Content.Split(new char[] { ',' })[PlaceId].Split(new char[] { '=' })[TypeId].ToString();
        }
        /// <summary>
        ///    内容"width=10"窃取具体值
        /// </summary>
        /// <param name="Content">内容"width=10,height=20"</param>
        /// <param name="PlaceId">0</param>
        /// <param name="TypeId">1</param>
        /// <returns></returns>
        public static string GetFieldContent(string Content, int TypeId)
        {
            return Content.Split(new char[] { '=' })[TypeId].ToString();
        }
        /// <summary>
        ///    _mysplit_
        /// </summary>
        /// <param name="Content">内容"width=10,height=20"</param>
        /// <param name="PlaceId">0</param>
        /// <param name="TypeId">1</param>
        /// <returns></returns>
        public static string GetmysplitContent(string Content, int PlaceNum)
        {
            return Content.Split(new string[] { "_mysplit_" }, StringSplitOptions.None)[PlaceNum];
        }
    }
}
