using CustomFluentValidation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CustomFluentValidation
{
    class Program
    {
        static void Main(string[] args)
        {
            var wo = new WorkOrderItem();
            wo.SystemStatus = "  ";
            wo.ScheduledStartTime = "02.13.14";

            var wo1 = new WorkOrderItem();
            wo1.SystemStatus = "A7897979897879";

            var validator = new WorkOrderItemValidator();
            var result = validator.Validate(wo);
            Console.WriteLine(result.IsValid);
            Console.WriteLine(result.ToString("\r\n"));

            var result1 = validator.Validate(wo1);
            Console.WriteLine(result1.IsValid);
            Console.WriteLine(result1.ToString("\r\n"));
            Console.ReadLine();
        }
    }
}
