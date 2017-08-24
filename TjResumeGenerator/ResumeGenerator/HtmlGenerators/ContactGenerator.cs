using System;
using System.Xml;
using System.Text.RegularExpressions;

namespace ResumeGenerator
{
    /// <summary>
    /// Html Contact section generator.
    /// </summary>
    public class ContactGenerator : HtmlGenerator
    {
        
        public ContactGenerator(string directory, bool verifyEmail) : base(ReplacementTags.CONTACT_TAG)
        {
			XmlDocument doc = new XmlDocument();
			doc.Load(DocumentNames.GetPath(directory, DocumentNames.CONTACT_DOC));
            WriteDocument(doc, verifyEmail);
        }

        public ContactGenerator(string document) : base(ReplacementTags.CONTACT_TAG)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(document);
            WriteDocument(doc, true);
        }

        void WriteDocument(XmlDocument doc, bool verifyEmail)
        {
			writer.WriteBeginTag("div", "contact");
			writer.WriteBeginTag("ul");

			XmlNodeList list = doc.FirstChild.ChildNodes;
			foreach (XmlNode n in list)
			{
                GenerateLine(n, verifyEmail);
            }

            writer.WriteAllEnds();
            html = writer.GetHtml();
		}

        HtmlWriter writer = new HtmlWriter();

		// email regex courtesy of http://emailregex.com/
		const string EMAIL_REGEX = 
            "^(?(\")(\".+?(?<!\\\\)\"@)|(([0-9a-z]((\\.(?!\\.))|[-!#\\$%&'\\*\\+/=\\?\\^`\\{\\}" +
            "\\|~\\w])*)(?<=[0-9a-z])@))(?(\\[)(\\[(\\d{1,3}\\.){3}\\d{1,3}\\])|(([0-9a-z][-\\w]" +
            "*[0-9a-z]*\\.)+[a-z0-9][\\-a-z0-9]{0,22}[a-z0-9]))$";

        public const string INVALID_EMAIL_MESSAGE = "Email in contact.xml is invalid";

        void GenerateLine(XmlNode node, bool verifyEmail)
        {
            switch (node.Name)
            {
                case "phone":
                    InsertListItem(node["type"].InnerText + " : " + node["number"].InnerText);
                    break;
                case "us-address":
                    HandleUSAddress(node);
                    break;
                case "email" :
                    if(verifyEmail){
                        Regex r = new Regex(EMAIL_REGEX);
                        if(!r.IsMatch(node.InnerText)){
                            throw new Exception(INVALID_EMAIL_MESSAGE);
                        }
                    }
                    InsertListItem(node.InnerText);
                    break;
                case "other" :
                    InsertListItem(node.InnerText);
                    break;
            }
        }

        void InsertListItem(string text)
        {
            writer.WriteSingleLineTag("li");
            writer.WriteContent(text);
            writer.WriteNextEnd();
        }

        void HandleUSAddress(XmlNode node)
        {
			if (node["street"] != null)
			{
				InsertListItem(node["street"].InnerText);
			}
			if (node["po-box"] != null)
			{
				InsertListItem(node["po-box"].InnerText);
			}
			InsertListItem(node["city"].InnerText + ", " + node["state"].InnerText + " " +
										 node["zipcode"].InnerText);
        }
    }
}

