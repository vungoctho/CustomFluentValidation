using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Electrix.EAI.FluentValidation
{
    public enum ValidateOperator
    {
        MaximumLength,
        MinimumLength,
        NotNullOrWhiteSpace,
        TimePattern,
        RegularPattern
    }

}
