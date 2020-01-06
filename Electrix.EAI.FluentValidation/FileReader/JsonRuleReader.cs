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
        public ValidationRule LoadValidationRules<T>(string containFolder, string filename)
        {
            var fullPath = Path.Combine($"{containFolder}", filename);
            if(File.Exists(fullPath))
            {
                var json = File.ReadAllText(fullPath);
                return JsonConvert.DeserializeObject<ValidationRule>(json);
            }
            
            return new ValidationRule();

        }

        public CommonSettings LoadCommonSettings(string containFolder, string filename)
        {
            var fullPath = Path.Combine($"{containFolder}", "commonSettings.json");
            if (File.Exists(fullPath))
            {
                var json = File.ReadAllText(fullPath);
                return JsonConvert.DeserializeObject<CommonSettings>(json);
            }
            
            return new CommonSettings();

        }
    }
}
