using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public static class SalaryCalculatorHelper
    {
        private static int maxSeniorityYearsCompensation = 5;

        // 1% precentage
        public static decimal TraineeSalaryIncreasePercentage { get; } = 0.01m;

        // 5% precentage
        public static decimal JuniorSalaryIncreasePercentage { get; } = 0.05m;
       
        // 10% precentage
        public static decimal SeniorSalaryIncreasePercentage { get; } = 0.1m;

        //15% precentage
        public static decimal ManagerSalaryIncreasePercentage { get; } = 0.15m;

        public static decimal SeniorityCompensationIncrease(decimal salary, int years) {
            if (years > maxSeniorityYearsCompensation)
              return  (salary * maxSeniorityYearsCompensation) / 100;
            else
                return (salary * years)/100;

        }
    }
}
