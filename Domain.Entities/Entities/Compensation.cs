using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class Compensation
    {
        private Compensation()
        { }
        public Compensation(decimal salary)
        {
            Salary = salary;
        }

        public decimal Salary { get; set; }
    }
}
