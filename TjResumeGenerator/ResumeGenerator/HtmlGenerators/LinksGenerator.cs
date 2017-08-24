using System;
using System.Xml;

namespace ResumeGenerator
{
	public class LinksGenerator : HtmlGenerator
	{
		public LinksGenerator(string dirName) : base(ReplacementTags.LINKS_TAG)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(dirName + FILE_NAME);

            base.html = "<div id=\"links-section\">\n";

            XmlNodeList links = doc.SelectNodes("/links/link");
            foreach(XmlNode n in links){
                base.html += "<a href=\"" + n["url"].InnerText + "\">" + n["text"].InnerText + "</a>";
            }

            base.html += "</div>";
		}

        private const string FILE_NAME = "/links.xml";
	}
}

