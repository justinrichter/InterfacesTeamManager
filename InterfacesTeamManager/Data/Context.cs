using InterfacesTeamManager.Models;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace InterfacesTeamManager.Data
{
    // My EF Context Class
    public class Context : DbContext
    {
        public DbSet<Employee> employees { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<Skill> Skills { get; set; }

        public Context()
        {
            Database.SetInitializer(new DatabaseInitializer());
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            // Table names will be based on the object names
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            // Using the fluent API to configure the precision and scale for the Employee's AverageRating
            modelBuilder.Entity<Employee>()
                .Property(cb => cb.AverageRating)
                .HasPrecision(5, 2);
        }
    }
}
