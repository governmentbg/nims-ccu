using Eumis.Public.Data.Companies.ViewObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Data.Companies.Repositories
{
    public interface ICompaniesRepository
    {
        CompanyProjectsVO GetBeneficaryProjects(string uin);

        CompanyProjectsVO GetContractorProjects(string uin);

        CompanyProjectsVO GetPartnerProjects(string uin);

        CompanyProjectsVO GetSubcontractorProjects(string uin);

        CompanyProjectsVO GetMemberProjects(string uin);
    }
}
