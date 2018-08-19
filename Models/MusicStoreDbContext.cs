using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace MusicStore.Models
{
    public class MusicStoreDbContext : DbContext
    {
        public DbSet<Branch> branches { get; set; }
        public DbSet<Customer> customers { get; set; }
        public DbSet<Item> items { get; set; }
    }
}
