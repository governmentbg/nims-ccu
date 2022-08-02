namespace Eumis.Domain.Procedures
{
    public partial class ProcedureVersion
    {
        public void DeactivateVersion()
        {
            this.IsActive = false;
        }
    }
}
