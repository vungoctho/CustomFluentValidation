using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectrixValidator.FileReader
{
    public interface IValidationRuleReader
    {
        ValidationRule LoadValidationRules<T>();

    }
}
