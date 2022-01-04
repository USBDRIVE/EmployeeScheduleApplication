using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeScheduleApplication.Models
{
    public class Employee
    {   
        [Key]
        public Guid EmployeeId { get; set; }
        //UserID from AspNetUser table
        public string OwnerId { get; set; }
        public string EmployeeName { get; set; }


    }
   
    
}
