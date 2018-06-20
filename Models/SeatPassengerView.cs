using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class SeatPassengerView
    {
        public List<Passenger> Passengers { get; set; }

        public List<ApplicationUser> Users { get; set; }

        public Seat Seat { get; set; }
    }
}