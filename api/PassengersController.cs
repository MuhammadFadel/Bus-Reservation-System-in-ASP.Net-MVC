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
    [Authorize]
    public class PassengersController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        [Route("api/CancelBooking/{pid}/{tid}")]
        [HttpDelete]
        [ResponseType(typeof(Passenger))]
        public IHttpActionResult CancelBooking(int pid, int tid)
        {
            if (pid == 0 || tid == 0)
            {
                return BadRequest();
            }

            PassengerTicketView PTV = new PassengerTicketView
            {
                Passenger = db.Passengers.Include(c => c.ApplicationUser).Include(c => c.Tickets).SingleOrDefault(c => c.Id == pid),
                Ticket = db.Tickets.Include(c => c.Payment).Include(c => c.Trip).Include(c => c.Trip.Line).Include(c => c.Trip.Bus).Include(c => c.Trip.Bus.BookedSeats).Single(c => c.Id == tid),
                Seats = db.Seats.Include(c => c.Passenger).Where(c => c.PassengerId == pid)
            };

            if (PTV.Passenger == null || PTV.Ticket == null || PTV.Seats == null)
            {
                return NotFound();
            }
            //Delete All Seats Before Ticket 
            foreach (var Seat in PTV.Seats)
            {
                if (PTV.Ticket.Trip.Bus.BookedSeats.Contains(Seat))
                {
                    db.Seats.Remove(Seat);
                }
            }

            if (PTV.Passenger.Tickets.Count == 1)
            {
                //34an lw mfe4 8er el ticket de w at3mlha Delete yrg3 tany User Not Passenger
                db.Tickets.Remove(PTV.Ticket);
                db.Passengers.Remove(PTV.Passenger);
            }
            else
            {
                db.Tickets.Remove(PTV.Ticket);
            }

            db.SaveChanges();


            try
            {
                MailMessage mail = new MailMessage();
                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");
                mail.From = new MailAddress("m.fadel3597@gmail.com");
                mail.To.Add(User.Identity.GetUserName());
                mail.Subject = "Cancel Booking Test";

                SmtpServer.Port = 587;
                SmtpServer.UseDefaultCredentials = true;
                SmtpServer.Credentials = new System.Net.NetworkCredential("Email", "Pass");
                SmtpServer.EnableSsl = true;
                SmtpServer.Send(mail);

            }
            catch
            {
                return Ok();
            }

            return Ok();
        }       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PassengerExists(int id)
        {
            return db.Passengers.Count(e => e.Id == id) > 0;
        }
    }
}