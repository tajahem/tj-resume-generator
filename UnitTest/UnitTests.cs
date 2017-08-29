using System;
using System.Collections;

namespace UnitTests
{
    public class UnitTests
    {
        public UnitTests()
        {
        }
    }

    class MainClass
    {
        public static void Main(string[] args)
        {
            // v this is terrible get rid of it soon
            bool keep = args.Length != 0;
            // ^
            bool status = true;
            ArrayList tests = new ArrayList();
            tests.Add(new GeneratorUnitTests(keep));

            foreach(UnitTest t in tests){
                t.Setup();
                if (!t.Test()) { status = false; }
                t.Cleanup();
            }

            if(status){
                Console.WriteLine("Build Passes");
            }else{
                Console.WriteLine("Build Fails");
            }

        }
    }
}