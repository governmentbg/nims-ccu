using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Domain.EvalSessions;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;

namespace Eumis.Domain.Projects
{
    public partial class Project : IAggregateRoot
    {
        #region Project

        public void UpdateProjectData(
            string name,
            string companyName,
            int companyTypeId,
            int companyLegalTypeId,
            string companyEmail,
            int? companySeatCountryId,
            int? companySeatSettlementId,
            string companySeatPostCode,
            string companySeatStreet,
            string companySeatAddress,
            int? companyCorrespondenceCountryId,
            int? companyCorrespondenceSettlementId,
            string companyCorrespondencePostCode,
            string companyCorrespondenceStreet,
            string companyCorrespondenceAddress,
            int? kidCodeId,
            int? duration,
            string nutsAddressFullPath,
            string nutsAddressFullPathName,
            decimal? totalBfpAmount,
            decimal? coFinancingAmount)
        {
            this.Name = name;
            this.CompanyName = companyName;
            this.CompanyTypeId = companyTypeId;
            this.CompanyLegalTypeId = companyLegalTypeId;
            this.CompanyEmail = companyEmail;
            this.CompanySeatCountryId = companySeatCountryId;
            this.CompanySeatSettlementId = companySeatSettlementId;
            this.CompanySeatPostCode = companySeatPostCode;
            this.CompanySeatStreet = companySeatStreet;
            this.CompanySeatAddress = companySeatAddress;
            this.CompanyCorrespondenceCountryId = companyCorrespondenceCountryId;
            this.CompanyCorrespondenceSettlementId = companyCorrespondenceSettlementId;
            this.CompanyCorrespondencePostCode = companyCorrespondencePostCode;
            this.CompanyCorrespondenceStreet = companyCorrespondenceStreet;
            this.CompanyCorrespondenceAddress = companyCorrespondenceAddress;
            this.KidCodeId = kidCodeId;
            this.Duration = duration;
            this.NutsAddressFullPath = nutsAddressFullPath;
            this.NutsAddressFullPathName = nutsAddressFullPathName;
            this.TotalBfpAmount = totalBfpAmount;
            this.CoFinancingAmount = coFinancingAmount;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateAttributes(
            string name,
            ProjectRegistrationStatus registrationStatus,
            ProjectRecieveType recieveType,
            DateTime recieveDate,
            DateTime submitDate,
            string storagePlace,
            int? originals,
            int? copies,
            string notes)
        {
            this.AssertIsNotElectronicProject();

            this.AssertIsNotWithdrawnProject();

            if (registrationStatus == ProjectRegistrationStatus.Withdrawn)
            {
                throw new DomainValidationException("Cannot change a Project status to 'Withdrawn' when updating a project");
            }

            this.Name = name;
            this.RegistrationStatus = registrationStatus;
            this.RecieveType = recieveType;
            this.RecieveDate = recieveDate;
            this.SubmitDate = submitDate;
            this.StoragePlace = storagePlace;
            this.Originals = originals;
            this.Copies = copies;
            this.Notes = notes;

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateEvalStatus(ProjectEvalStatus status)
        {
            if (status == ProjectEvalStatus.Contracted && this.EvalStatus != ProjectEvalStatus.Contracted)
            {
                ((IEventEmitter)this).Events.Add(new ProjectEvalStatusChangedEvent()
                {
                    ProjectId = this.ProjectId,
                    ProcedureId = this.ProcedureId,
                });
            }

            this.EvalStatus = status;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsNotElectronicProject()
        {
            if (this.RecieveType == ProjectRecieveType.Electronic)
            {
                throw new DomainValidationException("Cannot edit Project that has RecieveType 'Electronic'");
            }
        }

        private void AssertIsNotWithdrawnProject()
        {
            if (this.RegistrationStatus == ProjectRegistrationStatus.Withdrawn)
            {
                throw new DomainValidationException("Cannot edit Project that has ProjectRegistrationStatus 'Withdrawn'");
            }
        }

        public void Withdraw(IList<EvalSession> evalSessions)
        {
            if (evalSessions.Any())
            {
                var evalSession = evalSessions.Where(e => e.ProcedureId == this.ProcedureId && e.EvalSessionStatus != EvalSessionStatus.Canceled && e.EvalSessionProjects.Where(p => p.ProjectId == this.ProjectId && p.IsDeleted == false).Any()).SingleOrDefault();

                if (evalSession != null)
                {
                    if (evalSession.EvalSessionSheets.Where(p => p.ProjectId == this.ProjectId && p.Status != EvalSessionSheetStatus.Canceled).Any())
                    {
                        throw new DomainValidationException("Cannot withdraw Project that is included in a non-canceled EvalSession and has non-canceled sheets");
                    }

                    if (evalSession.EvalSessionEvaluations.Where(p => p.ProjectId == this.ProjectId && p.IsDeleted == false).Any())
                    {
                        throw new DomainValidationException("Cannot withdraw Project that is included in a non-canceled EvalSession and has non-canceled evaluation");
                    }
                }
            }

            this.RegistrationStatus = ProjectRegistrationStatus.Withdrawn;
            this.ModifyDate = DateTime.Now;
        }

        #endregion Project

        #region MonitorstatRequests

        public ProjectMonitorstatRequest CreateMonitorstatRequest(
            int procedureMonitorstatRequestId,
            int projectVersionXmlId,
            int? projectVersionXmlFileId,
            int? programmeDeclarationId,
            Guid? declarationBlobKey,
            string companyUin,
            UinType companyUinType)
        {
            var projectMonitorstatRequest = new ProjectMonitorstatRequest(
                procedureMonitorstatRequestId,
                projectVersionXmlId,
                projectVersionXmlFileId,
                declarationBlobKey,
                programmeDeclarationId,
                companyUin,
                companyUinType);

            this.MonitorstatRequests.Add(projectMonitorstatRequest);
            this.ModifyDate = DateTime.Now;

            return projectMonitorstatRequest;
        }

        public void UpdateMonitorstatRequest(
            int projectMonitorstatRequestId,
            int procedureMonitorstatRequestId,
            int? projectVersionXmlFileId,
            int? programmeDeclarationId,
            Guid? declarationBlobKey)
        {
            if (projectVersionXmlFileId == null && declarationBlobKey == null)
            {
                throw new DomainValidationException($"One of the {nameof(projectVersionXmlFileId)} or {nameof(declarationBlobKey)} must not be null");
            }

            var projectMonitorstatRequest = this.MonitorstatRequests
                .Where(x => x.ProjectMonitorstatRequestId == projectMonitorstatRequestId)
                .Single();

            var currentDate = DateTime.Now;

            projectMonitorstatRequest.ProcedureMonitorstatRequestId = procedureMonitorstatRequestId;
            projectMonitorstatRequest.ProjectVersionXmlFileId = projectVersionXmlFileId;
            projectMonitorstatRequest.ProgrammeDeclarationId = programmeDeclarationId;
            projectMonitorstatRequest.DeclarationBlobKey = declarationBlobKey;
            projectMonitorstatRequest.ModifyDate = currentDate;

            this.ModifyDate = currentDate;
        }

        public void RemoveMonitorstatRequest(int projectMonitorstatRequestId)
        {
            var request = this.MonitorstatRequests
                .Where(x => x.ProjectMonitorstatRequestId == projectMonitorstatRequestId)
                .Single();

            request.AssertSendIsAllowed();

            this.MonitorstatRequests.Remove(request);
        }

        #endregion
    }
}
