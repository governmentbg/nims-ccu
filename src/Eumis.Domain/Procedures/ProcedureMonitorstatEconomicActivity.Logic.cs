namespace Eumis.Domain.Procedures
{
    public partial class ProcedureMonitorstatEconomicActivity
    {
        public void AssertIsDraft()
        {
            if (this.Status != ProcedureMonitorstatEconomicActivityStatus.Draft)
            {
                throw new DomainValidationException("ProcedureMonitorstatEconomicActivity status must be draft!");
            }
        }
    }
}
