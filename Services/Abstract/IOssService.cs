using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Entities.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Abstract
{
    public interface IOssService<T,U,K> where T : class,INode
    where U:class,IUploadFile
    where K:class,IKey
    {
        Task<IList<T>> GetOssTask(string id);

        IList<T> GetOss(string id);

        Task<dynamic> CreateTask(K key);

        dynamic Create(K key);

        Task<dynamic> UploadObjectTask(U file,string rootPath);

        dynamic UploadObject(U file,string rootPath);
    }
}
