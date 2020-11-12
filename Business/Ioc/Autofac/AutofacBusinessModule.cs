using System;
using Autofac;
using forgeSampleAPI_DotNetCore.Caching.Redis;
using forgeSampleAPI_DotNetCore.Core.CrossCuttingCornces.Caching;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Concerete;
using forgeSampleAPI_DotNetCore.Services.Concerete;

namespace forgeSampleAPI_DotNetCore.Business.Ioc.Autofac
{
    public class AutofacBusinessModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<AutoDeskAuthService>().As<IAuthService>();
            builder.RegisterType<AuthServiceAdapter>().As<IAuthServiceAdapter>();

            builder.RegisterType<AutoDeskOssService>().As<IOssService<TreeNode, BucketUploadFile, BucketKey>>();
            builder.RegisterType<OssServiceAdapter<TreeNode, BucketUploadFile, BucketKey>>()
                .As<IOssServiceAdapter<TreeNode, BucketUploadFile, BucketKey>>();

            builder.RegisterType<AutoDeskTranslateObjectService>().As<ITranslateObjectService<TranslateObject>>();
            builder.RegisterType<TranslateObjectServiceAdapter<TranslateObject>>()
                .As<ITranslateObjectServiceAdapter<TranslateObject>>();


            builder.RegisterType<RedisCacheManager>().As<ICacheManager>().SingleInstance();
            builder.RegisterType<RedisServer>().SingleInstance();

            builder.RegisterType<AutoDeskModelDetailsService>().As<IModelDetailServices<ModelDetails>>();
            builder.RegisterType<ModelDetailsServiceAdapter<ModelDetails>>()
                .As<IModelDetailServicesAdapter<ModelDetails>>();

          
        }
    }
}
