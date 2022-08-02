using System;
using System.Collections.Generic;
using Eumis.Common.Db;
using Eumis.Data.Counters;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Domain.Irregularities;

namespace Eumis.ApplicationServices.Services.Irregularity
{
    public class IrregularityVersionService : IIrregularityVersionService
    {
        private IUnitOfWork unitOfWork;
        private ICountersRepository countersRepository;
        private IIrregularitiesRepository irregularitiesRepository;
        private IIrregularityVersionsRepository irregularityVersionsRepository;

        public IrregularityVersionService(
            IUnitOfWork unitOfWork,
            ICountersRepository countersRepository,
            IIrregularitiesRepository irregularitiesRepository,
            IIrregularityVersionsRepository irregularityVersionsRepository)
        {
            this.unitOfWork = unitOfWork;
            this.countersRepository = countersRepository;
            this.irregularitiesRepository = irregularitiesRepository;
            this.irregularityVersionsRepository = irregularityVersionsRepository;
        }

        public bool CanEditVersion(int versionId)
        {
            var irrStatus = this.irregularityVersionsRepository.GetIrregularityStatus(versionId);

            return irrStatus != IrregularityStatus.Removed;
        }

        public void ActivateVersion(int versionId, byte[] version)
        {
            if (!this.CanEditVersion(versionId))
            {
                throw new InvalidOperationException("Cannot activate version.");
            }

            var irrVersion = this.irregularityVersionsRepository.FindForUpdate(versionId, version);
            var irregularity = this.irregularitiesRepository.Find(irrVersion.IrregularityId);

            if (irregularity.Status == IrregularityStatus.New)
            {
                this.countersRepository.CreateIrregularityCounter(irregularity.ProgrammeId);
            }
            else
            {
                var prevVersion = this.irregularityVersionsRepository.GetActiveVersion(irregularity.IrregularityId);
                prevVersion.ArchiveVersion();
            }

            irrVersion.ActivateVersion(irregularity.RegNumberPattern);
            irregularity.SetData(irrVersion.RegNumber, irrVersion.ReportYear, irrVersion.ReportQuarter, irrVersion.CaseState, irrVersion.IrregularityEndDate);

            this.unitOfWork.Save();
        }

        public IList<string> CanCreateVersion(int irregularityId)
        {
            IList<string> errors = new List<string>();

            if (this.irregularityVersionsRepository.HasDraftVersions(irregularityId))
            {
                errors.Add("Не може да се създаде ново уведомление за нередност, когато съществува уведомление със статус чернова за същата нередност.");
            }

            return errors;
        }
    }
}
