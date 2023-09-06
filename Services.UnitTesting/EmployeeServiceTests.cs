using Application;
using Application.Services;
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
        public EmployeeServiceTests()
        {
            var calculatorFactoryMock = new Mock<ISalaryCalculatorFactory>();
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Trainee)).Returns(new TraineeSalaryCalculator());
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Junior)).Returns(new JuniorSalaryCalculator());
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Senior)).Returns(new SeniorSalaryCalculator());
            calculatorFactoryMock.Setup(factory => factory.CreateCalculator(EmployeeType.Manager)).Returns(new ManagerSalaryCalculator());
            employeeService = new EmployeeService(calculatorFactoryMock.Object);
        }

        [Fact]
        public void CalculateNewSalary_TraineeType_ReturnsExpectedValue()
        {
            decimal newSalary = employeeService.CalculateNewSalary(EmployeeType.Trainee, 2, 3000);
            Assert.Equal(3030, newSalary);
        }

        [Fact]
        public void GenerateEmployeeReport_ReturnsExpectedReport()
        {
            var employee = new Employee
            {
                Name = "John Doe",
                Type = (int)EmployeeType.Senior,
                Years = 7,
                Salary = 5000
            };
            var report = employeeService.GenerateEmployeeReport(employee);
            Assert.Contains("Employee Name: Joh***", report);
            Assert.Contains("Type: Senior", report);
            Assert.Contains("Years: 7", report);
            Assert.Contains("Salary: 5000", report);
        }
        [Fact]
        public void CalculateNewSalary_JuniorType_WithLessThan5Years_ReturnsExpectedValue()
        {
            decimal newSalary = employeeService.CalculateNewSalary(EmployeeType.Junior, 3, 4000);
            Assert.Equal(4200, newSalary);
        }

        [Fact]
        public void CalculateNewSalary_SeniorType_WithExactly5Years_ReturnsExpectedValue()
        {
            decimal newSalary = employeeService.CalculateNewSalary(EmployeeType.Senior, 5, 6000);
            Assert.Equal(6900, newSalary);
        }

        [Fact]
        public void CalculateNewSalary_SeniorType_WithLessThan5Years_ReturnsExpectedValue()
        {
            decimal newSalary = employeeService.CalculateNewSalary(EmployeeType.Senior, 4, 6000);
            Assert.Equal(6840, newSalary);
        }

        [Fact]
        public void CalculateNewSalary_ManagerType_WithMoreThan5Years_ReturnsExpectedValue()
        {
            decimal newSalary = employeeService.CalculateNewSalary(EmployeeType.Manager, 7, 8000);

            Assert.Equal(9600, newSalary);
        }


        [Fact]
        public void GenerateReport_ReturnsExpectedReportForMultipleEmployees()
        {
            var employees = new List<Employee>
        {
            new Employee { Name = "Alice Smith", Type = (int)EmployeeType.Trainee, Years = 1, Salary = 3000 },
            new Employee { Name = "Bob Johnson", Type = (int)EmployeeType.Junior, Years = 4, Salary = 4000 }
        };

            var report = employeeService.GenerateReport(employees);

            Assert.Contains("Employee Name: Ali***", report);
            Assert.Contains("Type: Trainee", report);
            Assert.Contains("Years: 1", report);
            Assert.Contains("Salary: 3000", report);

            Assert.Contains("Employee Name: Bob***", report);
            Assert.Contains("Type: Junior", report);
            Assert.Contains("Years: 4", report);
            Assert.Contains("Salary: 4000", report);
        }

    }
}
