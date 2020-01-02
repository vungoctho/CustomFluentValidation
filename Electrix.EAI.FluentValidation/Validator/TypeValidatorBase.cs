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
        protected string GetErrorMessage(Rule rule, List<string> errorMessages)
        {
            return string.Format(errorMessages[rule.ErrorIndex], rule.ErrorFormatParams);
        }
    }
}
