using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace AppCombi.Models
{
    public enum Dispo
    {
        Si, No
    }

    public class Chofer
    {
        public int ChoferID { get; set; }

        [Display(Name = "Nombre del Chofer: ")]
        [Required]
        [StringLength(60)]
        public string Nombre { get; set; }

        [Display(Name = "DNI: ")]
        [Required]
        [StringLength(8)]
        public string Dni { get; set; }

        [Display(Name = "Teléfono: ")]
        [Required]
        [StringLength(9)]
        public string Telefono { get; set; }

        [Display(Name = "Correo electrónico: ")]
        [Required]
        [EmailAddress]
        public string Correo { get; set; }

        //[Display(Name = "Placa del Carro: ")]
        //[Required]
        //[StringLength(7)]
        //public string Placa { get; set; }

        [Display(Name = "Disponibilidad: ")]
        [Required]
        public Dispo? Dispo { get; set; }

        [Display(Name = "Descripción: ")]
        [Required]
        [StringLength(80)]
        public string Descripcion { get; set; }

        public ICollection<Viaje> Viaje { get; set; }

    }
}
