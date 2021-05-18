using Microsoft.AspNetCore.Mvc;
using PoolCalculator.Service.Dtos;
using PoolCalculator.Service.Services;
using System.Threading.Tasks;

namespace PoolCalculator.Controllers
{
    [Route("api/v1/[controller]")]
    public class BonusPoolController : Controller
    {
        private readonly IBonusPoolService _service;
        public BonusPoolController(IBonusPoolService service)
        {
            _service = service;
        }

        [HttpPost()]
        public async Task<IActionResult> CalculateBonus([FromBody] CalculateBonusDto request)
        {
            //If request is not valid rturn a BadRequest
            if (!ModelState.IsValid) {
                return BadRequest();
            }

            return Ok(await _service.CalculateAsync(
                request.TotalBonusPoolAmount,
                request.SelectedEmployeeId));
        }
    }
}
