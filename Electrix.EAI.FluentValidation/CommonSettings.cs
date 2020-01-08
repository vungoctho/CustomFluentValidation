using Electrix.EAI.FluentValidation.FileReader;
using System.Collections.Generic;

namespace Electrix.EAI.FluentValidation
{
    public class CommonSettings
    {
        /*START : define key of error messsage, or key of any value in Common setting json file*/
        public static readonly string TimePattern = "TimePattern";
        public static readonly string IsRequiredErrorMessage = "IsRequiredErrorMessage";

        /*END : define key of error messsage, or key of any value in Common setting json file*/
        public List<KeyValue> Settings { get; set; }

        
    }
}
