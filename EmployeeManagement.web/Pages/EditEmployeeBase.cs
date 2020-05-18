using EmployeeManagement.Models;
using EmployeeManagement.web.Models;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Pages
{
    public class EditEmployeeBase : ComponentBase
    {
        [Inject]
        public IEmployeeService  EmployeeService { get; set; }

        [Inject]
        public IDepatmentService DepatmentService { get; set; }

        [Parameter]
        public string Id { get; set; }
        
        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();

        public List<Department> Departments = new List<Department>();

        protected override async Task OnInitializedAsync()
        {
            Employee = await EmployeeService.GetEmployee(int.Parse(Id));
            Departments = (await DepatmentService.GetDepartments()).ToList();
            EditEmployeeModel.FirstName = Employee.FirstName;
            EditEmployeeModel.LastName = Employee.LastName;
            EditEmployeeModel.Email = Employee.Email;
            EditEmployeeModel.ConfirmEmail = Employee.Email;
            EditEmployeeModel.Department = Employee.Department;
            EditEmployeeModel.DepartmentId = Employee.DepartmentId;
            EditEmployeeModel.Gender = Employee.Gender;
            EditEmployeeModel.PhotoPath = Employee.PhotoPath;            
        }

        protected void HandleSubmit()
        {

        }
    }
}
