using Eumis.Common.Localization;
using Newtonsoft.Json;

namespace Eumis.Domain.Procedures.DataObjects
{
    public class ProcedureDeclarationDO
    {
        public int ProcedureDeclarationId { get; set; }

        public int? ProgrammeDeclarationId { get; set; }

        public int ProcedureId { get; set; }

        public int ProgrammeId { get; set; }

        public int OrderNum { get; set; }

        public string Content
        {
            get
            {
                if (string.IsNullOrWhiteSpace(this.ContentEn))
                {
                    return this.ContentBg;
                }

                return SystemLocalization.GetDisplayName(this.ContentBg, this.ContentEn);
            }
        }

        [JsonIgnore]
        public string ContentBg { get; set; }

        [JsonIgnore]
        public string ContentEn { get; set; }

        public ActiveStatus Status { get; set; }

        public bool? IsRequired { get; set; }

        public byte[] Version { get; set; }
    }
}
