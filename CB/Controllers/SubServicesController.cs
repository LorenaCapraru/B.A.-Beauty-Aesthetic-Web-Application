﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CB.Data;
using CB.Models;
using Microsoft.AspNetCore.Authorization;

namespace CB.Controllers
{

    public class SubServicesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public SubServicesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: SubServices
        public async Task<IActionResult> Index()
        {
            return View(await _context.SubService.ToListAsync());
        }

        // GET: SubServices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subService = await _context.SubService
                .FirstOrDefaultAsync(m => m.SubServiceId == id);
            if (subService == null)
            {
                return NotFound();
            }

            return View(subService);
        }

        // GET: SubServices/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: SubServices/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("SubServiceId,SubServiceName")] SubService subService)
        {
            if (ModelState.IsValid)
            {
                _context.Add(subService);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(subService);
        }

        // GET: SubServices/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subService = await _context.SubService.FindAsync(id);
            if (subService == null)
            {
                return NotFound();
            }
            return View(subService);
        }

        // POST: SubServices/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("SubServiceId,SubServiceName")] SubService subService)
        {
            if (id != subService.SubServiceId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(subService);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SubServiceExists(subService.SubServiceId))
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
            return View(subService);
        }

        // GET: SubServices/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var subService = await _context.SubService
                .FirstOrDefaultAsync(m => m.SubServiceId == id);
            if (subService == null)
            {
                return NotFound();
            }

            return View(subService);
        }

        // POST: SubServices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var subService = await _context.SubService.FindAsync(id);
            _context.SubService.Remove(subService);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool SubServiceExists(int id)
        {
            return _context.SubService.Any(e => e.SubServiceId == id);
        }
    }
}
