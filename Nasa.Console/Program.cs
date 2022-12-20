using Nasa.Data.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/*var clienteHttp = HttpClientFactory.Create();
var respuesta = await clienteHttp.GetAsync("https://api.nasa.gov/neo/rest/v1/feed?start_date=2021-12-09&end_date=2021-12-12&api_key=zdUP8ElJv1cehFM0rsZVSQN7uBVxlDnu4diHlLSb");
JObject json = new JObject();
    if(respuesta.IsSuccessStatusCode)
{
    List<Asteroide> listaPlanetas = new();
    var contenido = await respuesta.Content.ReadAsStringAsync();
    json = JObject.Parse(contenido);
    var dias = json.GetValue("near_earth_objects");
    int contador = 1;
    foreach (var planetas in dias.Children())
    {
        foreach (var planeta in planetas.ElementAt(0))
        {
            var planetaObject = JsonConvert.DeserializeObject<Asteroide>(planeta.ToString());
            listaPlanetas.Add(planetaObject);
        }
    }

    //
    var listaPlanetasPeligrosos = listaPlanetas.Select(p => p).Where(p => p.IsPotentiallyHazardousAsteroid)
        .OrderByDescending(p => (p.EstimatedDiameter.Kilometers.EstimatedDiameterMin + p.EstimatedDiameter.Kilometers.EstimatedDiameterMax) / 2)
        .Take(3);

    foreach (var planeta in listaPlanetasPeligrosos)
    {
        var diametro = (planeta.EstimatedDiameter.Kilometers.EstimatedDiameterMin + planeta.EstimatedDiameter.Kilometers.EstimatedDiameterMax) / 2;
        Console.WriteLine(contador++ + ".- Nombre: " + planeta.Name + ", Diametro(KM): " + diametro);
    }
}*/
int dias = 15;
DateTime hoy = DateTime.Now;
DateTime fechaFutura = hoy.AddDays(dias);

string fecha1 = hoy.ToString("yyyy-MM-dd");
string fecha2 = fechaFutura.ToString("yyyy-MM-dd");

Console.WriteLine(fecha1);
Console.WriteLine(fecha2);