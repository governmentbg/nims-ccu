using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Common.Localization
{
    public class LocalizationPage
    {
        public LocalizationPage(string controllerName, string actionName)
        {
            this.ControllerName = controllerName;
            this.ActionName = actionName;
        }

        public string ControllerName { get; private set; }

        public string ActionName { get; private set; }
    }
}
