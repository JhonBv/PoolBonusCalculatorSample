using System;
using Xunit;
using Moq;
using PoolCalculator.Service.Services;
using PoolCalculator.Domain;


namespace PoolCalculatorTests
{
    
    public class CalculatorTests
    {
        private Mock<IBonusPoolService> _mockCalculator;

        [Fact]
        public void Calculate_Bonus_Calculation()
        {
            //Arrange
            decimal expected = 9163m;
            decimal bonusPool = 100000m;
            decimal expectedPercentage = 0.0916380297823596792668957617m;
            decimal budget = 654750m;
            decimal salary = 60000m;
            _mockCalculator = new Mock<IBonusPoolService>();
            _mockCalculator.Setup(c => c.CalculateAsync(bonusPool, 1)).ReturnsAsync(expected);

            //Act
            CalculateBonus calc = new CalculateBonus(1,bonusPool, salary, budget);
            
            var result = Decimal.Truncate(calc.BonusAllocation);
            var percentageOfBudgetSalary = calc.SalaryPercentage;

            //Assert
            Assert.IsType<decimal>(result);
            Assert.IsType<decimal>(percentageOfBudgetSalary);
            Assert.Equal(expected, result);
            Assert.Equal(expectedPercentage, percentageOfBudgetSalary);

        }
        
    }
}
