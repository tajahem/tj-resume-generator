using System;
using System.IO;

namespace ResumeGenerator
{
    /// <summary>
    /// Generates the base html file from a raw html file and an order file. 
    /// This allows for different ordering of the informational sections of the resume without having to edit
    /// swap section tags manually. 
    /// </summary>
    public class RawHtmlLoader
    {
        public RawHtmlLoader(string dirName)
        {
            order = new SectionOrder(dirName);
            htmlDoc = File.ReadAllText(DocumentNames.GetPath(dirName, DocumentNames.RAW_HTML));
            htmlDoc = htmlDoc.Replace(INSERT_TAG, order.order);
        }

        // The section order tag etc. are located locally because they absolutely must occur before
        // anything else or everything falls apart.
        const string INSERT_TAG = "<!--INSERT SECTIONS-->";

        SectionOrder order;
        public string htmlDoc;

    }

    public class SectionOrder
    {
        public SectionOrder(string dirName)
        {
            ReadOrder(DocumentNames.GetPath(dirName, DocumentNames.ORDER_DOC));
            if (!VerifyTags()) { throw new Exception("order.html is invalid"); }
        }

        public string order;

        void ReadOrder(string file)
        {
            order = File.ReadAllText(file);
        }

        bool VerifyTags()
        {
            bool allPresent = true;
            foreach (string s in ReplacementTags.SECTIONS)
            {
                if (!order.Contains(s))
                {
                    allPresent = false;
                }
            }
            return allPresent;
        }
    }
}
