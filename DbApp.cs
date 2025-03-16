using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Xml;
using Newtonsoft.Json;
using System.Diagnostics.Metrics;
using System.Diagnostics;

namespace EmpDB
{
    internal class DbApp
    {
        private List<Employee> employees = new List<Employee>();

        public void ReadEmployeeDataFromInputFile()
        {
            string json = File.ReadAllText(Constants.EmployeePayrollOutputJSONFile);
            employees = JsonConvert.DeserializeObject<List<Employee>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            }) ?? new List<Employee>();
        }

        private void WriteDataToOutputFile()
        {
            string json = JsonConvert.SerializeObject(employees, Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            File.WriteAllText(Constants.EmployeePayrollOutputJSONFile, json);
        }

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
                        AddEmployeeRecord();
                        break;
                    case 'f':
                        // Find a record
                        FindEmployeeRecord();
                        break;
                    case 'm':
                        // MOdify a record
                        ModifyEmployeeRecord();
                        break;
                    case 'd':
                        // Delete a record
                        DeleteEmployeeRecord();
                        break;
                    case 'p':
                        // Print all records
                        PrintAllEmployeeRecords();
                        break;
                    case 's':
                        // Save and exit
                        SaveEmployeeDataAndExit();
                        break;
                    case 'e':
                        // Exit without saving
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine($"\nERROR: {selection} is not a vaild input. Select again.\n");
                        break;
                }
            }
        }


        // Check if user input SSN exists
        private Employee? CheckIfSsnExists(string ssn)
        {
            var employee = employees.FirstOrDefault(employee => employee.SocialSecurityNumber == ssn);

            if (employee == null)
            {
                // Did not find a record
                Console.WriteLine($"{ssn} NOT FOUND.\n");
                return null;
            }
            else
            {
                // Found it!
                Console.WriteLine($"FOUND SSN: {ssn}\n");
                return employee;
            }
        }

        // Add new Employee Payroll Record
        private void AddEmployeeRecord()
        {
            // First, search if employee already exists
            Console.Write("Enter SSN to add: ");
            string ssn = Console.ReadLine();
            var employee = CheckIfSsnExists(ssn);
            if (employee == null)
            {
                // Record was NOT FOUND -- go ahead and add
                Console.WriteLine($"Adding new employee w/ SSN: {ssn}");

                // Gather initial employee data
                // Get first name
                Console.Write("Enter first name: ");
                string first = Console.ReadLine();

                //Get last name
                Console.Write("Enter last name: ");
                string last = Console.ReadLine();

                // Already have SSN

                // Get employee type
                Console.Write("[S]alaried, [H]ourly, [C]ommission, or [B]ase plus commission? ");
                string employeeType = Console.ReadLine();

                // Branch out to employee type
                if (employeeType.ToLower() == "s")
                {
                    // Salaried employee
                    Console.Write("Enter weekly salary: ");
                    decimal weeklySal = Convert.ToDecimal(Console.ReadLine());

                    SalariedEmployee salariedEmp = new SalariedEmployee(
                      firstName: first,
                      lastName: last,
                      socialSecurityNumber: ssn,
                      weeklySalary: weeklySal
                      );
                    employees.Add(salariedEmp);
                }   
                else if (employeeType.ToLower() == "h")
                {
                    // Hourly employee
                    Console.Write("Enter hourly wage: ");
                    decimal hrlyWage = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter hours worked: ");
                    decimal hrsWorked = Convert.ToDecimal(Console.ReadLine());

                    HourlyEmployee hourlyEmp = new HourlyEmployee(
                       firstName: first,
                       lastName: last,
                       socialSecurityNumber: ssn,
                       hourlyWage: hrlyWage,
                       hoursWorked: hrsWorked
                       );
                    employees.Add(hourlyEmp);
                }
                else if (employeeType.ToLower() == "c")
                {
                    // Commission employee
                    Console.Write("Enter gross sales: ");
                    decimal gSales = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("Enter commission rate: ");
                    decimal comRate = Convert.ToDecimal(Console.ReadLine());

                    CommissionEmployee commissionEmp = new CommissionEmployee(
                      firstName: first,
                      lastName: last,
                      socialSecurityNumber: ssn,
                      grossSales: gSales,
                      commissionRate: comRate
                      );
                    employees.Add(commissionEmp);
                }
                else if (employeeType.ToLower() == "b")
                {
                    // Base plus commission employee
                    Console.Write("ENTER gross sales: ");
                    decimal gSales = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("ENTER commission rate: ");
                    decimal comRate = Convert.ToDecimal(Console.ReadLine());

                    Console.Write("ENTER base salary: ");
                    decimal baseSal = Convert.ToDecimal(Console.ReadLine());

                    BasePlusCommissionEmployee basecommissionEmp = new BasePlusCommissionEmployee(
                      firstName: first,
                      lastName: last,
                      socialSecurityNumber: ssn,
                      grossSales: gSales,
                      commissionRate: comRate,
                      baseSalary: baseSal
                      );
                    employees.Add(basecommissionEmp);
                }
            }
            else
            {
                Console.WriteLine($"RECORD FOUND! Can't add employee's {ssn}!");
                Console.WriteLine("Record already exists!");
            }
        }


        // Finds Employee Records based on SSN user inputs
        private void FindEmployeeRecord()
        {
            // Ask user for SSN to search for 
            Console.Write("Please enter the employee's SSN to search for: ");
            string ssn = Console.ReadLine();
            var employee = CheckIfSsnExists(ssn);
            if (employee != null)
            {
                Console.WriteLine(employee);
            }

        }

        // Modify employee payroll record for the SSN user inputs
        private void ModifyEmployeeRecord()
        {
            // Ask user for SSN to modify or update
            Console.Write("Please enter the employee's SSN to modify: ");
            string ssn = Console.ReadLine();
            var employee = CheckIfSsnExists(ssn);
            if (employee != null)
            {
                // Modify first name
                Console.Write("Enter new first name (or press ENTER to keep current information): ");
                string first = Console.ReadLine();
                if (first != "")
                {
                    employee.FirstName = first;
                }

                // Modify last name
                Console.Write("Enter new last name (or press ENTER to keep current information): ");
                string last = Console.ReadLine();
                if (last != "")
                {
                    employee.LastName = last;
                }

                // Modify SSN
                Console.Write("Enter new social security number (or press ENTER to keep current information): ");
                string socialSecurity = Console.ReadLine();
                if (socialSecurity != "")
                {
                    employee.SocialSecurityNumber = socialSecurity;
                }

                // Check Employee Type
                // if SalariedEmployee modify weekly salary
                // if HourlyEmployee modify hourly wage and hours worked
                // if CommissionEmployee modify gross sales and comission rate
                // if BasePlusCommissionEmployee modify base salary
                if (employee is SalariedEmployee salaried)
                {
                    Console.Write("Enter new weekly salary (or press ENTER to keep current information): ");
                    string weeklySalary = Console.ReadLine();
                    if (weeklySalary != "")
                    {
                        decimal wkSalary = decimal.Parse(weeklySalary);
                        if (wkSalary >= 0)
                        {
                            salaried.WeeklySalary = wkSalary;
                        }
                    }
                }
                else if (employee is HourlyEmployee hourly)
                {
                    Console.Write("Enter new hourly wage (or press ENTER to keep current information): ");
                    string hourlyWage = Console.ReadLine();
                    if (hourlyWage != "")
                    {
                        decimal hrWage = decimal.Parse(hourlyWage);
                        if (hrWage >= 0)
                        {
                            hourly.Wage = hrWage;
                        }
                    }

                    Console.Write("Enter new hours worked (or press ENTER to keep current information): ");
                    string hoursWorked = Console.ReadLine();
                    if (hoursWorked != "")
                    {
                        decimal hrWorked = decimal.Parse(hoursWorked);
                        if (hrWorked >= 0)
                        {
                            hourly.Hours = hrWorked;
                        }
                    }
                }
                else if (employee is CommissionEmployee commission)
                {
                    Console.Write("Enter new gross sales (or press ENTER to keep current information): ");
                    string grossSales = Console.ReadLine();
                    if (grossSales != "")
                    {
                        decimal gross = decimal.Parse(grossSales);
                        if (gross >= 0)
                        {
                            commission.GrossSales = gross;
                        }
                    }

                    Console.Write("Enter new commission rate (or press ENTER to keep current information): ");
                    string commissionRate = Console.ReadLine();
                    if (commissionRate != "")
                    {
                        decimal commRate = decimal.Parse(commissionRate);
                        if (commRate >= 0)
                        {
                            commission.CommissionRate = commRate;
                        }
                    }
                }
                else if (employee is BasePlusCommissionEmployee based)
                {
                    Console.Write("Enter new base salary (or press ENTER to keep current information): ");
                    string baseSalary = Console.ReadLine();
                    if (baseSalary != "")
                    {
                        decimal bSalary = decimal.Parse(baseSalary);
                        if (bSalary >= 0)
                        {
                            based.BaseSalary = bSalary;
                        }
                    }
                }
                Console.WriteLine();
                Console.WriteLine($"{employee.FirstName}'s modification was successful");
                Console.WriteLine();

            }
        }

        // Delete Employee Payroll Record for SSN that user inputed
        private void DeleteEmployeeRecord()
        {
            bool remove = false;
            while (!remove)
            {
                Console.Write("Please enter the social security number of employee to delete: ");
                string ssn = Console.ReadLine();
                var employee = CheckIfSsnExists(ssn);
                if (employee != null)
                {
                    Console.WriteLine(employee);
                    Console.Write("Is this the employee record you wish to delete, [C]onfirm or [D]eny: ");
                    string input = Console.ReadLine();

                    if (input.ToLower() == "c")
                    {
                        employees.Remove(employee);
                        remove = true;
                        Console.WriteLine();
                        Console.Write($"Employee with SNN {ssn} deleted successfully.\n");
                    }
                    else if (input.ToLower() == "d") 
                    {
                        Console.Write("\n[S]earch for another employee record to delete, " +
                            "or press ENTER to return to the main menu: ");
                        string choice = Console.ReadLine();
                        if (choice.ToLower() == "s")
                        {
                            remove = false;
                        }
                        else
                        {
                            remove = true;
                        }
                    }

                }
            }
        }

        // Print all employee payroll records in json file
        private void PrintAllEmployeeRecords()
        {
            Console.WriteLine("***** Printing all employee payroll records in file *****\n");
            foreach (var employee in employees)
            {
                Console.Write(employee);
                Console.WriteLine($"Earned: {employee.Earnings():C}\n");
            }
            Console.WriteLine("***** Done printing *****\n");

        }

        // Save current records in json file and exits
        private void SaveEmployeeDataAndExit()
        {
            Console.WriteLine("Saving data and exiting.");
            WriteDataToOutputFile();
            Environment.Exit(0);
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
            menu += "[S]ave data to file and exit app\n";
            menu += "[E]xit app without saving changes\n";
            menu += "\n";
            menu += "User Key Selection: ";

            Console.Write(menu);
        }

        // Gets the key entered by user without having to hit enter
        private char GetUserSelection()
        {
            ConsoleKeyInfo key = Console.ReadKey();
            return key.KeyChar;
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
                firstName: "Emily",
                lastName: "Evans",
                socialSecurityNumber: "555-55-5555",
                grossSales: 10000.00M,
                commissionRate: .06M
                );

            CommissionEmployee emp6 = new CommissionEmployee(
                firstName: "Fredrick",
                lastName: "Foster",
                socialSecurityNumber: "666-66-6666",
                grossSales: 15000.00M,
                commissionRate: .07M
                );

            BasePlusCommissionEmployee emp7 = new BasePlusCommissionEmployee(
                firstName: "Gina",
                lastName: "Gibson",
                socialSecurityNumber: "777-77-7777",
                grossSales: 5000.00M,
                commissionRate: .04M,
                baseSalary: 300.00M
                );

            BasePlusCommissionEmployee emp8 = new BasePlusCommissionEmployee(
                firstName: "Henry",
                lastName: "Hills",
                socialSecurityNumber: "888-88-8888",
                grossSales: 8000.00M,
                commissionRate: .05M,
                baseSalary: 400.00M
               );

            // Adding employees to employees instance variable
            employees.Add(emp1);
            employees.Add(emp2);
            employees.Add(emp3);
            employees.Add(emp4);
            employees.Add(emp5);
            employees.Add(emp6);
            employees.Add(emp7);
            employees.Add(emp8);
        }
    }
}
