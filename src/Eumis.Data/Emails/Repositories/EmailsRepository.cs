using Eumis.Common;
using Eumis.Common.Db;
using Eumis.Common.Email;
using Eumis.Domain.Emails;
using Eumis.Domain.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Data.Emails.Repositories
{
    internal class EmailsRepository : AggregateRepository<Email>, IEmailsRepository
    {
        public EmailsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<int> GetPendingEmailIds(int limit, int maxFailedAttempts, TimeSpan failedAttemptTimeout)
        {
            var maxInterval = DateTime.Now - failedAttemptTimeout;

            return this.unitOfWork.DbContext.Set<Email>()
                .Where(e => e.Status == EmailStatus.Pending && (e.FailedAttempts == 0 || (e.FailedAttempts < maxFailedAttempts && e.ModifyDate < maxInterval)))
                .OrderBy(e => e.CreateDate)
                .Select(e => e.EmailId)
                .Take(limit)
                .ToList();
        }

        public IList<int> GetTodayEmailForProjectCommunications()
        {
            List<System.Data.SqlClient.SqlParameter> selectSqlParams = new List<System.Data.SqlClient.SqlParameter>();

            selectSqlParams.Add(new System.Data.SqlClient.SqlParameter("@fromDate", DateTime.Now.ToStartOfDay()));
            selectSqlParams.Add(new System.Data.SqlClient.SqlParameter("@toDate", DateTime.Now.ToEndOfDay()));

            string query = $@" 
                    SELECT ProjectCommunicationId 
                    FROM Emails
                    CROSS APPLY OPENJSON(Context) WITH
                    (
                        ProjectCommunicationId INT '$.ProjectCommunicationId'
                    )
                    WHERE MailTemplateName = '{EmailTemplate.ProjectQuestionExpireMessage}' AND CreateDate > @fromDate AND CreateDate < @toDate";

            var result = this.SqlQuery<int>(query, selectSqlParams).ToList();

            return result;
        }
    }
}
