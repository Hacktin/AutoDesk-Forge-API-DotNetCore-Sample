using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using forgeSampleAPI_DotNetCore.Entities;

namespace forgeSampleAPI_DotNetCore.Models.Mapping.AutoMapper
{
    public class ModelDetailsMapping:Profile
    {
        public ModelDetailsMapping()
        {
            CreateMap<ModelDetailsModel, ModelDetails>();
            CreateMap<ModelDetails, ModelDetailsModel>()
                .ForMember(m => m.urn, o => o.MapFrom(m => m.urn));
        }
    }
}
