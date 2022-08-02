using Eumis.Common.Localization;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Procedures.PortalViewObjects
{
    public class ProcedureTreePVO
    {
        public int Number { get; set; }

        public Guid Gid { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public ProcedureStatus Status { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterBg))]
        public ProcedureStatus StatusText { get; set; }

        [JsonConverter(typeof(SpecificEnumDescriptionConverterEn))]
        public ProcedureStatus StatusTextAlt
        {
            get
            {
                return this.StatusText;
            }
        }

        public bool IsIntroducedByLAG { get; set; }
    }
}
