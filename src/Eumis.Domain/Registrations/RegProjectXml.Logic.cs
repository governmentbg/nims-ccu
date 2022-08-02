using System;
using Eumis.Common;
using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.NonAggregates;
using Eumis.Domain.NotificationEvents;
using Eumis.Domain.Services;
using Eumis.Rio;

namespace Eumis.Domain.Registrations
{
    public partial class RegProjectXml
    {
        public override void SetXml(string xml)
        {
            throw new NotSupportedException();
        }

        public void SetXml(string xml, IProcedureDomainService procedureDomainService)
        {
            if (this.Status != RegProjectXmlStatus.Draft)
            {
                throw new DomainValidationException("Cannot update non-draft project's xml");
            }

            this.ModifyDate = DateTime.Now;
            this.SetXmlInt(xml, procedureDomainService);
        }

        private void SetXmlInt(string xml, IProcedureDomainService procedureDomainService)
        {
            base.SetXml(xml);

            var regDocument = this.GetDocument();
            var procedureCode = regDocument.Get(d => d.ProjectBasicData.Procedure.Code);
            this.ProcedureId = procedureDomainService.GetProcedureIdByCode(procedureCode);
            this.ProjectName = regDocument.Get(d => d.ProjectBasicData.Name).Truncate(400);
            this.ProjectNameAlt = regDocument.Get(d => d.ProjectBasicData.NameEN).Truncate(400);
            this.CompanyName = regDocument.Get(d => d.Candidate.Name).Truncate(200);
            this.CompanyNameAlt = regDocument.Get(d => d.Candidate.NameEN).Truncate(200);
        }

        public void MakeFinal()
        {
            if (this.Status != RegProjectXmlStatus.Draft)
            {
                throw new DomainValidationException("Cannot finalize non-draft project's xml");
            }

            this.ModifyDate = DateTime.Now;
            this.Status = RegProjectXmlStatus.Finalized;
        }

        public void MakeDraft()
        {
            if (this.Status != RegProjectXmlStatus.Finalized)
            {
                throw new DomainValidationException("Cannot definalize non-finalized project's xml");
            }

            //// TODO Validation

            this.ModifyDate = DateTime.Now;
            this.Status = RegProjectXmlStatus.Draft;
        }

        public void MakeSubmitted()
        {
            if (this.Status != RegProjectXmlStatus.Finalized)
            {
                throw new DomainValidationException("Cannot submit non-finalized project's xml");
            }

            this.ModifyDate = DateTime.Now;
            this.Status = RegProjectXmlStatus.Submitted;
        }

        public void MarkPaperRegistered(int projectId)
        {
            if (this.Status != RegProjectXmlStatus.Submitted)
            {
                throw new DomainValidationException("Cannot submit non-submitted project's xml");
            }

            this.ModifyDate = DateTime.Now;
            this.Status = RegProjectXmlStatus.Registered;
            this.RegistrationType = RegProjectXmlRegType.Paper;
            this.ProjectId = projectId;

            ((IEventEmitter)this).Events.Add(new ProjectRegisteredEvent() { RegProjectXmlId = this.RegProjectXmlId });
        }

        public void MakeRegistered(int projectId)
        {
            if (this.Status != RegProjectXmlStatus.Finalized)
            {
                throw new DomainValidationException("Cannot register non-finalized project's xml");
            }

            this.ModifyDate = DateTime.Now;
            this.Status = RegProjectXmlStatus.Registered;
            this.RegistrationType = RegProjectXmlRegType.Digital;
            this.ProjectId = projectId;

            ((IEventEmitter)this).Events.Add(new ProjectRegisteredEvent() { RegProjectXmlId = this.RegProjectXmlId });

            ((INotificationEventEmitter)this).NotificationEvents.Add(new ProcedureNotificationEvent(NotificationEventType.ProjectSubmitted, (int)this.ProjectId, this.ProcedureId));
        }
    }
}
