using System;
using System.Collections.Generic;

namespace ResumeGenerator
{
	public struct ReplacementTags
	{
		// REPLACEMENT TAGS
		public const string SCRIPT_TAG = "<!--INSERT SCRIPT-->";
		public const string STYLE_TAG = "<!--INSERT STYLE-->";
		public const string COVER_TAG = "<!--INSERT COVER-->";
		public const string CONTACT_TAG = "<!--INSERT CONTACT-->";
		public const string NAME_TAG = "<!--INSERT NAME-->";
		public const string GREETING_TAG = "<!--INSERT GREETING-->";
		public const string ABOUT_TAG = "<!--INSERT ABOUT-->";
		public const string SKILLS_TAG = "<!--INSERT SKILLS-->";
		public const string XP_TAG = "<!--INSERT XP-->";
		public const string EDUCATION_TAG = "<!--INSERT EDUCATION-->";
		public const string LINKS_TAG = "<!--INSERT LINKS-->";

		/// <summary>
		/// An array containing all of the replacement tags
		/// </summary>
		public static string[] TAGS= { SCRIPT_TAG, STYLE_TAG, COVER_TAG, CONTACT_TAG, GREETING_TAG, 
			ABOUT_TAG, SKILLS_TAG, XP_TAG, EDUCATION_TAG, NAME_TAG, LINKS_TAG};

        /// <summary>
        /// An array of the sections. 
        /// </summary>
		public static string[] SECTIONS= { ABOUT_TAG, SKILLS_TAG, XP_TAG, EDUCATION_TAG };
	}
}

