using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanchaApp.Modelo;
using System.Configuration;

namespace CanchaApp.Controllers
{
    public class TurnoReservadoController : Controller
    {
        private readonly CanchaAppContext _context;

        public TurnoReservadoController(CanchaAppContext context)
        {
            _context = context;
        }


        // GET: TurnoReservadoes
        public async Task<IActionResult> Index(string orderBy)
        {
            var canchaAppContext = _context.TurnoReservados.Include(t => t.IdCanchaNavigation).Include(t => t.IdUsuarioNavigation).Include(t => t.IdCanchaNavigation.IdCapacidadNavigation).Include(t => t.IdCanchaNavigation.IdTipoPisoNavigation);
            var canchas = await canchaAppContext.ToListAsync();
            if (!string.IsNullOrEmpty(orderBy))
            {
                switch (orderBy)
                {

                    case "MayorPrecio":
                        canchas = canchas.OrderByDescending(o => o.IdCanchaNavigation.Precio).ToList();
                        break;
                    case "MenorPrecio":
                        canchas = canchas.OrderBy(o => o.IdCanchaNavigation.Precio).ToList();
                        break;

                }
            }
            return View(canchas);
            //return View(await canchaAppContext.ToListAsync());
        }

        // GET: TurnoReservadoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {

            if (id == null || _context.TurnoReservados == null)
            {
                return NotFound();
            }

            var turnoReservado = await _context.TurnoReservados
                .Include(t => t.IdCanchaNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .Include(t => t.IdCanchaNavigation.IdCapacidadNavigation)
                .Include(t => t.IdCanchaNavigation.IdTipoPisoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoReservado == null)
            {
                return NotFound();
            }

            return View(turnoReservado);
        }

        // GET: TurnoReservadoes/Create
        public IActionResult Create()
        {
            ViewData["IdCancha"] = new SelectList(_context.Cancha, "Id", "Id");
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id");
            ViewBag.Turnos = obtenerTurno();
            ViewBag.Canchas = obtenerCanchaAux();
            return View();
        }

        // POST: TurnoReservadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdTurno,IdCancha")] TurnoReservado turnoReservado)
        {
            // if (ModelState.IsValid)
            //  {
            _context.Add(turnoReservado);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            //   }
            ViewData["IdCancha"] = new SelectList(_context.Cancha, "Id", "Id", turnoReservado.IdCancha);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdUsuario);
            ViewBag.Turnos = obtenerTurno();
            ViewBag.Canchas = obtenerCanchaAux();
            //   return View(turnoReservado);
        }

        // GET: TurnoReservadoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TurnoReservados == null)
            {
                return NotFound();
            }

            var turnoReservado = await _context.TurnoReservados.FindAsync(id);
            if (turnoReservado == null)
            {
                return NotFound();
            }
            ViewData["IdCancha"] = new SelectList(_context.Cancha, "Id", "Id", turnoReservado.IdCancha);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdUsuario);
            ViewBag.Turnos = obtenerTurno();
            ViewBag.Canchas = obtenerCanchaAux();
            return View(turnoReservado);
        }

        // POST: TurnoReservadoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,IdUsuario,IdTurno,IdCancha")] TurnoReservado turnoReservado)
        {
            if (id != turnoReservado.Id)
            {
                return NotFound();
            }

            // if (ModelState.IsValid)
            {
                //  try
                // {
                _context.Update(turnoReservado);
                await _context.SaveChangesAsync();
                // }
                // catch (DbUpdateConcurrencyException)
                // {
                //    if (!TurnoReservadoExists(turnoReservado.Id))
                //   {
                //      return NotFound();
                //  }
                //  else
                //  {
                //      throw;
                // }
                // }
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCancha"] = new SelectList(_context.Cancha, "Id", "Id", turnoReservado.IdCancha);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdUsuario);
            ViewBag.Turnos = obtenerTurno();
            ViewBag.Canchas = obtenerCanchaAux();
            // return View(turnoReservado);
        }

        // GET: TurnoReservadoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TurnoReservados == null)
            {
                return NotFound();
            }

            var turnoReservado = await _context.TurnoReservados
                .Include(t => t.IdCanchaNavigation)
                .Include(t => t.IdUsuarioNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (turnoReservado == null)
            {
                return NotFound();
            }

            return View(turnoReservado);
        }

        // POST: TurnoReservadoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TurnoReservados == null)
            {
                return Problem("Entity set 'CanchaAppContext.TurnoReservados'  is null.");
            }
            var turnoReservado = await _context.TurnoReservados.FindAsync(id);
            if (turnoReservado != null)
            {
                _context.TurnoReservados.Remove(turnoReservado);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TurnoReservadoExists(int id)
        {
            return (_context.TurnoReservados?.Any(e => e.Id == id)).GetValueOrDefault();
        }



        public List<Turno> obtenerTurno()
        {
            return _context.Turnos.ToList();
        }

        public List<Cancha> obtenerCancha()
        {
            return _context.Cancha.ToList();
        }

        public List<CanchaAux> obtenerCanchaAux()
        {
            List<CanchaAux> canchaAuxes = new List<CanchaAux>();
            var canchas = obtenerCancha();
            var capacidades = _context.Capacidad.ToList();
            var pisos = _context.TipoPisos.ToList();

            foreach (var cancha in canchas)
            {
                canchaAuxes.Add(new CanchaAux()
                {
                    cancha = cancha,
                    capacidad = (int)(_context.Capacidad.FirstOrDefault(c => c.Id == cancha.IdCapacidad)?.Tamaño),
                    piso = _context.TipoPisos.FirstOrDefault(p => p.Id == cancha.IdTipoPiso)?.TipoPiso1

                });
            }
            return canchaAuxes;
        }
    }
    public class CanchaAux
    {
        public Cancha cancha { get; set; }
        public int capacidad { get; set; }
        public String piso { get; set; }


    }
}
