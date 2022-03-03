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
        private readonly GetDataService.IGetDataService _typesService;
        public PostDocumentController(IDocumentSaver documentSaver, GetDataService.IGetDataService typesService)
        {
            _documentSaver = documentSaver;
            _typesService = typesService;
        }

        public async Task<IActionResult> Post([FromForm] IFormFile uploadedFile)
        {
            try
            {
                var allowedTypes = await _typesService.GetDataAsync();

                if (!allowedTypes.Any(System.IO.Path.GetExtension(uploadedFile.FileName).Contains))
                    return StatusCode((int)HttpStatusCode.BadRequest);

                var uploadedBase64 = await FileHelper.ConvertToBase64(uploadedFile);
                var result = await _documentSaver.SaveBase64Document(uploadedBase64);
                if (result)
                    return Ok();
                else return StatusCode((int)HttpStatusCode.InternalServerError);
            } 
            catch (Exception ex)
            {
                var s = ex.Message;
                return StatusCode((int)HttpStatusCode.InternalServerError);
            }
        }
    }
}
