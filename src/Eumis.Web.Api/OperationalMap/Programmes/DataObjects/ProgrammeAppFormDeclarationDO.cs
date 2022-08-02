using Eumis.Domain.OperationalMap.ProgrammeDeclarations;

namespace Eumis.Web.Api.OperationalMap.Programmes.DataObjects
{
    public class ProgrammeAppFormDeclarationDO
    {
        public ProgrammeAppFormDeclarationDO()
        {
        }

        public ProgrammeAppFormDeclarationDO(int programmeId, int orderNum)
        {
            this.ProgrammeId = programmeId;
            this.OrderNum = orderNum;
            this.FieldType = FieldType.CheckBox;
            this.IsConsentForNSIDataProviding = false;
            this.IsActive = true;
        }

        public ProgrammeAppFormDeclarationDO(ProgrammeAppFormDeclaration programmeDeclaration, bool isReadonly)
        {
            this.ProgrammeDeclarationId = programmeDeclaration.ProgrammeDeclarationId;
            this.ProgrammeId = programmeDeclaration.ProgrammeId;
            this.OrderNum = programmeDeclaration.OrderNum;
            this.Name = programmeDeclaration.Name;
            this.NameAlt = programmeDeclaration.NameAlt;
            this.Content = programmeDeclaration.Content;
            this.ContentAlt = programmeDeclaration.ContentAlt;
            this.IsActive = programmeDeclaration.IsActive;
            this.IsReadonly = isReadonly;
            this.FieldType = programmeDeclaration.FieldType;
            this.IsConsentForNSIDataProviding = programmeDeclaration.IsConsentForNSIDataProviding;
            this.Version = programmeDeclaration.Version;
        }

        public int ProgrammeDeclarationId { get; set; }

        public int ProgrammeId { get; set; }

        public int OrderNum { get; set; }

        public string Name { get; set; }

        public string NameAlt { get; set; }

        public string Content { get; set; }

        public string ContentAlt { get; set; }

        public bool IsActive { get; set; }

        public bool IsReadonly { get; set; }

        public FieldType FieldType { get; set; }

        public bool IsConsentForNSIDataProviding { get; set; }

        public byte[] Version { get; set; }
    }
}
