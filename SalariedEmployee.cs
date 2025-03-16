using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    public class SalariedEmployee : Employee
    {
        // private instance variable
        private decimal weeklySalary;

        // four-parameter constructor
        public SalariedEmployee(
            string firstName,
            string lastName,
            string socialSecurityNumber,
            decimal weeklySalary)
            : base(firstName, lastName, socialSecurityNumber)
        {
            WeeklySalary = weeklySalary; 
        }

        // property that get and set salaried employee's salary
        public decimal WeeklySalary
        {
            get
            {
                return weeklySalary;
            }
            set
            {
                if (value < 0) // validation 
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                    value,
                    $"{nameof(WeeklySalary)} must be >= 0");
                }

                weeklySalary = value;
            }
        }

        // calculate earning
        // override abstract method Earning in Employee
        public override decimal Earnings() 
        {
            return WeeklySalary;
        }

        // return string representation of Salaried Employee object
        public override string ToString() =>
            $"Salaried Employee: {base.ToString()}" +
            $"Weekly Salary{WeeklySalary:C}\n" +
            $"Earned: {Earnings():C}\n";

    }
}
