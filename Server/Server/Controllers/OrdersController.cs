using Server.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace HW2_Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdersController : ControllerBase
    {
        // GET: api/<OrdersController>
        [HttpGet]
        public IEnumerable<Vacation> Get()
        {
            return Vacation.Read();
        }

        [HttpGet("AvgPricePerNight")]
        public Object getAvgPrice(int month)
        {
            Vacation v = new Vacation();
            return v.GetAvgPricePerNight(month);
        }

        // GET api/<OrdersController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        
        [HttpGet("/startDate/{start}/endDate/{end}")] //Routing Resource
        public IActionResult GetByDates(DateTime start, DateTime end)
        {
            List<Vacation> vacationsList = Vacation.getByDates(start, end);

            if (vacationsList.Count > 0) //אם הרשימה לא ריקה תחזיר אותה
            {
                return Ok(vacationsList);
            }
            return NotFound("There are no results");
        }


        // POST api/<OrdersController>
        [HttpPost]
        public bool Post([FromBody] Vacation vacation)
        {
            return vacation.Insert();
        }

        // PUT api/<OrdersController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<OrdersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
