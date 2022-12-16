using Nasa.Data.Models;

namespace Nasa.Api.Services
{
    public class AsteroidesServiceNasa : IAsteroidesService
    {
        public Task<IEnumerable<Asteroide>> GetAsteroides(int valorDia, int numeroAsteroides)
        {
            throw new NotImplementedException();
        }
    }
}
