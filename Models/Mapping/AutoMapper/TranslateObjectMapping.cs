using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using forgeSampleAPI_DotNetCore.Entities;

namespace forgeSampleAPI_DotNetCore.Models.Mapping.AutoMapper
{
    public class TranslateObjectMapping:Profile
    {
        public TranslateObjectMapping()
        {
            CreateMap<TranslateObjectModel, TranslateObject>();
            CreateMap<TranslateObject, TranslateObjectModel>()
                .ForMember(t => t.bucketKey, o => o.MapFrom(t => t.bucketKey))
                .ForMember(t => t.objectName, o => o.MapFrom(t => t.objectName));
        }
    }
}
