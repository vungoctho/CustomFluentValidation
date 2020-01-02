using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrix.EAI.FluentValidation.FileReader
{
    public interface IValidationRuleReader
    {
        ValidationRule LoadValidationRules<T>(string containFolder);
        CommonSettings LoadCommonSettings(string containFolder);
    }
}
