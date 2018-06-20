using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class BusSeatsView
    {
        public Bus Bus { get; set; }

        public List<Seat> Seats { get; set; }
        
    }
}