﻿using SoftUni.Data;
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
            Console.WriteLine(GetEmployeesFromResearchAndDevelopment(context));
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
    }
}
