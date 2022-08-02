using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Mappers
{
    [Serializable]
    public enum NomenclatureType
    {
        CompanyLegalType = 1,
        CompanyType = 2,
        CompanySizeType = 3,

        InterventionField = 5,
        FormOfFinance = 6,
        EconomicDimension = 7,
        ESFSecondaryTheme = 8,
        TerritorialDeliveryMechanism = 9,
        TerritorialDimension = 10,
        ThematicObjective = 11,

        ErrandLegalAct = 12,

        AttachedDocumentType = 13,

        ProgrammePriority = 14,
        InvestmentPriority = 15,
        FinanceSource = 16,
        SpecificTarget = 17,
        ContractReportDocumentType = 18,

        Dummy = 999
    }
}
