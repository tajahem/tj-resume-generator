using System;
using UnitTests;
using ResumeGenerator;
using System.IO;
using System.Text.RegularExpressions;

namespace GeneratorTests
{
    public class SkillGeneratorTest : UnitTest
    {
        public SkillGeneratorTest(string directory)
        {
            this.directory = directory;
        }

        string directory;

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

        public override void Cleanup(){} // not needed

		public override void Setup()
        {
            File.WriteAllText(DocumentNames.GetPath(directory, DocumentNames.SKILLS_DOC), SKILLS_DATA);
        }

        public override bool Test()
        {
            AssertionTester tester = new AssertionTester(10);
            SkillsGenerator skills = new SkillsGenerator(directory);

            tester.WriteBeginTestSuite("Skills Generator Tests...");
            tester.AssertResult("Only one technical skills title", 
                                XMatchesOnly(skills.GetHtml(), "Technical Skills", 1));
            tester.AssertResult("Two skill-categories exist", 
                                XMatchesOnly(skills.GetHtml(), "skill-category", 2));
            // contains skill-name and data
            tester.AssertResult("Skill name is correctly formatted", false);
            // contains skill-mastery and data
            // contains skill-comment and data
            // percentage bar is correctly formatted



            tester.WriteTestSuiteResult();
            if (!tester.passing) { return false; }
            return true;
        }

        bool XMatchesOnly(string source, string sub, int amount){
            return Regex.Matches(source, sub).Count == amount;
        }

        bool ContainsClassAndData(string source, string className, string data){
            return (!source.Contains(className) && !source.Contains(data));
        }
    }
}
