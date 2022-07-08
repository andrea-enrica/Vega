
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaCarsApp.Core;
using VegaCarsApp.Core.Models;
using System.Linq;
using System.Linq.Expressions;

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

        // public async Task<IEnumerable<Vehicle>> GetVehicles(VehicleQuery queryObj)
        // {
        //     var query = Context.Vehicles
        //         .Include(v => v.Model)
        //             .ThenInclude(m => m.Make)
        //         .Include(v => v.Features)
        //             .ThenInclude(vf => vf.Feature)
        //         .AsQueryable();

        //     if (queryObj != null && queryObj.MakeId.HasValue)
        //         query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

        //     if (queryObj != null && queryObj.ModelId.HasValue)
        //         query = query.Where(v => v.ModelId == queryObj.ModelId.Value);
            

        //     // Expression<Func<Vehicle, object>> exp;
        //     var columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
        //     {
        //         ["make"] = v => v.Model.Make.Name,
        //         ["model"] = v => v.Model.Name,
        //         ["contactName"] = v => v.ContactName,
        //     };

        //     query = ApplyOrdering(queryObj, query, columnsMap);

        //     return await query.ToListAsync();
        // }

        // private IQueryable<Vehicle> ApplyOrdering(VehicleQuery queryObj, IQueryable<Vehicle> query, Dictionary<string, Expression<Func<Vehicle, object>>> columnsMapping) 
        // {
        //     if(String.IsNullOrWhiteSpace(queryObj.SortBy) || !columnsMapping.ContainsKey(queryObj.SortBy)) {
        //         return query;
        //     }

        //     if(queryObj.IsSortAscending) {
        //         return query.OrderBy(columnsMapping[queryObj.SortBy]);
        //     } else {
        //         return query.OrderByDescending(columnsMapping[queryObj.SortBy]);
        //     }
        // }

        public async Task<IEnumerable<Vehicle>> GetVehicles()
        {
            return await Context.Vehicles
                .Include(v => v.Model)
                .ThenInclude(m => m.Make)
                .Include(v => v.Features)
                .ThenInclude(vf => vf.Feature)
                .ToListAsync();
        }
    }
}