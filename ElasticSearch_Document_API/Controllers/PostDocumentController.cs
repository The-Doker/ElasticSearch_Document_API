using ElasticSearch_Document_API.Helpers;
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
            if (uploadedFile == null)
                return false;
            var uploadedBase64 = await FileHelper.ConvertToBase64(uploadedFile);
            return await _documentSaver.SaveBase64Document(uploadedBase64);
        }
    }
}
