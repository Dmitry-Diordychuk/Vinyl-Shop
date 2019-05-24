﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vinyl_Shop.Data;
using Vinyl_Shop.Extensions;
using Vinyl_Shop.Models;

namespace Vinyl_Shop.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _db;

        public HomeController(ApplicationDbContext db)
        {
            _db = db;
        }
        
        public async Task<IActionResult> Index()
        {
            var productList = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags).ToListAsync();
            return View(productList);
        }
  
        public async Task<IActionResult> Details(int id)
        {
            var product = await _db.Products.Include(m => m.ProductTypes).Include(m => m.SpecialTags).Where(m => m.Id == id).FirstOrDefaultAsync();


            return View(product);
        }

        [HttpPost, ActionName("Details")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DetailsPost(int id)
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if(listShoppingCart==null)
            {
                listShoppingCart = new List<int>();  
            }
            listShoppingCart.Add(id);
            HttpContext.Session.Set("ssShoppingCart", listShoppingCart);

            return RedirectToAction("Index", "Home", new { area = "Customer" });

        }

        public IActionResult Remove(int id)
        {
            List<int> listShoppingCart = HttpContext.Session.Get<List<int>>("ssShoppingCart");
            if(listShoppingCart.Count > 0)
            {
                if(listShoppingCart.Contains(id))
                {
                    listShoppingCart.Remove(id);
                }
            }

            HttpContext.Session.Set("ssShoppingCart", listShoppingCart);
            return RedirectToAction(nameof(Index));
        }
    }
}