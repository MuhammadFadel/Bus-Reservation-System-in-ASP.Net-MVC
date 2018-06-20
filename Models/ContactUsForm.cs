using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class ContactUsForm
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "Max Length For Name 250 Characters")]
        public String Name { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [EmailAddress(ErrorMessage ="Please Insert a Valid Mail")]
        [Display(Name = "Email Address")]
        public String EmailAddress { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Phone(ErrorMessage = "Please Insert a Valid Phone")]
        [Display(Name = "Phone Number")]
        public String PhoneNumber { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [DataType(DataType.Text)]
        [StringLength(500, ErrorMessage ="Please Ensure That Your Message in Range 500 Characters")]
        [Display(Name="Message")]
        public String Message { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public String TimeStamp { get; set; }

        [Required]
        public bool IsReaded { get; set; }

        public ContactUsForm()
        {
            this.IsReaded = false;
            this.TimeStamp = DateTime.Now.ToString();
        }
    }

}