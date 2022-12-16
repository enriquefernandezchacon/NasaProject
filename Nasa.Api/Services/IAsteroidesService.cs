using Nasa.Data.Models;

namespace Nasa.Api.Services
{
    public interface IAsteroidesService
    {
        Task<IEnumerable<Asteroide>> GetAsteroides(int valorDia, int numeroAsteroides); 
    }
}
