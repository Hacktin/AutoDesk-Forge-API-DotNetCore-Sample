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

        public AutoDeskModelDetailsService(IAuthServiceAdapter authServiceAdapter, ICacheManager cacheManager)
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

            string key = "metadata";

            if (_cacheManager.IsAdd(key))
            {
                return _cacheManager.Get<dynamic>(key);
            }

            else
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

                _cacheManager.Add(key, results, 60);


                return results;
            }


        }





        public async Task<dynamic> GetModelDetailPropertiesAsync(ModelDetails modelDetails)
        {

            List<dynamic> results = new List<dynamic>();

            string key = "properties";

            if (_cacheManager.IsAdd(key))
            {
                return _cacheManager.Get<dynamic>(key);
            }



            else
            {

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




        }

        public async Task<dynamic> GetModelDetailPropertiesAsyncByName(ModelDetails modelDetails)
        {
            dynamic arrayResult = await GetModelDetailPropertiesAsync(modelDetails);

            string key = modelDetails.name;

            dynamic selectedResult = null;

            if (_cacheManager.IsAdd(key))
            {
                return _cacheManager.Get<dynamic>(key);
            }


            else
            {

                foreach (dynamic a in arrayResult)
                {
                    if (a.name == modelDetails.name)
                    {
                        selectedResult = a;
                        break;
                    }
                }

                AddToCache(arrayResult, selectedResult, key);

                return selectedResult;
            }






        }

        public async Task<dynamic> GetModelDetailPropertiesAsyncByNamePattern(ModelDetails modelDetails)
        {


            List<dynamic> selectedResults = new List<dynamic>();

            string propertiesPattern = $"{modelDetails.pattern}.*([A-aZ-z][1-9])*";

            if (_cacheManager.IsAdd(propertiesPattern))
            {
                return _cacheManager.Get<dynamic>(propertiesPattern);
            }


            else
            {

                dynamic arrayResult = await GetModelDetailPropertiesAsync(modelDetails);

                Regex regex = new Regex(propertiesPattern);

                bool result = _cacheManager.IsAdd("properties");


                foreach (dynamic a in arrayResult)
                {
                    

                    if (result)
                    {
                        ListToProperties(selectedResults, regex, a, a.name.Value);
                    }

                    else
                    {
                        ListToProperties(selectedResults, regex, a, a.name);
                       
                    }
                }

                AddToCache(arrayResult, selectedResults, propertiesPattern);
            }


            return selectedResults;
        }







        #region privateMethods

        private async Task<dynamic> GetModelDetailGuid(DerivativesApi api, string urn)
        {
            return await api.GetMetadataAsync(urn);
        }

        private void ListToProperties(dynamic results, Regex regex, dynamic val, dynamic valProperty)
        {
            if (regex.IsMatch(valProperty))
            {
                results.Add(val);
            }
        }

        private void AddToCache(dynamic arrayResult,dynamic selectedResults,string selectedKey)
        {
            _cacheManager.Add("properties", arrayResult, 60);
            _cacheManager.Add(selectedKey, selectedResults, 60);
        }


        #endregion
       }
    }

