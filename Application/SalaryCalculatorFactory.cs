using Application.Services;
using Domain.Entities.Enums;
using Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application
{
    public class SalaryCalculatorFactory: ISalaryCalculatorFactory
    {
        public ISalaryCalculator CreateCalculator(EmployeeType type)
        {
            switch (type)
            {
                case EmployeeType.Trainee:
                    return new TraineeSalaryCalculator();
                case EmployeeType.Junior:
                    return new JuniorSalaryCalculator();
                case EmployeeType.Senior:
                    return new SeniorSalaryCalculator();
                case EmployeeType.Manager:
                    return new ManagerSalaryCalculator();
                default:
                    throw new ArgumentException("Invalid employee type");
            }
        }
    }
}
