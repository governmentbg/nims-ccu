using System;

namespace Eumis.Domain.Procedures.Json
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
            this.TitleAlt = specField.TitleAlt;
            this.Description = specField.Description;
            this.DescriptionAlt = specField.DescriptionAlt;
            this.IsRequired = specField.IsRequired;
            this.IsActive = specField.IsActive;
            this.MaxLength = (int)specField.MaxLength;
        }

        public int SpecFieldId { get; set; }

        public Guid Gid { get; set; }

        public string Title { get; set; }

        public string TitleAlt { get; set; }

        public string Description { get; set; }

        public string DescriptionAlt { get; set; }

        public bool IsRequired { get; set; }

        public bool IsActive { get; set; }

        public int MaxLength { get; set; }
    }
}
