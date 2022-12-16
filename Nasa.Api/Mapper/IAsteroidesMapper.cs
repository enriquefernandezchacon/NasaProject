using Nasa.Data.Models;

namespace Nasa.Api.Mapper
{
    public interface IAsteroidesMapper
    {
        AsteroideDto GetAsteroideDto(Asteroide asteroide);
        
        IEnumerable<AsteroideDto> GetEnumerableAsteroidesDto(IEnumerable<Asteroide> asteroides);
    }
}
