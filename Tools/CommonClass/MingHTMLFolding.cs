using ICSharpCode.TextEditor.Document;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tools
{
    /// <summary>
    /// The class to generate the foldings, it implements ICSharpCode.TextEditor.Document.IFoldingStrategy
    /// </summary>
    public class MingHTMLFolding : IFoldingStrategy
    {
        /// <summary>
        /// Generates the foldings for our document.
        /// </summary>
        /// <param name="document">The current document.</param>
        /// <param name="fileName">The filename of the document.</param>
        /// <param name="parseInformation">Extra parse information, not used in this sample.</param>
        /// <returns>A list of FoldMarkers.</returns>
        public List<FoldMarker> GenerateFoldMarkers(IDocument document, string fileName, object parseInformation)
        {
            List<FoldMarker> list = new List<FoldMarker>();
            //需要分开
            //int start = 0;
            //int start2 = 0;
            //int start3 = 0;
            //int start4 = 0;

            Dictionary<string, string> startdoms = new Dictionary<string, string>();
            startdoms.Add("<head>", "</head>");
            startdoms.Add("<head runat=\"server\">", "</head>");
            startdoms.Add("<html", "</html>");
            startdoms.Add("<div", "</div>");
            startdoms.Add("<body", "</body>");
            startdoms.Add("<footer", "</footer>");
            startdoms.Add("<section", "</section>");
            startdoms.Add("<header", "</header>");
            startdoms.Add("#region", "#endregion");
            startdoms.Add("<asp:repeater", "</asp:repeater>");

            //int start_script = 0;
            //stack 先进先出
            var startLines = new Stack<int>();

            // Create foldmarkers for the whole document, enumerate through every line.
            for (int i = 1; i < document.TotalNumberOfLines - 1; i++)
            {
                // Get the text of current line.
                string text = document.GetText(document.GetLineSegment(i));
                int start = 0;
                foreach (string key in startdoms.Keys)
                {
                    if (text.Trim().ToLower().StartsWith(key)) // Look for method starts
                    {
                        startLines.Push(i);
                        break;
                    }
                    else if (text.Trim().ToLower().StartsWith(startdoms[key]) && startLines.Count > 0) // Look for method endings
                    {
                        start = startLines.Pop();
                        list.Add(new FoldMarker(document, start, document.GetLineSegment(start).Length, i, 7));
                        break;
                    }
                    else { continue; }
                }

                //支持嵌套 {}
                if (text.Trim().StartsWith("{") || text.Trim().EndsWith("{")) // Look for method starts
                {
                    startLines.Push(i);
                }
                if (text.Trim().StartsWith("}")) // Look for method endings
                {
                    if (startLines.Count > 0)
                    {
                        int start0 = startLines.Pop();
                        list.Add(new FoldMarker(document, start0, document.GetLineSegment(start0).Length, i, 57, FoldType.TypeBody, "...}"));
                    }
                }
                // /// <summary>
                if (text.Trim().StartsWith("/// <summary>")) // Look for method starts
                {
                    startLines.Push(i);
                }
                if (text.Trim().StartsWith("/// <returns>")) // Look for method endings
                {

                    int start1 = startLines.Pop();
                    //获取注释文本（包括空格）
                    string display = document.GetText(document.GetLineSegment(start1 + 1).Offset, document.GetLineSegment(start1 + 1).Length);
                    //remove ///
                    display = display.Trim().TrimStart('/');
                    list.Add(new FoldMarker(document, start1, document.GetLineSegment(start1).Length, i, 57, FoldType.TypeBody, display));
                }
            }
            return list;
        }
    }
}
