using AppCombi.Models;
using Microsoft.EntityFrameworkCore;
namespace AppCombi.Data
{
    public class ViajeContext : DbContext
    {	
		public ViajeContext(DbContextOptions options) : base(options)
        {
        }

        public ViajeContext()
        {
        }

        public DbSet<Chofer> Choferes { get; set; }
        public DbSet<Carro> Carros { get; set; }
        public DbSet<Ruta> Rutas { get; set; }
        public DbSet<Viaje> Viajes { get; set; }
        public DbSet<Pasajero> Pasajeros { get; set; }
        public DbSet<DetalleViaje> DetalleViajes { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }

        //Sobreescribir los nombres de las tablas que se van a generar en la base de datos
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Chofer>().ToTable("Chofer");
            modelBuilder.Entity<Carro>().ToTable("Carro");
            modelBuilder.Entity<Ruta>().ToTable("Ruta");
            modelBuilder.Entity<Usuario>().ToTable("Usuario");
            modelBuilder.Entity<Pasajero>().ToTable("Pasajero");
            modelBuilder.Entity<Viaje>().ToTable("Viaje")/*.HasKey(c => new { c.ChoferID, c.CarroID, c.RutaID})*/;
            modelBuilder.Entity<DetalleViaje>().ToTable("DetalleViaje").HasKey(c => new { c.ViajeID, c.PasajeroID});


            modelBuilder.Entity<Viaje>()
            .HasMany(Viaje => Viaje.detalleViajes)
            .WithOne(detalle => detalle.Viaje)
            .HasForeignKey(detalle => detalle.ViajeID);
        }
        public List<Usuario> ListaUsuario()
        {
            return new List<Usuario> {
                new Usuario{Nombre ="jose",Correo="administrador@gmail.com",Clave="123"},
                //new Usuario{Nombre ="invitado",Correo="invitado@gmail.com",Clave="123",Roles =new string[]{"Invitado" } },
            };
        }
        public Usuario ValidarUsuario(string _correo, string _clave)
        {
            return ListaUsuario().Where(item => item.Correo == _correo && item.Clave == _clave).FirstOrDefault();
        }





        //public DbSet<Asiento> Asientos { get; set; }

        //Sobreescribir los nombres de las tablas que se van a generar en la base de datos
        // public DbSet<AppCombi.Models.Pasajero> Pasajero { get; set; }


    }
}
