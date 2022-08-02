using System;
using System.Collections.Generic;
using Eumis.Common.Json;
using Newtonsoft.Json;

namespace Eumis.Domain.Debts.ViewObjects
{
    public class ContractDebtPaymentCertReportVO
    {
        public int PaymentOrderNumber { get; set; }

        public int CertReportOrderNumber { get; set; }
    }
}
