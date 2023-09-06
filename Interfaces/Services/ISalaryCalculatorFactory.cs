using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface ISalaryCalculatorFactory
    {
        ISalaryCalculator CreateCalculator(EmployeeType type);
    }
}
