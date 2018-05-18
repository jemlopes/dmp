using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DesafioMundipagg.Models;

namespace DesafioMundipagg.Controllers
{
    [Produces("application/json")]
    [Route("api/templates")]
    public class TemplatesRestController : Controller
    {
        private readonly TemplateContext _context;

        public TemplatesRestController(TemplateContext context)
        {
            _context = context;
        }

        // GET: api/templates
        [HttpGet]
        public IEnumerable<Template> GetTemplates()
        {
            return _context.Templates;
        }

        // GET: api/templates/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await _context.Templates.SingleOrDefaultAsync(m => m.Id == id);

            if (template == null)
            {
                return NotFound();
            }

            return Ok(template);
        }

        // PUT: api/templates/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTemplate([FromRoute] int id, [FromBody] Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != template.Id)
            {
                return BadRequest();
            }

            if (IsCodeDuplicated(id , template.Code))
            {
                return BadRequest("Code duplicado");
            }

            _context.Entry(template).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TemplateExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/templates
        [HttpPost]
        public async Task<IActionResult> PostTemplate([FromBody] Template template)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (IsDuplicate(template.Code))
            {
                return BadRequest("Code duplicado");
            }

            _context.Templates.Add(template);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTemplate", new { id = template.Id }, template);
        }

        // DELETE: api/templates/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTemplate([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var template = await _context.Templates.SingleOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();

            return Ok(template);
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }


        private bool IsDuplicate(string code)
        {
            return _context.Templates.Any(e => e.Code == code);
        }

        private bool IsCodeDuplicated(int id , string code)
        {
            return _context.Templates.Any(e => e.Code == code && e.Id != id);
        }



    }
}