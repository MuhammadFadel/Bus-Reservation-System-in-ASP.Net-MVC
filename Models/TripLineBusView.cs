using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class TripLineBusView
    {
        public Trip Trip { get; set; }

        public List<Bus> Buses { get; set; }

        public List<Line> Lines { get; set; }
    }
}