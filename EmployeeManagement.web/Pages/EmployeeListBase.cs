using EmployeeManagement.Models;
using EmployeeManagement.web.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Pages
{
    public class EmployeeListBase:ComponentBase
    {
        [Inject]
        public IEmployeeService EmployeeService { get; set; }

        public bool ShowFooter { get; set; } = true;
        public List<Employee> Employees { get; set; }
        public int SelectedEmployeeCount { get; set; } = 0;

        public EmployeeListBase()
        {
            Employees = new List<Employee>();
        }
        protected override async Task OnInitializedAsync()
        {            
            Employees = (await EmployeeService.GetEmployees()).ToList();         
        }

        protected void EmployeeSelected(bool isSelected)
        {
            if (isSelected)
            {
                SelectedEmployeeCount++;
            }
            else
            {
                SelectedEmployeeCount--;
            }
        }

        //private void LoadEmployee()
        //{
        //    Employee e1 = new Employee()
        //    {
        //        EmployeeId = 1,
        //        FirstName = "Jack",
        //        LastName = "London",
        //        DateOfBirth = new DateTime(1982,04,05),
        //        DepartmentId = 1,
        //        Email = "jacklondon@mvc.com",
        //        Gender = Gender.Male,
        //        PhotoPath = "images/jack.jpg"
        //    };
        //    Employee e2 = new Employee()
        //    {
        //        EmployeeId = 2,
        //        FirstName = "Robert",
        //        LastName = "Langdon",
        //        DateOfBirth = new DateTime(1984, 07, 05),
        //        DepartmentId = 2,
        //        Email = "robertlangdon@mvc.com",
        //        Gender = Gender.Male,
        //        PhotoPath = "images/robert.jpg"
        //    };
        //    Employee e3 = new Employee()
        //    {
        //        EmployeeId = 3,
        //        FirstName = "James",
        //        LastName = "Langmore",
        //        DateOfBirth = new DateTime(1985, 03, 06),
        //        DepartmentId = 3,
        //        Email = "jameslangmore@mvc.com",
        //        Gender = Gender.Male,
        //        PhotoPath = "images/james.jpg"
        //    };
        //    Employee e4 = new Employee()
        //    {
        //        EmployeeId = 4,
        //        FirstName = "Sara",
        //        LastName = "Parker",
        //        DateOfBirth = new DateTime(1992, 06, 04),
        //        DepartmentId = 1,
        //        Email = "saraparker@mvc.com",
        //        Gender = Gender.Female,
        //        PhotoPath = "images/sara.jpg"
        //    };
        //    Employees = new List<Employee> { e1, e2, e3, e4 };
        //}

    }
}
