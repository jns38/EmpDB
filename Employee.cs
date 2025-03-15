using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }

        public Employee(string firstName,string lastName,string socialSecurityNumber)
        {
            FirstName = firstName;
            LastName = lastName;
            SocialSecurityNumber = socialSecurityNumber;
        }

        // return string representation of Employee object, using properties
        public override string ToString()
        {
            string display_str = "******** Employee Payroll Record ********\n";
            display_str += $"{FirstName} {LastName}\n";
            display_str += $"Social Security Number: {SocialSecurityNumber}\n";

            return display_str;
        }

        public abstract decimal Earnings(); 
    }
}
