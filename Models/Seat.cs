using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Seat
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        [Required]
        [Display(Name = "Seat Number")]
        [Range(1, 100)]
        public int SeatNumber { get; set; }

        public virtual Passenger Passenger { get; set; }

        [Required]
        [Display(Name = "Passenger Name")]
        public int PassengerId { get; set; }        

        [Required]
        [Display(Name = "Booked Time")]
        public String Time { get; set; }

        public Seat()
        {
            this.IsAvailable = true;
            this.Time = DateTime.Now.ToString();
        }

    }
}