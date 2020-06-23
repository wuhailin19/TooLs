using System.Text;
using System.Text.RegularExpressions;

namespace Tools
{
    public class CSharpFormatHelper
    {
        //remove empty lines from string
        public static string RemoveEmptyLines(string lines)
        {
            return Regex.Replace(lines, @"^\s*$\n|\r", "", RegexOptions.Multiline).TrimEnd();
        }
        //Indent String with Spaces
        public static string Indent(int count)
        {
            if (count <= 0)
            {
                return "";
            }
            else
            {
                return "    ".PadLeft(count * 2);
            }
        }

        //格式化C#代码
        public static string FormatCSharpCode(string code)
        {
            //去除空白行
            code = RemoveEmptyLines(code);
            StringBuilder sb = new StringBuilder();
            int count = 2;
            int times = 0;
            string[] lines = code.Split('\n');
            for (int i = 0; i < lines.Length; i++)
            {
                if (lines[i].TrimStart().StartsWith("{") || lines[i].TrimEnd().EndsWith("{"))
                {
                    sb.Append(Indent(count * times) + lines[i].TrimStart() + "\n");
                    times++;
                }
                else if (lines[i].TrimStart().StartsWith("}"))
                {
                    times--;
                    if (times <= 0)
                    {
                        times = 0;
                    }
                    sb.Append(Indent(count * times) + lines[i].TrimStart() + "\n");
                }
                else
                {
                    sb.Append(Indent(count * times) + lines[i].TrimStart() + "\n");
                }
            }
            return sb.ToString();
        }
        //格式化Html代码
        public static string FormatHtmlCode(string code)
        {
            //去除空白行
            code = RemoveEmptyLines(code);
            StringBuilder sb = new StringBuilder();
            int count = 2;
            int times = 0;
            string[] lines = code.Split('\n').Length>1? code.Split('\n'): code.Split('\r');
            string[] doms = { "head ", "body", "asp:repeater", "section","script", "div", "ul", "ol", "header", "asp:content " };
            string linestr = null;
            for (int i = 0; i < lines.Length; i++)
            {
                foreach (var dom in doms)
                {
                    if (lines[i].ToLower().TrimStart().StartsWith($"<{dom}")
                        && lines[i].ToLower().TrimStart().EndsWith($"</{dom}>"))
                    {
                        linestr = Indent(count * times) + lines[i].TrimStart() + "\n";
                        break;
                    }
                    else if (lines[i].ToLower().TrimStart().StartsWith($"<{dom}")
                        && !lines[i].ToLower().TrimStart().EndsWith($"</{dom}>"))
                    {
                        linestr = Indent(count * times) + lines[i].TrimStart() + "\n";
                        times++;
                        break;
                    }
                    else if (lines[i].ToLower().TrimStart().StartsWith($"</{dom.TrimEnd()}>")
                        || lines[i].ToLower().TrimStart().EndsWith($"</{dom.TrimEnd()}>"))
                    {
                        times--;
                        int domlength = $"</{dom.TrimEnd()}>".Length;
                        if (times <= 0)
                        {
                            times = 0;
                        }
                        if (lines[i].ToLower().TrimStart().EndsWith($"</{dom.TrimEnd()}>") && lines[i].ToLower().TrimStart().Length > $"</{dom.TrimEnd()}>".Length)
                        {
                            linestr = Indent(count * times) + ReplaceLastStr(lines[i].TrimEnd(), $"</{dom.TrimEnd()}>", $"\n{Indent(count * times)}</{dom.TrimEnd()}>") + "\n";
                        }
                        else
                        {
                            linestr = Indent(count * times) + lines[i].TrimStart() + "\n";
                        }
                        break;
                    }
                    else
                    {
                        linestr = Indent(count * times) + lines[i].TrimStart() + "\n";
                    }
                }
                sb.Append(linestr);
            }
            return sb.ToString();
        }
        /// <summary>
        /// 替换最后一个元素
        /// </summary>
        /// <param name="str">总字符串</param>
        /// <param name="str1">需替换字符串str1</param>
        /// <param name="str2">替换为str2</param>
        /// <returns></returns>
        static string ReplaceLastStr(string str, string str1, string str2)
        {
            str = str.Remove(str.Length - str1.Length);
            str = str + str2;

            return str;
        }
    }
}


