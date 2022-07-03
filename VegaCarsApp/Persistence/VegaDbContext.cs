using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VegaCarsApp.Models;

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

        public DbSet<Make> Makes { get; set; }

    }
}