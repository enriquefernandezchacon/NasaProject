using Nasa.Data.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace Nasa.Api.Services
{
    public class AsteroidesServiceNasa : IAsteroidesService
    {
        public async Task<IEnumerable<Asteroide>> GetAsteroides(int valorDia, int numeroAsteroides)
        {
            var clienteHttp = HttpClientFactory.Create();
            var respuesta = await clienteHttp.GetAsync(GetUrl(valorDia));
            if (respuesta.IsSuccessStatusCode)
            {
                List<Asteroide> listaPlanetas = new();
                var contenido = await respuesta.Content.ReadAsStringAsync();
                var json = JObject.Parse(contenido);
                var dias = json.GetValue("near_earth_objects");

                foreach (var planetas in dias.Children())
                {
                    foreach (var planeta in planetas.ElementAt(0))
                    {
                        var planetaObject = JsonConvert.DeserializeObject<Asteroide>(planeta.ToString());
                        listaPlanetas.Add(planetaObject);
                    }
                }

                var listaPlanetasPeligrosos = listaPlanetas.Select(p => p).Where(p => p.IsPotentiallyHazardousAsteroid)
                    .OrderByDescending(p => (p.EstimatedDiameter.Kilometers.EstimatedDiameterMin + p.EstimatedDiameter.Kilometers.EstimatedDiameterMax) / 2)
                    .Take(3);
                return listaPlanetasPeligrosos.ToList();
            }
            return null;
        }

        private string GetUrl(int dias)
        {
            DateTime hoy = DateTime.Now;
            DateTime fechaFutura = hoy.AddDays(dias);
            return $"https://api.nasa.gov/neo/rest/v1/feed?start_date={hoy.ToString("yyyy-MM-dd")}&end_date={fechaFutura.ToString("yyyy-MM-dd")}&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb";
        }
    }
}
