using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Passenger
    {
        [Required]
        public int Id { get; set; }               

        [Required]
        public virtual ApplicationUser ApplicationUser { get; set; }           

        public List<Ticket> Tickets { get; set; }

        [Required]
        public bool Blocked { get; set; }
    }
}