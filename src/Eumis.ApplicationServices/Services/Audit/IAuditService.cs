using Eumis.Domain.Audits;

namespace Eumis.ApplicationServices.Services.Audit
{
    public interface IAuditService
    {
        bool CanCreate(int userId, int programmeId, int? contractId, AuditLevel level);

        void CreateItems(Domain.Audits.Audit audit, int userId, int[] itemIds);
    }
}
