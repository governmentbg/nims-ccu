namespace Eumis.Authentication.Authorization.ClaimsContexts.Audit
{
    internal delegate IAuditClaimsContext AuditClaimsContextFactory(int auditId);

    internal interface IAuditClaimsContext
    {
        int AuditId { get; }

        int ProgrammeId { get; }

        int? ContractId { get; }
    }
}
