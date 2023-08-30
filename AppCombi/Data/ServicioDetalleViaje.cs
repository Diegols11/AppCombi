using AppCombi.Models;
namespace AppCombi.Data
{
    public class ServicioDetalleViaje
    {
        public List<DetalleViaje> ListaDetalleViaje { get; set; } =
            new List<DetalleViaje>();
        public void Agregar(DetalleViaje detalle)
        {
            ListaDetalleViaje.Add(detalle);
        }
        public void Eliminar(int i)
        {
            ListaDetalleViaje.RemoveAt(i);
        }

        public void EliminarTodos()
        {

            int d = 0;
            foreach (var item in ListaDetalleViaje)
            {
                ListaDetalleViaje.RemoveAt(d);
                d++;
            }
        }

    }
}
