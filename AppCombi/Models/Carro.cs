using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace AppCombi.Models
{
    public class Carro
    {
        public int CarroID { get; set; }
       // public int ChoferID { get; set; }

        [Display(Name = "Placa del Carro: ")]
        [Required]
        [StringLength(7)]
        public string Placa { get; set; }

        [Display(Name = "Color del Vehículo: ")]
        [Required]
        [StringLength(20)]
        public string Color { get; set; }

        [Display(Name = "Cantidad de asientos: ")]
        [Required]
        public int Asientos { get; set; }

        [Display(Name = "Fecha del Último Mantenimiento: ")]
        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}", ApplyFormatInEditMode = true)]
        public DateTime FechaMantenimiento { get; set; }

        public ICollection<Viaje> Viaje { get; set; }

        //public Chofer Chofer  { get; set; }
    }
}
