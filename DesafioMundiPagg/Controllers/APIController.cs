using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioMundiPagg.Services;

namespace DesafioMundiPagg.Controllers
{
    [Produces("application/json")]
    [Route("api/censo/{statecode}")]
    public class APIController : Controller
    {
        private readonly IMessagingService _service;
        public APIController(IMessagingService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult Translate(string statecode, [FromBody] string content)
        {
            if (!_service.TryParse(content))
            {
                return BadRequest();
            }
            return Ok("Teste");
        }



    }
}