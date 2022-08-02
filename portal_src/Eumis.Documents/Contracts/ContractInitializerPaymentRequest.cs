using System;
namespace Eumis.Documents.Contracts
{
    public class ContractInitializerPaymentRequest : ContractInitializerReport
    {
        public ContractEnumNomenclature type { get; set; }
    }
}