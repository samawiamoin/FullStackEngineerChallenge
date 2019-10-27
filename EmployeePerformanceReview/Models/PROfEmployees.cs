using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Models
{
    public class PROfEmployees : Base
    {

        // Employees assigned for performance review
        public List<Employee> Employees { get; set; }

        public string AssignedBy { get; set; }
    }
}
