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
    public class CanchaController : Controller
    {
        private readonly CanchaAppContext _context;

        public CanchaController(CanchaAppContext context)
        {
            _context = context;
        }

        // GET: Cancha
        public async Task<IActionResult> Index()
        {
            var canchaAppContext = _context.Cancha.Include(c => c.IdCapacidadNavigation).Include(c => c.IdTipoPisoNavigation);
            return View(await canchaAppContext.ToListAsync());
        }

        // GET: Cancha/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Cancha == null)
            {
                return NotFound();
            }

            var cancha = await _context.Cancha
                .Include(c => c.IdCapacidadNavigation)
                .Include(c => c.IdTipoPisoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancha == null)
            {
                return NotFound();
            }

            return View(cancha);
        }

        // GET: Cancha/Create
        public IActionResult Create()
        {
            ViewData["IdCapacidad"] = new SelectList(_context.Capacidad, "Id", "Id");
            ViewData["IdTipoPiso"] = new SelectList(_context.TipoPisos, "Id", "Id");
            ViewBag.Capacidades = obtenerCapacidad();
            ViewBag.TipoPisos = obtenerTipoPiso();
            return View();
        }

        // POST: Cancha/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdCapacidad,IdTipoPiso,Precio")] Cancha cancha)
        {
           // if (ModelState.IsValid)
          //  {
                _context.Add(cancha);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            //}
            ViewData["IdCapacidad"] = new SelectList(_context.Capacidad, "Id", "Id", cancha.IdCapacidadNavigation.Tamaño);
            ViewData["IdTipoPiso"] = new SelectList(_context.TipoPisos, "Id", "Id", cancha.IdTipoPisoNavigation.TipoPiso1);
            ViewBag.Capacidades = obtenerCapacidad();
            ViewBag.TipoPisos = obtenerTipoPiso();
         //   return View(cancha);
        }

        // GET: Cancha/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Cancha == null)
            {
                return NotFound();
            }

            var cancha = await _context.Cancha.FindAsync(id);
            if (cancha == null)
            {
                return NotFound();
            }
            ViewData["IdCapacidad"] = new SelectList(_context.Capacidad, "Id", "Id", cancha.IdCapacidad);
            ViewData["IdTipoPiso"] = new SelectList(_context.TipoPisos, "Id", "Id", cancha.IdTipoPiso);
            ViewBag.Capacidades = obtenerCapacidad();
            ViewBag.TipoPisos = obtenerTipoPiso();

            return View(cancha);
        }

        // POST: Cancha/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdCapacidad,IdTipoPiso,Precio")] Cancha cancha)
        {
            if (id != cancha.Id)
            {
                return NotFound();
            }

          //  if (ModelState.IsValid)
            {
             //   try
               // {
                    _context.Update(cancha);
                    await _context.SaveChangesAsync();
               // }
              //  catch (DbUpdateConcurrencyException)
              //  {
                //    if (!CanchaExists(cancha.Id))
               //     {
                //        return NotFound();
                //    }
                //    else
                  //  {
                  //      throw;
                   // }
              //  }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCapacidad"] = new SelectList(_context.Capacidad, "Id", "Id", cancha.IdCapacidad);
            ViewData["IdTipoPiso"] = new SelectList(_context.TipoPisos, "Id", "Id", cancha.IdTipoPiso);
            ViewBag.Capacidades = obtenerCapacidad();
            ViewBag.TipoPisos = obtenerTipoPiso();
            //  return View(cancha);
        }

        // GET: Cancha/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Cancha == null)
            {
                return NotFound();
            }

            var cancha = await _context.Cancha
                .Include(c => c.IdCapacidadNavigation)
                .Include(c => c.IdTipoPisoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (cancha == null)
            {
                return NotFound();
            }

            return View(cancha);
        }

        // POST: Cancha/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Cancha == null)
            {
                return Problem("Entity set 'CanchaAppContext.Cancha'  is null.");
            }

            var cancha = await _context.Cancha.FindAsync(id);
            var turnos = obtenerTurnoR();
            if (cancha != null)
            {
                foreach (var tur in turnos)
                {
                    if (tur.IdCancha==cancha.Id)
                    {
                        _context.TurnoReservados.Remove(tur);
                    }
                }

                _context.Cancha.Remove(cancha);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CanchaExists(int id)
        {
          return (_context.Cancha?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        public List<TurnoReservado> obtenerTurnoR()
        {
            return _context.TurnoReservados.ToList();
        }

        public List<Capacidad> obtenerCapacidad()
        {
            return _context.Capacidad.ToList();
        }
        public List<TipoPiso> obtenerTipoPiso()
        {
            return _context.TipoPisos.ToList();
        }
    }
}
