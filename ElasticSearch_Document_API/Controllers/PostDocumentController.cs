using ElasticSearch_Document_API.Data;
using ElasticSearch_Document_API.Helpers;
using ElasticSearch_Document_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
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

        public async Task<IActionResult> Post([FromForm] IFormFile uploadedFile)
        {
            try
            {
                if (!AllowedExtensions.AllowedExtensionsList.Any(System.IO.Path.GetExtension(uploadedFile.FileName).Contains))
                    return StatusCode(400);

                var uploadedBase64 = await FileHelper.ConvertToBase64(uploadedFile);
                var result = await _documentSaver.SaveBase64Document(uploadedBase64);
                if (result)
                    return Ok();
                else return StatusCode(500);
            } 
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
