using System;
using System.Threading.Tasks;
using Autodesk.Forge;
using forgeSampleAPI_DotNetCore.Services.Adapters;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace forgeSampleAPI_DotNetCore.Controllers
{
    [ApiController]
    public class OAuthController : ControllerBase
    {

        private readonly IAuthServiceAdapter _serviceAdapter;

        public OAuthController(IAuthServiceAdapter serviceAdapter)
        {
            this._serviceAdapter = serviceAdapter;
        }


        [HttpGet]
        [Route("api/forge/oauth/token")]
        public async Task<dynamic> GetPublicAsync()
        {
            return await _serviceAdapter.GetTokenTask();
        }

    }
}

