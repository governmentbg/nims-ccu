using Eumis.Common.Json;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System;

namespace Eumis.Data.Procedures.ViewObjects
{
    public class ProcedureInfoVO
    {
        public string Name { get; set; }

        public byte[] Version { get; set; }

        [JsonConverter(typeof(EnumDescriptionConverter))]
        public ProcedureStatus StatusName { get; set; }

        public ProcedureStatus Status { get; set; }

        public DateTime? ActivationDate { get; set; }

        public ProcedureContractReportDocumentsSectionStatus ProcedureContractReportDocumentsSectionStatus { get; set; }

        public ApplicationFormType ApplicationFormType { get; set; }

        public ProcedureKind ProcedureKind { get; set; }

        public bool IsIndicatorVisible { get; set; }

        public bool IsTimeLimitsVisible { get; set; }

        public bool IsAdditionalInformationVisible { get; set; }

        public bool IsAttachedDocumentsVisible { get; set; }
    }
}
