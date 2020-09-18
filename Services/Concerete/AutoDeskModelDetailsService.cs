using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Forge;
using Autodesk.Forge.Model;
using forgeSampleAPI_DotNetCore.Business.Helpers.AutoDeskForge;
using forgeSampleAPI_DotNetCore.Core.Business;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Enums;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Concerete
{
    public class AutoDeskModelDetailsService : IModelDetailServices<ModelDetails>
    {
        private readonly IAuthServiceAdapter _authServiceAdapter;

        public AutoDeskModelDetailsService(IAuthServiceAdapter authServiceAdapter)
        {
            this._authServiceAdapter = authServiceAdapter;
        }
        public dynamic GetModelDetailProperties(ModelDetails modelDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetModelDetailPropertiesAsync(ModelDetails modelDetails)
        {
            DerivativesApi derivativesApi =
                GeneralTokenConfigurationSettings<IDerivativesApi>.SetToken(new DerivativesApi(),
                    await _authServiceAdapter.GetSecondaryTokenTask());

            dynamic detail = await GetModelDetailGuid(derivativesApi, modelDetails.urn);

            dynamic metadata = detail.data.metadata;

            List<dynamic> results=new List<dynamic>();


            foreach (KeyValuePair<string, dynamic> m in new DynamicDictionaryItems(metadata))
            {
                
                results.Add(await derivativesApi.GetModelviewPropertiesAsync(modelDetails.urn,m.Value.guid));
            }


            return results;
        }

        //This method will continue to be playground :)

        #region privateMethods

        private async Task<dynamic> GetModelDetailGuid(DerivativesApi api, string urn)
        {
            return await api.GetMetadataAsync(urn);
        }

        #endregion
    }
}
