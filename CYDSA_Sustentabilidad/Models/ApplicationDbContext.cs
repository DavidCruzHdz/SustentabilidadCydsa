using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;

namespace CYDSA_Sustentabilidad.Controllers
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<IdsMediciones> Mediciones { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Entity<IdsMediciones>().ToTable("IdsMediciones");

            base.OnModelCreating(modelBuilder);
        }
    }
}