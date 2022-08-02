using System;
using System.Linq;

namespace Eumis.Domain.SpotChecks
{
    public partial class SpotCheckPlan
    {
        #region SpotCheckPlan

        public void UpdatePlanData(
            bool isConfirmed,
            DateTime? confirmationDate,
            string confirmationOrder,
            int countryId,
            int? districtId,
            int? municipalityId,
            int? settlementId,
            string address)
        {
            this.IsConfirmed = isConfirmed;
            this.ConfirmationDate = confirmationDate;
            this.ConfirmationOrder = confirmationOrder;
            this.CheckPlace.UpdateAttributes(countryId, districtId, municipalityId, settlementId, address);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //SpotCheckPlan

        #region SpotCheckPlanItem
        public void AddSpotCheckPlanItem(int itemId)
        {
            if (this.Level == SpotCheckLevel.Programme)
            {
                throw new DomainValidationException("SpotCheckPlan cannot be associated with multiple programmes.");
            }

            var newItem = new SpotCheckPlanItem()
            {
                SpotCheckPlanId = this.SpotCheckPlanId,
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

        public void RemoveSpotCheckPlanItem(int itemId)
        {
            var item = this.Items.Single(li => li.SpotCheckPlanItemId == itemId);
            this.Items.Remove(item);

            this.ModifyDate = DateTime.Now;
        }
        #endregion //SpotCheckPlanItem

        #region SpotCheckPlanTarget

        public void AddSpotCheckPlanTarget(SpotCheckTargetType type, string name)
        {
            this.Targets.Add(new SpotCheckPlanTarget()
            {
                SpotCheckPlanId = this.SpotCheckPlanId,
                Type = type,
                Name = name,
            });

            this.ModifyDate = DateTime.Now;
        }

        public SpotCheckPlanTarget GetSpotCheckPlanTarget(int targetId)
        {
            var target = this.Targets.SingleOrDefault(d => d.SpotCheckPlanTargetId == targetId);

            if (target == null)
            {
                throw new DomainObjectNotFoundException("Cannot find SpotCheckPlanTarget with id " + targetId);
            }

            return target;
        }

        public void UpdateSpotCheckPlanTarget(int targetId, SpotCheckTargetType type, string name)
        {
            var target = this.GetSpotCheckPlanTarget(targetId);
            target.UpdateAttributes(type, name);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveSpotCheckPlanTarget(int targetId)
        {
            var target = this.GetSpotCheckPlanTarget(targetId);
            this.Targets.Remove(target);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //SpotCheckPlanTarget

        #region SpotCheckPlanDoc

        public void AddSpotCheckPlanDocument(
            string description,
            string fileName,
            Guid fileKey)
        {
            this.Documents.Add(new SpotCheckPlanDoc()
            {
                SpotCheckPlanId = this.SpotCheckPlanId,
                FileName = fileName,
                Description = description,
                FileKey = fileKey,
            });

            this.ModifyDate = DateTime.Now;
        }

        public SpotCheckPlanDoc GetSpotCheckPlanDocument(int documentId)
        {
            var document = this.Documents.SingleOrDefault(d => d.SpotCheckPlanDocId == documentId);

            if (document == null)
            {
                throw new DomainObjectNotFoundException("Cannot find SpotCheckPlanDoc with id " + documentId);
            }

            return document;
        }

        public void UpdateSpotCheckPlanDocument(
            int documentId,
            string description,
            string fileName,
            Guid fileKey)
        {
            var document = this.GetSpotCheckPlanDocument(documentId);
            document.SetAttributes(description, fileName, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveSpotCheckPlanDocument(int documentId)
        {
            var document = this.GetSpotCheckPlanDocument(documentId);
            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //SpotCheckPlanDoc
    }
}
