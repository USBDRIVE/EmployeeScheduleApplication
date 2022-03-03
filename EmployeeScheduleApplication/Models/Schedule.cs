using System.ComponentModel.DataAnnotations;

namespace EmployeeScheduleApplication.Models
{
    public class Schedule
    {
        //[Key]
        //public int ScheduleId { get; set; } 
        [Key]
        public Guid ScheduleId { get; set; }
        public string ScheduleName { get; set; }


        public List<Shift>? Shifts { get; set; }
        public Guid OwnerId { get; set; }
    }
}
