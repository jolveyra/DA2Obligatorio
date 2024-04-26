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
        public DbSet<User> Users { get; set; } 
        public DbSet<Request> Requests { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Invitation> Invations { get; set; }

        public BuildingBossContext() { }

        public BuildingBossContext(DbContextOptions options) : base(options) { }

    }
}
