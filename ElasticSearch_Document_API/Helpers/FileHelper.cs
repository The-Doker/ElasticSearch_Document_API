using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
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

        public static HttpResponseMessage MakeHttpResponceFromBase64(string fileBase64, string fileName)
        {
            HttpResponseMessage response;
            response = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(Convert.FromBase64String(fileBase64))
            };
            response.Content.Headers.ContentDisposition =
                    new ContentDispositionHeaderValue("attachment")
                    {
                        FileName = "'" + fileName + "'"
                    };
            response.Content.Headers.ContentType =
                    new MediaTypeHeaderValue("application/octet-stream");
            return response;
        }
    }
}
