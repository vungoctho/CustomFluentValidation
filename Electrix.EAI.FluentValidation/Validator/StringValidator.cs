using FluentValidation;
using log4net;
using System.Collections.Generic;

namespace Electrix.EAI.FluentValidation.Validator
{
    public class StringValidator : TypeValidatorBase<string>
    {
        private ILog _logger = LogManager.GetLogger(typeof(StringValidator));
        public StringValidator(List<Rule> rules, List<KeyValue> errorMessages)
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
                        ruleFor = RuleFor(x => x).Matches(rule.Value?.Length > 0 ? rule.Value[0] : RuleService.GetCommonSetting(CommonSettings.TimePattern))
                            .When(x => !string.IsNullOrWhiteSpace(x));
                        break;
                    case ValidateOperator.RegularPattern:
                        ruleFor = RuleFor(x => x).Matches(rule.Value?.Length > 0 ? rule.Value[0] : "")
                            .When(x => !string.IsNullOrWhiteSpace(x));
                        break;
                    default:
                        _logger.Error($"String validator - Missing define Validate Operator {rule.Name}");
                        break;
                }

                if (ruleFor != null)
                {
                    ruleFor.WithMessage(GetErrorMessage(rule, errorMessages));
                }
            }
        }
    }
}
