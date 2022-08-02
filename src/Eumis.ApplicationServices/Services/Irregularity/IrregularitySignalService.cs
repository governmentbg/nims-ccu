using Eumis.Data.Core.Permissions;
using Eumis.Data.Irregularities.Repositories;
using Eumis.Data.Procedures.Repositories;
using Eumis.Domain.Contracts;
using Eumis.Domain.Projects;
using Eumis.Domain.Users.ProgrammePermissions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.ApplicationServices.Services.Irregularity
{
    public class IrregularitySignalService : IIrregularitySignalService
    {
        private IPermissionsRepository permissionsRepository;
        private IProceduresRepository proceduresRepository;
        private IIrregularitySignalsRepository irregularitySignalsRepository;

        public IrregularitySignalService(
            IPermissionsRepository permissionsRepository,
            IProceduresRepository proceduresRepository,
            IIrregularitySignalsRepository irregularitySignalsRepository)
        {
            this.permissionsRepository = permissionsRepository;
            this.proceduresRepository = proceduresRepository;
            this.irregularitySignalsRepository = irregularitySignalsRepository;
        }

        public IList<string> CanCreateSignal(int userId, Domain.Contracts.Contract contract, Project project)
        {
            IList<string> errors = new List<string>();

            if (contract == null && project == null)
            {
                errors.Add("В системата няма въведен такъв договор или проектно предложение.");
            }
            else
            {
                var programmeIds = Array.Empty<int>();
                int? programmeId = null;

                if (contract != null)
                {
                    programmeId = contract.ProgrammeId;
                    if (contract.ContractStatus != ContractStatus.Entered)
                    {
                        errors.Add("Договорът трябва да е в статус въведен, за да може с него да се асоциира сигнал за нередност.");
                    }
                }
                else if (project != null)
                {
                    programmeId = this.proceduresRepository.GetPrimaryProcedureProgrammeId(project.ProcedureId);
                    if (project.RegistrationStatus == ProjectRegistrationStatus.Withdrawn)
                    {
                        errors.Add("Проектното предложение не трябва да е оттеглено, за да може да се асоциира с него сигнал за нередност.");
                    }
                }

                if (!programmeIds.Contains(programmeId.Value))
                {
                    errors.Add("Нямате право за писане за съответната програма.");
                }
            }

            return errors;
        }

        public IList<string> CanUpdatePartial(int programmeId, int signalId, string signalNumber)
        {
            IList<string> errors = new List<string>();

            if (this.irregularitySignalsRepository.HasNonRemovedIrregularityWithTheSameNumber(programmeId, signalId, signalNumber))
            {
                errors.Add("В системата вече съществува активен или приключен сигнал за нередност с този номер.");
            }

            if (!this.irregularitySignalsRepository.HasRemovedIrregularityWithTheSameNumber(programmeId, signalId, signalNumber))
            {
                errors.Add("В системата не съществува анулиран сигнал с този номер");
            }

            return errors;
        }

        public IList<string> CanActivate(int programmeId, int signalId, string signalNumber)
        {
            IList<string> errors = new List<string>();

            if (this.irregularitySignalsRepository.HasNonRemovedIrregularityWithTheSameNumber(programmeId, signalId, signalNumber))
            {
                errors.Add("В системата вече съществува активен или приключен сигнал за нередност с този номер.");
            }

            return errors;
        }

        public IList<string> CanSetStatusToRemoved(int signalId)
        {
            IList<string> errors = new List<string>();

            if (this.irregularitySignalsRepository.HasAssociatedNonRemovedIrregularity(signalId))
            {
                errors.Add("Не може да се анулира сигнал, към който има асоциирана нередност, която не е анулирана.");
            }

            return errors;
        }
    }
}
