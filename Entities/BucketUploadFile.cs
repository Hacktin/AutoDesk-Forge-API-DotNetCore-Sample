using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities.Abstract;
using Microsoft.AspNetCore.Http;

namespace forgeSampleAPI_DotNetCore.Entities
{
    public class BucketUploadFile:BucketKey,IUploadFile
    {
        public IFormFile fileToUpload { get; set; }
    }
}
