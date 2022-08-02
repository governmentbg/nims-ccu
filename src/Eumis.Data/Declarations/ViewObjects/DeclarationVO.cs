using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Declarations.ViewObjects
{
    public class DeclarationVO
    {
        public int DeclarationId { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public DeclarationStatus Status { get; set; }

        public DateTime? ActivationDate { get; set; }

        public string Name
        {
            get
            {
                return SystemLocalization.GetDisplayName(this.NameBg, this.NameEn);
            }
        }

        [JsonIgnore]
        public string NameBg { get; set; }

        [JsonIgnore]
        public string NameEn { get; set; }

        public string Creator { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
