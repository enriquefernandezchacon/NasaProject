namespace Nasa.Data.Models
{
    public class AsteroideDto
    {
        public string Nombre { get; set; }
        public Double Diametro { get; set; }
        public Double Velocidad { get; set; }
        public DateTimeOffset FechaAproximacion { get; set; }
        public string Planeta { get; set; }
    }
}
