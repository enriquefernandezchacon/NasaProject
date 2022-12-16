using Microsoft.AspNetCore.Mvc;
using Nasa.Api.Mapper;
using Nasa.Api.Services;
using Nasa.Data.Models;
using System.Collections.Generic;

namespace Nasa.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AsteroidesController : ControllerBase
    {
        private readonly ILogger<AsteroidesController> _logger;
        private readonly IAsteroidesService _asteroidesService;
        private readonly IAsteroidesMapper _asteroidesMapper;

        public AsteroidesController(ILogger<AsteroidesController> logger, IAsteroidesService asteroidesService, IAsteroidesMapper asteroidesMapper)
        {
            _logger = logger;
            _asteroidesService = asteroidesService;
            _asteroidesMapper = asteroidesMapper;
        }

        [HttpGet("{dias}")]
        public IActionResult Get(string dias)
        {
            int valorDia;

            if (!Int32.TryParse(dias, out valorDia))
            {
                return BadRequest("El valor de dias no es correcto");
            }

            if (valorDia < 1 || valorDia > 7)
            {
                return BadRequest("El valor de dias debe estar entre 1 y 7");
            }
            
            var listaAsteroides = _asteroidesService.GetAsteroides(valorDia, 3);

            if (listaAsteroides == null)
            {
                return StatusCode(500);
            }

            return Ok(_asteroidesMapper.GetEnumerableAsteroidesDto(listaAsteroides.Result));
        }
    }
}