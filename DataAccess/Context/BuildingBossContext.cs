using Microsoft.EntityFrameworkCore;
using Domain;

namespace DataAccess.Context
{
    public class BuildingBossContext: DbContext
    {
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
        public virtual DbSet<Person> People { get; set; }
        public virtual DbSet<User> Users { get; set; } 
        public virtual DbSet<Request> Requests { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        public BuildingBossContext() { }

        public BuildingBossContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Request>()
            .HasOne(r => r.AssignedEmployee)
            .WithMany()
            .HasForeignKey(r => r.AssignedEmployeeId)
            .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
