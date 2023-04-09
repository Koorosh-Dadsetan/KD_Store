using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using KD_Store.Models;
using ClosedXML.Excel;
using SelectPdf;

namespace KD_Store.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly TestDbContext _context;

        public EmployeesController(TestDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Employee>? employee { get; set; } = default!;

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Employees.ToListAsync());
        }

        // GET: api/Employees/Search/Text
        [HttpGet]
        public async Task<IActionResult> Search([FromRoute] string id)
        {
            List<Employee> data =
                await _context.Employees
                .Where(x => x.FullName.Contains(id) || x.Mobile.Contains(id))
                .ToListAsync();

            return Ok(data);
        }

        // GET: api/Employees/GetEmployee/5
        [HttpGet]
        public async Task<IActionResult> GetEmployee([FromRoute] int id)
        {
            return Ok(await _context.Employees.FindAsync(id));
        }

        // POST: api/Employees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Employee addEmployee)
        {
            Employee employee = new Employee();

            employee.FullName = addEmployee.FullName;
            employee.Mobile = addEmployee.Mobile;
            employee.Age = addEmployee.Age;
            employee.Address = addEmployee.Address;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        // PUT: api/Employees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPut]
        public async Task<IActionResult> Edit([FromRoute] int id, [FromBody] Employee updateEmployee)
        {
            Employee employee = await _context.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.FullName = updateEmployee.FullName;
            employee.Mobile = updateEmployee.Mobile;
            employee.Address = updateEmployee.Address;
            employee.Age = updateEmployee.Age;

            _context.Employees.Update(employee);
            await _context.SaveChangesAsync();

            return Ok(employee);
        }

        // POST: api/Employees/Delete/5
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            var employee = await _context.Employees.FindAsync(id);
            if (employee != null)
            {
                _context.Employees.Remove(employee);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public FileResult ExportExcel()
        {
            if (_context.Employees != null)
            {
                employee = _context.Employees.AsEnumerable();
            }

            using (XLWorkbook workbook = new XLWorkbook { RightToLeft = true })
            {
                var worksheet = workbook.Worksheets.Add("Employees Sheet");
                var currentRow = 1;
                worksheet.Cell(currentRow, 1).Value = "ردیف";
                worksheet.Cell(currentRow, 2).Value = "نام و نام خانوادگی";
                worksheet.Cell(currentRow, 3).Value = "موبایل";
                worksheet.Cell(currentRow, 4).Value = "سن";
                worksheet.Cell(currentRow, 5).Value = "آدرس";

                foreach (var item in employee)
                {
                    currentRow++;
                    worksheet.Cell(currentRow, 1).Value = item.Id;
                    worksheet.Cell(currentRow, 2).Value = item.FullName;
                    worksheet.Cell(currentRow, 3).Value = item.Mobile;
                    worksheet.Cell(currentRow, 4).Value = item.Age;
                    worksheet.Cell(currentRow, 5).Value = item.Address;
                }

                var myCustomStyle = XLWorkbook.DefaultStyle;
                myCustomStyle.Alignment.SetVertical(XLAlignmentVerticalValues.Center);
                myCustomStyle.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                worksheet.RangeUsed(XLCellsUsedOptions.AllContents).Style = myCustomStyle;

                worksheet.Column(1).AdjustToContents();
                worksheet.Column(2).AdjustToContents();
                worksheet.Column(3).AdjustToContents();
                worksheet.Column(4).AdjustToContents();
                worksheet.Column(5).AdjustToContents();

                using (MemoryStream stream = new MemoryStream())
                {
                    workbook.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Employees.xlsx");
                }
            }
        }

        [HttpGet]
        public FileResult ExportPDF()
        {
            string htmlString = "<html><body><h1>Koorosh Dadsetan</h1></body></html>";

            using (MemoryStream stream = new MemoryStream())
            {
                HtmlToPdf converter = new HtmlToPdf();

                converter.Options.PdfPageSize = PdfPageSize.A4;
                converter.Options.PdfPageOrientation = PdfPageOrientation.Landscape;
                converter.Options.AutoFitWidth = HtmlToPdfPageFitMode.AutoFit;
                converter.Options.AutoFitHeight = HtmlToPdfPageFitMode.AutoFit;

                PdfDocument doc = converter.ConvertHtmlString(htmlString);

                //PdfDocument doc = converter.ConvertUrl("https://localhost:44485/employees");

                doc.Save(stream);
                doc.Close();

                return File(stream.ToArray(), "application/pdf", "Employees.pdf");
            }
        }
    }
}