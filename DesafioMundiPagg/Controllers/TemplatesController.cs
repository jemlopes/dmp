﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DesafioMundiPagg.Models;

namespace DesafioMundiPagg.Controllers
{
    public class TemplatesController : Controller
    {
        private readonly TemplateContext _context;

        public TemplatesController(TemplateContext context)
        {
            _context = context;
        }

        // GET: Templates
        public async Task<IActionResult> Index()
        {
            return View(await _context.Templates.ToListAsync());
        }

        // GET: Templates/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .SingleOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // GET: Templates/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Templates/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Code,Content")] Template template)
        {
            if (ModelState.IsValid)
            {
                _context.Add(template);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Templates/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates.SingleOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }
            return View(template);
        }

        // POST: Templates/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Code,Content")] Template template)
        {
            if (id != template.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(template);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TemplateExists(template.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(template);
        }

        // GET: Templates/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var template = await _context.Templates
                .SingleOrDefaultAsync(m => m.Id == id);
            if (template == null)
            {
                return NotFound();
            }

            return View(template);
        }

        // POST: Templates/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var template = await _context.Templates.SingleOrDefaultAsync(m => m.Id == id);
            _context.Templates.Remove(template);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TemplateExists(int id)
        {
            return _context.Templates.Any(e => e.Id == id);
        }
    }
}
