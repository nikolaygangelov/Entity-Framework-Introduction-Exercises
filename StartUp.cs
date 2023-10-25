using SoftUni.Data;
using SoftUni.Models;
using System;
using System.Linq;
using System.Text;

namespace SoftUni
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            //instantiate DbContext
            using SoftUniContext context = new SoftUniContext();

            //printing info about employees on the console by calling GetEmployeesFromResearchAndDevelopment method
            Console.WriteLine(AddNewAddressToEmployee(context));
        }

        //Problem 3
        //passing DbContext parameter
        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            //turning needed info about employee into a collection using anonymous object
            //using less data
            var info = context.Employees
                .Select(e => new
                {
                    Firstname = e.FirstName,
                    Midlename = e.MiddleName,
                    Lastname = e.LastName,
                    Jobtitle = e.JobTitle,
                    Salary = e.Salary,
                    Id = e.EmployeeId
                })
                .ToList();

            //using StringBuilder to gather all info in one string
            StringBuilder sb = new StringBuilder();

            //ordering elements directly in the foreach loop
            foreach (var employee in info.OrderBy(e => e.Id))
            {
                //using AppendLine so that every printing to be on separate line
                sb.AppendLine($"{employee.Firstname} {employee.Lastname} {employee.Midlename} {employee.Jobtitle} {employee.Salary:f2}");
            }

            //using Trim() to get rid of white spaces
            return sb.ToString().Trim();
        }

        //Problem 4
        public static string GetEmployeesWithSalaryOver50000(SoftUniContext context)
        {
            //turning needed info about employee into a collection using anonymous object
            //using less data
            var info = context.Employees
                .Select(e => new
                {
                    Firstname = e.FirstName,
                    Salary = e.Salary
                })
                .Where(e => e.Salary > 50000)
                .ToList();

            //using StringBuilder to gather all info in one string
            StringBuilder sb = new StringBuilder();

            //ordering elements directly in the foreach loop
            foreach (var employee in info.OrderBy(e => e.Firstname))
            {
                //using AppendLine so that every printing to be on separate line
                sb.AppendLine($"{employee.Firstname} - {employee.Salary:f2}");
            }

            //using Trim() to get rid of white spaces
            return sb.ToString().Trim();
        }

        //Problem 5
        public static string GetEmployeesFromResearchAndDevelopment(SoftUniContext context)
        {
            //turning needed info about employee into a collection using anonymous object
            //using less data
            var info = context.Employees
                .Select(e => new
                {
                    Firstname = e.FirstName,
                    Lastname = e.LastName,
                    Department = e.Department.Name,
                    Salary = e.Salary
                })// using filter clause to get employees from "Research and Development" department only 
                .Where(e => e.Department == "Research and Development")
                .ToList();

            //using StringBuilder to gather all info in one string
            StringBuilder sb = new StringBuilder();

            //ordering elements directly in the foreach loop
            foreach (var employee in info.OrderBy(e => e.Salary).ThenByDescending(e => e.Firstname))
            {
                //using AppendLine so that every printing to be on separate line
                sb.AppendLine($"{employee.Firstname} {employee.Lastname} from {employee.Department} - ${employee.Salary:f2}");
            }

            //using Trim() to get rid of white spaces
            return sb.ToString().Trim();
        }

        //Problem 6
        public static string AddNewAddressToEmployee(SoftUniContext context)
        {
            //create new object with needed info to be added
            Address newAddress = new Address()
            {
                AddressText = "Vitoshka 15",
                TownId = 4

            };

            // getting employee with the specific name only 
            Employee? nameNakov = context.Employees
                .FirstOrDefault(e => e.LastName == "Nakov");

            //setting new address of "Nakov" through assigning the new object
            nameNakov.Address = newAddress;

            //changes execution
            context.SaveChanges();

            //turning needed info about employee into a collection using anonymous object
            //using less data
            var info = context.Employees
                .Select(e => new
                {
                    e.Address,
                })
                .ToList();

            //using StringBuilder to gather all info in one string
            StringBuilder sb = new StringBuilder();

            //ordering elements directly in the foreach loop
            //take the first 10 entries of the result
            foreach (var employee in info.OrderByDescending(e => e.Address.AddressId).Take(10))
            {
                //using AppendLine so that every printing to be on separate line
                sb.AppendLine($"{employee.Address.AddressText}");
            }

            //using Trim() to get rid of white spaces
            return sb.ToString().Trim();
        }

    }
}
