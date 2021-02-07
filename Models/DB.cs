using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class DB : DbContext
    {
        public DB() : base("name=DBConnection")
        {
        }

        
        public DbSet<Tasks> Tasks { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            modelBuilder.Entity<Tasks>()
               .HasKey(entity => entity.ID);

        }
        public void _Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}