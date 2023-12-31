﻿using SoftUni.Data;
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

            //printing info about employees on the console by calling AddNewAddressToEmployee method
            Console.WriteLine(AddNewAddressToEmployee(context));
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
