using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyGlucoseDotNetCore.Data;
using MyGlucoseDotNetCore.Models;
using MyGlucoseDotNetCore.Services.Interfaces;

namespace MyGlucoseDotNetCore.Controllers
{
    public class GlucoseEntriesController : Controller
    {
        private readonly ApplicationDbContext _context;

        

        public GlucoseEntriesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: GlucoseEntry
        public async Task<IActionResult> Index()
        {
            return View(await _context.GlucoseEntries.ToListAsync());
        }

        // GET: GlucoseEntry/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var GlucoseEntries = await _context.GlucoseEntries
                .SingleOrDefaultAsync(m => m.Id == id);
            if (GlucoseEntries == null)
            {
                return NotFound();
            }

            return View(GlucoseEntries);
        }

        // GET: GlucoseEntry/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GlucoseEntry/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,PatientUsername,Measurement,BeforeAfter,WhichMeal,Date,Timestamp")] GlucoseEntry GlucoseEntries)
        {
            if (ModelState.IsValid)
            {
                GlucoseEntries.Id = Guid.NewGuid();
                _context.Add(GlucoseEntries);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(GlucoseEntries);
        }

        // GET: GlucoseEntry/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var GlucoseEntries = await _context.GlucoseEntries.SingleOrDefaultAsync(m => m.Id == id);
            if (GlucoseEntries == null)
            {
                return NotFound();
            }
            return View(GlucoseEntries);
        }

        // POST: GlucoseEntry/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("Id,PatientUsername,Measurement,BeforeAfter,WhichMeal,Date,Timestamp")] GlucoseEntry GlucoseEntries)
        {
            if (id != GlucoseEntries.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(GlucoseEntries);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GlucoseEntriesExists(GlucoseEntries.Id))
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
            return View(GlucoseEntries);
        }

        // GET: GlucoseEntry/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var GlucoseEntries = await _context.GlucoseEntries
                .SingleOrDefaultAsync(m => m.Id == id);
            if (GlucoseEntries == null)
            {
                return NotFound();
            }

            return View(GlucoseEntries);
        }

        // POST: GlucoseEntry/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var GlucoseEntries = await _context.GlucoseEntries.SingleOrDefaultAsync(m => m.Id == id);
            _context.GlucoseEntries.Remove(GlucoseEntries);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GlucoseEntriesExists(Guid id)
        {
            return _context.GlucoseEntries.Any(e => e.Id == id);
        }
    }
}

        
        