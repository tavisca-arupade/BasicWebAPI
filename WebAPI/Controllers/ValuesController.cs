using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("greet/[controller]")]
    [ApiController]
    public class GreetingController : ControllerBase
    {
        // GET api/values
        [HttpGet]
        public string Get()
        {
            return "say helloo";
        }

        // GET api/values/5
        [HttpGet("{greetValue}")]
        public ActionResult<string> Get(string greetValue)
        {
            return greetValue == "hello" ? "hi" : "hello";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
