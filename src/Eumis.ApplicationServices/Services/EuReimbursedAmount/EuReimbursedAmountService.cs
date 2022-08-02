using System;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.CertReports.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.EuReimbursedAmount
{
    public class EuReimbursedAmountService : IEuReimbursedAmountService
    {
        private IPermissionsRepository permissionsRepository;
        private ICertReportsRepository certReportsRepository;
        private IUnitOfWork unitOfWork;

        public EuReimbursedAmountService(
            IPermissionsRepository permissionsRepository,
            ICertReportsRepository certReportsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.certReportsRepository = certReportsRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool CanCreate(int userId, int programmeId)
        {
            var programmeIds = Array.Empty<int>();

            return programmeIds.Contains(programmeId);
        }

        public void AddCertReports(Domain.EuReimbursedAmounts.EuReimbursedAmount amount, int userId, int[] itemIds)
        {
            foreach (var itemId in itemIds)
            {
                if (this.certReportsRepository.GetProgrammeId(itemId) != amount.ProgrammeId)
                {
                    throw new InvalidOperationException(string.Format("Cannot create EuReimbursedAmount CertReport with id {0}", itemId));
                }

                amount.AddCertReport(itemId);
            }

            this.unitOfWork.Save();
        }
    }
}
