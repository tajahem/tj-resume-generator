using System;
using System.IO;
using ResumeGenerator;

namespace UnitTests 
{
    public class GeneratorUnitTests
    {
        
        public GeneratorUnitTests()
        {
            CreateAssets();
            RunTests();
            DestroyAssets();
        }


        const string TEST_DIR = "test";


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

        const string CONTACT_DATA =
			@"<contact>
                <us-address>
                    <street>555 Madeup St.</street>
                    <city>Lincoln</city>
                    <state>NE</state>
                    <zipcode>68588</zipcode>
                </us-address>
                <phone>
                    <number>1.555.555.1111</number>
                    <type>Home</type>
                </phone>
                <email>example@example.com</email>
            </contact>";
        const string INVALID_CONTACT_DATA =
            @"<contact>
                <email>invalid@invalid</email>
            </contact>";

        const string XP_DATA =
			@"<xp>
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

        AssertionTester suiteTester;

        void CreateAssets()
        {
            Directory.CreateDirectory(TEST_DIR);
            File.WriteAllText(DocumentNames.GetPath(TEST_DIR, DocumentNames.LINKS_DOC), LINKS_DATA);
            File.WriteAllText(DocumentNames.GetPath(TEST_DIR, DocumentNames.CONTACT_DOC), CONTACT_DATA);
            File.WriteAllText(DocumentNames.GetPath(TEST_DIR, DocumentNames.XP_DOC), XP_DATA);
            suiteTester = new AssertionTester(0);
            suiteTester.WriteBeginTestSuite("Begin Generator Tests...");
        }

        void DestroyAssets()
        {
            Directory.Delete(TEST_DIR, true);
            suiteTester.WriteTestSuiteResult();
        }

        // TESTS
        void RunTests()
        {
            TestLinks();
            TestContact();
            TestExperience();
        }

        void TestLinks()
        {
            AssertionTester tester = new AssertionTester(10);
            LinksGenerator linksTest = new LinksGenerator(TEST_DIR);

            tester.WriteBeginTestSuite("Begin LinksGenerator Tests...");
            tester.AssertResult("ID is present", linksTest.GetHtml().Contains("links-section"));
            tester.AssertResult("Link one text exists", linksTest.GetHtml().Contains("Link One"));
            tester.AssertResult("Link two url exists",
                                     linksTest.GetHtml().Contains("example.org/notreal"));
            tester.WriteTestSuiteResult();
            if (!tester.passing) { suiteTester.passing = false; }
        }

        void TestContact()
        {
            AssertionTester tester = new AssertionTester(10);
            ContactGenerator contact = new ContactGenerator(TEST_DIR, true);

            tester.WriteBeginTestSuite("Begin ContactsGenerator Tests...");
            tester.AssertResult("Contact class exists",contact.GetHtml().Contains("<div class=\"contact\">"));
            tester.AssertResult("Street line exists", 
                                       contact.GetHtml().Contains("<li>555 Madeup St.</li>"));
            try{
                ContactGenerator invalid = new ContactGenerator(INVALID_CONTACT_DATA);
                tester.AssertResult("ContactGenerator catches Invalid Email", false);
            }catch(Exception e){
                tester.AssertResult("ContactGenerator catches invalid email", 
                                           e.Message.Equals(ContactGenerator.INVALID_EMAIL_MESSAGE));
            }
            tester.WriteTestSuiteResult();
            if (!tester.passing) { suiteTester.passing = false; }
        }

        void TestExperience()
        {

            AssertionTester tester = new AssertionTester(10);
            ExperienceGenerator xp = new ExperienceGenerator(TEST_DIR);
            Console.WriteLine(xp.GetHtml());

            tester.WriteBeginTestSuite("Begin ExperienceGenerator Tests...");
            tester.AssertResult("Attendent job line exists", 
                                xp.GetHtml().Contains("<div class=\"job-title\">\nAttendant</div>"));
            tester.AssertResult("Points generated correctly", 
                                xp.GetHtml().Contains("<li>That one great thing you did</li>"));

            tester.WriteTestSuiteResult();
            if (!tester.passing) { suiteTester.passing = false; }
        }

        // END TESTS

    }
}
