using MelonBookshelf.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

//using System.Web.Http;


namespace MelonBookshelf.Controllers.API
{
    [ApiController]
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}")]

    public class BookController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public BookController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        // GET: api/values
        //[Route("resource")]
        //[HttpGet]
        //public List<Resource> Get()
        //{
        //    return new List<Resource>()
        //    {
        //    new Resource() {ResourceId=111,Title = "test1", Author = "test2"},
        //    new Resource() {Title = "test3", Author = "test4"}
        //};
        //}

        [Route("resource"), HttpGet]
        public async Task<IActionResult> Resource(int id)
        {


            using (HttpClient client = _httpClientFactory.CreateClient())
            {
                string cityID = "728385";
                string apiKey = "15f411ae608fb8661399e3a291cbffe5";
                HttpResponseMessage response = await client.GetAsync($"https://api.openweathermap.org/data/2.5/weather?q=Pavlikeni&appid={apiKey}");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();


                    WeatherData item = JsonConvert.DeserializeObject<WeatherData>(content);



                    if (item != null)
                    {
                        return Ok(item);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return StatusCode((int)response.StatusCode, "External API request failed.");
                }
            }

            //// POST api/values
            //[Route("resource")]
            //[HttpPost]
            //public void Post([FromBody] string value)
            //{
            //}

            //// PUT api/values/5
            //[Route("resource")]
            //[HttpPut("{id}")]
            //public void Put(int id, [FromBody] string value)
            //{
            //}

            //// DELETE api/values/5
            //[Route("resource")]
            //[HttpDelete("{id}")]
            //public void Delete(int id)
            //{
            //}
        }
    }
}
