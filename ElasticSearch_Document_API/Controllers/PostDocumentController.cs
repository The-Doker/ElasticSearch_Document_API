﻿using ElasticSearch_Document_API.Data;
using ElasticSearch_Document_API.Helpers;
using ElasticSearch_Document_API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using GetTypesService = GetDataService.GetDataServiceClient;

namespace ElasticSearch_Document_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PostDocumentController : ControllerBase
    {
        private readonly IDocumentSaver _documentSaver;
        private readonly GetTypesService _typesService;
        public PostDocumentController(IDocumentSaver documentSaver, GetTypesService typesService)
        {
            _documentSaver = documentSaver;
            _typesService = typesService;
        }

        public async Task<IActionResult> Post([FromForm] IFormFile uploadedFile)
        {
            try
            {
                _typesService.ClientCredentials.ServiceCertificate.Authentication.CertificateValidationMode = System.ServiceModel.Security.X509CertificateValidationMode.None;
                _typesService.ClientCredentials.UserName.UserName = "wcf";
                _typesService.ClientCredentials.UserName.Password = "wcf";

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
