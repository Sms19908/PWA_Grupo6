using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC_701_PAW_Lunes.Models;

namespace SC_701_PAW_Lunes.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public UserController(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }

        // Listar usuarios
        [Authorize(Roles = "Administrador")]
        public async Task<IActionResult> Index()
        {
            var usuarios = await _userManager.Users.ToListAsync();
            return View(usuarios);
        }

        // Crear usuario - GET
        public async Task<IActionResult> Create()
        {
            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View();
        }

        // Crear usuario - POST
        [HttpPost]
        public async Task<IActionResult> Create(User usuario, string password)
        {
            if (ModelState.IsValid)
            {
                var result = await _userManager.CreateAsync(usuario, password);

                if (result.Succeeded)
                {
                    // Asignar el rol al usuario
                    await _userManager.AddToRoleAsync(usuario, usuario.Rol);
                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(usuario);
        }

        // Editar usuario - GET
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null) return NotFound();

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return NotFound();

            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(usuario);
        }

        // Editar usuario - POST
        [HttpPost]
        public async Task<IActionResult> Edit(string id, User usuario)
        {
            if (id != usuario.Id) return NotFound();

            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByIdAsync(id);
                if (existingUser == null) return NotFound();

                existingUser.NombreCompleto = usuario.NombreCompleto;
                existingUser.PhoneNumber = usuario.Telefono;
                existingUser.Email = usuario.Email;
                existingUser.UserName = usuario.UserName;

                var result = await _userManager.UpdateAsync(existingUser);

                if (result.Succeeded)
                {
                    // Actualizar el rol si es necesario
                    var currentRoles = await _userManager.GetRolesAsync(existingUser);
                    await _userManager.RemoveFromRolesAsync(existingUser, currentRoles);
                    await _userManager.AddToRoleAsync(existingUser, usuario.Rol);

                    return RedirectToAction(nameof(Index));
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewBag.Roles = _roleManager.Roles.Select(r => r.Name).ToList();
            return View(usuario);
        }

        // Eliminar usuario - GET
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null) return NotFound();

            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario == null) return NotFound();

            return View(usuario);
        }

        // Eliminar usuario - POST
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var usuario = await _userManager.FindByIdAsync(id);
            if (usuario != null)
            {
                await _userManager.DeleteAsync(usuario);
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
