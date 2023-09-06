using Domain.Entities.Entities;
using Domain.Entities.Enums;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly ISalaryCalculatorFactory _calculatorFactory;

        public EmployeeService(ISalaryCalculatorFactory calculatorFactory)
        {
            _calculatorFactory = calculatorFactory;
        }
        public decimal CalculateNewSalary(EmployeeType type, int years, decimal salary)
        {
            var calculator = _calculatorFactory.CreateCalculator(type);
            return calculator.CalculateSalary(years, salary);
        }

        public string GenerateEmployeeReport(Employee employee)
        {
            var newSalary = CalculateNewSalary((EmployeeType)employee.Type, employee.Years, employee.Salary);
            var maskedName = MaskName(employee.Name);
            return $"Employee Name: {maskedName}, Type: {((EmployeeType)employee.Type)}, Years: {employee.Years}, Salary: {employee.Salary}, New Salary: {newSalary}";
        }

        public string GenerateReport(List<Employee> employees)
        {
            var report = "";
            foreach (var employee in employees)
            {
                report += GenerateEmployeeReport(employee) + Environment.NewLine;
            }
            return report;
        }

        private string MaskName(string name)
        {
            var firstChars = name.Substring(0, 3);
            var length = name.Length - 3;

            for (int i = 0; i < length; i++)
            {
                firstChars += "*";
            }

            return firstChars;
        }
    }
}
