namespace EmpDB
{
    internal class Program
    {
        static void Main()
        {
            DbApp database = new DbApp();

            // Read from EmployeeDb
            database.EmployeeDb();

            // Read input file data
            database.ReadEmployeeDataFromInputFile();

            // Run main DBApp
            database.RunDatabaseApp();



        }
    }
}
