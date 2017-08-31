using System;
using System.IO;
using UnitTests;
using ResumeGenerator;

namespace GeneratorTests
{
    public class XpGeneratorTest : UnitTest
    {
        public XpGeneratorTest(string directory, bool keep)
        {
            this.directory = directory;
            keepOutput = keep;
        }

        bool keepOutput;
        string directory;
        ExperienceGenerator xp;

        const string XP_DATA =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <xp>
                <job>
                    <title>Attendant</title>
                    <company>The Local Gas Station</company>
                    <dates>2015-2016</dates>
                </job>
                <job>
                    <title>General IT</title>
                    <company>The Local NPO</company>
                    <dates>2016-present</dates>
                    <point>That one great thing you did</point>
                    <point>The other equally great thing you did</point>
                </job>
            </xp>";

        public override void Setup()
        {
            File.WriteAllText(DocumentNames.GetPath(directory, DocumentNames.XP_DOC), XP_DATA);
        }

        public override bool Test()
        {
            AssertionTester tester = new AssertionTester(10);
            xp = new ExperienceGenerator(directory);

            tester.WriteBeginTestSuite("Begin ExperienceGenerator Tests...");
            tester.AssertResult("Attendent job line exists",
                                xp.GetHtml().Contains("<div class=\"job-title\">Attendant</div>"));
            tester.AssertResult("Points generated correctly",
                                xp.GetHtml().Contains("<li>That one great thing you did</li>"));

            tester.WriteTestSuiteResult();

            if (!tester.passing) { return false; }
            return true;
        }

        public override void Cleanup()
        {
            if (keepOutput)
            {
                File.WriteAllText(DocumentNames.GetPath(directory, "xp.html"), xp.GetHtml());
            }
        }
    }
}
