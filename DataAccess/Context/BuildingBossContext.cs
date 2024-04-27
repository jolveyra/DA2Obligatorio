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
        public DbSet<Flat> Flats { get; set; }
        public virtual DbSet<User> Users { get; set; } 
        public virtual DbSet<Request> Requests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public virtual DbSet<Invitation> Invitations { get; set; }
        public virtual DbSet<Session> Sessions { get; set; }

        public BuildingBossContext() { }

        public BuildingBossContext(DbContextOptions options) : base(options) { }

    }
}
