using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities.Abstract;


namespace forgeSampleAPI_DotNetCore.Entities
{
    public class BucketKey:IKey
    {
        public string bucketKey { get; set; }
    }
}
