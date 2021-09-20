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
using Org.BouncyCastle.OpenSsl;
using Org.BouncyCastle.Crypto;
using Org.BouncyCastle.Crypto.Generators;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Asn1.Sec;
using Org.BouncyCastle.Security;
using System.Numerics;

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

        public async Task<EncryptResponse> Encrypt(FileDecryptDto dto)
        {
            AmazonKeyManagementServiceClient client = new AmazonKeyManagementServiceClient();

            using (MemoryStream ms = new MemoryStream())
            using (FileStream file = new FileStream("private.pem", FileMode.Open, FileAccess.Read))
            {
                byte[] bytes = new byte[file.Length];
                file.Read(bytes, 0, (int)file.Length);
                ms.Write(bytes, 0, (int)file.Length);

                var response = await client.EncryptAsync(new EncryptRequest
                {
                    KeyId = dto.KeyId,
                    Plaintext = ms
                });

                string path = @"D:\private.pem";
                TextWriter textWriter = new StreamWriter(path);
                PemWriter pemWriter = new PemWriter(textWriter);

                var test = response.CiphertextBlob.ToArray();

                //File.WriteAllBytes(path, test);

                FileStream fs = new FileStream("D:\\private1.pem", FileMode.Create, FileAccess.Write);
                response.CiphertextBlob.WriteTo(fs);
                fs.Close();

                file.Flush();
                file.Close();

                return response;
            }
        }

        public string ConvertPrivateKeyToPem()
        {
            var ecKeyPairGenerator = new RsaKeyPairGenerator();
            Org.BouncyCastle.Math.BigInteger publicExponent = new Org.BouncyCastle.Math.BigInteger("65537");
            RsaKeyGenerationParameters ecKeyGenParams = new RsaKeyGenerationParameters(publicExponent, new SecureRandom(), 2048, 25);
            ecKeyPairGenerator.Init(ecKeyGenParams);
            AsymmetricCipherKeyPair pair = ecKeyPairGenerator.GenerateKeyPair();

            string path = @"D:\private.pem";
            TextWriter textWriter = new StreamWriter(path);
            PemWriter pemWriter = new PemWriter(textWriter);
            // passing pair results in the private key being written out
            pemWriter.WriteObject(pair);
            pemWriter.Writer.Flush();
            pemWriter.Writer.Close();

            return "";
        }
    }
}
