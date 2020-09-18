using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Models;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace forgeSampleAPI_DotNetCore.Controllers
{
    [ApiController]
    public class OssController : ControllerBase
    {

        private readonly IOssServiceAdapter<TreeNode, BucketUploadFile, BucketKey> _ossServiceAdapter;
        private readonly IMapper mapper;
        private IWebHostEnvironment _env;


        public OssController(IOssServiceAdapter<TreeNode, BucketUploadFile, BucketKey> ossServiceAdapter,
            IMapper mapper, IWebHostEnvironment env)
        {
            this._ossServiceAdapter = ossServiceAdapter;
            this.mapper = mapper;
            this._env = env;
        }





        [HttpGet]
        [Route("api/forge/oss/buckets")]
        public async Task<IList<TreeNode>> GetOSSAsync([FromQuery] string id)
        {
            return await _ossServiceAdapter.GetNodesTask(id);
        }


        [HttpPost]
        [Route("api/forge/oss/buckets")]
        public async Task<dynamic> CreateBucket([FromBody]CreateBucketModel createBucketModel)
        {
            BucketKey bucketKey = mapper.Map<BucketKey>(createBucketModel);
            return await _ossServiceAdapter.CreateTask(bucketKey);
        }


        [HttpPost]
        [Route("api/forge/oss/objects")]
        public async Task<dynamic> UploadObject([FromForm]UploadFileModel uploadFileModel)
        {
            BucketUploadFile uploadFile = mapper.Map<BucketUploadFile>(uploadFileModel);
            return await _ossServiceAdapter.UploadTask(uploadFile, _env.WebRootPath);
        }

       
    }
}