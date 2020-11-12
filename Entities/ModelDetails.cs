using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities.Abstract;

namespace forgeSampleAPI_DotNetCore.Entities
{
    public class ModelDetails:IModelDetails
    {
        public string urn { get; set; }

        public string? name { get; set;}

        public string? pattern { get; set; }
    }
}
