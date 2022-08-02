using Eumis.Domain.Procedures;

namespace Eumis.Web.Api.Procedures.DataObjects
{
    public class ProcedureSpecFieldDO
    {
        public ProcedureSpecFieldDO()
        {
        }

        public ProcedureSpecFieldDO(int procedureId, byte[] version)
        {
            this.ProcedureId = procedureId;
            this.Status = ActiveStatus.NotActivated;
            this.Version = version;
        }

        public ProcedureSpecFieldDO(ProcedureSpecField procedureSpecField, byte[] version)
        {
            this.ProcedureSpecFieldId = procedureSpecField.ProcedureSpecFieldId;
            this.ProcedureId = procedureSpecField.ProcedureId;
            this.Title = procedureSpecField.Title;
            this.TitleAlt = procedureSpecField.TitleAlt;
            this.Description = procedureSpecField.Description;
            this.DescriptionAlt = procedureSpecField.DescriptionAlt;
            this.IsRequired = procedureSpecField.IsRequired;
            this.MaxLength = procedureSpecField.MaxLength;
            this.IsActivated = procedureSpecField.IsActivated;
            this.IsActive = procedureSpecField.IsActive;
            this.Status = !procedureSpecField.IsActivated ?
                ActiveStatus.NotActivated :
                procedureSpecField.IsActive ? ActiveStatus.Active : ActiveStatus.Inactive;
            this.Version = version;
        }

        public int? ProcedureSpecFieldId { get; set; }

        public int ProcedureId { get; set; }

        public string Title { get; set; }

        public string TitleAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public bool? IsRequired { get; set; }

        public ProcedureSpecFieldMaxLength? MaxLength { get; set; }

        public bool IsActivated { get; set; }

        public bool IsActive { get; set; }

        public ActiveStatus Status { get; set; }

        public byte[] Version { get; set; }
    }
}
