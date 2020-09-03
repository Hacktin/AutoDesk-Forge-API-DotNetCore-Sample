using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities.Abstract;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Adapters.Concerete
{
    public class TranslateObjectServiceAdapter<T>:ITranslateObjectServiceAdapter<T>
    where T:class,ITranslateObject
    {

        public readonly ITranslateObjectService<T> _translateObjectService;

        public TranslateObjectServiceAdapter(ITranslateObjectService<T> translateObjectService)
        {
            this._translateObjectService = translateObjectService;
        }
        public async Task<dynamic> TranslateObjectTask(T translateObject)
        {
            return await _translateObjectService.TranslateTask(translateObject);
        }

        public dynamic TranslateObject(T translateObject)
        {
            throw new NotImplementedException();
        }

        
    }
}
