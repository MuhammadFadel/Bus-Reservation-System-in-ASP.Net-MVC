using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OnlineBusReservationV6.Models
{
    public class Driver
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Display(Name = "Driver Name")]
        [StringLength(100, MinimumLength = 5)]
        [DataType(DataType.Text, ErrorMessage ="This Field Is Required, Enter Your Name in Text Pattern Only")]
        public String Name { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Display(Name = "Identity Number")]
        [StringLength(14, MinimumLength = 14)]
        [DataType(DataType.Text, ErrorMessage = "This Field Is Required, Enter Your Identity Number in Text Pattern Only")]
        public String SSN { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Display(Name="Driver Licence")]
        [DataType(DataType.Text, ErrorMessage = "This Field Is Required, Enter Your Driving Licence Number in Text Pattern Only")]
        public String DriverLicence { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [Display(Name = "Phone Number")]
        [StringLength(11, ErrorMessage ="Enter Valid Phone Number", MinimumLength =11)]
        [DataType(DataType.PhoneNumber, ErrorMessage = "This Field Is Required, Enter Your Phone Number in Phone Pattern Only")]
        [Phone]        
        public String PhoneNumber { get; set; }

        [Required(ErrorMessage = "This Field Is Required")]
        [EmailAddress(ErrorMessage ="Enter a Valid Email Address")]
        [Display(Name = "Email Address")]
        [DataType(DataType.EmailAddress, ErrorMessage = "This Field Is Required, Enter Your Email Address.")]
        public String EmailAddress { get; set; }

        [Display(Name = "Profile Picture")]                
        public String ProfilePicture { get; set; }

        [Required]
        public bool IsAvailable { get; set; }

        public Driver()
        {
            IsAvailable = true;
        }

    }
}