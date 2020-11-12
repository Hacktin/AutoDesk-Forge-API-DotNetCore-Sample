using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities.Abstract;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Adapters.Concerete
{
    public class ModelDetailsServiceAdapter<M> : IModelDetailServicesAdapter<M> where M : class, IModelDetails
    {
        public readonly IModelDetailServices<M> _modelDetailServices;

        public ModelDetailsServiceAdapter(IModelDetailServices<M> modelDetailServices)
        {
            this._modelDetailServices = modelDetailServices;
        }

        public async Task<dynamic> GetModelDetailMetaDataAsync(M modelDetails)
        {
            return await _modelDetailServices.GetModelDetailMetaDataAsync(modelDetails);
        }

        public dynamic GetModelDetailProperties(M modelDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetModelDetailPropertiesAsync(M modelDetails)
        {
            return await _modelDetailServices.GetModelDetailPropertiesAsync(modelDetails);
        }

        public async Task<dynamic> GetModelDetailPropertiesAsyncByName(M modelDetails)
        {
            return await _modelDetailServices.GetModelDetailPropertiesAsyncByName(modelDetails);
        }

        public async Task<dynamic> GetModelDetailPropertiesAsyncByNamePattern(M modelDetails)
        {
            return await _modelDetailServices.GetModelDetailPropertiesAsyncByNamePattern(modelDetails);
        }
    }
}
