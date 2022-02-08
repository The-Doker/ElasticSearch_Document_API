using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ElasticController : ControllerBase
    {
        [HttpGet]
        public string Get()
        {
            return "Hello!";
        }
    }
}
