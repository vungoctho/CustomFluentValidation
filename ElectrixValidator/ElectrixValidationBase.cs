using ElectrixValidator.FileReader;
using ElectrixValidator.Validator;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ElectrixValidator
{
    public abstract class ElectrixValidationBase<T> : AbstractValidator<T>
    {
        private static ValidationRule _validationRule = null;
        public virtual string GetBaseResourceDirectory()
        {
            return AppDomain.CurrentDomain.BaseDirectory;
        }
       
        protected ValidationRule GetSetupRules {
            get {
                if (_validationRule == null)
                {
                    IValidationRuleReader reader = new JsonRuleReader();
                    _validationRule = reader.LoadValidationRules<T>();
                }
                return _validationRule;
            }
        }
        public ElectrixValidationBase()
        {
            var setupRules = GetSetupRules;
            var errorMessages = setupRules.ErrorMessages;
            foreach (var ruleFor in setupRules.ValidationRules)
            {
                var parameter = Expression.Parameter(typeof(T));
                var memberExpression = Expression.Property(parameter, typeof(T), ruleFor.PropertyName);

                var propertyType = typeof(T).GetProperty(ruleFor.PropertyName).PropertyType;
                switch(propertyType.Name)
                {
                    case "String":
                        var func = Expression.Lambda<Func<T, string>>(memberExpression, parameter);
                        RuleFor(func).SetValidator(new StringValidator(ruleFor.Rules, errorMessages));
                        break;
                }
            }
        }
    }
}
