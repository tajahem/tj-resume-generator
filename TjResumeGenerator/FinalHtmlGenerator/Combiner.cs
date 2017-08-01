using System;
using System.Collections.Generic;
using System.IO;

namespace FinalHtmlGenerator
{
	public class Combiner
	{
		public Combiner ()
		{
			sections = new Dictionary<string, string>();
		}

		// DOCUMENT NAMES
		const string HTML_DOC =  "base.html";
		const string JS_DOC = "script.js";
		const string STYLE_DOC = "style.css";
		const string COVER_DOC = "cover.html";

		private ElementOrder order; 

		private string dirName;
		private string output;
		private string html;

		private Dictionary<string, string> sections;

		private ResumeErrors errors;

		// skips static assets as they are verified when loaded
		private bool VerifyAssets()
		{
			if(html == null){ return false; }
			foreach(string tag in ReplaceTags.TAG_LIST)
			{
				if (!sections.ContainsKey(tag)) { return false; }	
			}
			return true;
		}

		private void Combine(string dirName)
		{
			LoadStaticAssets (dirName);
			if (VerifyAssets ()) 
			{
				output = HtmlDocument.generateHtml(order, html);
				foreach (string key in sections.Keys)
				{
					output.Replace (key, sections[key]);
				}		
			} 
			else 
			{
				errors.printErrors();
			}
			// grab name
			// grab coverLetter
			// grab coverContact
			// grab coverGreeting
			// grab skills
			// grab experience
			// grab about
			// grab education
			// grab resumeContact
		}

		private void LoadStaticAssets(string dirName)
		{
			LoadStaticAsset(dirName, JS_DOC, ReplaceTags.SCRIPT_TAG);
			LoadStaticAsset(dirName, STYLE_DOC, ReplaceTags.STYLE_TAG);
		}

		/// <summary>
		/// Loads the static asset into the sections map with the key provided.
		/// </summary>
		/// <param name="dirName">Parent directory of the asset.</param>
		/// <param name="asset">Asset name.</param>
		/// <param name="key">Key assosicated with the asset.</param>
		private void LoadStaticAsset(string dirName, string asset, string key)
		{
			try{
				// curious why this needs System when using the System.IO namespace
				StreamReader reader = File.OpenRead(System.IO.Path.Combine(dirName, asset));
				sections.Add(key, reader.ReadToEnd());
				reader.Close;
			}
			catch(Exception e) 
			{
				errors.addError("file read error for " + asset);
			}
		}
			
	}
}
	