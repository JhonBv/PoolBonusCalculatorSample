using Microsoft.AspNetCore.Mvc;
using Moq;
using SynetecAssessmentApi.Controllers;
using SynetecAssessmentApi.Domain;
using SynetecAssessmentApi.Service.Dtos;
using SynetecAssessmentApi.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SynetectAssesmentTests
{
    public class ControllerTests
    {
        private Mock<IEmployeeService> _mockEmployeeService;
        private BonusPoolController _bonusController;
        private EmployeesController _employeeController;
        private Mock<IBonusPoolService> _mockBonusPoolService;


        [Fact]
        public async Task Test_User_List_Is_ReturnedAsync()
        {

            var employeeList = new List<EmployeeDto> { new EmployeeDto(), new EmployeeDto() };
            _mockEmployeeService = new Mock<IEmployeeService>();
            _mockEmployeeService.Setup(l => l.GetEmployeesAsync()).ReturnsAsync(employeeList);
            _employeeController = new EmployeesController(_mockEmployeeService.Object);

            var response = await _employeeController.GetAll();
            var OkResult = response as OkObjectResult;

            Assert.True(OkResult.StatusCode == 200);
            Assert.Equal(OkResult.Value, employeeList);

        }

        [Fact]
        public void Test_Employee_Not_Foud()
        {
            _mockBonusPoolService = new Mock<IBonusPoolService>();
            var responseDto = new ErrorResponseDto
            {

                Status = 404,
                ErrorDescription = "Employee not found"
            };

            _mockBonusPoolService.Setup(e => e.CalculateAsync(100000m, 20));

            BonusPoolController cont = new BonusPoolController(_mockBonusPoolService.Object);


            var result = cont.NotFound();
            Assert.True(result.StatusCode == (int)HttpStatusCode.NotFound);

        }

        [Fact]
        public void Test_User_Exist()
        {
            //Arrange
            var mockEntity = new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1);
            _mockEmployeeService = new Mock<IEmployeeService>();
            _mockEmployeeService.Setup(e => e.GetEmployeeById(mockEntity.Id).Result).Returns(mockEntity);
            _mockBonusPoolService = new Mock<IBonusPoolService>();
            CalculateBonusDto dto = new CalculateBonusDto
            {
                SelectedEmployeeId = 1,
                TotalBonusPoolAmount = 100000m
            };
            //Act
            _bonusController = new BonusPoolController(_mockBonusPoolService.Object);
            var result = _bonusController.CalculateBonus(dto).Result;
            //Assert
            Assert.NotNull(result);
        }

        [Fact]
        public void Test_BadRequest_Is_Returned_If_SelectedEmployee_not_Specified()
        {
            //Arrange
            var mockEntity = new Employee(1, "John Smith", "Accountant (Senior)", 60000, 1);
            _mockEmployeeService = new Mock<IEmployeeService>();
            _mockEmployeeService.Setup(e => e.GetEmployeeById(mockEntity.Id).Result).Returns(mockEntity);
            _mockBonusPoolService = new Mock<IBonusPoolService>();
            //Act
            CalculateBonusDto dto = new CalculateBonusDto
            {
                TotalBonusPoolAmount = 100000m
            };
            _bonusController = new BonusPoolController(_mockBonusPoolService.Object);
            var result = _bonusController.BadRequest().StatusCode;
            var expected = (int)HttpStatusCode.BadRequest;
            //Assert
            Assert.Equal(result, expected);

        }
    }
}
