using System.ComponentModel.DataAnnotations;

namespace AppCombi.Models
{
    public class DetalleViaje
    {
        

        public int ViajeID { get; set; }
        public int PasajeroID { get; set; }

        [Display(Name = "Asiento:")]
        [Required]
        public string Asiento { get; set; }

        public Pasajero Pasajero { get; set; }
        public Viaje Viaje { get; set; }
    }
}
