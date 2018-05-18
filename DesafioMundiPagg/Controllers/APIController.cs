using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DesafioMundipagg.Services;
using DesafioMundipagg.Models;
using System.IO;
using System.Text;

namespace DesafioMundipagg.Controllers
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
        public IActionResult Translate(string statecode)
        {
            string content = null;
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                content = reader.ReadToEnd();
            }

            Result result = null;
            if (!_service.ValidateInput(content))
            {
                return BadRequest("Request invalido");
            }

            try
            {
                result = _service.ProcessMessage(statecode, content);
            } catch (YamlDotNet.Core.SyntaxErrorException)
            {
                return BadRequest("Template mal formatada");
            }
            catch(Exception)
            {
                return BadRequest("Arquivo com formato invalido");
            }

            return Ok(result);
        }
    }
}