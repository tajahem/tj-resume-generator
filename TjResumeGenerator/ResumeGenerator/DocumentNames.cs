using System;
using System.IO;

namespace ResumeGenerator
{
    /// <summary>
    /// Contains the document names expected by the resume generator.
    /// </summary>
    public struct DocumentNames
    {
		// html documents
        public const string RAW_HTML = "raw.html";
        public const string ABOUT_DOC = "_about.html";
        public const string EDUCATION_DOC = "_education.html";
        public const string COVER_DOC = "_cover.html";
        public const string ORDER_DOC = "_order.html";

        // xml documents
		public const string CONTACT_DOC = "contact.xml";
        public const string GREETING_DOC = "greeting.xml";
        public const string SKILLS_DOC = "skills.xml";
        public const string XP_DOC = "xp.xml";
        public const string LINKS_DOC = "links.xml";

		// other documents
		public const string JS_DOC = "script.js";
		public const string STYLE_DOC = "style.css";

		public static string GetPath(string directory, string doc)
        {
            return Path.Combine(directory, doc);
        }

    }
}
