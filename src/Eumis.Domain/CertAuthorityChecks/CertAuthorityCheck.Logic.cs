using System;
using System.Linq;

namespace Eumis.Domain.CertAuthorityChecks
{
    public partial class CertAuthorityCheck
    {
        #region CertAuthorityCheck

        public void UpdateCheckData(
            CertAuthorityCheckKind kind,
            CertAuthorityCheckType type,
            DateTime? dateFrom,
            DateTime? dateTo,
            CertAuthorityCheckSubjectType? subjectType,
            string subjectName,
            string team)
        {
            this.AssertIsDraft();

            this.Kind = kind;
            this.Type = type;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.SubjectType = subjectType;
            this.SubjectName = subjectName;
            this.Team = team;

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered(int? checkNum = null)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                this.IsActivated = true;
                this.CheckNum = checkNum.Value;
            }

            this.Status = CertAuthorityCheckStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = CertAuthorityCheckStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToRemoved(string deleteComment)
        {
            this.AssertIsDraft();

            if (!this.IsActivated)
            {
                throw new DomainValidationException("CertAuthority check must be activated!");
            }

            this.Status = CertAuthorityCheckStatus.Removed;
            this.DeleteNote = deleteComment;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != CertAuthorityCheckStatus.Draft)
            {
                throw new DomainValidationException("CertAuthority check status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != CertAuthorityCheckStatus.Entered)
            {
                throw new DomainValidationException("CertAuthority check status must be entered!");
            }
        }

        #endregion //CertAuthorityCheck

        #region CertAuthorityCheckAscertainment

        public CertAuthorityCheckAscertainment AddCertAuthorityCheckAscertainment(
            int nextOrderNum,
            CertAuthorityCheckAscertainmentType type,
            string ascertainment,
            CertAuthorityCheckAscertainmentStatus status,
            string recommendation,
            DateTime? recommendationDeadline,
            CertAuthorityCheckAscertainmentExecutionStatus? recommendationExecutionStatus,
            string certAuthorityComment,
            string managingAuthorityComment)
        {
            this.AssertIsDraft();

            var newAscertainment = new CertAuthorityCheckAscertainment()
            {
                CertAuthorityCheckId = this.CertAuthorityCheckId,
                OrderNum = nextOrderNum,
                Type = type,
                Ascertainment = ascertainment,
                Status = status,
                Recommendation = recommendation,
                RecommendationDeadline = recommendationDeadline,
                RecommendationExecutionStatus = recommendationExecutionStatus,
                CertAuthorityComment = certAuthorityComment,
                ManagingAuthorityComment = managingAuthorityComment,
            };

            this.Ascertainments.Add(newAscertainment);

            this.ModifyDate = DateTime.Now;

            return newAscertainment;
        }

        public CertAuthorityCheckAscertainment GetCertAuthorityCheckAscertainment(int ascertainmentId)
        {
            var ascertainment = this.Ascertainments.Single(a => a.CertAuthorityCheckAscertainmentId == ascertainmentId);

            if (ascertainment == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CertAuthorityCheckAscertainment with id " + ascertainmentId);
            }

            return ascertainment;
        }

        public void UpdateCertAuthorityCheckAscertainment(
            int ascertainmentId,
            CertAuthorityCheckAscertainmentType type,
            string ascertainment,
            CertAuthorityCheckAscertainmentStatus status,
            string recommendation,
            DateTime? recommendationDeadline,
            CertAuthorityCheckAscertainmentExecutionStatus? recommendationExecutionStatus,
            string certAuthorityComment,
            string managingAuthorityComment)
        {
            this.AssertIsDraft();

            var certAuthorityCheckAscertainment = this.GetCertAuthorityCheckAscertainment(ascertainmentId);
            certAuthorityCheckAscertainment.SetAttributes(
                type,
                ascertainment,
                status,
                recommendation,
                recommendationDeadline,
                recommendationExecutionStatus,
                certAuthorityComment,
                managingAuthorityComment);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveCertAuthorityCheckAscertainment(int ascertainmentId)
        {
            this.AssertIsDraft();

            var ascertainment = this.GetCertAuthorityCheckAscertainment(ascertainmentId);
            this.Ascertainments.Remove(ascertainment);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //CertAuthorityCheckAscertainment

        #region CertAuthorityCheckLevelItem

        public void AddCertAuthorityCheckLevelItem(int itemId)
        {
            this.AssertIsDraft();

            var newItem = new CertAuthorityCheckLevelItem()
            {
                CertAuthorityCheckId = this.CertAuthorityCheckId,
                Level = this.Level,
            };

            switch (this.Level)
            {
                case CertAuthorityCheckLevel.Programme:
                    newItem.ProgrammeId = itemId;
                    break;
                case CertAuthorityCheckLevel.ProgrammePriority:
                    newItem.ProgrammePriorityId = itemId;
                    break;
                case CertAuthorityCheckLevel.Procedure:
                    newItem.ProcedureId = itemId;
                    break;
                case CertAuthorityCheckLevel.Contract:
                    newItem.ContractId = itemId;
                    break;
            }

            this.LevelItems.Add(newItem);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveCertAuthorityCheckLevelItem(int itemId)
        {
            this.AssertIsDraft();

            var item = this.LevelItems.Single(li => li.CertAuthorityCheckLevelItemId == itemId);
            this.LevelItems.Remove(item);

            this.ModifyDate = DateTime.Now;
        }
        #endregion //CertAuthorityCheckLevelItem

        #region CertAuthorityCheckDocument

        public CertAuthorityCheckDocument FindCertAuthorityCheckDocument(int documentId)
        {
            var cacd = this.CertAuthorityCheckDocuments.Where(d => d.CertAuthorityCheckDocumentId == documentId).SingleOrDefault();

            if (cacd == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CertAuthorityCheckDocument with id " + documentId);
            }

            return cacd;
        }

        public void UpdateCertAuthorityCheckDocument(
            int documentId,
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsDraft();

            var cacd = this.FindCertAuthorityCheckDocument(documentId);
            cacd.SetAttributes(
                name,
                description,
                blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public CertAuthorityCheckDocument AddCertAuthorityCheckDocument(
            string name,
            string description,
            Guid? blobKey)
        {
            this.AssertIsDraft();

            var newCertAuthorityCheckDocument = new CertAuthorityCheckDocument()
            {
                CertAuthorityCheckId = this.CertAuthorityCheckId,
                Name = name,
                Description = description,
                BlobKey = blobKey,
            };

            this.CertAuthorityCheckDocuments.Add(newCertAuthorityCheckDocument);

            this.ModifyDate = DateTime.Now;

            return newCertAuthorityCheckDocument;
        }

        public void RemoveCertAuthorityCheckDocument(int documentId)
        {
            this.AssertIsDraft();

            var cacd = this.FindCertAuthorityCheckDocument(documentId);
            this.CertAuthorityCheckDocuments.Remove(cacd);

            this.ModifyDate = DateTime.Now;
        }

        #endregion // CertAuthorityCheckDocument

        #region CertAuthorityCheckProject

        public CertAuthorityCheckProject FindCertAuthorityCheckProject(int projectId)
        {
            var cacp = this.Projects.Where(d => d.ProjectId == projectId).SingleOrDefault();

            if (cacp == null)
            {
                throw new DomainObjectNotFoundException("Cannot find CertAuthorityCheckProject with id " + projectId);
            }

            return cacp;
        }

        public void AddCertAuthorityCheckProject(int projectId)
        {
            this.AssertIsDraft();

            var newItem = new CertAuthorityCheckProject()
            {
                CertAuthorityCheckId = this.CertAuthorityCheckId,
                ProjectId = projectId,
            };

            this.Projects.Add(newItem);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveCertAuthorityCheckProject(int projectId)
        {
            this.AssertIsDraft();

            var cacp = this.FindCertAuthorityCheckProject(projectId);

            this.Projects.Remove(cacp);
            this.ModifyDate = DateTime.Now;
        }

        #endregion // CertAuthorityCheckProject
    }
}
