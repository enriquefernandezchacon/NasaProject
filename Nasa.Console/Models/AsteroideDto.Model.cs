namespace Nasa.Data.Models
{
    public class AsteroideDto
    {
        /// <summary> 
        /// Employee id 
        /// </summary> 
        public string Nombre { get; set; }
        /// <summary> 
        /// Employee id 2
        /// </summary> 
        public Double Diametro { get; set; }
        public Double Velocidad { get; set; }
        public DateTimeOffset FechaAproximacion { get; set; }
        public string Planeta { get; set; }
    }
}
