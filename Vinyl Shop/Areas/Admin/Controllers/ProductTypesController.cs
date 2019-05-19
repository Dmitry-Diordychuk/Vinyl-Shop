using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vinyl_Shop.Data;
using Microsoft.EntityFrameworkCore;
using Vinyl_Shop.Models;

namespace Vinyl_Shop.Areas.Admin.Controllers
{
    [Area("Admin")]  // Добавления в админскую часть
    public class ProductTypesController : Controller
    {
        private readonly ApplicationDbContext _db;

        public ProductTypesController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.ProductTypes.ToList());
        }


        // Get create action method
        public IActionResult Create()
        {
            return View();
        }

        // Post create action method
        [HttpPost]
        [ValidateAntiForgeryToken]  // проверка токена
        public async Task<IActionResult> Create(ProductTypes productTypes)
        {
            if (ModelState.IsValid) // Если required проходит
            {
                _db.Add(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); //nameof что не печатать "Index" ошибки не проверяются
            }

            return View(productTypes);
        }

        // Get edit action method
        public async Task<IActionResult> Edit(int id)
        {
            if(id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if(productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Post edit action method
        [HttpPost]
        [ValidateAntiForgeryToken]  // проверка токена
        public async Task<IActionResult> Edit(int id, ProductTypes productTypes)
        {
            if(id != productTypes.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid) // Если required проходит
            {
                _db.Update(productTypes);
                await _db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(productTypes);
        }

        // Get Details action method
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Get delete action method
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var productType = await _db.ProductTypes.FindAsync(id);
            if (productType == null)
            {
                return NotFound();
            }
            return View(productType);
        }

        // Post delete action method
        [HttpPost, ActionName("Delete")] // ActionName отменяет совпадения параметров
        [ValidateAntiForgeryToken]  // проверка токена
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var productType = await _db.ProductTypes.FindAsync(id);
            _db.ProductTypes.Remove(productType);
            await _db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}