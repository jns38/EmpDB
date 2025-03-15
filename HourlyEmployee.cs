using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    public class HourlyEmployee : Employee
    {
        private decimal wage; // wage per hour
        private decimal hours; // hours worked for the week

        // five parameter constructor
        public HourlyEmployee (
            string firstName,
            string lastName,
            string socialSecurityNumber,
            decimal hourlyWage,
            decimal hoursWorked)
            : base(firstName, lastName, socialSecurityNumber)
        {
            Wage = hourlyWage;
            hours = hoursWorked;
        }

        // property that gets and sets hourly employee's wage
        public decimal Wage
        {
            get {
                return wage;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Wage)} must be >= 0)";
                }

                wage = value;
            }
        }

        // property that gets and sets hourly employee's hours
        public decimal Hours
        {
            get
            {
                return hours;
            }
            set
            {
                if (value < 0 || value > 168)
                {
                    throw new ArgumentOutOfRangeException(nameof(value),
                        value, $"{nameof(Hours)} must be >= 0 and <= 168)";
                }

                hours = value;
            }
        }

        // calculate earning
        // override Employee's abstract method Earnings
        public override decimal Earnings()
        {
            if (Hours <= 40) // no overtime
            {
                return Wage * Hours;
            }
            else
            {
                return (40 * Wage) + ((Hours - 40) * Wage * 1.5M);
            }
        }

        // return string representation of HourlyEmployee object
        public override string ToString()
        {
            string display_str = $"Hourly Employee: {base.ToString()}\n";
            display_str += $"Hourly Wage: {Wage:C}\n";
            display_str += $"Hours Worked: {Hours:F2}\n";

            return display_str;
        }
    }

}
