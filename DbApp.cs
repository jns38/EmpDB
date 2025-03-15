using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmpDB
{
    internal class DbApp
    {
        private List<Employee> employees = new List<Employee>();

        public void RunDatabaseApp()
        {
            while (true)
            {
                // First, display the main menu
                DisplayMainMenu();
                char selection = GetUserSelection();
                Console.WriteLine();
                // Switch for user selection
                switch (char.ToLower(selection))
                {
                    case 'a':
                        // Add a record
                        AddStudentRecord();
                        break;
                    case 'f':
                        // Find a record
                        FindStudentRecord();
                        break;
                    case 'm':
                        // MOdify a record
                        ModifyStudentRecord();
                        break;
                    case 'd':
                        // Delete a record
                        DeleteStudentRecord();
                        break;
                    case 'p':
                        // Print all records
                        PrintAllStudentRecords();
                        break;
                    case 'k':
                        // Print emails only
                        PrintAllStudentRecordKeys();
                        break;
                    case 's':
                        // Save and exit
                        SaveStudentDataAndExit();
                        break;
                    case 'e':
                        // Exit without saving
                        // TODO: Add confirmation of exit
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine($"\nERROR: {selection} is not a vaild input. Select again.\n");
                        break;
                }
            }
        }

        private void DisplayMainMenu()
        {
            string menu = string.Empty;
            menu += "*****************************************************\n";
            menu += "*********** Employee Payroll Database App ***********\n";
            menu += "*****************************************************\n";
            menu += "[A]dd a employee record (C in CRUD - Create)\n";
            menu += "[F]ind a employee record (R in CRUD - Read)\n";
            menu += "[M]odify a employee record (U in CRUD - Update)\n";
            menu += "[D]elete a employee record (D in CRUD - Delete)\n";
            menu += "[P]rint all records in current db storage\n";
            menu += "Print all primary [K]eys (social security number\n";
            menu += "[S]ave data to file and exit app\n";
            menu += "[E]xit app without saving changes\n";
            menu += "\n";
            menu += "User Key Selection: ";

            Console.Write(menu);
        }

        public void EmployeeDb()
        {
            SalariedEmployee emp1 = new SalariedEmployee(
                firstName: "Alice",
                lastName: "Albert",
                socialSecurityNumber: "111-11-1111",
                weeklySalary: 800.00M
                );

            SalariedEmployee emp2 = new SalariedEmployee(
                firstName: "Benjamin",
                lastName: "Brown",
                socialSecurityNumber: "222-22-2222",
                weeklySalary: 750.00M
                );

            HourlyEmployee emp3 = new HourlyEmployee(
                firstName: "Cathy",
                lastName: "Carter",
                socialSecurityNumber: "333-33-3333",
                hourlyWage: 16.75M,
                hoursWorked: 40.0M
                );

            HourlyEmployee emp4 = new HourlyEmployee(
                firstName: "David",
                lastName: "Davis",
                socialSecurityNumber: "444-44-4444",
                hourlyWage: 18.50M,
                hoursWorked: 42.0M
                );

            CommissionEmployee emp5 = new CommissionEmployee(
                );

            CommissionEmployee emp6 = new CommissionEmployee(
                );

            BasePlusCommissionEmployee emp7 = new BasePlusCommissionEmployee(
                );

            BasePlusCommissionEmployee emp8 = new BasePlusCommissionEmployee(
               );


        }
    }
}
