﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

using Sample.AspNetCore.Data;
using Sample.AspNetCore.Models;

using System.Linq;
using System.Threading.Tasks;

namespace Sample.AspNetCore.Controllers
{
    public class ProductsController : Controller
    {
        private readonly StoreDbContext context;


        public ProductsController(StoreDbContext context)
        {
            this.context = context;
        }


        // GET: Products/Create
        public IActionResult Create()
        {
            return View();
        }


        // POST: Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Reference,ImageUrl,ItemUrl,Type,Class,Price,DiscountAmount")]
            Product product)
        {
            if (!ModelState.IsValid)
            {
                return View(product);
            }

            this.context.Add(product);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // POST: Products/Delete/5
        [HttpPost]
        [ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await this.context.Products.FindAsync(id);
            this.context.Products.Remove(product);
            await this.context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


        // GET: Products/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = await this.context.Products
                .FirstOrDefaultAsync(m => m.ProductId == id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }


        // GET: Products/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var product = await this.context.Products.FindAsync(id);
            if (product == null)
                return NotFound();
            return View(product);
        }


        // POST: Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id,
            [Bind("Id,Name,Reference,ImageUrl,ItemUrl,Type,Class,Price,DiscountAmount")]
            Product product)
        {
            if (id != product.ProductId)
                return NotFound();

            if (ModelState.IsValid)
            {
                try
                {
                    this.context.Update(product);
                    await this.context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProductExists(product.ProductId))
                        return NotFound();
                    throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(product);
        }


        // GET: Products
        public async Task<IActionResult> Index()
        {
            return View(await this.context.Products.ToListAsync());
        }


        private bool ProductExists(int id)
        {
            return this.context.Products.Any(e => e.ProductId == id);
        }
    }
}
