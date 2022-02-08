using ElasticSearch_Document_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostDocumentController : ControllerBase
    {
        private readonly IDocumentSaver _documentSaver;
        public PostDocumentController(IDocumentSaver documentSaver)
        {
            _documentSaver = documentSaver;
        }
        public async Task<bool> Post([FromForm] IFormFile uploadedFile)
        {
            string uploadedBase64;

            if (uploadedFile == null)
                return false;
            using (var memStream = new MemoryStream())
            {
                await uploadedFile.CopyToAsync(memStream);
                uploadedBase64 = Convert.ToBase64String(memStream.ToArray());
            }
            return _documentSaver.SaveBase64Document(uploadedBase64);
        }
    }
}
