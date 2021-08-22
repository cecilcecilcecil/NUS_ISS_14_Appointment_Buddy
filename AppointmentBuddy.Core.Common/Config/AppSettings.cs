namespace AppointmentBuddy.Core.Common.Config
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public string PrivateKeyName { get; set; }
        public string PublicKeyName { get; set; }
        public string S3KeyStoreBucketName { get; set; }
        public string S3KeyStoreFilePath { get; set; }
        public string S3BucketName { get; set; }
        public string S3FilePath { get; set; }
    }
}
