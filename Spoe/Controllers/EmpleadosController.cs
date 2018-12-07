using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Spoe.Models;
using Newtonsoft.Json;
using Spoe.Data;

namespace Spoe.Controllers
{
	#region ValidacionUsuario
	[Authorize]
    [Route("[controller]/[action]")]
    public class EmpleadosController : Controller
    {
        private readonly UserManager<Empleado> _userManager;
        private readonly SignInManager<Empleado> _signInManager;
        private readonly ILogger _logger;
		private readonly ApplicationDbContext _context;

		public EmpleadosController(
            UserManager<Empleado> userManager,
            SignInManager<Empleado> signInManager,
            ILogger<EmpleadosController> logger, ApplicationDbContext context)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
			_context = context;
        }

        [TempData]
        public string ErrorMessage { get; set; }


		// GET: Estudiantes
		public IActionResult Index()
		{
			var applicationDbContext = _context.Users;
			return View(applicationDbContext.ToList());
		}

		[HttpGet]
        [AllowAnonymous]
        public IActionResult Register(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                var user = new Empleado { UserName = model.Email,
					Email = model.Email, Apellidos =model.Apellidos, Cargo = model.Cargo, Cedula = model.Cedula,
					 Fecha = model.Fecha, FechaNacimiento = model.FechaNacimiento, Nombres = model.Nombres,
					TipoUsuario = model.TipoUsuario};
                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");


                    _logger.LogInformation("User created a new account with password.");
					return RedirectToAction(nameof(Index));

				}
                AddErrors(result);
            }

            // En caso de errores
            return View(model);
        }
		#endregion

		#region EliminarEmpleado
		// GET: Empleados/Eliminar/5
		public async Task<IActionResult> Delete(string id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var empleado = await _userManager.FindByIdAsync(id);
			if (empleado == null)
			{
				return NotFound();
			}

			return View(empleado);
		}

		// POST: Empleados/Eliminar/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			var empleado = await _userManager.FindByIdAsync(id);
			await _userManager.DeleteAsync(empleado);
			return RedirectToAction(nameof(Index));
		}
		#endregion

		#region Helpers

		private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        #endregion
    }
}
