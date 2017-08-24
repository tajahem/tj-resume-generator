using System;
using System.IO;
using ResumeGenerator;
using UnitTests;

namespace GeneratorTests
{
    public class ContactGeneratorTest : UnitTest
    {
        public ContactGeneratorTest(string directory)
        {
            this.directory = directory;
        }

        string directory;

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

        public override void Cleanup()
        {
            // Nothing to do here as the suite will remove the parent directory
        }

        public override void Setup()
        {
            File.WriteAllText(DocumentNames.GetPath(directory, DocumentNames.CONTACT_DOC), CONTACT_DATA);
		}

        public override bool Test()
        {
			AssertionTester tester = new AssertionTester(10);
            ContactGenerator contact = new ContactGenerator(directory, true);

			tester.WriteBeginTestSuite("Begin ContactsGenerator Tests...");
			tester.AssertResult("Contact class exists", contact.GetHtml().Contains("<div class=\"contact\">"));
			tester.AssertResult("Street line exists",
									   contact.GetHtml().Contains("<li>555 Madeup St.</li>"));
			try
			{
				ContactGenerator invalid = new ContactGenerator(INVALID_CONTACT_DATA);
				tester.AssertResult("ContactGenerator catches Invalid Email", false);
			}
			catch (Exception e)
			{
				tester.AssertResult("ContactGenerator catches invalid email",
										   e.Message.Equals(ContactGenerator.INVALID_EMAIL_MESSAGE));
			}
			tester.WriteTestSuiteResult();
			if (!tester.passing) { return false; }
            return true;
        }
    }
}
