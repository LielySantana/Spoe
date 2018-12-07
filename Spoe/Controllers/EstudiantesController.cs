using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Spoe.Data;

namespace Spoe.Controllers
{
	[Authorize]
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;
		private readonly UserManager<Empleado> _userManager;

		public EstudiantesController(ApplicationDbContext context, UserManager<Empleado>userManager)
        {
            _context = context;
			_userManager = userManager;
        }

		#region VinculoEstudianteBD
		// GET: Estudiantes
		public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Estudiantes.Include(e => e.Empleado);
            return View(await applicationDbContext.ToListAsync());
        }
		#endregion

		#region VerEstudiante

		// GET: Estudiantes/Detalles/5
		public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }
		#endregion

		#region AgregarEstudiante

		// GET: Estudiantes/Añadir
		public IActionResult Create()
        {
            //ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id");
            return View();
        }

        // POST: Estudiantes/Añadir
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nombres,Apellidos,GradoCursar,NombreTutor,TelefonoTutor,Direccion,Documentos,Fecha,EmpleadoId")] Estudiante estudiante)
        {
            if (ModelState.IsValid)
            {
				var empleado = await _userManager.GetUserAsync(User);
				estudiante.EmpleadoId = empleado.Id;
                _context.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id", estudiante.EmpleadoId);
            return View(estudiante);
        }
		#endregion

		#region EditarEstudiante

		// GET: Estudiantes/Editar/5
		public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes.FindAsync(id);
            if (estudiante == null)
            {
                return NotFound();
            }
            ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id", estudiante.EmpleadoId);
            return View(estudiante);
        }

        // POST: Estudiantes/Editar/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nombres,Apellidos,GradoCursar,NombreTutor,TelefonoTutor,Direccion,Documentos,Fecha")] Estudiante estudiante)
        {
            if (id != estudiante.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
					estudiante.EmpleadoId = _context.Estudiantes.Where(e=>e.Id==estudiante.Id).Select(e=>e.EmpleadoId).SingleOrDefault();
                    _context.Update(estudiante);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EstudianteExists(estudiante.Id))
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
            ViewData["EmpleadoId"] = new SelectList(_context.Users, "Id", "Id", estudiante.EmpleadoId);
            return View(estudiante);
        }
		#endregion

		#region EliminarEstudiante
		// GET: Estudiantes/Eliminar/5
		public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var estudiante = await _context.Estudiantes
                .Include(e => e.Empleado)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (estudiante == null)
            {
                return NotFound();
            }

            return View(estudiante);
        }
		

		// POST: Estudiantes/Eliminar/5
		[HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var estudiante = await _context.Estudiantes.FindAsync(id);
            _context.Estudiantes.Remove(estudiante);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EstudianteExists(int id)
        {
            return _context.Estudiantes.Any(e => e.Id == id);
        }
		#endregion
	}
}
