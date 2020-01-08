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
        private static Dictionary<string,ValidationRule>  _validationRules = new Dictionary<string, ValidationRule>();
        public static ValidationRule GetSetupRules<T>(string containFolder, string filename)
        {
            var fileLoadedKey = $"{containFolder}{filename}";
            if (!_validationRules.ContainsKey(fileLoadedKey))
            {
                IValidationRuleReader reader = new JsonRuleReader();
                var rules = reader.LoadValidationRules<T>(containFolder, filename);
                _validationRules.Add(fileLoadedKey, rules);
                _logger?.Info($"Custom Fluent Validation From File - Load Setup Rules file for {typeof(T)}. Contain Folder: {containFolder}. Filename: {filename}");
                return rules;
            }
            return _validationRules[fileLoadedKey];
        }



        private static CommonSettings _commonSettings = null;

        public static CommonSettings GetCommonSettings(string containFolder, string filename)
        {            
            if (_commonSettings == null || _commonSettings.Settings == null || !_commonSettings.Settings.Any())
            {
                IValidationRuleReader reader = new JsonRuleReader();                
                _commonSettings = reader.LoadCommonSettings(containFolder, filename);
                _logger?.Info($"Custom Fluent Validation From File - Load Common setting file. Contain Folder: {containFolder}. Filename: {filename}");
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
