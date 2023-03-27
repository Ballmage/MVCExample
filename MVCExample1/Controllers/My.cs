using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVCExample1.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyController : Controller
    {


        private readonly ValuesHolder _holder;
        public MyController(ValuesHolder holder)
        {
            _holder = holder;

        }


        [HttpPost("create")]
        public IActionResult Create([FromQuery] DateTime date, [FromQuery] int temp)
        {
            _holder.Values.Add(date, temp);
            return Ok(_holder.Values);
        }


        [HttpGet("read")]
        public IActionResult Read([FromQuery] DateTime date1, [FromQuery] DateTime date2)
        {
            return Ok(_holder.GetTempBtwDate(date1, date2));
        }


        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date, [FromQuery] int temp)
        {
            _holder.Values[date] = temp;
            return Ok(_holder.Values);
        }


        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime date1, [FromQuery] DateTime date2)
        {
            _holder.DelBtw(date1, date2);
            return Ok(_holder.Values);


        }
    }
}
