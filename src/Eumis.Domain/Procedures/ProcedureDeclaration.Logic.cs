using System;

namespace Eumis.Domain.Procedures
{
    public abstract partial class ProcedureDeclaration
    {
        public void SetAttributes(int programmeDeclarationId, bool isRequired)
        {
            this.ProgrammeDeclarationId = programmeDeclarationId;
            this.IsRequired = isRequired;

            this.ModifyDate = DateTime.Now;
        }

        public void AssertIsNotActivated()
        {
            if (this.IsActivated)
            {
                throw new DomainValidationException("Cannot delete ProcedureDeclaration that is activated.");
            }
        }

        public void Deactivate()
        {
            if (!this.IsActive)
            {
                throw new DomainValidationException("Cannot deactivate ProcedureDeclaration.");
            }

            this.IsActive = false;
            this.ModifyDate = DateTime.Now;
        }

        public void Activate()
        {
            if (this.IsActive)
            {
                throw new DomainValidationException("Cannot activate ProcedureDeclaration.");
            }

            this.IsActive = true;
            this.ModifyDate = DateTime.Now;
        }
    }
}
