using System;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Adapters.Concerete
{
    public class AuthServiceAdapter : IAuthServiceAdapter
    {
        private readonly IAuthService _authService;

        public AuthServiceAdapter(IAuthService authService)
        {
            this._authService = authService;
        }

        public dynamic GetSecondaryToken()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetSecondaryTokenTask()
        {
            return await _authService.GetSecondaryTokenTask();
        }

        public dynamic GetToken()
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetTokenTask()
        {
            return await _authService.GetPrimaryTokenTask();
        }
    }
}
