using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace VegaCarsApi.Models
{
    public class VegaCarsContext : DbContext
    {
        public VegaCarsContext(DbContextOptions<VegaCarsContext> options)
            : base(options)
        {
        }

        public DbSet<Vehicle> Vehicles { get; set; } = null!;
    }
}