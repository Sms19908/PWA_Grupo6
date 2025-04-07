using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SC_701_PAW_Lunes.Models;
using SC_701_PAW_Lunes.Data;

namespace SC_701_PAW_Lunes.Controllers
{
    public class InventoryController: Controller
    {
        private readonly PAWDbContext _context;

        public InventoryController(PAWDbContext context)
        {
            _context = context;
        }

        //Listado del inventario

        public async Task<IActionResult> Index()
        {
            return View(await _context.Inventory.ToListAsync());
        }


        //Vista de un articulo de inventario

        public async Task<IActionResult> Detalle(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null) return NotFound();
            return View(inventory);
        }

        //Creacion Inventario
        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Crear(Inventory inventory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        //Editar Inventario

        public async Task<IActionResult> Editar(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null) return NotFound();
            return View(inventory);
        }
        
        [HttpPost]
        public async Task<IActionResult> Editar(int id, Inventory inventory)
        {
            if (id != inventory.Id_Inv) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(inventory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(inventory);
        }

        //Eliminar Inventario
        public async Task<IActionResult> Eliminar(int id)
        {
            var inventory = await _context.Inventory.FindAsync(id);
            if (inventory == null) return NotFound();

            _context.Inventory.Remove(inventory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

    }
}
