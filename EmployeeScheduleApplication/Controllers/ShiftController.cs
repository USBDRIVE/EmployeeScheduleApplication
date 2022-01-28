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
using System.Security.Claims;

namespace EmployeeScheduleApplication.Controllers
{
    public class ShiftController : Controller
    {
        private readonly ApplicationDbContext _context;


        public ShiftController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Shift>> GetShifts(String userId)
        {            
            var employees = await _context.Shift
                .Where(e => e.OwnerId == userId)
                .ToListAsync();
            return employees;
             

        }
        

        // GET: Shift
        public async Task<IActionResult> Index()
        {
            ViewData["shifts"] = await GetShifts(User.FindFirstValue(ClaimTypes.NameIdentifier));
            return View(await _context.Shift.ToListAsync());
        }

        // GET: Shift/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: Shift/Create
        public async Task<IActionResult> Create()
        {
            //get list of employees
            EmployeeController employeeControllers = new EmployeeController(_context);
            ScheduleController scheduleControllers = new ScheduleController(_context);
            var employeeList = await employeeControllers.GetEmployees(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var scheduleList = await scheduleControllers.GetSchedules(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            ViewData["employeeList"] = employeeList;
            ViewData["scheduleList"] = scheduleList;


            return View();
        }

        // POST: Shift/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ShiftId,StartTime,EndTime")] Shift shift, IFormCollection form)
        {
            string employeeId = form["Employee"]; // returns emplooyee id as string
            string scheduleId = form["Schedule"];
            //query database to get employee where id = employeeID
            //public async Task appendShiftToSchedule(Shift shift, Guid scheduleId)
            //{
            //    Schedule x = await _context.Schedule
            //                  .Where(s => s.ScheduleId == scheduleId)
            //                  .FirstOrDefaultAsync();
            //    x.Shifts.Append(shift);
            //    await _context.SaveChangesAsync();

            //}

            //adding shift to database
            var employee = await _context.Employee
                .FirstOrDefaultAsync(m => m.EmployeeId == Guid.Parse(employeeId));
            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.ScheduleId == Guid.Parse(scheduleId)); 



            shift.ShiftId = Guid.NewGuid();
            shift.Employee = employee;
            shift.Schedule = schedule;
            shift.OwnerId = User.FindFirstValue(ClaimTypes.NameIdentifier);
             _context.Add(shift);


            //ScheduleController sc = new ScheduleController(_context);

            //sc.updategg(schedule, shift);
            
            await _context.SaveChangesAsync();
            // this is pobably bad but yolo
            

            return RedirectToAction(nameof(Index));
        }
        public async void updateSchedule(Schedule s, Shift shift)
        {
            Schedule news = s;
            news.Shifts.Append(shift);

            _context.Entry(s).CurrentValues.SetValues(news);
            await _context.SaveChangesAsync();
        }

        // GET: Shift/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }

        // POST: Shift/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ShiftId,StartTime,EndTime")] Shift shift)
        {
            if (id != shift.ShiftId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.ShiftId))
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
            return View(shift);
        }

        // GET: Shift/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.Shift
                .FirstOrDefaultAsync(m => m.ShiftId == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: Shift/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var shift = await _context.Shift.FindAsync(id);
            _context.Shift.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(Guid id)
        {
            return _context.Shift.Any(e => e.ShiftId == id);
        }
    }
}
