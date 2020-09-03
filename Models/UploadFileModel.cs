using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace forgeSampleAPI_DotNetCore.Models
{
    public class UploadFileModel
    {
        public string bucketKey { get; set; }

        public IFormFile fileToUpload { get; set; }
    }
}
