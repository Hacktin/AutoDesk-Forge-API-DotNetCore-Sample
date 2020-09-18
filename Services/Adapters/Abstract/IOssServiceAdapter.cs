using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Entities.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Adapters.Abstract
{
    public interface IOssServiceAdapter<T,U,K> 
        where T:class,INode
     where U:class,IUploadFile
     where K:class,IKey
    {
        Task<IList<T>> GetNodesTask(string id);
        Task<dynamic> UploadTask(U file,string rootPath);

        Task<dynamic> CreateTask(K key);

        Task<dynamic> GetObjects(string bucketKey);



        IList<T> GetNodes(string id);
        dynamic Upload(U file,string rootPath);

        dynamic Create(K key);


    }
}
