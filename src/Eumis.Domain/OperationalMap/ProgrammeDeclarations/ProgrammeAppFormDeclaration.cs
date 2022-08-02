using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.OperationalMap.ProgrammeDeclarations
{
    public class ProgrammeAppFormDeclaration : ProgrammeDeclaration
    {
        public ProgrammeAppFormDeclaration()
        {
        }

        public ProgrammeAppFormDeclaration(
            int programmeId,
            string name,
            string nameAlt,
            string content,
            string contentAlt,
            FieldType fieldType,
            int orderNum,
            bool isConsentForNSIDataProviding)
            : base(programmeId, name, nameAlt, content, contentAlt, fieldType, orderNum, isConsentForNSIDataProviding)
        {
        }

        public override ProgrammeDeclarationType Type
        {
            get
            {
                return ProgrammeDeclarationType.AppForm;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProgrammeAppFormDeclarationMap : EntityTypeConfiguration<ProgrammeAppFormDeclaration>
    {
        public ProgrammeAppFormDeclarationMap()
        {
        }
    }
}
