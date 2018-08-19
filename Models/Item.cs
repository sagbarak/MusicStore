using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class Item
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Serial Number")]
        public int SerialNumber { get; set; }

        [Required]
        [Display(Name = "Album Name")]
        public String Name { get; set; }

        [Required]
        [Display(Name = "Artist")]
        public String Artist { get; set; }

        [Required]
        [Display(Name = "Genre")]
        public String Genre { get; set; }

        [Required]
        [Display(Name = "Year")]
        public int Year { get; set; }

        public String Description { get; set; }
        [Required]
        [Display(Name = "Price")]
        public float Price { get; set; }

        public string StoreName { set; get; }
        [Required]
        [Display(Name = "Branch")]
        public int BranchId { set; get; }
        public Branch branch { set; get; }
        [Required]
        [Display(Name="Sales")]
        public int Sales { set; get; }
        //		public int CustomerId { set; get; }
        //		public Customer customer { set; get; }

        public enum searcher
        {
            Name,
            Artist
        }
    }
}