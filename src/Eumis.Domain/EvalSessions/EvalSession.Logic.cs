using Eumis.Common.Json;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.Procedures;
using Eumis.Domain.Projects;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;

namespace Eumis.Domain.EvalSessions
{
    public partial class EvalSession : IAggregateRoot
    {
        #region EvalSession

        public void UpdateAttributes(
            string sessionNum,
            DateTime? sessionDate,
            string orderNum,
            DateTime? orderDate)
        {
            this.AssertIsDraftEvalSession();

            this.SessionNum = sessionNum;
            this.SessionDate = sessionDate;
            this.OrderNum = orderNum;
            this.OrderDate = orderDate;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeEvalSessionStatus(EvalSessionStatus status)
        {
            if (this.EvalSessionStatus == status)
            {
                throw new DomainValidationException("EvalSession status transition not allowed");
            }

            if (this.EvalSessionStatus != EvalSessionStatus.Active)
            {
                if (this.EvalSessionStatus == EvalSessionStatus.Draft)
                {
                    if (status != EvalSessionStatus.Active && status != EvalSessionStatus.Canceled)
                    {
                        throw new DomainValidationException("EvalSession status transition not allowed");
                    }
                }
                else if (this.EvalSessionStatus == EvalSessionStatus.Ended)
                {
                    if (status != EvalSessions.EvalSessionStatus.Draft)
                    {
                        throw new DomainValidationException("EvalSession status transition not allowed");
                    }
                }
                else if (this.EvalSessionStatus == EvalSessionStatus.Canceled)
                {
                    if (status != EvalSessions.EvalSessionStatus.Draft)
                    {
                        throw new DomainValidationException("EvalSession status transition not allowed");
                    }
                }
                else if (this.EvalSessionStatus == EvalSessionStatus.EndedByLAG)
                {
                    if (status != EvalSessions.EvalSessionStatus.Active && status != EvalSessions.EvalSessionStatus.Ended)
                    {
                        throw new DomainValidationException("EvalSession status transition not allowed");
                    }
                }
                else
                {
                    throw new DomainValidationException("EvalSession status transition not allowed");
                }
            }

            if (status == EvalSessions.EvalSessionStatus.Active)
            {
                if (this.CanChangeStatusToActive().Count != 0)
                {
                    throw new DomainValidationException("EvalSession status transition not allowed");
                }

                foreach (var evalSessionUser in this.EvalSessionUsers)
                {
                    if (evalSessionUser.Status == EvalSessionUserStatus.NotActivated)
                    {
                        evalSessionUser.Status = EvalSessionUserStatus.Activated;
                    }
                }
            }

            ((IEventEmitter)this).Events.Add(new EvalSessionStatusChangedEvent(this.EvalSessionId));

            this.EvalSessionStatus = status;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEnded(IList<Tuple<ProjectRegistrationStatus, bool, int?>> projectsInfo)
        {
            if (this.EvalSessionStatus == EvalSessionStatus.Ended)
            {
                throw new DomainValidationException("EvalSession status transition not allowed");
            }

            if (this.EvalSessionStatus != EvalSessionStatus.Active && this.EvalSessionStatus != EvalSessionStatus.EndedByLAG)
            {
                throw new DomainValidationException("EvalSession status transition not allowed");
            }

            if (this.CanChangeStatusToEnded(projectsInfo).Count != 0)
            {
                throw new DomainValidationException("EvalSession status transition not allowed");
            }

            ((IEventEmitter)this).Events.Add(new EvalSessionStatusChangedEvent(this.EvalSessionId));

            this.EvalSessionStatus = EvalSessionStatus.Ended;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEndedByLAG(IList<Tuple<ProjectRegistrationStatus, bool, int?>> projectsInfo)
        {
            if (this.EvalSessionStatus != EvalSessionStatus.Active)
            {
                throw new DomainValidationException("EvalSession status transition not allowed");
            }

            if (this.CanChangeStatusToEnded(projectsInfo).Count != 0)
            {
                throw new DomainValidationException("EvalSession status transition not allowed");
            }

            ((IEventEmitter)this).Events.Add(new EvalSessionStatusChangedEvent(this.EvalSessionId));

            this.EvalSessionStatus = EvalSessionStatus.EndedByLAG;
            this.ModifyDate = DateTime.Now;
        }

        public List<string> CanChangeStatusToActive()
        {
            var errors = new List<string>();

            if (this.EvalSessionUsers.Count(p => p.Type == EvalSessionUserType.Administrator) == 0 || this.EvalSessionUsers.Count(p => p.Type == EvalSessionUserType.Assessor) == 0 || this.EvalSessionProjects.Count == 0)
            {
                errors.Add(DomainTexts.EvalSession_CanChangeStatusToActive_AtLeastOneAdministratorAndUser);
            }

            return errors;
        }

        public List<string> CanChangeStatusToEnded(IList<Tuple<ProjectRegistrationStatus, bool, int?>> projectsInfo)
        {
            var errors = new List<string>();

            if (projectsInfo.Where(p => p.Item1 != ProjectRegistrationStatus.Withdrawn && p.Item2 == false && !p.Item3.HasValue).Any())
            {
                errors.Add(DomainTexts.EvalSession_CanChangeStatusToEnded_AllProjectsShouldBeClassifiedDeletedOrWithdrawn);
            }

            return errors;
        }

        private void AssertIsDraftEvalSession()
        {
            if (this.EvalSessionStatus != EvalSessionStatus.Draft)
            {
                throw new DomainValidationException("Cannot edit EvalSession that is not in Draft status");
            }
        }

        private void AssertIsActiveEvalSession()
        {
            if (this.EvalSessionStatus != EvalSessionStatus.Active)
            {
                throw new DomainValidationException("Cannot edit EvalSession that is not in Active status");
            }
        }

        private void AssertEvalSessionIsActiveOrEndedByLAG()
        {
            if (this.EvalSessionStatus != EvalSessionStatus.Active && this.EvalSessionStatus != EvalSessionStatus.EndedByLAG)
            {
                throw new DomainValidationException("Cannot edit EvalSession that is not in Active or EndedByLAG status");
            }
        }

        #endregion //EvalSession

        #region EvalSessionUser

        public EvalSessionUser FindEvalSessionUser(int evalSessionUserId)
        {
            var evalSessionUser = this.EvalSessionUsers.Where(e => e.EvalSessionUserId == evalSessionUserId).SingleOrDefault();

            if (evalSessionUser == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionUser with id " + evalSessionUserId);
            }

            return evalSessionUser;
        }

        public void AddEvalSessionUser(
            int userId,
            EvalSessionUserType type,
            string position)
        {
            this.AssertIsDraftEvalSession();

            EvalSessionUser evalSessionUser = new EvalSessionUser()
            {
                UserId = userId,
                Type = type,
                Position = position,
                Status = EvalSessionUserStatus.NotActivated,
            };

            this.EvalSessionUsers.Add(evalSessionUser);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateEvalSessionUser(int evalSessionUserId, string position)
        {
            this.AssertIsDraftEvalSession();

            var evalSessionUser = this.FindEvalSessionUser(evalSessionUserId);

            evalSessionUser.SetAttributes(position);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveEvalSessionUser(int evalSessionUserId)
        {
            this.AssertIsDraftEvalSession();

            if (this.EvalSessionSheets.Where(p => p.EvalSessionUserId == evalSessionUserId).Any())
            {
                throw new DomainException("Cannot remove EvalSessionProject that is included in a EvalSessionSheet");
            }

            if (this.EvalSessionStandpoints.Where(p => p.EvalSessionUserId == evalSessionUserId).Any())
            {
                throw new DomainException("Cannot remove EvalSessionProject that is included in a EvalSessionStandpoint");
            }

            var evalSessionUser = this.FindEvalSessionUser(evalSessionUserId);

            if (evalSessionUser.Status != EvalSessionUserStatus.NotActivated)
            {
                throw new DomainException("Cannot remove activated EvalSessionUser");
            }

            this.EvalSessionUsers.Remove(evalSessionUser);

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateEvalSessionUser(int evalSessionUserId)
        {
            this.ChangeEvalSessionUserStatus(evalSessionUserId, EvalSessionUserStatus.Activated);
        }

        public void DeactivateEvalSessionUser(int evalSessionUserId)
        {
            this.ChangeEvalSessionUserStatus(evalSessionUserId, EvalSessionUserStatus.Deactivated);
        }

        private void ChangeEvalSessionUserStatus(int evalSessionUserId, EvalSessionUserStatus userStatus)
        {
            this.AssertIsDraftEvalSession();

            var evalSessionUser = this.FindEvalSessionUser(evalSessionUserId);
            if (evalSessionUser.Status == userStatus)
            {
                throw new DomainValidationException("EvalSessionUser status transition not allowed");
            }

            evalSessionUser.Status = userStatus;
            this.ModifyDate = DateTime.Now;
        }

        #endregion //EvalSessionUser

        #region EvalSessionProject

        public EvalSessionProject FindEvalSessionProject(int projectId)
        {
            var evalSessionProject = this.EvalSessionProjects.Where(e => e.ProjectId == projectId).SingleOrDefault();

            if (evalSessionProject == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionProject with id " + projectId);
            }

            return evalSessionProject;
        }

        public void AddEvalSessionProject(int projectId, ProjectRegistrationStatus projectStatus)
        {
            this.AssertIsDraftEvalSession();

            if (projectStatus == ProjectRegistrationStatus.Withdrawn)
            {
                throw new DomainObjectNotFoundException("Cannot add withdrawn Project");
            }

            EvalSessionProject evalSessionProject = new EvalSessionProject()
            {
                ProjectId = projectId,
                IsDeleted = false,
            };

            this.EvalSessionProjects.Add(evalSessionProject);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveEvalSessionProject(int projectId)
        {
            this.AssertIsDraftEvalSession();

            if (this.EvalSessionSheets.Where(p => p.ProjectId == projectId).Any())
            {
                throw new DomainException("Cannot remove EvalSessionProject that is included in a EvalSessionSheet");
            }

            if (this.EvalSessionStandpoints.Where(p => p.ProjectId == projectId).Any())
            {
                throw new DomainException("Cannot remove EvalSessionProject that is included in a EvalSessionStandpoint");
            }

            var evalSessionProject = this.FindEvalSessionProject(projectId);

            this.EvalSessionProjects.Remove(evalSessionProject);

            this.ModifyDate = DateTime.Now;
        }

        public void CancelEvalSessionProject(int projectId, string cancelNote)
        {
            this.AssertIsActiveEvalSession();

            if (this.EvalSessionSheets.Where(p => p.ProjectId == projectId && p.Status != EvalSessionSheetStatus.Canceled).Any())
            {
                throw new DomainException("Cannot remove EvalSessionProject that is included in a non-canceled EvalSessionSheet");
            }

            if (this.EvalSessionEvaluations.Where(p => p.ProjectId == projectId && p.IsDeleted == false).Any())
            {
                throw new DomainException("Cannot remove EvalSessionProject that is included in a non-canceled EvalSessionEvaluation");
            }

            var evalSessionProject = this.FindEvalSessionProject(projectId);

            if (evalSessionProject.IsDeleted)
            {
                throw new DomainException("Cannot cancel an EvalSessionProject twice");
            }

            evalSessionProject.IsDeleted = true;
            evalSessionProject.IsDeletedNote = cancelNote;

            this.ModifyDate = DateTime.Now;
        }

        public void RestoreEvalSessionProject(int projectId)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionProject = this.FindEvalSessionProject(projectId);

            if (!evalSessionProject.IsDeleted)
            {
                throw new DomainException("Cannot restore an EvalSessionProject twice");
            }

            evalSessionProject.IsDeleted = false;
            evalSessionProject.IsDeletedNote = null;

            this.ModifyDate = DateTime.Now;
        }

        #endregion //EvalSessionProject

        #region EvalSessionSheet

        public EvalSessionSheet FindEvalSessionSheet(int evalSessionSheetId)
        {
            var evalSessionSheet = this.EvalSessionSheets.Where(e => e.EvalSessionSheetId == evalSessionSheetId).SingleOrDefault();

            if (evalSessionSheet == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionSheet with id " + evalSessionSheetId);
            }

            return evalSessionSheet;
        }

        public EvalSessionSheet AddEvalSessionSheet(
            int evalSessionUserId,
            int projectId,
            ProcedureEvalTableType evalTableType,
            EvalSessionDistributionType distributionType,
            string notes,
            ProjectRegistrationStatus? projectStatus = null,
            int? evalSessionDistributionId = null)
        {
            this.AssertIsActiveEvalSession();

            var currentDate = DateTime.Now;

            EvalSessionSheet evalSessionSheet = new EvalSessionSheet(
                this.EvalSessionId,
                evalSessionUserId,
                projectId,
                evalTableType,
                currentDate,
                distributionType,
                evalSessionDistributionId,
                notes);

            if (this.CanCreateEvalSessionSheet(evalSessionUserId, projectId, evalTableType, distributionType, projectStatus) != null)
            {
                // someone already created a similar sheet or validation failed
                throw new DomainValidationException("Cannot create EvalSessionSheet");
            }

            this.EvalSessionSheets.Add(evalSessionSheet);

            this.ModifyDate = currentDate;

            return evalSessionSheet;
        }

        public void ChangeEvalSessionSheetStatus(int evalSessionSheetId, EvalSessionSheetStatus status, string statusNote)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionSheet = this.FindEvalSessionSheet(evalSessionSheetId);

            if (evalSessionSheet.Status == status)
            {
                throw new DomainValidationException("EvalSessionSheet status transition not allowed");
            }

            switch (evalSessionSheet.Status)
            {
                case EvalSessionSheetStatus.Draft:
                    if (status != EvalSessionSheetStatus.Paused && status != EvalSessionSheetStatus.Ended)
                    {
                        throw new DomainValidationException("EvalSessionSheet status transition not allowed");
                    }

                    break;
                default:
                    throw new DomainValidationException("EvalSessionSheet status transition not allowed");
            }

            evalSessionSheet.StatusNote = statusNote;
            evalSessionSheet.Status = status;
            evalSessionSheet.StatusDate = DateTime.Now;
            this.ModifyDate = DateTime.Now;
        }

        public void CancelEvalSessionSheet(int sheetId, string statusNote)
        {
            this.AssertIsActiveEvalSession();

            if (this.EvalSessionEvaluations.Where(t => t.IsDeleted == false && t.EvalSessionEvaluationSheets.Where(p => p.EvalSessionSheetId == sheetId).Any()).Any())
            {
                throw new DomainValidationException("Cannot cancel an EvalSessionSheet which is associated with a non-deleted EvalSessionEvaluation");
            }

            var evalSessionSheet = this.FindEvalSessionSheet(sheetId);

            if (evalSessionSheet.ContinuedEvalSessionSheetId.HasValue)
            {
                throw new DomainValidationException("EvalSessionSheet status transition not allowed");
            }

            if (evalSessionSheet.Status == EvalSessionSheetStatus.Canceled)
            {
                throw new DomainValidationException("EvalSessionSheet status transition not allowed");
            }

            ((IEventEmitter)this).Events.Add(new EvalSessionSheetCanceledEvent() { EvalSessionSheetUserId = this.FindEvalSessionUser(evalSessionSheet.EvalSessionUserId).UserId });

            if (evalSessionSheet.DistributionType == EvalSessionDistributionType.Continued)
            {
                var parentEvalSessionSheet = this.EvalSessionSheets.Where(p => p.ContinuedEvalSessionSheetId == sheetId).Single();
                this.RemoveContinuedEvalSessionSheetId(parentEvalSessionSheet);
            }

            evalSessionSheet.StatusNote = statusNote;
            evalSessionSheet.Status = EvalSessionSheetStatus.Canceled;
            evalSessionSheet.StatusDate = DateTime.Now;
            this.ModifyDate = DateTime.Now;
        }

        public string CanCreateEvalSessionSheet(
            int evalSessionUserId,
            int projectId,
            ProcedureEvalTableType evalTableType,
            EvalSessionDistributionType distributionType,
            ProjectRegistrationStatus? projectStatus = null)
        {
            if (projectStatus == ProjectRegistrationStatus.Withdrawn)
            {
                return DomainTexts.EvalSession_CanCreateEvalSessionSheet_ProjectIsWithdrawn;
            }
            else if (this.FindEvalSessionProject(projectId).IsDeleted)
            {
                return DomainTexts.EvalSession_CanCreateEvalSessionSheet_ProjectIsWithdrawn;
            }
            else if (!this.EvalSessionUsers.Any(esu => esu.EvalSessionUserId == evalSessionUserId && esu.Status == EvalSessionUserStatus.Activated))
            {
                return DomainTexts.EvalSession_CanCreateEvalSessionSheet_UserIsNotActivated;
            }
            else if (this.EvalSessionEvaluations.Where(p => p.ProjectId == projectId && p.IsDeleted == false && p.EvalTableType == evalTableType).Any())
            {
                return DomainTexts.EvalSession_CanCreateEvalSessionSheet_NonDeletedEvaluationForThisEvalTypeExists;
            }
            else if (distributionType != EvalSessionDistributionType.Continued)
            {
                var results = this.EvalSessionSheets
                        .Where(ess =>
                            ess.EvalSessionUserId == evalSessionUserId &&
                            ess.ProjectId == projectId &&
                            ess.EvalTableType == evalTableType &&
                            ess.Status != EvalSessionSheetStatus.Canceled)
                        .ToList();

                if (results.Count() != 0)
                {
                    return DomainTexts.EvalSession_CanCreateEvalSessionSheet_DuplicatedEvalSessionSheet;
                }
            }

            return null;
        }

        public void AddContinuedEvalSessionSheetId(EvalSessionSheet es, int continuedEvalSessionSheetId)
        {
            if (es.ContinuedEvalSessionSheetId.HasValue)
            {
                throw new DomainException("Cannot continue an EvalSessionSheet twice");
            }

            es.ContinuedEvalSessionSheetId = continuedEvalSessionSheetId;
            this.ModifyDate = DateTime.Now;
        }

        public void RemoveContinuedEvalSessionSheetId(EvalSessionSheet es)
        {
            if (!es.ContinuedEvalSessionSheetId.HasValue)
            {
                throw new DomainException("Cannot remove a continued EvalSessionSheet twice");
            }

            es.ContinuedEvalSessionSheetId = null;
            this.ModifyDate = DateTime.Now;
        }

        #endregion //EvalSessionSheet

        #region EvalSessionStandpoint

        public EvalSessionStandpoint AddEvalSessionStandpoint(
            int evalSessionUserId,
            int projectId,
            ProjectRegistrationStatus projectStatus,
            string note)
        {
            this.AssertIsActiveEvalSession();

            var currentDate = DateTime.Now;

            EvalSessionStandpoint evalSessionStandpoint = new EvalSessionStandpoint()
            {
                EvalSessionUserId = evalSessionUserId,
                ProjectId = projectId,
                Status = EvalSessionStandpointStatus.Draft,
                StatusDate = currentDate,
                CreateDate = currentDate,
                Note = note,
            };

            if (this.CanCreateEvalSessionStandpoint(projectId, projectStatus) != null)
            {
                throw new DomainValidationException("Cannot create EvalSessionStandpoint");
            }

            this.EvalSessionStandpoints.Add(evalSessionStandpoint);

            this.ModifyDate = DateTime.Now;

            return evalSessionStandpoint;
        }

        public string CanCreateEvalSessionStandpoint(int projectId, ProjectRegistrationStatus projectStatus)
        {
            if (projectStatus == ProjectRegistrationStatus.Withdrawn)
            {
                return DomainTexts.EvalSession_CanCreateEvalSessionStandpoint_ProjectIsWithdrawn;
            }
            else if (this.FindEvalSessionProject(projectId).IsDeleted)
            {
                return DomainTexts.EvalSession_CanCreateEvalSessionStandpoint_ProjectIsDeleted;
            }

            return null;
        }

        public EvalSessionStandpoint FindEvalSessionStandpoint(int evalSessionStandpointId)
        {
            var evalSessionStandpoint = this.EvalSessionStandpoints.Where(e => e.EvalSessionStandpointId == evalSessionStandpointId).SingleOrDefault();

            if (evalSessionStandpoint == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionStandpoint with id " + evalSessionStandpointId);
            }

            return evalSessionStandpoint;
        }

        public void ChangeEvalSessionStandpointStatusToEnded(int evalSessionStandpointId)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionStandpoint = this.FindEvalSessionStandpoint(evalSessionStandpointId);

            if (evalSessionStandpoint.Status != EvalSessionStandpointStatus.Draft)
            {
                throw new DomainValidationException("EvalSessionStandpoint status transition not allowed");
            }

            evalSessionStandpoint.Status = EvalSessionStandpointStatus.Ended;
            evalSessionStandpoint.StatusDate = DateTime.Now;
            this.ModifyDate = DateTime.Now;
        }

        public void CancelEvalSessionStandpoint(int evalSessionStandpointId, string note)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionStandpoint = this.FindEvalSessionStandpoint(evalSessionStandpointId);

            if (evalSessionStandpoint.Status == EvalSessionStandpointStatus.Canceled)
            {
                throw new DomainValidationException("EvalSessionStandpoint status transition not allowed");
            }

            ((IEventEmitter)this).Events.Add(new EvalSessionStandpointCanceledEvent()
            {
                EvalSessionStandpointUserId = evalSessionStandpoint.EvalSession.FindEvalSessionUser(evalSessionStandpoint.EvalSessionUserId).UserId,
            });

            evalSessionStandpoint.DeleteNote = note;
            evalSessionStandpoint.Status = EvalSessionStandpointStatus.Canceled;
            evalSessionStandpoint.StatusDate = DateTime.Now;
            this.ModifyDate = DateTime.Now;
        }

        #endregion //EvalSessionStandpoint

        #region EvalSessionDistribution

        public EvalSessionDistribution FindEvalSessionDistribution(int evalSessionDistributionId)
        {
            var evalSessionDistribution = this.EvalSessionDistributions.Where(e => e.EvalSessionDistributionId == evalSessionDistributionId).SingleOrDefault();

            if (evalSessionDistribution == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionDistribution with id " + evalSessionDistributionId);
            }

            return evalSessionDistribution;
        }

        public EvalSessionDistribution AddEvalSessionDistribution(
            string number,
            ProcedureEvalTableType evalTableType,
            int assessorsPerProject,
            Procedure procedure)
        {
            this.AssertIsActiveEvalSession();

            if (this.CanCreateEvalSessionDistribution(procedure, evalTableType).Count != 0)
            {
                throw new DomainObjectNotFoundException("Cannot add EvalSessionDistribution because there are validation errors");
            }

            EvalSessionDistribution evalSessionDistribution = new EvalSessionDistribution()
            {
                EvalTableType = evalTableType,
                Code = number,
                CreateDate = DateTime.Now,
                Status = EvalSessionDistributionStatus.Applied,
                AssessorsPerProject = assessorsPerProject,
            };

            this.EvalSessionDistributions.Add(evalSessionDistribution);

            return evalSessionDistribution;
        }

        public Tuple<List<EvalSessionSheet>, List<EvalSessionDistributionProject>> GenerateEvalSessionSheets(
            int evalSessionDistributionId,
            int[] projectsIds,
            IList<EvalSessionDistributionUserDO> assessors,
            IDictionary<int, string> excludedProjects)
        {
            var evalSessionDistribution = this.FindEvalSessionDistribution(evalSessionDistributionId);
            var assessorsPerProject = evalSessionDistribution.AssessorsPerProject;
            var filteredProjectIds = new List<int>();
            var filteredUserIds = new List<int>();

            if (assessors.Count < 1)
            {
                throw new DomainException("Assessors count cannot be smaller than 1");
            }

            if (projectsIds.Length < 1)
            {
                throw new DomainException("Projects count cannot be smaller than 1");
            }

            if (assessorsPerProject < 1 || assessorsPerProject > assessors.Where(a => !a.IsDeleted).Count())
            {
                throw new DomainException("AssessorsPerProject cannot be smaller than 1 and cannot be larger than the actual asessors count");
            }

            foreach (var assessor in assessors)
            {
                evalSessionDistribution.EvalSessionDistributionUsers.Add(new EvalSessionDistributionUser
                {
                    EvalSessionId = assessor.EvalSessionId,
                    EvalSessionUserId = assessor.EvalSessionUserId,
                    IsDeleted = assessor.IsDeleted,
                    IsDeletedNote = assessor.IsDeletedNote,
                });

                if (!assessor.IsDeleted)
                {
                    filteredUserIds.Add(assessor.EvalSessionUserId);
                }
            }

            List<EvalSessionDistributionProject> evalSessionDistributionProjects = new List<EvalSessionDistributionProject>();

            foreach (var pId in projectsIds)
            {
                var esdp = new EvalSessionDistributionProject
                {
                    EvalSessionId = this.EvalSessionId,
                    EvalSessionDistributionId = evalSessionDistributionId,
                    ProjectId = pId,
                };

                // if the user is not trying to trick us(e.g. mess with the js) excludedProjects should contain
                // those projects that were excluded server-side(with validation errors)
                // but we can't rely on that so we double check
                var validationErrors = this.ValidateEvalSessionDistributionProject(pId, evalSessionDistribution.EvalTableType);
                if (validationErrors.Any())
                {
                    esdp.IsDeleted = true;
                    esdp.IsDeletedNote = string.Join(", ", validationErrors);
                }
                else if (excludedProjects.ContainsKey(pId))
                {
                    esdp.IsDeleted = true;
                    esdp.IsDeletedNote = excludedProjects[pId];

                    if (string.IsNullOrWhiteSpace(esdp.IsDeletedNote))
                    {
                        throw new DomainException("Excluded projects should have IsDeletedNote");
                    }
                }

                if (!esdp.IsDeleted)
                {
                    filteredProjectIds.Add(pId);
                }

                evalSessionDistributionProjects.Add(esdp);
            }

            if (filteredProjectIds.Count < 1)
            {
                throw new DomainException("Filtered projects count cannot be smaller than 1");
            }

            List<EvalSessionSheet> evalSessionSheets = new List<EvalSessionSheet>();

            byte[] buffer = new byte[4];
            int i, seed, randomId, selectedUserId, occurrencesLimit;
            var rngCsp = new RNGCryptoServiceProvider();

            // contain pairs of type (userId, number of times the userId has been used)
            Dictionary<int, int> userIds = new Dictionary<int, int>();
            Dictionary<int, int> usedUserIdsInCurrentProject = new Dictionary<int, int>(assessorsPerProject);
            Dictionary<int, int> userIdsTemp = new Dictionary<int, int>();
            Dictionary<int, int> usedUserIdsInLastProject = new Dictionary<int, int>(assessorsPerProject);

            foreach (int userId in filteredUserIds)
            {
                userIds.Add(userId, 0);
            }

            // calculate the minimum occurrences for an assessor in all projects, so that we can have an uniform distribution
            if (filteredProjectIds.Count * assessorsPerProject <= userIds.Count)
            {
                occurrencesLimit = 1;
            }
            else
            {
                double average = (filteredProjectIds.Count * assessorsPerProject) / userIds.Count;
                occurrencesLimit = (int)Math.Floor(average);
            }

            var currentDate = DateTime.Now;

            Action<int, int> addSheet = (projectId, userId) =>
            {
                if (this.CanCreateEvalSessionSheet(userId, projectId, evalSessionDistribution.EvalTableType, EvalSessionDistributionType.Automatic, null) != null)
                {
                    // someone already created a similar sheet or validation failed
                    throw new DomainValidationException("Cannot create EvalSessionSheet");
                }

                EvalSessionSheet evalSessionSheet = new EvalSessionSheet(
                    this.EvalSessionId,
                    userId,
                    projectId,
                    evalSessionDistribution.EvalTableType,
                    currentDate,
                    EvalSessionDistributionType.Automatic,
                    evalSessionDistributionId,
                    null);

                evalSessionSheets.Add(evalSessionSheet);
            };

            // randomly distributes assessorCount user ids from the given assessorIds for the given projectId
            Action<int, int, Dictionary<int, int>> distributeAssessors = (projectId, assessorCount, assessorIds) =>
            {
                for (i = 0; i < assessorCount; i++)
                {
                    rngCsp.GetBytes(buffer);
                    seed = BitConverter.ToInt32(buffer, 0);
                    randomId = new Random(seed).Next(assessorIds.Count);
                    selectedUserId = assessorIds.Keys.ElementAt(randomId);

                    assessorIds[selectedUserId]++;
                    if (assessorIds[selectedUserId] == occurrencesLimit)
                    {
                        // do not add this user id to usedUserIdsInCurrentProject, because it has completed the minimum
                        // and if added it can eventually be overused and this would ruin the uniform distribution
                        assessorIds.Remove(selectedUserId);
                    }
                    else
                    {
                        usedUserIdsInCurrentProject.Add(selectedUserId, assessorIds[selectedUserId]);
                        assessorIds.Remove(selectedUserId);
                    }

                    addSheet(projectId, selectedUserId);
                }

                // restore the used user ids in this project, so that the remaining projects can use them
                foreach (var assessorId in usedUserIdsInCurrentProject)
                {
                    assessorIds.Add(assessorId.Key, assessorId.Value);
                }

                usedUserIdsInCurrentProject.Clear();
            };

            foreach (var projectId in filteredProjectIds)
            {
                // we don't have enough user ids to distribute for this project
                if (userIds.Count < assessorsPerProject)
                {
                    // userIds contains the user ids that MUST be used, so that the distribution is uniform
                    // userIdsTemp contains the rest user ids available, that are not in userIds
                    // usedUserIdsInProjectTemp contains the used user ids for the last project(that did not have enough user ids or nothing if it did have),
                    // excluding the ones that MUST be used(the ones contained in userIds)
                    var filtUserIds = filteredUserIds.Where(u => !usedUserIdsInLastProject.Keys.Contains(u)).ToList();
                    usedUserIdsInLastProject.Clear();
                    foreach (int userId in filtUserIds)
                    {
                        if (userIds.Keys.Contains(userId))
                        {
                            continue;
                        }

                        // we add the user ids with occurrencesLimit - 1,
                        // so that they will be excluded from userids, after they are used
                        userIdsTemp.Add(userId, occurrencesLimit - 1);
                        usedUserIdsInLastProject.Add(userId, occurrencesLimit - 1);
                    }

                    var assessorsLeft = assessorsPerProject - userIds.Count;

                    // we use the user ids that MUST be used
                    distributeAssessors(projectId, userIds.Count, userIds);

                    // we distribute some of the remaining user ids, so that we accomplished the assessorsPerProject count
                    distributeAssessors(projectId, assessorsLeft, userIdsTemp);

                    // the user ids that are not in userIdsTemp and are contained in usedUserIdsInLastProject are the used ones
                    foreach (var userId in userIdsTemp)
                    {
                        userIds.Add(userId.Key, userId.Value);
                        usedUserIdsInLastProject.Remove(userId.Key);
                    }

                    userIdsTemp.Clear();
                }
                else
                {
                    distributeAssessors(projectId, assessorsPerProject, userIds);
                }
            }

            rngCsp.Dispose();

            this.ModifyDate = DateTime.Now;

            return new Tuple<List<EvalSessionSheet>, List<EvalSessionDistributionProject>>(evalSessionSheets, evalSessionDistributionProjects);
        }

        public List<EvalSessionSheet> RefuseEvalSessionDistribution(int evalSessionDistributionId, string statusNote)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionDistribution = this.FindEvalSessionDistribution(evalSessionDistributionId);

            if (evalSessionDistribution.Status != EvalSessionDistributionStatus.Applied)
            {
                throw new DomainValidationException("EvalSessionDistribution status transition not allowed");
            }

            var evalSessionSheets = this.EvalSessionSheets.Where(t => t.EvalSessionDistributionId == evalSessionDistributionId).ToList();
            var evalSessionSheetIds = evalSessionSheets.Select(t => t.EvalSessionSheetId).ToList();

            if (evalSessionSheets.Where(t => t.ContinuedEvalSessionSheetId.HasValue).Any())
            {
                throw new DomainValidationException("Cannot refuse EvalSessionDistribution when there are non-canceled continued sheets");
            }

            if (this.EvalSessionEvaluations.Where(t => t.IsDeleted == false && t.EvalSessionEvaluationSheets.Where(p => evalSessionSheetIds.Contains(p.EvalSessionSheetId)).Any()).Any())
            {
                throw new DomainValidationException("Cannot refuse EvalSessionDistribution when there are non-canceled EvalSessionEvaluations associated with some of the sheets");
            }

            List<EvalSessionSheet> sheets = new List<EvalSessionSheet>();
            foreach (var sheet in evalSessionSheets)
            {
                if (sheet.Status != EvalSessionSheetStatus.Canceled)
                {
                    this.CancelEvalSessionSheet(sheet.EvalSessionSheetId, statusNote);
                    sheets.Add(sheet);
                }
            }

            evalSessionDistribution.StatusNote = statusNote;
            evalSessionDistribution.Status = EvalSessionDistributionStatus.Refused;
            this.ModifyDate = DateTime.Now;

            return sheets;
        }

        public IList<string> ValidateEvalSessionDistributionProject(
            int projectId,
            ProcedureEvalTableType evalTableType)
        {
            var validationErrors = new List<string>();

            if (this.EvalSessionSheets.Where(ess => ess.ProjectId == projectId && ess.EvalTableType == evalTableType && ess.Status != EvalSessionSheetStatus.Canceled).Any())
            {
                validationErrors.Add(DomainTexts.EvalSession_ValidateEvalSessionDistributionProject_DuplicatedEvalSessionSheet);
            }

            if (evalTableType == ProcedureEvalTableType.TechFinance &&
                !this.EvalSessionEvaluations.Where(p => p.ProjectId == projectId && p.EvalTableType == ProcedureEvalTableType.AdminAdmiss && p.IsDeleted == false && p.EvalIsPassed == true).Any())
            {
                validationErrors.Add(DomainTexts.EvalSession_ValidateEvalSessionDistributionProject_ProjectHasNoOASD);
            }

            if (this.EvalSessionEvaluations.Where(p => p.ProjectId == projectId && p.EvalTableType == evalTableType && !p.IsDeleted).Any())
            {
                validationErrors.Add(DomainTexts.EvalSession_ValidateEvalSessionDistributionProject_ActiveEvaluationOfThisTypeExists);
            }

            return validationErrors;
        }

        public IList<string> CanCreateEvalSessionDistribution(Procedure procedure, ProcedureEvalTableType evalTableType)
        {
            var errors = new List<string>();

            if (!procedure.ProcedureEvalTables.Where(p => p.Type == evalTableType && p.IsActivated && p.IsActive).Any())
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionDistribution_NoActiveProcedureEvalTableType);
            }

            return errors;
        }

