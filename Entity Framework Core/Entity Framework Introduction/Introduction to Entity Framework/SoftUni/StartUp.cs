namespace SoftUni
{
    using System;
    using System.Linq;
    using System.Text;

    using Data;
    using Models;

    public class StartUp
    {
        public static void Main()
        {
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employyes = context
                .Employees
                .OrderBy(e => e.EmployeeId)
                .Select(e => sb.AppendLine($"{string.Join(" ", e.FirstName, e.LastName, e.MiddleName)} {e.JobTitle} {e.Salary:F2}"))
                .ToList();

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Salary > 50000)
                .Select(e => new
                {
                    e.FirstName,
                    e.Salary
                })
                .ToList()
                .OrderBy(e => e.FirstName);


            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} - {employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var employees = context
                .Employees
                .Where(e => e.Department.Name == "Research and Development")
                .Select(e => new
                {
                    e.FirstName,
                    e.LastName,
                    DepartmentName = e.Department.Name,
                    e.Salary
                })
                .OrderBy(e => e.Salary)
                .ThenByDescending(e => e.FirstName)
                .ToList();

            foreach (var employee in employees)
            {
                sb.AppendLine($"{employee.FirstName} {employee.LastName} from {employee.DepartmentName} - ${employee.Salary:F2}");
            }

            return sb.ToString().TrimEnd();
        }

        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            var sb = new StringBuilder();

            var address = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4
            };

            var employee = context
                .Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            employee.Address = address;

            context.SaveChanges();

            var employeesTexts = context
                .Employees
                .OrderByDescending(e => e.AddressId)
                .Take(10)
                .Select(e => e.Address.AddressText)
                .ToList();

            sb.Append(string.Join(Environment.NewLine, employeesTexts));

            return sb.ToString().TrimEnd();
        }
    }
}