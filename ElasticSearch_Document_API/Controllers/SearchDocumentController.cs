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
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SearchDocumentController : ControllerBase
    {
        private readonly IDocumentSearcher _documentSearcher;
        public SearchDocumentController(IDocumentSearcher documentSearcher)
        {
            _documentSearcher = documentSearcher;
        }
        public async Task<IActionResult> Post([FromForm] string searchQuery)
        {
            var result = await _documentSearcher.FindInSavedDocuments(searchQuery);
            return new JsonResult(result);
        }
    }
}
