using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Autodesk.Forge;
using Autodesk.Forge.Model;
using forgeSampleAPI_DotNetCore.Business.Helpers.AutoDeskForge;
using forgeSampleAPI_DotNetCore.Core.Business;
using forgeSampleAPI_DotNetCore.Core.CrossCuttingCornces.Caching;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace forgeSampleAPI_DotNetCore.Services.Concerete
{
    public class AutoDeskModelDetailsService : IModelDetailServices<ModelDetails>
    {
        private readonly IAuthServiceAdapter _authServiceAdapter;
        private readonly ICacheManager _cacheManager;

        public AutoDeskModelDetailsService(IAuthServiceAdapter authServiceAdapter,ICacheManager cacheManager)
        {
            this._authServiceAdapter = authServiceAdapter;
            this._cacheManager = cacheManager;
        }

        public dynamic GetModelDetailProperties(ModelDetails modelDetails)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetModelDetailMetaDataAsync(ModelDetails modelDetails)
        {

            DerivativesApi derivativesApi =
               GeneralTokenConfigurationSettings<IDerivativesApi>.SetToken(new DerivativesApi(),
                   await _authServiceAdapter.GetSecondaryTokenTask());

            dynamic detail = await GetModelDetailGuid(derivativesApi, modelDetails.urn);

            dynamic metadata = detail.data.metadata;

            List<dynamic> results = new List<dynamic>();


            foreach (KeyValuePair<string, dynamic> m in new DynamicDictionaryItems(metadata))
            {

                results.Add(await derivativesApi.GetModelviewPropertiesAsync(modelDetails.urn, m.Value.guid));
            }


            return results;
        }

      



        public async Task<dynamic> GetModelDetailPropertiesAsync(ModelDetails modelDetails)
        {

               List<dynamic> results = new List<dynamic>();

            
                DerivativesApi derivativesApi =
                GeneralTokenConfigurationSettings<IDerivativesApi>.SetToken(new DerivativesApi(),
                    await _authServiceAdapter.GetSecondaryTokenTask());

                dynamic detail = await GetModelDetailGuid(derivativesApi, modelDetails.urn);

                dynamic metadata = detail.data.metadata;


                dynamic r = null;


                foreach (KeyValuePair<string, dynamic> m in new DynamicDictionaryItems(metadata))
                {
                    r = await derivativesApi.GetModelviewPropertiesAsync(modelDetails.urn, m.Value.guid);

                    break;

                }

                dynamic collection = r.data.collection;


                foreach (KeyValuePair<string, dynamic> c in new DynamicDictionaryItems(collection))
                {
                    results.Add(c.Value);
                }

                return results;
            
                
        }

        public async Task<dynamic> GetModelDetailPropertiesAsyncByName(ModelDetails modelDetails)
        {
            dynamic arrayResult = await GetModelDetailPropertiesAsync(modelDetails);

            string key = modelDetails.name + "-name";

            dynamic selectedResult = null;

      
           
                foreach (KeyValuePair<string, dynamic> a in new DynamicDictionaryItems(arrayResult))
                {
                    if (a.Value.name == modelDetails.name)
                    {
                        selectedResult = a.Value;
                        break;
                    }
                }

                return selectedResult;
         
         
        }

        public async Task<dynamic> GetModelDetailPropertiesAsyncByNamePattern(ModelDetails modelDetails)
        {
            dynamic arrayResult = await GetModelDetailPropertiesAsync(modelDetails);

            List<dynamic> selectedResults = new List<dynamic>();

            string propertiesPattern = $"{modelDetails.pattern}.*([A-aZ-z][1-9])*";


           
                Regex regex = new Regex(propertiesPattern);

                foreach (dynamic a in arrayResult)
                {
                    if (regex.IsMatch(a.name))
                    {
                        selectedResults.Add(a);
                    }
                }

                return selectedResults;
            
           
        }



        #region privateMethods

        private async Task<dynamic> GetModelDetailGuid(DerivativesApi api, string urn)
        {
            return await api.GetMetadataAsync(urn);
        }

        #endregion
    }
}
