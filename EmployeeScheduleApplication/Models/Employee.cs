using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeScheduleApplication.Models
{
    public class Employee
    {   
        public Guid EmployeeId { get; set; }
        //UserID from AspNetUser table
        public string OwnerId { get; set; }
        public string EmployeeName { get; set; }


    }
   
    
}
