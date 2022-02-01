using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using WebApp.Data;
using WebApp.Models;

namespace WebApp.Controllers {
    public class OpgaverController : Controller {
        private readonly WebAppContext _context;

        public OpgaverController( WebAppContext context ) {
            _context = context;
        }

        // GET: Opgaver
        public async Task<IActionResult> Index() {
            return View( await _context.Opgaver.ToListAsync() );
        }

        // GET: Opgaver/Details/5
        public async Task<IActionResult> Details( int? id ) {
            if(id == null) {
                return NotFound();
            }

            var opgaver = await _context.Opgaver
                .FirstOrDefaultAsync( m => m.id == id );
            if(opgaver == null) {
                return NotFound();
            }

            return View( opgaver );
        }

        // GET: Opgaver/Create
        public IActionResult Create() {

            // Pass list of medarbejdere to the View, as to display them in a dropdown menu
            List<SelectListItem> _medarbejderList = _context.Medarbejder.OrderBy( m => m.id )
                                                    .Select( x => new SelectListItem { Value = x.id.ToString(), Text = x.navn } ).ToList();

            ViewBag.medarbejderList = _medarbejderList;
            return View();
        }

        // POST: Opgaver/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( [Bind( "id,navn,minAlder,medarbejder" )] Opgaver opgaver ) {

            // Find the actual medarbejder name based on the ID
            // Probably a better/nicer/faster/easier way to do this

            var medarbejdere = from m in _context.Medarbejder select m;
            var medarbejder = medarbejdere.Where( m => m.id.ToString().Equals(opgaver.medarbejder )).First() ;

            opgaver.medarbejder = medarbejder.navn;

            // Check if the alder of the medarbejder is above the minAlder of the opgave. If not, present the user with an error
            if(medarbejder.alder < opgaver.minAlder) {
                TempData["ErrorMessage"] = "Den valgte medarbejders alder skal værre større end eller lig med minimumns alderen\n";
                return RedirectToAction( "Create", "Opgaver" );
            }


            if(ModelState.IsValid) {
                
                _context.Add( opgaver );
                await _context.SaveChangesAsync();
                return RedirectToAction( nameof( Index ) );
            }
            return View( opgaver );
        }

        // GET: Opgaver/Edit/5
        public async Task<IActionResult> Edit( int? id ) {

            // Pass list of medarbejdere to the View, as to display them in a dropdown menu
            List<SelectListItem> _medarbejderList = _context.Medarbejder.OrderBy( m => m.id )
                                                    .Select( x => new SelectListItem { Value = x.id.ToString(), Text = x.navn } ).ToList();

            ViewBag.medarbejderList = _medarbejderList;


            if(id == null) {
                return NotFound();
            }

            var opgaver = await _context.Opgaver.FindAsync( id );
            if(opgaver == null) {
                return NotFound();
            }


            return View( opgaver );
        }

        // POST: Opgaver/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( int id, [Bind( "id,navn,minAlder,medarbejder" )] Opgaver opgaver ) {

            // Find the actual medarbejder name based on the ID
            // Probably a better/nicer/faster/easier way to do this

            var medarbejdere = from m in _context.Medarbejder select m;
            var medarbejder = medarbejdere.Where( m => m.id.ToString().Equals( opgaver.medarbejder ) ).First();

            opgaver.medarbejder = medarbejder.navn;

            if(id != opgaver.id) {
                return NotFound();
            }

            // Check if the alder of the medarbejder is above the minAlder of the opgave. If not, present the user with an error
            if(medarbejder.alder < opgaver.minAlder) {
                TempData["ErrorMessage"] = "Den valgte medarbejders alder skal værre større end eller lig med minimumns alderen\n";
                return RedirectToAction( "Edit", "Opgaver" );
            }

            if(ModelState.IsValid) {
                try {
                    _context.Update( opgaver );
                    await _context.SaveChangesAsync();
                }
                catch(DbUpdateConcurrencyException) {
                    if(!OpgaverExists( opgaver.id )) {
                        return NotFound();
                    }
                    else {
                        throw;
                    }
                }
                return RedirectToAction( nameof( Index ) );
            }
            return View( opgaver );
        }

        // GET: Opgaver/Delete/5
        public async Task<IActionResult> Delete( int? id ) {
            if(id == null) {
                return NotFound();
            }

            var opgaver = await _context.Opgaver
                .FirstOrDefaultAsync( m => m.id == id );
            if(opgaver == null) {
                return NotFound();
            }

            return View( opgaver );
        }

        // POST: Opgaver/Delete/5
        [HttpPost, ActionName( "Delete" )]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed( int id ) {
            var opgaver = await _context.Opgaver.FindAsync( id );
            _context.Opgaver.Remove( opgaver );
            await _context.SaveChangesAsync();
            return RedirectToAction( nameof( Index ) );
        }

        private bool OpgaverExists( int id ) {
            return _context.Opgaver.Any( e => e.id == id );
        }

    }
}
