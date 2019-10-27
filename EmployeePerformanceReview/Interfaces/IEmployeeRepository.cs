using EmployeePerformanceReview.Models;
using EmployeePerformanceReview.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Interfaces
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetEmployees();

        Task<Employee> GetEmployee(int? employeeId);

        Task<int> AddEmployee(EmployeeViewModel employee);

        Task<int> DeleteEmployee(int? employeeId);

        Task UpdateEmployee(Employee employee);

        Task<bool> AssignEmployeesForPR(AssignEmployeesViewModel assignEmployees);

        Task<Employee> GetEmployeeForLogin(AuthenticateUser authUser);
    }
}
