using Electrix.EAI.FluentValidation.FileReader;
using log4net;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrix.EAI.FluentValidation
{
    public class RuleService
    {
        private static ILog _logger = LogManager.GetLogger(typeof(RuleService));

        // We keep this ValidationRule in Cache to optimize the performance since we don't need to read Rules file muliple times
        private static ValidationRule _validationRule = null;
        public static ValidationRule GetSetupRules<T>(string containFolder, string filename)
        {
            if (_validationRule == null)
            {
                IValidationRuleReader reader = new JsonRuleReader();
                _validationRule = reader.LoadValidationRules<T>(containFolder, filename);
                _logger?.Info($"Custom Fluent Validation From File - Load Setup Rules file for {typeof(T)}");
            }
            return _validationRule;
        }



        private static CommonSettings _commonSettings = null;

        public static CommonSettings GetCommonSettings(string containFolder, string filename)
        {            
            if (_commonSettings == null)
            {
                IValidationRuleReader reader = new JsonRuleReader();                
                _commonSettings = reader.LoadCommonSettings(containFolder, filename);
                _logger?.Info($"Custom Fluent Validation From File - Load Common setting file");
            }
            return _commonSettings;
        }

        public static string GetCommonSetting(string key)
        {                        
            if(_commonSettings != null && _commonSettings.Settings?.Any(s=> s.Key == key) == true)
            {
                return _commonSettings.Settings.First(s => s.Key == key).Value;
            }
            return String.Empty;
        }
    }
}
