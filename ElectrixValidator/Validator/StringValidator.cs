using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectrixValidator.Validator
{
    public class StringValidator : AbstractValidator<string>
    {
        public StringValidator(List<Rule> rules, List<string> errorMessages)
        {
            foreach (var rule in rules)
            {
                switch (rule.Name)
                {
                    case ValidateOperator.MaximumLength:
                        RuleFor(x => x).MaximumLength(int.Parse(rule.Value[0])).WithMessage(errorMessages[rule.ErrorIndex]);
                        break;
                    case ValidateOperator.MinimumLength:
                        RuleFor(x => x).MinimumLength(int.Parse(rule.Value[0])).WithMessage(errorMessages[rule.ErrorIndex]);
                        break;
                }
            }
        }
    }
}
