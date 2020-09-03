using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using forgeSampleAPI_DotNetCore.Entities;

namespace forgeSampleAPI_DotNetCore.Models.Mapping.AutoMapper
{
    public class UploadFileMapping:Profile
    {
        public UploadFileMapping()
        {
            CreateMap<UploadFileModel, BucketUploadFile>();
            CreateMap<BucketUploadFile, UploadFileModel>()
                .ForMember(c => c.bucketKey, opt => opt.MapFrom(b => b.bucketKey))
                .ForMember(c => c.fileToUpload, opt => opt.MapFrom(b => b.fileToUpload));

        }
    }
}
