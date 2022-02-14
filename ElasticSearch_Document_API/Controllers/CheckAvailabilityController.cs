using ElasticSearch_Document_API.Services.Abstraction;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace ElasticSearch_Document_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CheckAvailabilityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                HttpResponseMessage response;
                using (var httpClient = new HttpClient())
                {
                    string url = $"http://elasticsearchplugin:9200/documenthelper/_search";
                    string body = @"{""query"":{""multi_match"":{""query"":""" +
                        "VPN"
                        + @""",""fields"":[""attachment.content"",""attachment.author"",""attachment.title""]}},""_source"":{""excludes"": [""attachment.content""]},""highlight"":{""fields"":{""attachment.content"":{""number_of_fragments"":10,""fragment_size"":300}}}}";

                    var content = new StringContent(body, Encoding.UTF8, "application/json");
                    response = httpClient.PostAsync(url, content).Result;
                }
                if (response != null)
                    return Ok("It's working");
                return Ok("It's not working");
            }
            catch
            {
                return BadRequest();
            }
        }
    }
}
