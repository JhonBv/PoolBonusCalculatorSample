using Microsoft.EntityFrameworkCore;
using PoolCalculator.Domain;
using PoolCalculator.Persistence;
using PoolCalculator.Service.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PoolCalculator.Service.Services
{
    public class EmployeeService:IEmployeeService
    {
        private readonly AppDbContext _dbContext;
        public EmployeeService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IEnumerable<EmployeeDto>> GetEmployeesAsync()
        {
            IEnumerable<Employee> employees = await _dbContext
                .Employees
                .Include(e => e.Department)
                .ToListAsync();

            List<EmployeeDto> result = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                result.Add(
                    new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    });
            }

            return result;
        }

        public async Task<Employee> GetEmployeeById(int id)
        {
            return await _dbContext.Employees.Include(d => d.Department).FirstOrDefaultAsync(item => item.Id == id);
        }

        /// <summary>
        /// Leaving the responsibility to obtain the Employees' salary budget to the Employees Service.
        /// The Task cannot be awaited as the return is not a Task but a decimal.
        /// </summary>
        /// <returns></returns>
        public Task<decimal> GetSalaryBudget()
        {
            var salaryBudget = _dbContext.Employees.SumAsync(x => x.Salary);
            return salaryBudget;
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
