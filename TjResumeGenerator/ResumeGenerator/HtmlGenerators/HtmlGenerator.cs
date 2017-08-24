using System;

namespace ResumeGenerator
{
	public abstract class HtmlGenerator
	{
		public string replacementTag;
		protected string html; 

		public HtmlGenerator (string replacementTag)
		{
			this.replacementTag = replacementTag;
		}

		public string GetHtml()
		{
			return html;
		}
	}
}

