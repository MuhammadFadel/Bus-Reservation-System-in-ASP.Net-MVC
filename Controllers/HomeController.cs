using OnlineBusReservationV6.Models;
using System;
using System.Data;
using System.Data.Entity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineBusReservationV6.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        public ActionResult Index()
        {
            LinesBusesDriversView LBDV = new LinesBusesDriversView
            {
                Trips = db.Trips.Include(c => c.Bus).Include(c => c.Bus.Driver).Include(c => c.Bus.BookedSeats).Include(c => c.Line).OrderByDescending(c => c.Id).Take(3),
                Buses = db.Buses.Include(c => c.BookedSeats).Include(c => c.Driver).OrderByDescending(c => c.Id).Take(3),
                Drivers = db.Drivers.OrderByDescending(c => c.Id).Take(3)
            };
            //Hngeb top 3 inserted Line, Top 3 Buses, Top 3 Drivers           

            return View(LBDV);
        }        

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Contact(ContactUsForm ContactUsForm)
        {           
            if (ModelState.IsValid)
            {
                db.ContactUsForms.Add(ContactUsForm);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View("Index", ContactUsForm);
        }        

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

    }    
}