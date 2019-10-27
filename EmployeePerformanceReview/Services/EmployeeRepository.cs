using EmployeePerformanceReview.Data;
using EmployeePerformanceReview.Interfaces;
using EmployeePerformanceReview.Models;
using EmployeePerformanceReview.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeePerformanceReview.Services
{
    public class EmployeeRepository : IEmployeeRepository
    {
        ApplicationDbContext db;

        public EmployeeRepository(ApplicationDbContext _db)
        {
            db = _db;
        }

        public async Task<int> AddEmployee(EmployeeViewModel employee)
        {
            Employee emp = new Employee();
            emp.AuthenticateUser = new AuthenticateUser();
            emp.AuthenticateUser.Username = employee.Username;
            emp.AuthenticateUser.Password = employee.Password;
            emp.AuthenticateUser.IsAdmin = false;
            emp.FullName = employee.FullName;
            emp.JobTitle = employee.JobTitle;
            if (db != null)
            {
                await db.Employees.AddAsync(emp);
                await db.SaveChangesAsync();

                return emp.Id;
            }

            return 0;
        }

        public async Task<int> DeleteEmployee(int? employeeId)
        {
            int result = 0;

            if (db != null)
            {
                //Find the employee for specific employee id
                var employee = await db.Employees.FirstOrDefaultAsync(x => x.Id == employeeId);

                if (employee != null)
                {
                    //Delete that employee
                    db.Employees.Remove(employee);
                    var PerformanceReviewsToDelete = db.PerformanceReviews.Where(x => x.EmployeeId == employeeId);
                    db.PerformanceReviews.RemoveRange(PerformanceReviewsToDelete);

                    //Commit the transaction
                    result = await db.SaveChangesAsync();
                }
                return result;
            }

            return result;
        }

        public async Task<Employee> GetEmployee(int? employeeId)
        {
            if (db != null)
            {
                return await db.Employees.Where(x => x.Id == employeeId).FirstOrDefaultAsync();
            }

            return null;
        }

        public async Task<List<Employee>> GetEmployees()
        {
            if (db != null)
            {
                return await db.Employees.ToListAsync();
            }

            return null;
        }

        public async Task UpdateEmployee(Employee employee)
        {
            if (db != null)
            {
                //Delete that employee
                db.Employees.Update(employee);

                //Commit the transaction
                await db.SaveChangesAsync();
            }
        }

        public async Task<bool> AssignEmployeesForPR(AssignEmployeesViewModel assignEmployees)
        {
            var employeeToAdd = await GetEmployee(assignEmployees.EmployeeId);
            var emps = db.Employees.Include(x => x.PROfEmployees).Where(x => assignEmployees.Employees.Any(x2 => x2.Id == x.Id));
            foreach (var e in emps)
            {
                e.PROfEmployees.Employees.Add(employeeToAdd);
                e.PROfEmployees.AssignedBy = assignEmployees.AssignedBy;
            }
            await db.SaveChangesAsync();
            return true;
        }

        public async Task<Employee> GetEmployeeForLogin(AuthenticateUser authUser)
        {
            if (db != null)
            {
                var employee = await db.Employees.Include(x => x.AuthenticateUser).Where(x => x.AuthenticateUser.Username == authUser.Username
                                                && x.AuthenticateUser.Password == authUser.Password
                                                && x.AuthenticateUser.IsAdmin == authUser.IsAdmin).FirstOrDefaultAsync();
                if(authUser.IsAdmin && employee == null)
                {
                    var emp = new Employee()
                    {
                        FullName = "Admin Admin",
                        JobTitle = "Administrator"
                    };
                    emp.AuthenticateUser = new AuthenticateUser();
                    emp.AuthenticateUser.Username = authUser.Username;
                    emp.AuthenticateUser.Password = authUser.Password;
                    emp.AuthenticateUser.IsAdmin = authUser.IsAdmin;
                    await db.Employees.AddAsync(emp);
                    await db.SaveChangesAsync();
                    employee = emp;
                }
                return employee;
            }

            return null;
        }
    }
}
