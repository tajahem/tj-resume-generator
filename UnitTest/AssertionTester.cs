using System;
namespace UnitTests
{
    public class AssertionTester
    {

        public AssertionTester(int padding)
        {
            this.padding = padding;
            pad = " ".PadRight(padding);
        }

        private const string LINE = "-------------------------------------";

        private string pad;
        public bool passing = true;
        private int padding;

        public void AssertResult(string assertMessage, bool pass)
        {
            if (!pass)
            {
                passing = false;
                Console.ForegroundColor = ConsoleColor.Red;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            // seem to have to have a whitespace with right padding to get the left padding to work
            Console.WriteLine(pad + assertMessage + "... " + pass);
        }

        public void WriteBeginTestSuite(string message)
        {
            Console.WriteLine();
            Console.WriteLine(pad + message);
            Console.WriteLine(pad + LINE);
        }

        public void WriteTestSuiteResult()
        {
            Console.ResetColor();
            Console.WriteLine(pad + LINE);
            Console.Write(pad);
            Console.ForegroundColor = ConsoleColor.White;
            if (passing)
            {
                Console.BackgroundColor = ConsoleColor.DarkGreen;
                Console.Write(CreateEndLine("TEST PASSED"));
            }
            else
            {
                Console.BackgroundColor = ConsoleColor.DarkRed;
                Console.Write(CreateEndLine("TEST FAILED"));
            }
            Console.ResetColor();
            Console.WriteLine("\n");
        }

        string CreateEndLine(string text)
        {
            int rPad = 40 - padding;
            if (rPad < 0) { rPad = 0; }
            return pad + text.PadRight(rPad);
        }
    }
}
