using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoolCalculator.Domain
{
    public class CalculateBonus:Entity
    {
        public decimal SalaryPercentage { get; private set; }
        public decimal BonusPoolAmount { get; private set; }
        public decimal EmployeeBaseSalary { get; private set; }
        public decimal TotalWageBugdet { get; private set; }
        public decimal BonusAllocation { get; private set; }
        public CalculateBonus(int id, decimal bonusPoolAmount, decimal employeeBaseSalary, decimal totalWageBudget) : base(id)
        {
            Id = id;
            EmployeeBaseSalary = employeeBaseSalary;
            BonusPoolAmount = bonusPoolAmount;

            SalaryPercentage = employeeBaseSalary / totalWageBudget;
            BonusAllocation = Calculate();
        }

        public decimal Calculate()
        {
            decimal BonusAllocation = (SalaryPercentage * BonusPoolAmount);

            return BonusAllocation;
        }
    }
}
