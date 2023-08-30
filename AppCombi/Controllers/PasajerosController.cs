using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using AppCombi.Data;
using AppCombi.Models;

namespace AppCombi.Controllers
{
    public class PasajerosController : Controller
    {
        private readonly ViajeContext _context;

        public PasajerosController(ViajeContext context)
        {
            _context = context;
        }

        // GET: Pasajeros
        public async Task<IActionResult> Index()
        {
              return View(await _context.Pasajeros.ToListAsync());
        }

        // GET: Pasajeros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pasajeros == null)
            {
                return NotFound();
            }

            var pasajero = await _context.Pasajeros
                .FirstOrDefaultAsync(m => m.PasajeroID == id);
            if (pasajero == null)
            {
                return NotFound();
            }

            return View(pasajero);
        }

        // GET: Pasajeros/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pasajeros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PasajeroID,NombrePasajero,Edad,DniPasajero")] Pasajero pasajero)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pasajero);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pasajero);
        }

        // GET: Pasajeros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pasajeros == null)
            {
                return NotFound();
            }

            var pasajero = await _context.Pasajeros.FindAsync(id);
            if (pasajero == null)
            {
                return NotFound();
            }
            return View(pasajero);
        }

        // POST: Pasajeros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PasajeroID,NombrePasajero,Edad,DniPasajero")] Pasajero pasajero)
        {
            if (id != pasajero.PasajeroID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pasajero);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PasajeroExists(pasajero.PasajeroID))
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
            return View(pasajero);
        }

        // GET: Pasajeros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pasajeros == null)
            {
                return NotFound();
            }

            var pasajero = await _context.Pasajeros
                .FirstOrDefaultAsync(m => m.PasajeroID == id);
            if (pasajero == null)
            {
                return NotFound();
            }

            return View(pasajero);
        }

        // POST: Pasajeros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pasajeros == null)
            {
                return Problem("Entity set 'ViajeContext.Pasajeros'  is null.");
            }
            var pasajero = await _context.Pasajeros.FindAsync(id);
            if (pasajero != null)
            {
                _context.Pasajeros.Remove(pasajero);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PasajeroExists(int id)
        {
          return _context.Pasajeros.Any(e => e.PasajeroID == id);
        }
    }
}
