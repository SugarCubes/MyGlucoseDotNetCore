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
    public class ExerciseEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ExerciseEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: ExerciseEntries
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ExerciseEntries.Include(e => e.Patient);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: ExerciseEntries/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.Patient)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }

            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Create
        public IActionResult Create()
        {
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id");
            return View();
        }

        // POST: ExerciseEntries/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UserName,Name,Minutes,Steps,CreatedAt,UpdatedAt,Timestamp")] ExerciseEntry exerciseEntry)
        {
            if (ModelState.IsValid)
            {
                exerciseEntry.Id = Guid.NewGuid();
                _context.Add(exerciseEntry);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id", exerciseEntry.UserName);
            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries.SingleOrDefaultAsync(m => m.Id == id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id", exerciseEntry.UserName);
            return View(exerciseEntry);
        }

        // POST: ExerciseEntries/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,UserName,Name,Minutes,Steps,CreatedAt,UpdatedAt,Timestamp")] ExerciseEntry exerciseEntry)
        {
            if (id != exerciseEntry.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(exerciseEntry);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExerciseEntryExists(exerciseEntry.Id))
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
            ViewData["UserName"] = new SelectList(_context.Patients, "Id", "Id", exerciseEntry.UserName);
            return View(exerciseEntry);
        }

        // GET: ExerciseEntries/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var exerciseEntry = await _context.ExerciseEntries
                .Include(e => e.Patient)
                .SingleOrDefaultAsync(m => m.Id == id);
            if (exerciseEntry == null)
            {
                return NotFound();
            }

            return View(exerciseEntry);
        }

        // POST: ExerciseEntries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var exerciseEntry = await _context.ExerciseEntries.SingleOrDefaultAsync(m => m.Id == id);
            _context.ExerciseEntries.Remove(exerciseEntry);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExerciseEntryExists(Guid id)
        {
            return _context.ExerciseEntries.Any(e => e.Id == id);
        }
    }
}
