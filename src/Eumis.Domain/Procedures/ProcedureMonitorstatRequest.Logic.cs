using Eumis.Domain.Monitorstat;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureMonitorstatRequest : IAggregateRoot
    {
        public void UpdateAttributes(DateTime? executionStartDate, DateTime? executionEndDate)
        {
            this.AssertIsDraft();

            this.Name = string.Empty;
            this.ExecutionEndDate = executionEndDate;
            this.ExecutionStartDate = executionStartDate;

            if (executionStartDate.HasValue)
            {
                this.Name = executionStartDate.Value.ToShortDateString() + " - ";
            }

            if (executionEndDate.HasValue)
            {
                if (string.IsNullOrEmpty(this.Name))
                {
                    this.Name = " - " + executionEndDate.Value.ToShortDateString();
                }
                else
                {
                    this.Name += executionEndDate.Value.ToShortDateString();
                }
            }

            this.ModifyDate = DateTime.Now;
        }

        public void AssertIsDraft()
        {
            if (this.Status != ProcedureMonitorstatRequestStatus.Draft)
            {
                throw new DomainValidationException("Cannot edit procedure monitorstat request that is not in Draft status");
            }
        }
    }
}
