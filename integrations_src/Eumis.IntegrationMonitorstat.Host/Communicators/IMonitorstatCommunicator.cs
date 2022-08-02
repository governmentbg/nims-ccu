using Monitorstat.Common.Contracts;
using Monitorstat.Common.MonitorstatService;
using System;
using System.Collections.Generic;

namespace Eumis.Integration.Monitorstat.Communicators
{
    public interface IMonitorstatCommunicator
    {
        IDictionary<string, string> GetSurveys(int year);

        IDictionary<string, string> GetReports(int year, string surveyCode);

        Guid CreateOperationalProgramme(ProgrammeDO programme);

        Guid CreateProgrammePriority(ProgrammePriorityDO programmePriority);

        Guid CreateProcedure(ProcedureDO procedure);

        Guid CreateProcedureInquiry(ProcedureInquiryDO inquiry);

        Guid CreateSubjectRequest(SubjectRequestDO inquiry);

        Nomenclature[] GetProgrammeStatuses();

        Nomenclature[] GetProgrammeGroups();
    }
}
