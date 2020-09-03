using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Services.Adapters.Abstract
{
    public interface IAuthServiceAdapter
    {
        Task<dynamic> GetTokenTask();

        Task<dynamic> GetSecondaryTokenTask();
        dynamic GetToken();

        dynamic GetSecondaryToken();
    }
}
