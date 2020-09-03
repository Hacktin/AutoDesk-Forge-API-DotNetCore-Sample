using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace forgeSampleAPI_DotNetCore.Services.Abstract
{
    public interface IAuthService
    {
        Task<dynamic> GetPrimaryTokenTask();

        dynamic GetPrimaryToken();

        Task<dynamic> GetSecondaryTokenTask();

        dynamic GetSecondaryToken();
    }
}
