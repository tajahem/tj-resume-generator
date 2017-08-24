using System;
using System.IO;
using System.Collections;
using GeneratorTests;

namespace UnitTests 
{
    public class GeneratorUnitTests : UnitTest
    {
        
        public GeneratorUnitTests()
        {
        }

        const string TEST_DIR = "test";

        AssertionTester suiteTester;
        ArrayList tests = new ArrayList();

        public override void Setup()
        {
			Directory.CreateDirectory(TEST_DIR);
            tests.Add(new LinksGeneratorTest(TEST_DIR));
            tests.Add(new ContactGeneratorTest(TEST_DIR));
            tests.Add(new XpGeneratorTest(TEST_DIR));
			suiteTester = new AssertionTester(0);
			suiteTester.WriteBeginTestSuite("Begin Generator Tests...");
        }

        public override bool Test()
        {
            foreach(UnitTest t in tests){
                t.Setup();
                if (!t.Test()) { suiteTester.passing = false; }
            }
            return suiteTester.passing;
        }

        public override void Cleanup()
        {
            Directory.Delete(TEST_DIR, true);
            suiteTester.WriteTestSuiteResult();
        }

        // END TESTS

    }
}
