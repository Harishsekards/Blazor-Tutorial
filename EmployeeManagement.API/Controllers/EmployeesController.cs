using EmployeeManagement.API.Models;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeesController(IEmployeeRepository employeeRepository)
        {
            this._employeeRepository = employeeRepository;
        }

        [HttpGet]
        public async Task<ActionResult<List<Employee>>> GetEmployees()
        {
            try
            {
                return Ok(await _employeeRepository.GetEmployees());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from Database");
            }

        }

        [HttpGet("{search}")]
        public async Task<ActionResult<IEnumerable<Employee>>> GetEmployees(string name,Gender? gender)
        {
            try
            {
                var model = await _employeeRepository.Search(name,gender);
                if (model.Any())
                {
                    return Ok(model);
                }
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from server");
            }

        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<Employee>> GetEmployee(int id)
        {
            try
            {
                var result = await _employeeRepository.GetEmployeeById(id);
                if (result == null)
                {
                    return NotFound();
                }
                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from Database");
            }
          
        }

        [HttpPost]
        public async Task<ActionResult<Employee>> CreateEmployee(Employee newEmployee)
        {
            try
            {
                if (newEmployee == null)
                {
                    return BadRequest();
                }
                var model = await _employeeRepository.GetEmployeeByEmail(newEmployee.Email);
                if (model != null)
                {
                    ModelState.AddModelError("email","Email already Exists");
                    return BadRequest(ModelState);
                }
               var createdEmployee = await _employeeRepository.Add(newEmployee);
               return CreatedAtAction(nameof(GetEmployee),new {id = createdEmployee.EmployeeId },createdEmployee);
            }
            catch (Exception)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from Database");
            }
        }

        [HttpPut()]
        public async Task<ActionResult<Employee>> UpdateEmployee(Employee updatedEmployee)
        {
            try
            {
               
                var employee = _employeeRepository.GetEmployeeById(updatedEmployee.EmployeeId);
                if (employee == null)
                {
                    return NotFound($"Employee with id {updatedEmployee.EmployeeId} is not found");
                }
                return await _employeeRepository.Update(updatedEmployee);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieving data from Database");
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Employee>> DeleteEmployee(int id)
        {
            try
            {
                var emp = await _employeeRepository.GetEmployeeById(id);
                if (emp == null)
                {
                    return NotFound($"Employee with id {id} doesn't exist anymore");
                }
                return await _employeeRepository.Delete(id);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error retrieveing Data from server");
            }
        }
        
    }
}
