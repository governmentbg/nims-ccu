using Eumis.Data.Core.Nomenclatures;

namespace Eumis.Data.ContractReports.ViewObjects
{
    public class ContractReportMicrosSettlementNomVO : EntityNomVO
    {
        public int? DistrictId { get; set; }

        public int MunicipalityId { get; set; }
    }
}
