using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace forgeSampleAPI_DotNetCore.Core.ErrorHandler
{
    public class ForgeApiErrorDetails:ErrorDetails
    {
        public dynamic errorContent { get; set; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }
}
