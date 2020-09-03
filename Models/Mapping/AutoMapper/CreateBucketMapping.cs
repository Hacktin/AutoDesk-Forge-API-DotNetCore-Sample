using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using forgeSampleAPI_DotNetCore.Entities;

namespace forgeSampleAPI_DotNetCore.Models.Mapping.AutoMapper
{
    public class CreateBucketMapping:Profile
    {
        public CreateBucketMapping()
        {
            CreateMap<CreateBucketModel, BucketKey>();
            CreateMap<BucketKey, CreateBucketModel>()
                .ForMember(c => c.bucketKey,opt => opt.MapFrom(b => b.bucketKey));


        }
    }
}
