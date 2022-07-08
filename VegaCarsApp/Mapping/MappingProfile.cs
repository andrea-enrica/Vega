using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.Features;
using VegaCarsApp.Controllers.DTOs;
using VegaCarsApp.Core.Models;
using static VegaCarsApp.Controllers.DTOs.SaveVehicleDTO;

namespace VegaCarsApp.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile() 
        {
            //Domain to Api Resources
            CreateMap<Make, MakeDTO>();
            CreateMap<Make, KeyValuePairDTO>();
            CreateMap<Model, KeyValuePairDTO>();
            CreateMap<Feature, KeyValuePairDTO>();
            CreateMap<Vehicle, SaveVehicleDTO>()
                .ForMember(vDTO => vDTO.Contact, operationObject => operationObject.MapFrom(v => new ContactDTO { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
                .ForMember(vDTO => vDTO.Features, operationObject => operationObject.MapFrom(v => v.Features.Select(vf => vf.FeatureId)));
            CreateMap<Vehicle, VehicleDTO>()
                .ForMember(vDTO => vDTO.Make, operationObject => operationObject.MapFrom(v => v.Model.Make))
                .ForMember(vDTO => vDTO.Contact, operationObject => operationObject.MapFrom(v => new ContactDTO { Name = v.ContactName, Email = v.ContactEmail, Phone = v.ContactPhone}))
                .ForMember(vDTO => vDTO.Features, operationObject => operationObject.MapFrom(v => v.Features.Select(vf => new KeyValuePairDTO {Id = vf.Feature.Id, Name = vf.Feature.Name})));

            //API Resource to Domain
            CreateMap<VehicleQueryDTO, VehicleQuery>();
            CreateMap<SaveVehicleDTO, Vehicle>()
                .ForMember(vehicle => vehicle.Id, operationObject => operationObject.Ignore())
                .ForMember(vehicle => vehicle.ContactName, operationObject => operationObject.MapFrom(vDTO => vDTO.Contact.Name))
                .ForMember(vehicle => vehicle.ContactEmail, operationObject => operationObject.MapFrom(vDTO => vDTO.Contact.Email))
                .ForMember(vehicle => vehicle.ContactPhone, operationObject => operationObject.MapFrom(vDTO => vDTO.Contact.Phone))
                .ForMember(vehicle => vehicle.Features, operationObject => operationObject.Ignore())
                .AfterMap((vDTO, v) => {
                    //Remove unselected features
                    var removedFeatures = v.Features.Where(f => !vDTO.Features.Contains(f.FeatureId));
                    foreach(var f in removedFeatures)
                        v.Features.Remove(f);

                    //Add new features
                    var addedFeatures = vDTO.Features.Where(id => !v.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature{FeatureId = id});
                    foreach(var f in addedFeatures)
                        v.Features.Add(f);
                });
        }
    }
}