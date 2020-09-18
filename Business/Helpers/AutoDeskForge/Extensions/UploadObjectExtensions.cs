using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Autodesk.Forge;

namespace forgeSampleAPI_DotNetCore.Business.Helpers.AutoDeskForge.Extensions
{
    public static class UploadObjectExtensions
    {
        public static dynamic uploadObj = null;
        private static string contentDisposition = "application/octet-stream";
        public static async Task<dynamic> UploadLessChunkSizeObject(this IObjectsApi objects, string path, string bucketKey)
        {
            using (StreamReader reader = new StreamReader(path))
            {
                uploadObj = await objects.UploadObjectAsync(bucketKey,
                    Path.GetFileName(path),
                    (int)reader.BaseStream.Length,
                    reader.BaseStream,
                    contentDisposition);
            }


            return uploadObj;
        }

        public static async Task<dynamic> UploadMoreThanChunkSizeObject(this IObjectsApi objects, long fileSize,string bucketKey,string fileName,string filePath,int uploadChunkSize=2)
        {
            long chunkSize = uploadChunkSize * 1024 * 1024;
            long numbersOfChunk = (long) Math.Round((double)(fileSize / chunkSize)) + 1;

            long start = 0;
            chunkSize = (numbersOfChunk > 1 ? chunkSize : fileSize);
            long end = chunkSize;

            string sessionId = Guid.NewGuid().ToString();
            string objectKey = Path.GetFileName(fileName);

            using (BinaryReader reader = new BinaryReader(new FileStream(filePath, FileMode.Open)))
            {
                for (int i = 0; i < numbersOfChunk; i++)
                {

                    string range = string.Format("bytes {0}-{1}/{2}", start, end, fileSize);
                    long numberOfBytes = chunkSize + 1;
                    byte[] fileBytes = new byte[numberOfBytes];

                    using (MemoryStream memoryStream = new MemoryStream(fileBytes))
                    {
                        reader.BaseStream.Seek((int)start, SeekOrigin.Begin);
                        int count = reader.Read(fileBytes, 0, (int)numberOfBytes);

                        await memoryStream.WriteAsync(fileBytes, 0, (int)numberOfBytes);
                        memoryStream.Position = 0;


                        uploadObj = await objects.UploadChunkAsync(bucketKey, objectKey,
                            (int)numberOfBytes, range, sessionId, memoryStream, contentDisposition);

                        start = end + 1;
                        chunkSize = ((start + chunkSize > fileSize) ? fileSize - start - 1 : chunkSize);
                        end = start + chunkSize;
                    }

                }
            }

            return uploadObj;
        }


        
    }
}
