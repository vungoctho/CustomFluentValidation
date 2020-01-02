using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectrixValidator.FileReader
{
    

    public class JsonRuleReader : IValidationRuleReader
    {
        public ValidationRule LoadValidationRules<T>()
        {
            // Get file validation json by T
            // Read json and parse to ValidationRules
            var filename = $"{typeof(T).Name}ValidationRules.json";
            var fullPath = Path.Combine($"{AppDomain.CurrentDomain.BaseDirectory}/Validation/Rules", filename);
            var json = File.ReadAllText(fullPath);

            var rules = JsonConvert.DeserializeObject<ValidationRule>(json);
            return rules;

        }
    }
}
