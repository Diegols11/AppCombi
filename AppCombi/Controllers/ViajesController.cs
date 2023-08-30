using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCombi.Data;
using AppCombi.Models;
using System.Configuration;

namespace AppCombi.Controllers
{
    public class ViajesController : Controller
    {
        private readonly ViajeContext _context;


        private readonly ServicioDetalleViaje _servicio;


        public ViajesController(ViajeContext context, ServicioDetalleViaje servicio)
        {
            _context = context;
            _servicio = servicio;
        }

        // GET: Viajes
        public async Task<IActionResult> Index()
        {
            var viajeContext = _context.Viajes.Include(v => v.Carro).Include(v => v.Chofer).Include(v => v.Ruta);
            _servicio.EliminarTodos();

            var listaDetalle = _servicio.ListaDetalleViaje.ToList();

            return View(await viajeContext.ToListAsync());
        }

        // GET: Viajes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.Carro)
                .Include(v => v.Chofer)
                .Include(v => v.Ruta)
                .FirstOrDefaultAsync(m => m.ViajeID == id);

            int idViaje = viaje.ViajeID;
            var detalle = _context.DetalleViajes.Where(m => m.ViajeID == idViaje);
            int k = 0;

            foreach (var item in detalle)
            {

                var vPasajero = _context.Pasajeros.Where(m => m.PasajeroID == item.PasajeroID).ToList();
                viaje.detalleViajes.ElementAt(k).Pasajero = vPasajero[0];
                k++;
            }

            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // GET: Viajes/Create
        public IActionResult Create()
        {
            ViewData["CarroID"] = new SelectList(_context.Carros, "CarroID", "Placa");
            ViewData["ChoferID"] = new SelectList(_context.Choferes, "ChoferID", "Nombre");
            ViewData["RutaID"] = new SelectList(_context.Rutas, "RutaID", "NombreRuta");
            ViewData["ListaPasajero"] = _context.Pasajeros.ToList();
            return View();
            
        }

        

        // POST: Viajes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ViajeID,RutaID,ChoferID,CarroID,Identif,FechaViaje,Estado")] Viaje viaje, string operacion, string buscaPasajero, string PasajeroID, string opEPasajeroID, string[] Asiento)
        {
            if (operacion == "buscar")
            {

                ViewData["ListaPasajero"] = _context.Pasajeros.Where(c => c.NombrePasajero.Contains(buscaPasajero)).ToList();

            }
            

            else if (!string.IsNullOrWhiteSpace(PasajeroID))
            {

                ViewData["ListaPasajero"] = _context.Pasajeros.ToList();
                List<string> asientos = new List<string>();
                asientos.Add("1");
                asientos.Add("2");
                asientos.Add("3");
                asientos.Add("4");
                asientos.Add("5");
                asientos.Add("6");
                asientos.Add("7");
                asientos.Add("8");
                asientos.Add("9");
                asientos.Add("10");
                asientos.Add("11");
                asientos.Add("12");
                asientos.Add("13");
                asientos.Add("14");
                asientos.Add("15");

                
                ViewData["Asiento"] = asientos;

                DetalleViaje detalle = new DetalleViaje();
                detalle.PasajeroID = int.Parse(PasajeroID);
                var mPasajero = _context.Pasajeros.Where(c => c.PasajeroID == int.Parse(PasajeroID)).ToList();
                detalle.Pasajero = mPasajero[0];
                //Agrega un objeto de detalle matricula en memoria volatil
                _servicio.Agregar(detalle);
                ViewData["ListaDetalle"] = _servicio.ListaDetalleViaje.ToList();
                viaje.detalleViajes.Add(detalle);
               
            }
            else if (opEPasajeroID != null)
            {
                
                List<string> asientos = new List<string>();
                asientos.Add("1");
                asientos.Add("2");
                asientos.Add("3");
                asientos.Add("4");
                asientos.Add("5");
                asientos.Add("6");
                asientos.Add("7");
                asientos.Add("8");
                asientos.Add("9");
                asientos.Add("10");
                asientos.Add("11");
                asientos.Add("12");
                asientos.Add("13");
                asientos.Add("14");
                asientos.Add("15");

                
                ViewData["Grupo"] = asientos;


                ViewData["ListaPasajero"] = _context.Pasajeros.ToList();
                _servicio.Eliminar(int.Parse(opEPasajeroID));
                ViewData["ListaDetalle"] = _servicio.ListaDetalleViaje.ToList();
            }


            else if (ModelState.IsValid)
            {
                var listaDetalle = _servicio.ListaDetalleViaje.ToList();
                int ind = 0;

               
                foreach (var item in listaDetalle)
                {
                    DetalleViaje detalle = new DetalleViaje();
                    
                    detalle.PasajeroID = item.PasajeroID;
                    detalle.Asiento = Asiento[ind];
                    viaje.detalleViajes.Add(detalle);
                    ind++;
                }

                _context.Add(viaje);
                await _context.SaveChangesAsync(); 
                int d = 0; //Indice de eliminar
                foreach (var item in listaDetalle)
                {
                    _servicio.Eliminar(d);
                    d++;
                }
                return RedirectToAction(nameof(Index));
            }
         
               // ViewData["ListaPasajero"] = _context.Pasajeros.ToList();
            

            ViewData["CarroID"] = new SelectList(_context.Carros, "CarroID", "Placa", viaje.CarroID);
            ViewData["ChoferID"] = new SelectList(_context.Choferes, "ChoferID", "Nombre", viaje.ChoferID);
            ViewData["RutaID"] = new SelectList(_context.Rutas, "RutaID", "NombreRuta", viaje.RutaID);
           
            return View(viaje);
        }

        // GET: Viajes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje == null)
            {
                return NotFound();
            }
            ViewData["CarroID"] = new SelectList(_context.Carros, "CarroID", "Placa", viaje.CarroID);
            ViewData["ChoferID"] = new SelectList(_context.Choferes, "ChoferID", "Nombre", viaje.ChoferID);
            ViewData["RutaID"] = new SelectList(_context.Rutas, "RutaID", "NombreRuta", viaje.RutaID);
            return View(viaje);
        }

        // POST: Viajes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ViajeID,RutaID,ChoferID,CarroID,Identif,FechaViaje,Estado")] Viaje viaje)
        {
            if (id != viaje.ViajeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(viaje);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ViajeExists(viaje.ViajeID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CarroID"] = new SelectList(_context.Carros, "CarroID", "Placa", viaje.CarroID);
            ViewData["ChoferID"] = new SelectList(_context.Choferes, "ChoferID", "Nombre", viaje.ChoferID);
            ViewData["RutaID"] = new SelectList(_context.Rutas, "RutaID", "NombreRuta", viaje.RutaID);
            return View(viaje);
        }

        // GET: Viajes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Viajes == null)
            {
                return NotFound();
            }

            var viaje = await _context.Viajes
                .Include(v => v.Carro)
                .Include(v => v.Chofer)
                .Include(v => v.Ruta)
                .FirstOrDefaultAsync(m => m.ViajeID == id);
            if (viaje == null)
            {
                return NotFound();
            }

            return View(viaje);
        }

        // POST: Viajes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Viajes == null)
            {
                return Problem("Entity set 'ViajeContext.Viajes'  is null.");
            }
            var viaje = await _context.Viajes.FindAsync(id);
            if (viaje != null)
            {
                _context.Viajes.Remove(viaje);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ViajeExists(int id)
        {
          return _context.Viajes.Any(e => e.ViajeID == id);
        }
    }
}
