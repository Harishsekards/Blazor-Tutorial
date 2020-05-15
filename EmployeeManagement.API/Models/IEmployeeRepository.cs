using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Models
{
    public interface IEmployeeRepository
    {
        Task<IEnumerable<Employee>> GetEmployees();
        Task<IEnumerable<Employee>> Search(string name,Gender? gender);
        Task<Employee> GetEmployeeById(int id);
        Task<Employee> GetEmployeeByEmail(string Email);
        Task<Employee> Delete(int id);
        Task<Employee> Update(Employee updatedEmployee);
        Task<Employee> Add(Employee newEmployee);
    }
}
