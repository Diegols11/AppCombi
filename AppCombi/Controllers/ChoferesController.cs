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
    public class ChoferesController : Controller
    {
        private readonly ViajeContext _context;

        public ChoferesController(ViajeContext context)
        {
            _context = context;
        }

        // GET: Choferes
        public async Task<IActionResult> Index()
        {
              return _context.Choferes != null ? 
                          View(await _context.Choferes.ToListAsync()) :
                          Problem("Entity set 'ViajeContext.Choferes'  is null.");
        }

        // GET: Choferes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Choferes == null)
            {
                return NotFound();
            }

            var chofer = await _context.Choferes
                .FirstOrDefaultAsync(m => m.ChoferID == id);
            if (chofer == null)
            {
                return NotFound();
            }

            return View(chofer);
        }

        // GET: Choferes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Choferes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ChoferID,Nombre,Dni,Telefono,Correo,Dispo,Descripcion")] Chofer chofer)
        {
            if (ModelState.IsValid)
            {
                _context.Add(chofer);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(chofer);
        }

        // GET: Choferes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Choferes == null)
            {
                return NotFound();
            }

            var chofer = await _context.Choferes.FindAsync(id);
            if (chofer == null)
            {
                return NotFound();
            }
            return View(chofer);
        }

        // POST: Choferes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ChoferID,Nombre,Dni,Telefono,Correo,Dispo,Descripcion")] Chofer chofer)
        {
            if (id != chofer.ChoferID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(chofer);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ChoferExists(chofer.ChoferID))
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
            return View(chofer);
        }

        // GET: Choferes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Choferes == null)
            {
                return NotFound();
            }

            var chofer = await _context.Choferes
                .FirstOrDefaultAsync(m => m.ChoferID == id);
            if (chofer == null)
            {
                return NotFound();
            }

            return View(chofer);
        }

        // POST: Choferes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Choferes == null)
            {
                return Problem("Entity set 'ViajeContext.Choferes'  is null.");
            }
            var chofer = await _context.Choferes.FindAsync(id);
            if (chofer != null)
            {
                _context.Choferes.Remove(chofer);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ChoferExists(int id)
        {
          return (_context.Choferes?.Any(e => e.ChoferID == id)).GetValueOrDefault();
        }
    }
}
