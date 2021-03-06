﻿using System;
using System.IO;
using System.Collections;
using GeneratorTests;

namespace UnitTests
{
    public class GeneratorUnitTests : UnitTest
    {

        public GeneratorUnitTests(bool keep)
        {
            this.keep = keep;
        }

        bool keep;

        const string TEST_DIR = "test";
        const string KEEP_DIR = "lastTest";

        AssertionTester suiteTester;
        ArrayList tests = new ArrayList();

        public override void Setup()
        {
            Directory.CreateDirectory(TEST_DIR);
            tests.Add(new LinksGeneratorTest(TEST_DIR, keep));
            tests.Add(new ContactGeneratorTest(TEST_DIR, keep));
            tests.Add(new XpGeneratorTest(TEST_DIR, keep));
            tests.Add(new SkillGeneratorTest(TEST_DIR, keep));
            tests.Add(new GreetingGeneratorTest(TEST_DIR, keep));
            suiteTester = new AssertionTester(0);
            suiteTester.WriteBeginTestSuite("Begin Generator Tests...");
        }

        public override bool Test()
        {
            foreach (UnitTest t in tests)
            {
                t.Setup();
                if (!t.Test()) { suiteTester.passing = false; }
            }
            suiteTester.WriteTestSuiteResult();
            return suiteTester.passing;
        }

        public override void Cleanup()
        {
            foreach (UnitTest t in tests)
            {
                t.Cleanup();
            }
            if (!keep)
            {
                Directory.Delete(TEST_DIR, true);
            }
            else
            {
                if (Directory.Exists(KEEP_DIR))
                {
                    Directory.Delete(KEEP_DIR, true);
                }
                Directory.Move(TEST_DIR, KEEP_DIR);
            }
        }

    }
}
