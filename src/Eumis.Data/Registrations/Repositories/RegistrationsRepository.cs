using Eumis.Common.Db;
using Eumis.Data.Registrations.ViewObjects;
using Eumis.Domain.Registrations;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;

namespace Eumis.Data.Registrations.Repositories
{
    internal class RegistrationsRepository : AggregateRepository<Registration>, IRegistrationsRepository
    {
        public RegistrationsRepository(IUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public Task<Registration> FindByEmailAsync(string email)
        {
            return this.Set()
                .Where(r => r.Email == email)
                .SingleOrDefaultAsync();
        }

        public Registration FindByActivationCode(string activationCode)
        {
            return this.Set()
                .Where(r => r.ActivationCode == activationCode)
                .SingleOrDefault();
        }

        public Registration FindByPasswordRecoveryCode(string passwordRecoveryCode)
        {
            return this.Set()
                .Where(r => r.PasswordRecoveryCode == passwordRecoveryCode)
                .SingleOrDefault();
        }

        public Registration FindByEmail(string email)
        {
            return this.Set()
                .Where(r => r.Email == email)
                .SingleOrDefault();
        }

        public IList<RegistrationsVO> GetRegistrations()
        {
            return (from r in this.unitOfWork.DbContext.Set<Registration>()
                    select new RegistrationsVO
                    {
                        RegistrationId = r.RegistrationId,
                        Email = r.Email,
                        FirstName = r.FirstName,
                        LastName = r.LastName,
                        Phone = r.Phone,
                    })
                    .ToList();
        }

        public string GetRegistrationEmailForProject(int projectId)
        {
            return (from rp in this.unitOfWork.DbContext.Set<RegProjectXml>()
                    join r in this.unitOfWork.DbContext.Set<Registration>() on rp.RegistrationId equals r.RegistrationId
                    where rp.ProjectId == projectId
                    select r.Email).SingleOrDefault();
        }

        public IList<Tuple<int, string>> GetRegistrationEmailsForProjects(int[] projectIds)
        {
            return (from rp in this.unitOfWork.DbContext.Set<RegProjectXml>()
                    join r in this.unitOfWork.DbContext.Set<Registration>() on rp.RegistrationId equals r.RegistrationId
                    where rp.ProjectId.HasValue && projectIds.Contains(rp.ProjectId.Value)
                    select new { ProjectId = rp.ProjectId.Value, Email = r.Email })
                .ToList()
                .Select(t => new Tuple<int, string>(t.ProjectId, t.Email))
                .ToList();
        }

        public bool RegistrationExists(string email)
        {
            return this.unitOfWork.DbContext.Set<Registration>().Any(r => r.Email == email);
        }

        public bool ActivationCodeExists(string activationCode)
        {
            return this.unitOfWork.DbContext.Set<Registration>().Any(r => r.ActivationCode == activationCode);
        }

        public bool PasswordRecoveryCodeExists(string passwordRecoveryCode)
        {
            return this.unitOfWork.DbContext.Set<Registration>().Any(r => r.PasswordRecoveryCode == passwordRecoveryCode);
        }

        public string GetEmail(int registrationId)
        {
            return this.FindWithoutIncludes(registrationId).Email;
        }

        public string GetEmailByProject(int projectId)
        {
            return (from rpx in this.unitOfWork.DbContext.Set<RegProjectXml>().Where(x => x.ProjectId == projectId)
                    join r in this.Set() on rpx.RegistrationId equals r.RegistrationId
                    select r.Email)
                    .FirstOrDefault();
        }
    }
}
