using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class TraineeSalaryCalculator : ISalaryCalculator
    {
        public decimal CalculateSalary(int years, decimal currentSalary)
        {
            return currentSalary / 100 + currentSalary;
        }
    }
}
