using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ManagerSalaryCalculator : ISalaryCalculator
    {
        public decimal CalculateSalary(int years, decimal currentSalary)
        {
            return ((currentSalary) * (years > 5 ? 5 : years)) / 100 + currentSalary + (currentSalary * 15 / 100);
        }
    }
}
