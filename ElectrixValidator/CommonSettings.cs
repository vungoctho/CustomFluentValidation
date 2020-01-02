using ElectrixValidator.FileReader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectrixValidator
{
    public class CommonSettings
    {
        public string TimePattern { get; set; }
        public string DatePattern { get; set; }

        
        private static CommonSettings _commonSettings = null;

        public static CommonSettings GetCommonSettings
        {
            get
            {
                if (_commonSettings == null)
                {
                    IValidationRuleReader reader = new JsonRuleReader();
                    _commonSettings = reader.LoadCommonSettings();
                }
                return _commonSettings;
            }
        }
    }
}
