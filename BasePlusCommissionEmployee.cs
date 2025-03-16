using JsonSubTypes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    public class BasePlusCommissionEmployee : CommissionEmployee
    {
        private decimal baseSalary; // base salary per week

        // six-parameter constructor
        public BasePlusCommissionEmployee(
            string firstName, 
            string lastName, 
            string socialSecurityNumber,
            decimal grossSales, 
            decimal commissionRate, 
            decimal baseSalary)
            : base(firstName, lastName, socialSecurityNumber, grossSales, commissionRate)
        {
            BaseSalary = baseSalary; // validate base salary
        }

        // property that gets and sets BasePlusCommissionEmployee's base salary
        public decimal BaseSalary
        {
            get
            {
                return baseSalary;
            }
            set
            {
                if (value < 0) // validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(BaseSalary)} must be >= 0");
                }

                baseSalary = value;
            }
        }

        // calculate earnings
        public override decimal Earnings() => BaseSalary + base.Earnings(); // base class earnings + base salary

        // return string representation of BasePlusCommissionEmployee object
        public override string ToString() =>
            $"Base-Salaried {base.ToString()}" +
            $"Base Salary: {BaseSalary:C}\n";
    }
}