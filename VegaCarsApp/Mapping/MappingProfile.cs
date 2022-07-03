using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using VegaCarsApp.Controllers.DTOs;
using VegaCarsApp.Models;

namespace VegaCarsApp.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() {
            CreateMap<Make, MakeDTO>();
            CreateMap<Model, ModelDTO>();
            CreateMap<Feature, FeatureDTO>();
        }
    }
}