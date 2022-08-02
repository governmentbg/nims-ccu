using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Eumis.Documents.Enums
{
    [Serializable]
    public class RegistrationTypeNomenclature
    {
        public string Name
        {
            get
            {
                return App_LocalResources.RegistrationTypeNomenclature.ResourceManager.GetString(ResourceKey);
            }
        }

        public string Code { get; set; }
        public string ResourceKey { get; set; }

        public static readonly RegistrationTypeNomenclature Digital = new RegistrationTypeNomenclature { ResourceKey = "Digital", Code = "digital" };
        public static readonly RegistrationTypeNomenclature Paper = new RegistrationTypeNomenclature { ResourceKey = "Paper", Code = "paper" };
        public static readonly RegistrationTypeNomenclature DigitalOrPaper = new RegistrationTypeNomenclature { ResourceKey = "DigitalOrPaper", Code = "digitalOrPaper" };
    }
}
