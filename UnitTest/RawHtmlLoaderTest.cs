using System;
using System.IO;
using ResumeGenerator;

namespace UnitTests
{
    public class RawHtmlLoaderTest : UnitTest
    {
        public RawHtmlLoaderTest()
        {
        }

        const string TEST_DIR = "test";

        public override void Cleanup()
        {
            Directory.Delete(TEST_DIR, true);
        }

        public override void Setup()
        {
            Directory.CreateDirectory(TEST_DIR);
            File.WriteAllText(DocumentNames.GetPath(TEST_DIR, DocumentNames.RAW_HTML),
                              CreateRawHtml());
            File.WriteAllText(DocumentNames.GetPath(TEST_DIR, DocumentNames.ORDER_DOC),
                              CreateOrder());
        }

        public override bool Test()
        {
            AssertionTester tester = new AssertionTester(0);
            tester.WriteBeginTestSuite("Begin RawHtmlLoader test");
            try
            {
                RawHtmlLoader loader = new RawHtmlLoader(TEST_DIR);
                tester.AssertResult("Order Document is valid", true);
                tester.AssertResult("Raw html contains insert style",
                                    loader.htmlDoc.Contains(ReplacementTags.STYLE_TAG));
                tester.AssertResult("Raw html contains insert script",
                                    loader.htmlDoc.Contains(ReplacementTags.SCRIPT_TAG));
                tester.AssertResult("Raw html contains insert sections",
                                    loader.htmlDoc.Contains("<!--INSERT SECTIONS-->"));
            }
            catch (Exception e)
            {
                if (e.Message.Equals("order.html is invalid"))
                {
                    tester.AssertResult("Order Document is valid", false);
                }
                else
                {
                    Console.Write(e.Message);
                }
            }
            tester.WriteTestSuiteResult();
            if (!tester.passing) { return false; }
            return true;
        }

        // overly long, but not going to split it up at the moment
        string CreateRawHtml()
        {
            HtmlWriter writer = new HtmlWriter();
            writer.WriteContent("<!DOCTYPE html>");
            writer.WriteNewLine();
            writer.WriteBeginTag("html");
            //write head
            writer.WriteBeginTag("head");
            writer.WriteSingleLineTag("title", "RESUME GENERATOR RAW HTML TEST");
            writer.WriteBeginTag("script");
            writer.WriteComment("INSERT SCRIPT");
            writer.WriteNextEnd();
            writer.WriteBeginTag("style");
            writer.WriteComment("INSERT STYLE");
            writer.WriteNextEnd();
            writer.WriteNextEnd();
            //write body
            writer.WriteBeginTag("body");
            writer.WriteBeginTag("div", "cover");
            writer.WriteComment("INSERT CONTACT");
            writer.WriteComment("INSERT GREETING");
            writer.WriteComment("INSERT COVER");
            writer.WriteNextEnd();

            writer.WriteBeginTag("div", "resume");
            writer.WriteBeginTag("div", "sidebar");
            writer.WriteComment("INSERT CONTACT");
            writer.WriteComment("INSERT LINKS");
            writer.WriteNextEnd();
            writer.WriteComment("INSERT SECTIONS");
            writer.WriteAllEnds();
            return writer.GetHtml();
        }

        string CreateOrder()
        {
            HtmlWriter writer = new HtmlWriter();
            writer.WriteComment("INSERT ABOUT");
            writer.WriteComment("INSERT SKILLS");
            writer.WriteComment("INSERT XP");
            writer.WriteComment("INSERT EDUCATION");
            return writer.GetHtml();
        }
    }
}
