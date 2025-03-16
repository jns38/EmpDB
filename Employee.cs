using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EmpDB;

namespace EmpDB
{
    public abstract class Employee
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string SocialSecurityNumber { get; set; }
       
        public Employee(string firstName, string lastName, string socialSecurityNumber)
        {
            this.FirstName = firstName;
            this.LastName = lastName;
            this.SocialSecurityNumber = socialSecurityNumber;
        }

        // return string representation of Employee object, using properties
        public override string ToString()
        {
            string display_str = $"{FirstName} {LastName}\n";
            display_str += $"Social Security Number: {SocialSecurityNumber}\n";

            return display_str;
        }

        public abstract decimal Earnings();

    }
}

