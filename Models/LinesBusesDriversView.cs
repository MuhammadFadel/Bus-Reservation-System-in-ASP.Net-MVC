using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class LinesBusesDriversView
    {
        public IEnumerable<Driver> Drivers { get; set; }

        public IEnumerable<Trip> Trips { get; set; }

        public IEnumerable<Bus> Buses { get; set; }

        public ContactUsForm ContactUsForm { get; set; }
    }
}