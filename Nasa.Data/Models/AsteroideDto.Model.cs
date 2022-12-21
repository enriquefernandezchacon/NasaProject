using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Nasa.Data.Models
{
    public class AsteroideDto
    {
        /// <summary> 
        /// Nombre del Asteroide
        /// </summary> 
        /// 
        [Required]
        public string Nombre { get; set; }

        /// <summary> 
        /// Diamentro del Asteroide (m)
        /// </summary> 
        [Required]
        public Double Diametro { get; set; }

        /// <summary> 
        /// Velocidad del Asteroide (km/h)
        /// </summary> 
        [Required]
        public Double Velocidad { get; set; }

        /// <summary> 
        /// Fecha de la aproximación al planeta señalado
        /// </summary> 
        [Required]
        public DateTimeOffset FechaAproximacion { get; set; }

        /// <summary> 
        /// Planeta al que se aproxima
        /// </summary> 
        [Required]
        public string Planeta { get; set; }
    }
}
