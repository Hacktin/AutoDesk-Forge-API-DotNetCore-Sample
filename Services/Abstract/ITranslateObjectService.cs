using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Forge.Model;
using forgeSampleAPI_DotNetCore.Entities.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Abstract
{
   public interface ITranslateObjectService<T> where T:class,ITranslateObject
   {
       Task<dynamic> TranslateTask(T translateObject);
       dynamic Translate(T translateObject);
   }
}
