using Nasa.Data.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using System.Net.Http;

namespace Nasa.Api.Services
{
    public class AsteroidesServiceNasa : IAsteroidesService
    {
        private readonly IConfiguration _configuration;
        public AsteroidesServiceNasa(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<IEnumerable<Asteroide>> GetAsteroides(int valorDia, int numeroAsteroides)
        {
            var respuesta = await HttpClientFactory.Create().GetAsync(GetUrl(valorDia));
            
            if (respuesta.IsSuccessStatusCode)
            {
                var json = await respuesta.Content.ReadAsStringAsync();

                var listaPlanetas = JObject.Parse(json).GetValue("near_earth_objects").SelectMany(x => x.SelectMany(y => y)).Select(planeta => JsonConvert.DeserializeObject<Asteroide>(planeta.ToString()));

                return listaPlanetas.Select(p => p).Where(p => p.IsPotentiallyHazardousAsteroid)
                    .OrderByDescending(p => (p.EstimatedDiameter.Kilometers.EstimatedDiameterMin + p.EstimatedDiameter.Kilometers.EstimatedDiameterMax) / 2)
                    .Take(3);
            }
            return null;
        }

        private string GetUrl(int dias)
        {
            DateTime hoy = DateTime.Now;
            DateTime fechaFutura = hoy.AddDays(dias);
            string url = _configuration.GetSection("NasaService").GetSection("url").Value;
            url += "?api_key=" + _configuration["NasaService:ApiKey"];
            url += $"&start_date ={hoy.ToString("yyyy-MM-dd")}&end_date={fechaFutura.ToString("yyyy-MM-dd")}";
            return url;
        }
    }
}
