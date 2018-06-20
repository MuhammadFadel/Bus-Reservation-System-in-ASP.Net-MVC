using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using OnlineBusReservationV6.Models;
using System.Net.Mail;
using Microsoft.AspNet.Identity;

namespace OnlineBusReservationV6.api
{
    [Authorize(Roles ="Admin")]
    public class TicketsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Tickets
        public IQueryable<Ticket> GetTickets()
        {
            return db.Tickets;
        }

        //// GET: api/Tickets/5
        //[ResponseType(typeof(Ticket))]
        //public IHttpActionResult GetTicket(int id)
        //{
        //    Ticket ticket = db.Tickets.Find(id);
        //    if (ticket == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(ticket);
        //}

        //// PUT: api/Tickets/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutTicket(int id, Ticket ticket)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != ticket.Id)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(ticket).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TicketExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        //// POST: api/Tickets
        //[ResponseType(typeof(Ticket))]
        //public IHttpActionResult PostTicket(Ticket ticket)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Tickets.Add(ticket);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = ticket.Id }, ticket);
        //}


        // DELETE: api/Tickets/5        




        [Route("api/DeleteTicket/{pid}/{tid}")]        
        public IHttpActionResult DeleteTicket(int? pid, int tid)
        {

            if (pid == null)
            {
                return BadRequest();
            }
            PassengerTicketView PTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Payment).Include(c => c.Trip).Include(c => c.Trip.Bus).Include(c => c.Trip.Line).SingleOrDefault(c => c.Id == tid),
                Seats = db.Seats.Include(c => c.Passenger).Where(c => c.PassengerId == pid)
            };

            if (PTV == null)
            {
                return NotFound();
            }

            foreach (var Seat in PTV.Seats)
            {
                db.Seats.Remove(Seat);
            }

            db.Tickets.Remove(PTV.Ticket);

            db.SaveChanges();


            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("m.fadel3597@gmail.com");
                mail.To.Add(User.Identity.GetUserName());
                mail.Subject = "Delete Booking Test";

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch
            {
                return Ok();
            }


            //Here We should write Mail Notification Function contain the State of the ticket             
            return Ok();
        }

         
        [HttpDelete]
        [Route("api/BlockTicket/{pid}/{tid}")]
        public IHttpActionResult BlockTicket(int? pid, int tid)
        {
            if (pid == null)
            {
                return NotFound();
            }
            PassengerTicketView PTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Payment).Include(c => c.Trip).Include(c => c.Trip.Bus).Include(c => c.Trip.Line).SingleOrDefault(c => c.Id == tid)
            };

            if (PTV == null)
            {
                return NotFound();
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


            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("m.fadel3597@gmail.com");
                mail.To.Add(User.Identity.GetUserName());
                mail.Subject = "Block Booking Test";

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("", "");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch
            {
                return Ok();
            }


            //Here We should write Mail Notification Function contain the State of the ticket             
            return Ok();
        }


        [HttpDelete]
        [Route("api/DeleteTrip/{id}")]
        public IHttpActionResult DeleteTrip(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Trip trip = db.Trips.Find(id);
            if (trip == null)
            {
                return NotFound();
            }

            try
            {
                db.Trips.Remove(trip);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpDelete]
        [Route("api/DeleteBus/{id}")]
        public IHttpActionResult DeleteBus(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Bus bus = db.Buses.Find(id);
            if (bus == null)
            {
                return NotFound();
            }

            try
            {
                db.Buses.Remove(bus);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("api/DeleteDriver/{id}")]
        public IHttpActionResult DeleteDriver(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Driver driver = db.Drivers.Find(id);
            if (driver == null)
            {
                return NotFound();
            }

            try
            {
                db.Drivers.Remove(driver);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }


        [HttpDelete]
        [Route("api/DeleteLine/{id}")]
        public IHttpActionResult DeleteLine(int? id)
        {
            if (id == null)
            {
                return BadRequest();
            }
            Line line = db.Lines.Find(id);
            if (line == null)
            {
                return NotFound();
            }

            try
            {
                db.Lines.Remove(line);
                db.SaveChanges();
                return Ok();
            }
            catch
            {
                return BadRequest();
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool TicketExists(int id)
        {
            return db.Tickets.Count(e => e.Id == id) > 0;
        }
    }
}