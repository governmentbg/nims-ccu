using System;
using System.Collections.Generic;
using Eumis.Domain.Emails;

namespace Eumis.Data.Emails.Repositories
{
    public interface IEmailsRepository : IAggregateRepository<Email>
    {
        IList<int> GetPendingEmailIds(int limit, int maxFailedAttempts, TimeSpan failedAttemptTimeout);

        IList<int> GetTodayEmailForProjectCommunications();
    }
}
