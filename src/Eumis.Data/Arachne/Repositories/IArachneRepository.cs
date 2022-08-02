using Eumis.Data.Core;
using Eumis.Domain.Arachne;

namespace Eumis.Data.Arachne.Repositories
{
    public interface IArachneRepository : IRepository
    {
        ECDataExchangeXmlFormat GetArachneReport(int programmeId);
    }
}
