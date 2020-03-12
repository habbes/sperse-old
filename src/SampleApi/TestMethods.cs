using System;

namespace SampleApi
{
    public class TestMethods
    {
        public TestMethods()
        {
            Method = x => x;
        }
        public Func<int, int> Method { get; set; }
    }
}