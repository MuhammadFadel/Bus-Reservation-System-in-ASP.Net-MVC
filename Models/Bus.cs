using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Bus
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [StringLength(250)]
        [Display(Name = "Bus Color")]
        [DataType(DataType.Text, ErrorMessage = "This Field Is Required, Enter Bus Color in Text Pattern Only")]
        public String Color { get; set; }
        
        [Display(Name = "Driver Name")]
        public Driver Driver { get; set; }

        
        [Display(Name = "Driver Name")]
        public int DriverId { get; set; }

        [Required]
        [Display(Name = "Bus Number")]
        public String BusNumber { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Display(Name = "Maximum Seats Number")]
        [Range(10, 100)]
        public int MaximumSeats { get; set; }

        public List<Seat> BookedSeats { get; set; }

        [Display(Name ="Bus Picture")]
        public String BusPicture { get; set; }
    }
}