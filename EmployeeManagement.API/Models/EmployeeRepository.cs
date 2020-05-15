using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Update.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Models
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext context;

        public EmployeeRepository(AppDbContext context)
        {
            this.context = context;
        }

        public async Task<Employee> Add(Employee newEmployee)
        {
            await context.Employees.AddAsync(newEmployee);
            await context.SaveChangesAsync();
            return newEmployee;

        }

        public async Task<Employee> Delete(int id)
        {
          Employee employee =  await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == id);
            if (employee != null)
            {
                context.Employees.Remove(employee);
                await context.SaveChangesAsync();
            }          
          return employee;
        }

        public async Task<Employee> GetEmployeeByEmail(string Email)
        {
            return await context.Employees.FirstOrDefaultAsync(e => e.Email == Email);
            
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
          return await context.Employees.Include(e => e.Department).FirstOrDefaultAsync(e => e.EmployeeId == id);
        }

        public async Task<IEnumerable<Employee>> GetEmployees()
        {
            return await context.Employees.ToListAsync();
        }

        public async Task<IEnumerable<Employee>> Search(string name, Gender? gender)
        {
            IQueryable<Employee> query =  context.Employees;
            if (!(string.IsNullOrEmpty(name)))
            {
                query = query.Where(e => e.FirstName.Contains(name) || e.LastName.Contains(name));
            }
            if (gender != null)
            {
                query = query.Where(e => e.Gender == gender);
            }

            return await query.ToListAsync();
        }

        public async Task<Employee> Update(Employee updatedEmployee)
        {
            Employee employee = await context.Employees.FirstOrDefaultAsync(e => e.EmployeeId == updatedEmployee.EmployeeId);
            if (employee != null)
            {
                employee.FirstName = updatedEmployee.FirstName;
                employee.LastName = updatedEmployee.LastName;
                employee.Gender = updatedEmployee.Gender;
                employee.Email = updatedEmployee.Email;
                employee.DepartmentId = updatedEmployee.DepartmentId;
                employee.DateOfBirth = updatedEmployee.DateOfBirth;
                employee.PhotoPath = updatedEmployee.PhotoPath;
            }
            await context.SaveChangesAsync();
            return updatedEmployee;
        }
    }
}
