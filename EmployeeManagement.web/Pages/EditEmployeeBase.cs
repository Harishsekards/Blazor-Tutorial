using AutoMapper;
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

        [Inject]
        public IMapper Mapper{ get; set; }

        [Inject]
        public NavigationManager Navigation { get; set; }

        [Parameter]
        public string Id { get; set; }
        public string PageHeaderText { get; set; }
        public Employee Employee { get; set; } = new Employee();
        public EditEmployeeModel EditEmployeeModel { get; set; } = new EditEmployeeModel();

        public List<Department> Departments = new List<Department>();

        protected override async Task OnInitializedAsync()
        {
            int.TryParse(Id,out int employeeId);
            if (employeeId != 0)
            {
                PageHeaderText = "Edit Employee";
                Employee = await EmployeeService.GetEmployee(employeeId);
            }
            else
            {
                PageHeaderText = "Create Employee";
                Employee = new Employee()
                {
                    DepartmentId = 1,
                    DateOfBirth = DateTime.Now,
                    PhotoPath= "images/noImage.jpg"
                };
            }
            
            Departments = (await DepatmentService.GetDepartments()).ToList();
            Mapper.Map(Employee,EditEmployeeModel);                    
        }

        protected async Task HandleSubmit()
        {
            Mapper.Map(EditEmployeeModel,Employee);
            Employee result = null;
            if (Employee.EmployeeId != 0)
            {                
                result = await EmployeeService.UpdateEmployee(Employee);
            }
            else
            {                
                result = await EmployeeService.CreateEmployee(Employee);
            }
            
            if (result != null)
            {
                Navigation.NavigateTo("/");
            }
        }

        protected async Task Delete()
        {
            await EmployeeService.DeleteEmployee(Employee.EmployeeId);
            Navigation.NavigateTo("/");
        }
    }
}
