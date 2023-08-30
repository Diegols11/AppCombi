using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppCombi.Models
{
    public class Ruta
    {
        public int RutaID { get; set; }

        [Display(Name = "Nombre de la Ruta: ")]
        [Required]
        [StringLength(15)]
        public string NombreRuta { get; set; }

        [Display(Name = "Lugar de Salida: ")]
        [Required]
        [StringLength(15)]
        public string Salida { get; set; }

        [Display(Name = "Lugar de Llegada: ")]
        [Required]
        [StringLength(15)]
        public string Llegada { get; set; }

        [Display(Name = "Tiempo de Viaje (Minutos): ")]
        [Required]     
        public int Minuto { get; set; }

        public ICollection<Viaje> Viaje { get; set; }
    }
}
