using System.Threading.Tasks;

namespace PoolCalculator.Service.Services
{
    public interface IBonusPoolService
    {
        Task<object> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId);
    }
}
