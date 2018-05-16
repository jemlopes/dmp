using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioMundiPagg.Services;
using DesafioMundiPagg.Models;

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
            Result result = null;
            if (!_service.ValidateInput(content))
            {
                return BadRequest("Request invalido");
            }

            try
            {
                result = _service.ProcessMessage(statecode, content);
            } catch 
            {
                return BadRequest("Arquivo com formato invalido");
            }
            return Ok(result);
        }
    }
}