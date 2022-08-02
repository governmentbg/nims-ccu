using R_10000;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Documents.Contracts
{
    [Serializable]
    public class ContractCentralProcurement
    {
        public ContractPrivateNomenclature centralProcurement { get; set; }

        public DateTime? announcedDate { get; set; }

        public string pPANumber { get; set; }

        public ContractCentralProcurementPlan procurementPlan { get; set; }

        public IList<ContractCentralDifferentiatedPosition> differentiatedPositions { get; set; }

        public IList<ContractCentralProcurementDocument> procurementDocuments { get; set; }
    }

    [Serializable]
    public class ContractCentralProcurementPlan
    {
        public ContractPublicNomenclature errandArea { get; set; }

        public ContractPrivateNomenclature errandLegalAct { get; set; }

        public ContractPublicNomenclature errandType { get; set; }

        public string name { get; set; }

        public string description { get; set; }
    }

    [Serializable]
    public class ContractCentralDifferentiatedPosition
    {
        public ContractPrivateNomenclature differentiatedPosition { get; set; }

        public string name { get; set; }

        public string comment { get; set; }
    }

    [Serializable]
    public class ContractCentralProcurementDocument
    {
        public ContractPrivateNomenclature procurementDocument { get; set; }

        public string name { get; set; }

        public string description { get; set; }

        public Guid? blobKey { get; set; }
    }
}