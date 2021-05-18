using PoolCalculator.Domain;
using PoolCalculator.Service.Dtos;
using System.Collections.Generic;

using System.Threading.Tasks;

namespace PoolCalculator.Service.Services
{
    public interface IEmployeeService
    {
        Task<IEnumerable<EmployeeDto>> GetEmployeesAsync();
        Task<decimal> GetSalaryBudget();
        Task<Employee> GetEmployeeById(int id);
    }
}
