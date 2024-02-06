using GP.Business.IService;
using GP.Common.Helpers;
using GP.Models.Data;
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
        private QuizletDbContext _quizletDbContext;
        private AuthHelper _authHelper;
        private readonly IAccountService _accountService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public WeatherForecastController(
            ILogger<WeatherForecastController> logger, 
            QuizletDbContext quizletDbContext, 
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

        [HttpGet("test"), Authorize]
        public Response TestDB()
        {
            string currentUser = _accountService.GetCurrentUsername();
            var res = _quizletDbContext.Accounts.Where(x => x.Username == currentUser);
            return new Response(StatusCodes.Status200OK, res);
        }
    }
}