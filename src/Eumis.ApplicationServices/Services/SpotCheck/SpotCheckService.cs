using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Common.Db;
using Eumis.Data.Contracts.Repositories;
using Eumis.Data.Core.Permissions;
using Eumis.Data.OperationalMap.ProgrammePriorities.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Data.SpotChecks.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Procedures;
using Eumis.Domain.SpotChecks;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.SpotCheck
{
    public class SpotCheckService : ISpotCheckService
    {
        private IPermissionsRepository permissionsRepository;
        private ISpotChecksRepository spotChecksRepository;
        private ISpotCheckPlansRepository spotCheckPlansRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractsRepository contractsRepository;
        private IUnitOfWork unitOfWork;

        public SpotCheckService(
            IPermissionsRepository permissionsRepository,
            ISpotChecksRepository spotChecksRepository,
            ISpotCheckPlansRepository spotCheckPlansRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractsRepository contractsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.spotChecksRepository = spotChecksRepository;
            this.spotCheckPlansRepository = spotCheckPlansRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractsRepository = contractsRepository;
            this.unitOfWork = unitOfWork;
        }

        public bool CanCreatePlannedCheck(int userId, int spotCheckPlanId)
        {
            var canCreate = true;

            var programmeId = this.spotCheckPlansRepository.GetProgrammeId(spotCheckPlanId);
            var programmeIds = Array.Empty<int>();
            if (!programmeIds.Contains(programmeId))
            {
                canCreate = false;
            }

            return canCreate;
        }

        public bool CanCreateCheck(int userId, int programmeId, SpotCheckLevel level, int? contractId)
        {
            var programmeIds = Array.Empty<int>();
            var canCreate = programmeIds.Contains(programmeId);

            if (level == SpotCheckLevel.ContractContract)
            {
                var contract = this.contractsRepository.Find(contractId.Value);

                canCreate = contract.ProgrammeId == programmeId && contract.ContractStatus == ContractStatus.Entered;
            }

            return canCreate;
        }

        public IList<string> CanEnterCheck(int spotCheckId)
        {
            IList<string> errors = new List<string>();

            if (!this.spotChecksRepository.HasDocuments(spotCheckId))
            {
                errors.Add("Проверките на място трябва да имат приложени документи.");
            }

            return errors;
        }

        public void AddItems(Domain.SpotChecks.SpotCheck check, int userId, int[] itemIds)
        {
            foreach (var itemId in itemIds)
            {
                bool canCreate = true;

                switch (check.Level)
                {
                    case SpotCheckLevel.ProgrammePriority:
                        canCreate = this.programmePrioritiesRepository.GetProgrammeId(itemId) == check.ProgrammeId;
                        break;
                    case SpotCheckLevel.Procedure:
                        var programmeIds = this.proceduresRepository.GetProcedureProgrammeIds(itemId);
                        var procStatus = this.proceduresRepository.GetProcedureStatus(itemId);

                        canCreate = programmeIds.Contains(check.ProgrammeId)
                            && Procedure.EvalSessionOrProjectCreationStatuses.Contains(procStatus);
                        break;
                    case SpotCheckLevel.Contract:
                        var contractStatus = this.contractsRepository.GetContractStatus(itemId);

                        canCreate = this.contractsRepository.GetProgrammeId(itemId) == check.ProgrammeId &&
                            contractStatus == ContractStatus.Entered;
                        break;
                    case SpotCheckLevel.ContractContract:
                        canCreate = this.contractsRepository.GetContractContractContractId(itemId) == check.ContractId.Value;
                        break;
                }

                if (!canCreate)
                {
                    throw new InvalidOperationException(string.Format("Cannot create SpotCheck item with id {0}", itemId));
                }

                check.AddSpotCheckItem(itemId);
            }

            this.unitOfWork.Save();
        }
    }
}
