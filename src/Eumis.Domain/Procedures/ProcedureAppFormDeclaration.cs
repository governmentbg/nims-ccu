using System.Data.Entity.ModelConfiguration;

namespace Eumis.Domain.Procedures
{
    public partial class ProcedureAppFormDeclaration : ProcedureDeclaration
    {
        public ProcedureAppFormDeclaration()
        {
        }

        public ProcedureAppFormDeclaration(int procedureId, int programmeDeclarationId, bool isRequired)
            : base(procedureId, programmeDeclarationId, isRequired)
        {
        }

        public override ProcedureDeclarationType Type
        {
            get
            {
                return ProcedureDeclarationType.AppForm;
            }
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("", "SA1402:FileMayOnlyContainASingleType", Justification = "Map classes should be in the same file for simplicity")]
    public class ProcedureAppFormDeclarationMap : EntityTypeConfiguration<ProcedureAppFormDeclaration>
    {
        public ProcedureAppFormDeclarationMap()
        {
        }
    }
}
