using Microsoft.EntityFrameworkCore;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Persistence;
using SynetecAssessmentApi.Service.Dtos;
using SynetecAssessmentApi.Service.Logging;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace SynetecAssessmentApi.Service.Services
{
    public class BonusPoolService:IBonusPoolService
    {
        private readonly AppDbContext _dbContext;
        private readonly IEmployeeService _employees;
        private readonly ILoggerManager _logger;
        public BonusPoolService(IEmployeeService employees, AppDbContext dbContext, ILoggerManager logger)
        {
            _employees = employees;
            _dbContext = dbContext;
            _logger = logger;
        }

        public async Task<Object> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId)
        {

            //Passing the responsibility to obtain information regarding Employess (Salary, etc) to the EmployeesRepository
            decimal totalSalary = 0.0m;
            Employee employee = null;
            decimal bonusAllocation = 0.0m;
            CalculateBonus calc;
            //calculate the bonus allocation for the employee
            using (_dbContext)
            {
                try
                {
                    employee = await _employees.GetEmployeeById(selectedEmployeeId);
                    totalSalary = await _employees.GetSalaryBudget();
                    calc = new CalculateBonus(employee.Id, bonusPoolAmount, employee.Salary, totalSalary);
                    bonusAllocation = calc.Calculate();
                }

                catch (Exception e) when (employee == null)
                {
                    _logger.LogError(e.Message);
                    return new ErrorResponseDto
                    {
                        Status = (int)HttpStatusCode.NotFound,
                        ErrorDescription = "Employee not found"
                    };
                }
                catch (Exception e) when (totalSalary == 0m)
                {
                    _logger.LogError(e.Message);
                    return new ErrorResponseDto
                    {
                        Status = (int)HttpStatusCode.NoContent,
                        ErrorDescription = "Employee budget could not be calculated"
                    };

                }
            }//using


                

                _logger.LogInfo($"Calculation for {employee.Id } performed on {DateTime.Now}");

                return new BonusPoolCalculatorResultDto
                {
                    Employee = new EmployeeDto
                    {
                        Fullname = employee.Fullname,
                        JobTitle = employee.JobTitle,
                        Salary = employee.Salary,
                        Department = new DepartmentDto
                        {
                            Title = employee.Department.Title,
                            Description = employee.Department.Description
                        }
                    },
                    //Truncating for Display purposes only. If this value was to be passed on to another service for further calculations, then I would not truncate it.
                    BonusAmount = Decimal.Truncate(bonusAllocation)
                };
            

        }
    }
}
