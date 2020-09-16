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
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Concerete
{
    public class AutoDeskTranslateObjectService : ITranslateObjectService<TranslateObject>
    {
        private readonly IAuthServiceAdapter _authServiceAdapter;

        private List<JobPayloadItem> outputs = new List<JobPayloadItem>()
        {
            new JobPayloadItem(JobPayloadItem.TypeEnum.Svf, new List<JobPayloadItem.ViewsEnum>
            {
                JobPayloadItem.ViewsEnum._2d,
                JobPayloadItem.ViewsEnum._3d
            })
        };


        public AutoDeskTranslateObjectService(IAuthServiceAdapter authServiceAdapter)
        {
            this._authServiceAdapter = authServiceAdapter;
        }


        public dynamic Translate(TranslateObject translateObject)
        {
            throw new NotImplementedException();
        }
        

        public async Task<dynamic> TranslateTask(TranslateObject translateObject)
        {
            string Urn = translateObject.objectName;
            string rootFileName = translateObject.RootFileName;

            JobPayload job = BusinessLogicRunner.RunnerStatmentOptional<JobPayload>((rootFileName != null),
                new JobPayload(new JobPayloadInput(Urn, true, rootFileName), new JobPayloadOutput(outputs)),
                new JobPayload(new JobPayloadInput(Urn), new JobPayloadOutput(outputs)));

            DerivativesApi derivativesApi =
                GeneralTokenConfigurationSettings<IDerivativesApi>.SetToken(new DerivativesApi(),
                    await _authServiceAdapter.GetSecondaryTokenTask());



            dynamic jobTranslate = await derivativesApi.TranslateAsync(job,true);

            return jobTranslate;
        }


        
    }
}
