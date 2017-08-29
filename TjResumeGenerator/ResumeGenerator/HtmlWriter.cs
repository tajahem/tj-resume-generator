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
        int tabLevel = 0;

        public void WriteBeginTag(string tag)
        {
            html.Append(GetTabs() + "<" + tag + ">" + Environment.NewLine);
            tags.Push(tag);
            ++tabLevel; 
        }

        public void WriteBeginTag(string tag, string tagClass)
        {
            html.Append(GetTabs() + "<" + tag + " class=\"" + tagClass + "\">" + Environment.NewLine);
            tags.Push(tag);
            ++tabLevel;
        }

        public void WriteSingleLineTag(string tag, string content)
        {
            html.Append(GetTabs() + "<" + tag + ">" + content + 
                        "</" + tag + ">" + Environment.NewLine);
        }

        public void WriteSingleLineTag(string tag, string tagClass, string content)
        {
            html.Append(GetTabs()+ "<" + tag + " class=\"" + tagClass + "\">" + content 
                        + "</" + tag + ">" + Environment.NewLine);
        }

        public void WriteBeginTagWithId(string tag, string id)
        {
            html.Append(GetTabs()+ "<" + tag + " id=\"" + id + "\">" + Environment.NewLine);
            tags.Push(tag);
            ++tabLevel;
        }

        public void WriteLink(string url, string text)
        {
            html.Append(GetTabs() + "<a href=\"" + url + "\">" + text + "</a>" +
                        Environment.NewLine);
        }

        public void WriteInlineLink(string url, string text)
        {
            html.Append("<a href=\"" + url + "\">" + text + "</a>");

        }

        public void WriteContent(string text)
        {
            html.Append(text);
        }

        public void WriteTabs()
        {
            html.Append(GetTabs());
        }

        public void WriteNewLine()
        {
            html.Append(Environment.NewLine);
        }

        public void WriteContentOnNewline(string text){
            html.Append(Environment.NewLine + GetTabs() + text + Environment.NewLine);
        }

        public void WriteNextEnd()
        {
            --tabLevel;
            html.Append(GetTabs() + "</" + tags.Pop() + ">" + Environment.NewLine);
        }

        public void WriteAllEnds()
        {
            while (tags.Count > 0){
                WriteNextEnd();
            }
        }

        public string GetHtml()
        {
            return html.ToString();
        }

        string GetTabs(){
            StringBuilder t = new StringBuilder();
            for (int i = 0; i < tabLevel; i++){
                t.Append("\t");
            }
            return t.ToString();
        }
    }
}
