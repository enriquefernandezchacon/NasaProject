using Nasa.Data.Models;

namespace Nasa.Api.Mapper
{
    public class AsteroidesMapper : IAsteroidesMapper
    {
        public AsteroideDto GetAsteroideDto(Asteroide asteroide)
        {
            return new AsteroideDto()
            {
                Nombre = asteroide.Name,
                Diametro = (asteroide.EstimatedDiameter.Meters.EstimatedDiameterMin + asteroide.EstimatedDiameter.Meters.EstimatedDiameterMax) / 2,
                Velocidad = Double.Parse(asteroide.CloseApproachData.FirstOrDefault().RelativeVelocity.KilometersPerHour),
                FechaAproximacion = asteroide.CloseApproachData.FirstOrDefault().CloseApproachDate,
                Planeta =  asteroide.CloseApproachData.FirstOrDefault().OrbitingBody
            };
        }

        public IEnumerable<AsteroideDto> GetEnumerableAsteroidesDto(IEnumerable<Asteroide> asteroides)
        {
            List<AsteroideDto> lista = new();
            foreach (var asteroide in asteroides)
            {
                lista.Add(GetAsteroideDto(asteroide));
            }
            return lista;
        }
    }
}
