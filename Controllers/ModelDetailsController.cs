using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Models;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace forgeSampleAPI_DotNetCore.Controllers
{
    [ApiController]
    public class ModelDetailsController : ControllerBase
    {
        private IModelDetailServicesAdapter<ModelDetails> _modelDetailServicesAdapter;
        private IMapper _mapper;


        public ModelDetailsController(IModelDetailServicesAdapter<ModelDetails> modelDetailServicesAdapter,IMapper mapper)
        {
            this._modelDetailServicesAdapter = modelDetailServicesAdapter;
            this._mapper = mapper;
        }


        [HttpPost]
        [Route("api/forge/model/properties")]
        public async Task<dynamic> GetProperties([FromBody] ModelDetailsModel modelDetailsModel )
        {
            ModelDetails modelDetails = _mapper.Map<ModelDetails>(modelDetailsModel);

            return await _modelDetailServicesAdapter.GetModelDetailPropertiesAsync(modelDetails);


        }

        [HttpPost]
        [Route("api/forge/model/properties/pattern")]
        public async Task<dynamic> GetPropertiesByPattern([FromBody] ModelDetailsModel modelDetailsModel)
        {
            ModelDetails modelDetails = _mapper.Map<ModelDetails>(modelDetailsModel);

            return await _modelDetailServicesAdapter.GetModelDetailPropertiesAsyncByNamePattern(modelDetails);


        }

        [HttpPost]
        [Route("api/forge/model/property")]
        public async Task<dynamic> GetProperty([FromBody] ModelDetailsModel modelDetailsModel)
        {
            ModelDetails modelDetails = _mapper.Map<ModelDetails>(modelDetailsModel);

            return await _modelDetailServicesAdapter.GetModelDetailPropertiesAsyncByName(modelDetails);


        }

        [HttpPost]
        [Route("api/forge/model/metadata")]
        public async Task<dynamic> GetMetaData([FromBody] ModelDetailsModel modelDetailsModel)
        {
            ModelDetails modelDetails = _mapper.Map<ModelDetails>(modelDetailsModel);

            return await _modelDetailServicesAdapter.GetModelDetailMetaDataAsync(modelDetails);



        }
    }
}