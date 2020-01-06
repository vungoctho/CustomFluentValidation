using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrix.EAI.FluentValidation.Validator
{
    public abstract class TypeValidatorBase<T> : AbstractValidator<T>
    {
        protected string GetErrorMessage(Rule rule, List<KeyValue> errorMessages)
        {
            var item = errorMessages.FirstOrDefault(s => s.Key == rule.ErrorName);
            if (item != null)
            {
                return string.Format(item.Value, rule.ErrorFormatParams);
            }
            else if (!string.IsNullOrEmpty(RuleService.GetCommonSetting(rule.ErrorName)))
            {
                // Look up in common setting if any
                return string.Format(RuleService.GetCommonSetting(rule.ErrorName), rule.ErrorFormatParams);
            }
            return "Somthing wrongs when finding error message";

        }
    }
}
