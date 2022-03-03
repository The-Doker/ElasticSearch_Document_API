using ElasticSearch_Document_API.Services.Abstraction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
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
        public async Task<IActionResult> Get(string documentId)
        {
            try
            {
                if (string.IsNullOrEmpty(documentId))
                    throw new ArgumentNullException(nameof(documentId));

                var responseFromElastic = await _documentGiver.GetDocumentFromSavedFiles(documentId);
                return File(Convert.FromBase64String(responseFromElastic.DataBase64),
                    "application/octet-stream",
                    responseFromElastic.Name + "." + responseFromElastic.Type);
            }
            catch
            {
                return new NotFoundResult();
            }
        }
    }
}
