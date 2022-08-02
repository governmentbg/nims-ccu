using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Audits
{
    public partial class Audit
    {
        #region Audit

        public void UpdateAuditData(
            AuditInstitution auditInstitution,
            AuditType auditType,
            AuditKind auditKind,
            DateTime? finalReportDate,
            string finalReportNum,
            DateTime? checkStartDate,
            DateTime? checkEndDate,
            AuditSubjectType? auditSubjectType,
            string auditSubjectName,
            string comment)
        {
            this.AuditInstitution = auditInstitution;
            this.AuditType = auditType;
            this.AuditKind = auditKind;
            this.FinalReportDate = finalReportDate;
            this.FinalReportNum = finalReportNum;
            this.CheckStartDate = checkStartDate;
            this.CheckEndDate = checkEndDate;
            this.AuditSubjectType = auditSubjectType;
            this.AuditSubjectName = auditSubjectName;
            this.Comment = comment;

            this.ModifyDate = DateTime.Now;
        }

        #endregion //Audit

        #region AuditAscertainment

        public AuditAscertainment AddAscertainment(
            int nextOrderNum,
            string ascertainment,
            string recommendation,
            bool? recommendationsFulfilled,
            bool isFinancial,
            decimal? financialSum,
            IList<int> items)
        {
            if (this.Level == AuditLevel.Programme && items.Count != 0)
            {
                throw new DomainValidationException("AuditAscertainment with level programme does not have associated items.");
            }

            if (this.Level != AuditLevel.Programme && items.Count == 0)
            {
                throw new DomainValidationException("AuditAscertainment must have associated items.");
            }

            var newAscertainment = new AuditAscertainment()
            {
                AuditId = this.AuditId,
                OrderNum = nextOrderNum,
                Ascertainment = ascertainment,
                Recommendation = recommendation,
                RecommendationsFulfilled = recommendationsFulfilled,
                IsFinancial = isFinancial,
                FinancialSum = financialSum,
            };

            var auditItemIds = this.LevelItems.Select(i => i.AuditLevelItemId);
            foreach (var item in items)
            {
                if (!auditItemIds.Contains(item))
                {
                    throw new DomainValidationException("Invalid item id.");
                }

                newAscertainment.Items.Add(new AuditAscertainmentItem
                {
                    AuditLevelItemId = item,
                });
            }

            this.Ascertainments.Add(newAscertainment);

            this.ModifyDate = DateTime.Now;

            return newAscertainment;
        }

        public AuditAscertainment GetAscertainment(int ascertainmentId)
        {
            var ascertainment = this.Ascertainments.Single(a => a.AuditAscertainmentId == ascertainmentId);

            if (ascertainment == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AuditAscertainment with id " + ascertainmentId);
            }

            return ascertainment;
        }

        public void UpdateAscertainment(
            int ascertainmentId,
            string ascertainment,
            string recommendation,
            bool? recommendationsFulfilled,
            bool isFinancial,
            decimal? financialSum,
            IList<int> items)
        {
            if (this.Level == AuditLevel.Programme && items.Count != 0)
            {
                throw new DomainValidationException("AuditAscertainment with level programme does not have associated items.");
            }

            if (this.Level != AuditLevel.Programme && items.Count == 0)
            {
                throw new DomainValidationException("AuditAscertainment must have associated items.");
            }

            var auditAscertainment = this.GetAscertainment(ascertainmentId);
            auditAscertainment.SetAttributes(
                ascertainment,
                recommendation,
                recommendationsFulfilled,
                isFinancial,
                financialSum);

            var currentAscertainmentIds = auditAscertainment.Items.Select(i => i.AuditLevelItemId);
            var auditItemIds = this.LevelItems.Select(i => i.AuditLevelItemId);
            var newItemIds = items.Where(i => !currentAscertainmentIds.Contains(i)).ToList();
            foreach (var newItemId in newItemIds)
            {
                if (!auditItemIds.Contains(newItemId))
                {
                    throw new DomainValidationException("Invalid item id.");
                }

                auditAscertainment.Items.Add(new AuditAscertainmentItem
                {
                    AuditAscertainmentId = auditAscertainment.AuditAscertainmentId,
                    AuditLevelItemId = newItemId,
                });
            }

            var removedItemIds = currentAscertainmentIds.Where(i => !items.Contains(i)).ToList();
            foreach (var removedItemId in removedItemIds)
            {
                var item = auditAscertainment.Items.Single(i => i.AuditLevelItemId == removedItemId);
                auditAscertainment.Items.Remove(item);
            }

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveAscertainment(int ascertainmentId)
        {
            var ascertainment = this.GetAscertainment(ascertainmentId);
            this.Ascertainments.Remove(ascertainment);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //AuditAscertainment

        #region AuditLevelItem
        public void AddAuditLevelItem(int itemId)
        {
            if (this.Level == AuditLevel.Programme)
            {
                throw new DomainValidationException("Audits cannot be associated with multiple programmes.");
            }

            var newItem = new AuditLevelItem()
            {
                AuditId = this.AuditId,
            };

            switch (this.Level)
            {
                case AuditLevel.ProgrammePriority:
                    newItem.Level = AuditItemLevel.ProgrammePriority;
                    newItem.ProgrammePriorityId = itemId;
                    break;
                case AuditLevel.Procedure:
                    newItem.Level = AuditItemLevel.Procedure;
                    newItem.ProcedureId = itemId;
                    break;
                case AuditLevel.Contract:
                    newItem.Level = AuditItemLevel.Contract;
                    newItem.ContractId = itemId;
                    break;
                case AuditLevel.ContractContract:
                    newItem.Level = AuditItemLevel.ContractContract;
                    newItem.ContractContractId = itemId;
                    break;
            }

            this.LevelItems.Add(newItem);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveAuditLevelItem(int itemId)
        {
            var item = this.LevelItems.Single(li => li.AuditLevelItemId == itemId);
            this.LevelItems.Remove(item);

            this.ModifyDate = DateTime.Now;
        }
        #endregion //AuditLevelItem

        #region AuditDoc

        public void AddDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.Documents.Add(new AuditDoc()
            {
                AuditId = this.AuditId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public AuditDoc GetDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.AuditDocId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AuditDoc with id " + documentId);
            }

            return document;
        }

        public void UpdateDocument(
            int documentId,
            string description,
            string fileName,
            Guid fileKey)
        {
            var document = this.GetDocument(documentId);
            document.SetAttributes(description, fileName, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveDocument(int documentId)
        {
            var document = this.GetDocument(documentId);
            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //AuditDoc

        #region AuditProject

        public AuditProject FindAuditProject(int projectId)
        {
            var ap = this.Projects.Where(d => d.ProjectId == projectId).SingleOrDefault();

            if (ap == null)
            {
                throw new DomainObjectNotFoundException("Cannot find AuditProject with id " + projectId);
            }

            return ap;
        }

        public void AddAuditProject(int projectId)
        {
            var newItem = new AuditProject()
            {
                AuditId = this.AuditId,
                ProjectId = projectId,
            };

            this.Projects.Add(newItem);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveAuditProject(int projectId)
        {
            var cacp = this.FindAuditProject(projectId);

            this.Projects.Remove(cacp);
            this.ModifyDate = DateTime.Now;
        }

        #endregion // AuditProject
    }
}
