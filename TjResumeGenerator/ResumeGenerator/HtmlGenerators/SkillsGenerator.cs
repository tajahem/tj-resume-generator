using System;
using System.Xml;
using System.Collections;
using System.Collections.Generic;

namespace ResumeGenerator
{
	public class SkillsGenerator : HtmlGenerator
	{

		public SkillsGenerator (string dirName) : base(ReplacementTags.SKILLS_TAG)
		{
            writer = new HtmlWriter();
            writer.WriteBeginTagWithId("div", "skills");
            XmlDocument doc = new XmlDocument();
            doc.Load(DocumentNames.GetPath(dirName, DocumentNames.SKILLS_DOC));
            GenerateSkills(doc.SelectNodes("/skills/skill"));
            writer.WriteAllEnds();
            html = writer.GetHtml();
		}

        HtmlWriter writer;
        Dictionary<string, SkillCategory> categories = new Dictionary<string, SkillCategory>();

        public void GenerateSkills(XmlNodeList nodes)
        {
            foreach (XmlNode n in nodes){
                Skill tmp = new Skill(n);
                if(categories.ContainsKey(tmp.category)){
                    categories[tmp.category].AddSkill(tmp);
                }else{
                    categories.Add(tmp.category, new SkillCategory(tmp.category));
                    categories[tmp.category].AddSkill(tmp);
                }
            }
            foreach(SkillCategory category in categories.Values){
                category.WriteCategory(writer);
            }
        }

        class SkillCategory
        {
            public SkillCategory(string name)
            {
                this.name = name;
            }

            public string name;
            ArrayList skills = new ArrayList();

            public void AddSkill(Skill skill)
            {
                skills.Add(skill);
            }

            public void WriteCategory(HtmlWriter writer)
            {
                writer.WriteBeginTag("div", "skill-category");
                writer.WriteSingleLineTag("div", "skill-category-title", name);
                foreach(Skill s in skills){
                    s.WriteSkill(writer); 
                }
                writer.WriteNextEnd();
            }
        }// end skill category

        class Skill
        {
            public Skill(XmlNode data)
            {
                this.data = data;
                category = data["category"].InnerText;
            }

            public string category;
            XmlNode data;

            public void WriteSkill(HtmlWriter writer)
            {
                writer.WriteBeginTag("div", "skill");
                writer.WriteSingleLineTag("div", "skill-name", data["name"].InnerText);
                if(data["percentage"] != null){
                    writer.WriteBeginTag("div", "skill-percentage-bar");
                    writer.WriteTabs();
                    writer.WriteContent("<div style=\"width: "+ data["percentage"].InnerText +
                                        ";\"></div>");
                    writer.WriteNewLine();
                    writer.WriteNextEnd();
                }
                if(data["mastery"] != null){
                    writer.WriteSingleLineTag("div", "skill-mastery", data["mastery"].InnerText);
                }
                if(data["comments"] != null){
                    writer.WriteSingleLineTag("div", "skill-comments", data["comments"].InnerText);
                }
                writer.WriteNextEnd();
            }
        }// end  skill
	}
}

