using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Models
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options):
            base(options)
        {

        }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Department>().HasData(
                new Department { DepartmentId = 1,DepartmentName = "IT" },
                new Department { DepartmentId = 2,DepartmentName = "CS"},
                new Department { DepartmentId = 3,DepartmentName = "Payroll"},
                new Department { DepartmentId = 4,DepartmentName = "HR"}
                );
            modelBuilder.Entity<Employee>().HasData(
                new Employee { EmployeeId = 1, FirstName=  "Jack",  LastName=  "London",   DateOfBirth=  new DateTime(1990, 04, 07), Email=  "jacklondon@mvc.com",    DepartmentId = 1, Gender = Gender.Male, PhotoPath=  "images/jack.jpg"  },
                new Employee { EmployeeId = 2, FirstName = "James", LastName = "Langmore", DateOfBirth = new DateTime(1980, 05, 08), Email = "jameslangmore@mvc.com", DepartmentId = 2, Gender = Gender.Male, PhotoPath = "images/james.jpg" },
                new Employee { EmployeeId = 3, FirstName = "Jess", LastName = "Parker",    DateOfBirth = new DateTime(1985, 07, 09), Email = "jessparker@mvc.com",    DepartmentId = 3, Gender = Gender.Male, PhotoPath = "images/jess.jpg" }
                );
        }

    }
}
