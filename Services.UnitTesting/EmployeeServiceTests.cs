using Application;
using Application.Services;
using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using Domain.Entities.Entities;
using Domain.Entities.Enums;
using Interfaces.Services;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UnitTesting
{
    public class EmployeeServiceTests
    {

        private EmployeeService employeeService;
        private IFixture fixture;
        public EmployeeServiceTests()
        {
            //Setup
            fixture = new Fixture().Customize(new AutoMoqCustomization()); 
            var calculatorFactoryMock = fixture.Freeze<Mock<ISalaryCalculatorFactory>>();
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Trainee)).Returns(fixture.Create<TraineeSalaryCalculator>());
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Junior)).Returns(fixture.Create<JuniorSalaryCalculator>());
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Senior)).Returns(fixture.Create<SeniorSalaryCalculator>());
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Manager)).Returns(fixture.Create<ManagerSalaryCalculator>());

            employeeService = new EmployeeService(calculatorFactoryMock.Object);

        }

        [Theory, AutoData]
        public void CalculateNewSalary_TraineeType_ReturnsExpectedValue(int years, decimal salary)
        {
            // Act
            decimal newSalary = employeeService.CalculateNewSalary(EmployeeType.Trainee, years, salary);

            // Assert Trainee Raise will be 1%
            var expected = salary + (salary * 1 / 100);
            Assert.Equal(expected, newSalary);

        }

        [Theory]
        [InlineData(EmployeeType.Junior, 3, 4000, 4320)]
        [InlineData(EmployeeType.Junior, 6, 4000, 4400)]
        public void CalculateNewSalary_JuniorType_ReturnsExpectedValue(EmployeeType employeeType, int years, decimal baseSalary, decimal expectedSalary)
        {
            // Arrange
            employeeType = EmployeeType.Junior;

            // Act
            decimal newSalary = employeeService.CalculateNewSalary(employeeType, years, baseSalary);

            // Assert
            Assert.Equal(expectedSalary, newSalary);
        }

        [Theory]
        [InlineData(EmployeeType.Senior, 5, 6000, 6900)]
        [InlineData(EmployeeType.Senior, 6, 6000, 6900)]
        [InlineData(EmployeeType.Senior, 4, 6000, 6840)]
        public void CalculateNewSalary_SeniorType_ReturnsExpectedValue(EmployeeType employeeType, int years, decimal baseSalary, decimal expectedSalary)
        {
            //Act
            decimal newSalary = employeeService.CalculateNewSalary(employeeType, years, baseSalary);

            //Assert
            Assert.Equal(expectedSalary, newSalary);
        }

        [Theory]
        [InlineData(EmployeeType.Manager, 7, 8000, 9600)]
        [InlineData(EmployeeType.Manager, 4, 8000, 9520)]
        public void CalculateNewSalary_ManagerType_ReturnsExpectedValue(EmployeeType employeeType, int years, decimal baseSalary, decimal expectedSalary)
        {
            //Act
            decimal newSalary = employeeService.CalculateNewSalary(employeeType, years, baseSalary);

            //Assert
            Assert.Equal(expectedSalary, newSalary);
        }

        [Theory, AutoData]
        public void GenerateEmployeeReport_ReturnsExpectedReport(Compensation compensation,DateTime startDate)
        {
            //Arrange
            var employee = fixture.Build<Employee>()
            .With(e => e.Type, (int)EmployeeType.Senior)
            .With(e => e.StartDate, startDate)
            .With(e => e.Compensation, compensation)
            .Create();

            //Act
            var report = employeeService.GenerateEmployeeReport(employee);

            //Assert
            Assert.Contains($"Employee Name: {employee.Name.Substring(0, 3)}***", report);
            Assert.Contains($"Type: {EmployeeType.Senior}", report);
            Assert.Contains($"Years: {DateTime.Now.Year - startDate.Year}", report);
            Assert.Contains($"Salary: {compensation.Salary}", report);
        }

        [Theory, AutoData]
        public void GenerateReport_ReturnsExpectedReportForMultipleEmployees(List<Employee> employees, [Frozen] IFixture fixture)
        {
            //Arrange
            fixture.Customize<Employee>(c =>
            {
                return c.Without(e => e.Name);
            });

            //Act
            var report = employeeService.GenerateReport(employees);

            //Assert
            foreach (var employee in employees)
            {
                Assert.Contains($"Employee Name: {employee.Name.Substring(0, 3)}***", report);
                Assert.Contains($"Type: {employee.Type}", report);
                Assert.Contains($"Years: {employee.Years}", report);
                Assert.Contains($"Salary: {employee.Compensation.Salary}", report);
            }
        }

    }
}
