using ElasticSearch_Document_API.Data;
using ElasticSearch_Document_API.Helpers;
using ElasticSearch_Document_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
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
                ServiceReference1.Service1Client client = new ServiceReference1.Service1Client();
                var wcfStrings = await client.GetDataAsync(0);

                if (!wcfStrings.Any(System.IO.Path.GetExtension(uploadedFile.FileName).Contains))
                    return StatusCode((int)HttpStatusCode.BadRequest);

                var uploadedBase64 = await FileHelper.ConvertToBase64(uploadedFile);
                var result = await _documentSaver.SaveBase64Document(uploadedBase64);
                if (result)
                    return Ok();
                else return StatusCode((int)HttpStatusCode.InternalServerError);
            } 
            catch
            {
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
