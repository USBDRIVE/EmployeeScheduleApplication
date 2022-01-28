#nullable disable
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using EmployeeScheduleApplication.Data;
using EmployeeScheduleApplication.Models;
using System.Security.Claims;

namespace EmployeeScheduleApplication.Controllers
{
    public class ScheduleController : Controller
    {
        private readonly ApplicationDbContext _context;
        public ScheduleController(ApplicationDbContext context)
        {
            _context = context;
        }
        public async Task<List<Schedule>> GetSchedules(Guid userId)
        {
            var employees = await _context.Schedule
                .Where(e => e.OwnerId == userId)
                .ToListAsync();
            return employees;


        }
        public async Task<List<Shift>> GetShifts(Schedule schedule)
        {
            List<Shift> shifts = await _context.Shift
                .Where(e => e.Schedule == schedule)
                .ToListAsync();
            return shifts;
        }
        public async void updategg(Schedule s, Shift shift)
        {
            Schedule n = s;
            s.Shifts.Append(shift);
            _context.Update(s);
            await _context.SaveChangesAsync();
        }


        //make a function to add shift to list
        // GET: Schedule
        public async Task<IActionResult> Index()
        {
            ViewData["schedules"] = await GetSchedules(Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)));
            return View(await _context.Schedule.ToListAsync());
        }

        // GET: Schedule/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // GET: Schedule/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Schedule/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ScheduleId,ScheduleName")] Schedule schedule)
        {
            schedule.ScheduleId = Guid.NewGuid();
            schedule.OwnerId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            _context.Add(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Schedule/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule.FindAsync(id);
            if (schedule == null)
            {
                return NotFound();
            }
            return View(schedule);
        }

        // POST: Schedule/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ScheduleId,OwnerId")] Schedule schedule)
        {
            if (id != schedule.ScheduleId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(schedule);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ScheduleExists(schedule.ScheduleId))
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
            return View(schedule);
        }

        private bool ScheduleExists(Guid scheduleId)
        {
            throw new NotImplementedException();
        }

        private bool ScheduleExists(string scheduleId)
        {
            throw new NotImplementedException();
        }
        //public async Task<IActionResult> Details(Schedule sched)
        //{
        //    List<Shift> f  =  await GetShifts(sched);
        //    ViewBag["h"] = f;
        //    return View();
                
        //}

        // GET: Schedule/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var schedule = await _context.Schedule
                .FirstOrDefaultAsync(m => m.ScheduleId == id);
            if (schedule == null)
            {
                return NotFound();
            }

            return View(schedule);
        }

        // POST: Schedule/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var schedule = await _context.Schedule.FindAsync(id);
            _context.Schedule.Remove(schedule);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }


    }
}
