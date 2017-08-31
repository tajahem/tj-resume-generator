using System;
using System.IO;
using UnitTests;
using ResumeGenerator;

namespace GeneratorTests
{
    public class GreetingGeneratorTest : UnitTest
    {
        public GreetingGeneratorTest(string directory, bool keep)
        {
            this.directory = directory;
            keepOutput = keep;
        }

        string directory;
        bool keepOutput;
        GreetingGenerator greeting;

        const string GREETING_DATA =
            @"<?xml version=""1.0"" encoding=""UTF-8""?>
            <greeting>
                <name>Awesome Company Name</name>
                <us-address>
                    <street>556 Madeup St.</street>
                    <city>Lincoln</city>
                    <state>NE</state>
                    <zipcode>68588</zipcode>
                </us-address>
                <salutation>Dear Hiring Manager;</salutation>
            </greeting>";

        public override void Cleanup()
        {
            if (keepOutput)
            {
                File.WriteAllText(DocumentNames.GetPath(directory, "greeting.html"),
                                  greeting.GetHtml());
            }
        }

        public override void Setup()
        {
            File.WriteAllText(DocumentNames.GetPath(directory, DocumentNames.GREETING_DOC),
                              GREETING_DATA);
        }

        public override bool Test()
        {
            AssertionTester tester = new AssertionTester(10);
            greeting = new GreetingGenerator(directory);

            tester.WriteBeginTestSuite("Begin Greeting Generator Test");
            tester.AssertResult("Company Name exists",
                                greeting.GetHtml().Contains("<li>Awesome Company Name</li>"));
            tester.AssertResult("Street line exists",
                                greeting.GetHtml().Contains("<li>556 Madeup St.</li>"));
            tester.AssertResult("Second address line is correct",
                                greeting.GetHtml().Contains("<li>Lincoln, NE 68588</li>"));
            tester.AssertResult("Correctly formatted salutation",
                                greeting.GetHtml().Contains(FormattedSalutation()));
            tester.WriteTestSuiteResult();

            if (!tester.passing) { return false; }
            return true;
        }

        string FormattedSalutation()
        {
            string div = "\t<div id=\"salutation\">";
            string sal = "\t\tDear Hiring Manager;";
            string end = "\t</div>";
            return div + Environment.NewLine + sal + Environment.NewLine + end;
        }
    }
}
