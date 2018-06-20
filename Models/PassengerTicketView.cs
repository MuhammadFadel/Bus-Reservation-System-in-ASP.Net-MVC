using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class PassengerTicketView
    {
        public Passenger Passenger { get; set; }

        public Ticket Ticket { get; set; }

        public IEnumerable<Seat> Seats { get; set; }

        public IEnumerable<Feedback> Feedbacks { get; set; }
    }
}