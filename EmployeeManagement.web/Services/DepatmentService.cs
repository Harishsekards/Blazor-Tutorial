using EmployeeManagement.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace EmployeeManagement.web.Services
{
    public class DepatmentService : IDepatmentService
    {
        private readonly HttpClient _httpClient;

        public DepatmentService(HttpClient httpClient)
        {
            this._httpClient = httpClient;
        }
        public async Task<Department> GetDepartmentById(int id)
        {
            return await _httpClient.GetJsonAsync<Department>($"api/depatments/{id}");
        }

        public async Task<IEnumerable<Department>> GetDepartments()
        {
            return await _httpClient.GetJsonAsync<List<Department>>("api/depatments");
        }
    }
}
