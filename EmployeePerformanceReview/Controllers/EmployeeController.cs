using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeePerformanceReview.Interfaces;
using EmployeePerformanceReview.Models;
using EmployeePerformanceReview.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePerformanceReview.Controllers
{
    [Route("api/employee")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        IEmployeeRepository employeeRepository;

        public EmployeeController(IEmployeeRepository _employeeRepository)
        {
            employeeRepository = _employeeRepository;
        }

        [HttpGet]
        [Route("GetEmployees")]
        public async Task<IActionResult> GetEmployees()
        {
            try
            {
                var employees = await employeeRepository.GetEmployees();
                if (employees == null)
                {
                    return NotFound();
                }

                return Ok(employees);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet]
        [Route("GetEmployee/{employeeId}")]
        public async Task<IActionResult> GetEmployee(int? employeeId)
        {
            if (employeeId == null)
            {
                return BadRequest();
            }

            try
            {
                var employee = await employeeRepository.GetEmployee(employeeId);

                if (employee == null)
                {
                    return NotFound();
                }

                return Ok(employee);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(EmployeeViewModel model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var employeeId = await employeeRepository.AddEmployee(model);
                    if (employeeId > 0)
                    {
                        return Ok(employeeId);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("DeleteEmployee/{employeeId}")]
        public async Task<IActionResult> DeleteEmployee(int? employeeId)
        {
            int result = 0;

            if (employeeId == null)
            {
                return BadRequest();
            }

            try
            {
                result = await employeeRepository.DeleteEmployee(employeeId);
                if (result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }


        [HttpPut]
        [Route("UpdateEmployee")]
        public async Task<IActionResult> UpdatePost(Employee model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await employeeRepository.UpdateEmployee(model);

                    return Ok();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName ==
                             "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }

                    return BadRequest();
                }
            }

            return BadRequest();
        }

        [HttpPost]
        [Route("AssignEmployeesForPR")]
        public async Task<IActionResult> AssignEmployeesForPR(AssignEmployeesViewModel assignEmployees)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var assigned = await employeeRepository.AssignEmployeesForPR(assignEmployees);
                    if (assigned)
                    {
                        return Ok(assigned);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                catch (Exception)
                {

                    return BadRequest();
                }

            }

            return BadRequest();
        }
    }
}