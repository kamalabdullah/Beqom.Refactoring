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
        private const int charNameLenth = 3;
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
            var newSalary = CalculateNewSalary((EmployeeType)employee.Type, employee.Years, employee.Compensation.Salary);
            var maskedName = MaskName(employee.Name);
            return $"Employee Name: {maskedName}, Type: {((EmployeeType)employee.Type)}, Years: {employee.Years}, Salary: {employee.Compensation.Salary}, New Salary: {newSalary}";
        }

        public string GenerateReport(List<Employee> employees)
        {
            StringBuilder reportStringBuilder = new StringBuilder("");
            foreach (var employee in employees)
            {
                reportStringBuilder.Append(GenerateEmployeeReport(employee));
                reportStringBuilder.Append(Environment.NewLine);
            }
            return reportStringBuilder.ToString();
        }
        private string MaskName(string name)
        {
            int length = name.Length;

            if (length <= charNameLenth)
            {
                return new string('*', length);
            }

            StringBuilder stringBuilder = new StringBuilder(length);
            stringBuilder.Append(name, 0, charNameLenth);
            stringBuilder.Append('*', length - charNameLenth);

            return stringBuilder.ToString();
        }
    }
}
