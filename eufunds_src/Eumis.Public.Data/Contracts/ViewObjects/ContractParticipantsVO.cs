using Eumis.Public.Data.UmisVOs;
using Eumis.Public.Domain.Entities.Umis.NonAggregates;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Public.Data.Contracts.ViewObjects
{
    public class ContractParticipantsVO : ContractCommonVO
    {
        private readonly Func<ContractPartnerVO, bool> personalPartnerCondition = c => c.UinType == UinType.PersonalBulstat;

        private readonly Func<ContractContractorVO, bool> personalContractorCondition = c => c.UinType == UinType.PersonalBulstat;

        private readonly Func<ContractSubcontractorVO, bool> personalSubcontractorCondition = c => c.UinType == UinType.PersonalBulstat;

        public IEnumerable<ContractPartnerVO> Partners { get; set; }

        public IEnumerable<ContractPartnerVO> PartnersPersonal
        {
            get
            {
                return this.Partners.Where(this.personalPartnerCondition);
            }
        }

        public IEnumerable<ContractPartnerVO> PartnersNonPersonal
        {
            get
            {
                return this.Partners.Where(c => !this.personalPartnerCondition(c));
            }
        }

        public IEnumerable<ContractContractorVO> Contractors { get; set; }

        public IEnumerable<ContractContractorVO> ContractorsPersonal
        {
            get
            {
                return this.Contractors?.Where(this.personalContractorCondition);
            }
        }

        public IEnumerable<ContractContractorVO> ContractorsNonPersonal
        {
            get
            {
                return this.Contractors?.Where(c => !this.personalContractorCondition(c));
            }
        }

        public IEnumerable<ContractSubcontractorVO> Subcontractors { get; set; }

        public IEnumerable<ContractSubcontractorVO> SubcontractorsPersonal
        {
            get
            {
                return this.Subcontractors.Where(this.personalSubcontractorCondition);
            }
        }

        public IEnumerable<ContractSubcontractorVO> SubcontractorsNonPersonal
        {
            get
            {
                return this.Subcontractors.Where(c => !this.personalSubcontractorCondition(c));
            }
        }

        public IEnumerable<ContractSubcontractorVO> Members { get; set; }

        public IEnumerable<ContractSubcontractorVO> MembersPersonal
        {
            get
            {
                return this.Members.Where(this.personalSubcontractorCondition);
            }
        }

        public IEnumerable<ContractSubcontractorVO> MembersNonPersonal
        {
            get
            {
                return this.Members.Where(c => !this.personalSubcontractorCondition(c));
            }
        }
    }
}
