using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using OnlineBusReservationV6.Models;
using System.IO;
using Microsoft.AspNet.Identity;

namespace OnlineBusReservationV6.Controllers
{
    [Authorize(Roles ="Admin")]
    public class AdminController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Admin
        public ActionResult Index()
        {
            DashboardViewModel DBVM = new DashboardViewModel
            {
                Trips = db.Trips.Include(c => c.Bus).Include(c => c.Line).ToList(),
                Feedbacks = db.Feedbacks.Include(c => c.PassengerId).Include(c => c.PassengerId.ApplicationUser).Include(c => c.TripId).ToList(),
                Buses = db.Buses.Count(),
                Passengers = db.Passengers.Count(),
                Tickets = db.Tickets.Count(),
                ContactUsForms = db.ContactUsForms.ToList()
            };
            return View(DBVM);
        }

        public ActionResult AdminProfile()
        {
            var AdminId = User.Identity.GetUserId();
            if (AdminId != null)
            {
                ApplicationUser admin = db.Users.SingleOrDefault(c => c.Id == AdminId);
                return View(admin);
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }
        public ActionResult Seats(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            BusSeatsView BSV = new BusSeatsView
            {
                Bus = db.Buses.Include(c => c.BookedSeats).SingleOrDefault(c => c.Id == id),
                Seats = db.Seats.Include(c => c.Passenger).Include(c => c.Passenger.ApplicationUser).ToList()
            };

            if (BSV == null)
            {
                return HttpNotFound();
            }
            return View(BSV);
        }

        public ActionResult EditSeat(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SeatPassengerView BSV = new SeatPassengerView
            {
                Seat = db.Seats.Include(c => c.Passenger).Include(c => c.Passenger.ApplicationUser).SingleOrDefault(c => c.Id == id),
                Passengers = db.Passengers.Include(c => c.ApplicationUser).ToList(),
            };

            if (BSV == null)
            {
                return HttpNotFound();
            }
            return View(BSV);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditSeat(Seat seat)
        {
            if (ModelState.IsValid)
            {
                if (seat.Id != 0)
                {
                    Seat seatInDB = db.Seats.Single(c => c.Id == seat.Id);
                    seatInDB.PassengerId = seat.PassengerId;
                    seatInDB.SeatNumber = seat.SeatNumber;
                    seatInDB.IsAvailable = seat.IsAvailable;

                    db.SaveChanges();
                    return RedirectToAction("Buses");
                }
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        public ActionResult Tickets()
        {
            PassengersTicketsView PTV = new PassengersTicketsView
            {
                Passengers = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).ToList(),
                Tickets = db.Tickets.Include(c => c.Trip.Bus).Include(c => c.Payment).Include(c => c.Trip).Include(c => c.Trip.Line).ToList(),
                Buses = db.Buses.Include(c=>c.Driver).Include(c=>c.BookedSeats).ToList()
            };                                    
            return View(PTV);
        }        

        [Route("Admin/ViewTicket/{pid}/{tid}")]
        public ActionResult ViewTicket(int? pid, int tid)
        {
            if (pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerTicketView PTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Payment).Include(c => c.Trip).Include(c => c.Trip.Bus).Include(c => c.Trip.Line).SingleOrDefault(c => c.Id == tid),
                Seats = db.Seats.Include(c => c.Passenger).Where(c=>c.PassengerId == pid)
            };                      

            if (PTV == null)
            {
                return HttpNotFound();
            }
            return View(PTV);
        }

        [Route("Admin/DeleteTicket/{pid}/{tid}")]
        public ActionResult DeleteTicket(int? pid, int tid)
        {
            if (pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerTicketView PTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Payment).Include(c => c.Trip).Include(c => c.Trip.Bus).Include(c => c.Trip.Line).SingleOrDefault(c => c.Id == tid),
                Seats = db.Seats.Include(c => c.Passenger).Where(c => c.PassengerId == pid)
            };

            if (PTV == null)
            {
                return HttpNotFound();
            }

            foreach (var Seat in PTV.Seats)
            {
                db.Seats.Remove(Seat);
            }

            db.Tickets.Remove(PTV.Ticket);

            db.SaveChanges();

            //Here We should write Mail Notification Function contain the State of the ticket 

            return RedirectToAction("ViewPassenger", new { pid = pid});
        }


        [Route("Admin/BlockTicket/{pid}/{tid}")]
        public ActionResult BlockTicket(int? pid, int tid)
        {
            if (pid == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerTicketView PTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Payment).Include(c => c.Trip).Include(c => c.Trip.Bus).Include(c => c.Trip.Line).SingleOrDefault(c => c.Id == tid)
            };

            if (PTV == null)
            {
                return HttpNotFound();
            }
            if (PTV.Ticket.IsBlocked)
            {
                PTV.Ticket.IsBlocked = false;
                IEnumerable<Seat> Seats = db.Seats.Where(c => c.PassengerId == pid);
                foreach (var Seat in Seats)
                {
                    Seat.IsAvailable = false;
                }
                
            }
            else
            {
                PTV.Ticket.IsBlocked = true;
                IEnumerable<Seat> Seats = db.Seats.Where(c => c.PassengerId == pid);
                foreach (var Seat in Seats)
                {
                    Seat.IsAvailable = true;
                }
            }
            db.SaveChanges();

            //Here We should write Mail Notification Function contain the State of the ticket 

            return RedirectToAction("ViewTicket", new { pid = pid, tid = tid });
        }


