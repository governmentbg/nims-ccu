using System;
namespace Eumis.Documents.Contracts
{
    public class ContractInitializerOffer : ContractInitializer
    {
        public string contractVersionXml { get; set; }
        public string contractProcurementXml { get; set; }

        public string contractGid { get; set; }
        public string procurementsGid { get; set; }
        public string planGid { get; set; }
        public string positionGid { get; set; }
    }
}