using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrix.EAI.FluentValidation
{
    public class ValidationRule 
    {        
        public List<KeyValue> ErrorMessages { get; set; }
        public List<ValidationRuleFor> ValidationRules { get; set; }
    }

    public class ValidationRuleFor
    {
        public string PropertyName { get; set; }
        public bool IsRequired { get; set; } = true;
        public List<Rule> Rules { get; set; }
    }

    public class Rule
    {
        public ValidateOperator Name { get; set; }
        public string[] Value { get; set; }
        public string ErrorName { get; set; }
        public string[] ErrorFormatParams { get; set; }        
    }

    public class KeyValue
    {
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
