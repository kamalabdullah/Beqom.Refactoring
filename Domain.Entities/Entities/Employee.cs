using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.Entities
{
    public class Employee
    {
        private Employee()
        {
        }
        public Employee(string name, int type, DateTime startDate, Compensation compensation)
        {
            Name = name;
            Type = type;
            StartDate = startDate;
            Compensation = compensation;
        }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public int Type { get; set; }
        public int Years
        {
            get
            {
                int years = DateTime.Now.Year - StartDate.Year;
                return years;
            }
        }
        public Compensation Compensation { get; set; }

    }
}
