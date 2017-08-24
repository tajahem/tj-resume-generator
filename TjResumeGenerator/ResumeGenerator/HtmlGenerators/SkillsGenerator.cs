using System;

namespace ResumeGenerator
{
	public class SkillsGenerator : HtmlGenerator
	{

		public SkillsGenerator (string dirName) : base(ReplacementTags.SKILLS_TAG)
		{
			LoadXML (dirName);
		}

		private void LoadXML(string dirName)
		{
			
		}

		private void GenerateHtml(string dirName)
		{
			
		}

	}
}

