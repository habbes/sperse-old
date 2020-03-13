using System;

namespace SampleApi
{
    public class TestMethods
    {
        public TestMethods()
        {
            Execute = x => x;
        }
        public Func<int, int> Execute { get; set; }
    }
}