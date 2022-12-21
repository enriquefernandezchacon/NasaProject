using Microsoft.AspNetCore.Mvc;
using Nasa.Api.Mapper;
using Nasa.Api.Services;
using Nasa.Data.Models;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace Nasa.Api.Controllers.v1
{
    /*
     * BUGFIX Arreglar unidades en velocidad y diametro, no coge la coma de los decimales
     * TODO Implementar appsettings para guardar la APIKEY
     */
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public class AsteroidesController : ControllerBase
    {
        private readonly IAsteroidesService _asteroidesService;
        private readonly IAsteroidesMapper _asteroidesMapper;

        public AsteroidesController(IAsteroidesService asteroidesService, IAsteroidesMapper asteroidesMapper)
        {
            _asteroidesService = asteroidesService;
            _asteroidesMapper = asteroidesMapper;
        }

        // POST: api/v1/Asteroides/asteroidesenelfuturo/3
        /// <summary>
        /// Los 3 asteroides mas peligrosos
        /// </summary>
        /// <remarks>
        /// Devuelve los 3 asteroides con mas posibilidades de impacta en la tierra desde hoy hasta la fecha indicada mediante los dias indicados por parametro
        /// </remarks>
        /// <param name="dias">Cantidad de dias desde hoy hasta la fecha requerida. Mínimo 1, Máximo 7</param>
        /// <returns>3 objetos de tipo PlanetaDto</returns>
        /// <response code="200">Peticion correcta</response>
        /// <response code="400">Atributo "dias" fuera de rango</response>
        /// <response code="500">Error del servicio por parte de la Nasa</response>
        [HttpPost]
        [Route("asteroidesenelfuturo/{dias}")]
        [ProducesResponseType(typeof(List<AsteroideDto>), 200)]
        [SwaggerResponse(statusCode: StatusCodes.Status400BadRequest, type: null, description: "dias fuera de rango")]
        [SwaggerResponse(statusCode: StatusCodes.Status500InternalServerError, type: null, description: "nasa api service fuera de servicio")]
        public IActionResult GetTresAsteroidesPeligrosos(string dias)
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