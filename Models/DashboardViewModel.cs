using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class DashboardViewModel
    {
        public int Passengers { get; set; }

        public int Tickets { get; set; }

        public IEnumerable<Trip> Trips { get; set; }

        public int Buses { get; set; }

        public IEnumerable<Feedback> Feedbacks { get; set; }

        public IEnumerable<ContactUsForm> ContactUsForms { get; set; }
    }
}