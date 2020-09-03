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
    public class ModelDerivativeController : ControllerBase
    {

        private readonly ITranslateObjectServiceAdapter<TranslateObject> _translateObjectServiceAdapter;
        private IMapper _mapper;

        public ModelDerivativeController(ITranslateObjectServiceAdapter<TranslateObject> translateObjectServiceAdapter,IMapper mapper )
        {
            this._translateObjectServiceAdapter = translateObjectServiceAdapter;
            this._mapper = mapper;
        }


        [HttpPost]
        [Route("api/forge/modelderivative/jobs")]
        public async Task<dynamic> TranslateObject([FromBody] TranslateObjectModel objModel)
        {
            TranslateObject translateObject = _mapper.Map<TranslateObject>(objModel);

            return await _translateObjectServiceAdapter.TranslateObjectTask(translateObject);
        }
    }
}