using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using QuoteMonster.Model;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace QuoteMonster.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private static string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

		private readonly IConfigurationRoot _cfg;
		private readonly PropertyContext _context;

		public SampleDataController(IConfigurationRoot cfg, PropertyContext context)
		{
			_cfg = cfg;
			_context = context;
		}

        [HttpGet("[action]")]
        public IEnumerable<WeatherForecast> WeatherForecasts()
        {
			var properties = _context.Properties.ToArray();
			var rng = new Random();

			return properties.Select(p => new WeatherForecast() {
				DateFormatted = p.Name,
				Summary = p.Value,
				TemperatureC = rng.Next(-20, 55)
			});
        }

        public class WeatherForecast
        {
            public string DateFormatted { get; set; }
            public int TemperatureC { get; set; }
            public string Summary { get; set; }

            public int TemperatureF
            {
                get
                {
                    return 32 + (int)(TemperatureC / 0.5556);
                }
            }
        }
    }
}
