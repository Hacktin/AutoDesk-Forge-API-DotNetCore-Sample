using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Core.ErrorHandler;
using Newtonsoft.Json;

namespace forgeSampleAPI_DotNetCore.Core.ErrorHandler
{
    public class ErrorDetails
    {

        public string message { get; set; }
        public int code { get; set; }

        
    }
}
