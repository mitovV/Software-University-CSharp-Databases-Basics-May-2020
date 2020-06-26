namespace MiniORM.App
{
    using System.Linq;

    using Data;
    using Data.Entities;

    public class Startup
    {
        public static void Main()
        {
            var connectionString = @"Server=.\SQLEXPRESS;Database=MiniORM;Integrated Security=true;";

            var context = new SoftUniDbContext(connectionString);

            context.Employees.Add(new Employee
            {
                FirsName = "Gosho",
                LastName = "Inserted",
                DepartmentId = context.Departments.First().Id,
                IsEmployed = true
            });

            var employee = context.Employees.Last();
            employee.FirsName = "Modified";

            context.SaveChanges();
        }
    }
}
