using Eumis.Common.Db;
using Eumis.Data.ContractRegistrations.ViewObjects;
using Eumis.Data.Linq;
using Eumis.Domain.Contracts;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Eumis.Data.ContractRegistrations.Repositories
{
    internal class ContractRegistrationsRepository : AggregateRepository<ContractRegistration>, IContractRegistrationsRepository
    {
        public ContractRegistrationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public IList<ContractRegistrationsVO> GetContractRegistrations(string email, string uin, string firstName, string lastName, string phone, int? contractId)
        {
            var predicate = PredicateBuilder.True<ContractRegistration>()
                .AndStringMatches(c => c.Email, email, false)
                .AndStringMatches(c => c.Uin, uin, false)
                .AndStringMatches(c => c.FirstName, firstName, false)
                .AndStringMatches(c => c.LastName, lastName, false)
                .AndStringMatches(c => c.Phone, phone, false);

            var query = from cr in this.unitOfWork.DbContext.Set<ContractRegistration>().Where(predicate)
                        select cr;

            if (contractId.HasValue)
            {
                query = from cr in query
                        join ccr in this.unitOfWork.DbContext.Set<ContractsContractRegistration>() on new { ContractId = contractId.Value, cr.ContractRegistrationId } equals new { ccr.ContractId, ccr.ContractRegistrationId } into g1
                        from ccr in g1.DefaultIfEmpty()
                        where ccr == null
                        select cr;
            }

            var resultsQuery = from cr in query
                               join ccr in this.unitOfWork.DbContext.Set<ContractsContractRegistration>() on cr.ContractRegistrationId equals ccr.ContractRegistrationId into g2
                               from ccr in g2.DefaultIfEmpty()

                               join c in this.unitOfWork.DbContext.Set<Contract>() on ccr.ContractId equals c.ContractId into g3
                               from c in g3.DefaultIfEmpty()
                               group new { c.RegNumber, c.ContractId }
                               by new
                               {
                                   cr.ContractRegistrationId,
                                   cr.Email,
                                   cr.UinType,
                                   cr.Uin,
                                   cr.FirstName,
                                   cr.LastName,
                                   cr.Phone,
                               }
                                into g
                               select new
                               {
                                   g.Key.ContractRegistrationId,
                                   g.Key.Email,
                                   g.Key.UinType,
                                   g.Key.Uin,
                                   g.Key.FirstName,
                                   g.Key.LastName,
                                   g.Key.Phone,
                                   Contracts = g.ToList(),
                               };

            return (from cr in resultsQuery
                    select new ContractRegistrationsVO
                    {
                        ContractRegistrationId = cr.ContractRegistrationId,
                        Email = cr.Email,
                        UinType = cr.UinType,
                        Uin = cr.Uin,
                        FirstName = cr.FirstName,
                        LastName = cr.LastName,
                        Phone = cr.Phone,
                        Contracts = cr.Contracts.Select(c => new ContractRegistrationContractVO()
                        {
                            ContractId = c.ContractId,
                            RegNumber = c.RegNumber,
                        })
                        .ToList(),
                    })
                    .ToList();
        }

        public bool IsUnique(string email)
        {
            return !this.unitOfWork.DbContext.Set<ContractRegistration>().Where(e => e.Email == email).Any();
        }

        public bool ActivationCodeExists(string activationCode)
        {
            return this.unitOfWork.DbContext.Set<ContractRegistration>().Any(r => r.ActivationCode == activationCode);
        }

        public ContractRegistration FindByActivationCode(string activationCode)
        {
            return this.Set()
                .Where(r => r.ActivationCode == activationCode)
                .SingleOrDefault();
        }

        public ContractRegistration FindByPasswordRecoveryCode(string passwordRecoveryCode)
        {
            return this.Set()
                .Where(r => r.PasswordRecoveryCode == passwordRecoveryCode)
                .SingleOrDefault();
        }

        public ContractRegistration FindByEmail(string email)
        {
            return this.Set()
                .Where(r => r.Email == email)
                .SingleOrDefault();
        }

        public bool PasswordRecoveryCodeExists(string passwordRecoveryCode)
        {
            return this.unitOfWork.DbContext.Set<ContractRegistration>().Any(r => r.PasswordRecoveryCode == passwordRecoveryCode);
        }

        public string GetRegistrationEmail(int contractRegistrationId)
        {
            return (from cr in this.unitOfWork.DbContext.Set<ContractRegistration>()
                    where cr.ContractRegistrationId == contractRegistrationId
                    select cr.Email).Single();
        }

        public Task<ContractRegistration> FindByEmailAsync(string email)
        {
            return this.Set()
                .Where(r => r.Email == email)
                .SingleOrDefaultAsync();
        }

        public int GetContractId(int contractRegistrationId)
        {
            return (from ccr in this.unitOfWork.DbContext.Set<ContractsContractRegistration>()
                    where ccr.ContractRegistrationId == contractRegistrationId
                    select ccr.ContractId).First();
        }
    }
}
