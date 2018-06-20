using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Line
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Display(Name = "From")]
        [StringLength(100)]
        [DataType(DataType.Text, ErrorMessage = "Should be in text format")]
        public String From { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Display(Name = "To")]
        [StringLength(100)]
        [DataType(DataType.Text, ErrorMessage = "Should be in text format")]
        public String To { get; set; }

        [Required]
        [Display(Name = "Line Price")]
        [DataType(DataType.Currency, ErrorMessage = "Should be in Currency format")]
        public Double Price { get; set; }

        [Display(Name = "Line Picture")]
        public String LinePicture { get; set; }

        public new string ToString => "From " + From + " To " + To;

    }
}