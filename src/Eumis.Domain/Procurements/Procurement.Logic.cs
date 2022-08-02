using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Procurements
{
    public partial class Procurement
    {
        public void SetAttributes(
            string name,
            string shortName,
            int? errandAreaId,
            int? errandLegalActId,
            int? errandTypeId,
            decimal? prognosysAmount,
            string description,
            string internetAddress,
            decimal? expectedAmount,
            string ppaNumber,
            DateTime? planDate,
            DateTime? offersDeadlineDate,
            DateTime? announcedDate)
        {
            this.AssertIsDraft();

            this.Name = name;
            this.ShortName = shortName;
            this.ErrandAreaId = errandAreaId;
            this.ErrandLegalActId = errandLegalActId;
            this.ErrandTypeId = errandTypeId;
            this.PrognosysAmount = prognosysAmount;
            this.Description = description;
            this.InternetAddress = internetAddress;
            this.ExpectedAmount = expectedAmount;
            this.PPANumber = ppaNumber;
            this.PlanDate = planDate;
            this.OffersDeadlineDate = offersDeadlineDate;
            this.AnnouncedDate = announcedDate;

            this.ModifyDate = DateTime.Now;
        }

        public void AssertIsDraft()
        {
            if (this.Status != ProcurementStatus.Draft)
            {
                throw new DomainException("Cannot edit Procurement when not in 'Draft' status");
            }
        }

        public void ChangeStatus(ProcurementStatus status)
        {
            this.Status = status;

            this.ModifyDate = DateTime.Now;
        }

        public List<string> CanAcivate()
        {
            var errors = new List<string>();
            if (!this.ErrandAreaId.HasValue)
            {
                errors.Add("Не е попълненo полето 'Обект на процедурата'");
            }

            if (!this.ErrandLegalActId.HasValue)
            {
                errors.Add("Не е попълнено полето 'Приложим нормативен акт'");
            }

            if (!this.ErrandTypeId.HasValue)
            {
                errors.Add("Не е попълненo полето 'Тип на процедурата'");
            }

            if (string.IsNullOrEmpty(this.PPANumber))
            {
                errors.Add("Не е попълненo полето 'Уникален номер от регистъра на АОП'");
            }

            return errors;
        }

        #region Documents

        public ProcurementDocument FindProcurementDocument(int procurementDocumentId)
        {
            var document = this.Documents
                .Where(x => x.ProcurementDocumentId == procurementDocumentId)
                .Single();

            return document;
        }

        public void UpdateProcurementDocument(int procurementDocumentId, string name, string description, Guid? fileKey)
        {
            this.AssertIsDraft();

            var document = this.FindProcurementDocument(procurementDocumentId);

            document.SetAttributes(name, description, fileKey);

            this.ModifyDate = DateTime.Now;
        }

        public ProcurementDocument CreateProcurementDocument(string name, string description, Guid? fileKey)
        {
            this.AssertIsDraft();

            var document = new ProcurementDocument();
            document.SetAttributes(name, description, fileKey);

            this.Documents.Add(document);

            this.ModifyDate = DateTime.Now;

            return document;
        }

        public void RemoveProcurementDocument(int documentId)
        {
            this.AssertIsDraft();

            var document = this.FindProcurementDocument(documentId);

            this.Documents.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        #endregion

        #region DifferentiatedPosition

        public ProcurementDifferentiatedPosition FindProcurementDifferentiatedPosition(int differentiatedPositionId)
        {
            var document = this.DifferentiatedPositions
                .Where(x => x.ProcurementDifferentiatedPositionId == differentiatedPositionId)
                .Single();

            return document;
        }

        public void UpdateProcurementDifferentiatedPosition(int differentiatedPositionId, string name, string comment,  int? companyId)
        {
            this.AssertIsDraft();

            var differentiatedPosition = this.FindProcurementDifferentiatedPosition(differentiatedPositionId);

            differentiatedPosition.SetAttributes(name, comment, companyId);

            this.ModifyDate = DateTime.Now;
        }

        public ProcurementDifferentiatedPosition CreateDifferentiatedPosition(string name, string description, int? companyId)
        {
            this.AssertIsDraft();

            var differentiatedPosition = new ProcurementDifferentiatedPosition(name, description);
            differentiatedPosition.CompanyId = companyId;

            this.DifferentiatedPositions.Add(differentiatedPosition);

            this.ModifyDate = DateTime.Now;

            return differentiatedPosition;
        }

        public void RemoveDifferentiatedPosition(int differentiatedPositionId)
        {
            this.AssertIsDraft();

            var differentiatedPosition = this.FindProcurementDifferentiatedPosition(differentiatedPositionId);

            this.DifferentiatedPositions.Remove(differentiatedPosition);

            this.ModifyDate = DateTime.Now;
        }

        #endregion
    }
}
