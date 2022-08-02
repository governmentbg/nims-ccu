using System.Collections.Generic;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Projects;

namespace Eumis.ApplicationServices.Services.Irregularity
{
    public interface IIrregularitySignalService
    {
        IList<string> CanCreateSignal(int userId, Domain.Contracts.Contract contract, Project project);

        IList<string> CanUpdatePartial(int programmeId, int signalId, string signalNumber);

        IList<string> CanActivate(int programmeId, int signalId, string signalNumber);

        IList<string> CanSetStatusToRemoved(int signalId);
    }
}
