using System;
using System.Collections.Generic;
using System.IO;

namespace ResumeGenerator
{
	public class Combiner
	{
		public Combiner ()
		{
			sections = new Dictionary<string, string>();
            errors = new ResumeErrors();
		}

		private string output;
		//private string html;

		private Dictionary<string, string> sections;

		private ResumeErrors errors;

		/// <summary>
		/// Generic method for adding information to be inserted into the appropriate 
		/// section of the final document
		/// </summary>
		/// <param name="key">The replacement tag.</param>
		/// <param name="content">The content to insert.</param>
		public void AddSection(string key, string content)
		{
			sections.Add(key, content);
		}

		/// <summary>
		/// Creates the finished html document and returns success or failure. The finished result
        /// will still need to be retireved and stored.
		/// </summary>
		/// <param name="dirName">Location of the assets folder.</param>
		public bool CreateFinishedDoc(string dirName)
		{
            output = new RawHtmlLoader(dirName).htmlDoc;
			if (VerifyAssets()) 
			{
				foreach (string key in sections.Keys)
				{
					output.Replace(key, sections[key]);
				}
				return true;
			} 
			else 
			{
				errors.PrintErrors();
				return false;
			}
		}

        /// <summary>
        /// Gets the html document. WARNING: If CreateFinishedDoc is not called first it will return null.
        /// </summary>
        /// <returns>The html.</returns>
		public string GetHtml()
		{
			return output;
		}

		// skips static assets as they are verified when loaded
		private bool VerifyAssets()
		{
            if(output == null){ return false; }
			foreach(string tag in ReplacementTags.TAGS)
			{
				if (!sections.ContainsKey(tag)) {
                    errors.AddError("Document is missing " + tag);
                    return false; }	
			}
			return true;
		}	
	}
}
	