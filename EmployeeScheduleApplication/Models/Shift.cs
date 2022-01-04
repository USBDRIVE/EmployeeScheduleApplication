// https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/sql/date-and-time-data
// link to time/date datatypes in ASP.NET

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace EmployeeScheduleApplication.Models
{
    public  class Shift 
    {
        [Key]    
        public Guid ShiftId { get; set; }
        public Employee Employee { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }

    }
}
