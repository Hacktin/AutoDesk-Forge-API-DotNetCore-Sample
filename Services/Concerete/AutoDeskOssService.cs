using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Eventing.Reader;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Forge;
using Autodesk.Forge.Model;
using forgeSampleAPI_DotNetCore.Business.Helpers.AutoDeskForge;
using forgeSampleAPI_DotNetCore.Business.Helpers.AutoDeskForge.Extensions;
using forgeSampleAPI_DotNetCore.Core.Configurations;
using forgeSampleAPI_DotNetCore.Core.Encoding;
using forgeSampleAPI_DotNetCore.Entities;
using forgeSampleAPI_DotNetCore.Services.Abstract;
using forgeSampleAPI_DotNetCore.Services.Adapters.Abstract;

namespace forgeSampleAPI_DotNetCore.Services.Concerete
{
    public class AutoDeskOssService: IOssService<TreeNode,BucketUploadFile,BucketKey>
    {
        private readonly IAuthServiceAdapter _authServiceAdapter;
        IList<TreeNode> nodes = new List<TreeNode>();

        public AutoDeskOssService(IAuthServiceAdapter authServiceAdapter)
        {
            this._authServiceAdapter = authServiceAdapter;
        }

        public dynamic Create(BucketKey key)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> CreateTask(BucketKey key)
        {
            IBucketsApi buckets =
                GeneralTokenConfigurationSettings<IBucketsApi>.SetToken(new BucketsApi(),
                    await _authServiceAdapter.GetSecondaryTokenTask());

            var clientId = AppSettings.GetAppSetting("FORGE_CLIENT_ID").ToLower();
            var bucketKey = key.bucketKey.ToLower();

            PostBucketsPayload bucketPayload = new PostBucketsPayload(string.Format("{0}{1}",bucketKey,clientId.Substring(0,18)), null,
                PostBucketsPayload.PolicyKeyEnum.Transient);

            var result= await buckets.CreateBucketAsync(bucketPayload, "US");

            return result;
        }

        public IList<TreeNode> GetOss(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<IList<TreeNode>> GetOssTask(string id)
        {
            string ClientId = AppSettings.GetAppSetting("FORGE_CLIENT_ID").ToLower();

            if (id == "#")
            {
                IBucketsApi bucketsApi =
                    GeneralTokenConfigurationSettings<IBucketsApi>.SetToken(new BucketsApi(),
                        await _authServiceAdapter.GetSecondaryTokenTask());
                dynamic buckets = await bucketsApi.GetBucketsAsync("US", 100);

                foreach (KeyValuePair<string, dynamic> bucket in new DynamicDictionaryItems(buckets.items))
                {
                    nodes.Add(new TreeNode(bucket.Value.bucketKey, bucket.Value.bucketKey.Replace(ClientId + "-", string.Empty), "bucket", true));
                }

                
            }
            else
            {
                IObjectsApi objectsApi =
                    GeneralTokenConfigurationSettings<IObjectsApi>.SetToken(new ObjectsApi(),
                        await _authServiceAdapter.GetSecondaryTokenTask());
                var objectList = objectsApi.GetObjects(id);
                foreach (KeyValuePair<string, dynamic> objInfo in new DynamicDictionaryItems(objectList.items))
                {
                    nodes.Add(new TreeNode(Base64Encoding.Encode((string)objInfo.Value.objectId),
                        objInfo.Value.objectKey, "object", false));
                }
            }

            return nodes;

        }

        public dynamic UploadObject(BucketUploadFile file,string rootPath)
        {
            throw new NotImplementedException();
        }

        public async Task<dynamic> UploadObjectTask(BucketUploadFile file,string rootPath)
        {
            string fileSavePath = await CreateAndSaveFile(file, rootPath);
            IObjectsApi objects = GeneralTokenConfigurationSettings<IObjectsApi>.SetToken(new ObjectsApi(), await _authServiceAdapter.GetSecondaryTokenTask());

            dynamic uploadObj = null;

            
            long fileSize = file.fileToUpload.Length;
            int UPLOAD_CHUNCK_SIZE = 2;

            string bucketKey = file.bucketKey;
            string fileName = Path.GetFileName(file.fileToUpload.FileName);

            if (fileSize > UPLOAD_CHUNCK_SIZE * 1024 * 1024)
            {
                uploadObj=await objects.UploadMoreThanChunkSizeObject(fileSize, bucketKey, fileName, fileSavePath,UPLOAD_CHUNCK_SIZE);
            }
            else
            {
                uploadObj=await objects.UploadLessChunkSizeObject(fileSavePath, bucketKey);
                
            }

            File.Delete(fileSavePath);

            return uploadObj;
        }

        #region privateMethods
        private async Task<string> CreateAndSaveFile(BucketUploadFile file, string rootPath)
        {
            string fileSavePath = Path.Combine(rootPath, Path.GetFileName(file.fileToUpload.FileName));

            using (var stream = new FileStream(fileSavePath, FileMode.Create))
            {
                await file.fileToUpload.CopyToAsync(stream);
            }

            return fileSavePath;
        }

       

        





        #endregion


    }


   
}
