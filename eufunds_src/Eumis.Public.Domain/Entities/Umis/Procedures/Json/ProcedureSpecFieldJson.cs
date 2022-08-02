using System;

namespace Eumis.Public.Domain.Entities.Umis.Procedures.Json
{
    public class ProcedureSpecFieldJson
    {
        public ProcedureSpecFieldJson()
        {
        }

        public ProcedureSpecFieldJson(ProcedureSpecField specField)
        {
            this.SpecFieldId = specField.ProcedureSpecFieldId;
            this.Gid = specField.Gid;
            this.Title = specField.Title;
            this.Description = specField.Description;
            this.IsRequired = specField.IsRequired;
            this.IsActive = specField.IsActive;
            this.MaxLength = (int)specField.MaxLength;
        }

        public int SpecFieldId { get; set; }

        public Guid Gid { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }

        public int MaxLength { get; set; }
    }
}
