#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EmployeeScheduleApplication.Data;
using EmployeeScheduleApplication.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using IdentityModel;

namespace EmployeeScheduleApplication.Controllers
{
    //test account username is j@j.com
    public class EmployeeController : Controller
    {

        
       //sstatic String claimsUserString = System.Security.Claims.ClaimTypes.NameIdentifier.ToString().Substring(69);
       //public static String currUserId = claimsUserString.TrimStart().TrimEnd();
       

        //public String currUserId = claimsUserString.Substring(69); later1



        private readonly ApplicationDbContext _context;

        public EmployeeController(ApplicationDbContext context)
        {
            _context = context;
        }
        public String GetCurrentUserId() 
        {
            //if (User.Identity.IsAuthenticated) // need a differnt way of checking if user authenticated, or get rid of checking?
            //{
                return User.FindFirstValue(ClaimTypes.NameIdentifier);
               
            //}
            //else
            //{
            //    return null;
            //}
            
            //if userIdString comeback null that means user is not logged in
        }
        public async Task<List<Employee>> GetEmployees(String userId)
        {            //get all employees where OwnerId = currUserId

            //await using(var context = _context)
            //{
            //    //var query = from em in context.employee
            //    //            where em.ownerid == curruserid
            //    //            select em;

            //    //list<employee> employees = query.tolist<employee>(); // if this dosent work try to list async

            //    ViewData["employees"] = employees;
            //}
            var employees = await _context.Employee
                .Where(e => e.OwnerId == userId)
                .ToListAsync();
            return employees;
           
            
        }
        // GET: Employee
        public async Task<IActionResult> Index()
        {
           
            ViewData["employees"] = await GetEmployees(GetCurrentUserId());
            //get all employees with owner id of logged in user and put that in a list ( ToListAsync() ).
            return View(await _context.Employee.ToListAsync());
        }

        // GET: Employee/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // GET: Employee/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("EmployeeId,EmployeeName")] Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid();
            
            
            employee.OwnerId = GetCurrentUserId();
            //if (ModelState.IsValid)
            //{

            //    _context.Add(employee);
            //    await _context.SaveChangesAsync();
            //    return RedirectToAction(nameof(Index));
            //}
            _context.Add(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
            return View(employee);
        }

        // GET: Employee/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee.FindAsync(id);
            if (employee == null)
            {
                return NotFound();
            }
            return View(employee);
        }

        // POST: Employee/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("EmployeeId,EmployeeName")] Employee employee)
        {
            if (id != employee.EmployeeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(employee);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EmployeeExists(employee.EmployeeId))
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
            return View(employee);
        }

        // GET: Employee/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        // POST: Employee/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var employee = await _context.Employee.FindAsync(id);
            _context.Employee.Remove(employee);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EmployeeExists(Guid id)
        {
            return _context.Employee.Any(e => e.EmployeeId == id);
        }
    }
}
