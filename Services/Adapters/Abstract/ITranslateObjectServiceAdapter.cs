using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Forge.Model;
using forgeSampleAPI_DotNetCore.Entities.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Adapters.Abstract
{
    public interface ITranslateObjectServiceAdapter<T> where T:class,ITranslateObject
    {

        Task<dynamic> TranslateObjectTask(T translateObject);

     
        dynamic TranslateObject(T translateObject);
    }
}
