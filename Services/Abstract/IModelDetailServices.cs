using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Abstract
{
    public interface IModelDetailServices<M> where M:class,IModelDetails
    {
        Task<dynamic> GetModelDetailPropertiesAsync(M modelDetails);

        dynamic GetModelDetailProperties(M modelDetails);
    }
}
