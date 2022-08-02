using Eumis.Common.Json;
using Eumis.Domain.Indicators;
using Eumis.Domain.Indicators.DataObjects;
using Eumis.Domain.Procedures;
using Newtonsoft.Json;
using System.Linq;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureIndicatorDO
    {
        public ProcedureIndicatorDO()
        {
            this.Status = ActiveStatus.NotActivated;
            this.Indicator = new IndicatorDO();
        }

        public ProcedureIndicatorDO(int procedureId, byte[] version)
            : this()
        {
            this.ProcedureId = procedureId;
            this.Version = version;
        }

        public ProcedureIndicatorDO(Procedure procedure, byte[] version)
            : this()
        {
            if (procedure.ProcedureProgrammes.Count == 1)
            {
                this.Indicator.ProgrammeId = procedure.ProcedureProgrammes.Single().ProgrammeId;
                this.Indicator.SourceMapNodeId = procedure.ProcedureProgrammes.Single().ProgrammeId;
            }

            this.ProcedureId = procedure.ProcedureId;
            this.Version = version;
        }

        public ProcedureIndicatorDO(Indicator indicator, ProcedureIndicator procedureIndicator, byte[] version)
        {
            this.Indicator = new IndicatorDO(indicator, procedureIndicator.SourceMapNodeId);
            this.ProcedureId = procedureIndicator.ProcedureId;
            this.BaseTotalValue = procedureIndicator.BaseTotalValue;
            this.BaseMenValue = procedureIndicator.BaseMenValue;
            this.BaseWomenValue = procedureIndicator.BaseWomenValue;
            this.BaseYear = procedureIndicator.BaseYear;
            this.TargetTotalValue = procedureIndicator.TargetTotalValue;
            this.TargetMenValue = procedureIndicator.TargetMenValue;
            this.TargetWomenValue = procedureIndicator.TargetWomenValue;
            this.MilestoneTargetTotalValue = procedureIndicator.MilestoneTargetTotalValue;
            this.MilestoneTargetMenValue = procedureIndicator.MilestoneTargetMenValue;
            this.MilestoneTargetWomenValue = procedureIndicator.MilestoneTargetWomenValue;
            this.DataSource = procedureIndicator.DataSource;
            this.Description = procedureIndicator.Description;
            this.IsActive = procedureIndicator.IsActive;
            this.IsActivated = procedureIndicator.IsActivated;
            this.Status = !procedureIndicator.IsActivated ?
                ActiveStatus.NotActivated :
                procedureIndicator.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive;
            this.Version = version;
        }

        public IndicatorDO Indicator { get; set; }

        public int ProcedureId { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BaseTotalValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BaseMenValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? BaseWomenValue { get; set; }

        public int? BaseYear { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TargetTotalValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TargetMenValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? TargetWomenValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? MilestoneTargetTotalValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? MilestoneTargetMenValue { get; set; }

        [JsonConverter(typeof(MoneyConverter))]
        public decimal? MilestoneTargetWomenValue { get; set; }

        public string DataSource { get; set; }

        public string Description { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public ActiveStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
