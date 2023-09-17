using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SeniorSalaryCalculator : ISalaryCalculator
    {
        public decimal CalculateSalary(int years, decimal currentSalary)
        {
            decimal increasePrecentageAmount = currentSalary * SalaryCalculatorHelper.SeniorSalaryIncreasePercentage;
            decimal seniorityLevelIncreaseAmount = SalaryCalculatorHelper.SeniorityCompensationIncrease(currentSalary, years);
            decimal newSalary = currentSalary+ increasePrecentageAmount + seniorityLevelIncreaseAmount;
            return newSalary;
        }
    }
}
