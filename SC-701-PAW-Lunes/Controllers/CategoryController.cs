using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC_701_PAW_Lunes.Models;
using SC_701_PAW_Lunes.Data;

namespace SC_701_PAW_Lunes.Controllers
{
    public class CategoryController : Controller
    {
        private readonly PAWDbContext _context;

        public CategoryController(PAWDbContext context)
        {
            _context = context;
        }

        //Listado de las categorias
        public async Task<IActionResult> Categorias()
        {
            return View(await _context.Category.ToListAsync());
        }

        //Creacion Categorias
        public IActionResult CrearC()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CrearC(Category category)
        {
            if (ModelState.IsValid)
            {
                _context.Add(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //Editar Categoria

        public async Task<IActionResult> EditarC(int id)
        {
            var category = await _context.Inventory.FindAsync(id);
            if (category == null) return NotFound();
            return View(category);
        }

        [HttpPost]
        public async Task<IActionResult> EditarC(int id, Category category)
        {
            if (id != category.Id_Cat) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(category);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        //Eliminar Category
        public async Task<IActionResult> EliminarC(int id)
        {
            var category = await _context.Inventory.FindAsync(id);
            if (category == null) return NotFound();

            _context.Inventory.Remove(category);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
