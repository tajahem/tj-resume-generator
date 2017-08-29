using System;
using System.IO;
using ResumeGenerator;
using UnitTests;

namespace GeneratorTests
{
    public class LinksGeneratorTest : UnitTest
    {
        public LinksGeneratorTest(string directory, bool keep)
        {
            this.directory = directory;
            keepOutput = keep;
        }

        bool keepOutput;
        string directory;
        LinksGenerator linksTest;

		const string LINKS_DATA =
			@"<links>
                <link>
                    <text>Link One</text>
                    <url>example.org</url>
                </link>
                <link>
                    <text>Link Two</text>
                    <url>example.org/notreal</url>
                </link>
            </links>";

        public override void Cleanup()
        {
			if (keepOutput){
				File.WriteAllText(DocumentNames.GetPath(directory, "links.html"), linksTest.GetHtml());
			}
        }

        public override void Setup()
        {
            File.WriteAllText(DocumentNames.GetPath(directory, DocumentNames.LINKS_DOC), LINKS_DATA); 
        }

        public override bool Test()
        {
			AssertionTester tester = new AssertionTester(10);
            linksTest = new LinksGenerator(directory);

			tester.WriteBeginTestSuite("Begin LinksGenerator Tests...");
			tester.AssertResult("ID is present", linksTest.GetHtml().Contains("links-section"));
			tester.AssertResult("Link one text exists", linksTest.GetHtml().Contains("Link One"));
			tester.AssertResult("Link two url exists",
									 linksTest.GetHtml().Contains("example.org/notreal"));
			tester.WriteTestSuiteResult();
			if (!tester.passing) { return false; }
            return true;
		}
    }
}