        #endregion //EvalSessionDistribution

        #region EvalSessionEvaluation

        public EvalSessionEvaluation FindEvalSessionEvaluation(int evaluationId)
        {
            var evalSessionEvaluation = this.EvalSessionEvaluations.Where(e => e.EvalSessionEvaluationId == evaluationId).SingleOrDefault();

            if (evalSessionEvaluation == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionEvaluation with id " + evaluationId);
            }

            return evalSessionEvaluation;
        }

        public EvalSessionEvaluation AddEvalSessionEvaluation(
            IList<EvalSessionSheetStatus> oldSheetStatuses,
            IList<EvalSessionSheetStatus> actualSheetStatuses,
            int[] sheetIds,
            int projectId,
            ProcedureEvalTableType evalTableType,
            EvalSessionEvaluationCalculationType calculationType,
            ProcedureEvalType evalType,
            bool evalIsPassed,
            decimal? evalPoints,
            string evalNote,
            ProjectRegistrationStatus projectStatus)
        {
            this.AssertIsActiveEvalSession();

            if (oldSheetStatuses.Count != actualSheetStatuses.Count)
            {
                throw new DomainException("EvalSessionSheet has been added");
            }
            else if (actualSheetStatuses.Except(oldSheetStatuses).Count() != 0)
            {
                throw new DomainException("EvalSessionSheet status was changed");
            }

            if (projectStatus == ProjectRegistrationStatus.Withdrawn)
            {
                throw new DomainException("Cannot add an EvalSessionEvaluation when the ProjectStatus is 'Withdrawn'");
            }

            if (this.FindEvalSessionProject(projectId).IsDeleted)
            {
                throw new DomainException("Cannot add an EvalSessionEvaluation when the Project is canceled");
            }

            var sheets = this.EvalSessionSheets.Where(p => sheetIds.Contains(p.EvalSessionSheetId));

            if (sheets.Count() == 0 || (sheets.Count(p => p.Status == EvalSessionSheetStatus.Ended) == 0) || (sheets.Count(p => p.Status == EvalSessionSheetStatus.Draft) > 0))
            {
                throw new DomainException("Cannot add EvalSessionEvaluation when there are no EvalSessionSheets or there is/are no EvalSessionSheet/s with status 'Ended' and some with status 'Draft'");
            }

            if (sheets.First().EvalTableType != evalTableType)
            {
                throw new DomainException("Type missmatch between EvalSessionSheets type and EvalSessionEvaluation type");
            }

            if (this.EvalSessionEvaluations.Where(p => p.ProjectId == projectId && p.EvalTableType == evalTableType && p.IsDeleted == false).Any())
            {
                throw new DomainException("Cannot have two not canceled EvalSessionEvaluations at the same time");
            }

            EvalSessionEvaluation evalSessionEvaluation = new EvalSessionEvaluation()
            {
                EvalSessionId = this.EvalSessionId,
                ProjectId = projectId,
                EvalTableType = evalTableType,
                CalculationType = calculationType,
                EvalType = evalType,
                EvalIsPassed = evalIsPassed,
                EvalPoints = evalPoints,
                EvalNote = evalNote,
                IsDeleted = false,
                CreateDate = DateTime.Now,
            };

            this.EvalSessionEvaluations.Add(evalSessionEvaluation);

            this.ModifyDate = DateTime.Now;

            return evalSessionEvaluation;
        }

