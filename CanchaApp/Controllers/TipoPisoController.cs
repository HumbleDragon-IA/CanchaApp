using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanchaApp.Modelo;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.IdentityModel.Tokens;

namespace CanchaApp.Controllers
{
    public class TipoPisoController : Controller
    {
        private readonly CanchaAppContext _context;

        public TipoPisoController(CanchaAppContext context)
        {
            _context = context;
        }

        // GET: TipoPiso
        public async Task<IActionResult> Index()
        {
              return _context.TipoPisos != null ? 
                          View(await _context.TipoPisos.ToListAsync()) :
                          Problem("Entity set 'CanchaAppContext.TipoPisos'  is null.");
        }

        // GET: TipoPiso/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.TipoPisos == null)
            {
                return NotFound();
            }

            var tipoPiso = await _context.TipoPisos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPiso == null)
            {
                return NotFound();
            }

            return View(tipoPiso);
        }

        // GET: TipoPiso/Create
        public IActionResult Create()
        {
            ViewBag.TipoPisos = obtenerTipoPiso();
            return View();
        }

        // POST: TipoPiso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoPiso1,tipoDePiso")] TipoPiso tipoPiso)
        {
            if(!tipoPiso.TipoPiso1.IsNullOrEmpty()) {

                if (ModelState.IsValid && !coincidePiso(tipoPiso.TipoPiso1))
                {
                    _context.Add(tipoPiso);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "El tipo de piso ya existe");
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "El tipo de piso no puede ser vacio");
            }
          
            
            ViewBag.TipoPisos = obtenerTipoPiso();
            return View(tipoPiso);
        }

        private Boolean coincidePiso(String tipoPiso) {
            var list = obtenerTipoPiso();
            Boolean coincide = false;
            foreach (var item in list)
            {
                if (item.TipoPiso1 == tipoPiso)
                {
                    coincide = true;
                }
            }
            return coincide;
        }

        // GET: TipoPiso/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.TipoPisos == null)
            {
                return NotFound();
            }

            var tipoPiso = await _context.TipoPisos.FindAsync(id);
            if (tipoPiso == null)
            {
                return NotFound();
            }
            ViewBag.TipoPisos = obtenerTipoPiso();
            return View(tipoPiso);
        }

        // POST: TipoPiso/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoPiso1,tipoDePiso")] TipoPiso tipoPiso)
        {
            if (id != tipoPiso.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tipoPiso);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TipoPisoExists(tipoPiso.Id))
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
            ViewBag.TipoPisos = obtenerTipoPiso();
            return View(tipoPiso);
        }

        // GET: TipoPiso/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.TipoPisos == null)
            {
                return NotFound();
            }

            var tipoPiso = await _context.TipoPisos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tipoPiso == null)
            {
                return NotFound();
            }

            return View(tipoPiso);
        }

        // POST: TipoPiso/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.TipoPisos == null)
            {
                return Problem("Entity set 'CanchaAppContext.TipoPisos'  is null.");
            }
            var tipoPiso = await _context.TipoPisos.FindAsync(id);
            var turnos = obtenerTurnoR();
            var canchas = obtenerCanchaR();

            if (tipoPiso != null)
            {
                foreach (var can in canchas)
                {
                    if (can.IdTipoPiso == tipoPiso.Id) {
                        foreach (var tur in turnos)
                        {
                            if(tur.IdCancha==can.Id)
                            _context.TurnoReservados.Remove(tur);
                        }

                        _context.Cancha.Remove(can);
                    }
                }
                             
                _context.TipoPisos.Remove(tipoPiso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPisoExists(int id)
        {
          return (_context.TipoPisos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
        public List<TurnoReservado> obtenerTurnoR()
        {
            return _context.TurnoReservados.ToList();
        }

        public List<Cancha> obtenerCanchaR()
        {
            return _context.Cancha.ToList();
        }
        public List<TipoPiso> obtenerTipoPiso()
        {
            return _context.TipoPisos.ToList();
        }
    }
}
