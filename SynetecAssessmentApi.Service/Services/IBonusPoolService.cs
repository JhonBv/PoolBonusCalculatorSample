using System.Threading.Tasks;

namespace SynetecAssessmentApi.Service.Services
{
    public interface IBonusPoolService
    {
        Task<object> CalculateAsync(decimal bonusPoolAmount, int selectedEmployeeId);
    }
}
