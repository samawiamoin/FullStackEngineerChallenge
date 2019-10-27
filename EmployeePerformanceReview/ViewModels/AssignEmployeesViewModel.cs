using EmployeePerformanceReview.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.ViewModels
{
    public class AssignEmployeesViewModel
    {
        public int EmployeeId { get; set; }

        // Employees assigned for performance review
        public List<Employee> Employees { get; set; }

        public string AssignedBy { get; set; }
    }
}
