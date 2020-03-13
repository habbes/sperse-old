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
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private TestMethods testMethods;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, TestMethods testMethods)
        {
            _logger = logger;
            this.testMethods = testMethods;
        }

        [HttpGet("{x}")]
        public int Get(int x)
        {
            return testMethods.Method(x);
        }

        [HttpGet("Update/{source}")]
        public bool Update(string source)
        {
            var compiler = new SperseCompiler();
            testMethods.Method = compiler.Compile(source) as Func<int, int>;
            return true;
        }
    }
}