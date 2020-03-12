using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Habbes.Sperse.Compiler;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace SampleApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DynamicController : ControllerBase
    {
        private TestMethods testMethods;

        public DynamicController(TestMethods testMethods)
        {
            this.testMethods = testMethods;
        }
        // GET
        public int Execute(int x)
        {
            return testMethods.Method(x);
        }

        public string Update(string source)
        {
            var compiler = new SperseCompiler();
            testMethods.Method = (Func<int, int>) compiler.Compile(source);
            return "Success";
        }
    }
}