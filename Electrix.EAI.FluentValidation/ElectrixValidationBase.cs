using Electrix.EAI.FluentValidation.FileReader;
using Electrix.EAI.FluentValidation.Validator;
using FluentValidation;
using log4net;
using System;
using System.IO;
using System.Linq.Expressions;

namespace Electrix.EAI.FluentValidation
{
    public abstract class ElectrixValidationBase<T> : AbstractValidator<T>
    {
        
        private ILog _logger = LogManager.GetLogger(typeof(ElectrixValidationBase<T>));
        
        public virtual string GetContainFolder()
        {
            return Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Validation\\Rules");
        }
       
        
        public ElectrixValidationBase()
        {
            var setupRules = RuleService.GetSetupRules<T>(GetContainFolder());
            var errorMessages = setupRules.ErrorMessages;
            foreach (var ruleFor in setupRules.ValidationRules)
            {
                var parameter = Expression.Parameter(typeof(T));
                var memberExpression = Expression.Property(parameter, typeof(T), ruleFor.PropertyName);

                var propertyType = typeof(T).GetProperty(ruleFor.PropertyName).PropertyType;
                switch(propertyType.Name.ToLower())
                {
                    case "string":
                        var func = Expression.Lambda<Func<T, string>>(memberExpression, parameter);
                        RuleFor(func).SetValidator(new StringValidator(ruleFor.Rules, errorMessages));
                        break;
                    default:
                        _logger?.Error($"Custom Fluent Validation From File - Mising define Property Data Type {propertyType.Name} to build Rule");
                        break;
                }
            }
        }
    }
}
