using Electrix.EAI.FluentValidation.FileReader;
using Electrix.EAI.FluentValidation.Validator;
using FluentValidation;
using log4net;
using System;
using System.IO;
using System.Linq;
using System.Linq.Expressions;

namespace Electrix.EAI.FluentValidation
{
    public abstract class ElectrixValidationBase<T> : AbstractValidator<T>
    {
        
        private ILog _logger = LogManager.GetLogger(typeof(ElectrixValidationBase<T>));
        
        public virtual string GetContainFolder()
        {            
            return AppDomain.CurrentDomain.BaseDirectory;
        }

        public ElectrixValidationBase() : this(null, null) { }


        public ElectrixValidationBase(string containFolder, string filename)
        {
            LoadCommonFile();
            var setupRules = LoadSetupFile(containFolder, filename);
            if (setupRules.ValidationRules == null || !setupRules.ValidationRules.Any()) return;

            var errorMessages = setupRules.ErrorMessages;
            foreach (var ruleFor in setupRules.ValidationRules)
            {
                var parameter = Expression.Parameter(typeof(T));
                var memberExpression = Expression.Property(parameter, typeof(T), ruleFor.PropertyName);
                var propertyInfo = typeof(T).GetProperty(ruleFor.PropertyName);
                var propertyType = propertyInfo.PropertyType;
                switch (Type.GetTypeCode(propertyType))
                {
                    case TypeCode.String:
                        var func = Expression.Lambda<Func<T, string>>(memberExpression, parameter);
                        RuleFor(func).NotNull().WithMessage(string.Format(RuleService.GetCommonSetting(CommonSettings.IsRequiredErrorMessage), ruleFor.PropertyName))
                            .SetValidator(new StringValidator(ruleFor.Rules, errorMessages));
                        break;
                    default:
                        _logger?.Error($"Custom Fluent Validation From File - Mising define Property Data Type {propertyType.Name} to build Rule");
                        break;
                }
            }
        }

        public ValidationRule LoadSetupFile(string containFolder, string filename)
        {
            containFolder = containFolder ?? Path.Combine(GetContainFolder(), "Validation\\Rules"); 
            filename = filename ?? $"{typeof(T).Name}ValidationRules.json";

            return RuleService.GetSetupRules<T>(containFolder, filename);
        }

        public void LoadCommonFile()
        {
            string containFolder = Path.Combine(GetContainFolder(), "Common");
            string filename = "commonSettings.json";
            RuleService.GetCommonSettings(containFolder, filename);
        }
    }
}
