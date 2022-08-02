using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Eumis.Authentication.TokenProviders;
using Eumis.Data.Monitorstat.Contracts;

namespace Eumis.ApplicationServices.Communicators
{
    public interface IMonitorstatRestApiCommunicator
    {
        IDictionary<string, string> GetSurveys(int year);

        IDictionary<string, string> GetReports(int year, string surveyCode);

        Guid CreateOperationalProgramme(ProgrammeDO programme);

        Guid CreateProgrammePriority(ProgrammePriorityDO programmePriority);

        Guid CreateProcedure(ProcedureDO procedure);

        Guid CreateProcedureInquiryRequest(ProcedureInquiryDO procedure);

        Guid CreateSubjectRequest(SubjectRequestDO subjectRequestDO);
    }
}
