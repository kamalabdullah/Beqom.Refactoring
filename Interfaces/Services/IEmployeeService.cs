using Domain.Entities.Entities;
using Domain.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Interfaces.Services
{
    public interface IEmployeeService
    {
        decimal CalculateNewSalary(EmployeeType type, int years, decimal salary);
        string GenerateEmployeeReport(Employee employee);
        string GenerateReport(List<Employee> employees);
    }
}
