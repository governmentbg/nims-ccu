using Eumis.Common.Json;
using Eumis.Common.Localization;
using Eumis.Domain;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Users.ViewObjects
{
    public class UserDeclarationVO
    {
        public int? DeclarationId { get; set; }

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

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public DeclarationStatus Status { get; set; }

        public DateTime? ActivationDate { get; set; }

        public bool IsAccepted { get; set; }

        public DateTime? AcceptDate { get; set; }
    }
}
