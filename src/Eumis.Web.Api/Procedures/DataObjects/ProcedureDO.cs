using System;
using Eumis.Common.Json;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureDO
    {
        public ProcedureDO()
        {
        }

        public ProcedureDO(Procedure procedure)
        {
            this.ProcedureId = procedure.ProcedureId;
            this.ProcedureStatus = procedure.ProcedureStatus;
            this.ApplicationFormType = procedure.ApplicationFormType;
            this.Year = procedure.Year;
            this.Code = procedure.Code;
            this.Name = procedure.Name;
            this.NameAlt = procedure.NameAlt;
            this.Description = procedure.Description;
            this.DescriptionAlt = procedure.DescriptionAlt;
            this.AllowedRegistrationType = procedure.AllowedRegistrationType;
            this.ProjectMinAmount = procedure.ProjectMinAmount;
            this.ProjectMinAmountInfo = procedure.ProjectMinAmountInfo;
            this.ProjectMinAmountInfoAlt = procedure.ProjectMinAmountInfoAlt;
            this.ProjectMaxAmount = procedure.ProjectMaxAmount;
            this.ProjectMaxAmountInfo = procedure.ProjectMaxAmountInfo;
            this.ProjectMaxAmountInfoAlt = procedure.ProjectMaxAmountInfoAlt;
            this.ProjectDuration = procedure.ProjectDuration;
            this.ActivationDate = procedure.ActivationDate;
            this.Version = procedure.Version;
            this.ProcedureKind = procedure.ProcedureKind;
        }

        public int? ProcedureId { get; set; }

        public ProcedureStatus? ProcedureStatus { get; set; }

        public ProcedureKind ProcedureKind { get; set; }

        public ApplicationFormType? ApplicationFormType { get; set; }

        public int? Year { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public AllowedRegistrationType? AllowedRegistrationType { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ProjectMinAmount { get; set; }

        public string ProjectMinAmountInfo { get; set; }

        public string ProjectMinAmountInfoAlt { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? ProjectMaxAmount { get; set; }

        public string ProjectMaxAmountInfo { get; set; }

        public string ProjectMaxAmountInfoAlt { get; set; }

        public int? ProjectDuration { get; set; }

        public DateTime? ActivationDate { get; set; }

        public byte[] Version { get; set; }
    }
}
