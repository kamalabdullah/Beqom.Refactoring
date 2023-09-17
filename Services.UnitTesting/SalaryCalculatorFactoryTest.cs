using Application;
using Application.Services;
using Domain.Entities.Enums;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.UnitTesting
{
    public class SalaryCalculatorFactoryTest
    {
        ISalaryCalculatorFactory factory;
        public SalaryCalculatorFactoryTest()
        {
            //Setup
            factory = new SalaryCalculatorFactory();
        }
        [Fact]
        public void CreateCalculator_ReturnsTraineeCalculator_WhenEmployeeTypeIsTrainee()
        {
            // Arrange
            var employeeType = EmployeeType.Trainee;

            // Act
            var calculator = factory.CreateCalculator(employeeType);

            // Assert
            Assert.IsType<TraineeSalaryCalculator>(calculator);
        }
        [Fact]
        public void CreateCalculator_ReturnsJuniorCalculator_WhenEmployeeTypeIsJunior()
        {
            // Arrange
            var employeeType = EmployeeType.Junior;

            // Act
            var calculator = factory.CreateCalculator(employeeType);

            // Assert
            Assert.IsType<JuniorSalaryCalculator>(calculator);
        }

        [Fact]
        public void CreateCalculator_ReturnsSeniorCalculator_WhenEmployeeTypeIsSenior()
        {
            // Arrange
            var employeeType = EmployeeType.Senior;

            // Act
            var calculator = factory.CreateCalculator(employeeType);

            // Assert
            Assert.IsType<SeniorSalaryCalculator>(calculator);
        }

        [Fact]
        public void CreateCalculator_ReturnsManagerCalculator_WhenEmployeeTypeIsManager()
        {
            // Arrange
            var employeeType = EmployeeType.Manager;

            // Act
            var calculator = factory.CreateCalculator(employeeType);

            // Assert
            Assert.IsType<ManagerSalaryCalculator>(calculator);
        }

        [Fact]
        public void CreateCalculator_ThrowsArgumentException_WhenEmployeeTypeIsInvalid()
        {
            // Invalid employee type
            var employeeType = (EmployeeType)99; 

            // Act and Assert
            Assert.Throws<ArgumentException>(() => factory.CreateCalculator(employeeType));
        }
    }
}
