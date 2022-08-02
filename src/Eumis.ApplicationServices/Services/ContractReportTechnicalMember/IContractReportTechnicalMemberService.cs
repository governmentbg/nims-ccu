using Eumis.Domain.Contracts;

namespace Eumis.ApplicationServices.Services.ContractReportTechnicalMember
{
    public interface IContractReportTechnicalMemberService
    {
        void CreateContractReportTechnicalMembers(ContractReportTechnical technical);

        void DeleteContractReportTechnicalMembers(ContractReportTechnical technical);
    }
}
