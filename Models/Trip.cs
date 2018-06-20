using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Trip
    {
        [Required]
        public int Id { get; set; }

        public Line Line { get; set; }
        [Required]
        [Display(Name = "Line")]
        public int LineId { get; set; }
        

        public virtual Bus Bus { get; set; }
        [Required]
        [Display(Name = "Bus")]
        public int BusId { get; set; }
        
        public String Time { get; set; }

        public String TripPicture { get; set; }

        public new string ToString => "From " + Line.From + " To " + Line.To;

    }
}