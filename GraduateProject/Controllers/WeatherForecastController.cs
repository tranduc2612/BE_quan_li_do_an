using GP.Business.IService;
using GP.Common.Helpers;
using GP.Models.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace GraduateProject.Controllers
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
        private ManagementGraduationProjectContext _quizletDbContext;
        private AuthHelper _authHelper;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            ManagementGraduationProjectContext quizletDbContext, 
            AuthHelper authHelper, 
            IHttpContextAccessor httpContextAccessor,
            IAccountService accountService)
        {
            _logger = logger;
            _quizletDbContext = quizletDbContext;
            _authHelper = authHelper;
            _httpContextAccessor = httpContextAccessor;
            _accountService = accountService;
        }

        [HttpGet(Name = "GetWeatherForecast"), Authorize(Roles = "ADMIN")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}