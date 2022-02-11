using System;
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
    public class AppointmentsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AppointmentsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Appointments

        public async Task<IActionResult> Index(string search)
        {

            var app = from a in _context.Appointment
                      .Include(a => a.Patient)
                      .Include(a => a.Service)
                      .Include(a => a.SubService)
                      select a;
            if (!String.IsNullOrEmpty(search))
            {
                app = app.Where(a => a.Patient.FullName.Contains(search));
            }

            return View(await app.ToListAsync());

        }



        // GET: Appointments/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .Include(a => a.SubService)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // GET: Appointments/Create
        public IActionResult Create()
        {
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FullName");
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName");
            ViewData["SubServiceId"] = new SelectList(_context.SubService, "SubServiceId", "SubServiceName");
            return View();
        }

        // POST: Appointments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AppointmentId,PatientId,Date,ServiceId,SubServiceId,Docs,Notes")] Appointment appointment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(appointment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientId", appointment.PatientId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            ViewData["SubServiceId"] = new SelectList(_context.SubService, "SubServiceId", "SubServiceId", appointment.SubServiceId);
            return View(appointment);
        }

        // GET: Appointments/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment.FindAsync(id);
            if (appointment == null)
            {
                return NotFound();
            }
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "FullName", appointment.PatientId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceName", appointment.ServiceId);
            ViewData["SubServiceId"] = new SelectList(_context.SubService, "SubServiceId", "SubServiceName", appointment.SubServiceId);
            return View(appointment);
        }

        // POST: Appointments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AppointmentId,PatientId,Date,ServiceId,SubServiceId,Docs,Notes")] Appointment appointment)
        {
            if (id != appointment.AppointmentId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(appointment);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AppointmentExists(appointment.AppointmentId))
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
            ViewData["PatientId"] = new SelectList(_context.Patient, "PatientId", "PatientId", appointment.PatientId);
            ViewData["ServiceId"] = new SelectList(_context.Service, "ServiceId", "ServiceId", appointment.ServiceId);
            ViewData["SubServiceId"] = new SelectList(_context.SubService, "SubServiceId", "SubServiceId", appointment.SubServiceId);
            return View(appointment);
        }

        // GET: Appointments/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var appointment = await _context.Appointment
                .Include(a => a.Patient)
                .Include(a => a.Service)
                .Include(a => a.SubService)
                .FirstOrDefaultAsync(m => m.AppointmentId == id);
            if (appointment == null)
            {
                return NotFound();
            }

            return View(appointment);
        }

        // POST: Appointments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _context.Appointment.FindAsync(id);
            _context.Appointment.Remove(appointment);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AppointmentExists(int id)
        {
            return _context.Appointment.Any(e => e.AppointmentId == id);
        }
    }
}
