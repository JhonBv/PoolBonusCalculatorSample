using Microsoft.AspNetCore.Mvc;
using PoolCalculator.Service.Dtos;
using PoolCalculator.Service.Services;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace PoolCalculator.Controllers
{
    /// <summary>
    /// Hence we are obtaining a list of Employees from the Database, so using a Service layer makes more sense.
    /// </summary>
    [Route("api/v1/[controller]")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeeService _employees;
        public EmployeesController(IEmployeeService employees)
        {
            _employees = employees;
        }
        [HttpGet]
        [ProducesResponseType(typeof(List<EmployeeDto>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(string), (int)HttpStatusCode.NotFound)]
        [Produces("application/json")]
        public async Task<IActionResult> GetAll()
        {
            var result = await _employees.GetEmployeesAsync();
            
            if (result == null)
            {
                var response = new ErrorResponseDto {
                    Status = (int)HttpStatusCode.NotFound,
                    ErrorDescription = "No relevant data could be found in our database!"
                };
                return BadRequest(response);
            }
            return Ok(result);
        }
    }
}
