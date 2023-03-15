using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Net.Http;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Rob.Api;

[Route("api/[controller]")]
[ApiController]
public class ArticlesController : ControllerBase
{
    private readonly HttpClient _httpClient;

    public ArticlesController(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient();
    }

    // GET: api/<ArticlesController>
    [HttpGet]
    public IEnumerable<string> Get()
    {

        return new string[] { "value1", "value2" };
    }

    // GET api/<ArticlesController>/5
    [HttpGet("{id}")]
    public string Get(int id)
    {
        return "value";
    }

    // POST api/<ArticlesController>
    [HttpPost]
    public void Post([FromBody] string value)
    {
    }

    // PUT api/<ArticlesController>/5
    [HttpPut("{id}")]
    public void Put(int id, [FromBody] string value)
    {
    }

    // DELETE api/<ArticlesController>/5
    [HttpDelete("{id}")]
    public void Delete(int id)
    {
    }
}