        public void RemoveEvalSessionEvaluation(int evalSessionEvaluationId, string isDeletedNote)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionEvaluation = this.FindEvalSessionEvaluation(evalSessionEvaluationId);

            if (evalSessionEvaluation.IsDeleted)
            {
                throw new DomainException("Cannot delete an EvalSessionEvaluation twice");
            }

            var evalSessionProjectStandings = this.EvalSessionProjectStandings.Where(p => p.ProjectId == evalSessionEvaluation.ProjectId && p.IsDeleted == false).ToList();

            if (evalSessionProjectStandings.Any())
            {
                switch (evalSessionEvaluation.EvalTableType)
                {
                    case ProcedureEvalTableType.AdminAdmiss:
                    case ProcedureEvalTableType.TechFinance:
                    case ProcedureEvalTableType.Complex:
                        if (evalSessionProjectStandings.Any(e => !e.IsPreliminary))
                        {
                            throw new DomainException("Cannot delete an EvalSessionEvaluation when there is a non-canceled EvalSessionProjectStanding");
                        }

                        break;
                    default:
                        break;
                }
            }

            evalSessionEvaluation.IsDeleted = true;
            evalSessionEvaluation.IsDeletedNote = isDeletedNote;

            this.ModifyDate = DateTime.Now;
        }

