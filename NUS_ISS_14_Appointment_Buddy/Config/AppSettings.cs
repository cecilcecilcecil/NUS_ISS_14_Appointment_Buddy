using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NUS_ISS_14_Appointment_Buddy.WEB.Config
{
    public class AppSettings
    {
        public int PageSize { get; set; }
        public string Secret { get; set; }
        public string UploadAndDownloadFilePath { get; set; }
        public string CannotAwardDate { get; set; }
        public string DisplayFilePath { get; set; }
        public string PrivateKeyName { get; set; }
        public string PublicKeyName { get; set; }
        public string IntranetPolicy { get; set; }
        public string InternetPolicy { get; set; }
        public string ExternalPolicy { get; set; }
        public string KmsKeyId { get; set; }
        public string S3KeyStoreBucketName { get; set; }
        public string S3KeyStoreFilePath { get; set; }
        public string S3WebStaticFilePath { get; set; }
        public string CacheDuration { get; set; }
        public string DashboardCacheDuration { get; set; }
        public string CacheTokenKeyName { get; set; }
        public string CacheBaseKey { get; set; }
    }
}
