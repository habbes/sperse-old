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
    [Route("{controller}")]
    public class DemoController : ControllerBase
    {

        private TestMethods testMethods;

        public DemoController(TestMethods testMethods)
        {
            this.testMethods = testMethods;
        }

        [HttpGet()]
        public string Get()
        {
            return "Call /demo/execute/:input to execute method and /demo/update/:source to update method definition";
        }

        [HttpGet("Execute/{x}")]
        public int Execute(int x)
        {
            return testMethods.Execute(x);
        }

        [HttpGet("Update/{source}")]
        public string Update(string source)
        {
            try
            {
                var compiler = new SperseCompiler();
                testMethods.Execute = compiler.Compile(source) as Func<int, int>;
            }
            catch (Exception e)
            {
                return $"Error: {e.Message}";
            }
            return "Successfully redefined method";
        }
    }
}