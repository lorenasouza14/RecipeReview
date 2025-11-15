using Microsoft.AspNetCore.Mvc;


namespace RecipeReview.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceitaController : ControllerBase
    {
        // GET: api/<ReceitaController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<ReceitaController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<ReceitaController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<ReceitaController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ReceitaController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
