using Eumis.Domain.Core;
using Eumis.Domain.MonitoringFinancialControl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Web.Api.ActuallyPaidAmounts.DataObjects
{
    public class ActuallyPaidAmountDocumentDO
    {
        public ActuallyPaidAmountDocumentDO()
        {
        }

        public ActuallyPaidAmountDocumentDO(int actuallyPaidAmountId, byte[] version)
        {
            this.ActuallyPaidAmountId = actuallyPaidAmountId;
            this.Version = version;
        }

        public ActuallyPaidAmountDocumentDO(ActuallyPaidAmountDocument paidAmountDocument, byte[] version)
        {
            this.ActuallyPaidAmountDocumentId = paidAmountDocument.ActuallyPaidAmountDocumentId;
            this.ActuallyPaidAmountId = paidAmountDocument.ActuallyPaidAmountId;
            this.Name = paidAmountDocument.Name;
            this.Description = paidAmountDocument.Description;

            if (paidAmountDocument.File != null)
            {
                this.File = new FileDO
                {
                    Key = paidAmountDocument.File.Key,
                    Name = paidAmountDocument.File.FileName,
                };
            }

            this.Version = version;
        }

        public int ActuallyPaidAmountDocumentId { get; set; }

        public int ActuallyPaidAmountId { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public FileDO File { get; set; }

        public byte[] Version { get; set; }
    }
}
