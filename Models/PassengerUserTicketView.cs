using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class PassengerUserTicketView
    {
        public Passenger Passenger { get; set; }

        public List<Ticket> Tickets { get; set; }

        public List<Line> Lines { get; set; }

        public List<Trip> Tips { get; set; }

        public List<Bus> Buses { get; set; }
    }
}