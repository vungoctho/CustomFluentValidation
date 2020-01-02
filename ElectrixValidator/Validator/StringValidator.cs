using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectrixValidator.Validator
{
    public class StringValidator : TypeValidatorBase<string>
    {
        public StringValidator(List<Rule> rules, List<string> errorMessages)
        {
            foreach (var rule in rules)
            {
                IRuleBuilderOptions<string, string> ruleFor = null;
                switch (rule.Name)
                {
                    case ValidateOperator.MaximumLength:
                        ruleFor = RuleFor(x => x).MaximumLength(int.Parse(rule.Value[0]));
                        break;
                    case ValidateOperator.MinimumLength:
                        ruleFor = RuleFor(x => x).MinimumLength(int.Parse(rule.Value[0]));
                        break;
                    case ValidateOperator.NotNullOrWhiteSpace:
                        ruleFor = RuleFor(x => x).Must(x => !string.IsNullOrWhiteSpace(x));
                        break;
                    case ValidateOperator.TimePattern:
                        ruleFor = RuleFor(x => x).Matches(rule.Value[0]).When(x => !string.IsNullOrWhiteSpace(x));
                        break;
                }

                if(ruleFor != null)
                {
                    ruleFor.WithMessage(GetErrorMessage(rule, errorMessages));
                }
            }
        }
    }
}
