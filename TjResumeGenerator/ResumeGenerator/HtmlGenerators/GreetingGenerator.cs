using System;
using System.Xml;
using System.IO;

namespace ResumeGenerator
{
    public class GreetingGenerator : HtmlGenerator
    {
        public GreetingGenerator(string dirName) : base(ReplacementTags.GREETING_TAG)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(DocumentNames.GetPath(dirName, DocumentNames.GREETING_DOC));
            XmlNode node = doc.SelectSingleNode("/greeting");

            writer = new HtmlWriter();

            writer.WriteBeginTagWithId("div", "greeting");
            WriteUl(node);
            WriteSalutation(node);
            writer.WriteAllEnds();

            html = writer.GetHtml();

        }

        HtmlWriter writer;

        void WriteUl(XmlNode n)
        {
            writer.WriteBeginTag("ul");
            writer.WriteSingleLineTag("li", n["name"].InnerText);
            if (n["us-address"] != null)
            {
                WriteUSAddress(n.SelectSingleNode("/greeting/us-address"));
            }
            writer.WriteNextEnd();
        }

        void WriteUSAddress(XmlNode n)
        {
            writer.WriteSingleLineTag("li", n["street"].InnerText);
            writer.WriteSingleLineTag("li",
                                      n["city"].InnerText + ", " +
                                      n["state"].InnerText + " " + n["zipcode"].InnerText);
        }

        void WriteSalutation(XmlNode n)
        {
            writer.WriteBeginTagWithId("div", "salutation");
            writer.WriteTabs();
            writer.WriteContent(n["salutation"].InnerText);
            writer.WriteNewLine();
            writer.WriteNextEnd();
        }
    }
}

