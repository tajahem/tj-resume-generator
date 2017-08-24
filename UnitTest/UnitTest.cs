using System;
namespace UnitTests
{
    public abstract class UnitTest
    {
        public UnitTest()
        {
        }

        public abstract void Setup();
        public abstract bool Test();
        public abstract void Cleanup();
    }
}
