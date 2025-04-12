using Recrute.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

using Stripe;
using System.Data;
using Recrute.Models;
using ClosedXML.Excel;

namespace ExportToExcelService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ExportController : ControllerBase
    {
        private readonly RecruteDbContext _context;
        public static string CompName {  get; set; }

        public ExportController(RecruteDbContext context)
        {
            _context = context;
        }

        [HttpGet("Workers")]
        public async Task<IActionResult> ExportToExcel()
        {
            // Fetch data from the database
            var workers = await _context.workers
                .Where(x => x.CompName == CompName)
                .ToListAsync();

            // Generate Excel file using ClosedXML
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Workers");

                // Add header row
                worksheet.Cell(1, 1).Value = "Company Name";
                worksheet.Cell(1, 2).Value = "Employe";
                worksheet.Cell(1, 3).Value = "Position";
                worksheet.Cell(1, 4).Value = "Applicant";
                worksheet.Cell(1, 5).Value = "Payment";

                // Populate data rows
                for (int i = 0; i < workers.Count; i++)
                {
                    var worker = workers[i];
                    worksheet.Cell(i + 2, 1).Value = worker.CompName;
                    worksheet.Cell(i + 2, 2).Value = worker.Emp_Username;
                    worksheet.Cell(i + 2, 3).Value = worker.Pozition;
                    worksheet.Cell(i + 2, 4).Value = worker.Ap_Username;
                    worksheet.Cell(i + 2, 5).Value = worker.Payment;
                }

                // Save the workbook to a memory stream
                using (var stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);

                    // Return the file as a downloadable response
                    return File(
                        stream.ToArray(),
                        "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                        "Works.xlsx"
                    );
                }
            }
        }
    }

}
