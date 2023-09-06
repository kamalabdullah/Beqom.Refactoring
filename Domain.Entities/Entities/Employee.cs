using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class Employee
    {
        public string Name { get; set; }
        public int Type { get; set; }
        public int Years { get; set; }
        public decimal Salary { get; set; }
    }
}
