using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    public class CommissionEmployee : Employee
    {
        //comment
        private decimal grossSales; // gross weekly sales
        private decimal commissionRate; // commission percentage

        // five-parameter contructor
        public CommissionEmployee(string firstName, string lastName, string socialSecurityNumber,
            decimal grossSales, decimal commissionRate)
            : base(firstName, lastName, socialSecurityNumber)
        {
            GrossSales = grossSales; // validate and store gross sales
            CommissionRate = commissionRate; // validate commission rate
        }

        // property that gets and sets commission employee's gross sales
        public decimal GrossSales
        {
            get
            {
                return grossSales;
            }
            set
            {
                if (value < 0) //validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(GrossSales)} must be >= 0");
                }

                grossSales = value;
            }
        }

        // property that gets and sets commission employee's commission rate
        public decimal CommissionRate
        {
            get
            {
                return commissionRate;
            }
            set
            {
                if (value <= 0 || value >= 1) //validation
                {
                    throw new ArgumentOutOfRangeException(nameof(value), value, $"{nameof(CommissionRate)} must be > 0 and < 1");
                }

                commissionRate = value;
            }
        }

        // calculate earnings; override abstract method Earnings in Employee
        public override decimal Earnings() => CommissionRate * GrossSales;

        // return string representation of CommissionEmployee object
        public override string ToString() =>
                   $"Commission employee: {base.ToString()}\n" +
                   $"Gross sales: {GrossSales:C}\n" +
                   $"Commission rate: {CommissionRate:F2}";
    }
}