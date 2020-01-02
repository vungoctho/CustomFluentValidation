using ElectrixValidator;
using ElectrixValidator.Validator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CustomFluentValidation.Models
{
    public class WorkOrderItem 
    {
        public string SystemStatus { get; set; }
        public double? ActivityValue { get; set; }
        
    }
    
    public class WorkOrderItemValidator : ElectrixValidationBase<WorkOrderItem>
    {
        public WorkOrderItemValidator() : base()
        {
        }
    }
}
