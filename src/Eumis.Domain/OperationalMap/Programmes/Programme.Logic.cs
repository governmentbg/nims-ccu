using System;
using System.Collections.Generic;
using System.Linq;
using Eumis.Domain.Core;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.OperationalMap.MapNodes;
using Eumis.Domain.OperationalMap.ProcedureManuals;

namespace Eumis.Domain.OperationalMap.Programmes
{
    public partial class Programme
    {
        #region Programme

        public void UpdateProgramme(
            string name,
            string nameAlt,
            string description,
            string descriptionAlt,
            int? companyId)
        {
            this.AssertIsNotCanceled();

            this.Name = name;
            this.NameAlt = nameAlt;
            this.Description = description;
            this.DescriptionAlt = descriptionAlt;
            this.CompanyId = companyId;

            var programmeDataChangedEvent = new ProgrammeNotificationEvent(NotificationEventType.ProgrammeDataChanged, this.MapNodeId, this.MapNodeId);
            ((INotificationEventEmitter)this).NotificationEvents.Add(programmeDataChangedEvent);

            this.ModifyDate = DateTime.Now;
        }

        #endregion //Programme

        #region MapNodeRelation

        public void AddProgrammeRelation()
        {
            this.AssertIsNotCanceled();

            this.MapNodeRelation = new MapNodeRelation
            {
                MapNodeId = this.MapNodeId,
                ProgrammeId = this.MapNodeId,
            };
        }

        #endregion //MapNodeRelation

        #region ProgrammeProcedureManual

        public ProgrammeProcedureManual FindProgrammeProcedureManual(int programmeProcedureManualId)
        {
            var programmeProcedureManual = this.ProgrammeProcedureManuals.Where(e => e.ProgrammeProcedureManualId == programmeProcedureManualId).SingleOrDefault();

            if (programmeProcedureManual == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProgrammeProcedureManual with id " + programmeProcedureManualId);
            }

            return programmeProcedureManual;
        }

        public IList<string> CanAddProgrammeProcedureManual()
        {
            var errors = new List<string>();

            if (this.ProgrammeProcedureManuals.Count() == 0)
            {
                return errors;
            }

            var programmeHasProcedureManualInDraft = this.ProgrammeProcedureManuals.Any(pm => pm.Status == ProgrammeProcedureManualStatus.Draft);

            if (programmeHasProcedureManualInDraft)
            {
                errors.Add("Вече съществува процедурен наръчник за тази програма със статус 'Чернова'.");
            }

            return errors;
        }

