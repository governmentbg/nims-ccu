using Eumis.Domain.EvalSessions;

namespace Eumis.Web.Api.EvalSessions.DataObjects
{
    public class EvalSessionReportProjectPartnerDO
    {
        public EvalSessionReportProjectPartnerDO(EvalSessionReportProjectPartner partner)
        {
            this.PartnerUin = partner.PartnerUin;
            this.PartnerName = partner.PartnerName;
            this.PartnerLegalType = partner.PartnerLegalType.Name;
            this.PartnerAddress = partner.PartnerAddress;
            this.PartnerRepresentative = partner.PartnerRepresentative;
            this.PartnerFinancialContribution = partner.PartnerFinancialContribution;
        }

        public string PartnerUin { get; set; }

        public string PartnerName { get; set; }

        public string PartnerLegalType { get; set; }

        public string PartnerAddress { get; set; }

        public string PartnerRepresentative { get; set; }

        public decimal? PartnerFinancialContribution { get; set; }
    }
}
