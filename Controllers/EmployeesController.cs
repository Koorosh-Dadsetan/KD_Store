using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KD_Store;
using KD_Store.Models;

namespace KD_Store.Controllers
{
    //[ApiController]
    //[Route("api/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly DBCtx _context;

        public EmployeesController(DBCtx context)
        {
            _context = context;
        }

        // GET: api/Employees
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _context.Employees.ToListAsync());
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
    }
}