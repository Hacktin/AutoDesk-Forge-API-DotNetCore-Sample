using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Core.ErrorHandler
{
    public class ForgeApiErrorDetails:ErrorDetails
    {
        public dynamic errorContent { get; set; }
    }
}
