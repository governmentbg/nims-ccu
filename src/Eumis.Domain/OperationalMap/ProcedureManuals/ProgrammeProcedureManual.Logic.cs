using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.Programmes;
using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.ProcedureManuals
{
    public partial class ProgrammeProcedureManual
    {
        public void SetAttributes(string name, string description, Guid blobKey)
        {
            this.Name = name;
            this.Description = description;
            this.BlobKey = blobKey;
        }

        public void ChangeStatus(ProgrammeProcedureManualStatus status, int? userId = null)
        {
            if (status == ProgrammeProcedureManualStatus.Actual)
            {
                this.ActivatedByUserId = userId;
                this.ActivationDate = DateTime.Now;
            }

            this.Status = status;
        }
    }
}
