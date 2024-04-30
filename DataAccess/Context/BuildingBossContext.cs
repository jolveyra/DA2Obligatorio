using Microsoft.EntityFrameworkCore;
using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Context
{
    public class BuildingBossContext: DbContext
    {
        public virtual DbSet<Building> Buildings { get; set; }
        public virtual DbSet<Flat> Flats { get; set; }
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

            //modelBuilder.Entity<Building>()
            // .HasMany(b => b.MaintenanceEmployees)
            // .WithMany()
            // .HasOne(b => b.Manager)
            // .WithMany()
            // .OnDelete(DeleteBehavior.NoAction);
        }

        public void AddInitialAdministrator()
        {
            if (Users.Any(u => u.Role == Role.Administrator))
            {
                return;
            }

            User admin = new User()
            {
                Email = "admin@gmail.com", Name = "Admin", Surname = "Admin",
                Password = "Admin1234", Role = Role.Administrator
            };
            Users.Add(admin);
            Sessions.Add(new Session() { UserId = admin.Id });

            SaveChanges();
        }
    }
}
