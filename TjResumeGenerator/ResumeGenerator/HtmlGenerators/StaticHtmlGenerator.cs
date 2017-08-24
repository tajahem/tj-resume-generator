using System;
using System.IO;

namespace ResumeGenerator
{
	/// <summary>
	/// Generic html generator just reads in a basic html file.
	/// </summary>
	public class StaticHtmlGenerator : HtmlGenerator
	{
		public StaticHtmlGenerator(string dirName, string fileName, string tag) : base(tag)
		{
            html = File.ReadAllText(DocumentNames.GetPath(dirName, fileName));
		}
			
	}
}

