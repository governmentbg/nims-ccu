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
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.SpotChecks;
using Eumis.Domain.Users.ProgrammePermissions;

namespace Eumis.ApplicationServices.Services.SpotCheck
{
    public class SpotCheckPlanService : ISpotCheckPlanService
    {
        private IPermissionsRepository permissionsRepository;
        private ISpotCheckPlansRepository spotCheckPlansRepository;
        private IProgrammePrioritiesRepository programmePrioritiesRepository;
        private IProceduresRepository proceduresRepository;
        private IContractsRepository contractsRepository;
        private IUnitOfWork unitOfWork;

        public SpotCheckPlanService(
            IPermissionsRepository permissionsRepository,
            ISpotCheckPlansRepository spotCheckPlansRepository,
            IProgrammePrioritiesRepository programmePrioritiesRepository,
            IProceduresRepository proceduresRepository,
            IContractsRepository contractsRepository,
            IUnitOfWork unitOfWork)
        {
            this.permissionsRepository = permissionsRepository;
            this.spotCheckPlansRepository = spotCheckPlansRepository;
            this.programmePrioritiesRepository = programmePrioritiesRepository;
            this.proceduresRepository = proceduresRepository;
            this.contractsRepository = contractsRepository;
            this.unitOfWork = unitOfWork;
        }

        public IList<string> CanCreate(int userId, int programmeId, int? contractId, SpotCheckLevel level, Year year, Month month)
        {
            var errors = new List<string>();

            var programmeIds = Array.Empty<int>();
            if (!programmeIds.Contains(programmeId))
            {
                errors.Add("Не може да се създаде нов план за проверка на място към програма, за която нямате право за писане на проверки на място.");
            }

            if (level == SpotCheckLevel.ContractContract)
            {
                var contract = this.contractsRepository.Find(contractId.Value);

                if (contract.ProgrammeId != programmeId || contract.ContractStatus != ContractStatus.Entered)
                {
                    errors.Add("Договорът, който сте избрали е невалиден.");
                }
            }

            if (!this.spotCheckPlansRepository.IsCheckPlanUnique(programmeId, year, month))
            {
                errors.Add("Не може да се създаде повече от един план за една програма в един и същи месец и година.");
            }

            return errors;
        }

        public void AddItems(SpotCheckPlan plan, int userId, int[] itemIds)
        {
            foreach (var itemId in itemIds)
            {
                bool canCreate = true;

                switch (plan.Level)
                {
                    case SpotCheckLevel.ProgrammePriority:
                        canCreate = this.programmePrioritiesRepository.GetProgrammeId(itemId) == plan.ProgrammeId;
                        break;
                    case SpotCheckLevel.Procedure:
                        var programmeIds = this.proceduresRepository.GetProcedureProgrammeIds(itemId);
                        var procStatus = this.proceduresRepository.GetProcedureStatus(itemId);

                        canCreate = programmeIds.Contains(plan.ProgrammeId)
                            && Procedure.EvalSessionOrProjectCreationStatuses.Contains(procStatus);
                        break;
                    case SpotCheckLevel.Contract:
                        var contractStatus = this.contractsRepository.GetContractStatus(itemId);

                        canCreate = this.contractsRepository.GetProgrammeId(itemId) == plan.ProgrammeId &&
                            contractStatus == ContractStatus.Entered;
                        break;
                    case SpotCheckLevel.ContractContract:
                        canCreate = this.contractsRepository.GetContractContractContractId(itemId) == plan.ContractId.Value;
                        break;
                }

                if (!canCreate)
                {
                    throw new InvalidOperationException(string.Format("Cannot create SpotCheckPlan item with id {0}", itemId));
                }

                plan.AddSpotCheckPlanItem(itemId);
            }

            this.unitOfWork.Save();
        }
    }
}
