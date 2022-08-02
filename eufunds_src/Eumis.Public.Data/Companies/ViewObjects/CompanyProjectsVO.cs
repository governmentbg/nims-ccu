using Eumis.Public.Data.UmisVOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Eumis.Public.Data.Companies.ViewObjects
{
    public class CompanyProjectsVO
    {
        public IList<ContractVO> BeneficaryProjects { get; set; }

        public int? BeneficaryContractsCount { get; set; }

        public IList<ProjectDetailsVO> ContractorProjects { get; set; }

        public int? ContractorContractsCount { get; set; }

        public IList<PartnerProjectsVO> PartnerProjects { get; set; }

        public int? PartnerContractsCount { get; set; }

        public IList<ProjectDetailsVO> SubcontractorProjects { get; set; }

        public int? SubcontractorContractsCount { get; set; }

        public IList<ProjectDetailsVO> MemberProjects { get; set; }

        public int? MemberContractsCount { get; set; }
    }
}
