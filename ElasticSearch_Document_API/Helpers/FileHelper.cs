using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Helpers
{
    public static class FileHelper
    {
        public async static Task<string> ConvertToBase64(IFormFile uploadedFile)
        {
            string uploadedBase64;
            using (var memStream = new MemoryStream())
            {
                await uploadedFile.CopyToAsync(memStream);
                uploadedBase64 = Convert.ToBase64String(memStream.ToArray());
            }
            return uploadedBase64;
        }

    }
}
