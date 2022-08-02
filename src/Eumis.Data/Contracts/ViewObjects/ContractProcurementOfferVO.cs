using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Data.Contracts.ViewObjects
{
    public class ContractProcurementOfferVO
    {
        public Guid OfferGid { get; set; }

        public DateTime OfferSubmitDate { get; set; }

        public Guid ProcurementPlanGid { get; set; }

        public Guid DifferentiatedPositionGid { get; set; }
    }
}
