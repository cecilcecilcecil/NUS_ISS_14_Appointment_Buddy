using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace AppointmentBuddy.Core.Model
{
    [Bind("FileStream, KeyId")]
    public class FileDecryptDto
    {
        public Stream FileStream { get; set; }
        public string KeyId { get; set; }

    }
}
