using System;
using System.Collections.Generic;
using System.Text;

namespace ResumeGenerator
{
    public class HtmlWriter
    {
        public HtmlWriter()
        {
        }

        StringBuilder html = new StringBuilder();
        Stack<string> tags = new Stack<string>();


        public void WriteBeginTag(string tag)
        {
            html.Append("<" + tag + ">\n");
            tags.Push(tag);
        }

        public void WriteBeginTag(string tag, string tagClass)
        {
            html.Append("<" + tag + " class=\"" + tagClass + "\">\n");
            tags.Push(tag);
        }

        public void WriteSingleLineTag(string tag)
        {
			html.Append("<" + tag + ">");
            tags.Push(tag);
        }

        public void WriteSingleLineTag(string tag, string tagClass)
        {
			html.Append("<" + tag + " class=\"" + tagClass + "\">");
            tags.Push(tag);
        }

        public void WriteBeginTagWithId(string tag, string id)
        {
			html.Append("<" + tag + " id=\"" + id + "\">\n");
            tags.Push(tag);
        }

        public void WriteLink(string url, string text)
        {
            html.Append("<a href=\"" + url + "\">" + text + "</a>");
        }

        public void WriteContent(string text)
        {
            html.Append(text);
        }

        public void WriteNextEnd()
        {
            html.Append("</" + tags.Pop() + ">\n");
        }

        public void WriteAllEnds()
        {
           while (tags.Count > 0){
                html.Append("</" + tags.Pop() + ">\n");
            }
        }

        public string GetHtml()
        {
            return html.ToString();
        }
    }
}
