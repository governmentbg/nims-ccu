using System;
using Eumis.Data.Core;
using Eumis.Data.Registrations.PortalViewObjects;
using Eumis.Domain.Registrations;

namespace Eumis.Data.Registrations.Repositories
{
    public interface IRegProjectXmlsRepository : IAggregateRepository<RegProjectXml>
    {
        RegProjectXml Find(int registrationId, Guid gid);

        RegProjectXml FindOrDefault(int registrationId, string hash);

        RegProjectXml Find(string hashStartsWith);

        RegProjectXml FindForUpdate(int registrationId, Guid gid, byte[] version);

        PagePVO<RegProjectXmlPVO> GetAllForRegistration(int registrationId, RegProjectXmlStatus status, int offset = 0, int? limit = null);

        PagePVO<RegProjectXmlPVO> GetAllProjectCommunicationsForRegistration(int registrationId, int offset = 0, int? limit = null);

        string[] SubmittedHashesStartingWith(string startsWith, int[] programmeIds);

        int? GetProjectId(Guid registeredGid);
    }
}
