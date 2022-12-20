using Microsoft.AspNetCore.Mvc;
using Nasa.Api.Mapper;
using Nasa.Api.Services;
using Nasa.Data.Models;

namespace Nasa.Api.Controllers.v1
{
    [ApiController]
    [Route("api/[controller]")]
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

        // POST: api/v1/Asteroides/asteroidesenelfuturo/3
        /// <summary>
        /// Obtiene los 3 asteroides con mas peligro para la tierra desde hoy hasta la fecha indicada mediante los dias indicados
        /// por parametro
        /// </summary>
        /// <param name="dias"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("asteroidesenelfuturo/{dias}")]
        [ProducesResponseType(typeof(List<AsteroideDto>), 200)]
        public IActionResult Get(string dias)
        {
            int valorDia;

            if (!int.TryParse(dias, out valorDia))
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