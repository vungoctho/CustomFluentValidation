using FluentValidation;
using System.Collections.Generic;

namespace CustomFluentValidation.Models
{
    public static class ValidatorExtensions
    {
        public static readonly string DayPattern = @"^(0[1-9]|[12][0-9]|3[01])[.](0[1-9]|1[012])[.]\d{4}$";
        public static readonly string TimePattern = @"^(?:(?:([01]?\d|2[0-3])\.)?([0-5]?\d)\.)?([0-5]?\d)$";
        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainFewerThan<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num)
        {

            return ruleBuilder.Must((rootObject, list, context) =>
            {
                context.MessageFormatter
                  .AppendArgument("MaxElements", num)
                  .AppendArgument("TotalElements", list.Count);

                return list.Count < num;
            })
            .WithMessage("{PropertyName} must contain fewer than {MaxElements} items. The list contains {TotalElements} element");
        }

        public static IRuleBuilderOptions<T, IList<TElement>> ListMustContainAtLeast<T, TElement>(this IRuleBuilder<T, IList<TElement>> ruleBuilder, int num, string propertyName = null)
        {
            if (string.IsNullOrWhiteSpace(propertyName))
            {
                return ruleBuilder.Must((rootObject, list, context) =>
                {
                    context.MessageFormatter.AppendArgument("RealPropertyName", num);
                    context.MessageFormatter.AppendArgument("MaxElements", num);

                    return list != null && list.Count >= num;
                })
                .WithMessage("{RealPropertyName} must be provided at least {TotalElements} element(s)");
            }
            else
            {
                return ruleBuilder.Must((rootObject, list, context) =>
                {
                    context.MessageFormatter
                      .AppendArgument("MaxElements", num);

                    return list != null && list.Count >= num;
                })
                .WithMessage("{PropertyName} must be provided at least {TotalElements} element(s)");
            }
        }

        public static IRuleBuilderOptions<T, string> NotStartWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(m => m != null && !m.StartsWith(" ")).WithMessage("{PropertyName} should not start with whitespace");
        }

        public static IRuleBuilderOptions<T, string> NotEndWithWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            return ruleBuilder.Must(m => m != null && !m.EndsWith(" ")).WithMessage("{PropertyName} should not end with whitespace");
        }

        public static IRuleBuilderOptions<T, string> NotNullOrWhiteSpace<T>(this IRuleBuilder<T, string> ruleBuilder, string propertyName = null)
        {
            return propertyName != null ? ruleBuilder.Must(m => !string.IsNullOrWhiteSpace(m)).WithMessage($"{propertyName} must be provided") :
                ruleBuilder.Must(m => !string.IsNullOrWhiteSpace(m)).WithMessage("{PropertyName} must be provided");
        }

        public static IRuleBuilderOptions<T, string> MaxLengthString<T>(this IRuleBuilder<T, string> ruleBuilder, int length, string propertyName = null)
        {
            return propertyName != null ? ruleBuilder.MaximumLength(length).WithMessage($"{propertyName} length cannot be more than {length} characters") :
                ruleBuilder.MaximumLength(length).WithMessage("{PropertyName} length cannot be more than " + length.ToString() + " characters");
        }


        

    }
}
