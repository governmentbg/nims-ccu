using Eumis.Domain.NonAggregates;
using Eumis.Domain.Sebra;
using System;

namespace Eumis.ApplicationServices.Services.Sebra
{
    public interface ISebraService
    {
        f GetSebraReport(
            int programmeId,
            int procedureId,
            DateTime fromDate,
            DateTime toDate,
            int fromNumber,
            int toNumber,
            string sendername,
            string acc,
            string o1,
            SebraPaymentType? type = null);

        (f xml, string procedureCode) GetSebraReportByFile(
            Guid blobKey,
            string sendername,
            string acc,
            string o1,
            SebraPaymentType? type = null);
    }
}
