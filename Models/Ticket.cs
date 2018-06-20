using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Ticket
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Trip")]
        public Trip Trip { get; set; }

        [Required]
        [Display(Name = "Booking Time")]
        public String BookingTime { get; set; }

        [Required]
        public bool IsBlocked { get; set; }

        public virtual Payment Payment { get; set; }

        [Required]
        public int PaymentId { get; set; }
        

    }
}