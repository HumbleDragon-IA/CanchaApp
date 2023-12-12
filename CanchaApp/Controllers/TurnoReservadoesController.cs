using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CanchaApp.Modelo;
using System.Configuration;
using Microsoft.IdentityModel.Tokens;
using Microsoft.CodeAnalysis.CSharp.Syntax;

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
                    case "MayorHorario":
                        canchas = canchas.OrderByDescending(o => o.IdTurno).ToList();
                        break;
                    case "MenorHorario":
                        canchas = canchas.OrderBy(o => o.IdTurno).ToList();
                        break;
                }
            }
            ViewBag.Turnos = obtenerTurno();
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
            ViewData["IdTurno"] = new SelectList(_context.Turnos, "Id", "Id");
            ViewBag.Turnos = obtenerTurno();
            ViewBag.Canchas = obtenerCanchaAux();
            ViewBag.Usuarios = obtenerUsuario();
            obtenerTurnoReservado();

            return View();
        }

        public void obtenerTurnoReservado()
        {
            List<TurnoReservado> turnos = _context.TurnoReservados.ToList();
            List<int> idsTurno = turnos.Select(x => x.IdTurno).ToList();
            List<int> idsCancha = turnos.Select(x => x.IdCancha).ToList();
            ViewBag.idsTurnosReservados = idsTurno;
            ViewBag.idsCancha = idsCancha;
        }

        // POST: TurnoReservadoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,IdUsuario,IdTurno,IdCancha")] TurnoReservado turnoReservado)
        {

            if (!existeTurno(turnoReservado))
            {
                _context.Add(turnoReservado);
                await _context.SaveChangesAsync();
                ViewData["IdCancha"] = new SelectList(_context.Cancha, "Id", "Id", turnoReservado.IdCancha);
                ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdUsuario);
                ViewData["IdTurno"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdTurno);
                ViewBag.Turnos = obtenerTurno();
                ViewBag.Canchas = obtenerCanchaAux();
                ViewBag.Usuarios = obtenerUsuario();
                return RedirectToAction(nameof(Index));

            }
            ModelState.AddModelError(string.Empty, "El turno ya existe.");
            ViewData["IdCancha"] = new SelectList(_context.Cancha, "Id", "Id", turnoReservado.IdCancha);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdUsuario);
            ViewData["IdTurno"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdTurno);
            ViewBag.Turnos = obtenerTurno();
            ViewBag.Canchas = obtenerCanchaAux();
            ViewBag.Usuarios = obtenerUsuario();
            return View(turnoReservado);
        }

        private Boolean existeTurno(TurnoReservado tr)
        {
            Boolean existe = false;
            var turnos = _context.TurnoReservados.ToList();
            int i = 0;

            foreach (var tur in turnos)
            {
                if (tur.IdCancha == tr.IdCancha && tur.IdTurno == tr.IdTurno)
                {
                    existe = true;
                }
            }

            return existe;

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
            ViewBag.Usuarios = obtenerUsuario();
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

          
            {
                try
                {
                    _context.Update(turnoReservado);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                { Console.WriteLine("aca rompe"); }
                
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdCancha"] = new SelectList(_context.Cancha, "Id", "Id", turnoReservado.IdCancha);
            ViewData["IdUsuario"] = new SelectList(_context.Usuario, "Id", "Id", turnoReservado.IdUsuario);
            ViewBag.Turnos = obtenerTurno();
            ViewBag.Canchas = obtenerCanchaAux();
            ViewBag.Usuarios = obtenerUsuario();
          
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
                .Include(t => t.IdCanchaNavigation.IdCapacidadNavigation)
                .Include(t => t.IdCanchaNavigation.IdTipoPisoNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);

            var usuario = _context.Usuario.Where(u => u.Id == turnoReservado.IdUsuario);
 
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


        public TimeSpan obtenerHorario(int id)
        {
            var turnos = obtenerTurno();
            TimeSpan hora= new TimeSpan();
            foreach (var tur in turnos)
            {
                if (tur.Id== id)
                {
                    hora = tur.HoraInicio;
                }
            }

            return hora;
        }
        public List<Turno> obtenerTurno()
        {
            return _context.Turnos.ToList();
        }

        public List<Cancha> obtenerCancha()
        {
            return _context.Cancha.ToList();
        }

        public List<Usuario> obtenerUsuario()
        {
            return _context.Usuario.ToList();
        }

        public List<CanchaAux> obtenerCanchaAux()
        {
            List<CanchaAux> canchaAuxes = new List<CanchaAux>();
            var canchas = obtenerCancha();
            var capacidades = _context.Capacidad.ToList();
            var pisos = _context.TipoPisos.ToList();
            var precio = 0;
            foreach (var cancha in canchas)
            {
                canchaAuxes.Add(new CanchaAux()
                {
                    cancha = cancha,
                    capacidad = (int)(_context.Capacidad.FirstOrDefault(c => c.Id == cancha.IdCapacidad)?.Tamaño),
                    piso = _context.TipoPisos.FirstOrDefault(p => p.Id == cancha.IdTipoPiso)?.TipoPiso1,
                    precio = (double)cancha.Precio
                }) ;
            }
            return canchaAuxes;
        }

        
    }
    public class CanchaAux
    {
        public Cancha cancha { get; set; }
        public int capacidad { get; set; }
        public String piso { get; set; }

        public double precio { get; set; }
    }
}
