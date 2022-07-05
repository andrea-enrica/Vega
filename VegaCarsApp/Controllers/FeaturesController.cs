using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VegaCarsApp.Controllers.DTOs;
using VegaCarsApp.Core.Models;
using VegaCarsApp.Persistence;

namespace VegaCarsApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeaturesController : Controller
  {
    private readonly VegaDbContext context;
    private readonly IMapper mapper;
    public FeaturesController(VegaDbContext context, IMapper mapper)
    {
      this.mapper = mapper;
      this.context = context;
    }

    [HttpGet("/api/features")]
    public async Task<IEnumerable<KeyValuePairDTO>> GetFeatures()
    {
      var features = await context.Features.ToListAsync();

      return mapper.Map<List<Feature>, List<KeyValuePairDTO>>(features); 
    }
  }
}