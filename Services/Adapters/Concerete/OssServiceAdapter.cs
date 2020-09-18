using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Entities.Abstract;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace forgeSampleAPI_DotNetCore.Services.Adapters.Concerete
{
    public class OssServiceAdapter<T, U, K> : IOssServiceAdapter<T, U, K>
    where T : class, INode
    where U : class, IUploadFile
    where K : class, IKey
    {
        private readonly IOssService<T, U, K> _ossService;

        public OssServiceAdapter(IOssService<T, U, K> ossService)
        {
            this._ossService = ossService;
        }
        public async Task<dynamic> CreateTask(K key)
        {
            return await _ossService.CreateTask(key);
        }


        public async Task<IList<T>> GetNodesTask(string id)
        {
            return await _ossService.GetOssTask(id);
        }

        public async Task<dynamic> UploadTask(U file, string rootPath)
        {
            return await _ossService.UploadObjectTask(file, rootPath);
        }


        public dynamic Create(K key)
        {
            throw new NotImplementedException();
        }

        public IList<T> GetNodes(string id)
        {
            throw new NotImplementedException();
        }

        public dynamic Upload(U file, string rootPath)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> GetObjects(string bucketKey)
        {
            return await _ossService.GetObjects(bucketKey);
        }
    }
}
