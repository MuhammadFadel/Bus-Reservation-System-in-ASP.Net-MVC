using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class BookingViewModel
    {               
        public List<Trip> Trips { get; set; }

        public int Trip { get; set; }

        [Display(Name ="Number Of Chairs")]
        public int NumberOfChairs { get; set; }

        public String PaymentMethod { get; set; }

    }

    public class BookingTicket
    {
        public int Trip { get; set; }

        public String PaymentMethod { get; set; }

        public int NumberOfChairs { get; set; }
        
    }
}