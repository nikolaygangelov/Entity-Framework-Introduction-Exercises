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

            //printing info about employees on the console by calling GetEmployeesFullInformation method
            Console.WriteLine(GetEmployeesFullInformation(context));
        }

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

        
    }
}
