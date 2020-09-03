using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Forge.Client;

namespace forgeSampleAPI_DotNetCore.Business.Helpers.AutoDeskForge
{
    public class GeneralTokenConfigurationSettings<T> where T:class,IApiAccessor
    {
        public static T SetToken(T accessor, dynamic token)
        {
            accessor.Configuration.AccessToken = token.access_token;
            return accessor;
        }

        
    }
}
