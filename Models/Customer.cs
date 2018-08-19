using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Name")]
        public String Name { get; set; }

        //public Boolean PremiumClient { get; set; }
        [Required]
        //	[Age]
        [Display(Name = "Age")]
        public int Age { get; set; }
        [Required]
        //	[Phone]
        [Display(Name = "Phone Number")]
        public long PhoneNumber { get; set; }
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public String Email { get; set; }
        [Required]
        [Display(Name = "Address")]
        public String Address { get; set; }
        [Required]
        [Display(Name = "City")]
        public string City { get; set; }
        
        public string LogId { get; set; }
  
    }
}