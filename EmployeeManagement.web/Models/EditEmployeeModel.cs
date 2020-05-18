using EmployeeManagement.Models;
using EmployeeManagement.Models.CustomValidators;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Models
{
    public class EditEmployeeModel
    {
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "First Name must be provided ")]
        [MinLength(3, ErrorMessage = "First Name mus be minimum of 3 characters")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Last Name mus be provided")]
        [MinLength(3, ErrorMessage = "Last Name mus be minimum of 3 characters")]
        public string LastName { get; set; }

        [EmailAddress]
        [EmailDomainValidator(AllowedDomain = "mvc.com")]
        [Required]
        public string Email { get; set; }

        [Required]
        [EmailAddress]
        [CompareProperty("Email",ErrorMessage ="Email and confirmEmail must match")]
        public string ConfirmEmail { get; set; }

        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
        public int DepartmentId { get; set; }
        public string PhotoPath { get; set; }        
        public Department Department { get; set; }
    }
}
