using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using VegaCarsApp.Controllers.DTOs;
using VegaCarsApp.Core;
using VegaCarsApp.Core.Models;

namespace VegaCarsApp.Controllers
{ 
    [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController : ControllerBase
    {
        private readonly int MAX_BYTES = 10 * 1024 * 1024;
        private readonly string[] ACCEPTED_FILE_TYPES = new[] {".jpg", ".jpeg", ".png"};

        private readonly IVehicleRepository vehicleRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        public IWebHostEnvironment host { get; }
        private readonly IPhotoRepository photoRepository;
        public PhotosController(IWebHostEnvironment host, IVehicleRepository vehicleRepository, IPhotoRepository photoRepository, IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.photoRepository = photoRepository;
            this.host = host;
            this.mapper = mapper;
            this.unitOfWork = unitOfWork;
            this.vehicleRepository = vehicleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoDTO>> GetPhotos(int vehicleId) 
        {
            var photos = await photoRepository.GetPhotos(vehicleId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoDTO>>(photos);
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile file)
        {   
            //IncludeRelated to not load the entire object
            var vehicle = await vehicleRepository.GetVehicle(vehicleId, includeRelated: false);
            if (vehicle == null)
                return NotFound();

            if(file == null) return BadRequest("Null file");
            if(file.Length == 0) return BadRequest("Empty file");
            if(file.Length > MAX_BYTES) return BadRequest("Max file size exceeded");
            if(!ACCEPTED_FILE_TYPES.Any(s => s == Path.GetExtension(file.FileName))) return BadRequest("Invalid file type");
            //will return the exact path on the hosting machine
            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");

            //if the directory doesn't exist create the directory for the first time
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            //generate a new file name for security reasons
            //guid represent the filename + the extension
            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);
            
            //we are using stream to read the input file and store it at this path
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            vehicle.Photos.Add(photo);
            await unitOfWork.CompleteAsync();

            return Ok(mapper.Map<Photo, PhotoDTO>(photo));
        }
  }
}