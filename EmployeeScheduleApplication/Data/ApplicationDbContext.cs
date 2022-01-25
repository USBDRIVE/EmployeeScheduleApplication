using EmployeeScheduleApplication.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EmployeeScheduleApplication.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public virtual DbSet<Employee> Employee { get; set; }
        public virtual DbSet<Shift> Shift { get; set; }
        public virtual DbSet<Schedule> Schedule { get; set; }
    }
}