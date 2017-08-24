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
            bool status = true;
            ArrayList tests = new ArrayList();
            tests.Add(new GeneratorUnitTests());

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