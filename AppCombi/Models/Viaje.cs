using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppCombi.Models
{
    public class Viaje
    {
        public int ViajeID { get; set; }
        public int RutaID { get; set; }
        public int ChoferID { get; set; }
        public int CarroID { get; set; }
        //public int PasajeroID { get; set;}

        [Display(Name = "Código: ")]
        [Required]
        [StringLength(15)]
        public string Identif { get; set; }

        [Display(Name = "Fecha: ")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaViaje { get; set; }

        [Display(Name = "Estado : ")]
        [Required]
        [StringLength(15)]
        public string Estado { get; set; }

        public ICollection<DetalleViaje> detalleViajes { get; set; }

        public Chofer Chofer { get; set; }
        public Carro Carro { get; set; }

        public Ruta Ruta { get; set; }

        public Viaje() 
        {
            this.detalleViajes = new HashSet<DetalleViaje>();
        }

        //public Pasajero Pasajero { get; set; }

    }
}
