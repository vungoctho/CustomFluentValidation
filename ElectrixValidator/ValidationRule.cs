using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectrixValidator
{
    public class ValidationRule 
    {        
        public List<string> ErrorMessages { get; set; }
        public List<ValidationRuleFor> ValidationRules { get; set; }
    }

    public class ValidationRuleFor
    {
        public string PropertyName { get; set; }
        public List<Rule> Rules { get; set; }
    }

    public class Rule
    {
        public ValidateOperator Name { get; set; }
        public string[] Value { get; set; }
        public int ErrorIndex { get; set; }
        public string[] ErrorFormatParams { get; set; }

        
    }
}
