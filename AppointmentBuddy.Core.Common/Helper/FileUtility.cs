using Amazon.KeyManagementService;
using Amazon.KeyManagementService.Model;
using Amazon.S3;
using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Amazon.S3.Transfer;
using AppointmentBuddy.Core.Model;

namespace AppointmentBuddy.Core.Common.Helper
{
    public class FileUtility
    {
        public async Task<Stream> DownloadFileFromS3AsStream(string accessKey, string secretAccessKey, string s3bucketName, string s3FilePath, string fileName)
        {
            var awsCredentials = new Amazon.Runtime.BasicAWSCredentials(accessKey, secretAccessKey);
            AmazonS3Client s3Client = new AmazonS3Client();

            ListObjectsRequest listRequest = new ListObjectsRequest();
            listRequest.BucketName = s3bucketName; //Amazon Bucket Name
            listRequest.Prefix = s3FilePath + "/" + fileName;
            listRequest.MaxKeys = 1;
            var listResponse = await s3Client.ListObjectsAsync(listRequest);

            if (listResponse.S3Objects.Count > 0)
            {
                GetObjectRequest request = new GetObjectRequest
                {
                    BucketName = s3bucketName, //Amazon Bucket Name
                    Key = s3FilePath + "/" + fileName, //Amazon S3 Folder path
                };

                var response = await s3Client.GetObjectAsync(request);
                var responseStream = response.ResponseStream;

                return responseStream;
            }

            return null;
        }

        public async Task<Stream> Decrypt(FileDecryptDto dto)
        {
            AmazonKeyManagementServiceClient client = new AmazonKeyManagementServiceClient();

            var memoryStream = new MemoryStream();
            dto.FileStream.CopyTo(memoryStream);

            var response = await client.DecryptAsync(new DecryptRequest
            {
                KeyId = dto.KeyId,  //"80f63bf1-db61-4407-a1a6-ebd9687f0a8f",
                CiphertextBlob = memoryStream // The encrypted data (ciphertext).
            });

            return response.Plaintext;
        }
    }
}
