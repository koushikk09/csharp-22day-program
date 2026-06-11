using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EHRMvcCleanDemo.Data;
using EHRMvcCleanDemo.Models;

namespace EHRMvcCleanDemo.Controllers
{
    public class PatientsController : Controller
    {
        private readonly EHRDbContext _context;

        public PatientsController(EHRDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var patients = await _context.Patients
                .Where(p => p.IsActive==true)
                .ToListAsync();

            _context.AuditLogs.Add(new AuditLog
            {
                UserId = "DemoUser",
                Action = "View",
                TableName = "Patients",
                AccessDate = DateTime.UtcNow,
                Details = "Viewed patient list"
            });

            await _context.SaveChangesAsync();

            return View(patients);
        }
    }
}