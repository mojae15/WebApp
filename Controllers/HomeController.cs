using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApp.Data;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace WebApp.Controllers {
    public class HomeController : Controller {

        private readonly WebAppContext _context;

        public HomeController( WebAppContext context ) {
            _context = context;

        }

        public IActionResult Index() {

            //Pass the amount of medarbejdere and opgaver to the view, as to be able to display them later
            int _medarbejderCount = _context.Medarbejder.Count();
            int _opgaverCount = _context.Opgaver.Count();
            ViewBag.medarbejderCount = _medarbejderCount;
            ViewBag.opgaverCount = _opgaverCount;
            List<Medarbejder> _medarbejderList = _context.Medarbejder.ToList();


            return View();
        }

        public IActionResult Privacy() {
            return View();
        }


        [ResponseCache( Duration = 0, Location = ResponseCacheLocation.None, NoStore = true )]
        public IActionResult Error() {
            return View( new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier } );
        }
    }
}