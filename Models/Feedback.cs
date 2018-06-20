using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Feedback
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [StringLength(500, ErrorMessage ="You Message Should Be Less Than 500 Characters")]
        [DataType(DataType.Text, ErrorMessage = "This Field Is Required")]
        public String FeedbackMessage { get; set; }
        
        public Passenger PassengerId { get; set; }
        
        public Trip TripId { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        public String Timestamp { get; set; }

        [Required]
        public bool IsReadeds { get; set; }

        public Feedback()
        {
            this.IsReadeds = false;
            this.Timestamp = DateTime.Now.ToString();
        }
    }
}