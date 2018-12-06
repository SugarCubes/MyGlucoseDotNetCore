using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;

namespace MyGlucoseDotNetCore.Controllers
{
    [Authorize( Roles = Roles.DOCTOR )]
    public class MealItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MealItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MealItems
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MealItems.Include(m => m.Meal);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MealItems/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItem = await _context.MealItems
                .Include(m => m.Meal)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealItem == null)
            {
                return NotFound();
            }

            return View(mealItem);
        }

        // GET: MealItems/Create
        public IActionResult Create()
        {
            ViewData["MealId"] = new SelectList(_context.MealEntries, "Id", "Id");
            return View();
        }

        // POST: MealItems/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,MealId,Name,Carbs,Servings,UpdatedAt")] MealItem mealItem)
        {
            if (ModelState.IsValid)
            {
                mealItem.Id = Guid.NewGuid();
                _context.Add(mealItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["MealId"] = new SelectList(_context.MealEntries, "Id", "Id", mealItem.MealId);
            return View(mealItem);
        }

        // GET: MealItems/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItem = await _context.MealItems.SingleOrDefaultAsync(m => m.Id == id);
            if (mealItem == null)
            {
                return NotFound();
            }
            ViewData["MealId"] = new SelectList(_context.MealEntries, "Id", "Id", mealItem.MealId);
            return View(mealItem);
        }

        // POST: MealItems/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,MealId,Name,Carbs,Servings,UpdatedAt")] MealItem mealItem)
        {
            if (id != mealItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealItemExists(mealItem.Id))
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
            ViewData["MealId"] = new SelectList(_context.MealEntries, "Id", "Id", mealItem.MealId);
            return View(mealItem);
        }

        // GET: MealItems/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealItem = await _context.MealItems
                .Include(m => m.Meal)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealItem == null)
            {
                return NotFound();
            }

            return View(mealItem);
        }

        // POST: MealItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mealItem = await _context.MealItems.SingleOrDefaultAsync(m => m.Id == id);
            _context.MealItems.Remove(mealItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealItemExists(Guid id)
        {
            return _context.MealItems.Any(e => e.Id == id);
        }
    }
}
