
using Microsoft.EntityFrameworkCore;
using VegaCarsApp.Core;
using VegaCarsApp.Core.Models;

namespace VegaCarsApp.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        public VegaDbContext Context { get; }
        public VehicleRepository(VegaDbContext context)
        {
            this.Context = context; 
        }
        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if(!includeRelated)
                return await Context.Vehicles.FindAsync(id);

            return await Context.Vehicles
                .Include(v => v.Features)
                //load nested objects
                    .ThenInclude(vf => vf.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public void Add(Vehicle vehicle) 
        {
            Context.Vehicles.Add(vehicle);
        }

        public void Remove(Vehicle vehicle) 
        {
            Context.Vehicles.Remove(vehicle);
        }
    }
}