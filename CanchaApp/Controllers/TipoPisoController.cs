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
            return View();
        }

        // POST: TipoPiso/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoPiso1,tipoDePiso")] TipoPiso tipoPiso)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tipoPiso);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tipoPiso);
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
            if (tipoPiso != null)
            {
                _context.TipoPisos.Remove(tipoPiso);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TipoPisoExists(int id)
        {
          return (_context.TipoPisos?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
