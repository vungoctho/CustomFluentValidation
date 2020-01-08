using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Hosting;

namespace Electrix.EAI.FluentValidation
{
    public class WebValidationBase<T> : ElectrixValidationBase<T>
    {
        public override string GetContainFolder()
        {
            var folder = HttpContext.Current.Server.MapPath("~");
#if DEBUG
            folder = Path.Combine(folder, "bin");
#endif
            return folder;
        }
    }
}
