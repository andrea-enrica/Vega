using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaCarsApp.Core.Models;

namespace VegaCarsApp.Persistence
{
    public class VegaDbContext : DbContext
    {
        private readonly IConfiguration configuration;

        public VegaDbContext(DbContextOptions<VegaDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

         protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(this.configuration.GetConnectionString("Default"));
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder){
            // PasswordHasher<User> passwordHasher = new PasswordHasher<User>();
            modelBuilder.Entity<Make>().HasData(
                new Make { id = 1, Name = "Make1" },
                new Make { id = 2, Name = "Make2" },
                new Make { id = 3, Name = "Make3" }
            );
            modelBuilder.Entity<Model>().HasData(
                new Model{ id = 1, Name = "Make1-ModelA", MakeId = 1 },
                new Model{ id = 2, Name = "Make1-ModelB", MakeId = 1 },      
                new Model{ id = 3, Name = "Make1-ModelC", MakeId = 1 },

                new Model{ id = 4, Name = "Make2-ModelA", MakeId = 2 },
                new Model{ id = 5, Name = "Make2-ModelB", MakeId = 2 },
                new Model{ id = 6, Name = "Make2-ModelC", MakeId = 2 },

                new Model{ id = 7, Name = "Make3-ModelA", MakeId = 3 },
                new Model{ id = 8,  Name = "Make3-ModelB", MakeId = 3 },
                new Model{ id = 9, Name = "Make3-ModelC", MakeId = 3 }
            );

            modelBuilder.Entity<Feature>().HasData(
                new Feature{ Id = 1, Name = "Feature1"},
                new Feature{ Id = 2, Name = "Feature2"},
                new Feature{ Id = 3, Name = "Feature3"}
            );

            modelBuilder.Entity<VehicleFeature>().HasKey(vf => 
                new {vf.VehicleId, vf.FeatureId}
            );
        }

        public DbSet<Make> Makes { get; set; }

        public DbSet<Model> Models { get; set;}

        public DbSet<Feature> Features { get; set; }

        public DbSet<Vehicle> Vehicles { get; set; }

    }
}