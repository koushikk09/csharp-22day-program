using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using EHRMvcCleanDemo.Data;
using EHRMvcCleanDemo.Models;

namespace EHRMvcCleanDemo.Controllers
{
    public class AppointmentController : Controller
    {
        private readonly EHRDbContext _context;

        public AppointmentController(EHRDbContext context)
        {
            _context = context;
        }

        // GET: Appointment/Create
        public IActionResult Create()
        {
            ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "FullName");
            ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "FullName");
            return View();
        }

        // POST: Appointment/Create
        [HttpPost]
        public async Task<IActionResult> Create(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                // ✅ Fix: reload dropdowns
                ViewBag.Doctors = new SelectList(_context.Doctors, "DoctorId", "FullName");
                ViewBag.Patients = new SelectList(_context.Patients, "PatientId", "FullName");

                return View(appointment);
            }

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync(); // ✅ Save first to get ID

            // ✅ Audit AFTER save
            _context.AuditLogs.Add(new AuditLog
            {
                UserId = "DemoUser",
                Action = "Create",
                TableName = "Appointments",
                RecordId = appointment.AppointmentId,
                PatientId = appointment.PatientId,
                AccessDate = DateTime.UtcNow,
                Details = "Created appointment"
            });

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Doctor");
        }
    }
}