        public void AddEvalSessionEvaluationSheet(EvalSessionEvaluation evalSessionEvaluation, int evalSessionSheetId)
        {
            this.AssertIsActiveEvalSession();

            evalSessionEvaluation.EvalSessionEvaluationSheets.Add(new EvalSessionEvaluationSheet
            {
                EvalSessionId = this.EvalSessionId,
                EvalSessionSheetId = evalSessionSheetId,
            });

            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanCreateEvalSessionEvaluation(Project project, ProcedureEvalTableType evalTableType)
        {
            List<string> errors = new List<string>();

            if (project.RegistrationStatus == ProjectRegistrationStatus.Withdrawn)
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionEvaluation_ProjectIsWithdrawn);
            }
            else if (this.EvalSessionProjects.Where(p => p.ProjectId == project.ProjectId).Single().IsDeleted == true)
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionEvaluation_ProjectIsDeleted);
            }
            else
            {
                if (this.EvalSessionEvaluations.Where(p => p.ProjectId == project.ProjectId && p.IsDeleted == false && p.EvalTableType == evalTableType).Any())
                {
                    errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionEvaluation_NonDeletedEvaluationExists);
                }

                var sheets = this.EvalSessionSheets.Where(p => p.ProjectId == project.ProjectId && p.EvalTableType == evalTableType).ToList();

                if (sheets.Where(p => p.Status == EvalSessionSheetStatus.Draft).Any())
                {
                    errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionEvaluation_HasDraftEvalSessionSheet);
                }

                if (!sheets.Where(p => p.Status == EvalSessionSheetStatus.Ended).Any())
                {
                    errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionEvaluation_AtLeastOneFinishedEvalSessionSheetIsRequired);
                }
            }

            return errors;
        }

        #endregion //EvalSessionEvaluation

        #region EvalSessionDocument

        public EvalSessionDocument FindEvalSessionDocument(int evalSessionDocumentId)
        {
            var evalSessionDocument = this.EvalSessionDocuments.Where(e => e.EvalSessionDocumentId == evalSessionDocumentId).SingleOrDefault();

            if (evalSessionDocument == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionDocument with id " + evalSessionDocumentId);
            }

            return evalSessionDocument;
        }

        public void AddEvalSessionDocument(
            string name,
            Guid blobKey,
            string description)
        {
            this.AssertEvalSessionIsActiveOrEndedByLAG();

            EvalSessionDocument evalSessionDocument = new EvalSessionDocument()
            {
                EvalSessionId = this.EvalSessionId,
                Name = name,
                BlobKey = blobKey,
                Description = description,
            };

            this.EvalSessionDocuments.Add(evalSessionDocument);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateEvalSessionDocument(
            int evalSessionDocumentId,
            string name,
            Guid blobKey,
            string description)
        {
            this.AssertEvalSessionIsActiveOrEndedByLAG();

            var evalSessionDocument = this.FindEvalSessionDocument(evalSessionDocumentId);

            evalSessionDocument.SetAttributes(name, blobKey, description);

            this.ModifyDate = DateTime.Now;
        }

        public void CancelEvalSessionDocument(int evalSessionDocumentId, string isDeletedNote)
        {
            this.AssertEvalSessionIsActiveOrEndedByLAG();

            var evalSessionDocument = this.FindEvalSessionDocument(evalSessionDocumentId);

            evalSessionDocument.Remove(isDeletedNote);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //EvalSessionDocument

        #region EvalSessionProjectStanding

        public EvalSessionProjectStanding FindEvalSessionProjectStanding(int projectStandingId)
        {
            var evalSessionProjectStanding = this.EvalSessionProjectStandings.Where(e => e.EvalSessionProjectStandingId == projectStandingId).SingleOrDefault();

            if (evalSessionProjectStanding == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionProjectStanding with id " + projectStandingId);
            }

            return evalSessionProjectStanding;
        }

        public EvalSessionProjectStanding AddEvalSessionProjectStanding(
            int projectId,
            bool isPreliminary,
            decimal? actualGrandAmount,
            int? orderNum,
            EvalSessionProjectStandingStatus status,
            decimal? grandAmount,
            string notes,
            bool hasProjectCommunicationInProgress,
            int projectVersionXmlId,
            int? rejectionReasonId,
            bool hasPreliminaryEvalTable,
            int? evalSessionStandingId = null)
        {
            this.AssertIsActiveEvalSession();

            if (actualGrandAmount != grandAmount)
            {
                throw new DomainObjectNotFoundException("Project grandAmount has been changed");
            }

            if (orderNum.HasValue && status != EvalSessionProjectStandingStatus.Approved && status != EvalSessionProjectStandingStatus.Reserve)
            {
                throw new DomainObjectNotFoundException("EvalSessionProjectStanding can have orderNum only if it is in 'Approved' or 'Reserve' status");
            }

            if (!orderNum.HasValue && (status == EvalSessionProjectStandingStatus.Approved || status == EvalSessionProjectStandingStatus.Reserve))
            {
                throw new DomainObjectNotFoundException("EvalSessionProjectStanding must have orderNum when the status is 'Approved' or 'Reserve'");
            }

            if (orderNum.HasValue && orderNum.Value < 1)
            {
                throw new DomainObjectNotFoundException("OrderNum of EvalSessionProjectStanding cannot be a number smaller than 1");
            }

            if (grandAmount.HasValue && grandAmount.Value < 0)
            {
                throw new DomainObjectNotFoundException("GrandAmount of EvalSessionProjectStanding cannot be a number smaller than 0");
            }

            if (this.CanCreateEvalSessionProjectStanding(projectId, isPreliminary, hasProjectCommunicationInProgress, true, hasPreliminaryEvalTable).Count != 0)
            {
                throw new DomainObjectNotFoundException("Cannot create EvalSessionProjectStanding due to validation errors");
            }

            EvalSessionProjectStanding evalSessionProjectStanding = new EvalSessionProjectStanding()
            {
                EvalSessionId = this.EvalSessionId,
                ProjectId = projectId,
                IsPreliminary = isPreliminary,
                OrderNum = orderNum,
                ManualOrderNum = orderNum,
                Status = status,
                ManualStatus = status,
                GrandAmount = (status == EvalSessionProjectStandingStatus.Approved || status == EvalSessionProjectStandingStatus.Reserve) ? grandAmount : null,
                Notes = notes,
                CreateDate = DateTime.Now,
                ProjectVersionXmlId = projectVersionXmlId,
                EvalSessionStandingId = evalSessionStandingId,
                RejectionReasonId = rejectionReasonId,
            };

            this.EvalSessionProjectStandings.Add(evalSessionProjectStanding);

            this.ModifyDate = DateTime.Now;

            return evalSessionProjectStanding;
        }

        public void RemoveEvalSessionProjectStanding(int projectStandingId, string isDeletedNote)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionProjectStanding = this.FindEvalSessionProjectStanding(projectStandingId);

            if (evalSessionProjectStanding.IsDeleted)
            {
                throw new DomainObjectNotFoundException("Cannot delete EvalSessionProjectStanding twice");
            }

            evalSessionProjectStanding.IsDeleted = true;
            evalSessionProjectStanding.IsDeletedNote = isDeletedNote;

            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanCreateEvalSessionProjectStanding(
            int projectId,
            bool isPreliminary,
            bool hasProjectCommunicationInProgress,
            bool hasProjectVersion,
            bool hasPreliminaryEvalTable)
        {
            List<string> errors = new List<string>();

            if (this.EvalSessionProjectStandings
                .Where(p =>
                    p.ProjectId == projectId &&
                    p.IsPreliminary == isPreliminary &&
                    p.IsDeleted == false)
                .Any())
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionProjectStanding_ActiveStandingExists);
            }

            if (!isPreliminary &&
                hasPreliminaryEvalTable &&
                !this.EvalSessionProjectStandings
                    .Where(p =>
                        p.ProjectId == projectId &&
                        p.IsPreliminary == true &&
                        p.IsDeleted == false)
                    .Any())
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionProjectStanding_ProjectDoesNotHavePreliminaryStanding);
            }

            if (!hasProjectVersion)
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionProjectStanding_ProjectDoesNotHaveActualVersion);
            }

            if (hasProjectCommunicationInProgress)
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionProjectStanding_ProjectHasCommunicationInProgress);
            }

            return errors;
        }

        public void AddEvalSessionProjectStandingEvaluation(EvalSessionProjectStanding evalSessionProjectStanding, int evalSessionEvaluationId)
        {
            this.AssertIsActiveEvalSession();

            evalSessionProjectStanding.EvalSessionProjectStandingEvaluations.Add(new EvalSessionProjectStandingEvaluation
            {
                EvalSessionId = this.EvalSessionId,
                EvalSessionEvaluationId = evalSessionEvaluationId,
            });

            this.ModifyDate = DateTime.Now;
        }

        #endregion //EvalSessionProjectStanding

        #region EvalSessionResult

        public EvalSessionResult FindEvalSessionResult(int resultId)
        {
            var evalSessionResult = this.EvalSessionResults.Where(e => e.EvalSessionResultId == resultId).SingleOrDefault();

            if (evalSessionResult == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionAdminAdmissResult with id " + resultId);
            }

            return evalSessionResult;
        }

        public int GetEvalSessionResultNextOrderNum(EvalSessionResultType type)
        {
            var lastOrderNumber = this.EvalSessionResults
                .Where(t => t.Type == type)
                .Max(p => (int?)p.OrderNum);

            return lastOrderNumber.HasValue ? lastOrderNumber.Value + 1 : 1;
        }

        public void ChangeEvalSessionResultStatusToPublish(int evalSessionResultId, int userId)
        {
            var evalSessionResult = this.FindEvalSessionResult(evalSessionResultId);

            if (this.EvalSessionStatus == EvalSessionStatus.EndedByLAG)
            {
                throw new DomainException("EvalSessionAdminAdmissResult transition status to publish is not allowed");
            }

            if (evalSessionResult.Status != EvalSessionResultStatus.Draft ||
                this.EvalSessionResults.Where(x => x.Status == EvalSessionResultStatus.Draft && x.Type == evalSessionResult.Type).Count() > 1)
            {
                throw new DomainException("EvalSessionAdminAdmissResult transition status to publish is not allowed");
            }

            EvalSessionResult oldEvalSessionResult = this.EvalSessionResults.Where(x => x.Status == EvalSessionResultStatus.Published && x.Type == evalSessionResult.Type).FirstOrDefault();
            if (oldEvalSessionResult != null)
            {
                oldEvalSessionResult.Status = EvalSessionResultStatus.Archived;
                oldEvalSessionResult.ModifyDate = DateTime.Now;
            }

            var modifyDate = DateTime.Now;
            this.ModifyDate = modifyDate;
            evalSessionResult.Status = EvalSessionResultStatus.Published;
            evalSessionResult.ModifyDate = modifyDate;
            evalSessionResult.PublicationDate = modifyDate;
            evalSessionResult.PublicationUserId = userId;

            ((IEventEmitter)this).Events.Add(new EvalSessionPublishedEvent()
            {
                ProcedureId = this.ProcedureId,
                EvalSessionId = this.EvalSessionId,
                EvalSessionResultId = evalSessionResultId,
                EvalSessionResultType = evalSessionResult.Type,
            });
        }

        public void ChangeEvalSessionResultStatusToCancel(int adminAdmissResultId, string note)
        {
            var evalSessionResult = this.FindEvalSessionResult(adminAdmissResultId);

            if (this.EvalSessionStatus == EvalSessionStatus.EndedByLAG)
            {
                throw new DomainException("EvalSessionAdminAdmissResult transition status to cancel is not allowed");
            }

            if (evalSessionResult.Status != EvalSessionResultStatus.Published)
            {
                throw new DomainException("EvalSessionAdminAdmissResult transition status to cancel is not allowed");
            }

            var modifyDate = DateTime.Now;
            this.ModifyDate = modifyDate;
            evalSessionResult.ModifyDate = modifyDate;
            evalSessionResult.Status = EvalSessionResultStatus.Canceled;
            evalSessionResult.StatusNote = note;
        }

        public List<string> EvalSessionResultResultCanPublish(int adminAdmissResultId)
        {
            var errors = new List<string>();

            var adminAdmissResult = this.FindEvalSessionResult(adminAdmissResultId);
            if (adminAdmissResult.Status != EvalSessionResultStatus.Draft)
            {
                errors.Add(DomainTexts.EvalSession_AdminAdmissResult_StatusIsntDraft);
            }

            if (!adminAdmissResult.Projects.Any())
            {
                errors.Add(DomainTexts.EvalSession_AdminAdmissResult_MissingProjects);
            }

            return errors;
        }

        public List<string> CanCreateResult(EvalSessionResultType type)
        {
            var errors = new List<string>();

            if (this.EvalSessionStatus == EvalSessionStatus.EndedByLAG)
            {
                throw new DomainException("Cannot add result to EvalSession that is in status 'EndedByLAG'");
            }

            if (this.EvalSessionResults.Where(x => x.Status == EvalSessionResultStatus.Draft && x.Type == type).Any())
            {
                errors.Add(string.Format(DomainTexts.EvalSession_AdminAdmissResult_StatusDraftExists, EvalSessionResultStatus.Draft.GetEnumDescription(), type.GetEnumDescription()));
            }

            return errors;
        }

        #endregion //EvalSessionResult

        #region EvalSessionStanding

        public EvalSessionStanding FindEvalSessionStanding(int evalSessionStandingId)
        {
            var evalSessionStanding = this.EvalSessionStandings.Where(e => e.EvalSessionStandingId == evalSessionStandingId).SingleOrDefault();

            if (evalSessionStanding == null)
            {
                throw new DomainObjectNotFoundException("Cannot find EvalSessionStanding with id " + evalSessionStandingId);
            }

            return evalSessionStanding;
        }

        public EvalSessionStanding AddEvalSessionStanding(
            string code,
            NewEvalSessionStandingType type,
            int? preliminaryBudgetPercentage,
            IList<EvalSessionStandingProjectDO> projects,
            bool hasProjectCommunicationInProgress,
            bool noProjectVersion,
            bool hasPreliminaryEvalTable)
        {
            this.AssertIsActiveEvalSession();

            var errors = this.CanCreateEvalSessionStanding(
                projects,
                type,
                hasProjectCommunicationInProgress,
                noProjectVersion,
                hasPreliminaryEvalTable);

            if (errors.Count != 0)
            {
                throw new DomainObjectNotFoundException("Cannot add EvalSessionStanding because there are validation errors");
            }

            EvalSessionStanding evalSessionStanding = new EvalSessionStanding()
            {
                EvalSessionId = this.EvalSessionId,
                Code = code,
                IsPreliminary = type == NewEvalSessionStandingType.Preliminary,
                PreliminaryBudgetPercentage = preliminaryBudgetPercentage,
                Status = EvalSessionStandingStatus.Applied,
                StatusDate = DateTime.Now,
            };

            this.EvalSessionStandings.Add(evalSessionStanding);

            return evalSessionStanding;
        }

        public void GenerateProjectStandings(
            EvalSessionStanding evalSessionStanding,
            NewEvalSessionStandingType type,
            Dictionary<int, bool> projectsHaveCommunicationInProgress,
            Dictionary<int, int> projectsVersions,
            IList<EvalSessionStandingProjectDO> projects,
            Dictionary<int, Dictionary<string, decimal>> projectsAmounts,
            Dictionary<string, decimal> procedureAmounts,
            bool hasPreliminaryEvalTable)
        {
            Func<EvalSessionStandingProjectDO, bool> isPassingCheck;
            IComparer<EvalSessionStandingProjectDO> passedComparer;

            if (type == NewEvalSessionStandingType.Complex)
            {
                isPassingCheck = t => (!hasPreliminaryEvalTable || (t.IsApprovedInPreliminaryStanding == true)) && t.IsPassedComplex == true;
                passedComparer = new ComplexStandingProjectComparer();
            }
            else if (type == NewEvalSessionStandingType.TwoStep)
            {
                isPassingCheck = t => (!hasPreliminaryEvalTable || (t.IsApprovedInPreliminaryStanding == true)) && t.IsPassedASD == true && t.IsPassedTFO == true;
                passedComparer = new TwoStepStandingProjectComparer();
            }
            else if (type == NewEvalSessionStandingType.Preliminary)
            {
                isPassingCheck = t => t.IsPassedPreliminary == true;
                passedComparer = new PreliminaryStandingProjectComparer();
            }
            else
            {
                throw new DomainException($"Unexpected {nameof(type)}.");
            }

            var passedProjects = projects.Where(isPassingCheck).OrderByDescending(t => t, passedComparer);
            var notPassedProjects = projects.Where(t => !isPassingCheck(t));

            Func<int, bool> tryDeduct = (projectId) =>
            {
                var projectAmounts = projectsAmounts[projectId];
                foreach (var amount in projectAmounts)
                {
                    if (procedureAmounts[amount.Key] < amount.Value)
                    {
                        return false;
                    }
                }

                foreach (var amount in projectAmounts)
                {
                    procedureAmounts[amount.Key] = procedureAmounts[amount.Key] - amount.Value;
                }

                return true;
            };

            Func<int, decimal> getProjectGrandAmount = (projectId) =>
            {
                decimal grandAmount = 0;
                var projectAmounts = projectsAmounts[projectId];
                foreach (var amount in projectAmounts)
                {
                    grandAmount += amount.Value;
                }

                return grandAmount;
            };

            Action<int, int?, EvalSessionProjectStandingStatus, bool> addEvalSessionProjectStanding = (projectId, orderNum, status, hasGrandAmount) =>
            {
                decimal? grandAmount = null;
                if (hasGrandAmount)
                {
                    grandAmount = getProjectGrandAmount(projectId);
                }

                var esps = this.AddEvalSessionProjectStanding(
                    projectId,
                    type == NewEvalSessionStandingType.Preliminary,
                    grandAmount,
                    orderNum,
                    status,
                    grandAmount,
                    null,
                    projectsHaveCommunicationInProgress[projectId],
                    projectsVersions[projectId],
                    null,
                    hasPreliminaryEvalTable,
                    evalSessionStanding.EvalSessionStandingId);

                var evaluationIds = this.EvalSessionEvaluations.Where(t => t.ProjectId == esps.ProjectId).Select(t => t.EvalSessionEvaluationId).ToList();
                foreach (var evaluationId in evaluationIds)
                {
                    this.AddEvalSessionProjectStandingEvaluation(esps, evaluationId);
                }
            };

            foreach (var p in projects)
            {
                evalSessionStanding.Projects.Add(new EvalSessionStandingProject
                {
                    EvalSessionId = this.EvalSessionId,
                    ProjectId = p.ProjectId,
                });
            }

            int orderNumber = 1;
            bool tryDeductProcedureBudgetFailedOnce = false;
            decimal? lastApprovedPoints = null;

            foreach (var pp in passedProjects)
            {
                if (tryDeduct(pp.ProjectId) && !tryDeductProcedureBudgetFailedOnce)
                {
                    addEvalSessionProjectStanding(pp.ProjectId, orderNumber, EvalSessionProjectStandingStatus.Approved, true);
                    orderNumber++;
                    if (type == NewEvalSessionStandingType.Preliminary)
                    {
                        lastApprovedPoints = pp.PointsPreliminary;
                    }
                }
                else
                {
                    tryDeductProcedureBudgetFailedOnce = true;
                    if (type == NewEvalSessionStandingType.Preliminary)
                    {
                        if (lastApprovedPoints.HasValue && lastApprovedPoints == pp.PointsPreliminary)
                        {
                            addEvalSessionProjectStanding(pp.ProjectId, orderNumber, EvalSessionProjectStandingStatus.Reserve, true);
                            orderNumber++;
                        }
                        else
                        {
                            addEvalSessionProjectStanding(pp.ProjectId, null, EvalSessionProjectStandingStatus.Rejected, false);
                        }
                    }
                    else
                    {
                        addEvalSessionProjectStanding(pp.ProjectId, orderNumber, EvalSessionProjectStandingStatus.Reserve, true);
                        orderNumber++;
                    }
                }
            }

            foreach (var npp in notPassedProjects)
            {
                if (type == NewEvalSessionStandingType.Complex)
                {
                    if (npp.IsApprovedInPreliminaryStanding == false)
                    {
                        addEvalSessionProjectStanding(npp.ProjectId, null, EvalSessionProjectStandingStatus.RejectedAtPreliminary, false);
                    }
                    else if (npp.IsPassedComplex == false)
                    {
                        addEvalSessionProjectStanding(npp.ProjectId, null, EvalSessionProjectStandingStatus.Rejected, false);
                    }
                    else
                    {
                        throw new DomainException("A project at this step must have one of the following: 'IsPassedPreliminary' or 'IsPassedComplex'");
                    }
                }
                else if (type == NewEvalSessionStandingType.TwoStep)
                {
                    if (npp.IsApprovedInPreliminaryStanding == false)
                    {
                        addEvalSessionProjectStanding(npp.ProjectId, null, EvalSessionProjectStandingStatus.RejectedAtPreliminary, false);
                    }
                    else if (npp.IsPassedASD == false)
                    {
                        addEvalSessionProjectStanding(npp.ProjectId, null, EvalSessionProjectStandingStatus.RejectedAtASD, false);
                    }
                    else if (npp.IsPassedTFO == false)
                    {
                        addEvalSessionProjectStanding(npp.ProjectId, null, EvalSessionProjectStandingStatus.RejectedAtTFO, false);
                    }
                    else
                    {
                        throw new DomainException("A project at this step must have one of the following: 'IsPassedPreliminary', 'IsPassedAsd' or 'IsPassedTFO'");
                    }
                }
                else if (type == NewEvalSessionStandingType.Preliminary)
                {
                    addEvalSessionProjectStanding(npp.ProjectId, null, EvalSessionProjectStandingStatus.Rejected, false);
                }
                else
                {
                    throw new DomainException($"Unexpected {nameof(type)}.");
                }
            }
        }

        public void RefuseEvalSessionStanding(int evalSessionStandingId, string statusNote)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionStanding = this.FindEvalSessionStanding(evalSessionStandingId);

            if (evalSessionStanding.Status != EvalSessionStandingStatus.Applied && evalSessionStanding.Status != EvalSessionStandingStatus.AppliedWithChanges)
            {
                throw new DomainValidationException("EvalSessionStanding status transition not allowed");
            }

            var evalSessionProjectStandings = this.EvalSessionProjectStandings.Where(t => t.EvalSessionStandingId == evalSessionStandingId).ToList();
            foreach (var projectStanding in evalSessionProjectStandings)
            {
                projectStanding.IsDeleted = true;
                if (string.IsNullOrEmpty(projectStanding.IsDeletedNote))
                {
                    projectStanding.IsDeletedNote = statusNote;
                }
            }

            evalSessionStanding.StatusNote = statusNote;
            evalSessionStanding.Status = EvalSessionStandingStatus.Refused;
            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanApplyArrangedEvalSessionStanding(int evalSessionStandingId, Dictionary<string, decimal> procedureBudgetLeft, Dictionary<int, Dictionary<string, decimal>> projectsAmounts)
        {
            List<string> errors = new List<string>();

            Func<int, bool> tryDeduct = (projectId) =>
            {
                var projectAmounts = projectsAmounts[projectId];
                foreach (var amount in projectAmounts)
                {
                    if (procedureBudgetLeft[amount.Key] < amount.Value)
                    {
                        return false;
                    }
                }

                foreach (var amount in projectAmounts)
                {
                    procedureBudgetLeft[amount.Key] = procedureBudgetLeft[amount.Key] - amount.Value;
                }

                return true;
            };

            var allProjects = this.EvalSessionProjectStandings
                .Where(x => x.EvalSessionStandingId == evalSessionStandingId)
                .OrderBy(x => x.ManualOrderNum);

            var tryDeductFailedOnce = false;

            foreach (var project in allProjects)
            {
                if (project.ManualStatus != EvalSessionProjectStandingStatus.Approved && project.ManualStatus != EvalSessionProjectStandingStatus.Reserve)
                {
                    continue;
                }

                if (tryDeduct(project.ProjectId) && !tryDeductFailedOnce)
                {
                    if (project.ManualStatus != EvalSessionProjectStandingStatus.Approved)
                    {
                        errors.Add(DomainTexts.EvalSession_CanApplyArrangedStanding_BudgetChanged);
                        return errors;
                    }
                }
                else
                {
                    if (project.ManualStatus != EvalSessionProjectStandingStatus.Reserve)
                    {
                        errors.Add(DomainTexts.EvalSession_CanApplyArrangedStanding_BudgetChanged);
                        return errors;
                    }
                }
            }

            return errors;
        }

        public IEnumerable<EvalSessionProjectStanding> ApplyArrangedEvalSessionStanding(int evalSessionStandingId)
        {
            this.AssertIsActiveEvalSession();

            var evalSessionStanding = this.FindEvalSessionStanding(evalSessionStandingId);

            if (evalSessionStanding.Status != EvalSessionStandingStatus.Applied)
            {
                throw new DomainValidationException("EvalSessionStanding status transition not allowed");
            }

            var projects = this.EvalSessionProjectStandings.Where(x => x.EvalSessionStandingId == evalSessionStandingId);

            foreach (var project in projects)
            {
                project.OrderNum = project.ManualOrderNum;
                project.Status = project.ManualStatus;
            }

            evalSessionStanding.Status = EvalSessionStandingStatus.AppliedWithChanges;
            this.ModifyDate = DateTime.Now;

            return projects;
        }

        public List<string> CanCreateEvalSessionStanding(
            IList<EvalSessionStandingProjectDO> projects,
            NewEvalSessionStandingType type,
            bool hasProjectCommunicationInProgress,
            bool noProjectVersion,
            bool hasPreliminaryEvalTable)
        {
            List<string> errors = new List<string>();

            if (projects.Count == 0)
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_NoProjectsInStanding);
            }

            if (this.EvalSessionProjectStandings.Where(t => !t.IsDeleted && t.IsPreliminary == (type == NewEvalSessionStandingType.Preliminary)).Any())
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_ProjectWithActiveStandingExists);
            }

            if (hasProjectCommunicationInProgress)
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_ProjectHasCommunicationInProgress);
            }

            if (noProjectVersion)
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomeProjectsHaveNoActualVersion);
            }

            Func<ProcedureEvalTableType, bool> isStandingEvaluation;
            switch (type)
            {
                case NewEvalSessionStandingType.Complex:
                    isStandingEvaluation = (evalTableType) => evalTableType == ProcedureEvalTableType.Complex;
                    break;
                case NewEvalSessionStandingType.TwoStep:
                    isStandingEvaluation = (evalTableType) =>
                        evalTableType == ProcedureEvalTableType.AdminAdmiss ||
                        evalTableType == ProcedureEvalTableType.TechFinance;
                    break;
                default:
                    throw new DomainException($"Unexpected {nameof(type)}");
            }

            var evaluations = this.EvalSessionEvaluations.Where(t => t.IsDeleted == false && isStandingEvaluation(t.EvalTableType)).ToList();

            if (evaluations.Any())
            {
                if (type == NewEvalSessionStandingType.Complex)
                {
                    var eligibleProjects = projects.Where(t => !hasPreliminaryEvalTable || t.IsApprovedInPreliminaryStanding == true);

                    if (eligibleProjects.Where(t => !t.IsPassedComplex.HasValue).Count() != 0)
                    {
                        if (hasPreliminaryEvalTable)
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomePreliminaryPassedProjectsHaveNoComplexEval);
                        }
                        else
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomeProjectsHaveNoComplexEval);
                        }
                    }
                    else
                    {
                        if (!eligibleProjects.All(t => t.PointsComplex.HasValue) && eligibleProjects.Any(t => t.PointsComplex.HasValue))
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_ComplexPointsInconsistency);
                        }
                    }
                }
                else if (type == NewEvalSessionStandingType.Preliminary)
                {
                    if (projects.Any(p => !p.IsPassedPreliminary.HasValue))
                    {
                        errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomeProjectsHaveNoPreliminaryEval);
                    }
                }
                else if (type == NewEvalSessionStandingType.TwoStep)
                {
                    var eligibleProjects = projects.Where(t => !hasPreliminaryEvalTable || t.IsApprovedInPreliminaryStanding == true);

                    bool haveEvaluations = true;
                    if (eligibleProjects.Where(t => !t.IsPassedASD.HasValue).Count() != 0)
                    {
                        haveEvaluations = false;
                        if (hasPreliminaryEvalTable)
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomePreliminaryPassedProjectsHaveNoOASD);
                        }
                        else
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomeProjectsHaveNoOASD);
                        }
                    }

                    if (eligibleProjects.Where(t => t.IsPassedASD == true && !t.IsPassedTFO.HasValue).Count() != 0)
                    {
                        haveEvaluations = false;
                        if (hasPreliminaryEvalTable)
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomePreliminaryPassedProjectsHaveOASDButHaveNoTFO);
                        }
                        else
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomeProjectsHaveOASDButHaveNoTFO);
                        }
                    }

                    if (eligibleProjects.Where(t => (t.IsPassedASD == null || t.IsPassedASD == false) && t.IsPassedTFO == true).Count() != 0)
                    {
                        haveEvaluations = false;
                        if (hasPreliminaryEvalTable)
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomePreliminaryPassedProjectsHaveTFOButHaveNoOASD);
                        }
                        else
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_SomeProjectsHaveTFOButHaveNoOASD);
                        }
                    }

                    if (haveEvaluations)
                    {
                        if ((!eligibleProjects.All(t => t.PointsASD.HasValue) && eligibleProjects.Any(t => t.PointsASD.HasValue)) ||
                            (!eligibleProjects.Where(t => t.IsPassedASD == true).All(t => t.PointsTFO.HasValue)
                            && eligibleProjects.Where(t => t.IsPassedASD == true).Any(t => t.PointsTFO.HasValue)))
                        {
                            errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_TwoStepPointsInconsistency);
                        }
                    }
                }
                else
                {
                    throw new DomainException($"Unexpected {nameof(type)}");
                }
            }
            else
            {
                errors.Add(DomainTexts.EvalSession_CanCreateEvalSessionStanding_NoActiveEvals);
            }

            return errors;
        }

        public void SwitchEvalSessionStandingProjectsOrder(int evalSessionStandingId, int projectXId, int projectYId, Dictionary<string, decimal> procedureBudgetLeft, Dictionary<int, Dictionary<string, decimal>> projectsAmounts)
        {
            var project1 = this.EvalSessionProjectStandings.Where(x => x.ProjectId == projectXId && x.EvalSessionStandingId == evalSessionStandingId).Single();

            var project2 = this.EvalSessionProjectStandings.Where(x => x.ProjectId == projectYId && x.EvalSessionStandingId == evalSessionStandingId).Single();

            var tempOrderNum = project1.ManualOrderNum;

            project1.ManualOrderNum = project2.ManualOrderNum;

            project2.ManualOrderNum = tempOrderNum;

            Func<int, bool> tryDeduct = (projectId) =>
            {
                var projectAmounts = projectsAmounts[projectId];
                foreach (var amount in projectAmounts)
                {
                    if (procedureBudgetLeft[amount.Key] < amount.Value)
                    {
                        return false;
                    }
                }

                foreach (var amount in projectAmounts)
                {
                    procedureBudgetLeft[amount.Key] = procedureBudgetLeft[amount.Key] - amount.Value;
                }

                return true;
            };

            var allProjects = this.EvalSessionProjectStandings
                .Where(x => x.EvalSessionStandingId == evalSessionStandingId)
                .OrderBy(x => x.ManualOrderNum);

            var tryDeductFailedOnce = false;

            foreach (var project in allProjects)
            {
                if (project.Status != EvalSessionProjectStandingStatus.Approved && project.Status != EvalSessionProjectStandingStatus.Reserve)
                {
                    continue;
                }

                if (tryDeduct(project.ProjectId) && !tryDeductFailedOnce)
                {
                    project.ManualStatus = EvalSessionProjectStandingStatus.Approved;
                }
                else
                {
                    project.ManualStatus = EvalSessionProjectStandingStatus.Reserve;
                }
            }
        }

        private class ComplexStandingProjectComparer : Comparer<EvalSessionStandingProjectDO>
        {
            public override int Compare(EvalSessionStandingProjectDO x, EvalSessionStandingProjectDO y)
            {
                if (x.PointsComplex.HasValue != y.PointsComplex.HasValue)
                {
                    throw new DomainException("Two EvalSessionEvaluations of type Complex have difference by having or not having EvalPoints");
                }

                int result = 0;

                // Complex comparison
                if (!x.PointsComplex.HasValue && !y.PointsComplex.HasValue)
                {
                    result = 0;
                }
                else if (x.PointsComplex.Value != y.PointsComplex.Value)
                {
                    result = x.PointsComplex.Value > y.PointsComplex.Value ? 1 : -1;
                }

                if (result != 0)
                {
                    return result;
                }

                return 0 - x.ProjectRegNumber.CompareTo(y.ProjectRegNumber);
            }
        }

        private class TwoStepStandingProjectComparer : Comparer<EvalSessionStandingProjectDO>
        {
            public override int Compare(EvalSessionStandingProjectDO x, EvalSessionStandingProjectDO y)
            {
                if (x.PointsTFO.HasValue != y.PointsTFO.HasValue)
                {
                    throw new DomainException("Two EvalSessionEvaluations of type TFO have difference by having or not having EvalPoints");
                }

                if (x.PointsASD.HasValue != y.PointsASD.HasValue)
                {
                    throw new DomainException("Two EvalSessionEvaluations of type ASD have difference by having or not having EvalPoints");
                }

                int result = 0;

                // TFO comparison
                if (!x.PointsTFO.HasValue && !y.PointsTFO.HasValue)
                {
                    result = 0;
                }
                else if (x.PointsTFO.Value != y.PointsTFO.Value)
                {
                    result = x.PointsTFO.Value > y.PointsTFO.Value ? 1 : -1;
                }

                if (result != 0)
                {
                    return result;
                }

                // OASD comparison
                if (!x.PointsASD.HasValue && !y.PointsASD.HasValue)
                {
                    result = 0;
                }
                else if (x.PointsASD.Value != y.PointsASD.Value)
                {
                    result = x.PointsASD.Value > y.PointsASD.Value ? 1 : -1;
                }

                if (result != 0)
                {
                    return result;
                }

                return 0 - x.ProjectRegNumber.CompareTo(y.ProjectRegNumber);
            }
        }

        private class PreliminaryStandingProjectComparer : Comparer<EvalSessionStandingProjectDO>
        {
            public override int Compare(EvalSessionStandingProjectDO x, EvalSessionStandingProjectDO y)
            {
                if (x.PointsPreliminary.HasValue != y.PointsPreliminary.HasValue)
                {
                    throw new DomainException("Two EvalSessionEvaluations of type Preliminary have difference by having or not having EvalPoints");
                }

                int result = 0;

                // Complex comparison
                if (!x.PointsPreliminary.HasValue && !y.PointsPreliminary.HasValue)
                {
                    result = 0;
                }
                else if (x.PointsPreliminary.Value != y.PointsPreliminary.Value)
                {
                    result = x.PointsPreliminary.Value > y.PointsPreliminary.Value ? 1 : -1;
                }

                if (result != 0)
                {
                    return result;
                }

                return 0 - x.ProjectRegNumber.CompareTo(y.ProjectRegNumber);
            }
        }

        #endregion //EvalSessionStanding
    }
}
