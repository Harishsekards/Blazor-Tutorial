using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Models
{
    public interface IDepartmentRepository
    {
        Task<Department> GetDepartmentById(int id);
        Task<IEnumerable<Department>> Departments();
    }
}
