using Eumis.Domain.Core;
using Eumis.Domain.Events;
using Eumis.Domain.RioExtensions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Eumis.Domain.Projects
{
    public partial class ProjectVersionXml
    {
        #region ProjectVersionXml

        public override void SetXml(string xml)
        {
            if (this.Status != ProjectVersionXmlStatus.Draft)
            {
                throw new DomainValidationException("Cannot update non-draft project's xml");
            }

            this.SetXmlInt(xml);
        }

        private void SetXmlInt(string xml)
        {
            this.ModifyDate = DateTime.Now;
            base.SetXml(xml);

            // get the document after the xml is set
            var versionDoc = this.GetDocument();

            this.SetAmounts(versionDoc);
        }

        private void SetXmlInt(Rio.Project versionDoc)
        {
            this.SetAmounts(versionDoc);
            this.SetXml(versionDoc);
        }

        private void SetAmounts(Rio.Project versionDoc)
        {
            var budget = versionDoc.GetBudget();

            var totalBfpAmount = budget.Select(b => b.GrandAmount).Aggregate(0M, (a, b) => a + b);
            var coFinancingAmount = budget.Select(b => b.SelfAmount).Aggregate(0M, (a, b) => a + b);

            this.TotalBfpAmount = totalBfpAmount;
            this.CoFinancingAmount = coFinancingAmount;
        }

        public void SetAttributes(string createNote)
        {
            this.CreateNote = createNote;

            this.ModifyDate = DateTime.Now;
        }

        public void ActivateProjectVersion(bool isServiceContractActivation = false)
        {
            this.AssertCanActivateProjectVersion();

            this.Status = ProjectVersionXmlStatus.Actual;
            this.ModifyDate = DateTime.Now;

            if (!isServiceContractActivation)
            {
                ((IEventEmitter)this).Events.Add(new ProjectVersionActivatedEvent() { ProjectVersionId = this.ProjectVersionXmlId });
            }
        }

        public void ArchiveProjectVersion()
        {
            this.AssertIsActualProjectVersion();

            this.Status = ProjectVersionXmlStatus.Archive;

            this.ModifyDate = DateTime.Now;
        }

        public IList<Rio.Company> GetCompanies()
        {
            var document = this.GetDocument();

            var companies = new List<Rio.Company>();
            companies.Add(document.Candidate);

            if (document.Partners != null &&
                document.Partners.PartnerCollection != null &&
                document.Partners.PartnerCollection.Count > 0)
            {
                companies.AddRange(document.Partners.PartnerCollection);
            }

            return companies;
        }

        #endregion ProjectVersionXml

        #region Automatic ProjectVersionXml

        public void SetLastBudgetRowAmounts(Rio.Project document, decimal amount)
        {
            var programmeDetailsExpenseBudget = document
                .DirectionsBudgetContractCollection
                .Last()
                .Budget
                .ProgrammeBudgetCollection
                .Last()
                .ProgrammeExpenseBudgetCollection
                .Last()
                .ProgrammeDetailsExpenseBudgetCollection
                .Last();

            programmeDetailsExpenseBudget.GrandAmount = amount;
            programmeDetailsExpenseBudget.TotalAmount = amount;

            var directionsBudgetContract = document.DirectionsBudgetContractCollection.Last();

            directionsBudgetContract.Contract.TotalProjectCost = amount;

            this.SetXml(document);
        }

        #endregion Automatic ProjectVersionXml

        #region Private

        private void AssertCanActivateProjectVersion()
        {
            if (this.Status != ProjectVersionXmlStatus.Draft && this.Status != ProjectVersionXmlStatus.Archive)
            {
                throw new DomainValidationException("ProjectVersionXml status must be draft or archived");
            }
        }

        private void AssertIsDraftProjectVersion()
        {
            if (this.Status != ProjectVersionXmlStatus.Draft)
            {
                throw new DomainValidationException("ProjectVersionXml status must be draft");
            }
        }

        private void AssertIsActualProjectVersion()
        {
            if (this.Status != ProjectVersionXmlStatus.Actual)
            {
                throw new DomainValidationException("ProjectVersionXml status must be actual");
            }
        }

        #endregion Private
    }
}
