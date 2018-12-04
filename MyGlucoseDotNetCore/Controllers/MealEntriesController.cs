using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;

namespace MyGlucoseDotNetCore.Controllers
{
    public class MealEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MealEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: MealEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.MealEntries.Include(m => m.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: MealEntries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntry = await _context.MealEntries
                .Include(m => m.Patient)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealEntry == null)
            {
                return NotFound();
            }

            return View(mealEntry);
        }

        // GET: MealEntries/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id");
            return View();
        }

        // POST: MealEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,TotalCarbs,CreatedAt,UpdatedAt,Timestamp")] MealEntry mealEntry)
        {
            if (ModelState.IsValid)
            {
                mealEntry.Id = Guid.NewGuid();
                _context.Add(mealEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id", mealEntry.UserName);
            return View(mealEntry);
        }

        // GET: MealEntries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntry = await _context.MealEntries.SingleOrDefaultAsync(m => m.Id == id);
            if (mealEntry == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id", mealEntry.UserName);
            return View(mealEntry);
        }

        // POST: MealEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,TotalCarbs,CreatedAt,UpdatedAt,Timestamp")] MealEntry mealEntry)
        {
            if (id != mealEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mealEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MealEntryExists(mealEntry.Id))
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
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id", mealEntry.UserName);
            return View(mealEntry);
        }

        // GET: MealEntries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mealEntry = await _context.MealEntries
                .Include(m => m.Patient)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (mealEntry == null)
            {
                return NotFound();
            }

            return View(mealEntry);
        }

        // POST: MealEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var mealEntry = await _context.MealEntries.SingleOrDefaultAsync(m => m.Id == id);
            _context.MealEntries.Remove(mealEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MealEntryExists(Guid id)
        {
            return _context.MealEntries.Any(e => e.Id == id);
        }
    }
}
