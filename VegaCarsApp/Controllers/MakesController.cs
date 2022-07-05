using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaCarsApp.Controllers.DTOs;
using VegaCarsApp.Models;
using VegaCarsApp.Persistence;

namespace VegaCarsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MakesController: ControllerBase
    {
        public readonly VegaDbContext context;
        private readonly IMapper mapper;
    
        public MakesController(VegaDbContext context, IMapper mapper)
        {
            this.mapper = mapper;
            this.context = context;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeDTO>> GetMakes()
        {
            var makes = await context.Makes.Include(m => m.Models).ToListAsync();
            return mapper.Map<List<Make>, List<MakeDTO>>(makes);
        }
    }
}