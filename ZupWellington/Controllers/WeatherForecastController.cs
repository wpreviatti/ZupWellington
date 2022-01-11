using EFClient.Dominio;
using EFClient.Repo;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ZupWellington.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var COCO = new Cliente {
                DtCadastro = DateTime.Now,
                Mail = "alelui@allal.com",
                Nome = "koaop",
                //NumeroChapaId = 1,
                Senha = "123",
                Sobrenome = "ajsfassf",
                Telefone = 1

            };
            using (var contexto = new ZupContext())
            {
                contexto.Cliente.Add(COCO);
                contexto.SaveChanges();
            }
            return Ok();
        }
    }
}
