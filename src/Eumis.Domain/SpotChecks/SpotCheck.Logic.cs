using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.SpotChecks
{
    public partial class SpotCheck
    {
        #region SpotCheck

        public void UpdateCheckData(
            int? checkNum,
            string team,
            DateTime? dateFrom,
            DateTime? dateTo,
            string orderNum,
            DateTime? orderDate,
            string goal,
            string reportNum,
            DateTime? reportDate,
            DateTime? reportRecieveDate,
            int countryId,
            int? districtId,
            int? municipalityId,
            int? settlementId,
            string address)
        {
            this.AssertIsDraft();

            this.CheckNum = checkNum;
            this.RegNumber = checkNum.HasValue ? checkNum.ToString() : null;

            this.Team = team;
            this.DateFrom = dateFrom;
            this.DateTo = dateTo;
            this.OrderNum = orderNum;
            this.OrderDate = orderDate;
            this.Goal = goal;
            this.ReportNum = reportNum;
            this.ReportDate = reportDate;
            this.ReportRecieveDate = reportRecieveDate;

            this.CheckPlace.UpdateAttributes(countryId, districtId, municipalityId, settlementId, address);

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToEntered()
        {
            this.AssertIsDraft();

            this.Status = SpotCheckStatus.Entered;
            this.ModifyDate = DateTime.Now;
        }

        public void ChangeStatusToDraft()
        {
            this.AssertIsEntered();

            this.Status = SpotCheckStatus.Draft;
            this.ModifyDate = DateTime.Now;
        }

        private void AssertIsDraft()
        {
            if (this.Status != SpotCheckStatus.Draft)
            {
                throw new DomainValidationException("Spot check status must be draft!");
            }
        }

        private void AssertIsEntered()
        {
            if (this.Status != SpotCheckStatus.Entered)
            {
                throw new DomainValidationException("Spot check status must be entered!");
            }
        }

        #endregion //SpotCheck

        #region SpotCheckItem
        public void AddSpotCheckItem(int itemId)
        {
            this.AssertIsDraft();

            if (this.Level == SpotCheckLevel.Programme)
            {
                throw new DomainValidationException("SpotCheck cannot be associated with multiple programmes.");
            }

            var newItem = new SpotCheckItem()
            {
                SpotCheckId = this.SpotCheckId,
            };

            switch (this.Level)
            {
                case SpotCheckLevel.ProgrammePriority:
                    newItem.Level = SpotCheckItemLevel.ProgrammePriority;
                    newItem.ProgrammePriorityId = itemId;
                    break;
                case SpotCheckLevel.Procedure:
                    newItem.Level = SpotCheckItemLevel.Procedure;
                    newItem.ProcedureId = itemId;
                    break;
                case SpotCheckLevel.Contract:
                    newItem.Level = SpotCheckItemLevel.Contract;
                    newItem.ContractId = itemId;
                    break;
                case SpotCheckLevel.ContractContract:
                    newItem.Level = SpotCheckItemLevel.ContractContract;
                    newItem.ContractContractId = itemId;
                    break;
            }

            this.Items.Add(newItem);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveSpotCheckItem(int itemId)
        {
            this.AssertIsDraft();

            var item = this.Items.Single(li => li.SpotCheckItemId == itemId);
            this.Items.Remove(item);

            this.ModifyDate = DateTime.Now;
        }
        #endregion //SpotCheckItem

        #region SpotCheckAscertainment

        public SpotCheckAscertainment AddSpotCheckAscertainment(
            SpotCheckAscertainmentType type,
            string ascertainment,
            SpotCheckAscertainmentStatus status,
            string checkSubjectComment,
            string managingAuthorityComment,
            IList<int> items)
        {
            this.AssertIsDraft();

            if (this.Level == SpotCheckLevel.Programme && items.Count != 0)
            {
                throw new DomainValidationException("SpotCheckAscertainment with level programme does not have associated items.");
            }

            if (this.Level != SpotCheckLevel.Programme && items.Count == 0)
            {
                throw new DomainValidationException("SpotCheckAscertainment must have associated items.");
            }

            var orderNumber = (this.Ascertainments.Max(a => (int?)a.OrderNumber) ?? 0) + 1;
            var newAscertainment = new SpotCheckAscertainment()
            {
                SpotCheckId = this.SpotCheckId,
                OrderNumber = orderNumber,
                Type = type,
                Ascertainment = ascertainment,
                Status = status,
                CheckSubjectComment = checkSubjectComment,
                ManagingAuthorityComment = managingAuthorityComment,
            };

            var checkItemIds = this.Items.Select(i => i.SpotCheckItemId);
            foreach (var item in items)
            {
                if (!checkItemIds.Contains(item))
                {
                    throw new DomainValidationException("Invalid item id.");
                }

                newAscertainment.Items.Add(new SpotCheckAscertainmentItem
                {
                    SpotCheckItemId = item,
                });
            }

            this.Ascertainments.Add(newAscertainment);

            this.ModifyDate = DateTime.Now;

            return newAscertainment;
        }

        public SpotCheckAscertainment GetSpotCheckAscertainment(int ascertainmentId)
        {
            var ascertainment = this.Ascertainments.Single(a => a.SpotCheckAscertainmentId == ascertainmentId);

            if (ascertainment == null)
            {
                throw new DomainObjectNotFoundException("Cannot find SpotCheckAscertainment with id " + ascertainmentId);
            }

            return ascertainment;
        }

        public void UpdateSpotCheckAscertainment(
            int ascertainmentId,
            SpotCheckAscertainmentType type,
            string ascertainment,
            SpotCheckAscertainmentStatus status,
            string checkSubjectComment,
            string managingAuthorityComment,
            IList<int> items)
        {
            this.AssertIsDraft();

            if (this.Level == SpotCheckLevel.Programme && items.Count != 0)
            {
                throw new DomainValidationException("SpotCheckAscertainment with level programme does not have associated items.");
            }

            if (this.Level != SpotCheckLevel.Programme && items.Count == 0)
            {
                throw new DomainValidationException("SpotCheckAscertainment must have associated items.");
            }

            var spotCheckAscertainment = this.GetSpotCheckAscertainment(ascertainmentId);
            spotCheckAscertainment.SetAttributes(
                type,
                ascertainment,
                status,
                checkSubjectComment,
                managingAuthorityComment);

            var currentAscertainmentIds = spotCheckAscertainment.Items.Select(i => i.SpotCheckItemId);
            var auditItemIds = this.Items.Select(i => i.SpotCheckItemId);
            var newItemIds = items.Where(i => !currentAscertainmentIds.Contains(i)).ToList();
            foreach (var newItemId in newItemIds)
            {
                if (!auditItemIds.Contains(newItemId))
                {
                    throw new DomainValidationException("Invalid item id.");
                }

                spotCheckAscertainment.Items.Add(new SpotCheckAscertainmentItem
                {
                    SpotCheckAscertainmentId = spotCheckAscertainment.SpotCheckAscertainmentId,
                    SpotCheckItemId = newItemId,
                });
            }

            var removedItemIds = currentAscertainmentIds.Where(i => !items.Contains(i)).ToList();
            foreach (var removedItemId in removedItemIds)
            {
                var item = spotCheckAscertainment.Items.Single(i => i.SpotCheckItemId == removedItemId);
                spotCheckAscertainment.Items.Remove(item);
            }

            this.ModifyDate = DateTime.Now;
        }

        public IList<string> CanRemoveSpotCheckAscertainment(int ascertainmentId)
        {
            IList<string> errors = new List<string>();

            if (this.Recommendations.Any(r => r.Ascertainments.Select(a => a.SpotCheckAscertainmentId).Contains(ascertainmentId)))
            {
                errors.Add("Не можете да изтриете констатация, която е свързана с препоръки.");
            }

            return errors;
        }

        public void RemoveSpotCheckAscertainment(int ascertainmentId)
        {
            this.AssertIsDraft();

            if (!this.CanRemoveSpotCheckAscertainment(ascertainmentId).Any())
            {
                throw new DomainValidationException("Cannot remove SpotCheckAscertainment.");
            }

            var ascertainment = this.GetSpotCheckAscertainment(ascertainmentId);
            this.Ascertainments.Remove(ascertainment);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //SpotCheckAscertainment

        #region SpotCheckRecommendation

        public SpotCheckRecommendation AddSpotCheckRecommendation(
            string recommendation,
            DateTime? deadline,
            SpotCheckRecommendationStatus? executionStatus,
            DateTime? statusDate,
            DateTime? executionDate,
            DateTime? executionProofDate)
        {
            this.AssertIsDraft();

            var orderNumber = (this.Recommendations.Max(r => (int?)r.OrderNumber) ?? 0) + 1;
            var newRecommendation = new SpotCheckRecommendation()
            {
                SpotCheckId = this.SpotCheckId,
                OrderNumber = orderNumber,
                Recommendation = recommendation,
                Deadline = deadline,
                ExecutionStatus = executionStatus,
                StatusDate = statusDate,
                ExecutionDate = executionDate,
                ExecutionProofDate = executionProofDate,
            };

            this.Recommendations.Add(newRecommendation);

            this.ModifyDate = DateTime.Now;

            return newRecommendation;
        }

        public SpotCheckRecommendation GetSpotCheckRecommendation(int recommendationId)
        {
            var recommendation = this.Recommendations.Single(a => a.SpotCheckRecommendationId == recommendationId);

            if (recommendation == null)
            {
                throw new DomainObjectNotFoundException("Cannot find SpotCheckRecommendation with id " + recommendationId);
            }

            return recommendation;
        }

        public void UpdateSpotCheckRecommendation(
            int recommendationId,
            string recommendation,
            DateTime? deadline,
            SpotCheckRecommendationStatus? executionStatus,
            DateTime? statusDate,
            DateTime? executionDate,
            DateTime? executionProofDate)
        {
            this.AssertIsDraft();

            var checkRecommendation = this.GetSpotCheckRecommendation(recommendationId);
            checkRecommendation.SetAttributes(
                recommendation,
                deadline,
                executionStatus,
                statusDate,
                executionDate,
                executionProofDate);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveSpotCheckRecommendation(int recommendationId)
        {
            this.AssertIsDraft();

            var checkRecommendation = this.GetSpotCheckRecommendation(recommendationId);
            this.Recommendations.Remove(checkRecommendation);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //SpotCheckRecommendation

        #region SpotCheckAscertainmentRecommendation
        public void AddRecommendationAscertainments(int recommendationId, int[] ascertainmentIds)
        {
            this.AssertIsDraft();

            var recommendation = this.GetSpotCheckRecommendation(recommendationId);

            foreach (var ascertainmentId in ascertainmentIds)
            {
                if (!this.Ascertainments.Any(a => a.SpotCheckAscertainmentId == ascertainmentId))
                {
                    throw new DomainValidationException("Cannot add Ascertainment that is not in the current check.");
                }

                if (recommendation.Ascertainments.Any(a => a.SpotCheckAscertainmentId == ascertainmentId))
                {
                    throw new DomainValidationException("Cannot add Ascertainment twice in one Recommendation.");
                }

                recommendation.Ascertainments.Add(new SpotCheckRecommendationAscertainment
                {
                    SpotCheckAscertainmentId = ascertainmentId,
                    SpotCheckRecommendationId = recommendationId,
                });
            }

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveRecommendationAscertainment(int recommendationId, int itemId)
        {
            this.AssertIsDraft();

            var recommendation = this.GetSpotCheckRecommendation(recommendationId);
            var ascertainment = recommendation.Ascertainments.Single(a => a.SpotCheckRecommendationAscertainmentId == itemId);

            recommendation.Ascertainments.Remove(ascertainment);
            this.ModifyDate = DateTime.Now;
        }
        #endregion //SpotCheckAscertainmentRecommendation

        #region SpotCheckTarget

        public void AddSpotCheckTarget(SpotCheckTargetType type, string name)
        {
            this.AssertIsDraft();

            this.Targets.Add(new SpotCheckTarget()
            {
                SpotCheckId = this.SpotCheckId,
                Type = type,
                Name = name,
            });

            this.ModifyDate = DateTime.Now;
        }

        public SpotCheckTarget GetSpotCheckTarget(int targetId)
        {
            var target = this.Targets.Single(d => d.SpotCheckTargetId == targetId);

            if (target == null)
            {
                throw new DomainObjectNotFoundException("Cannot find SpotCheckTarget with id " + targetId);
            }

            return target;
        }

        public void UpdateSpotCheckTarget(int targetId, SpotCheckTargetType type, string name)
        {
            this.AssertIsDraft();

            var target = this.GetSpotCheckTarget(targetId);
            target.UpdateAttributes(type, name);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveSpotCheckTarget(int targetId)
        {
            this.AssertIsDraft();

            var target = this.GetSpotCheckTarget(targetId);
            this.Targets.Remove(target);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //SpotCheckTarget

        #region SpotCheckDoc

        public void AddSpotCheckDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            this.Documents.Add(new SpotCheckDoc()
            {
                SpotCheckId = this.SpotCheckId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public SpotCheckDoc GetSpotCheckDocument(int documentId)
        {
            var document = this.Documents.Single(d => d.SpotCheckDocId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find SpotCheckDoc with id " + documentId);
            }

            return document;
        }

        public void UpdateSpotCheckDocument(
            int documentId,
            string description,
            string fileName,
            Guid fileKey)
        {
            this.AssertIsDraft();

            var document = this.GetSpotCheckDocument(documentId);
            document.SetAttributes(description, fileName, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveSpotCheckDocument(int documentId)
        {
            this.AssertIsDraft();

            var document = this.GetSpotCheckDocument(documentId);
            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //SpotCheckDoc
    }
}
