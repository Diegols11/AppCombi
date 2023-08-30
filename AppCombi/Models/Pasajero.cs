using System.ComponentModel.DataAnnotations;
namespace AppCombi.Models
{
    public class Pasajero
    {
        public int PasajeroID { get; set; }

        [Display(Name = "Nombre del Pasajero: ")]
        [Required]
        [StringLength(30)]
        public string NombrePasajero { get; set; }

        [Display(Name = "Edad: ")]
        [Required]
        public int Edad { get; set; }

        [Display(Name = "DNI: ")]
        [Required]
        [StringLength(8)]
        public string DniPasajero { get; set; }

        public ICollection<DetalleViaje> DetalleViaje { get; set; }
    }
}
