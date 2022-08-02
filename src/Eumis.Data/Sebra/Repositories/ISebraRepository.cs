using Eumis.Data.Core;
using Eumis.Data.Sebra.ViewObjects;
using System;
using System.Collections.Generic;

namespace Eumis.Data.Sebra.Repositories
{
    public interface ISebraRepository : IRepository
    {
        string GetProcedureCode(int procedureId);

        SebraCompanyVO GetProgrammeCompany(int programmeId);

        List<SebraProjectVO> GetProjects(int[] projectIds);

        List<SebraProjectVO> GetProjects(int procedureId, DateTime fromDate, DateTime toDate, int fromNumber, int toNumber);

        SebraProjectInfoVO GetProjectsInfo(int[] projectIds);

        IList<SebraProjectIbanVO> GetProjectIbans(int[] projectIds);
    }
}
