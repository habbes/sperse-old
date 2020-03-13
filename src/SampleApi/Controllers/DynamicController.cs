using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Habbes.Sperse.Compiler;
using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Routing;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace SampleApi.Controllers
{
    public class DynamicController : ODataController
    {
        private TestMethods testMethods;

        public DynamicController(TestMethods testMethods)
        {
            this.testMethods = testMethods;
        }

        [HttpGet]
        [EnableQuery]
        //[ODataRoute("Call(x={x})")]
        [ODataRoute("Call(x={x})")]
        public int Call([FromODataUri] int x)
        {
            return testMethods.Method(x);
        }

        public string Update(string source)
        {
            var compiler = new SperseCompiler();
            testMethods.Method = (Func<int, int>)compiler.Compile(source);
            return "Success";
        }
    }
}