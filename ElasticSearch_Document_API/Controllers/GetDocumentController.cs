using ElasticSearch_Document_API.Helpers;
using ElasticSearch_Document_API.Services;
using ElasticSearch_Document_API.Services.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GetDocumentController : ControllerBase
    {
        private readonly IDocumentGiver _documentGiver;
        public GetDocumentController(IDocumentGiver documentGiver)
        {
            _documentGiver = documentGiver;
        }
        [HttpGet]
        public async Task<FileContentResult> Get(string documentId)
        {
            var responseFromElastic = await _documentGiver.GetDocumentFromSavedFiles(documentId);
            return File(Convert.FromBase64String(responseFromElastic.DataBase64), 
                "application/octet-stream", 
                responseFromElastic.Name + "." + responseFromElastic.Type);
        }
    }
}
