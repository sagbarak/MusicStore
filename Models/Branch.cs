using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Branch
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "StoreName")]
        public String StoreName { get; set; }


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
        public IList<Item> items { get; set; }
    }
}