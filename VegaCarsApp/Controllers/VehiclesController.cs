using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaCarsApp.Controllers.DTOs;
using VegaCarsApp.Core.Models;
using VegaCarsApp.Core;

namespace VegaCarsApp.Controllers
{
    [ApiController]
    [Route("/api/vehicles")]
    public class VehiclesController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IVehicleRepository vehicleRepository;
        public IUnitOfWork UnitOfWork { get; }
        public VehiclesController(IMapper mapper, IVehicleRepository vehicleRepository, IUnitOfWork unitOfWork)
        {
            this.UnitOfWork = unitOfWork;
            this.vehicleRepository = vehicleRepository;
            this.mapper = mapper;
        }

        // [HttpGet]
        // public async Task<IEnumerable<VehicleDTO>> GetVehicles(VehicleQueryDTO vehicleQueryDTO)
        // {     
        //     var filter = mapper.Map<VehicleQueryDTO, VehicleQuery>(vehicleQueryDTO);
         
        //     var vehicles = await vehicleRepository.GetVehicles(filter);

        //     return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleDTO>>(vehicles);
        // }

        [HttpGet]
        public async Task<IEnumerable<VehicleDTO>> GetVehicles()
        {     
            var vehicles = await vehicleRepository.GetVehicles();

            return mapper.Map<IEnumerable<Vehicle>, IEnumerable<VehicleDTO>>(vehicles);
        }

        [HttpPost]
        public async Task<IActionResult> CreateVehicle([FromBody] SaveVehicleDTO vehicleDTO) 
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = mapper.Map<SaveVehicleDTO, Vehicle>(vehicleDTO);
            vehicle.LastUpdate = DateTime.Now;

            vehicleRepository.Add(vehicle);

            await UnitOfWork.CompleteAsync();
            
            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);

            var resultVehicle = mapper.Map<Vehicle, VehicleDTO>(vehicle);
            return Ok(resultVehicle);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] SaveVehicleDTO vehicleDTO) 
        {
            if(!ModelState.IsValid)
                return BadRequest(ModelState);
            //get the vehicle item
            var vehicle = await vehicleRepository.GetVehicle(id);

            if(vehicle == null)
                return NotFound();

            mapper.Map<SaveVehicleDTO, Vehicle>(vehicleDTO, vehicle);
            vehicle.LastUpdate = DateTime.Now;
            //save the changes
            await UnitOfWork.CompleteAsync();

            //refetch the vehicle item
            vehicle = await vehicleRepository.GetVehicle(vehicle.Id);
            var resultVehicle = mapper.Map<Vehicle, VehicleDTO>(vehicle);
            return Ok(resultVehicle);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id, includeRelated: false);
            if(vehicle == null)
                return NotFound();

            vehicleRepository.Remove(vehicle);
            await UnitOfWork.CompleteAsync();

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetVehicle(int id)
        {
            var vehicle = await vehicleRepository.GetVehicle(id);

            if(vehicle == null)
                return NotFound();

            var vehicleResource = mapper.Map<Vehicle, VehicleDTO>(vehicle);
            return Ok(vehicleResource);
        }
    }
}