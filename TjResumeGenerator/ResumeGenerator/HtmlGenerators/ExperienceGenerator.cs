using System;
using System.Xml;

namespace ResumeGenerator
{
	public class ExperienceGenerator : HtmlGenerator
	{
		public ExperienceGenerator (string directory) : base(ReplacementTags.XP_TAG)
		{
            writer.WriteBeginTagWithId("div", "id");
            GenerateJobs(directory);

            writer.WriteAllEnds();
            html = writer.GetHtml();
		}

        HtmlWriter writer = new HtmlWriter();

        private void GenerateJobs(string directory)
        {
			XmlDocument doc = new XmlDocument();
			doc.Load(DocumentNames.GetPath(directory, DocumentNames.XP_DOC));

            XmlNodeList list = doc.SelectNodes("/xp/job");
            foreach(XmlNode node in list){
                writer.WriteBeginTag("div", "job");
                writer.WriteBeginTag("div", "job-header");

                WriteDiv(node, "title");
                WriteDiv(node, "company");

                if(node["dates"].InnerText != null){
                    WriteDiv(node, "dates");
                }
                writer.WriteNextEnd(); // end header

                WritePoints(node);
                writer.WriteNextEnd(); //end job
            }
		}

        void WriteDiv(XmlNode node, string name)
        {
			writer.WriteBeginTag("div", "job-" + name);
			writer.WriteContent(node[name].InnerText);
			writer.WriteNextEnd();
        }

		void WritePoints(XmlNode node)
		{
			XmlNodeList points = node.SelectNodes("point");

			if (points.Count > 0)
			{
				writer.WriteBeginTag("ul");
				foreach (XmlNode p in points)
				{
					writer.WriteSingleLineTag("li");
					writer.WriteContent(p.InnerText);
					writer.WriteNextEnd();
				}
				writer.WriteNextEnd(); // end ul
			}
		}

	}
}