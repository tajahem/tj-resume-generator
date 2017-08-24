using System;
using System.Xml;

namespace ResumeGenerator
{
	public class LinksGenerator : HtmlGenerator
	{
		public LinksGenerator(string dirName) : base(ReplacementTags.LINKS_TAG)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(DocumentNames.GetPath(dirName, DocumentNames.LINKS_DOC));

            HtmlWriter writer = new HtmlWriter();

            writer.WriteBeginTagWithId("div", "links-section");


            XmlNodeList links = doc.SelectNodes("/links/link");
            foreach(XmlNode n in links){
                writer.WriteLink(n["url"].InnerText, n["text"].InnerText);
            }

            writer.WriteNextEnd();

            html = writer.GetHtml();
		}
	}
}

