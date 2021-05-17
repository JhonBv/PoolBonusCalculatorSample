using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Service.Dtos;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace SynetecAssessmentApi.Service.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<decimal> GetSalaryBudget();
        Task<Employee> GetEmployeeById(int id);
    }
}
