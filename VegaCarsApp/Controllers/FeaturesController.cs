using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    public async Task<IEnumerable<FeatureDTO>> GetFeatures()
    {
      var features = await context.Features.ToListAsync();

      return mapper.Map<List<Feature>, List<FeatureDTO>>(features); 
    }
  }
}