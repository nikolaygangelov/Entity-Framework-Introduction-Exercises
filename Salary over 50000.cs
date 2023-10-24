using SoftUni.Data;
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

            //printing info about employees on the console by calling GetEmployeesWithSalaryOver50000 method
            Console.WriteLine(GetEmployeesWithSalaryOver50000(context));
        }


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
    }
}