        public void AddProgrammeProcedureManual(ProgrammeProcedureManual programmeProcedureManual)
        {
            this.AssertIsNotCanceled();

            this.ProgrammeProcedureManuals.Add(programmeProcedureManual);

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateProgrammeProcedureManual(
            int programmeProcedureManualId,
            string name,
            string description,
            Guid blobKey)
        {
            this.AssertIsNotCanceled();

            var programmeProcedureManual = this.FindProgrammeProcedureManual(programmeProcedureManualId);

            this.AssertProgrammeProcedureManualIsDraft(programmeProcedureManual);

            programmeProcedureManual.SetAttributes(name, description, blobKey);

            this.ModifyDate = DateTime.Now;
        }

        public void ChangeProgrammeProcedureManualStatusToActual(int programmeProcedureManualId, int userId)
        {
            this.AssertIsNotCanceled();

            var programmeProcedureManual = this.FindProgrammeProcedureManual(programmeProcedureManualId);

            this.AssertProgrammeProcedureManualIsDraft(programmeProcedureManual);

            if (this.ProgrammeProcedureManuals.Count > 1)
            {
                var currentActualProgrammeProcedureManual = this.ProgrammeProcedureManuals.Single(pm => pm.Status == ProgrammeProcedureManualStatus.Actual);

                currentActualProgrammeProcedureManual.ChangeStatus(ProgrammeProcedureManualStatus.Archived);
            }

            programmeProcedureManual.ChangeStatus(ProgrammeProcedureManualStatus.Actual, userId);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProgrammeProcedureManual(int programmeProcedureManualId)
        {
            this.AssertIsNotCanceled();

            var programmeProcedureManual = this.FindProgrammeProcedureManual(programmeProcedureManualId);

            this.AssertProgrammeProcedureManualIsDraft(programmeProcedureManual);

            this.ProgrammeProcedureManuals.Remove(programmeProcedureManual);

            this.ModifyDate = DateTime.Now;
        }

        public int GetProgrammeProcedureManualNextOrderNum()
        {
            int orderNum = 1;

            if (this.ProgrammeProcedureManuals.Count() > 0)
            {
                var lastOrderNum = this.ProgrammeProcedureManuals
                    .OrderByDescending(ppm => ppm.OrderNum)
                    .Select(ppm => ppm.OrderNum)
                    .First();

                orderNum = lastOrderNum + 1;
            }

            return orderNum;
        }

        private void AssertProgrammeProcedureManualIsDraft(ProgrammeProcedureManual programmeProcedureManual)
        {
            if (programmeProcedureManual.Status != ProgrammeProcedureManualStatus.Draft)
            {
                throw new DomainValidationException("ProgrammeProcedureManual status must be 'Draft'!");
            }
        }

        #endregion //ProgrammeProcedureManual

        #region ProgrammeApplicationDocument

        public ProgrammeApplicationDocument FindProgrammeApplicationDocument(int programmeApplicationDocumentId)
        {
            var programmeApplicationDocument = this.ProgrammeApplicationDocuments.Where(e => e.ProgrammeApplicationDocumentId == programmeApplicationDocumentId).SingleOrDefault();

            if (programmeApplicationDocument == null)
            {
                throw new DomainObjectNotFoundException("Cannot find ProgrammeApplicationDocument with id " + programmeApplicationDocumentId);
            }

            return programmeApplicationDocument;
        }

        public void AddProgrammeApplicationDocument(
            string name,
            string extension,
            bool isSignatureRequired)
        {
            this.AssertIsNotCanceled();

            this.ProgrammeApplicationDocuments.Add(new ProgrammeApplicationDocument(name, extension, isSignatureRequired));

            this.ModifyDate = DateTime.Now;
        }

        public void UpdateProgrammeApplicationDocument(
            int programmeApplicationDocumentId,
            string extension,
            bool isSignatureRequired)
        {
            this.AssertIsNotCanceled();

            var document = this.FindProgrammeApplicationDocument(programmeApplicationDocumentId);

            document.SetAttributes(extension, isSignatureRequired);

            this.ModifyDate = DateTime.Now;
        }

        public void RemoveProgrammeApplicationDocument(int programmeApplicationDocumentId)
        {
            this.AssertIsNotCanceled();

            var document = this.FindProgrammeApplicationDocument(programmeApplicationDocumentId);

            this.ProgrammeApplicationDocuments.Remove(document);

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProgrammeApplicationDocument(int programmeApplicationDocumentId)
        {
            this.AssertIsNotCanceled();

            var document = this.FindProgrammeApplicationDocument(programmeApplicationDocumentId);
            document.IsActive = true;

            this.ModifyDate = DateTime.Now;
        }

        public void DeactivateProgrammeApplicationDocument(int programmeApplicationDocumentId)
        {
            this.AssertIsNotCanceled();

            var document = this.FindProgrammeApplicationDocument(programmeApplicationDocumentId);
            document.IsActive = false;

            this.ModifyDate = DateTime.Now;
        }

        public bool IsApplicationDocumentExtensionChanged(int programmeApplicationDocumentId, string newExtension)
        {
            var document = this.FindProgrammeApplicationDocument(programmeApplicationDocumentId);

            return document.Extension != newExtension;
        }

        #endregion //ProgrammeApplicationDocument
    }
}
