using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrix.EAI.FluentValidation.FileReader
{
    public class JsonRuleReader : IValidationRuleReader
    {
        public ValidationRule LoadValidationRules<T>(string containFolder)
        {
            var filename = $"{typeof(T).Name}ValidationRules.json";
            var fullPath = Path.Combine($"{containFolder}", filename);
            
            var json = File.ReadAllText(fullPath);

            var rules = JsonConvert.DeserializeObject<ValidationRule>(json);
            return rules;

        }

        public CommonSettings LoadCommonSettings(string containFolder)
        {
            var fullPath = Path.Combine($"{containFolder}", "commonSettings.json");
            var json = File.ReadAllText(fullPath);

            var settings = JsonConvert.DeserializeObject<CommonSettings>(json);
            return settings;

        }
    }
}
