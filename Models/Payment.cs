using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Payment
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [Display(Name = "Payment Type")]
        public String Method { get; set; }

    }
}