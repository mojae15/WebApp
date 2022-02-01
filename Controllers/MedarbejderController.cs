#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers
{
    public class MedarbejderController : Controller
    {
        private readonly WebAppContext _context;

        public MedarbejderController(WebAppContext context)
        {
            _context = context;
            
        }

        // GET: Medarbejder
        public async Task<IActionResult> Index()
        {
            return View(await _context.Medarbejder.ToListAsync());
        }

        // GET: Medarbejder/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medarbejder = await _context.Medarbejder
                .FirstOrDefaultAsync(m => m.id == id);
            if (medarbejder == null)
            {
                return NotFound();
            }

            return View(medarbejder);
        }

        // GET: Medarbejder/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Medarbejder/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("id,navn,telefonNummer,eMail,alder")] Medarbejder medarbejder)
        {
            if (ModelState.IsValid)
            {
                _context.Add(medarbejder);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(medarbejder);
        }

        // GET: Medarbejder/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medarbejder = await _context.Medarbejder.FindAsync(id);
            if (medarbejder == null)
            {
                return NotFound();
            }
            return View(medarbejder);
        }

        // POST: Medarbejder/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("id,navn,telefonNummer,eMail,alder")] Medarbejder medarbejder)
        {
            if (id != medarbejder.id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(medarbejder);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedarbejderExists(medarbejder.id))
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
            return View(medarbejder);
        }

        // GET: Medarbejder/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medarbejder = await _context.Medarbejder
                .FirstOrDefaultAsync(m => m.id == id);
            if (medarbejder == null)
            {
                return NotFound();
            }

            return View(medarbejder);
        }

        // POST: Medarbejder/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medarbejder = await _context.Medarbejder.FindAsync(id);
            _context.Medarbejder.Remove(medarbejder);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MedarbejderExists(int id)
        {
            return _context.Medarbejder.Any(e => e.id == id);
        }
    }
}
