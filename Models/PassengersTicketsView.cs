using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class PassengersTicketsView
    {
        public List<Passenger> Passengers { get; set; }

        public List<Ticket> Tickets { get; set; }

        public Passenger SpecificPassenger { get; set; }

        public List<Bus> Buses { get; set; }

        public Bus SpecificBus { get; set; }
    }
}