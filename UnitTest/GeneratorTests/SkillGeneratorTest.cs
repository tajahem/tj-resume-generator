using System;
using UnitTests;
using ResumeGenerator;
using System.IO;
using System.Text.RegularExpressions;

namespace GeneratorTests
{
    public class SkillGeneratorTest : UnitTest
    {
        public SkillGeneratorTest(string directory, bool keep)
        {
            this.directory = directory;
            keepOutput = keep;
        }

        string directory;
        SkillsGenerator skills;
        bool keepOutput;

		const string SKILLS_DATA = 
            @"<skills>
                <skill>
                    <name>C#</name>
                    <category>Technical Skills</category>
                    <percentage>20</percentage>
                    <mastery>Beginner</mastery>
                </skill>
                <skill>
                    <name>Javascript</name>
                    <category>Technical Skills</category>
                    <percentage>75</percentage>
                    <mastery>Advanced</mastery>
                    <comments>Expereince working with JQuery, JSON and Ajax</comments>
                </skill>
                <skill>
                    <name>Monodevelop</name>
                    <category>Tools</category>
                    <percentage>25</percentage>
                    <mastery>Beginner</mastery>
                </skill>
            </skills>";

        public override void Cleanup()
        {
            if(keepOutput){
                File.WriteAllText(DocumentNames.GetPath(directory, "skills.html"), skills.GetHtml());
            }
        }

		public override void Setup()
        {
            File.WriteAllText(DocumentNames.GetPath(directory, DocumentNames.SKILLS_DOC), SKILLS_DATA);
        }

        public override bool Test()
        {
            AssertionTester tester = new AssertionTester(10);
            skills = new SkillsGenerator(directory);

            tester.WriteBeginTestSuite("Skills Generator Tests...");
            tester.AssertResult("Only one technical skills title", 
                                XMatchesOnly(skills.GetHtml(), 
                                    "<div class=\"skill-category-title\">Technical Skills</div>", 1));
            tester.AssertResult("Two skill-categories exist", 
                                XMatchesOnly(skills.GetHtml(), "class=\"skill-category\"", 2));
            // contains skill-name and data
            tester.AssertResult("Skill name is correctly formatted", DivExists("name", "C#"));
            // contains skill-mastery and data
            tester.AssertResult("Skill mastery is correctly formatted", DivExists("mastery", "Beginner"));
            // contains skill-comment and data
            string comment = "Expereince working with JQuery, JSON and Ajax";
            tester.AssertResult("Skill comment is correctly formatted", DivExists("comments", comment));
            // percentage bar is correctly formatted
            tester.AssertResult("Percentage Bar is correctly formatted",
                                skills.GetHtml().Contains(FormattedBar()));
            tester.WriteTestSuiteResult();
            if (!tester.passing) { return false; }
            return true;
        }

        bool DivExists(string className, string text)
        {
            string t = "<div class=\"skill-" + className + "\">" + text + "</div>";
            return skills.GetHtml().Contains(t);
        }

        string FormattedBar()
        {
			string barBg = "\t\t\t<div class=\"skill-percentage-bar\">";
			string innerBar = "\t\t\t\t<div style=\"width: 25;\"></div>";
            return barBg + Environment.NewLine + innerBar + Environment.NewLine + "\t\t\t</div>";
        }

        bool XMatchesOnly(string source, string sub, int amount){
            return Regex.Matches(source, sub).Count == amount;
        }

    }
}
