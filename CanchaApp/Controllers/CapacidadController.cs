using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanchaApp.Modelo;

namespace CanchaApp.Controllers
{
    public class CapacidadController : Controller
    {
        private readonly CanchaAppContext _context;

        public CapacidadController(CanchaAppContext context)
        {
            _context = context;
        }

        // GET: Capacidad
        public async Task<IActionResult> Index()
        {
              return _context.Capacidad != null ? 
                          View(await _context.Capacidad.ToListAsync()) :
                          Problem("Entity set 'CanchaAppContext.Capacidad'  is null.");
        }

        // GET: Capacidad/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Capacidad == null)
            {
                return NotFound();
            }

            var capacidad = await _context.Capacidad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (capacidad == null)
            {
                return NotFound();
            }

            return View(capacidad);
        }

        // GET: Capacidad/Create
        public IActionResult Create()
        {
            ViewBag.Capacidades = obtenerCapacidad();
            return View();
        }

        // POST: Capacidad/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Tamaño")] Capacidad capacidad)
        {
            if (ModelState.IsValid)
            {
                _context.Add(capacidad);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewBag.Capacidades = obtenerCapacidad();
            return View(capacidad);
        }

        // GET: Capacidad/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Capacidad == null)
            {
                return NotFound();
            }

            var capacidad = await _context.Capacidad.FindAsync(id);
            if (capacidad == null)
            {
                return NotFound();
            }
            ViewBag.Capacidades = obtenerCapacidad();
            return View(capacidad);
        }

        // POST: Capacidad/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Tamaño")] Capacidad capacidad)
        {
            if (id != capacidad.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(capacidad);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CapacidadExists(capacidad.Id))
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
            ViewBag.Capacidades = obtenerCapacidad();
            return View(capacidad);
        }

        // GET: Capacidad/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Capacidad == null)
            {
                return NotFound();
            }

            var capacidad = await _context.Capacidad
                .FirstOrDefaultAsync(m => m.Id == id);
            if (capacidad == null)
            {
                return NotFound();
            }

            return View(capacidad);
        }

        // POST: Capacidad/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Capacidad == null)
            {
                return Problem("Entity set 'CanchaAppContext.Capacidad'  is null.");
            }
            var capacidad = await _context.Capacidad.FindAsync(id);
            var turnos = obtenerTurnoR();
            var canchas = obtenerCanchaR();
            if (capacidad != null)
            {
                foreach (var can in canchas)
                {
                    if (can.IdCapacidad == capacidad.Id)
                    {
                        foreach (var tur in turnos)
                        {
                            if (tur.IdCancha == can.Id)
                                _context.TurnoReservados.Remove(tur);
                        }

                        _context.Cancha.Remove(can);
                    }
                }


                _context.Capacidad.Remove(capacidad);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CapacidadExists(int id)
        {
          return (_context.Capacidad?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public List<TurnoReservado> obtenerTurnoR()
        {
            return _context.TurnoReservados.ToList();
        }

        public List<Cancha> obtenerCanchaR()
        {
            return _context.Cancha.ToList();
        }
        public List<Capacidad> obtenerCapacidad()
        {
            return _context.Capacidad.ToList();
        }
    }
}
