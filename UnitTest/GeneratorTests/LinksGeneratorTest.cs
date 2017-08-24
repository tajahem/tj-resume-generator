using System;
using System.IO;
using ResumeGenerator;
using UnitTests;

namespace GeneratorTests
{
    public class LinksGeneratorTest : UnitTest
    {
        public LinksGeneratorTest(string directory)
        {
            this.directory = directory;
        }

        string directory;

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
            // Nothing to do here as the suite class will delete the directory
        }

        public override void Setup()
        {
            File.WriteAllText(DocumentNames.GetPath(directory, DocumentNames.LINKS_DOC), LINKS_DATA); 
        }

        public override bool Test()
        {
			AssertionTester tester = new AssertionTester(10);
            LinksGenerator linksTest = new LinksGenerator(directory);

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