        public ActionResult Passengers()
        {
            return View(db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).ToList());
        }

        public ActionResult ViewPassenger(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PassengerUserTicketView PUTV = new PassengerUserTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == id),
                Tickets = db.Tickets.Include(c => c.Trip).Include(c => c.Trip.Line).Include(c => c.Payment).ToList()
            };

            if (PUTV.Passenger == null)
            {
                return HttpNotFound();
            }
            return View(PUTV);
        }

        //ElFunction m4 bt3ml SaveChanges(); m4 3arf leh rbna yostorha
        public ActionResult EditPassenger(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passengers.Include(c=>c.ApplicationUser).Include(c=>c.Tickets).SingleOrDefault(c=>c.Id == id);            

            if (passenger == null)
            {
                return HttpNotFound();
            }
                        
            return View(passenger);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditPassenger(Passenger passenger)
        {
            if (ModelState.IsValid)
            {
                if (passenger.Id != 0)
                {
                    Passenger passengerInDB = db.Passengers.Include(c=>c.ApplicationUser).Include(c=>c.Tickets).Single(c => c.Id == passenger.Id);                   
                    passengerInDB.Blocked = passenger.Blocked;

                    IEnumerable<Seat> Seats = db.Seats.Where(c => c.PassengerId == passenger.Id);
                    foreach(var Seat in Seats)
                    {
                        Seat.IsAvailable = passenger.Blocked;
                    }

                    db.SaveChanges();

                    return RedirectToAction("ViewPassenger", new { id = passenger.Id });
                }
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult DeletePassenger(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Passenger passenger = db.Passengers.Find(id);
            if (passenger == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Passengers.Remove(passenger);
                db.SaveChanges();
                return RedirectToAction("Passengers");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }

        public ActionResult Trips()
        {
            return View(db.Trips.Include(c => c.Line).Include(c => c.Bus).Include(c => c.Bus.BookedSeats).Include(c => c.Bus.Driver).ToList());
        }

        public ActionResult AddTrip()
        {
            TripLineBusView Trip = new TripLineBusView
            {
                Lines = db.Lines.ToList(),
                Buses = db.Buses.ToList()
            };

            return View("AddTrip", Trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddTrip(Trip trip, HttpPostedFileBase TripPictureFile)
        {
            if (ModelState.IsValid)
            {
                if (TripPictureFile != null)
                {
                    String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Trip_" + TripPictureFile.FileName));
                    TripPictureFile.SaveAs(path);
                    trip.TripPicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Trip_" + TripPictureFile.FileName);
                }
                db.Trips.Add(trip);
                db.SaveChanges();
                return RedirectToAction("Trips");
            }
            return View(trip);
        }

        public ActionResult ViewTrip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Trip trip = db.Trips.Include(c=>c.Line).Include(c=>c.Bus).Include(c => c.Bus.Driver).Include(c => c.Bus.BookedSeats).SingleOrDefault(c => c.Id == id);

            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        public ActionResult EditTrip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TripLineBusView trip = new TripLineBusView
            {
                Trip = db.Trips.Find(id),
                Lines = db.Lines.ToList(),
                Buses = db.Buses.ToList()
            };

            if (trip == null)
            {
                return HttpNotFound();
            }
            return View(trip);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditTrip(Trip trip, HttpPostedFileBase TripPictureFile)
        {
            if (ModelState.IsValid)
            {
                if (trip.Id != 0)
                {
                    Trip tripInDB = db.Trips.Single(c => c.Id == trip.Id);
                    if (TripPictureFile != null)
                    {
                        String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Trip_" + TripPictureFile.FileName));
                        TripPictureFile.SaveAs(path);
                        trip.TripPicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Trip_" + TripPictureFile.FileName);
                        tripInDB.TripPicture = trip.TripPicture;
                    }

                    tripInDB.LineId = trip.LineId;
                    tripInDB.BusId = trip.BusId;
                    tripInDB.Time = trip.Time;

                    db.SaveChanges();
                    return RedirectToAction("ViewTrip", new { id = trip.Id });
                }
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }


        public ActionResult DeleteTrip(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Trips.Remove(trip);
                db.SaveChanges();
                return RedirectToAction("Trips");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }

        public ActionResult Buses()
        {
            return View(db.Buses.Include(c => c.Driver).Include(c => c.BookedSeats).ToList());
        }

        public ActionResult AddBus()
        {
            BusDriverView bus = new BusDriverView
            {
                Drivers = db.Drivers.ToList()
            };
            return View("AddBus", bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddBus(Bus bus, HttpPostedFileBase BusPictureFile)
        {
            if (ModelState.IsValid)
            {
                if (BusPictureFile != null)
                {
                    String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Bus_" + BusPictureFile.FileName));
                    BusPictureFile.SaveAs(path);
                    bus.BusPicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Bus_" + BusPictureFile.FileName);
                }
                db.Buses.Add(bus);
                db.SaveChanges();
                return RedirectToAction("Buses");
            }
            return View(bus);
        }

        public ActionResult ViewBus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Bus bus = db.Buses.Include(c => c.Driver).Include(c => c.BookedSeats).SingleOrDefault(c => c.Id == id);

            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        public ActionResult EditBus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            BusDriverView bus = new BusDriverView
            {
                Bus = db.Buses.Find(id),
                Drivers = db.Drivers.ToList()
            };            
                        
            if (bus == null)
            {
                return HttpNotFound();
            }
            return View(bus);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditBus(Bus bus, HttpPostedFileBase BusPictureFile)
        {
            if (ModelState.IsValid)
            {
                if (bus.Id != 0)
                {
                    Bus busInDB = db.Buses.Single(c => c.Id == bus.Id);
                    if (BusPictureFile != null)
                    {
                        String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Bus_" + BusPictureFile.FileName));
                        BusPictureFile.SaveAs(path);
                        bus.BusPicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Bus_" + BusPictureFile.FileName);
                        busInDB.BusPicture = bus.BusPicture;
                    }

                    busInDB.Color = bus.Color;
                    busInDB.DriverId = bus.DriverId;
                    busInDB.BusNumber = bus.BusNumber;
                    busInDB.MaximumSeats = bus.MaximumSeats;

                    Driver driver = db.Drivers.Find(bus.DriverId);
                    driver.IsAvailable = false;

                    db.SaveChanges();
                    return RedirectToAction("ViewBus", new { id = bus.Id });
                }
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult DeleteBus(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Buses.Remove(bus);
                db.SaveChanges();
                return RedirectToAction("Buses");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }


        public ActionResult Drivers()
        {
            return View(db.Drivers.ToList());
        }

        public ActionResult AddDriver()
        {
            return View("AddDriver");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddDriver(Driver driver, HttpPostedFileBase ProfilePictureFile)
        {
            if (ModelState.IsValid)
            {
                if (ProfilePictureFile != null)
                {
                    String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ProfilePictureFile.FileName));
                    ProfilePictureFile.SaveAs(path);
                    driver.ProfilePicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ProfilePictureFile.FileName);
                }
                db.Drivers.Add(driver);
                db.SaveChanges();
                return RedirectToAction("Drivers");
            }
            return View(driver);
        }

        public ActionResult ViewDriver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.SingleOrDefault(c => c.Id == id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        public ActionResult EditDriver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }
            return View(driver);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditDriver(Driver driver, HttpPostedFileBase ProfilePictureFile)
        {
            if (ModelState.IsValid)
            {
                if (driver.Id != 0)
                {
                    Driver driverInDB = db.Drivers.Single(c => c.Id == driver.Id);

                    if (ProfilePictureFile != null)
                    {
                        String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ProfilePictureFile.FileName));
                        ProfilePictureFile.SaveAs(path);
                        driver.ProfilePicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_" + ProfilePictureFile.FileName);
                        driverInDB.ProfilePicture = driver.ProfilePicture;
                    }

                    driverInDB.Name = driver.Name;
                    driverInDB.PhoneNumber = driver.PhoneNumber;
                    driverInDB.SSN = driver.SSN;
                    driverInDB.DriverLicence = driver.DriverLicence;
                    driverInDB.EmailAddress = driver.EmailAddress;

                    db.SaveChanges();
                    return RedirectToAction("ViewDriver", new { id = driver.Id });
                }
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult DeleteDriver(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Drivers.Remove(driver);
                db.SaveChanges();
                return RedirectToAction("Drivers");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }


        
        public ActionResult Lines()
        {
            return View(db.Lines.ToList());
        }

        public ActionResult AddLine()
        {
            return View("AddLine");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddLine(Line line, HttpPostedFileBase LinePictureFile)
        {
            if (ModelState.IsValid)
            {
                if (LinePictureFile != null)
                {
                    String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Line_" + LinePictureFile.FileName));
                    LinePictureFile.SaveAs(path);
                    line.LinePicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Line_" + LinePictureFile.FileName);
                }
                db.Lines.Add(line);
                db.SaveChanges();
                return RedirectToAction("Lines");
            }
            return View(line);
        }

        public ActionResult ViewLine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = db.Lines.SingleOrDefault(c => c.Id == id);
            if (line == null)
            {
                return HttpNotFound();
            }
            return View(line);
        }

        public ActionResult EditLine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return HttpNotFound();
            }
            return View(line);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditLine(Line line, HttpPostedFileBase LinePictureFile)
        {
            if (ModelState.IsValid)
            {
                if (line.Id != 0)
                {
                    Line lineInDB = db.Lines.Single(c => c.Id == line.Id);
                    if (LinePictureFile != null)
                    {
                        String path = Path.Combine(Server.MapPath("~/Uploads"), (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Line_" + LinePictureFile.FileName));
                        LinePictureFile.SaveAs(path);
                        line.LinePicture = (DateTime.Now.ToString("yyyyMMddHHmmss") + "_Line_" + LinePictureFile.FileName);
                        lineInDB.LinePicture = line.LinePicture;
                    }

                    lineInDB.From = line.From;
                    lineInDB.To = line.To;
                    lineInDB.Price = line.Price;

                    db.SaveChanges();
                    return RedirectToAction("ViewLine", new { id = line.Id });
                }
                return HttpNotFound();
            }
            return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
        }

        public ActionResult DeleteLine(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return HttpNotFound();
            }

            try
            {
                db.Lines.Remove(line);
                db.SaveChanges();
                return RedirectToAction("Lines");
            }
            catch
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }


        }


        public ActionResult Feedbacks()
        {
            return View(db.Feedbacks.Include(c => c.PassengerId).Include(c => c.PassengerId.ApplicationUser).Include(c => c.TripId).Include(c => c.TripId.Line).OrderByDescending(c => c.Id).ToList());
        }

        public ActionResult ViewFeedback(int? id)
        {
            if(id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Feedback feedback = db.Feedbacks.Include(c=>c.PassengerId).Include(c => c.PassengerId.ApplicationUser).Include(c => c.TripId).Include(c => c.TripId.Bus).Include(c => c.TripId.Line).SingleOrDefault(c => c.Id == id);
            if (feedback == null)
            {
                return HttpNotFound();
            }
            return View(feedback);
        }

        public ActionResult ContactForms()
        {
            return View(db.ContactUsForms.OrderByDescending(c => c.Id).ToList());
        }

        public ActionResult ViewContactForm(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactUsForm contactUsForm = db.ContactUsForms.SingleOrDefault(c => c.Id == id);
            if (contactUsForm == null)
            {
                return HttpNotFound();
            }
            return View(contactUsForm);
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